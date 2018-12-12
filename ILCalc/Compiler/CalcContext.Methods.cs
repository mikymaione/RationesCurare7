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
  public sealed partial class CalcContext<T>
  {
    #region Methods

    /// <summary>
    /// Compiles the <see cref="Evaluator{T}"/> object for evaluating
    /// the specified <paramref name="expression"/>.</summary>
    /// <param name="expression">Expression to compile.</param>
    /// <exception cref="SyntaxException"><paramref name="expression"/>
    /// contains syntax error(s) and can't be compiled.</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="expression"/> is null.</exception>
    /// <remarks>Not available in the .NET CF versions.</remarks>
    /// <returns><see cref="Evaluator{T}"/> object
    /// for evaluating expression.</returns>
    public Evaluator<T> CreateEvaluator(string expression)
    {
      if (expression == null)
        throw new ArgumentNullException("expression");

      var compiler = new EvaluatorCompiler<T>(ArgsCount, OverflowCheck);
      ParseOptimized(expression, compiler);

      return new Evaluator<T>(
        expression, compiler.CreateDelegate(), ArgsCount);
    }

    /// <summary>
    /// Compiles the <see cref="Tabulator{T}"/> object
    /// for evaluating the specified <paramref name="expression"/>
    /// in some ranges of arguments.</summary>
    /// <param name="expression">Expression to compile.</param>
    /// <exception cref="SyntaxException">
    /// <paramref name="expression"/> contains syntax error(s)
    /// and can't be compiled.</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="expression"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Current expression's arguments <see cref="Arguments">count</see>
    /// is not supported (only 1 or 2 arguments supported by now).</exception>
    /// <remarks>Not available in the .NET CF versions.</remarks>
    /// <returns><see cref="Tabulator{T}"/> object
    /// for evaluating expression.</returns>
    public Tabulator<T> CreateTabulator(string expression)
    {
      if (expression == null)
        throw new ArgumentNullException("expression");
      if (ArgsCount == 0)
        throw new ArgumentException(Resource.errTabulatorWrongArgs);

      var compiler = new TabulatorCompiler<T>(ArgsCount, OverflowCheck);
      ParseOptimized(expression, compiler);

      var del = compiler.CreateDelegate();

      if (ArgsCount > 2)
      {
        var alloc = TabulatorCompiler<T>.GetAllocator(ArgsCount);
        return new Tabulator<T>(expression, del, ArgsCount, alloc);
      }

      return new Tabulator<T>(expression, del, ArgsCount);
    }

    #endregion
  }
}