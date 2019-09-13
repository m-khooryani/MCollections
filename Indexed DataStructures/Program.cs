using System;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            //new SortedSet<int>().m
            IndexedSortedSet<int> set = new IndexedSortedSet<int>();
            List<int> list = new List<int>();
            set.ExceptWith(list);
            Console.WriteLine((set as ICollection<int>).IsReadOnly);
            set.Add(1);
            set.Add(12);
            set.Add(14);
            set.Add(11);
            set.Add(5);

            int[] a = new int[5];
            set.CopyTo(a,2,1);
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        private static void Test()
        {
            Random rand = new Random();
            IndexedSortedSet<int> set = new IndexedSortedSet<int>();
            int counter = 0;
            while (true)
            {
                set.Clear();
                int size = rand.Next(20000);
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
