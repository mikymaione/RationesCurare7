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
  /// Represents the object
  /// for the expression evaluation.
  /// </summary>
  /// <typeparam name="T">
  /// Expression values type.</typeparam>
  /// <seealso cref="Evaluator{T}"/>
  /// <seealso cref="Interpret{T}"/>
  public interface IEvaluator<T>
  {
    /// <summary>
    /// Gets the arguments count, that this
    /// <see cref="IEvaluator{T}"/> implemented for.
    /// </summary>
    int ArgumentsCount { get; }

    /// <summary>
    /// Invokes the expression evaluation
    /// with providing no arguments.</summary>
    /// <overloads>Invokes the expression evaluation.</overloads>
    /// <returns>Evaluated value.</returns>
    T Evaluate();

    /// <summary>
    /// Invokes the expression evaluation
    /// with providing one argument.</summary>
    /// <param name="arg">Expression argument.</param>
    /// <returns>Evaluated value.</returns>
    T Evaluate(T arg);

    /// <summary>
    /// Invokes the expression evaluation
    /// with providing two arguments.</summary>
    /// <param name="arg1">First expression argument.</param>
    /// <param name="arg2">Second expression argument.</param>
    /// <returns>Evaluated value.</returns>
    T Evaluate(T arg1, T arg2);

    /// <summary>
    /// Invokes the expression evaluation
    /// with providing the specified arguments.</summary>
    /// <param name="args">Expression arguments.</param>
    /// <returns>Evaluated value.</returns>
    T Evaluate(params T[] args);

    /// <summary>
    /// Returns the expression string, that this
    /// <see cref="IEvaluator{T}"/> represents.</summary>
    /// <returns>Expression string.</returns>
    string ToString();
  }
}