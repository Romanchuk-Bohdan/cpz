public interface ICommand
{
    void Execute();
    void Undo();
}

public class AddChildCommand : ICommand
{
    private readonly LightElementNode _parent;
    private readonly LightNode _child;

    public AddChildCommand(LightElementNode parent, LightNode child)
    {
        _parent = parent;
        _child = child;
    }

    public void Execute()
    {
        _parent.AddChild(_child);
    }

    public void Undo()
    {
        _parent.RemoveChild(_child);
    }
}

public class CommandInvoker
{
    private readonly Stack<ICommand> _history = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void UndoCommand()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            command.Undo();
        }
    }
}

public class LightElementNode : LightNode
{
    private readonly string _tagName;
    private readonly DisplayType _displayType;
    private readonly ClosingType _closingType;
    private readonly List<string> _cssClasses;
    public readonly List<LightNode> _children;

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

    public void RemoveChild(LightNode node)
    {
        _children.Remove(node);
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
        Console.WriteLine($"Елемент {_tagName} створено.");
    }

    public override void OnRendered()
    {
        Console.WriteLine($"Елемент {_tagName} відрендерено.");
    }
}