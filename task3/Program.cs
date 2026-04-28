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
    private readonly Dictionary<string, List<Action>> _eventListeners;

    public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
    {
        _tagName = tagName;
        _displayType = displayType;
        _closingType = closingType;
        _cssClasses = new List<string>();
        _children = new List<LightNode>();
        _eventListeners = new Dictionary<string, List<Action>>();
    }

    public void AddClass(string className)
    {
        _cssClasses.Add(className);
    }

    public void AddChild(LightNode node)
    {
        _children.Add(node);
    }

    public void AddEventListener(string eventType, Action listener)
    {
        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<Action>();
        }
        _eventListeners[eventType].Add(listener);
    }

    public void RemoveEventListener(string eventType, Action listener)
    {
        if (_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType].Remove(listener);
        }
    }

    public void DispatchEvent(string eventType)
    {
        if (_eventListeners.ContainsKey(eventType))
        {
            foreach (var listener in _eventListeners[eventType])
            {
                listener.Invoke();
            }
        }
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

        LightElementNode button = new LightElementNode("button", DisplayType.Inline, ClosingType.Paired);
        button.AddClass("btn-primary");
        button.AddChild(new LightTextNode("Зберегти"));

        LightElementNode img = new LightElementNode("img", DisplayType.Inline, ClosingType.Single);
        img.AddClass("profile-icon");

        div.AddChild(h1);
        div.AddChild(button);
        div.AddChild(img);

        Action onClick = () => Console.WriteLine("[Подія] Кнопку натиснуто!");
        Action onMouseOverBtn = () => Console.WriteLine("[Подія] Курсор наведено на кнопку!");
        Action onMouseOverImg = () => Console.WriteLine("[Подія] Курсор наведено на зображення!");

        button.AddEventListener("click", onClick);
        button.AddEventListener("mouseover", onMouseOverBtn);
        img.AddEventListener("mouseover", onMouseOverImg);

        Console.WriteLine("--- HTML структура ---");
        Console.WriteLine(div.OuterHTML);
        
        Console.WriteLine("\n--- Симуляція подій ---");
        button.DispatchEvent("click");
        button.DispatchEvent("mouseover");
        img.DispatchEvent("mouseover");

        Console.WriteLine("\n--- Видалення підписки ---");
        button.RemoveEventListener("click", onClick);
        
        Console.WriteLine("Спроба натиснути кнопку після видалення події 'click':");
        button.DispatchEvent("click"); 
    }
}