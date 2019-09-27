using System;
using System.Collections.Generic;

namespace MCollections
{
    internal class NonDuplicateRedBlackTree<T> : RedBlackTree<T>
    {
        public NonDuplicateRedBlackTree(IComparer<T> comparer) : base(comparer)
        {
        }

        public override bool AddIfNotPresent(T item)
        {
            bool v = base.AddIfNotPresent(item);
            if (!v)
            {
                throw new ArgumentException("An item with the same key has already been added");
            }
            return v;
        }
    }
}