using System;
using System.Collections.Generic;
using System.Text;

public enum DisplayType
{
    Block,
    Inline
}

public enum ClosingType
{
    Paired,
    Single
}

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

public class LightElementNode : LightNode
{
    private readonly string _tagName;
    private readonly DisplayType _displayType;
    private readonly ClosingType _closingType;
    private readonly List<string> _cssClasses;
    private readonly List<LightNode> _children;

    public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
    {
        _tagName = tagName;
        _displayType = displayType;
        _closingType = closingType;
        _cssClasses = new List<string>();
        _children = new List<LightNode>();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        _children.Add(node);
    }

    public int ChildrenCount => _children.Count;

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
            sb.Append("<").Append(_tagName);

            if (_cssClasses.Count > 0)
            {
                sb.Append(" class=\"").Append(string.Join(" ", _cssClasses)).Append("\"");
            }

            sb.Append(">");

            if (_closingType == ClosingType.Paired)
            {
                sb.Append(InnerHTML);
                sb.Append("</").Append(_tagName).Append(">");
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
        
        LightElementNode div = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
        div.AddClass("container");
        div.AddClass("dark-theme");

        LightElementNode h1 = new LightElementNode("h1", DisplayType.Block, ClosingType.Paired);
        h1.AddChild(new LightTextNode("Мова розмітки LightHTML"));

        LightElementNode hr = new LightElementNode("hr", DisplayType.Block, ClosingType.Single);

        LightElementNode ul = new LightElementNode("ul", DisplayType.Block, ClosingType.Paired);
        ul.AddClass("list-group");

        for (int i = 1; i <= 3; i++)
        {
            LightElementNode li = new LightElementNode("li", DisplayType.Block, ClosingType.Paired);
            li.AddClass("list-item");
            li.AddChild(new LightTextNode($"Елемент списку {i} "));

            if (i == 2)
            {
                LightElementNode strong = new LightElementNode("strong", DisplayType.Inline, ClosingType.Paired);
                strong.AddClass("highlight");
                strong.AddChild(new LightTextNode("(Важливий)"));
                li.AddChild(strong);
            }

            ul.AddChild(li);
        }

        div.AddChild(h1);
        div.AddChild(hr);
        div.AddChild(ul);

        Console.WriteLine("--- InnerHTML головного контейнера ---\n");
        Console.WriteLine(div.InnerHTML);
        
        Console.WriteLine("\n--- OuterHTML головного контейнера ---\n");
        Console.WriteLine(div.OuterHTML);
        
        Console.WriteLine($"\nКількість прямих дочірніх елементів у div: {div.ChildrenCount}");
    }
}