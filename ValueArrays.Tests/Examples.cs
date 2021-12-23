// SPDX-FileCopyrightText: 2021 TillW <https://www.github.com/x789/>
// SPDX-License-Identifier: MIT
using System;
using System.Linq;
using Xunit;

namespace ValueArrays.Tests;

public sealed class Examples
{
    private record Line(ValueArray<string> points) { }

    [Fact]
    public void Usage()
    {
        var array = new ValueArray<string>(new[] { "one", "two", "three" });
        var copy = new ValueArray<string>(array.ToArray());
        var other = new ValueArray<string>(new[] { "two", "one", "three" });

        // Equality is determined based on the items...
        Assert.True(array.Equals(copy));

        // ...and the order.
        Assert.False(array.Equals(other));

        // Like equality, the hash-code of a value-array is determined based on its content.
        Assert.True(array.GetHashCode() == copy.GetHashCode());
        Assert.False(array.GetHashCode() == other.GetHashCode());

        // The equality-operators are overloaded and work as expected.
        Assert.True(array == copy);
        Assert.True(array != other);

        // Value-arrays are enumerable...
        foreach (var item in array)
            Console.WriteLine(item); // prints "one", "two", "three"

        // ... and therefore support LINQ.
        Assert.True(array.Where(x => x == "two").Any());

        // They have an indexer...
        Console.WriteLine(array[2]); // prints "three"

        // ...and a 'Length' property.
        for (var i = 0; i < array.Length; i++)
            Console.WriteLine(array[i]); // prints "one", "two", "three"

        // Value-arrays are immutable. The indexer is read-only...
        //array[1] = "foo"; // triggers CS0200 "indexer is read only"

        // ...and changing the original data has no side-effects.
        var data = new[] { 1, 2, 3 };
        var va = new ValueArray<int>(data);
        data[1] = 4;
        Assert.True(va[1] == 2);

        // They can safely be used to model value-objects as records, since they fit into the built-in Equals() and GetHashCode().
        var line1 = new Line(array);
        var line2 = new Line(copy);
        Assert.True(line1.Equals(line2));
        Assert.True(line1 == line2);
        Assert.True(line1.GetHashCode() == line2.GetHashCode());
    }
}