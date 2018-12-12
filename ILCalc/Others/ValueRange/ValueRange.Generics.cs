/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using ILCalc.Custom;

namespace ILCalc
{
  /// <summary>
  /// Provides the static methods for creating
  /// <see cref="ValueRange{T}"/> instances.</summary>
  /// <remarks>This static class helps to create
  /// <see cref="ValueRange{T}"/> instances without
  /// specifying type parameter.</remarks>
  public static class ValueRange
  {
    #region Generics

    static readonly SupportCollection<object> Support;

    static ValueRange()
    {
      Support = new SupportCollection<object>(3);

      Support.Add<Single>(new SingleRangeSupport());
      Support.Add<Double>(new DoubleRangeSupport());
      Support.Add<Int32>(new Int32RangeSupport());
      Support.Add<Int64>(new Int64RangeSupport());
      Support.Add<Decimal>(new DecimalRangeSupport());
    }

    internal static IRangeSupport<T> Resolve<T>()
    {
      object support = Support.Find<T>();
      if(support == null)
      {
        return new UnknownRangeSupport<T>();
      }

      return (IRangeSupport<T>) support;
    }

    #endregion
    #region Methods

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ValueRange{T}"/> structure with the
    /// specified begin, end and count values.</summary>
    /// <param name="begin">Range begin value.</param>
    /// <param name="end">Range end value.</param>
    /// <param name="count">Range iterations count value.</param>
    /// <typeparam name="T">Range values type.</typeparam>
    /// <remarks>Is not guaranteed that by using this method
    /// you will get range with <paramref name="count"/> iterations -
    /// because of floating-point numbers precision,
    /// needed range step cannot be rightly evaluated
    /// for any <paramref name="count"/> value.</remarks>
    /// <returns>Returnds a new instance of the
    /// <see cref="ValueRange{T}"/> structure with the
    /// specified begin, end and count values.</returns>
    public static ValueRange<T> FromCount<T>(T begin, T end, int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException("count");

      return new ValueRange<T>(begin, end, count);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ValueRange{T}"/> structure with the
    /// specified begin, end and step values.</summary>
    /// <param name="begin">Range begin value.</param>
    /// <param name="end">Range end value.</param>
    /// <param name="step">Range step value.</param>
    /// <typeparam name="T">Range values type.</typeparam>
    /// <returns>Returns a new instance of the
    /// <see cref="ValueRange{T}"/> structure with the
    /// specified begin, end and step values.</returns>
    public static ValueRange<T> Create<T>(T begin, T end, T step)
    {
      return new ValueRange<T>(begin, end, step);
    }

    #endregion
    #region Supports

    sealed class Int32RangeSupport : IRangeSupport<Int32>
    {
      public int StepFromCount(ValueRange<int> r, int count)
      {
        return (r.End - r.Begin) / count;
      }

      public int GetCount(ValueRange<int> r)
      {
        return (r.End - r.Begin) / r.Step;
      }

      public ValueRangeValidness Validate(ValueRange<int> r)
      {
        if (r.Step == 0)
        {
          return ValueRangeValidness.Endless;
        }

        if (r.Begin > r.End != r.Step < 0)
        {
          return ValueRangeValidness.WrongSign;
        }

        return ValueRangeValidness.Correct;
      }
    }

    sealed class Int64RangeSupport : IRangeSupport<Int64>
    {
      public long StepFromCount(ValueRange<long> r, int count)
      {
        return (r.End - r.Begin) / count;
      }

      public int GetCount(ValueRange<long> r)
      {
        return (int) ((r.End - r.Begin) / r.Step);
      }

      public ValueRangeValidness Validate(ValueRange<long> r)
      {
        if (r.Step == 0)
        {
          return ValueRangeValidness.Endless;
        }

        if (r.Begin > r.End != r.Step < 0)
        {
          return ValueRangeValidness.WrongSign;
        }

        if ((r.End - r.Begin) / r.Step >= int.MaxValue)
        {
          return ValueRangeValidness.TooLoong;
        }

        return ValueRangeValidness.Correct;
      }
    }

    sealed class SingleRangeSupport : IRangeSupport<Single>
    {
      public float StepFromCount(
        ValueRange<float> r, int count)
      {
        return (r.End - r.Begin) / count;
      }

      public int GetCount(ValueRange<float> r)
      {
        float len = r.End - r.Begin;
        int rcount = (int) (len / r.Step) + 1;

        if (len % r.Step == 0) rcount--;
        return rcount;
      }

      public ValueRangeValidness Validate(ValueRange<float> r)
      {
        if (float.IsInfinity(r.Begin) || float.IsNaN(r.Begin)
         || float.IsInfinity(r.Step)  || float.IsNaN(r.Step)
         || float.IsInfinity(r.End)   || float.IsNaN(r.End))
        {
          return ValueRangeValidness.NotFinite;
        }

        if (r.Begin + r.Step == r.Begin ||
            r.End   - r.Step == r.End)
        {
          return ValueRangeValidness.Endless;
        }

        if (r.Begin > r.End != r.Step < 0)
        {
          return ValueRangeValidness.WrongSign;
        }

        if ((r.End - r.Begin) / r.Step >= int.MaxValue)
        {
          return ValueRangeValidness.TooLoong;
        }

        return ValueRangeValidness.Correct;
      }
    }

    sealed class DoubleRangeSupport : IRangeSupport<Double>
    {
      public double StepFromCount(
        ValueRange<double> r, int count)
      {
        return (r.End - r.Begin) / count;
      }

      public int GetCount(ValueRange<double> r)
      {
        double len = r.End - r.Begin;
        int rcount = (int) (len / r.Step) + 1;

        if (len % r.Step == 0) rcount--;
        return rcount;
      }

      public ValueRangeValidness Validate(ValueRange<double> r)
      {
        if (double.IsInfinity(r.Begin) || double.IsNaN(r.Begin)
         || double.IsInfinity(r.Step)  || double.IsNaN(r.Step)
         || double.IsInfinity(r.End)   || double.IsNaN(r.End))
        {
          return ValueRangeValidness.NotFinite;
        }

        if (r.Begin + r.Step == r.Begin ||
            r.End   - r.Step == r.End)
        {
          return ValueRangeValidness.Endless;
        }

        if (r.Begin > r.End != r.Step < 0)
        {
          return ValueRangeValidness.WrongSign;
        }

        if ((r.End - r.Begin) / r.Step >= int.MaxValue)
        {
          return ValueRangeValidness.TooLoong;
        }

        return ValueRangeValidness.Correct;
      }
    }

    sealed class DecimalRangeSupport : IRangeSupport<Decimal>
    {
      public decimal StepFromCount(ValueRange<decimal> r, int count)
      {
        return (r.End - r.Begin) / count;
      }

      // TODO: is this correct for decimal?
      public int GetCount(ValueRange<decimal> r)
      {
        decimal len = r.End - r.Begin;
        int rcount = (int) (len / r.Step) + 1;

        if (len % r.Step == 0) rcount--;
        return rcount;
      }

      public ValueRangeValidness Validate(ValueRange<decimal> r)
      {
        if (r.Begin + r.Step == r.Begin ||
            r.End   - r.Step == r.End)
        {
          return ValueRangeValidness.Endless;
        }

        if (r.Begin > r.End != r.Step < 0)
        {
          return ValueRangeValidness.WrongSign;
        }

        if ((r.End - r.Begin) / r.Step >= int.MaxValue)
        {
          return ValueRangeValidness.TooLoong;
        }

        return ValueRangeValidness.Correct;
      }
    }

    sealed class UnknownRangeSupport<T> : IRangeSupport<T>
    {
      public T StepFromCount(ValueRange<T> r, int count)
      {
        return default(T);
      }

      public int GetCount(ValueRange<T> r) { return 0; }

      public ValueRangeValidness Validate(ValueRange<T> r)
      {
        return ValueRangeValidness.NotSupported;
      }
    }

    #endregion
  }
}
