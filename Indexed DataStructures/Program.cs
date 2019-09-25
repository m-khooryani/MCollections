using System;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            Test2();
            SortedDictionary<int, string> sd = new SortedDictionary<int, string>();
            sd.Add(1, "1");
            sd.Add(2, "2");
            foreach (var item in sd.Keys)
            {

            }
            IndexedDictionary<int, string> dictionary = new IndexedDictionary<int, string>();
            dictionary.Add(1, "5");
            dictionary.Add(5, "1");
            foreach (var item in dictionary.Values)
            {

            }
            dictionary.Add(1, "5");
            dictionary.Add(5, "1");
            dictionary.Add(1, "54");
        }

        private static void Test2()
        {
            Random rand = new Random(0);
            int size = 20000;
            IndexedSet<int> set = new IndexedSet<int>();
            HashSet<int> hashSet = new HashSet<int>();
            List<int> list = new List<int>();
            while (true)
            {
                set.Clear();
                hashSet.Clear();
                int n = rand.Next(100);
                for (int i = 0; i < n; i++)
                {
                    int item = rand.Next(100);
                    hashSet.Add(item);
                    set.Add(item);
                }
                n = rand.Next(100);
                for (int i = 0; i < n; i++)
                {
                    list.Add(rand.Next(100));
                }
                try
                {
                    hashSet.SymmetricExceptWith(list);
                    set.SymmetricExceptWith(list);
                    //Assert.Equal(set.Count, hashSet.Count);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static void Test()
        {
            Random rand = new Random();
            IndexedSet<int> set = new IndexedSet<int>();
            int counter = 0;
            while (true)
            {
                set.Clear();
                int size = rand.Next(20000);
                HashSet<int> hashSet = new HashSet<int>();
                for (int i = 0; i < size; i++)
                {
                    int x = rand.Next(3000);
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
