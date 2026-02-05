using System;
using ExtensionMethods;
using GenericCollections;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello World";
            Console.WriteLine($"Original: {text}");
            Console.WriteLine($"Inverted: {text.Invert()}");
            Console.WriteLine($"Count of 'l': {text.CountChar('l')}");

            Console.WriteLine(new string('-', 20));

            int[] numbers = { 1, 2, 3, 2, 4, 1, 5, 2 };
            Console.WriteLine($"Array: {string.Join(", ", numbers)}");
            Console.WriteLine($"Count of 2: {numbers.CountValue(2)}");

            var unique = numbers.GetUniqueElements();
            Console.WriteLine($"Unique: {string.Join(", ", unique)}");

            Console.WriteLine(new string('-', 20));

            var dict = new ExtendedDictionary<int, string, double>();

            dict.Add(1, "Apple", 1.50);
            dict.Add(2, "Banana", 2.00);
            dict.Add(3, "Orange", 1.80);

            Console.WriteLine($"Count: {dict.Count}");
            Console.WriteLine($"Contains Key 2: {dict.ContainsKey(2)}");
            Console.WriteLine($"Contains Values ('Banana', 2.00): {dict.ContainsValues("Banana", 2.00)}");

            dict.Remove(2);
            Console.WriteLine($"After removing Key 2, Count: {dict.Count}");

            try
            {
                var item = dict[1];
                Console.WriteLine($"Index[1]: Key={item.Key}, V1={item.Value1}, V2={item.Value2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Iterating foreach:");
            foreach (var elem in dict)
            {
                Console.WriteLine($"Key: {elem.Key}, Val1: {elem.Value1}, Val2: {elem.Value2}");
            }

            Console.ReadKey();
        }
    }
}