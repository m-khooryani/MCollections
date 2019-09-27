using MCollections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace XUnitTestProject
{
    public class SymmetricExceptWithTests
    {
        [Fact]
        public void Test1()
        {
            Random rand = new Random(0);
            int size = 20000;
            IndexedSet<int> set = new IndexedSet<int>();
            HashSet<int> hashSet = new HashSet<int>();
            List<int> list = new List<int>();
            while (size-- > 0)
            {
                Debug.WriteLine(size);
                set.Clear();
                hashSet.Clear();
                list.Clear();
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
                hashSet.SymmetricExceptWith(list);
                set.SymmetricExceptWith(list);
                Assert.Equal(set.Count, hashSet.Count);
            }
        }
    }
}
