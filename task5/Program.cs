using System;
using System.Collections.Generic;

namespace TextEditorMemento
{
    public interface IMemento
    {
    }

    public class TextDocumentMemento : IMemento
    {
        public string Content { get; }

        public TextDocumentMemento(string content)
        {
            Content = content;
        }
    }

    public class TextDocument
    {
        public string Content { get; set; } = string.Empty;

        public IMemento Save()
        {
            return new TextDocumentMemento(Content);
        }

        public void Restore(IMemento memento)
        {
            if (memento is TextDocumentMemento textMemento)
            {
                Content = textMemento.Content;
            }
        }
    }

    public class TextEditor
    {
        private TextDocument _document;
        private Stack<IMemento> _history;

        public TextEditor()
        {
            _document = new TextDocument();
            _history = new Stack<IMemento>();
        }

        public void Write(string text)
        {
            _document.Content += text;
        }

        public void SetContent(string text)
        {
            _document.Content = text;
        }

        public void Print()
        {
            Console.WriteLine($"Поточний текст: \"{_document.Content}\"");
        }

        public void Save()
        {
            _history.Push(_document.Save());
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var memento = _history.Pop();
                _document.Restore(memento);
            }
            else
            {
                Console.WriteLine("Немає збережених станів для скасування.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var editor = new TextEditor();

            editor.Write("Рядок 1. ");
            editor.Print();
            editor.Save();

            editor.Write("Рядок 2. ");
            editor.Print();
            editor.Save();

            editor.SetContent("Абсолютно новий текст документа.");
            editor.Print();

            Console.WriteLine("\n--- Скасування (Undo) 1 ---");
            editor.Undo();
            editor.Print();

            Console.WriteLine("\n--- Скасування (Undo) 2 ---");
            editor.Undo();
            editor.Print();

            Console.WriteLine("\n--- Спроба скасування у порожній історії ---");
            editor.Undo();
            editor.Undo();
        }
    }
}