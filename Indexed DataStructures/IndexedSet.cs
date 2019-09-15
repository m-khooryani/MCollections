using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Indexed_DataStructures
{
    [Serializable, DebuggerTypeProxy(typeof(ICollectionDebugView<>)), DebuggerDisplay("Count = {Count}"), TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    public class IndexedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
    {
        internal readonly Tree<T> tree;

        public IndexedSet()
        {
            this.tree = new Tree<T>(Comparer<T>.Default);
        }

        public IndexedSet(IComparer<T> comparer)
        {
            this.tree = new Tree<T>(comparer);
        }

        public int Count => this.tree.Count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public T Max => tree.Max;

        public T Min => tree.Min;

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
                return this.tree.GetByIndex(index);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.tree.DFS();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.tree.DFS();
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
            return tree.Contains(item);
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
            this.tree.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            this.tree.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.tree.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.tree.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.tree.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.tree.IsSuperSetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.tree.Overlaps(other);
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
