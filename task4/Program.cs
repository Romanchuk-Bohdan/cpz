using System;
using System.Collections.Generic;
using System.Text;

public interface IImageLoadStrategy
{
    void Load(string href);
}

public class NetworkImageLoadStrategy : IImageLoadStrategy
{
    public void Load(string href)
    {
        Console.WriteLine($"[Мережа] Завантаження зображення за URL: {href}");
    }
}

public class FileImageLoadStrategy : IImageLoadStrategy
{
    public void Load(string href)
    {
        Console.WriteLine($"[Файл] Завантаження зображення з локального шляху: {href}");
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
    protected readonly string _tagName;
    protected readonly DisplayType _displayType;
    protected readonly ClosingType _closingType;
    protected readonly List<string> _cssClasses;
    protected readonly List<LightNode> _children;
    protected readonly Dictionary<string, string> _attributes;

    public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
    {
        _tagName = tagName;
        _displayType = displayType;
        _closingType = closingType;
        _cssClasses = new List<string>();
        _children = new List<LightNode>();
        _attributes = new Dictionary<string, string>();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        _children.Add(node);
    }

    public void AddAttribute(string key, string value)
    {
        _attributes[key] = value;
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

            foreach (var attr in _attributes)
            {
                sb.Append($" {attr.Key}=\"{attr.Value}\"");
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

public class LightImageNode : LightElementNode
{
    private readonly IImageLoadStrategy _loadStrategy;

    public LightImageNode(string href) : base("img", DisplayType.Inline, ClosingType.Single)
    {
        AddAttribute("src", href);
        
        if (href.StartsWith("http://") || href.StartsWith("https://"))
        {
            _loadStrategy = new NetworkImageLoadStrategy();
        }
        else
        {
            _loadStrategy = new FileImageLoadStrategy();
        }
    }

    public void LoadImage()
    {
        string href = _attributes["src"];
        _loadStrategy.Load(href);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        LightElementNode div = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
        div.AddClass("gallery-container");

        LightImageNode webImage = new LightImageNode("https://example.com/images/photo.jpg");
        webImage.AddClass("web-img");

        LightImageNode localImage = new LightImageNode("C:/Images/avatar.png");
        localImage.AddClass("local-img");

        div.AddChild(webImage);
        div.AddChild(localImage);

        Console.WriteLine("--- Симуляція завантаження зображень ---\n");
        webImage.LoadImage();
        localImage.LoadImage();

        Console.WriteLine("\n--- HTML структура ---\n");
        Console.WriteLine(div.OuterHTML);
    }
}