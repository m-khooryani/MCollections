using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MCollections
{
    [DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
    public partial class IndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        internal readonly ISelfBalanceTree<KeyValuePair<TKey, TValue>> tree;
        private KeyCollection _keys;
        private object syncRoot;
        private ValueCollection _values;

        public IndexedDictionary()
        {
            this.tree = new NonDuplicateRedBlackTree<KeyValuePair<TKey, TValue>>(new KeyCalueCompare<TKey, TValue>());
        }

        public IndexedDictionary(IComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            this.tree = new NonDuplicateRedBlackTree<KeyValuePair<TKey, TValue>>(comparer);
        }

        public TValue this[TKey key]
        {
            get
            {
                var node = this.tree.Search(new KeyValuePair<TKey, TValue>(key, default));
                if(node == null)
                {
                    return default;
                }
                return node.Item.Value;
            }
            set
            {
                KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, value);
                var node = this.tree.Search(item);
                if (node == null)
                {
                    this.tree.AddIfNotPresent(item);
                }
                else
                {
                    node.Item = item;
                }
            }
        }
        object IDictionary.this[object key]
        {
            get
            {
                if (key is TKey key1)
                {
                    Node<KeyValuePair<TKey, TValue>> node = this.tree.Search(new KeyValuePair<TKey, TValue>(key1, default));
                    if (node.IsNil())
                    {
                        return null;
                    }
                    return node.Item.Value;
                }
                else
                {
                    return null;
                }
            }
            set
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
                try
                {
                    TKey key1 = (TKey)key;
                    try
                    {
                        this[key1] = (TValue)value;
                    }
                    catch (InvalidCastException)
                    {
                        throw new ArgumentException("value");
                    }
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentException("key");
                }
            }
        }

        public TValue GetByIndex(int index) => this.tree.GetByIndex(index).Value;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;

        public int Count => tree.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            this.tree.AddIfNotPresent(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Clear()
        {
            tree.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return tree.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return this.tree.Contains(new KeyValuePair<TKey, TValue>(key, default));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int count = array.Length;
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count > (array.Length - arrayIndex))
            {
                throw new ArgumentException();
            }
            int num = arrayIndex;
            int c = 0;
            foreach (KeyValuePair<TKey, TValue> t in this)
            {
                if (c >= count)
                {
                    break;
                }
                c++;
                array[num++] = t;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array is KeyValuePair<TKey, TValue>[] localArray)
            {
                this.CopyTo(localArray, index);
            }
            else
            {
                object[] objects = array as object[];
                int num = index;
                foreach (KeyValuePair<TKey, TValue> t in this)
                {
                    objects[num++] = t;
                }
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return this.tree.InOrder();
        }

        public bool Remove(TKey key)
        {
            return this.tree.Remove(new KeyValuePair<TKey, TValue>(key, default));
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
            return this.tree.InOrder();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator(this);
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


        void IDictionary.Add(object key, object value)
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

        bool IDictionary.Contains(object key)
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

        void IDictionary.Remove(object key)
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

        bool ICollection.IsSynchronized => false;

        bool IDictionary.IsFixedSize => false;

        bool IDictionary.IsReadOnly => false;

        ICollection IDictionary.Keys => this.Keys;

        public KeyCollection Keys
        {
            get
            {
                if (this._keys == null)
                {
                    this._keys = new KeyCollection(this.tree);
                }
                return this._keys;
            }
        }

        public ValueCollection Values
        {
            get
            {
                if (this._values == null)
                {
                    this._values = new ValueCollection(this.tree);
                }
                return this._values;
            }
        }

        ICollection IDictionary.Values => Values;

        object ICollection.SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange(ref this.syncRoot, new object(), null);
                }
                return this.syncRoot;
            }
        }
    }
}
