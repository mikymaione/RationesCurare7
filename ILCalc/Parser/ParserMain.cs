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
  sealed partial class Parser<T>
  {
    #region Fields

    readonly CalcContext<T> context;
    readonly IListEnumerable[] literals;

    IExpressionOutput<T> output;
    string expr;

    static readonly ILiteralParser<T>
      Literal = LiteralParser.Resolve<T>();

    #endregion
    #region Constructor

    public Parser(CalcContext<T> context)
    {
      Debug.Assert(context != null);

      this.context = context;
      this.literals = new IListEnumerable[]
      {
        context.Arguments,
        context.Constants,
        context.Functions
      };

      InitCulture();
    }

    #endregion
    #region Properties

    CalcContext<T> Context
    {
      get { return this.context; }
    }

    IExpressionOutput<T> Output
    {
      get { return this.output; }
    }

    public void Parse(
      string expression, IExpressionOutput<T> exprOutput)
    {
      Debug.Assert(expression != null);
      Debug.Assert(exprOutput != null);

      this.expr = expression;
      //this.xlen = expression.Length;
      this.output = exprOutput;
      this.exprDepth = 0;
      this.prePos = 0;
      this.value = default(T);

      int i = 0;
      Parse(ref i, false);
    }

    #endregion
    #region StaticData

    /////////////////////////////////////////
    // WARNING: do not modify items order! //
    /////////////////////////////////////////
    enum Item
    {
      Operator   = 0,
      Separator  = 1,
      Begin      = 2,
      Number     = 3,
      End        = 4,
      Identifier = 5,
    }

    const string Operators = "-+*/%^";

    static readonly int[] Priority = { 0, 0, 1, 1, 1, 3, 2 };

    #endregion
  }
}