/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;

namespace ILCalc
{
  using State = DebuggerBrowsableState;
  using Br = DebuggerBrowsableAttribute;

  /// <summary>
  /// Represents the object for evaluating
  /// compiled expression in specified range
  /// of argument values.<br/>
  /// Instance of this class can be get from the
  /// <see cref="CalcContext{T}.CreateTabulator"/>
  /// method.<br/>This class cannot be inherited.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <remarks>Not available in the .NET CF versions.</remarks>
  /// <threadsafety instance="true" static="true"/>
  [DebuggerDisplay("{ToString()} ({RangesCount} range(s))")]
  public sealed class Tabulator<T>
  {
    #region Fields

    [Br(State.Never)] readonly TabFunc1<T> tabulator1;
    [Br(State.Never)] readonly TabFunc2<T> tabulator2;
    [Br(State.Never)] readonly TabFuncN<T> tabulatorN;
    [Br(State.Never)] readonly Allocator<T> allocator;
    
    [Br(State.Never)] readonly string expression;
    [Br(State.Never)] readonly int argsCount;

#if FULL_FW
    [Br(State.Never)] readonly Delegate asyncTab;
#endif

    #endregion
    #region Constructors

    Tabulator(string expression, int argsCount)
    {
      Debug.Assert(expression != null);
      Debug.Assert(argsCount > 0);

      this.expression = expression;
      this.argsCount = argsCount;
    }

    internal Tabulator(
      string expression, Delegate method,
      int argsCount, Allocator<T> alloc)
      : this(expression, argsCount)
    {
      Debug.Assert(method != null);
      Debug.Assert(alloc != null);
      Debug.Assert(argsCount > 2);

      this.tabulator1 = ThrowMethod1;
      this.tabulator2 = ThrowMethod2;
      this.tabulatorN = (TabFuncN<T>) method;
      this.allocator = alloc;

#if FULL_FW
      this.asyncTab = (TabFuncN<T>) this.tabulatorN.Invoke;
#endif
    }

    internal Tabulator(
      string expression, Delegate method, int argsCount)
      : this(expression, argsCount)
    {
      Debug.Assert(method != null);
      Debug.Assert(argsCount <= 2);

      if (argsCount == 1)
      {
        this.tabulator2 = ThrowMethod2;
        this.tabulator1 = (TabFunc1<T>) method;
        this.tabulatorN = (a, d) =>
          this.tabulator1((T[]) a, d[0], d[1]);
      }
      else
      {
        this.tabulator1 = ThrowMethod1;
        this.tabulator2 = (TabFunc2<T>) method;
        this.tabulatorN = (a, d) =>
          this.tabulator2((T[][]) a, d[0], d[1], d[2], d[3]);
      }

#if FULL_FW
      if (argsCount == 1)
           this.asyncTab = (TabFunc1<T>) this.tabulator1.Invoke;
      else this.asyncTab = (TabFunc2<T>) this.tabulator2.Invoke;
#endif

    }

    #endregion
    #region Members

    /// <summary>
    /// Gets the argument ranges count, that
    /// this Tabulator implemented for.</summary>
    [Br(State.Never)]
    public int RangesCount
    {
      get { return this.argsCount; }
    }

    /// <summary>
    /// Returns the expression string, that
    /// this Tabulator represents.</summary>
    /// <returns>Expression string.</returns>
    public override string ToString()
    {
      return this.expression;
    }

    #endregion
    #region Tabulate

