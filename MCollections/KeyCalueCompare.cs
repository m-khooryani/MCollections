using System.Collections.Generic;

namespace MCollections;

internal class KeyCalueCompare<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
{
    public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
    {
        var comparer = Comparer<TKey>.Default;
        return comparer.Compare(x.Key, y.Key);
    }
}
