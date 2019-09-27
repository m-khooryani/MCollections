using MCollections;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class IsProperSubsetOfTests
    {
        [Fact]
        public void Test1()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            IEnumerable<int> set2 = new List<int>();
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test2()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test3()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { };
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.True(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test4()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test5()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 1, 1, 1 };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test6()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1 };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test7()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3 };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test8()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 2, 3 };
            Assert.False(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test9()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 2, 3, 4 };
            Assert.True(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test10()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 3, 3, 3, 3, 3 };
            IEnumerable<int> set2 = new List<int>() { 1, 2, 3, 4 };
            Assert.True(set1.IsProperSubsetOf(set2));
        }

        [Fact]
        public void Test11()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 3, 3, 3, 3, 3 };
            IEnumerable<int> set2 = null;
            Assert.Throws<ArgumentNullException>(() => set1.IsProperSubsetOf(set2));
        }
    }
}
