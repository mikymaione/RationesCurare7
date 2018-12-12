/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Globalization;

namespace ILCalc
{
  using State = DebuggerBrowsableState;
  using Br = DebuggerBrowsableAttribute;

  /// <summary>
  /// Represents the expression context (arguments,
  /// constants and functions available to use in expression,
  /// parsing settings) and provides methods to compile,
  /// evaluate and validate expressions in runtime.<br/>
  /// This class cannot be inherited.</summary>
  /// <typeparam name="T">
  /// Expression values type.<br/>
  /// Supported types are: <see cref="Int32"/>,
  /// <see cref="Int64"/>, <see cref="Single"/>,
  /// <see cref="Double"/>, <see cref="Decimal"/>.
  /// </typeparam>
  /// <threadsafety instance="false"/>
  [Serializable]
  public sealed partial class CalcContext<T>
  {
    #region Fields

    // Imports collections
    [Br(State.Never)] readonly ArgumentCollection arguments;
    [Br(State.Never)] readonly ConstantDictionary<T> constants;
    [Br(State.Never)] readonly FunctionCollection<T> functions;

    // Context settings
    [Br(State.Never)] CultureInfo parseCulture;
    [Br(State.Never)] OptimizeModes optimizeMode;
    [Br(State.Never)] bool implicitMul = true;
    [Br(State.Never)] bool ignoreCase = true;
    [Br(State.Never)] bool checkedMode;

    // Parser instance
    [Br(State.Never)]
    [NonSerialized]
    Parser<T> parser;

