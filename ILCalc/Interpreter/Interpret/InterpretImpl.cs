/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using ILCalc.Custom;

namespace ILCalc
{
  [Serializable]
  sealed class InterpretImpl<T, TSupport> : Interpret<T>
    where TSupport : IArithmetic<T>, new()
  {
    #region Fields

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static readonly TSupport Generic = new TSupport();

    #endregion
    #region Constructor

    public InterpretImpl(
      string expr, int args, InterpretCreator<T> creator) :
      base(expr, args, creator) { }

    #endregion
    #region Interpreter

    internal override T Run(T[] stack, T[] args)
    {
      int c = 0, // code position
          n = 0, // number position
          f = 0, // functions pos
          i = -1; // stack marker

      while (true)
      {
        Code op = this.code[c++];
        if (CodeHelper.IsOp(op))
        {
          T value = stack[i--];
          if (op != Code.Neg)
          {
            if (op == Code.Add)
              stack[i] = Generic.Add(stack[i], value);
            else if (op == Code.Mul)
              stack[i] = Generic.Mul(stack[i], value);
            else if (op == Code.Sub)
              stack[i] = Generic.Sub(stack[i], value);
            else if (op == Code.Div)
              stack[i] = Generic.Div(stack[i], value);
            else if (op == Code.Mod)
              stack[i] = Generic.Mod(stack[i], value);
            else
              stack[i] = Generic.Pow(stack[i], value);
          }
          else
          {
            stack[++i] = Generic.Neg(value);
          }
        }
        else if (op == Code.Number)
        {
          stack[++i] = this.numbs[n++];
        }
        else
        {
          if (op == Code.Argument)
            stack[++i] = args[(int) this.code[c++]];
          else if (op == Code.Delegate0)
            stack[++i] = ((EvalFunc0<T>) this.funcs[f++])();
          else if (op == Code.Delegate1)
            stack  [i] = ((EvalFunc1<T>) this.funcs[f++])(stack[i]);
          else if (op == Code.Delegate2)
            stack[--i] = ((EvalFunc2<T>) this.funcs[f++])(stack[i], stack[i + 1]);
          else if (op == Code.Function)
          {
            ((FuncCall<T>) this.funcs[f++]).Invoke(stack, ref i);
          }
          else return stack[0];
        }
      }
    }

    #endregion
    #region Tabulation

    internal override T[] Tab1Impl(T[] array, T begin, T step)
    {
      var stack = new T[this.stackMax];
      var args = new[] { begin };

      for (int i = 0; i < array.Length; i++)
      {
        array[i] = Run(stack, args);
        args[0] = Generic.Add(args[0], step);
      }

      return array;
    }

    internal override T[][] Tab2Impl(T[][] array,
      ValueRange<T> range1,
      ValueRange<T> range2)
    {
      var stack = new T[this.stackMax];
      var args = new[] { range1.Begin, range2.Begin };

      for (int i = 0; i < array.Length; i++)
      {
        T[] row = array[i];

        args[1] = range2.Begin;
        for (int j = 0; j < row.Length; j++)
        {
          row[j] = Run(stack, args);
          args[1] = Generic.Add(args[1], range2.Step);
        }

        args[0] = Generic.Add(args[0], range1.Step);
      }

      return array;
    }

    internal override Array TabNImpl(
      Array array, params ValueRange<T>[] ranges)
    {
      var stack = new T[this.stackMax];
      var args  = new T[ranges.Length];

      return TabNImpl(args, stack, array, 0, ranges);
    }

    internal Array TabNImpl(
      T[] args, T[] stack, Array xarray,
      int pos, params ValueRange<T>[] ranges)
    {
      int next = pos + 1;
      if (ranges.Length - pos == 2)
      {
        var array = (T[][]) xarray;
        T step = ranges[next].Step;

        args[pos] = ranges[pos].Begin;
        for (int i = 0; i < array.Length; i++)
        {
          T[] row = array[i];

          args[next] = ranges[next].Begin;
          for (int j = 0; j < row.Length; j++)
          {
            row[j] = Run(stack, args);
            args[next] = Generic.Add(args[next], step);
          }

          args[pos] = Generic.Add(
            args[pos], ranges[pos].Step);
        }
      }
      else
      {
        args[pos] = ranges[pos].Begin;
        for (int i = 0; i < xarray.Length; i++)
        {
          var array = (Array) xarray.GetValue(i);
          TabNImpl(args, stack, array, next, ranges);

          args[pos] = Generic.Add(
            args[pos], ranges[pos].Step);
        }
      }

      return xarray;
    }

    #endregion
  }
}