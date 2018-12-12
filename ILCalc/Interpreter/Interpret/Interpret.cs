/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Threading;

namespace ILCalc
{
  using Br = DebuggerBrowsableAttribute;
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Represents the object for evaluating expression by interpreter.<br/>
  /// Instance of this class can be get from
  /// the <see cref="CalcContext{T}.CreateInterpret"/> method.<br/>
  /// This class cannot be inherited.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <threadsafety>
  /// Instance <see cref="Evaluate()"/> methods are not thread-safe.
  /// Use the <see cref="EvaluateSync()"/> method group instead.
  /// </threadsafety>
  [DebuggerDisplay("{ToString()} ({ArgumentsCount} argument(s))")]
  [Serializable]
  public abstract partial class Interpret<T>
  {
    #region Fields

    // expression info:
    [Br(State.Never)] readonly string expression;
    [Br(State.Never)] readonly int argsCount;

    // interpretation data:
    [Br(State.Never)] internal readonly int stackMax;
    [Br(State.Never)] internal readonly Code[] code;
    [Br(State.Never)] internal readonly T[] numbs;
    [Br(State.Never)] internal readonly object[] funcs;

    // stack & params array, sync object, async tabulator:
    [Br(State.Never), NonSerialized] T[] stackArray;
    [Br(State.Never), NonSerialized] T[] paramArray;
    [Br(State.Never), NonSerialized] object syncRoot;

#if FULL_FW
    [Br(State.Never), NonSerialized] Delegate asyncTab;
#endif

    #endregion
    #region Constructor

