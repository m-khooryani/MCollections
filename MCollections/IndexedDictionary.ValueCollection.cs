using System;
using System.Collections;
using System.Collections.Generic;

namespace MCollections;

public partial class IndexedDictionary<TKey, TValue>
{
    public sealed class ValueCollection : IEnumerable<TValue>, ICollection, ICollection<TValue>
    {
        private readonly ISelfBalanceTree<KeyValuePair<TKey, TValue>> tree;

        internal ValueCollection(ISelfBalanceTree<KeyValuePair<TKey, TValue>> root)
        {
            this.tree = root;
        }

        public int Count => tree.Count;

        public bool IsSynchronized => false;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TValue item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(TValue item)
        {
            return this.tree.Contains(new KeyValuePair<TKey, TValue>(default, item));
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array is TValue[] localArray)
            {
                this.CopyTo(localArray, index);
            }
            else
            {
                object[] objects = array as object[];
                int num = index;
                foreach (TValue t in this)
                {
                    objects[num++] = t;
                }
            }
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            int count = array.Length;
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (count > (array.Length - arrayIndex))
            {
                throw new ArgumentException();
            }
            int num = arrayIndex;
            int c = 0;
            foreach (TValue t in this)
            {
                if (c >= count)
                {
                    break;
                }
                c++;
                array[num++] = t;
            }
        }

        public bool Remove(TValue item)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InOrder();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return InOrder();
        }

        private IEnumerator<TValue> InOrder()
        {
            if (this.tree.root.IsNil())
            {
                yield break;
            }
            Stack<Node<KeyValuePair<TKey, TValue>>> stack = new Stack<Node<KeyValuePair<TKey, TValue>>>();
            Node<KeyValuePair<TKey, TValue>> node = this.tree.root;
            while ((!node.IsNil()) || stack.Count > 0)
            {
                while (!node.IsNil())
                {
                    stack.Push(node);
                    node = node.Left;
                }
                node = stack.Pop();
                yield return node.Item.Value;
                node = node.Right;
            }
        }
    }
}
