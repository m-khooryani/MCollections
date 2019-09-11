using System.Collections;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    public class IndexedSortedSet<T> : IEnumerable<T>
    {
        private Tree<T> tree;

        public IndexedSortedSet()
        {
            this.tree = new Tree<T>(Comparer<T>.Default);
        }

        public IndexedSortedSet(IComparer<T> comparer)
        {
            this.tree = new Tree<T>(comparer);
        }

        public bool Add(T item)
        {
            return this.tree.AddIfNotPresent(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.tree.DFS();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
