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
using System.Text;
using ILCalc.Custom;

namespace ILCalc
{
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Defines a set of (begin, end, step) values,
  /// that represents the range of values.<br/>
  /// This structure is immutable.</summary>
  /// <typeparam name="T">
  /// Range values type.<br/>
  /// Supported types list: <see cref="Int32"/>,
  /// <see cref="Int64"/>, <see cref="Single"/>,
  /// <see cref="Double"/>, <see cref="Decimal"/>.
  /// </typeparam>
  /// <threadsafety instance="true" static="true"/>
  [DebuggerDisplay("[{Begin} - {End}] step {Step}")]
  [Serializable]
  public struct ValueRange<T> : IEquatable<ValueRange<T>>
  {
    #region Fields

    [DebuggerBrowsable(State.Never)] readonly T begin, step, end;
    [DebuggerBrowsable(State.Never)] readonly int count;

    [DebuggerBrowsable(State.Never)]
    readonly ValueRangeValidness valid;

    [DebuggerBrowsable(State.Never)]
    static readonly
      IRangeSupport<T> Generic = ValueRange.Resolve<T>();

    #endregion
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ValueRange{T}"/> structure with
    /// the specified begin, end and step values.</summary>
    /// <param name="begin">Range begin value.</param>
    /// <param name="end">Range end value.</param>
    /// <param name="step">Range step value.</param>
    public ValueRange(T begin, T end, T step)
    {
      this.begin = begin;
      this.step = step;
      this.end = end;
      this.valid = 0;
      this.count = 0;

      this.valid = Generic.Validate(this);
      if (this.valid == ValueRangeValidness.Correct)
      {
        this.count = Generic.GetCount(this);
      }
    }

