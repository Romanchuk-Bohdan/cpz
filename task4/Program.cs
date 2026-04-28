using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public interface ITextReader
{
    char[][] ReadText(string filePath);
}

public class SmartTextReader : ITextReader
{
    public char[][] ReadText(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        char[][] result = new char[lines.Length][];
        
        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].ToCharArray();
        }
        
        return result;
    }
}

public class SmartTextChecker : ITextReader
{
    private readonly ITextReader _realReader;

    public SmartTextChecker(ITextReader realReader)
    {
        _realReader = realReader;
    }

    public char[][] ReadText(string filePath)
    {
        Console.WriteLine($"[SmartTextChecker] Відкриття файлу: {filePath}");
        
        char[][] result = _realReader.ReadText(filePath);
        
        Console.WriteLine($"[SmartTextChecker] Успішно прочитано: {filePath}");
        Console.WriteLine($"[SmartTextChecker] Закриття файлу: {filePath}");

        int linesCount = result.Length;
        int charsCount = 0;
        
        foreach (var line in result)
        {
            charsCount += line.Length;
        }

        Console.WriteLine($"[SmartTextChecker] Загальна кількість рядків: {linesCount}");
        Console.WriteLine($"[SmartTextChecker] Загальна кількість символів: {charsCount}");

        return result;
    }
}

public class SmartTextReaderLocker : ITextReader
{
    private readonly ITextReader _realReader;
    private readonly Regex _restrictionRegex;

    public SmartTextReaderLocker(ITextReader realReader, string regexPattern)
    {
        _realReader = realReader;
        _restrictionRegex = new Regex(regexPattern);
    }

    public char[][] ReadText(string filePath)
    {
        if (_restrictionRegex.IsMatch(filePath))
        {
            Console.WriteLine("Access denied!");
            return Array.Empty<char[]>();
        }

        return _realReader.ReadText(filePath);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        File.WriteAllText("public_data.txt", "Перший рядок\nДругий рядок\nТретій рядок");
        File.WriteAllText("secret_data.txt", "confidential_info");

        ITextReader baseReader = new SmartTextReader();
        
        ITextReader loggingProxy = new SmartTextChecker(baseReader);
        
        ITextReader secureProxy = new SmartTextReaderLocker(loggingProxy, @"secret.*\.txt$");

        Console.WriteLine("--- Спроба доступу до дозволеного файлу ---");
        char[][] publicText = secureProxy.ReadText("public_data.txt");

        Console.WriteLine("\n--- Спроба доступу до забороненого файлу ---");
        char[][] secretText = secureProxy.ReadText("secret_data.txt");

        File.Delete("public_data.txt");
        File.Delete("secret_data.txt");
    }
}