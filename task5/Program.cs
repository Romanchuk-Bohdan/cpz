using System;
using System.Collections.Generic;
using System.Text;

public interface INodeState
{
    string RenderTag(string tagName, string classes, string innerHtml, ClosingType closingType);
}

public class VisibleState : INodeState
{
    public string RenderTag(string tagName, string classes, string innerHtml, ClosingType closingType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<").Append(tagName);

        if (!string.IsNullOrEmpty(classes))
        {
            sb.Append(" class=\"").Append(classes).Append("\"");
        }

        sb.Append(">");

        if (closingType == ClosingType.Paired)
        {
            sb.Append(innerHtml);
            sb.Append("</").Append(tagName).Append(">");
        }

        return sb.ToString();
    }
}

public class HiddenState : INodeState
{
    public string RenderTag(string tagName, string classes, string innerHtml, ClosingType closingType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<").Append(tagName);

        if (!string.IsNullOrEmpty(classes))
        {
            sb.Append(" class=\"").Append(classes).Append("\"");
        }

        sb.Append(" style=\"display: none;\">");

        if (closingType == ClosingType.Paired)
        {
            sb.Append(innerHtml);
            sb.Append("</").Append(tagName).Append(">");
        }

        return sb.ToString();
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
    private INodeState _state;

    public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
    {
        _tagName = tagName;
        _displayType = displayType;
        _closingType = closingType;
        _cssClasses = new List<string>();
        _children = new List<LightNode>();
        _state = new VisibleState();
    }

    public void SetState(INodeState state)
    {
        _state = state;
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
            string classes = string.Join(" ", _cssClasses);
            return _state.RenderTag(_tagName, classes, InnerHTML, _closingType);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        LightElementNode alert = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
        alert.AddClass("alert-box");
        alert.AddChild(new LightTextNode("Важливе повідомлення!"));

        Console.WriteLine("Поточний стан: Видимий");
        Console.WriteLine(alert.OuterHTML);

        alert.SetState(new HiddenState());

        Console.WriteLine("\nПоточний стан: Прихований");
        Console.WriteLine(alert.OuterHTML);
    }
}