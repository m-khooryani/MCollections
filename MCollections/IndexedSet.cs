using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MCollections;

[DebuggerTypeProxy(typeof(CollectionDebugView<>)), DebuggerDisplay("Count = {Count}")]
public class IndexedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
{
    internal readonly ISelfBalanceTree<T> tree;

    public IndexedSet()
    {
        this.tree = new RedBlackTree<T>(Comparer<T>.Default);
    }

    public IndexedSet(IComparer<T> comparer)
    {
        this.tree = new RedBlackTree<T>(comparer);
    }

    public int Count => this.tree.Count;

    public bool IsReadOnly => false;

    private object syncRoot;
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
        return this.tree.InOrder();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.tree.InOrder();
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
        foreach (T t in this)
        {
            if (c >= count)
            {
                break;
            }
            c++;
            array[num++] = t;
        }
    }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return false;
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
        return this.tree.SetEquals(other);
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        this.tree.SymmetricExceptWith(other);
    }

    public void UnionWith(IEnumerable<T> other)
    {
        this.tree.UnionWith(other);
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
        if (array is T[] localArray)
        {
            this.CopyTo(localArray, index);
        }
        else
        {
            object[] objects = array as object[];
            int num = index;
            foreach (T t in this)
            {
                objects[num++] = t;
            }
        }
    }

    public int IndexOfKey(T key)
    {
        return tree.IndexOfKey(key);
    }

    bool ICollection.IsSynchronized
    {
        get
        {
            return false;
        }
    }
}
