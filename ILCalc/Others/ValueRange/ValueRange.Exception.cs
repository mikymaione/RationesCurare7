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
  /// <summary>
  /// The exception that is thrown when the <see cref="ILCalc.ValueRange"/>
  /// instance validation is failed.<br/>
  /// This class cannot be inherited.</summary>
  /// <remarks>Not available in the .NET CF versions.</remarks>
  [Serializable]
  public sealed class InvalidRangeException : Exception
  {
    #region Fields

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly ValueType range;

    /// <summary>
    /// Gets the range, that caused this exception.</summary>
    /// <value>Range, that caused an exception,
    /// if not avaliable - <c>null</c>.</value>
    public ValueType Range
    {
      get { return this.range; }
    }

    #endregion
    #region Constructors

    /// <summary>Initializes a new instance of the
    /// <see cref="InvalidRangeException"/> class.</summary>
    /// <overloads>Initializes a new instance of the
    /// <see cref="InvalidRangeException"/> class.</overloads>
    public InvalidRangeException() { }

    /// <summary>
    /// Initializes a new instance of
    /// the <see cref="InvalidRangeException"/>
    /// class with a specified error message.</summary>
    /// <param name="message">
    /// The message that describes the error.</param>
    public InvalidRangeException(string message)
      : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidRangeException"/>
    /// class with a specified error message and a reference to the
    /// inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or
    /// a <c>null</c> reference if no inner exception is specified.
    /// </param>
    public InvalidRangeException(string message, Exception innerException)
      : base(message, innerException) { }

    internal InvalidRangeException(string message, ValueType range)
      : base(message)
    {
      this.range = range;
    }

#if FULL_FW

    private InvalidRangeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }

#endif

    #endregion
  }
}