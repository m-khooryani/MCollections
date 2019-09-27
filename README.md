# MCollections

in .NET, two data structures are useful when you need data to be sorted:
* [SortedList<TKey,TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.8) insert, edit, remove, search are in O(N), and index lookup in O(1).
* [SortedDictionary<TKey,TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2?view=netframework-4.8) insert, edit, remove, search are in O(lg(N)), and index lookup is not supported.


MCollections provided two data structure with insert, edit, remove, search and index lookup In O(Lg(N))

download using NuGet: [MCollections](https://www.nuget.org/packages/MCollections/1.0.0)

## IndexedDictionary<TKey, TValue>

```csharp
IndexedDictionary<string, int> dictionary = new IndexedDictionary<string, int>()
{
    { "Zero", 0 },
    { "One", 1 },
    { "Two", 2 },
};
int valueWithIndex2 = dictionary.GetByIndex(2); // 0
int someValue = dictionary["One"]; // 1
```

## IndexedSet< T >

```csharp
IndexedSet<string> set = new IndexedSet<string>();
set.Add("two");
set.Add("one");
set.Add("zero");
string first = set[0]; // "one"
```
