/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using ILCalc.Custom;

namespace ILCalc
{
  using FactoryCollection = SupportCollection<Delegate>;

  #region Factory Delegates

  delegate QuickInterpret<T> QuickFactory<T>(T[] arguments);

  delegate Interpret<T> InterpFactory<T>(
    string expr, int args, InterpretCreator<T> creator);

  #endregion

  static class Arithmetics
  {
    #region Initialize

    static readonly FactoryCollection
      QuickSupport, InterpSupport,
      QuickChecked, InterpChecked;

    static Arithmetics()
    {
      QuickSupport = new FactoryCollection();
      QuickChecked = new FactoryCollection();
      InterpSupport = new FactoryCollection();
      InterpChecked = new FactoryCollection();

      Register<Int32, Int32Arithmetic>(false);
      Register<Int64, Int64Arithmetic>(false);
      Register<Single, SingleArithmetic>(false);
      Register<Double, DoubleArithmetic>(false);
      Register<Decimal, DecimalArithmetic>(false);

      Register<Int32, Int32CheckedArithmetic>(true);
      Register<Int64, Int64CheckedArithmetic>(true);
    }

    static void Register<T, TSupport>(bool checks)
      where TSupport : IArithmetic<T>, new()
    {
      InterpFactory<T> interpFactory =
        (s, a, c) => new InterpretImpl<T, TSupport>(s,a,c);

      QuickFactory<T> quickFactory =
        args => new QuickInterpretImpl<T, TSupport>(args);

      if (checks)
      {
        QuickChecked.Add<T>(quickFactory);
        InterpChecked.Add<T>(interpFactory);
      }
      else
      {
        QuickSupport.Add<T>(quickFactory);
        InterpSupport.Add<T>(interpFactory);
      }
    }

    #endregion
    #region Resolvers

    public static QuickFactory<T> ResolveQuick<T>(bool checks)
    {
      if (checks)
        return (QuickFactory<T>) QuickChecked.Find<T>();

      var factory = QuickSupport.Find<T>();
      if (factory == null)
      {
        return args =>
          new QuickInterpretImpl<T, UnknownArithmetic<T>>(args);
      }

      return (QuickFactory<T>) factory;
    }

    public static InterpFactory<T> ResolveInterp<T>(bool checks)
    {
      if (checks)
        return (InterpFactory<T>) InterpChecked.Find<T>();

      var factory = InterpSupport.Find<T>();
      if (factory == null)
      {
        return (s, a, c) =>
          new InterpretImpl<T, UnknownArithmetic<T>>(s,a,c);
      }

      return (InterpFactory<T>) factory;
    }

    #endregion
    #region Built-in

    struct Int32Arithmetic : IArithmetic<Int32>
    {
      public int Zero { get { return 0; } }
      public int One  { get { return 1; } }

      public int Neg(int x) { return -x; }
      public int Add(int x, int y) { return x + y; }
      public int Sub(int x, int y) { return x - y; }
      public int Mul(int x, int y) { return x * y; }
      public int Div(int x, int y) { return x / y; }
      public int Mod(int x, int y) { return x % y; }

      public int Pow(int x, int y)
      {
        return MathHelper.Pow(x, y);
      }

      public int? IsIntergal(int value) { return value;  }
    }

    struct Int64Arithmetic : IArithmetic<Int64>
    {
      public long Zero { get { return 0; } }
      public long One  { get { return 1; } }

      public long Neg(long x) { return -x; }
      public long Add(long x, long y) { return x + y; }
      public long Sub(long x, long y) { return x - y; }
      public long Mul(long x, long y) { return x * y; }
      public long Div(long x, long y) { return x / y; }
      public long Mod(long x, long y) { return x % y; }

      public long Pow(long x, long y)
      {
        return MathHelper.Pow(x, y);
      }

      public int? IsIntergal(long value)
      {
        if (int.MinValue < value ||
            int.MaxValue < value) return null;
        return (int) value;
      }
    }

    struct SingleArithmetic : IArithmetic<Single>
    {
      public float Zero { get { return 0.0f; } }
      public float One  { get { return 1.0f; } }

