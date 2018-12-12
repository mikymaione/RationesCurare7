/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ILCalc
{
  [Serializable]
  abstract class BufferOutput<T> : IExpressionOutput<T>
  {
    #region Fields

    protected readonly List<FunctionInfo<T>> functions;
    protected readonly List<T> numbers;
    protected readonly List<Code> code;
    protected readonly List<int> data;

    #endregion
    #region Constructor

    protected BufferOutput()
    {
      this.functions = new List<FunctionInfo<T>>(2);
      this.numbers = new List<T>(4);
      this.code = new List<Code>(8);
      this.data = new List<int>(2);
    }

    #endregion
    #region IExpressionOutput

    public void PutConstant(T value)
    {
      this.code.Add(Code.Number);
      this.numbers.Add(value);
    }

    public void PutOperator(Code oper)
    {
      Debug.Assert(CodeHelper.IsOp(oper));

      this.code.Add(oper);
    }

    public void PutArgument(int id)
    {
      Debug.Assert(id >= 0);

      this.code.Add(Code.Argument);
      this.data.Add(id);
    }

    public void PutSeparator()
    {
      this.code.Add(Code.Separator);
    }

    public void PutBeginCall()
    {
      this.code.Add(Code.BeginCall);
    }

    public void PutCall(FunctionInfo<T> func, int args)
    {
      Debug.Assert(func != null);
      Debug.Assert(args >= 0);

      this.code.Add(Code.Function);
      this.data.Add(args);
      this.functions.Add(func);
    }

    public void PutExprEnd()
    {
      this.code.Add(Code.Return);
    }

    #endregion
    #region Methods

    public void WriteTo(IExpressionOutput<T> output)
    {
      int i = 0, n = 0,
          f = 0, d = 0;

      while (true)
      {
        Code op = this.code[i++];

        if (CodeHelper.IsOp(op))      output.PutOperator(op);
        else if (op == Code.Number)   output.PutConstant(this.numbers[n++]);
        else if (op == Code.Argument) output.PutArgument(this.data[d++]);
        else if (op == Code.Function) output.PutCall(
          this.functions[f++], this.data[d++]);
        else if (op == Code.Separator) output.PutSeparator();
        else if (op == Code.BeginCall) output.PutBeginCall();
        else { output.PutExprEnd(); break; }
      }
    }

    #endregion
  }
}