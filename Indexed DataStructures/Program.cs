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
            Test();
            set.Add(10);
            set.Add(5);
            set.Add(20);
            set.Add(1);
            set.Add(8);
            set.Add(15);
            set.Add(25);
            set.Add(12);
            set.Add(16);
            set.Add(23);

            set.Remove(25);

            for (int i = 0; i < set.Count; i++)
            {
                Console.WriteLine($"{i}- {set[i]}");
            }
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

        private static void Test()
        {
            Random rand = new Random();
            int counter = 0;
            while (true)
            {
                int size = rand.Next(20000);
                IndexedSortedSet<int> set = new IndexedSortedSet<int>();
                HashSet<int> hashSet = new HashSet<int>();
                for (int i = 0; i < size; i++)
                {
                    int x = rand.Next(300);
                    set.Add(x);
                    hashSet.Add(x);
                }
                int size2 = rand.Next(5000);
                for(int i=0; i<size2; i++)
                {
                    int x = rand.Next(300);
                    set.Remove(x);
                    hashSet.Remove(x);
                }
                if (set.Count != hashSet.Count)
                {
                    throw new Exception();
                }
                List<int> list = new List<int>(hashSet);
                list.Sort();
                for (int i = 0; i < set.Count; i++)
                {
                    if (set[i] != list[i])
                    {
                        throw new Exception();
                    }
                }
                Console.WriteLine($"success {++counter}");
            }
        }
    }
}
