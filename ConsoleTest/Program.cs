using MCollections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleTest;

class Program
{
    static int threadId = 0;
    static void Main(string[] args)
    {
        ParallelTest();
    }

    private static void Benchmark()
    {
        HashSet<int> set = new HashSet<int>();
        //IndexedDictionary<int, int> sd = new IndexedDictionary<int, int>(); // 820
        SortedDictionary<int, int> sd = new SortedDictionary<int, int>(); // 324
        //SortedList<int, int> sd = new SortedList<int, int>();
        int n = 500 * 1000;
        Random rand = new Random();
        int max = 1000 * 1000;
        while (set.Count < n)
        {
            set.Add(rand.Next(max));
        }
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (int i in set)
        {
            sd.Add(i, 0);
        }
        stopwatch.Stop();
        Console.WriteLine("SD: " + stopwatch.ElapsedMilliseconds);
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
        int tid = ++threadId;
        Random rand = new Random();
        IndexedSet<int> set = new IndexedSet<int>();
        int counter = 0;
        while (true)
        {
            set.Clear();
            int size = rand.Next(20);
            HashSet<int> hashSet = new HashSet<int>();
            for (int i = 0; i < size; i++)
            {
                int x = rand.Next(15);
                set.Add(x);
                hashSet.Add(x);
            }
            int size2 = rand.Next(10);
            for (int i = 0; i < size2; i++)
            {
                int x = rand.Next(17);
                set.Remove(x);
                hashSet.Remove(x);
            }
            if (set.Count != hashSet.Count)
            {
                throw new Exception();
            }
            for (int i = 0; i < set.Count; i++)
            {
                if (set.IndexOfKey(set[i]) != i)
                {
                    throw new Exception("IndexOfKey does not work properly");
                }
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
