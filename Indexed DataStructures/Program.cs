using System;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            //SortedSet<int> set = new SortedSet<int>();
            IndexedSortedSet<int> set = new IndexedSortedSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(10);
            set.Add(5);
            var ss = set.GetEnumerator();
            var sss = ss.Current;
            ss.MoveNext();
            sss = ss.Current;
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
