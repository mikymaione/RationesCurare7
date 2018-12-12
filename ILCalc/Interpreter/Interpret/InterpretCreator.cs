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
  sealed class InterpretCreator<T> : IExpressionOutput<T>
  {
    #region Fields

    int npos, cpos, fpos;

    T[] nums;
    Code[] codes;
    object[] funcs;

    static readonly object[]
      EmptyFuncs = new object[0];

    int stackMax, stackSize;

    #endregion
    #region Constructor

    public InterpretCreator()
    {
      this.codes = new Code[8];
      this.funcs = EmptyFuncs;
      this.nums = new T[4];
    }

    #endregion
    #region Properties

    public Code[] Codes
    {
      get { return this.codes; }
    }

    public T[] Numbers
    {
      get { return this.nums; }
    }

    public object[] Functions
    {
      get { return this.funcs; }
    }

    public int StackMax
    {
      get { return this.stackMax; }
    }

    #endregion
    #region IExpressionOutput

    public void PutConstant(T value)
    {
      AddCode(Code.Number);

      if (this.npos == this.nums.Length)
      {
        ExpandArray(ref this.nums);
      }

      this.nums[this.npos++] = value;

      if (++this.stackSize > this.stackMax)
      {
        this.stackMax = this.stackSize;
      }
    }

    public void PutArgument(int id)
    {
      Debug.Assert(id >= 0);
      AddCode(Code.Argument);
      AddCode((Code) id);

      if (++this.stackSize > this.stackMax)
      {
        this.stackMax = this.stackSize;
      }
    }

    public void PutOperator(Code oper)
    {
      Debug.Assert(CodeHelper.IsOp(oper));
      AddCode(oper);

      if (oper != Code.Neg)
      {
        this.stackSize--;
      }
    }

    public void PutSeparator() { }
    public void PutBeginCall() { }

    public void PutCall(FunctionInfo<T> func, int args)
    {
      Debug.Assert(func != null);
      Debug.Assert(args >= 0);

      Delegate deleg = func.MakeDelegate();

      if (deleg == null)
      {
        AddFunc(func, args);
        AddCode(Code.Function);
        RecalcStackSize(args);
        return;
      }

      AddDelegate(deleg);
      switch (func.ArgsCount)
      {
        case 0: AddCode(Code.Delegate0);
                if (++this.stackSize > this.stackMax)
                  this.stackMax = this.stackSize;
                break;
        case 1: AddCode(Code.Delegate1);
                break;
        case 2: AddCode(Code.Delegate2);
                this.stackSize--;
                break;
      }
    }

    public void PutExprEnd()
    {
      AddCode(Code.Return);
    }

    #endregion
    #region Helpers

    void AddCode(Code code)
    {
      if (this.cpos == this.codes.Length)
      {
        ExpandArray(ref this.codes);
      }

      this.codes[this.cpos++] = code;
    }

    static void ExpandArray<U>(ref U[] src)
    {
      int size = (src.Length == 0) ? 4 : src.Length * 2;
      var dest = new U[size];

      if (src.Length > 0)
      {
        Array.Copy(src, 0, dest, 0, src.Length);
      }

      src = dest;
    }

    void RecalcStackSize(int argsCount)
    {
      Debug.Assert(argsCount >= 0);

      if (argsCount == 0)
      {
        if (++this.stackSize > this.stackMax)
        {
          this.stackMax = this.stackSize;
        }
      }
      else this.stackSize -= argsCount - 1;
    }

    void AddDelegate(Delegate deleg)
    {
      Debug.Assert(deleg != null);

      if (this.fpos == this.funcs.Length)
      {
        ExpandArray(ref this.funcs);
      }

      this.funcs[this.fpos++] = deleg;
    }

    void AddFunc(FunctionInfo<T> func, int argsCount)
    {
      Debug.Assert(func != null);
      Debug.Assert(argsCount >= 0);

      if (this.fpos == this.funcs.Length)
      {
        ExpandArray(ref this.funcs);
      }

      this.funcs[this.fpos++] =
        new FuncCall<T>(func, argsCount);
    }

    #endregion
//    #region Static Data
//
//    static readonly Type EvalType0 = typeof(EvalFunc0<T>);
//    static readonly Type EvalType1 = typeof(EvalFunc1<T>);
//    static readonly Type EvalType2 = typeof(EvalFunc2<T>);
//
//    #endregion
  }
}