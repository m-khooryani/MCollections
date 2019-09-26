using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Indexed_DataStructures
{
    class Program
    {
        static int thread = 0;
        static void Main(string[] args)
        {
            //ParallelTest();
            //Test2();
            SortedDictionary<int, string> sd = new SortedDictionary<int, string>();
            sd.Add(1, "1");
            sd.Add(2, "2");
            foreach (var item in sd.Keys)
            {

            }
            IndexedDictionary<int, string> dictionary = new IndexedDictionary<int, string>();
            dictionary.Add(1, "5");
            dictionary.Add(5, "1");

            IDictionary d = dictionary;
            foreach (DictionaryEntry item in d)
            {

            }
            foreach (DictionaryEntry item in d)
            {

            }
            foreach (var item in dictionary.Values)
            {

            }
            dictionary.Add(1, "5");
            dictionary.Add(5, "1");
            dictionary.Add(1, "54");
        }

        private static void ParallelTest()
        {
            Task task1 = new Task(() => Test());
            Task task2 = new Task(() => Test());
            Task task3 = new Task(() => Test());
            Task task4 = new Task(() => Test());
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            Task.WaitAll(task1, task2, task3, task4);
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
            int tid = ++thread;
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
                Console.WriteLine($"{tid} success {++counter}");
            }
        }
    }
}
