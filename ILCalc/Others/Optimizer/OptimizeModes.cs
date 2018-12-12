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
  /// Provides enumerated values to use to set expression optimizer options.
  /// Expression optimizer will be used by <see cref="CalcContext{T}"/>
  /// when creating objects for evaluating expressions.
  /// </summary>
  [Flags, Serializable]
  public enum OptimizeModes
  {
    /// <summary>
    /// Specifies that no optimizations are should be done.
    /// </summary>
    None = 0,

    /// <summary>
    /// Constant folding optimization should be done for the expression.
    /// It is used to perform partical evaluation of
    /// operators in parse-time when operands are known values:
    /// <c>2 + 8 / 4</c> will be replaced by <c>4</c>.
    /// </summary>
    ConstantFolding = 1 << 0,

    /// <summary>
    /// Function folding optimization should be done for the expression.
    /// It is used for invoking function in parse-time when all the
    /// arguments of function are known values: <c>sin(pi / 6)</c>
    /// will be replaced with <c>0.5</c>.<br/>
    /// <i>WARNING: Functions should not produce any side-effects
    /// or you may get an unexpected result.</i>
    /// </summary>
    FunctionFolding = 1 << 1,

    /// <summary>
    /// Power operator optimization should be done for the expression.
    /// It is used for replacing expressions like <c>x ^ 4</c> with 
    /// <c>x * x * x * x</c> that evaluates much faster.
    /// </summary>
    PowOptimize = 1 << 2,

    /// <summary>
    /// Specifies that all of optimizations are should be done.
    /// </summary>
    PerformAll = 0x0007
  }
}