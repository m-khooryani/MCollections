using System.Collections.Generic;

namespace MCollections;

internal interface ISelfBalanceTree<T>
{
    int Count { get; }
    T Max { get; }
    T Min { get; }
    bool AddIfNotPresent(T item);
    IEnumerator<T> InOrder();
    bool Remove(T item);
    Node<T> Search(T item);
    void Clear();
    IComparer<T> Comparer { get; }
    Node<T> root { get; }

    bool Contains(T item);
    T GetByIndex(int index);
    int IndexOfKey(T key);
    void ExceptWith(IEnumerable<T> other);
    void IntersectWith(IEnumerable<T> other);
    bool IsProperSubsetOf(IEnumerable<T> other);
    bool IsProperSupersetOf(IEnumerable<T> other);
    bool IsSubsetOf(IEnumerable<T> other);
    bool IsSuperSetOf(IEnumerable<T> other);
    bool Overlaps(IEnumerable<T> other);
    bool SetEquals(IEnumerable<T> other);
    void SymmetricExceptWith(IEnumerable<T> other);
    void UnionWith(IEnumerable<T> other);
}