    internal Interpret(
      string expression, int argsCount,
      InterpretCreator<T> creator)
    {
      this.code = creator.Codes;
      this.funcs = creator.Functions;
      this.numbs = creator.Numbers;

      this.expression = expression;
      this.stackMax = creator.StackMax;
      this.argsCount = argsCount;

      this.stackArray = new T[creator.StackMax];
      this.paramArray = new T[argsCount];
      this.syncRoot = new object();

#if FULL_FW
      switch (argsCount)
      {
        case 1: this.asyncTab = (TabFunc1) Tab1Impl; break;
        case 2: this.asyncTab = (TabFunc2) Tab2Impl; break;
        default: this.asyncTab = (TabFuncN) TabNImpl; break;
      }
#endif

    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the arguments count, that this
    /// <see cref="Interpret{T}"/> implemented for.
    /// </summary>
    [Br(State.Never)]
    public int ArgumentsCount
    {
      get { return this.argsCount; }
    }

    /// <summary>
    /// Returns the expression string, that this
    /// <see cref="Interpret{T}"/> represents.</summary>
    /// <returns>Expression string.</returns>
    public override string ToString()
    {
      return this.expression;
    }

    #endregion
    #region Evaluate

    /// <summary>
    /// Invokes the expression interpreter
    /// with giving no arguments.</summary>
    /// <overloads>Invokes the expression interpreter.</overloads>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with no arguments given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate()
    {
      if (this.argsCount != 0)
        throw WrongArgsCount(0);

      return Run(this.stackArray, this.paramArray);
    }

    /// <summary>
    /// Invokes the expression interpreter
    /// with giving one argument.</summary>
    /// <param name="arg">Expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with one arguments given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg)
    {
      if (this.argsCount != 1)
        throw WrongArgsCount(1);

      this.paramArray[0] = arg;
      return Run(this.stackArray, this.paramArray);
    }

    /// <summary>
    /// Invokes the expression interpreter
    /// with giving two arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with two argument given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg1, T arg2)
    {
      if (this.argsCount != 2)
        throw WrongArgsCount(2);

      this.paramArray[0] = arg1;
      this.paramArray[1] = arg2;
      return Run(this.stackArray, this.paramArray);
    }

    /// <summary>
    /// Invokes the expression interpreter
    /// with giving three arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <param name="arg3">Third expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with three arguments given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg1, T arg2, T arg3)
    {
      if (this.argsCount != 2)
        throw WrongArgsCount(2);

      this.paramArray[0] = arg1;
      this.paramArray[1] = arg2;
      this.paramArray[2] = arg3;
      return Run(this.stackArray, this.paramArray);
    }

    /// <summary>
    /// Invokes the expression interpreter.</summary>
    /// <param name="args">Expression arguments.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="args"/> doesn't specify needed
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(params T[] args)
    {
      if (args == null ||
          args.Length != this.argsCount)
      {
        throw WrongArgsCount(args);
      }

      return Run(this.stackArray, args);
    }

    #endregion
    #region EvaluateSync

    /// <summary>
    /// Synchronously invokes the expression
    /// interpreter with giving no arguments.</summary>
    /// <overloads>Synchronously invokes
    /// the expression interpreter.</overloads>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with no arguments given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T EvaluateSync()
    {
      if (this.argsCount != 0)
        throw WrongArgsCount(0);

      if (Monitor.TryEnter(this.syncRoot))
        try { return Run(this.stackArray, this.paramArray); }
        finally { Monitor.Exit(this.syncRoot); }

      // no need for allocate zero-lenght array
      return Run(new T[this.stackMax], this.paramArray);
    }

    /// <summary>
    /// Synchronously invokes the expression interpreter
    /// with giving one argument.</summary>
    /// <param name="arg">Expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with one argument given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T EvaluateSync(T arg)
    {
      if (this.argsCount != 1)
        throw WrongArgsCount(1);

      if (Monitor.TryEnter(this.syncRoot))
        try
        {
          this.paramArray[0] = arg;
          return Run(this.stackArray, this.paramArray);
        }
        finally { Monitor.Exit(this.syncRoot); }

      return Run(new T[this.stackMax], new[] { arg });
    }

    /// <summary>
    /// Synchronously invokes the expression interpreter
    /// with giving two arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't evaluate expression with two arguments given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T EvaluateSync(T arg1, T arg2)
    {
      if (this.argsCount != 2)
        throw WrongArgsCount(2);

      if (Monitor.TryEnter(this.syncRoot))
        try
        {
          this.paramArray[0] = arg1;
          this.paramArray[1] = arg2;
          return Run(this.stackArray, this.paramArray);
        }
        finally { Monitor.Exit(this.syncRoot); }

      return Run(new T[this.stackMax], new[] { arg1, arg2 });
    }

    /// <summary>
    /// Synchronously invokes the expression interpreter.</summary>
    /// <param name="args">Expression arguments.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="args"/> doesn't specify needed
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T EvaluateSync(params T[] args)
    {
      if (args == null || args.Length != this.argsCount)
      {
        throw WrongArgsCount(args);
      }

      if (Monitor.TryEnter(this.syncRoot))
        try { return Run(this.stackArray, args); }
        finally { Monitor.Exit(this.syncRoot); }

      return Run(new T[this.stackMax], args);
    }

    #endregion
    #region Tabulate

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving one argument range.</summary>
    /// <overloads>Invokes interpreter to tabulate the expression.</overloads>
    /// <param name="begin">Argument range begin value.</param>
    /// <param name="end">Argument range end value.</param>
    /// <param name="step">Argument range step value.</param>
    /// <exception cref="InvalidRangeException">
    /// Argument range from <paramref name="begin"/>, <paramref name="end"/>
    /// and <paramref name="step"/> is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with one range given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>One-dimensional array of the evaluated values.</returns>
    public T[] Tabulate(T begin, T end, T step)
    {
      if (this.argsCount != 1)
        throw WrongRangesCount(1);

      var array = new T[
        new ValueRange<T>(begin, end, step).ValidCount
        ];
      return Tab1Impl(array, begin, step);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving one argument range.</summary>
    /// <param name="range">Argument range.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range"/> is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with one range given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>One-dimensional array of the evaluated values.</returns>
    public T[] Tabulate(ValueRange<T> range)
    {
      if (this.argsCount != 1)
        throw WrongRangesCount(1);

      var array = new T[range.ValidCount];
      return Tab1Impl(array, range.Begin, range.Step);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving two argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <exception cref="InvalidRangeException">
    /// <paramref name="range1"/> or <paramref name="range2"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with two ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>Two-dimensional jagged array of the evaluated values.</returns>
    public T[][] Tabulate(ValueRange<T> range1, ValueRange<T> range2)
    {
      if (this.argsCount != 2)
        throw WrongRangesCount(2);

      var array = new T[range1.ValidCount][];
      int count = range2.ValidCount;

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new T[count];
      }

      return Tab2Impl(array, range1, range2);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving two argument ranges.</summary>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <param name="range3">Third argument range.</param>
    /// <exception cref="InvalidRangeException"><paramref name="range1"/>,
    /// <paramref name="range2"/> or <paramref name="range3"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with two ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>Three-dimensional jagged array of the evaluated values
    /// casted to <see cref="Array"/> type.</returns>
    public Array Tabulate(
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3)
    {
      if (this.argsCount != 3)
        throw WrongRangesCount(3);

      Array array = Interpret.Allocate(range1, range2, range3);
      return TabNImpl(array, range1, range2, range3);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with the specified argument <paramref name="ranges"/>.</summary>
    /// <param name="ranges">Argument ranges.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="ranges"/> is null.</exception>
    /// <exception cref="InvalidRangeException">
    /// Some instance of <paramref name="ranges"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="ranges"/> doesn't specify needed
    /// <see cref="ArgumentsCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns><see cref="ArgumentsCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public Array Tabulate(params ValueRange<T>[] ranges)
    {
      if (ranges == null || ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      Array array = Interpret.Allocate(ranges);
      TabNImpl(array, ranges);

      return array;
    }

    #endregion
    #region TabulateToArray

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving one argument range and output
    /// to the pre-allocated array.</summary>
    /// <overloads>Invokes interpreter to tabulate the expression
    /// with output to the pre-allocated array.</overloads>
    /// <param name="array">One-dimensional allocated
    /// array for evaluated values.</param>
    /// <param name="begin">Argument range begin value.</param>
    /// <param name="step">Argument range step value.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with one range given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>One-dimensional array of the evaluated values.</returns>
    public void TabulateToArray(T[] array, T begin, T step)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (this.argsCount != 1)
        throw WrongRangesCount(1);

      Tab1Impl(array, begin, step);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving one argument range and output
    /// to the pre-allocated array.</summary>
    /// <param name="array">One-dimensional allocated
    /// array for the evaluated values.</param>
    /// <param name="range">Argument range.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="InvalidRangeException"><paramref name="range"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with one range given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public void TabulateToArray(T[] array, ValueRange<T> range)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (this.argsCount != 1)
        throw WrongRangesCount(1);

      Tab1Impl(array, range.Begin, range.Step);
    }

    /// <summary>
    /// Invokes interpreter to tabulate the expression
    /// with giving one argument range and output
    /// to the pre-allocated array.</summary>
    /// <param name="array">Two-dimensional jagged
    /// pre-allocated array for the evaluated values.</param>
    /// <param name="range1">First argument range.</param>
    /// <param name="range2">Second argument range.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with two ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly evaluated (by the attached
    /// <see cref="Interpret.Allocate{T}(ILCalc.ValueRange{T}, ILCalc.ValueRange{T})"/>
    /// method), or interpret may throw <see cref="NullReferenceException"/>.</remarks>
    public void TabulateToArray(
      T[][] array, ValueRange<T> range1, ValueRange<T> range2)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (this.argsCount != 2)
        throw WrongRangesCount(2);

      Tab2Impl(array, range1, range2);
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
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with two ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly allocated by using the attached
    /// <see cref="Interpret.Allocate{T}(ILCalc.ValueRange{T}, ILCalc.ValueRange{T}, ILCalc.ValueRange{T})"/>
    /// method.<br/>Otherwise this interpret may throw <see cref="NullReferenceException"/>
    /// or <see cref="InvalidCastException"/>.</remarks>
    public void TabulateToArray(
      Array array,
      ValueRange<T> range1,
      ValueRange<T> range2,
      ValueRange<T> range3)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (this.argsCount != 3)
        throw WrongRangesCount(3);

      TabNImpl(array, range1, range2, range3);
    }

    /// <summary>
    /// Invokes the compiled expression tabulation with the specified argument
    /// <paramref name="ranges"/> and output to the specified array.</summary>
    /// <param name="array"><see cref="ArgumentsCount">N</see>-dimensional
    /// jagged pre-allocated array of the evaluated values
    /// casted to <see cref="Array"/> type.</param>
    /// <param name="ranges">Argument ranges.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="ranges"/> doesn't specify needed
    /// <see cref="ArgumentsCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <remarks>Pre-allocated array should be correctly allocated by using the
    /// attached <see cref="Interpret.Allocate{T}(ILCalc.ValueRange{T}[])"/> method.<br/>
    /// Otherwise this interpret may throw <see cref="NullReferenceException"/>
    /// or <see cref="InvalidCastException"/>.</remarks>
    public void TabulateToArray(
      Array array, params ValueRange<T>[] ranges)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      if (ranges == null ||
          ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      TabNImpl(array, ranges);
    }

    #endregion
    #region AsyncTabulate

    /// <summary>
    /// Begins an asynchronous tabulation of the
    /// expression with giving one argument range.</summary>
    /// <overloads>Begins an asynchronous
    /// tabulation of the expression.</overloads>
    /// <param name="range">Argument range.</param>
    /// <param name="callback">
    /// The <see cref="AsyncCallback"/> delegate.</param>
    /// <param name="state">An object that contains
    /// state information for this tabulation.</param>
    /// <exception cref="InvalidRangeException"><paramref name="range"/>
    /// is not valid for iteration over it.</exception>
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with one range given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns>An <see cref="IAsyncResult"/> that references
    /// the asynchronous tabulation result.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T> range,
      AsyncCallback callback,
      object state)
    {
      if (this.argsCount != 1)
        throw WrongRangesCount(1);

      var array = new T[range.ValidCount];

#if FULL_FW

      return ((TabFunc1) this.asyncTab)
        .BeginInvoke(array, range.Begin, range.Step,
          callback, state);

#else

      return new AsyncHelper<T[]>(() =>
        Tab1Impl(array, range.Begin, range.Step),
        callback, state);

#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the
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
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with two ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
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
      if (this.argsCount != 2)
        throw WrongRangesCount(2);

      var array = new T[range1.ValidCount][];
      int count = range2.ValidCount;

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new T[count];
      }

#if FULL_FW

      return ((TabFunc2) this.asyncTab)
        .BeginInvoke(array, range1, range2,
          callback, state);

#else

      return new AsyncHelper<T[][]>(() =>
        Tab2Impl(array, range1, range2),
        callback, state);

#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the
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
    /// <exception cref="ArgumentException"><see cref="Interpret{T}"/>
    /// can't tabulate expression with three ranges given, you should specify
    /// valid <see cref="ArgumentsCount">arguments count</see>.</exception>
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
      if (this.argsCount != 3)
        throw WrongRangesCount(3);

      Array array = Interpret.Allocate(range1, range2, range3);

#if FULL_FW

      return ((TabFuncN) this.asyncTab)
        .BeginInvoke(array,
          new[] { range1, range2, range3 },
          callback, state);

#else

      return new AsyncHelper<Array>(() =>
        TabNImpl(array, range1, range2, range3),
        callback, state);


#endif
    }

    /// <summary>
    /// Begins an asynchronous tabulation of the expression
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
    /// <see cref="ArgumentsCount">ranges count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    /// <returns><see cref="ArgumentsCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public IAsyncResult BeginTabulate(
      ValueRange<T>[] ranges,
      AsyncCallback callback,
      object state)
    {
      if (ranges == null ||
          ranges.Length != this.argsCount)
      {
        throw WrongRangesCount(ranges);
      }

