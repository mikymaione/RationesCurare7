/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;

namespace ILCalc
{
  /// <summary>
  /// Provides the static methods for allocating the arrays
  /// by the specified <see cref="ValueRange{T}">ranges</see>.
  /// </summary>
  public static class Tabulator
  {
    #region Allocate

    /// <summary>
    /// Allocates the array with length, that needed to tabulate
    /// some expression in the specified argument range.</summary>
    /// <overloads>
    /// Allocates the array with length(s), that needed to tabulate
    /// some expression in specified argument range(s).</overloads>
    /// <param name="begin">Argument range begin value.</param>
    /// <param name="end">Argument range end value.</param>
    /// <param name="step">Argument range step value.</param>
    /// <typeparam name="T">Array elements type.</typeparam>
    /// <exception cref="InvalidRangeException">
    /// Argument range from <paramref name="begin"/>,
    /// <paramref name="end"/> and <paramref name="step"/>
    /// is not valid for iteration over it.</exception>
    /// <returns>Allocated one-dimensional array.</returns>
    public static T[] Allocate<T>(T begin, T end, T step)
    {
      return new T[
        new ValueRange<T>(begin, end, step).ValidCount
        ];
    }

    /// <summary>
    /// Allocates the array with length, that needed to tabulate
    /// some expression in the specified argument range.</summary>
    /// <param name="range">Argument range.</param>
    /// <typeparam name="T">Array elements type.</typeparam>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range"/> is not valid for iteration over it.</exception>
    /// <returns>Allocated one-dimensional array.</returns>
    public static T[] Allocate<T>(ValueRange<T> range)
    {
      return new T[range.ValidCount];
    }

    /// <summary>
    /// Allocates the array with lengths, that needed to tabulate
    /// some expression in the two specified argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <typeparam name="T">Array elements type.</typeparam>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range1"/> or <paramref name="range2"/>
    /// is not valid for iteration over it.</exception>
    /// <returns>Allocated two-dimensional jagged array.</returns>
    public static T[][] Allocate<T>(
      ValueRange<T> range1, ValueRange<T> range2)
    {
      var array = new T[range1.ValidCount][];
      int count = range2.ValidCount;

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new T[count];
      }

      return array;
    }

    /// <summary>
    /// Allocates the array with lengths, that needed to tabulate
    /// some expression in the three specified argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="range3">Third argument range.</param>
    /// <typeparam name="T">Array elements type.</typeparam>
    /// <exception cref="InvalidRangeException"><paramref name="range1"/>,
    /// <paramref name="range2"/> or <paramref name="range3"/>
    /// is not valid for iteration over it.</exception>
    /// <returns>Allocated three-dimensional jagged array
    /// casted to <see cref="Array"/> type.</returns>
    public static Array Allocate<T>(
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3)
    {
      var alloc = TabulatorCompiler<T>.GetAllocator(3);

      return alloc(
        range1.ValidCount,
        range2.ValidCount,
        range3.ValidCount);
    }

    /// <summary>
    /// Allocates the array with lengths, that needed
    /// to tabulate some expression in the specified
    /// argument <paramref name="ranges"/>.</summary>
    /// <param name="ranges">Argument ranges.</param>
    /// <typeparam name="T">Array elements type.</typeparam>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="ranges"/> is null.</exception>
    /// <exception cref="InvalidRangeException">
    /// Some instance of <paramref name="ranges"/>
    /// is not valid for iteration over it.</exception>
    /// <returns>Allocated <paramref name="ranges"/>-dimensional
    /// jagged array casted to <see cref="Array"/> type.</returns>
    public static Array Allocate<T>(params ValueRange<T>[] ranges)
    {
      if (ranges == null)
        throw new ArgumentNullException("ranges");

      if (ranges.Length == 1) return new double[ranges[0].ValidCount];
      if (ranges.Length == 2) return Allocate(ranges[0], ranges[1]);

      var sizes = new int[ranges.Length];
      for (int i = 0; i < ranges.Length; i++)
      {
        sizes[i] = ranges[i].ValidCount;
      }

      var alloc = TabulatorCompiler<T>.GetAllocator(ranges.Length);
      return alloc(sizes);
    }

    #endregion
  }
}