using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCollections;
using Xunit;

namespace XUnitTestProject;

public class IndexOfKeyTests
{
    [Fact]
    public void IndexOf_when_set_is_empty_is_minus_one()
    {
        var set = new IndexedSet<int>();

        var idx = set.IndexOfKey(1);

        Assert.Equal(-1, idx);
    }

    [Fact]
    public void IndexOf_key_when_is_not_present_in_collection_is_minus_one()
    {
        var set = new IndexedSet<int>
        {
            2
        };

        var idx = set.IndexOfKey(1);

        Assert.Equal(-1, idx);
    }

    [Fact]
    public void IndexOf_key_when_collection_has_only_that_key_is_zero()
    {
        var set = new IndexedSet<int>
        {
            1
        };

        var idx = set.IndexOfKey(1);

        Assert.Equal(0, idx);
    }

    [Fact]
    public void IndexOf_key_when_key_was_removed_is_minus_one()
    {
        var set = new IndexedSet<int>
        {
            1,2,3,4
        };

        set.Remove(3);

        var idx = set.IndexOfKey(3);

        Assert.Equal(-1, idx);
    }

    [Fact]
    public void IndexOf_key_success_1()
    {
        var set = new IndexedSet<int>
        {
            0
        };

        var idx = set.IndexOfKey(0);

        Assert.Equal(0, idx);
    }

    [Fact]
    public void IndexOf_key_success_2()
    {
        var set = new IndexedSet<int>
        {
            3, 5, 4, 2, 1, 6, 8, 10
        };

        var idx = set.IndexOfKey(5);

        Assert.Equal(4, idx);
    }

    [Fact]
    public void IndexOf_key_success_3()
    {
        var set = new IndexedSet<int>
        {
            -5,5,-4,4,-3,3,-2,2,1,0
        };

        var idx = set.IndexOfKey(3);

        Assert.Equal(7, idx);
    }

    [Fact]
    public void IndexOf_key_success_4()
    {
        var set = new IndexedSet<int>
        {
            -5,5,-4,4,-3,3,-2,2,1,0
        };

        var idx = set.IndexOfKey(-3);

        Assert.Equal(2, idx);
    }

    [Fact]
    public void IndexOf_key_success_5()
    {
        var set = new IndexedSet<int>
        {
            -5,5,-4,4,-3,3,-2,2,1,0
        };

        var idx = set.IndexOfKey(5);

        Assert.Equal(9, idx);
    }

    [Fact]
    public void IndexOf_key_success_6()
    {
        var set = new IndexedSet<int>
        {
            1,2,4
        };

        var idx = set.IndexOfKey(2);

        Assert.Equal(1, idx);
    }

    [Fact]
    public void IndexOf_key_success_7()
    {
        var dictionary = new IndexedDictionary<int, int>
        {
            {1,10 }, {2,20}, {4,40}
        };

        var idx = dictionary.IndexOfKey(2);

        Assert.Equal(1, idx);
    }
}
