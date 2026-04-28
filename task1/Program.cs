using System;
using System.IO;
using System.Text;
using System.Text.Unicode;

public interface ILogger
{
    void Log(string message);
    void Error(string message);
    void Warn(string message);
}

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

public class FileWriter
{
    private readonly string _filePath;

    public FileWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void Write(string text)
    {
        File.AppendAllText(_filePath, text);
    }

    public void WriteLine(string text)
    {
        File.AppendAllText(_filePath, text + Environment.NewLine);
    }
}

public class FileLoggerAdapter : ILogger
{
    private readonly FileWriter _fileWriter;

    public FileLoggerAdapter(FileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }

    public void Log(string message)
    {
        _fileWriter.WriteLine("[LOG]: " + message);
    }

    public void Error(string message)
    {
        _fileWriter.WriteLine("[ERROR]: " + message);
    }

    public void Warn(string message)
    {
        _fileWriter.WriteLine("[WARN]: " + message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        ILogger consoleLogger = new Logger();
        
        consoleLogger.Log("Звичайне повідомлення у консоль");
        consoleLogger.Warn("Попередження у консоль");
        consoleLogger.Error("Помилка у консоль");

        Console.WriteLine();

        FileWriter fileWriter = new FileWriter("application_log.txt");
        ILogger fileLogger = new FileLoggerAdapter(fileWriter);

        fileLogger.Log("Звичайне повідомлення у файл");
        fileLogger.Warn("Попередження у файл");
        fileLogger.Error("Помилка у файл");

        Console.WriteLine("Запис у файл завершено. Перевірте application_log.txt");
    }
}