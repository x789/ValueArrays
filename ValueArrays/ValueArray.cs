// SPDX-FileCopyrightText: 2021 TillW <https://www.github.com/x789/>
// SPDX-License-Identifier: MIT
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ValueArrays;

/// <summary>
/// An immutable (read-only) container whose equality is determined upon its content.
/// Therefore, two instances of <see cref="ValueArray{T}"/> are equal when the contained items and their order are equal.
/// </summary>
/// <typeparam name="T">Type of elements in the container.</typeparam>
public sealed class ValueArray<T> : IEnumerable<T>
{
    private readonly T[] values;
    private readonly int hashCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueArray{T}"/> class.
    /// </summary>
    /// <param name="values">Items to put into the container.</param>
    /// <remarks>The container will create a shallow copy of <paramref name="values"/>. Afterward changes to the array provided via <paramref name="values"/> will not be taken into account.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="values"/></exception>
    public ValueArray([NotNull] T[]? values)
    {
        ArgumentNullException.ThrowIfNull(values);
        this.values = new T[values.Length];
        values.CopyTo(this.values, 0);

        var hashGenerator = new HashCode();
        foreach (var value in this.values) hashGenerator.Add(value);
        this.hashCode = hashGenerator.ToHashCode();
    }

    /// <summary>
    /// Gets the value at a specific index.
    /// </summary>
    /// <param name="index">Index of the value (0 based).</param>
    /// <returns>Element at <paramref name="index"/>.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less than 0 or greater or equal to <see cref="Length"/>.</exception>
    public T this[int index] => this.values[index];

    /// <summary>
    /// Gets the length of the array.
    /// </summary>
    public int Length => this.values.Length;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is ValueArray<T> other && this.hashCode == other.hashCode && this.values.Length == other.values.Length)
        {
            for (int i = 0; i < this.values.Length; i++)
                if (!Equals(this.values[i], other.values[i])) return false;
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => this.hashCode;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.values.Length; i++) yield return this.values[i];
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public static bool operator ==(ValueArray<T>? left, ValueArray<T>? right) => Equals(left, right);

    /// <inheritdoc/>
    public static bool operator !=(ValueArray<T>? left, ValueArray<T>? right) => !Equals(left, right);
}