      Array array = Interpret.Allocate(ranges);

#if FULL_FW

      return ((TabFuncN) this.asyncTab)
        .BeginInvoke(array, ranges, callback, state);

#else

      return new AsyncHelper<Array>(() =>
        TabNImpl(array, ranges), callback, state);

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
    /// <returns><see cref="ArgumentsCount">N</see>-dimensional jagged array
    /// of the evaluated values casted to <see cref="Array"/> type.</returns>
    public Array EndTabulate(IAsyncResult result)
    {
      if (result == null)
        throw new ArgumentNullException("result");

#if FULL_FW
      switch (this.argsCount)
      {
        case 1: return ((TabFunc1) this.asyncTab).EndInvoke(result);
        case 2: return ((TabFunc2) this.asyncTab).EndInvoke(result);
        default: return ((TabFuncN) this.asyncTab).EndInvoke(result);
      }
#else
      switch (this.argsCount)
      {
        case 1: return ((AsyncHelper<T[]>) result).EndInvoke();
        case 2: return ((AsyncHelper<T[][]>) result).EndInvoke();
        default: return ((AsyncHelper<Array>) result).EndInvoke();
      }
#endif
    }

    #endregion
    #region Delegates

    delegate T[] TabFunc1(T[] array, T begin, T step);

    delegate T[][] TabFunc2(T[][] array,
      ValueRange<T> range1, ValueRange<T> range2);

