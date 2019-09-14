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
            IndexedSet<int> set1 = new IndexedSet<int>();
            IEnumerable<int> set2 = new List<int>();
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test2()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test3()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 5 };
            set1.ExceptWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test4()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 2, 3 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test5()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 4,5,6 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test6()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test7()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test8()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 4 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test9()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            IndexedSet<int> set2 = new IndexedSet<int>();
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test10()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test11()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 5 };
            set1.ExceptWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test12()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 1, 2, 3 };
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test13()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 4, 5, 6 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test14()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 1 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test15()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 0 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test16()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 4 };
            set1.ExceptWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test17()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = null;
            Assert.Throws< ArgumentNullException>(() => set1.ExceptWith(set2));
        }

        [Fact]
        public void Test18()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = set1;
            set1.ExceptWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test19()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 1, 4 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test20()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IndexedSet<int> set2 = new IndexedSet<int>() { 1, 0 };
            set1.ExceptWith(set2);
            Assert.Equal(2, set1.Count);
        }
    }
}
