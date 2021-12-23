# ValueArrays

This package contains the implemetation of a `ValueArray`, which is an immutable (read-only) container whose equality is determined based on its content. That means, two `ValueArray`s with the same items in the same order are equal.

`ValueArray`s are the perfect fit if your DDD value-objects need to contain a collection of objects and you do not want to care about equality and immutability. This makes modeling ValueObjects with C# `record`s a breeze.

## Usage
```csharp
var array = new ValueArray<string>(new[] { "one", "two", "three" });
var copy = new ValueArray<string>(array.ToArray());
var other = new ValueArray<string>(new[] { "two", "one", "three" });

// Equality is determined based on the items...
array.Equals(copy); // true

// ...and the order.
array.Equals(other); // false

// Like equality, the hash-code of a value-array is determined based on its content.
array.GetHashCode() == copy.GetHashCode(); // true
array.GetHashCode() == other.GetHashCode(); // false

// The equality-operators are overloaded and work as expected.
array == copy; // true
array != other; // true

// Value-arrays are enumerable...
foreach (var item in array)
    Console.WriteLine(item); // prints "one", "two", "three"

// ... and therefore support LINQ.
array.Where(x => x == "two").Any(); // true

// They have an indexer...
Console.WriteLine(array[2]); // prints "three"

// ...and a 'Length' property.
for (var i = 0; i < array.Length; i++)
    Console.WriteLine(array[i]); // prints "one", "two", "three"

// Value-arrays are immutable. The indexer is read-only...
array[1] = "foo"; // triggers CS0200 "indexer is read only"

// ...and changing the original data has no side-effects.
var data = new[] { 1, 2, 3 };
var va = new ValueArray<int>(data);
data[1] = 4;
va[1] == 2; // true

// They can safely be used to model value-objects as records, since they fit into the built-in Equals() and GetHashCode().
var line1 = new Line(array);
var line2 = new Line(copy);
line1.Equals(line2); // true
line1 == line2; // true
line1.GetHashCode() == line2.GetHashCode(); // true
 ```

----
(c) 2021 TillW -- [https://github.com/x789/ValueArrays](https://github.com/x789/ValueArrays)