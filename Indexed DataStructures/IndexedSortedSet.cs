using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Indexed_DataStructures
{
    [Serializable, DebuggerTypeProxy(typeof(ICollectionDebugView<>)), DebuggerDisplay("Count = {Count}"), TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    public class IndexedSortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
    {
        private readonly Tree<T> tree;

        public IndexedSortedSet()
        {
            this.tree = new Tree<T>(Comparer<T>.Default);
        }

        public IndexedSortedSet(IComparer<T> comparer)
        {
            this.tree = new Tree<T>(comparer);
        }

        public int Count => this.tree.Count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool Add(T item)
        {
            return this.tree.AddIfNotPresent(item);
        }

        public bool Remove(T item)
        {
            return this.tree.Remove(item);
        }

        public T this[int index]
        {
            get
            {
                return this.tree.GetNthItem(index);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.tree.DFS();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            this.tree.Clear();
        }

        public bool Contains(T item)
        {
            return this.tree.Search(item) != null;
        }

        public void CopyTo(T[] array)
        {
            this.CopyTo(array, 0, this.Count);
        }

        public void CopyTo(T[] array, int index)
        {
            this.CopyTo(array, index, this.Count);
        }

        public void CopyTo(T[] array, int index, int count)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (count > (array.Length - index))
            {
                throw new ArgumentException();
            }
            int num = index;
            int c = 0;
            foreach(T t in this)
            {
                if (c >= count)
                {
                    break;
                }
                c++;
                array[num++] = t;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
