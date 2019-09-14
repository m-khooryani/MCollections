using Indexed_DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class IsSuperSetOfTests
    {
        [Fact]
        public void Test1()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            IEnumerable<int> set2 = new List<int>();
            Assert.True(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test2()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.False(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test3()
        {
            IndexedSet<int> set1 = new IndexedSet<int>();
            IEnumerable<int> set2 = new List<int>() { 0, 0 };
            Assert.False(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test4()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0, 0 };
            Assert.True(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test5()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 0 };
            IEnumerable<int> set2 = new List<int>() { 0 };
            Assert.True(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test6()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1,2,3,4,5 };
            IEnumerable<int> set2 = new List<int>() { 1,1,1,2,2,3,3,3 };
            Assert.True(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test7()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 4 };
            IEnumerable<int> set2 = new List<int>() { 1, 1, 1, 2, 2, 3, 3, 3,-1 };
            Assert.False(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test8()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 4 };
            IEnumerable<int> set2 = new List<int>() { 1, 1, 1, 2, 2, 3, 3, 3,5 };
            Assert.False(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test9()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 4 };
            IEnumerable<int> set2 = new List<int>() { };
            Assert.True(set1.IsSupersetOf(set2));
        }

        [Fact]
        public void Test10()
        {
            IndexedSet<int> set1 = new IndexedSet<int>() { 1, 2, 3, 4 };
            IEnumerable<int> set2 = null;
            Assert.Throws<ArgumentNullException>(() => set1.IsSupersetOf(set2));
        }
    }
}
