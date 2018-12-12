/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.Diagnostics;
using ILCalc.Custom;

namespace ILCalc
{
  sealed class QuickInterpretImpl<T, TSupport> : QuickInterpret<T>
    where TSupport : IArithmetic<T>, new()
  {
    #region Fields

    static readonly TSupport Generic = new TSupport();

    #endregion
    #region Constructor

    public QuickInterpretImpl(T[] arguments)
      : base(arguments) { }

    #endregion
    #region IExpressionOutput

    public override void PutOperator(Code oper)
    {
      Debug.Assert(CodeHelper.IsOp(oper));
      Debug.Assert(this.pos >= 0);

      T value = this.stack[this.pos];
      if (oper != Code.Neg)
      {
        Debug.Assert(this.pos >= 0);
        Debug.Assert(this.pos < this.stack.Length);

        T temp = this.stack[--this.pos];

        if      (oper == Code.Add) temp = Generic.Add(temp, value);
        else if (oper == Code.Mul) temp = Generic.Mul(temp, value);
        else if (oper == Code.Sub) temp = Generic.Sub(temp, value);
        else if (oper == Code.Div) temp = Generic.Div(temp, value);
        else if (oper == Code.Mod) temp = Generic.Mod(temp, value);
        else temp = Generic.Pow(temp, value);

        this.stack[this.pos] = temp;
      }
      else
      {
        this.stack[this.pos] = Generic.Neg(value);
      }
    }

    public override int? IsIntegral(T value)
    {
      return Generic.IsIntergal(value);
    }

    #endregion
  }
}