using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericCollections
{
    public class ExtendedDictionaryElement<T, U, V>
    {
        public T Key { get; set; }
        public U Value1 { get; set; }
        public V Value2 { get; set; }

        public ExtendedDictionaryElement(T key, U value1, V value2)
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }
    }

    public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
    {
        private List<ExtendedDictionaryElement<T, U, V>> _elements;

        public ExtendedDictionary()
        {
            _elements = new List<ExtendedDictionaryElement<T, U, V>>();
        }

        public int Count => _elements.Count;

        public void Add(T key, U value1, V value2)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException($"Element with key {key} already exists.");
            }
            _elements.Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
        }

        public bool Remove(T key)
        {
            var item = _elements.FirstOrDefault(e => EqualityComparer<T>.Default.Equals(e.Key, key));
            if (item != null)
            {
                return _elements.Remove(item);
            }
            return false;
        }

        public bool ContainsKey(T key)
        {
            return _elements.Any(e => EqualityComparer<T>.Default.Equals(e.Key, key));
        }

        public bool ContainsValues(U value1, V value2)
        {
            return _elements.Any(e =>
                EqualityComparer<U>.Default.Equals(e.Value1, value1) &&
                EqualityComparer<V>.Default.Equals(e.Value2, value2));
        }

        public ExtendedDictionaryElement<T, U, V> this[T key]
        {
            get
            {
                var item = _elements.FirstOrDefault(e => EqualityComparer<T>.Default.Equals(e.Key, key));
                if (item == null)
                {
                    throw new KeyNotFoundException($"Key {key} not found.");
                }
                return item;
            }
        }

        public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}