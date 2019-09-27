using IndexedCollections;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class OverlapsTests
    {
        [Fact]
        public void Test1()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { };
            IEnumerable<int> set2 = new List<int>() { };
            Assert.False(set1.Overlaps(set2));
        }

        [Fact]
        public void Test2()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { };
            Assert.False(set1.Overlaps(set2));
        }

        [Fact]
        public void Test3()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { };
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.False(set1.Overlaps(set2));
        }

        [Fact]
        public void Test4()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.True(set1.Overlaps(set2));
        }

        [Fact]
        public void Test5()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3 };
            IEnumerable<int> set2 = new List<int>() { 0,4,5,6,7,8,9 };
            Assert.False(set1.Overlaps(set2));
        }

        [Fact]
        public void Test6()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3 };
            IEnumerable<int> set2 = new List<int>() { 0,4,5,6,7,8,9,1 };
            Assert.True(set1.Overlaps(set2));
        }

        [Fact]
        public void Test7()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3 };
            IEnumerable<int> set2 = new List<int>() { 1,2,3 };
            Assert.True(set1.Overlaps(set2));
        }

        [Fact]
        public void Test8()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3 };
            IEnumerable<int> set2 = set1;
            Assert.True(set1.Overlaps(set2));
        }

        [Fact]
        public void Test9()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3 };
            IEnumerable<int> set2 = null;
            Assert.Throws<ArgumentNullException>(() => set1.Overlaps(set2));
        }
    }
}