      public float Neg(float x) { return -x; }
      public float Add(float x, float y) { return x + y; }
      public float Sub(float x, float y) { return x - y; }
      public float Mul(float x, float y) { return x * y; }
      public float Div(float x, float y) { return x / y; }
      public float Mod(float x, float y) { return x % y; }

      public float Pow(float x, float y)
      {
        return (float) Math.Pow(x, y);
      }

      public int? IsIntergal(float value)
      {
        var xint = (int) value;
        if (xint == value) return xint;
        return null;
      }
    }

    struct DoubleArithmetic : IArithmetic<Double>
    {
      public double Zero { get { return 0.0; } }
      public double One  { get { return 1.0; } }

      public double Neg(double x) { return -x; }
      public double Add(double x, double y) { return x + y; }
      public double Sub(double x, double y) { return x - y; }
      public double Mul(double x, double y) { return x * y; }
      public double Div(double x, double y) { return x / y; }
      public double Mod(double x, double y) { return x % y; }

      public double Pow(double x, double y)
      {
        return Math.Pow(x, y);
      }

      public int? IsIntergal(double value)
      {
        var xint = (int) value;
        if (xint == value) return xint;
        return null;
      }
    }

    struct DecimalArithmetic : IArithmetic<Decimal>
    {
      public decimal Zero { get { return 0M; } }
      public decimal One  { get { return 1M; } }

      public decimal Neg(decimal x) { return -x; }
      public decimal Add(decimal x, decimal y) { return x + y; }
      public decimal Sub(decimal x, decimal y) { return x - y; }
      public decimal Mul(decimal x, decimal y) { return x * y; }
      public decimal Div(decimal x, decimal y) { return x / y; }
      public decimal Mod(decimal x, decimal y) { return x % y; }

      public decimal Pow(decimal x, decimal y)
      {
        return MathHelper.Pow(x, y);
      }

      public int? IsIntergal(decimal value)
      {
        var integral = (int) value;
        if (value == integral) return integral;
        return null;
      }
    }

    struct Int32CheckedArithmetic : IArithmetic<Int32>
    {
      public int Zero { get { return 0; } }
      public int One  { get { return 1; } }

      public int Neg(int x)        { return checked(-x); }
      public int Add(int x, int y) { return checked(x + y); }
      public int Sub(int x, int y) { return checked(x - y); }
      public int Mul(int x, int y) { return checked(x * y); }
      public int Div(int x, int y) { return checked(x / y); }
      public int Mod(int x, int y) { return checked(x % y); }

      public int Pow(int x, int y)
      {
        return MathHelper.PowChecked(x, y);
      }

      public int? IsIntergal(int value) { return value; }
    }

    struct Int64CheckedArithmetic : IArithmetic<Int64>
    {
      public long Zero { get { return 0; } }
      public long One  { get { return 1; } }

      public long Neg(long x)         { return checked(-x); }
      public long Add(long x, long y) { return checked(x + y); }
      public long Sub(long x, long y) { return checked(x - y); }
      public long Mul(long x, long y) { return checked(x * y); }
      public long Div(long x, long y) { return checked(x / y); }
      public long Mod(long x, long y) { return checked(x % y); }

      public long Pow(long x, long y)
      {
        return MathHelper.PowChecked(x, y);
      }

      public int? IsIntergal(long value)
      {
        if (int.MinValue < value ||
            int.MaxValue > value) return null;
        return (int) value;
      }
    }

    sealed class UnknownArithmetic<T> : IArithmetic<T>
    {
      static Exception MakeException(string name)
      {
        Debug.Assert(name != null);

        return new NotSupportedException(string.Format(
          Resource.errNotSupported, name, typeof(T)));
      }

      public T Zero { get { throw MakeException("get_Zero"); } }
      public T One  { get { throw MakeException("get_One");  } }

      public T Neg(T x)      { throw MakeException("-"); }
      public T Add(T x, T y) { throw MakeException("+"); }
      public T Sub(T x, T y) { throw MakeException("-"); }
      public T Mul(T x, T y) { throw MakeException("*"); }
      public T Div(T x, T y) { throw MakeException("/"); }
      public T Mod(T x, T y) { throw MakeException("%"); }
      public T Pow(T x, T y) { throw MakeException("^"); }

      public int? IsIntergal(T value) { return null; }
    }

    #endregion
  }
}
