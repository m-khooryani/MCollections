using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IndexedCollections
{
    internal sealed class DictionaryDebugView<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        public DictionaryDebugView(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException("dictionary");
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] Items
        {
            get
            {
                KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.dictionary.Count];
                this.dictionary.CopyTo(array, 0);
                return array;
            }
        }
    }
}