    #endregion
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CalcContext{T}"/>
    /// class that is contains empty expression context.</summary>
    /// <overloads>Initializes a new instance
    /// of the <see cref="CalcContext{T}"/> class.</overloads>
    public CalcContext()
    {
      this.arguments = new ArgumentCollection();
      this.constants = new ConstantDictionary<T>();
      this.functions = new FunctionCollection<T>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalcContext{T}"/>
    /// class that is contains specified arguments list.</summary>
    /// <param name="arguments">Arguments names.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="arguments"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some name of <paramref name="arguments"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some name of <paramref name="arguments"/>
    /// is already exist in the list.</exception>
    public CalcContext(params string[] arguments) : this()
    {
      if (arguments == null)
        throw new ArgumentNullException("arguments");

      this.arguments.AddRange(arguments);
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the <see cref="ArgumentCollection"/>
    /// available for use in the expression.
    /// </summary>
    public ArgumentCollection Arguments
    {
      get { return this.arguments; }
    }

    /// <summary>
    /// Gets the <see cref="ConstantDictionary{T}"/>
    /// available for use in the expression.
    /// </summary>
    public ConstantDictionary<T> Constants
    {
      get { return this.constants; }
    }

    /// <summary>
    /// Gets the <see cref="FunctionCollection{T}"/>
    /// available for use in the expression.
    /// </summary>
    public FunctionCollection<T> Functions
    {
      get { return this.functions; }
    }

    /// <summary>
    /// Gets or sets <see cref="CultureInfo"/>
    /// instance used for expression parsing.</summary>
    /// <exception cref="NotSupportedException">
    /// <paramref name="value"/> is neutral culture,
    /// that can't be used as parse culture.</exception>
    /// <remarks>Can be <c>null</c> for ignoring culture-sensitive
    /// characters and using ordinal compare for strings.</remarks>
    public CultureInfo Culture
    {
      get { return this.parseCulture; }
      set
      {
        if (value != null) CheckNeutral(value);

        this.parseCulture = value;
        if (this.parser != null)
        {
          this.parser.InitCulture();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether ignore
    /// case mode is will be used for identifiers names
    /// in the expresion.</summary>
    /// <value><b>true</b> by default.</value>
    public bool IgnoreCase
    {
      get { return this.ignoreCase; }
      set { this.ignoreCase = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether implicit
    /// multiplication will be allowed in the expression.
    /// </summary>
    /// <value><b>true</b> by default.</value>
    public bool ImplicitMul
    {
      get { return this.implicitMul; }
      set { this.implicitMul = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether arithmetic
    /// checks are enabled while the expression evaluation.
    /// </summary>
    /// <remarks>Using this option will reduce
    /// perfomance of the evaluation.</remarks>
    /// <value><b>false</b> by default.</value>
    public bool OverflowCheck
    {
      get { return this.checkedMode; }
      set { this.checkedMode = value; }
    }

    /// <summary>
    /// Gets or sets a bitwise OR combination of
    /// <see cref="OptimizeModes"/> enumeration values
    /// that specify optimization modes for expression.
    /// </summary>
    /// <remarks>
    /// Be ready for catching <see cref="OverflowException"/>
    /// when creating of Evaluator/Interpret
    /// if <see cref="OverflowCheck"/> are enabled, because
    /// overflows may be caused at optimization step.</remarks>
    /// <value><see cref="OptimizeModes.None"/>
    /// by default.</value>
    public OptimizeModes Optimization
    {
      get { return this.optimizeMode; }
      set { this.optimizeMode = value; }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Evaluates the given <paramref name="expression"/>
    /// using quick interpretation mode.</summary>
    /// <param name="expression">Expression to evaluate.</param>
    /// <param name="arguments">Expression arguments values.</param>
    /// <exception cref="SyntaxException">
    /// <paramref name="expression"/> contains
    /// syntax error(s) and can't be evaluated.</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="expression"/> is null.<br/>-or-<br/>
    /// <paramref name="arguments"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Wrong arguments count was specified by the
    /// <paramref name="arguments"/> parameter.</exception>
    /// <exception cref="ArithmeticException">
    /// Expression evaluation thrown the
    /// <see cref="ArithmeticException"/>.</exception>
    /// <returns>Evaluated value.</returns>
    public T Evaluate(string expression, params T[] arguments)
    {
      if (expression == null) throw new ArgumentNullException("expression");
      if (arguments  == null) throw new ArgumentNullException("arguments");

      if (arguments.Length != ArgsCount)
        throw WrongArgsCount(arguments.Length, ArgsCount);

      var interp = QuickInterpret<T>
        .Create(OverflowCheck, arguments);

      ParseSimple(expression, interp);
      return interp.Result;
    }

    /// <summary>
    /// Generates the <see cref="Interpret{T}"/>
    /// object for evaluating the specified
    /// <paramref name="expression"/>.</summary>
    /// <param name="expression">Expression to create
    /// <see cref="Interpret{T}"/> from.</param>
    /// <exception cref="SyntaxException">
    /// <paramref name="expression"/> contains
    /// syntax error(s) and can't be used for the
    /// <see cref="Interpret{T}"/> creation.</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="expression"/> is null.</exception>
    /// <returns><see cref="Interpret{T}"/> object
    /// for evaluating expression.</returns>
    public Interpret<T> CreateInterpret(string expression)
    {
      if (expression == null)
        throw new ArgumentNullException("expression");

      var creator = new InterpretCreator<T>();
      ParseOptimized(expression, creator);

      return Interpret<T>.Create(
        OverflowCheck, expression, ArgsCount, creator);
    }

    /// <summary>
    /// Validates the specified
    /// <paramref name="expression"/>.</summary>
    /// <param name="expression">Expression to validate.</param>
    /// <exception cref="SyntaxException">
    /// <paramref name="expression"/>
    /// contains syntax error(s)</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="expression"/> is null.</exception>
    public void Validate(string expression)
    {
      if (expression == null)
        throw new ArgumentNullException("expression");

      ParseSimple(expression, NullWriter<T>.Instance);
    }

    #endregion
    #region Helpers

    int ArgsCount
    {
      get { return this.arguments.Count; }
    }

    static void CheckNeutral(CultureInfo culture)
    {
      if (culture.IsNeutralCulture)
      {
        throw new NotSupportedException(
          string.Format(
            Resource.errNeutralCulture, culture.Name));
      }
    }

    static ArgumentException WrongArgsCount(int actual, int expected)
    {
      return new ArgumentException(
        string.Format(
          Resource.errWrongArgsCount, actual, expected));
    }

    void ParseSimple(string expression, IExpressionOutput<T> output)
    {
      if (this.parser == null)
      {
        this.parser = new Parser<T>(this);
      }

      this.parser.Parse(expression, output);
    }

    void ParseOptimized(string expression, IExpressionOutput<T> output)
    {
      if (this.parser == null)
      {
        this.parser = new Parser<T>(this);
      }

      if (this.optimizeMode == OptimizeModes.None)
      {
        this.parser.Parse(expression, output);
      }
      else
      {
        var optimizer = new OptimizeOutput<T>(
          output, Optimization, OverflowCheck);

        this.parser.Parse(expression, optimizer);
      }
    }

    #endregion
  }
}