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

    public virtual void OnCreated() { }
    public virtual void OnInserted() { }
    public virtual void OnRendered() { }

    public string Render()
    {
        OnCreated();
        string result = OuterHTML;
        OnRendered();
        return result;
    }
}

public class LightTextNode : LightNode
{
    private readonly string _text;

    public LightTextNode(string text)
    {
        _text = text;
        OnCreated();
    }

    public override string OuterHTML => _text;
    public override string InnerHTML => _text;

    public override void OnCreated()
    {
        Console.WriteLine("Створено текстовий вузол.");
    }

    public override void OnInserted()
    {
        Console.WriteLine("Текстовий вузол додано до дерева.");
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
        OnCreated();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        _children.Add(node);
        node.OnInserted();
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

    public override void OnCreated()
    {
        Console.WriteLine($"Створено елемент: {_tagName}");
    }

    public override void OnRendered()
    {
        Console.WriteLine($"Відрендерено елемент: {_tagName}");
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

        LightElementNode h1 = new LightElementNode("h1", DisplayType.Block, ClosingType.Paired);
        h1.AddChild(new LightTextNode("Мова розмітки LightHTML"));

        div.AddChild(h1);

        Console.WriteLine("\n--- Рендеринг головного контейнера ---");
        string result = div.Render();
        Console.WriteLine("\n" + result);
    }
}