    delegate Array TabFuncN(
      Array array, params ValueRange<T>[] data);

    #endregion
    #region Internals

    internal abstract T Run(T[] stack, T[] args);

    internal abstract T[] Tab1Impl(T[] array, T begin, T step);

    internal abstract T[][] Tab2Impl(
      T[][] array,
      ValueRange<T> range1,
      ValueRange<T> range2);

    internal abstract Array TabNImpl(
      Array array, params ValueRange<T>[] ranges);

    #endregion
    #region ThrowHelpers

    private Exception WrongArgsCount(int actualCount)
    {
      return new ArgumentException(
        string.Format(
          Resource.errWrongArgsCount,
          actualCount, this.argsCount));
    }

    private Exception WrongArgsCount(T[] args)
    {
      if (args == null)
        return new ArgumentNullException("args");

      return new ArgumentException(
        string.Format(
          Resource.errWrongArgsCount,
          args.Length, this.argsCount));
    }

    private Exception WrongRangesCount(ValueRange<T>[] ranges)
    {
      if (ranges == null)
        return new ArgumentNullException("ranges");

      return new ArgumentException(
        string.Format(
          Resource.errWrongRangesCount,
          ranges.Length, this.argsCount));
    }

    private Exception WrongRangesCount(int actualCount)
    {
      return new ArgumentException(
        string.Format(
          Resource.errWrongRangesCount,
          actualCount, this.argsCount));
    }

    #endregion
    #region Creation

    internal static Interpret<T> Create(
      bool checks, string expr, int args,
      InterpretCreator<T> creator)
    {
      return checks ?
        Checked(expr, args, creator) :
        Normal(expr, args, creator);
    }

    [Br(State.Never)]
    static readonly InterpFactory<T> Normal, Checked;
  
    static Interpret()
    {
      Normal = Arithmetics.ResolveInterp<T>(false);
      Checked = Arithmetics.ResolveInterp<T>(true) ?? Normal;
    }

    #endregion
  }
}