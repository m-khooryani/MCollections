using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IndexedCollections
{
    internal sealed class CollectionDebugView<T>
    {
        private readonly ICollection<T> collection;

        public CollectionDebugView(ICollection<T> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException("collection");
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                T[] array = new T[this.collection.Count];
                this.collection.CopyTo(array, 0);
                return array;
            }
        }
    }
}
