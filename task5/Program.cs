using System;
using System.Collections;
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

public class DepthFirstIterator : IEnumerator<LightNode>
{
    private readonly LightNode _root;
    private Stack<LightNode> _stack;
    private LightNode _current;

    public DepthFirstIterator(LightNode root)
    {
        _root = root;
        _stack = new Stack<LightNode>();
        _stack.Push(_root);
    }

    public LightNode Current => _current;

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_stack.Count == 0)
        {
            return false;
        }

        _current = _stack.Pop();

        if (_current is LightElementNode element)
        {
            for (int i = element.Children.Count - 1; i >= 0; i--)
            {
                _stack.Push(element.Children[i]);
            }
        }

        return true;
    }

    public void Reset()
    {
        _stack.Clear();
        _stack.Push(_root);
        _current = null;
    }

    public void Dispose() { }
}

public abstract class LightNode : IEnumerable<LightNode>
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }

    public IEnumerator<LightNode> GetEnumerator()
    {
        return new DepthFirstIterator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
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
    public string TagName { get; }
    private readonly DisplayType _displayType;
    private readonly ClosingType _closingType;
    private readonly List<string> _cssClasses;
    public List<LightNode> Children { get; }

    public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
    {
        TagName = tagName;
        _displayType = displayType;
        _closingType = closingType;
        _cssClasses = new List<string>();
        Children = new List<LightNode>();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        Children.Add(node);
    }

    public int ChildrenCount => Children.Count;

    public override string InnerHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in Children)
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
            sb.Append("<").Append(TagName);

            if (_cssClasses.Count > 0)
            {
                sb.Append(" class=\"").Append(string.Join(" ", _cssClasses)).Append("\"");
            }

            sb.Append(">");

            if (_closingType == ClosingType.Paired)
            {
                sb.Append(InnerHTML);
                sb.Append("</").Append(TagName).Append(">");
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
        LightElementNode h1 = new LightElementNode("h1", DisplayType.Block, ClosingType.Paired);
        h1.AddChild(new LightTextNode("Заголовок"));
        LightElementNode p = new LightElementNode("p", DisplayType.Block, ClosingType.Paired);
        p.AddChild(new LightTextNode("Текст абзацу"));

        div.AddChild(h1);
        div.AddChild(p);

        Console.WriteLine("Обхід дерева в глибину:");
        foreach (var node in div)
        {
            if (node is LightElementNode element)
            {
                Console.WriteLine($"Елемент: {element.TagName}");
            }
            else if (node is LightTextNode textNode)
            {
                Console.WriteLine($"Текст: {textNode.OuterHTML}");
            }
        }
    }
}