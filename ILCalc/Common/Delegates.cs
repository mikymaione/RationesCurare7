/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

namespace ILCalc
{
  /// <summary>
  /// Represents the compiled expression
  /// with no arguments.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <returns>Evaluated value.</returns>
  public delegate T EvalFunc0<T>();

  /// <summary>
  /// Represents the compiled expression
  /// with one argument.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <param name="arg">Expression argument.</param>
  /// <returns>Evaluated value.</returns>
  public delegate T EvalFunc1<T>(T arg);

  /// <summary>
  /// Represents the compiled expression
  /// with two arguments.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <param name="arg1">First expression argument.</param>
  /// <param name="arg2">Second expression argument.</param>
  /// <returns>Evaluated value.</returns>
  public delegate T EvalFunc2<T>(T arg1, T arg2);

  /// <summary>
  /// Represents the compiled expression
  /// with three or more arguments.</summary>
  /// <typeparam name="T">Expression values type.</typeparam>
  /// <param name="args">Expression arguments.</param>
  /// <returns>Evaluated value.</returns>
  public delegate T EvalFuncN<T>(params T[] args);
}