using System;
using System.Collections;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    public partial class IndexedDictionary<TKey, TValue>
    {
        public sealed class KeyCollection : IEnumerable<TKey>, ICollection, ICollection<TKey>
        {
            private readonly ISelfBalanceTree<KeyValuePair<TKey, TValue>> tree;

            internal KeyCollection(ISelfBalanceTree<KeyValuePair<TKey, TValue>> root)
            {
                this.tree = root;
            }

            public int Count => tree.Count;

            public bool IsSynchronized => false;

            public object SyncRoot => throw new NotImplementedException();

            public bool IsReadOnly => throw new NotImplementedException();

            public void Add(TKey item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TKey item)
            {
                return this.tree.Contains(new KeyValuePair<TKey, TValue>(item, default));
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
                if (array is TKey[] localArray)
                {
                    this.CopyTo(localArray, index);
                }
                else
                {
                    object[] objects = array as object[];
                    int num = index;
                    foreach (TKey t in this)
                    {
                        objects[num++] = t;
                    }
                }
            }

            public void CopyTo(TKey[] array, int arrayIndex)
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
                foreach (TKey t in this)
                {
                    if (c >= count)
                    {
                        break;
                    }
                    c++;
                    array[num++] = t;
                }
            }

            public bool Remove(TKey item)
            {
                throw new NotSupportedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return InOrder();
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return InOrder();
            }

            private IEnumerator<TKey> InOrder()
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
                    yield return node.Item.Key;
                    node = node.Right;
                }
            }
        }
    }
}
