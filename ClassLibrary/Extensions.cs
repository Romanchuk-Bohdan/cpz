using System;
using System.Linq;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Invert(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int CountChar(this string str, char symbol)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            return str.Count(c => c == symbol);
        }
    }

    public static class ArrayExtensions
    {
        public static int CountValue<T>(this T[] array, T value) where T : IEquatable<T>
        {
            if (array == null) return 0;
            return array.Count(item => item.Equals(value));
        }

        public static T[] GetUniqueElements<T>(this T[] array)
        {
            if (array == null) return Array.Empty<T>();
            return array.Distinct().ToArray();
        }
    }
}