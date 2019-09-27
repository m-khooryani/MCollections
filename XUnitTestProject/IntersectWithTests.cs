using MCollections;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class IntersectWithTests
    {
        [Fact]
        public void Test1()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            List<int> set2 = new List<int>();
            set1.IntersectWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test2()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1 };
            List<int> set2 = new List<int>();
            set1.IntersectWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test3()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1 };
            List<int> set2 = new List<int>() { 0 };
            set1.IntersectWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test4()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1 };
            List<int> set2 = new List<int>() { 1 };
            set1.IntersectWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test5()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            List<int> set2 = new List<int>() { 0 };
            set1.IntersectWith(set2);
            Assert.Empty(set1);
        }

        [Fact]
        public void Test6()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            List<int> set2 = new List<int>() { 1 };
            set1.IntersectWith(set2);
            Assert.Single(set1);
        }

        [Fact]
        public void Test7()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            List<int> set2 = new List<int>() { 4, 1, 2 };
            set1.IntersectWith(set2);
            Assert.Equal(2, set1.Count);
        }

        [Fact]
        public void Test8()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            List<int> set2 = new List<int>() { 1, 2, 3 };
            set1.IntersectWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test9()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            List<int> set2 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            set1.IntersectWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test10()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> set2 = new List<int>() { 1, 2, 3 };
            set1.IntersectWith(set2);
            Assert.Equal(3, set1.Count);
        }

        [Fact]
        public void Test11()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> set2 = null;
            Assert.Throws<ArgumentNullException>(() => set1.IntersectWith(set2));
        }

        [Fact]
        public void Test12()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var set2 = set1;
            set1.IntersectWith(set2);
            Assert.Equal(10, set1.Count);
        }
    }
}
