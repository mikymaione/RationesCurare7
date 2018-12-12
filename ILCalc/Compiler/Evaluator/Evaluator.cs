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

  // TODO: Serialization?

  /// <summary>
  /// Represents the object for the compiled expression evaluation.<br/>
  /// Instance of this class can be get from the
  /// <see cref="CalcContext{T}.CreateEvaluator"/> method.<br/>
  /// This class cannot be inherited.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <remarks>
  /// Instance contains read-only fields with delegates for slightly
  /// more performance invokation of the compiled methods.<br/>
  /// Not available in the .NET CF / Silverlight versions.</remarks>
  /// <threadsafety instance="true"/>
  [DebuggerDisplay("{ToString()} ({ArgumentsCount} argument(s))")]
  public sealed class Evaluator<T> : IEvaluator<T>
  {
    #region Fields

    /// <summary>
    /// Directly invokes the compiled expression with giving no arguments.
    /// This field is readonly.</summary>
    [DebuggerBrowsable(State.Never)]
    public readonly EvalFunc0<T> Evaluate0;

    /// <summary>
    /// Directly invokes the compiled expression with giving one argument.
    /// This field is readonly.</summary>
    [DebuggerBrowsable(State.Never)]
    public readonly EvalFunc1<T> Evaluate1;

    /// <summary>
    /// Directly invokes the compiled expression with giving two arguments.
    /// This field is readonly.</summary>
    [DebuggerBrowsable(State.Never)]
    public readonly EvalFunc2<T> Evaluate2;

    /// <summary>
    /// Directly invokes the compiled expression with specified arguments.
    /// This field is readonly.</summary>
    [DebuggerBrowsable(State.Never)]
    public readonly EvalFuncN<T> EvaluateN;

    [DebuggerBrowsable(State.Never)] readonly string expression;
    [DebuggerBrowsable(State.Never)] readonly int argsCount;

    #endregion
    #region Constructor

    internal Evaluator(
      string expression, Delegate method, int argsCount)
    {
      Debug.Assert(expression != null);
      Debug.Assert(argsCount >= 0);
      Debug.Assert(method != null);

      this.expression = expression;
      this.argsCount = argsCount;
      this.Evaluate0 = Throw0;
      this.Evaluate1 = Throw1;
      this.Evaluate2 = Throw2;

      if (argsCount == 0)
      {
        this.Evaluate0 = (EvalFunc0<T>) method;
        this.EvaluateN = (a => this.Evaluate0());
      }
      else if (argsCount == 1)
      {
        this.Evaluate1 = (EvalFunc1<T>) method;
        this.EvaluateN = (a => this.Evaluate1(a[0]));
      }
      else if (argsCount == 2)
      {
        this.Evaluate2 = (EvalFunc2<T>) method;
        this.EvaluateN = (a => this.Evaluate2(a[0], a[1]));
      }
      else
      {
        this.EvaluateN = (EvalFuncN<T>) method;
      }
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the arguments count, that this
    /// <see cref="Evaluator{T}"/> implemented for.</summary>
    [DebuggerBrowsable(State.Never)]
    public int ArgumentsCount
    {
      get { return this.argsCount; }
    }

    /// <summary>
    /// Returns the expression string,
    /// that this <see cref="Evaluator{T}"/> represents.</summary>
    /// <returns>Expression string.</returns>
    public override string ToString()
    {
      return this.expression;
    }

    #endregion
    #region Evaluate

    /// <summary>
    /// Invokes the compiled expression evaluation
    /// with giving no arguments.</summary>
    /// <overloads>Invokes the compiled expression evaluation.</overloads>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Evaluator{T}"/>
    /// with no arguments is not compiled, you should specify valid
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate()
    {
      return this.Evaluate0();
    }

    /// <summary>
    /// Invokes the compiled expression evaluation
    /// with giving one argument.</summary>
    /// <param name="arg">Expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Evaluator{T}"/>
    /// with one argument is not compiled, you should specify valid
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg)
    {
      return this.Evaluate1(arg);
    }

    /// <summary>
    /// Invokes the compiled expression evaluation
    /// with giving two arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Evaluator{T}"/>
    /// with two arguments is not compiled, you should specify valid
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg1, T arg2)
    {
      return this.Evaluate2(arg1, arg2);
    }

    /// <summary>
    /// Invokes the compiled expression evaluation
    /// with giving three arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <param name="arg3">Third expression argument.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException"><see cref="Evaluator{T}"/>
    /// with three arguments is not compiled, you should specify valid
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(T arg1, T arg2, T arg3)
    {
      return this.EvaluateN(arg1, arg2, arg3);
    }

    /// <summary>
    /// Invokes the compiled expression evaluation with the
    /// specified <paramref name="args">arguments</paramref>.</summary>
    /// <param name="args">Expression arguments.</param>
    /// <returns>Evaluated value.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="args"/> doesn't specify needed
    /// <see cref="ArgumentsCount">arguments count</see>.</exception>
    /// <exception cref="ArithmeticException">Expression evaluation
    /// thrown the <see cref="ArithmeticException"/>.</exception>
    public T Evaluate(params T[] args)
    {
      if (args == null
       || args.Length != this.argsCount)
      {
        WrongArgs(args);
      }

      return this.EvaluateN(args);
    }

    #endregion
    #region Throw Methods

    T Throw0()
    {
      throw new ArgumentException(string.Format(
        Resource.errWrongArgsCount, 0, this.argsCount));
    }

    T Throw1(T arg)
    {
      throw new ArgumentException(string.Format(
        Resource.errWrongArgsCount, 1, this.argsCount));
    }

    T Throw2(T arg1, T arg2)
    {
      throw new ArgumentException(string.Format(
        Resource.errWrongArgsCount, 2, this.argsCount));
    }

    void WrongArgs(T[] args)
    {
      if (args == null)
        throw new ArgumentNullException("args");

      throw new ArgumentException(
        string.Format(
          Resource.errWrongArgsCount,
          args.Length,
          this.argsCount));
    }

    #endregion
  }
}