    internal ValueRange(T begin, T end, int count)
    {
      this.begin = begin;
      this.end = end;
      this.step = default(T);
      this.valid = 0;
      this.count = 0;

      this.step  = Generic.StepFromCount(this, count);
      this.valid = Generic.Validate(this);
      this.count = count;
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the begining value of the range.
    /// </summary>
    [DebuggerHidden]
    public T Begin
    {
      get { return this.begin; }
    }

    /// <summary>
    /// Gets the ending value of the range.
    /// </summary>
    [DebuggerHidden]
    public T End
    {
      get { return this.end; }
    }

    /// <summary>
    /// Gets the step value of the range.
    /// </summary>
    [DebuggerHidden]
    public T Step
    {
      get { return this.step; }
    }

    /// <summary>
    /// Gets the count of the steps, that would
    /// be taken while iteration over the range.
    /// </summary>
    [DebuggerHidden]
    public int Count
    {
      get { return this.count; }
    }

    /// <summary>
    /// Gets the value indicating when this instance
    /// is valid for iteration over it.</summary>
    /// <returns><b>true</b> if range is valid,
    /// otherwise <b>false</b></returns>
    [DebuggerHidden]
    public bool IsValid
    {
      get
      {
        return this.valid ==
          ValueRangeValidness.Correct;
      }
    }

    [DebuggerBrowsable(State.Never)]
    internal int ValidCount
    {
      get
      {
        if (this.valid != ValueRangeValidness.Correct)
        {
          Validate();
        }

        return this.count;
      }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Sets the begin value of the current range instance.</summary>
    /// <param name="value">New range begin value.</param>
    /// <returns>A new <see cref="ValueRange{T}"/>
    /// whose range <see cref="Begin"/>
    /// equals <paramref name="value"/>.</returns>
    public ValueRange<T> SetBegin(T value)
    {
      return new ValueRange<T>(value, this.end, this.step);
    }

    /// <summary>
    /// Sets the end value of the current range instance.</summary>
    /// <param name="value">New range end value.</param>
    /// <returns>A new <see cref="ValueRange{T}"/>
    /// whose range <see cref="End"/>
    /// equals <paramref name="value"/>.</returns>
    public ValueRange<T> SetEnd(T value)
    {
      return new ValueRange<T>(this.begin, value, this.step);
    }

    /// <summary>
    /// Sets the step value of the current range instance.</summary>
    /// <param name="value">New range step value.</param>
    /// <returns>A new <see cref="ValueRange{T}"/> whose range
    /// <see cref="Step"/> equals <paramref name="value"/>.</returns>
    public ValueRange<T> SetStep(T value)
    {
      return new ValueRange<T>(this.begin, this.end, value);
    }

    /// <summary>
    /// Sets the count of the steps, that would
    /// be taken while iteration over the range.</summary>
    /// <param name="value">New iterations count value.</param>
    /// <returns>A new <see cref="ValueRange{T}"/> whose range
    /// <see cref="Count"/> should be equal <paramref name="value"/>.</returns>
    /// <remarks>Is not guaranteed that by using this method
    /// you will get range with <see cref="Count"/> iterations -
    /// because of floating-point numbers precision,
    /// needed range step cannot be rightly evaluated for any
    /// <paramref name="value">count</paramref> value.</remarks>
    public ValueRange<T> SetCount(int value)
    {
      return new ValueRange<T>(
        this.begin, this.end,
        Generic.StepFromCount(this, value));
    }

    /// <summary>
    /// Converts the values of this range to its
    /// equivalent string representation.</summary>
    /// <returns>Expression string.</returns>
    public override string ToString()
    {
      var buf = new StringBuilder();
      buf
        .Append(this.begin)
        .Append(" - ").Append(this.end)
        .Append(" : ").Append(this.step);

      return buf.ToString();
    }

    #endregion
    #region Validate

    /// <summary>
    /// Throws an <see cref="InvalidRangeException"/> if this
    /// range instance is not valid for iteration over it.</summary>
    /// <exception cref="InvalidRangeException">
    /// Range is not valid for iteration over it.
    /// </exception>
    public void Validate()
    {
      string msg = null;
      switch (this.valid)
      {
        case ValueRangeValidness.Correct:   return;
        case ValueRangeValidness.ZeroInit:  msg = Resource.errRangeZeroInit;  break;
        case ValueRangeValidness.NotFinite: msg = Resource.errRangeNotFinite; break;
        case ValueRangeValidness.Endless:   msg = Resource.errEndlessLoop;    break;
        case ValueRangeValidness.WrongSign: msg = Resource.errWrongStepSign;  break;
        case ValueRangeValidness.TooLoong:  msg = Resource.errTooLongRange;   break;
        case ValueRangeValidness.NotSupported:
          msg = string.Format(Resource.errGenericRange, typeof(T));
          break;
      }

      throw new InvalidRangeException(msg);
    }

    #endregion
    #region Equality

    /// <summary>
    /// Returns the hash code of this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
      return
        this.begin.GetHashCode() ^
        this.end.GetHashCode()   ^
        this.step.GetHashCode();
    }

    /// <summary>
    /// Indicates whether the current <see cref="ValueRange{T}"/> is
    /// equal to another <see cref="ValueRange{T}"/> structure.</summary>
    /// <overloads>Returns a value indicating whether two instances
    /// of the <see cref="ValueRange{T}"/> is equal.</overloads>
    /// <param name="other">An another
    /// <see cref="ValueRange{T}"/> to compare with.</param>
    /// <returns><b>true</b> if the current <see cref="ValueRange{T}"/>
    /// is equal to the other <see cref="ValueRange{T}"/>;
    /// otherwise, <b>false</b>.</returns>
    public bool Equals(ValueRange<T> other)
    {
      var comparer = EqualityComparer<T>.Default;

      return comparer.Equals(this.begin, other.begin)
          && comparer.Equals(this.step, other.step)
          && comparer.Equals(this.end, other.end);
    }

    /// <summary>
    /// Indicates whether the current <see cref="ValueRange{T}"/>
    /// is equal to another object.</summary>
    /// <param name="obj">An another
    /// <see cref="object"/> to compare with.</param>
    /// <returns><b>true</b> if the current <see cref="ValueRange{T}"/>
    /// is equal to the other <see cref="ILCalc.ValueRange{T}"/>;
    /// otherwise, <b>false</b>.</returns>
    public override bool Equals(object obj)
    {
      return obj is ValueRange<T>
         && Equals((ValueRange<T>) obj);
    }

    /// <summary>
    /// Returns a value indicating whether two instances
    /// of <see cref="ValueRange{T}"/> are equal.</summary>
    /// <param name="a">First <see cref="ValueRange{T}"/>.</param>
    /// <param name="b">Second <see cref="ValueRange{T}"/>.</param>
    /// <returns><b>true</b> if <paramref name="a"/>
    /// and <paramref name="b"/> are equal;
    /// otherwise, <b>false</b>.</returns>
    public static bool operator ==(ValueRange<T> a, ValueRange<T> b)
    {
      return a.Equals(b);
    }

    /// <summary>
    /// Returns a value indicating whether two instances
    /// of <see cref="ValueRange{T}"/> are not equal.</summary>
    /// <param name="a">First <see cref="ValueRange{T}"/>.</param>
    /// <param name="b">Second <see cref="ValueRange{T}"/>.</param>
    /// <returns><b>true</b> if <paramref name="a"/>
    /// and <paramref name="b"/> are not equal;
    /// otherwise, <b>false</b>.</returns>
    public static bool operator !=(ValueRange<T> a, ValueRange<T> b)
    {
      return !a.Equals(b);
    }

    #endregion
  }

  enum ValueRangeValidness : byte
  {
    ZeroInit = 0,
    Correct, NotFinite,
    Endless, WrongSign,
    TooLoong,
    NotSupported
  }
}