    /// <summary>
    /// Invokes the compiled expression tabulation
    /// with giving one argument range.</summary>
    /// <overloads>Invokes the compiled expression tabulation.</overloads>
    /// <param name="begin">Argument range begin value.</param>
    /// <param name="end">Argument range end value.</param>
    /// <param name="step">Argument range step value.</param>
    /// <exception cref="InvalidRangeException">
    /// Argument range from <paramref name="begin"/>, <paramref name="end"/>
    /// and <paramref name="step"/> is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with one ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>One-dimensional array of the evaluated values.</returns>
    public T[] Tabulate(T begin, T end, T step)
    {
      return this.tabulator1(
        new T[new ValueRange<T>(begin, end, step).ValidCount],
        step,
        begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation
    /// with giving one argument range.</summary>
    /// <param name="range">Argument range.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range"/> is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with one ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>One-dimensional array of the evaluated values.</returns>
    public T[] Tabulate(ValueRange<T> range)
    {
      return this.tabulator1(
        new T[range.ValidCount],
        range.Step,
        range.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation
    /// with giving two argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range1"/> or <paramref name="range2"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with two ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>Two-dimensional jagged array of the evaluated values.</returns>
    public T[][] Tabulate(ValueRange<T> range1, ValueRange<T> range2)
    {
      var array = new T[range1.ValidCount][];
      int count = range2.ValidCount;

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new T[count];
      }

      return this.tabulator2(
        array,
        range1.Step, range2.Step,
        range1.Begin, range2.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation
    /// with giving three argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="range3">Third argument range.</param>
    /// <exception cref="InvalidRangeException"><paramref name="range1"/>,
    /// <paramref name="range2"/> or <paramref name="range3"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with three ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>Three-dimensional jagged array of the evaluated values
    /// casted to <see cref="Array"/> type.</returns>
    public Array Tabulate(
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3)
    {
      if (this.argsCount != 3) throw WrongRangesCount(3);

      return this.tabulatorN(
        this.allocator(
          range1.ValidCount,
          range2.ValidCount,
          range3.ValidCount),
        range1.Step,  range2.Step,  range3.Step,
        range1.Begin, range2.Begin, range3.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation
    /// with the specified argument <paramref name="ranges"/>.</summary>
    /// <param name="ranges">Argument ranges.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="ranges"/> is null.</exception>
    /// <exception cref="InvalidRangeException">
    /// Some instance of <paramref name="ranges"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="ranges"/> doesn't specify needed
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns><see cref="RangesCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public Array Tabulate(params ValueRange<T>[] ranges)
    {
      if (ranges == null
       || ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      var lengths = new int[ranges.Length];
      var data  = new T[2 * ranges.Length];

      for (int i = 0; i < ranges.Length; i++)
      {
        ValueRange<T> range = ranges[i];

        lengths[i] = range.ValidCount;
        data[i] = range.Step;
        data[ranges.Length + i] = range.Begin;
      }

      return this.tabulatorN(this.allocator(lengths), data);
    }

    #endregion
    #region TabulateToArray

    /// <summary>
    /// Invokes the compiled expression tabulation with giving one
    /// argument range and output to the specified array.</summary>
    /// <overloads>Invokes the compiled expression tabulation
    /// with output to the pre-allocated array.</overloads>
    /// <param name="array">One-dimensional allocated
    /// array for evaluated values.</param>
    /// <param name="begin">Argument range begin value.</param>
    /// <param name="step">Argument range step value.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with one range is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public void TabulateToArray(T[] array, T begin, T step)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      this.tabulator1(array, step, begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation with giving one
    /// argument range and output to the specified array.</summary>
    /// <param name="array">One-dimensional allocated
    /// array for the evaluated values.</param>
    /// <param name="range">Argument range.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with one range is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public void TabulateToArray(T[] array, ValueRange<T> range)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      this.tabulator1(array, range.Step, range.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation with giving two
    /// argument ranges and output to the specified array.</summary>
    /// <param name="array">Two-dimensional jagged
    /// pre-allocated array for the evaluated values.</param>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with two ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly evaluated (by the attached
    /// <see cref="Tabulator.Allocate{T}(ILCalc.ValueRange{T}, ILCalc.ValueRange{T})"/>
    /// method), or tabulator may throw <see cref="NullReferenceException"/>.</remarks>
    public void TabulateToArray(
      T[][] array, ValueRange<T> range1, ValueRange<T> range2)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      this.tabulator2(
        array,
        range1.Step,
        range2.Step,
        range1.Begin,
        range2.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation with giving three
    /// argument ranges and output to the specified array.</summary>
    /// <param name="array">Three-dimensional jagged pre-allocated array
    /// of the evaluated values casted to <see cref="Array"/> type.</param>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="range3">Third argument range.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with three ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly allocated by using the attached
    /// <see cref="Tabulator.Allocate{T}(ILCalc.ValueRange{T}, ILCalc.ValueRange{T}, ILCalc.ValueRange{T})"/> method.<br/>
    /// Otherwise this tabulator may throw <see cref="NullReferenceException"/>
    /// or <see cref="InvalidCastException"/>.</remarks>
    public void TabulateToArray(
      Array array,
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      this.tabulatorN(
        array,
        range1.Step,
        range2.Step,
        range3.Step,
        range1.Begin,
        range2.Begin,
        range3.Begin);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation with the specified argument
    /// <paramref name="ranges"/> and output to the specified array.</summary>
    /// <param name="array"><see cref="RangesCount">N</see>-dimensional
    /// jagged pre-allocated array of the evaluated values
    /// casted to <see cref="Array"/> type.</param>
    /// <param name="ranges">Argument ranges.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="ranges"/> doesn't specify needed
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly allocated by using
    /// the attached <see cref="Tabulator.Allocate{T}(ILCalc.ValueRange{T}[])"/> method.<br/>
    /// Otherwise this tabulator may throw <see cref="NullReferenceException"/>
    /// or <see cref="InvalidCastException"/>.</remarks>
    public void TabulateToArray(Array array, params ValueRange<T>[] ranges)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      if (ranges == null
       || ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      var data = new T[ranges.Length * 2];
      for (int i = 0; i < ranges.Length; i++)
      {
        ValueRange<T> range = ranges[i];
        range.Validate();

        data[i] = range.Step;
        data[ranges.Length + i] = range.Begin;
      }

      this.tabulatorN(array, data);
    }

    #endregion
    #region AsyncTabulate

    /// <summary>
    /// Begins an asynchronous tabulation of the compiled
    /// expression with giving one argument range.</summary>
    /// <overloads>Begins an asynchronous tabulation
    /// of the compiled expression.</overloads>
    /// <param name="range">Argument range.</param>
    /// <param name="callback">
    /// The <see cref="AsyncCallback"/> delegate.</param>
    /// <param name="state">An object that contains
    /// state information for this tabulation.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range"/> is not valid
    /// for iteration over it.</exception>
    /// <exception cref="ArgumentException">
    /// <see cref="Tabulator{T}"/> with one range
    /// is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>An <see cref="IAsyncResult"/> that references
    /// the asynchronous tabulation result.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T> range, AsyncCallback callback, object state)
    {
      if (this.argsCount != 1) throw InvalidArgs(1);

#if SILVERLIGHT

      return new AsyncHelper<T[]>(() => 
        this.tabulator1(new T[range.ValidCount], range.Step, range.Begin),
        callback, state);

#else

      return ((TabFunc1<T>) this.asyncTab).BeginInvoke(
        new T[range.ValidCount], range.Step, range.Begin, callback, state);

#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the compiled
    /// expression with giving two argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="callback">
    /// The <see cref="AsyncCallback"/> delegate.</param>
    /// <param name="state">An object that contains
    /// state information for this tabulation.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range1"/> or <paramref name="range2"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with two ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>An <see cref="IAsyncResult"/> that references
    /// the asynchronous tabulation result.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T> range1,
      ValueRange<T> range2,
      AsyncCallback callback,
      object state)
    {
      if (this.argsCount != 2) throw InvalidArgs(2);

      var array = new T[range1.ValidCount][];
      int count = range2.ValidCount;

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new T[count];
      }

#if SILVERLIGHT

      return new AsyncHelper<T[][]>(() =>
        this.tabulator2(array, range1.Step,
          range2.Step, range1.Begin, range2.Begin),
        callback, state);

#else

      return ((TabFunc2<T>) this.asyncTab)
        .BeginInvoke(array, range1.Step,
          range2.Step, range1.Begin, range2.Begin,
          callback, state);

#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the compiled
    /// expression with giving three argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="range3">Third argument range.</param>
    /// <param name="callback">
    /// The <see cref="AsyncCallback"/> delegate.</param>
    /// <param name="state">An object that contains
    /// state information for this tabulation.</param>
    /// <exception cref="InvalidRangeException"><paramref name="range1"/>,
    /// <paramref name="range2"/> or <paramref name="range3"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Tabulator{T}"/>
    /// with three ranges is not compiled, you should specify valid
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>An <see cref="IAsyncResult"/> that references
    /// the asynchronous tabulation result.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3,
      AsyncCallback callback,
      object state)
    {
      if (this.argsCount != 3) throw WrongRangesCount(3);

      var array = this.allocator(
        range1.ValidCount,
        range2.ValidCount,
        range3.ValidCount);

      var args = new[]
      {
        range1.Step,  range2.Step,  range3.Step,
        range1.Begin, range2.Begin, range3.Begin
      };

#if SILVERLIGHT

      return new AsyncHelper<Array>(() =>
        this.tabulatorN(array, args),
        callback, state);

#else

      return ((TabFuncN<T>) this.asyncTab)
        .BeginInvoke(array, args, callback, state);

#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the compiled expression
    /// with specified argument <paramref name="ranges"/>.</summary>
    /// <param name="ranges">Argument ranges.</param>
    /// <param name="callback">
    /// The <see cref="AsyncCallback"/> delegate.</param>
    /// <param name="state">An object that contains
    /// state information for this tabulation.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="ranges"/> is null.</exception>
    /// <exception cref="InvalidRangeException">
    /// Some instance of <paramref name="ranges"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="ranges"/> doesn't specify needed
    /// <see cref="RangesCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns><see cref="RangesCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T>[] ranges,
      AsyncCallback callback,
      object state)
    {
      if (ranges == null
       || ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      var lengths = new int[ranges.Length];
      var data  = new T[2 * ranges.Length];

      for (int i = 0; i < ranges.Length; i++)
      {
        ValueRange<T> range = ranges[i];

        lengths[i] = range.ValidCount;
        data[i] = range.Step;
        data[ranges.Length + i] = range.Begin;
      }

#if SILVERLIGHT

      return new AsyncHelper<Array>(() =>
        this.tabulatorN(this.allocator(lengths), data),
        callback, state);

#else

      return ((TabFuncN<T>) this.asyncTab)
        .BeginInvoke(this.allocator(lengths),
          data, callback, state);

#endif
    }

    /// <summary>
    /// Ends a pending asynchronous tabulation task.</summary>
    /// <param name="result">An <see cref="IAsyncResult"/>
    /// that stores state information and any user defined
    /// data for this asynchronous operation.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="result"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="EndTabulate"/>
    /// was previously called for the asynchronous tabulation.</exception>
    /// <returns><see cref="RangesCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public Array EndTabulate(IAsyncResult result)
    {
      if (result == null)
        throw new ArgumentNullException("result");

#if SILVERLIGHT

      switch (this.argsCount)
      {
        case 1: return ((AsyncHelper<T[]>)   result).EndInvoke();
        case 2: return ((AsyncHelper<T[][]>) result).EndInvoke();
        default: return ((AsyncHelper<Array>) result).EndInvoke();
      }

#else

      switch (this.argsCount)
      {
        case 1:  return ((TabFunc1<T>) this.asyncTab).EndInvoke(result);
        case 2:  return ((TabFunc2<T>) this.asyncTab).EndInvoke(result);
        default: return ((TabFuncN<T>) this.asyncTab).EndInvoke(result);
      }

#endif
    }

    #endregion
    #region ThrowMethods

    T[] ThrowMethod1(T[] array, T step, T begin)
    {
      throw new ArgumentException(string.Format(
        Resource.errWrongRangesCount, 1, this.argsCount));
    }

    T[][] ThrowMethod2(
      T[][] array, T step1, T step2, T begin1, T begin2)
    {
      throw new ArgumentException(string.Format(
        Resource.errWrongRangesCount, 2, this.argsCount));
    }

    Exception InvalidArgs(int actualCount)
    {
      return new ArgumentException(string.Format(
        Resource.errWrongRangesCount,
        actualCount,
        this.argsCount));
    }

    Exception WrongRangesCount(ValueRange<T>[] ranges)
    {
      if (ranges == null)
        return new ArgumentNullException("ranges");

      return new ArgumentException(string.Format(
        Resource.errWrongRangesCount,
        ranges.Length,
        this.argsCount));
    }

    Exception WrongRangesCount(int actualCount)
    {
      return new ArgumentException(string.Format(
        Resource.errWrongRangesCount,
        actualCount,
        this.argsCount));
    }

    #endregion
  }

  delegate T[]   TabFunc1<T>(T[] array, T step, T begin);
  delegate T[][] TabFunc2<T>(T[][] array, T step1, T step2, T begin1, T begin2);
  delegate Array TabFuncN<T>(Array array, params T[] data);
  delegate Array Allocator<T>(params int[] lengths);
}