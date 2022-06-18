using System;
using System.Collections;

namespace MCollections;

public partial class IndexedDictionary<TKey, TValue>
{
    private class DictionaryEnumerator : IDictionaryEnumerator
    {
        private readonly DictionaryEntry[] items;
        private int index = -1;

        public DictionaryEnumerator(IndexedDictionary<TKey, TValue> dictionary)
        {
            items = new DictionaryEntry[dictionary.Count];
            int index = 0;
            foreach (var item in dictionary)
            {
                items[index++] = new DictionaryEntry(item.Key, item.Value);
            }
        }

        public object Current
        {
            get
            {
                ValidateIndex();
                return items[index];
            }
        }

        public DictionaryEntry Entry
        {
            get
            {
                return (DictionaryEntry)Current;
            }
        }

        public object Key
        {
            get
            {
                ValidateIndex();
                return items[index].Key;
            }
        }

        public object Value
        {
            get
            {
                ValidateIndex();
                return items[index].Value;
            }
        }

        public bool MoveNext()
        {
            if (index < items.Length - 1)
            {
                index++;
                return true;
            }
            return false;
        }

        private void ValidateIndex()
        {
            if (index < 0 || index >= items.Length)
            {
                throw new InvalidOperationException("Enumerator is before or after the collection.");
            }
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
