using Indexed_DataStructures;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class ExceptWithTests
    {
        [Fact]
        public void Test1()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>();
            IEnumerable<int> set2 = new List<int>();
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test2()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test3()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 5 };
            set1.ExceptWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test4()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 2, 3 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test5()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 4,5,6 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test6()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test7()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test8()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 4 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test9()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>();
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>();
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test10()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 0 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test11()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 0 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 5 };
            set1.ExceptWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test12()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 1, 2, 3 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test13()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 4, 5, 6 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test14()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 1 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test15()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test16()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 4 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test17()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = null;
            Assert.Throws< ArgumentNullException>(() => set1.ExceptWith(set2));
        }

        [Fact]
        public void Test18()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = set1;
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test19()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 1, 4 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test20()
        {
            IndexedSortedSet<int> set1 = new IndexedSortedSet<int>() { 1, 2, 3 };
            IndexedSortedSet<int> set2 = new IndexedSortedSet<int>() { 1, 0 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }
    }
}
