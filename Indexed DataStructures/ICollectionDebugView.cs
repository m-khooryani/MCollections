using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Indexed_DataStructures
{
    internal sealed class ICollectionDebugView<T>
    {
        private readonly ICollection<T> collection;

        public ICollectionDebugView(ICollection<T> collection)
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
