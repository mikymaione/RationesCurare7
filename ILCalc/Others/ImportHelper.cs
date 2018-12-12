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
  static class ImportHelper
  {
    #region Support

    delegate object Factory();

    static readonly SupportCollection<Factory> Const;
    static readonly SupportCollection<Factory> Funcs;

    static ImportHelper()
    {
      Const = new SupportCollection<Factory>();

      Const.Add<Int32>(Int32Constants);
      Const.Add<Int64>(Int64Constants);
      Const.Add<Single>(SingleConstants);
      Const.Add<Double>(DoubleConstants);
      Const.Add<Decimal>(DecimalConstants);

      Funcs = new SupportCollection<Factory>();
      Funcs.Add<Int32>(Int32Functions);
      Funcs.Add<Int64>(Int64Functions);
      Funcs.Add<Single>(SingleFunctions);
      Funcs.Add<Double>(DoubleFunctions);
      Funcs.Add<Decimal>(DecimalFunctions);
    }

    public static ConstantDictionary<T> ResolveConstants<T>()
    {
      var factory = Const.Find<T>();
      if (factory == null)
        return new ConstantDictionary<T>();

      return (ConstantDictionary<T>) factory();
    }

    public static FunctionCollection<T> ResolveFunctions<T>()
    {
      var factory = Funcs.Find<T>();
      if (factory == null)
        return new FunctionCollection<T>();

      return (FunctionCollection<T>) factory();
    }

    #endregion
    #region Constants

    static object Int32Constants()
    {
      return new ConstantDictionary<int>
      {
        { "MaxValue", Int32.MaxValue },
        { "MinValue", Int32.MinValue }
      };
    }

    static object Int64Constants()
    {
      return new ConstantDictionary<long>
      {
        { "MaxValue", Int64.MaxValue },
        { "MinValue", Int64.MinValue }
      };
    }

    static object SingleConstants()
    {
      return new ConstantDictionary<float>
      {
        { "E", (float) Math.E },
        { "Pi", (float) Math.PI },
        { "NaN", Single.NaN },
        { "Inf", Single.PositiveInfinity },
        { "Eps", Single.Epsilon }
      };
    }

    static object DoubleConstants()
    {
      return new ConstantDictionary<double>
      {
        { "E", Math.E },
        { "Pi", Math.PI },
        { "NaN", Double.NaN },
        { "Inf", Double.PositiveInfinity },
        { "Eps", Double.Epsilon }
      };
    }

    static object DecimalConstants()
    {
      return new ConstantDictionary<decimal>
      {
        { "MaxValue", Decimal.MaxValue },
        { "MinValue", Decimal.MinValue }
      };
    }

    #endregion
    #region Functions

    static object Int32Functions()
    {
      return new FunctionCollection<int>
      {
        { "Abs", Math.Abs },
        { "Max", Math.Max },
        { "Min", Math.Min },
        { "Sign", Math.Sign }
      };
    }

    static object Int64Functions()
    {
      return new FunctionCollection<long>
      {
        { "Abs", Math.Abs },
        { "Max", Math.Max },
        { "Min", Math.Min }
      };
    }

    static object SingleFunctions()
    {
      return new FunctionCollection<float>
      {
        { "Abs", Math.Abs },
        { "Max", Math.Max },
        { "Min", Math.Min }
      };
    }

    static object DoubleFunctions()
    {
      return new FunctionCollection<double>
      {
        { "Abs", Math.Abs },
        { "Sin", Math.Sin }, { "Sinh", Math.Sinh }, { "Asin", Math.Asin },
        { "Cos", Math.Cos }, { "Cosh", Math.Cosh }, { "Acos", Math.Acos },
        { "Tan", Math.Tan }, { "Tanh", Math.Tanh }, { "Atan", Math.Atan },
        { "Atan2", Math.Atan2 }, { "Ceil", Math.Ceiling },
        { "Floor", Math.Floor }, { "Round", Math.Round },
#if !CF
#if !SILVERLIGHT
        { "Trunc", Math.Truncate },
#endif
        { "Log", (EvalFunc2<double>) Math.Log },
#endif
        { "Log", (EvalFunc1<double>) Math.Log },
        { "Log10", Math.Log10 },
        { "Min", Math.Min }, { "Max", Math.Max },
        { "Exp", Math.Exp }, { "Pow", Math.Pow },
        { "Sqrt", Math.Sqrt }
      };
    }

    static object DecimalFunctions()
    {
      return new FunctionCollection<decimal>
      {
        { "Abs", Math.Abs },
        { "Max", Math.Max },
        { "Min", Math.Min },
        { "Round", Math.Round },
#if FULL_FW
        { "Floor", Math.Floor },
        { "Ceil",  Math.Ceiling },
        { "Trunc", Math.Truncate }
#endif
      };
    }

    #endregion
  }
}
