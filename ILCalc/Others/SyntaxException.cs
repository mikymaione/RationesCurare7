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
  using State = DebuggerBrowsableState;

  /// <summary>
  /// The exception that is thrown when syntax
  /// error occurs during expression parsing
  /// by <see cref="CalcContext{T}"/>.<br/>
  /// This class cannot be inherited.
  /// </summary>
  [Serializable]
  public sealed class SyntaxException : Exception
  {
    #region Fields

    [DebuggerBrowsable(State.Never)] readonly string exprString;
    [DebuggerBrowsable(State.Never)] readonly int pos;
    [DebuggerBrowsable(State.Never)] readonly int len;

    #endregion
    #region Constructors

    /// <summary>Initializes a new instance of the
    /// <see cref="SyntaxException"/> class.</summary>
    /// <overloads>Initializes a new instance of the
    /// <see cref="SyntaxException"/> class.</overloads>
    public SyntaxException() { this.exprString = string.Empty; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxException"/>
    /// class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public SyntaxException(string message)
      : base(message)
    {
      this.exprString = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxException"/>
    /// class with a specified error message and a reference to the
    /// inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or
    /// a <c>null</c> reference if no inner exception is specified.
    /// </param>
    public SyntaxException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.exprString = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxException"/>
    /// class with a specified error message and information about
    /// the syntax error location in the expression.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="expression">Expression string.</param>
    /// <param name="pos">Position ot the error range.</param>
    /// <param name="len">Length of the error range.</param>
    internal SyntaxException(
      string message, string expression, int pos, int len)
      : base(message)
    {
      Debug.Assert(message != null);
      Debug.Assert(expression != null);
      Debug.Assert(expression.Length >= pos && pos >= 0);
      Debug.Assert(expression.Length >= pos + len && len >= 0);

      this.exprString = expression;
      this.pos = pos;
      this.len = len;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxException"/>
    /// class with a specified error message, information about
    /// the syntax error location in the expression and a reference
    /// to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="expression">Expression string.</param>
    /// <param name="pos">Position ot the error range.</param>
    /// <param name="len">Length of the error range.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or
    /// a <c>null</c> reference if no inner exception is specified.
    /// </param>
    internal SyntaxException(
      string message, string expression, int pos, int len, Exception innerException)
      : base(message, innerException)
    {
      Debug.Assert(message != null);
      Debug.Assert(expression != null);
      Debug.Assert(expression.Length > pos && pos >= 0);
      Debug.Assert(expression.Length >= pos + len && len >= 0);

      this.exprString = expression;
      this.pos = pos;
      this.len = len;
    }

#if SERIALIZE

    SyntaxException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }

#endif

    #endregion
    #region Properties

    /// <summary>
    /// Gets the start position of the expression
    /// substring that thrown an exception.
    /// </summary>
    public int Position
    {
      get { return this.pos; }
    }

    /// <summary>
    /// Gets the length of expression substring that thrown an exception.
    /// </summary>
    public int Length
    {
      get { return this.len; }
    }

    /// <summary>
    /// Gets the full expression string that thrown an exception.
    /// </summary>
    public string Expression
    {
      get { return this.exprString; }
    }

    /// <summary>
    /// Gets the expression substring that thrown an exception.
    /// </summary>
    public string Substring
    {
      get { return this.exprString.Substring(this.pos, this.len); }
    }

    #endregion
  }
}