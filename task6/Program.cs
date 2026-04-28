using System;
using System.Collections.Generic;
using System.Text;

public enum DisplayType { Block, Inline }
public enum ClosingType { Paired, Single }

public abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

public class LightTextNode : LightNode
{
    private readonly string _text;

    public LightTextNode(string text)
    {
        _text = text;
    }

    public override string OuterHTML => _text;
    public override string InnerHTML => _text;
}

public class ElementMarkupFlyweight
{
    public string TagName { get; }
    public DisplayType DisplayType { get; }
    public ClosingType ClosingType { get; }

    public ElementMarkupFlyweight(string tagName, DisplayType displayType, ClosingType closingType)
    {
        TagName = tagName;
        DisplayType = displayType;
        ClosingType = closingType;
    }
}

public class FlyweightFactory
{
    private readonly Dictionary<string, ElementMarkupFlyweight> _flyweights = new Dictionary<string, ElementMarkupFlyweight>();

    public ElementMarkupFlyweight GetFlyweight(string tagName, DisplayType displayType, ClosingType closingType)
    {
        string key = $"{tagName}_{displayType}_{closingType}";
        if (!_flyweights.ContainsKey(key))
        {
            _flyweights[key] = new ElementMarkupFlyweight(tagName, displayType, closingType);
        }
        return _flyweights[key];
    }
    
    public int FlyweightsCount => _flyweights.Count;
}

public class TextNodeFactory
{
    private readonly Dictionary<string, LightTextNode> _textNodes = new Dictionary<string, LightTextNode>();

    public LightTextNode GetTextNode(string text)
    {
        if (!_textNodes.ContainsKey(text))
        {
            _textNodes[text] = new LightTextNode(text);
        }
        return _textNodes[text];
    }
}

public class LightElementNode : LightNode
{
    private readonly ElementMarkupFlyweight _markup;
    private readonly List<LightNode> _children;
    private readonly List<string> _cssClasses;

    public LightElementNode(ElementMarkupFlyweight markup)
    {
        _markup = markup;
        _children = new List<LightNode>();
        _cssClasses = new List<string>();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        _children.Add(node);
    }

    public override string InnerHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children)
            {
                sb.Append(child.OuterHTML);
            }
            return sb.ToString();
        }
    }

    public override string OuterHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<").Append(_markup.TagName);

            if (_cssClasses.Count > 0)
            {
                sb.Append(" class=\"").Append(string.Join(" ", _cssClasses)).Append("\"");
            }

            sb.Append(">");

            if (_markup.ClosingType == ClosingType.Paired)
            {
                sb.Append(InnerHTML);
                sb.Append("</").Append(_markup.TagName).Append(">");
            }

            return sb.ToString();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        int linesCount = 1000000;
        string[] bookText = new string[linesCount];
        
        for (int i = 0; i < linesCount; i++)
        {
            if (i == 0) bookText[i] = "Romeo and Juliet";
            else if (i % 10 == 0) bookText[i] = "ACT V";
            else if (i % 5 == 0) bookText[i] = "  Scene I. Mantua. A Street.";
            else bookText[i] = "Enter Romeo. If I may trust the flattering truth of sleep...";
        }

        GC.Collect();
        long memoryBefore = GC.GetTotalMemory(true);

        FlyweightFactory tagFactory = new FlyweightFactory();
        TextNodeFactory textFactory = new TextNodeFactory();
        
        ElementMarkupFlyweight divMarkup = tagFactory.GetFlyweight("div", DisplayType.Block, ClosingType.Paired);
        LightElementNode largeRoot = new LightElementNode(divMarkup);

        for (int i = 0; i < bookText.Length; i++)
        {
            string line = bookText[i];
            ElementMarkupFlyweight currentMarkup;

            if (i == 0)
            {
                currentMarkup = tagFactory.GetFlyweight("h1", DisplayType.Block, ClosingType.Paired);
            }
            else if (line.Length < 20)
            {
                currentMarkup = tagFactory.GetFlyweight("h2", DisplayType.Block, ClosingType.Paired);
            }
            else if (line.StartsWith(" ") || line.StartsWith("\t"))
            {
                currentMarkup = tagFactory.GetFlyweight("blockquote", DisplayType.Block, ClosingType.Paired);
            }
            else
            {
                currentMarkup = tagFactory.GetFlyweight("p", DisplayType.Block, ClosingType.Paired);
            }

            LightElementNode lineNode = new LightElementNode(currentMarkup);
            lineNode.AddChild(textFactory.GetTextNode(line));
            largeRoot.AddChild(lineNode);
        }

        GC.Collect();
        long memoryAfter = GC.GetTotalMemory(true);

        Console.WriteLine($"Пам'ять для дерева з {linesCount} вузлів: {(memoryAfter - memoryBefore) / (1024.0 * 1024.0):F2} MB");
        Console.WriteLine($"Унікальних структур тегів у пам'яті: {tagFactory.FlyweightsCount}");

        Console.WriteLine("\n--- Демонстрація правильності (прев'ю) ---");
        
        string[] sampleText = new string[] 
        {
            "Romeo and Juliet",
            "ACT V",
            "  Scene I. Mantua. A Street.",
            "Enter Romeo. If I may trust the flattering truth of sleep..."
        };

        LightElementNode sampleRoot = new LightElementNode(divMarkup);
        
        for (int i = 0; i < sampleText.Length; i++)
        {
            string line = sampleText[i];
            ElementMarkupFlyweight currentMarkup;

            if (i == 0) currentMarkup = tagFactory.GetFlyweight("h1", DisplayType.Block, ClosingType.Paired);
            else if (line.Length < 20) currentMarkup = tagFactory.GetFlyweight("h2", DisplayType.Block, ClosingType.Paired);
            else if (line.StartsWith(" ") || line.StartsWith("\t")) currentMarkup = tagFactory.GetFlyweight("blockquote", DisplayType.Block, ClosingType.Paired);
            else currentMarkup = tagFactory.GetFlyweight("p", DisplayType.Block, ClosingType.Paired);

            LightElementNode lineNode = new LightElementNode(currentMarkup);
            lineNode.AddChild(textFactory.GetTextNode(line));
            sampleRoot.AddChild(lineNode);
        }

        Console.WriteLine(sampleRoot.OuterHTML);
    }
}