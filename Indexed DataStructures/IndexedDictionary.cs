using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Indexed_DataStructures
{
    public class IndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        internal readonly Tree<KeyValuePair<TKey, TValue>> tree;

        public IndexedDictionary()
        {
            this.tree = new Tree<KeyValuePair<TKey, TValue>>(new KeyCalueCompare<TKey, TValue>());
        }

        public IndexedDictionary(IComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            this.tree = new Tree<KeyValuePair<TKey, TValue>>(comparer);
        }

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => tree.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => throw new NotImplementedException();

        ICollection IDictionary.Keys => throw new NotImplementedException();

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        ICollection IDictionary.Values => throw new NotImplementedException();

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            this.tree.AddIfNotPresent(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                TValue local = default;
                if (local != null)
                {
                    throw new ArgumentNullException("value");
                }
            }
            if (key is TKey key1)
            {
                if (value is TValue value2)
                {
                    this.tree.AddIfNotPresent(new KeyValuePair<TKey, TValue>(key1, value2));
                }
                else
                {
                    throw new ArgumentException("value");
                }
            }
            else
            {
                throw new ArgumentException("value");
            }
        }

        public void Clear()
        {
            tree.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return tree.Contains(item);
        }

        public bool Contains(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (key is TKey key1)
            {
                return this.tree.Contains(new KeyValuePair<TKey, TValue>(key1, default));
            }
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return this.tree.Contains(new KeyValuePair<TKey, TValue>(key, default));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.tree.DFS();
        }

        public bool Remove(TKey key)
        {
            return this.tree.Remove(new KeyValuePair<TKey, TValue>(key, default));
        }

        public void Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (key is TKey key1)
            {
                this.tree.Remove(new KeyValuePair<TKey, TValue>(key1, default));
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            var node = this.tree.Search(new KeyValuePair<TKey, TValue>(key, default));
            if (node.IsNil())
            {
                value = default;
                return false;
            }
            value = node.Item.Value;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.tree.DFS();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private static bool IsKeyTypeValid(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return key is TKey;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            this.tree.AddIfNotPresent(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.tree.Contains(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.tree.Remove(item);
        }
    }
}
