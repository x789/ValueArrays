// SPDX-FileCopyrightText: 2021 TillW <https://www.github.com/x789/>
// SPDX-License-Identifier: MIT
using System.Collections.Generic;
using Xunit;

namespace ValueArrays.Tests;

public class ValueArrayTests
{
    [Fact]
    public void Indexer()
    {
        var data = new[] { "one", "two", "three" };
        var sut = new ValueArray<string>(data);

        for (int i = 0; i < sut.Length; i++)
        {
            Assert.Equal(data[i], sut[i]);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    public void Indexer_OutOfBounds(int index)
    {
        var data = new[] { "one", "two", "three" };
        var sut = new ValueArray<string>(data);

        Assert.Throws<System.IndexOutOfRangeException>(() => sut[index]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void Length(int length)
    {
        var data = new int[length];
        var sut = new ValueArray<int>(data);

        Assert.Equal(length, sut.Length);
    }

    [Fact]
    public void GetHashCode_Equal()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new int[data.Length];
        data.CopyTo(data2, 0);
        var other = new ValueArray<int>(data);
        var otherHash = other.GetHashCode();
        var sut = new ValueArray<int>(data2);

        var sutHash = sut.GetHashCode();

        Assert.Equal(otherHash, sutHash);
    }

    [Fact]
    public void Equals_Equal()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new int[data.Length];
        data.CopyTo(data2, 0);
        var other = new ValueArray<int>(data);
        var sut = new ValueArray<int>(data2);

        var areEqual = sut.Equals(other);

        Assert.True(areEqual);
    }

    [Fact]
    public void Equals_Equal_ContainingNull()
    {
        var data = new int?[] { 1, 2, null, 8, 13 };
        var data2 = new int?[data.Length];
        data.CopyTo(data2, 0);
        var other = new ValueArray<int?>(data);
        var sut = new ValueArray<int?>(data2);

        var areEqual = sut.Equals(other);

        Assert.True(areEqual);
    }

    [Fact]
    public void EqualityOperator_Equal()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new int[data.Length];
        data.CopyTo(data2, 0);
        var array1 = new ValueArray<int>(data);
        var array2 = new ValueArray<int>(data2);

        var areEqual = array2 == array1;

        Assert.True(areEqual);
    }

    [Fact]
    public void EqualityOperator_NotEqual()
    {
        var left = new ValueArray<int>(new[] { 1, 2, 5, 8, 13 });
        var right = new ValueArray<int>(new[] { 21, 34, 55 });

        var areEqual = left == right;

        Assert.False(areEqual);
    }

    [Fact]
    public void EqualityOperator_LeftIsNull()
    {
        var array = new ValueArray<int>(new[] { 21, 34, 55 });

        var areEqual = null == array;

        Assert.False(areEqual);
    }

    [Fact]
    public void EqualityOperator_RightIsNull()
    {
        var array = new ValueArray<int>(new[] { 21, 34, 55 });

        var areEqual = array == null;

        Assert.False(areEqual);
    }

    [Fact]
    public void EqualityOperator_BothAreNull()
    {
        ValueArray<object>? left = null;
        ValueArray<object>? right = null;

        var areEqual = left == right;

        Assert.True(areEqual);
    }

    [Fact]
    public void GetHashCode_NotEqual()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new[] { 8, 13 };
        var other = new ValueArray<int>(data);
        var otherHash = other.GetHashCode();
        var sut = new ValueArray<int>(data2);

        var sutHash = sut.GetHashCode();

        Assert.NotEqual(otherHash, sutHash);
    }

    [Fact]
    public void Equals_NotEqual()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new[] { 8, 13 };
        var other = new ValueArray<int>(data);
        var sut = new ValueArray<int>(data2);

        var areEqual = sut.Equals(other);

        Assert.False(areEqual);
    }

    [Fact]
    public void GetHashCode_Equivalent()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new[] { 1, 5, 2, 13, 8 };
        var other = new ValueArray<int>(data);
        var otherHash = other.GetHashCode();
        var sut = new ValueArray<int>(data2);

        var sutHash = sut.GetHashCode();

        Assert.NotEqual(otherHash, sutHash);
    }

    [Fact]
    public void Equals_Equivalent()
    {
        var data = new[] { 1, 2, 5, 8, 13 };
        var data2 = new[] { 1, 5, 2, 13, 8 };
        var other = new ValueArray<int>(data);
        var sut = new ValueArray<int>(data2);

        var areEqual = sut.Equals(other);

        Assert.False(areEqual);
    }

    [Fact]
    public void Equals_Null()
    {
        var sut = new ValueArray<int>(new[] { 1, 2, 5, 8, 13 });

        var areEqual = sut.Equals(null);

        Assert.False(areEqual);
    }

    [Fact]
    public void Equals_OtherType()
    {
        var sut = new ValueArray<int>(new[] { 1, 2, 5, 8, 13 });

        var areEqual = sut.Equals(new object());

        Assert.False(areEqual);
    }

    [Fact]
    public void GetEnumerator()
    {
        var expected = new[] { "foo", "bar", "baz" };
        var actual = new List<object>();
        var sut = new ValueArray<string>(expected);

        foreach (var x in (System.Collections.IEnumerable)sut)
        {
            actual.Add(x);
        }

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetEnumerator_Generic()
    {
        var expected = new[] { "foo", "bar", "baz" };
        var actual = new List<string>();
        var sut = new ValueArray<string>(expected);

        foreach (var x in sut)
        {
            actual.Add(x);
        }

        Assert.Equal(expected, actual);
    }
}