using System;
using System.Collections.Generic;
using System.Text;

public interface IVisitor
{
    void VisitElementNode(LightElementNode node);
    void VisitTextNode(LightTextNode node);
}

public class TextExtractionVisitor : IVisitor
{
    public StringBuilder ExtractedText { get; } = new StringBuilder();

    public void VisitElementNode(LightElementNode node)
    {
    }

    public void VisitTextNode(LightTextNode node)
    {
        ExtractedText.Append(node.OuterHTML).Append(" ");
    }
}

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
    public abstract void Accept(IVisitor visitor);
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

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitTextNode(this);
    }
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

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitElementNode(this);
        foreach (var child in _children)
        {
            child.Accept(visitor);
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
        LightElementNode h1 = new LightElementNode("h1", DisplayType.Block, ClosingType.Paired);
        h1.AddChild(new LightTextNode("Екстракція"));
        LightElementNode p = new LightElementNode("p", DisplayType.Block, ClosingType.Paired);
        p.AddChild(new LightTextNode("чистого тексту."));

        div.AddChild(h1);
        div.AddChild(p);

        TextExtractionVisitor visitor = new TextExtractionVisitor();
        div.Accept(visitor);

        Console.WriteLine("Згенерований HTML:");
        Console.WriteLine(div.OuterHTML);
        
        Console.WriteLine("\nВитягнутий текст через Visitor:");
        Console.WriteLine(visitor.ExtractedText.ToString().Trim());
    }
}