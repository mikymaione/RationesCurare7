/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Reflection;
using System.Reflection.Emit;
using ILCalc.Custom;

namespace ILCalc
{
  // TODO: enumeration for operators

  static class CompilerSupport
  {
    #region Generics

    static readonly SupportCollection<object> Support;

    static CompilerSupport()
    {
      Support = new SupportCollection<object>();

      Support.Add<Int32>(new Int32ExprCompiler());
      Support.Add<Int64>(new Int64ExprCompiler());
      Support.Add<Single>(new SingleExprCompiler());
      Support.Add<Double>(new DoubleExprCompiler());
      Support.Add<Decimal>(new DecimalExprCompiler());
    }

    public static ICompiler<T> Resolve<T>()
    {
      var compiler = (ICompiler<T>) Support.Find<T>();
      if (compiler == null)
        return new UnknownExprCompiler<T>();

      return compiler;
    }

    public static bool SupportLiterals<T>(ICompiler<T> support)
    {
      return !(support is UnknownExprCompiler<T>);
    }

    #endregion
    #region Compilers

    sealed class Int32ExprCompiler : ICompiler<Int32>
    {
      static readonly MethodInfo PowMethod =
        typeof(MathHelper).GetMethod(
          "Pow", new[] { typeof(int), typeof(int) });

      static readonly MethodInfo PowChecked =
        typeof(MathHelper).GetMethod(
          "PowChecked", new[] { typeof(int), typeof(int) });

      public void LoadConst(ILGenerator il, int value)
      {
        il_EmitLoadI4(il, value);
      }

      public void Operation(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowMethod);
        }
        else
        {
          il.Emit(OpOperators[op]);
        }
      }

      /*
            dup
  IL_000b:  ldc.i4     0x80000000
  IL_0010:  beq.s      IL_0016
  IL_0013:  neg
  IL_0014:  br.s       IL_0019
  IL_0017:  ldc.i4.1
  IL_0018:  sub
  IL_0019:  stloc.2

       
       */

      public void CheckedOp(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowChecked);
        }
        else if (op == 6)
        {
          Label end = il.DefineLabel();
          Label thr = il.DefineLabel();

          il.Emit(OpCodes.Dup);
          il.Emit(OpCodes.Ldc_I4, Int32.MinValue);
          il.Emit(OpCodes.Beq, thr);
          il.Emit(OpCodes.Neg);
          il.Emit(OpCodes.Br, end);
          il.MarkLabel(thr);
          il.Emit(OpCodes.Ldc_I4_1);
          il.Emit(OpCodes.Sub_Ovf);
          il.MarkLabel(end);
        }
        else
        {
          il.Emit(OpOperatorsChecked[op]);
        }
      }

      public void LoadElem(ILGenerator il)
      {
        il.Emit(OpCodes.Ldelem_I4);
      }

      public void SaveElem(ILGenerator il)
      {
        il.Emit(OpCodes.Stelem_I4);
      }
    }

    sealed class Int64ExprCompiler : ICompiler<Int64>
    {
      static readonly MethodInfo PowMethod =
        typeof(MathHelper).GetMethod(
          "Pow", new[] { typeof(long), typeof(long) });

      static readonly MethodInfo PowChecked =
        typeof(MathHelper).GetMethod(
          "PowChecked", new[] { typeof(long), typeof(long) });

      public void LoadConst(ILGenerator il, long value)
      {
        if (value >= int.MinValue &&
            value <= int.MaxValue)
        {
          il_EmitLoadI4(il, (int) value);
          il.Emit(OpCodes.Conv_I8);
        }
        else
        {
          il.Emit(OpCodes.Ldc_I8, value);
        }
      }

      public void Operation(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowMethod);
        }
        else
        {
          il.Emit(OpOperators[op]);
        }
      }

      public void CheckedOp(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowChecked);
        }
        else if (op == (int) Code.Neg)
        {
          Label end = il.DefineLabel();
          Label thr = il.DefineLabel();

          il.Emit(OpCodes.Dup);
          il.Emit(OpCodes.Ldc_I8, Int64.MinValue);
          il.Emit(OpCodes.Beq, thr);
          il.Emit(OpCodes.Neg);
          il.Emit(OpCodes.Br, end);
          il.MarkLabel(thr);
          il.Emit(OpCodes.Ldc_I4_1);
          il.Emit(OpCodes.Conv_I8);
          il.Emit(OpCodes.Sub_Ovf);
          il.MarkLabel(end);
        }
        else
        {
          il.Emit(OpOperatorsChecked[op]);
        }
      }

      public void LoadElem(ILGenerator il)
      {
        il.Emit(OpCodes.Ldelem_I8);
      }

      public void SaveElem(ILGenerator il)
      {
        il.Emit(OpCodes.Stelem_I8);
      }
    }

    sealed class SingleExprCompiler : ICompiler<Single>
    {
      static readonly MethodInfo PowMethod =
        typeof(MathHelper).GetMethod(
          "Pow", new[]{ typeof(float), typeof(float) });

      static readonly MethodInfo PowChecked =
        typeof(MathHelper).GetMethod(
          "PowChecked", new[]{ typeof(float), typeof(float) });

      public void LoadConst(ILGenerator il, float value)
      {
        il.Emit(OpCodes.Ldc_R4, value);
      }

      public void Operation(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowMethod);
        }
        else
        {
          il.Emit(OpOperators[op]);
        }
      }

      public void CheckedOp(ILGenerator il, int op)
      {
        if (op == (int) Code.Pow)
        {
          il.Emit(OpCodes.Call, PowChecked);
        }
        else
        {
          il.Emit(OpOperators[op]);
          il.Emit(OpCodes.Ckfinite);
        }
      }

      public void LoadElem(ILGenerator il)
      {
        il.Emit(OpCodes.Ldelem_R4);
      }

      public void SaveElem(ILGenerator il)
      {
        il.Emit(OpCodes.Stelem_R4);
      }
    }

    sealed class DoubleExprCompiler : ICompiler<Double>
    {
      static readonly MethodInfo PowMethod =
        typeof(Math).GetMethod("Pow");

      public void LoadConst(ILGenerator il, double value)
      {
        il.Emit(OpCodes.Ldc_R8, value);
      }

      public void Operation(ILGenerator il, int op)
      {
        if (op != (int) Code.Pow)
        {
          il.Emit(OpOperators[op]);
        }
        else
        {
          il.Emit(OpCodes.Call, PowMethod);
        }
      }

      public void CheckedOp(ILGenerator il, int op)
      {
        Operation(il, op);
        il.Emit(OpCodes.Ckfinite);
      }

      public void LoadElem(ILGenerator il)
      {
        il.Emit(OpCodes.Ldelem_R8);
      }

      public void SaveElem(ILGenerator il)
      {
        il.Emit(OpCodes.Stelem_R8);
      }

      
    }

    sealed class DecimalExprCompiler : ICompiler<Decimal>
    {
      #region StaticData

      const decimal MinI4 = Int32.MinValue;
      const decimal MaxI4 = Int32.MaxValue;
      const decimal MaxI8 = Int64.MaxValue;
      const decimal MinI8 = Int64.MinValue;

      static readonly Type DecimalType = typeof(Decimal);

      static readonly MethodInfo[] Operators = new[]
      {
        DecimalType.GetMethod("Subtract", PublicStatic),
        DecimalType.GetMethod("Add", PublicStatic),
        DecimalType.GetMethod("Multiply", PublicStatic),
        DecimalType.GetMethod("Divide", PublicStatic),
        DecimalType.GetMethod("Remainder", PublicStatic),
        typeof(MathHelper).GetMethod(
          "Pow", new[] { DecimalType, DecimalType }),
        DecimalType.GetMethod("Negate", PublicStatic),
      };

      static readonly ConstructorInfo CtorFromInt32 =
        DecimalType.GetConstructor(
          PublicInstance, null, new[] { typeof(int) }, null);

      static readonly ConstructorInfo CtorFromInt64 =
        DecimalType.GetConstructor(
          PublicInstance, null, new[] { typeof(long) }, null);

#if SILVERLIGHT

      static readonly ConstructorInfo CtorFromIntBits =
        DecimalType.GetConstructor(
          PublicInstance, null, new[] { typeof(int), typeof(int),
          typeof(int), typeof(bool), typeof(byte) }, null);

#else

      static readonly ConstructorInfo CtorFromIntBits =
        DecimalType.GetConstructor(
          BindingFlags.Instance | BindingFlags.NonPublic,
          null, new[] {
            typeof(int), typeof(int),
            typeof(int), typeof(int) }, null);

#endif

      #endregion

      public void LoadConst(ILGenerator il, decimal value)
      {
        // if value is integral:
        if (value == Decimal.Truncate(value))
        {
          // fit into int32:
          if (value >= MinI4 && value <= MaxI4)
          {
            il_EmitLoadI4(il, Decimal.ToInt32(value));
            il.Emit(OpCodes.Newobj, CtorFromInt32);
            return;
          }

          // fit into int64:
          if (value >= MinI8 && value <= MaxI8)
          {
            long x = Decimal.ToInt64(value);
            il.Emit(OpCodes.Ldc_I8, x);
            il.Emit(OpCodes.Newobj, CtorFromInt64);
            return;
          }
        }

        int[] bits = Decimal.GetBits(value);

        il_EmitLoadI4(il, bits[0]);
        il_EmitLoadI4(il, bits[1]);
        il_EmitLoadI4(il, bits[2]);

#if SILVERLIGHT
        il_EmitLoadI4(il, (bits[3] >> 31) & 1);
        il_EmitLoadI4(il, (byte) (bits[3] >> 16));
#else
        il_EmitLoadI4(il, bits[3]);
#endif
        il.Emit(OpCodes.Newobj, CtorFromIntBits);
      }

      public void Operation(ILGenerator il, int op)
      {
        il.Emit(OpCodes.Call, Operators[op]);
      }

      public void CheckedOp(ILGenerator il, int op)
      {
        Operation(il, op);
      }

      public void LoadElem(ILGenerator il) { }
      public void SaveElem(ILGenerator il) { }
    }

    sealed class UnknownExprCompiler<T> : ICompiler<T>
    {
      static Exception MakeException(string name)
      {
        return new NotSupportedException(string.Format(
          Resource.errNotSupported, name, typeof(T)));
      }

      const string Operators = "-+*/%^";

      public void LoadConst(ILGenerator il, T value)
      {
        throw MakeException("load const");
      }

      public void Operation(ILGenerator il, int op)
      {
        throw MakeException(Operators[op].ToString());
      }

      public void CheckedOp(ILGenerator il, int op)
      {
        throw MakeException(Operators[op].ToString());
      }

      public void LoadElem(ILGenerator il) { throw MakeException("load elem"); }
      public void SaveElem(ILGenerator il) { throw MakeException("store elem"); }
    }

    #endregion
    #region Helpers

    // ReSharper disable InconsistentNaming

    public static void il_EmitLoadI4(ILGenerator il, int value)
    {
      if (value < sbyte.MinValue ||
          value > sbyte.MaxValue)
      {
        il.Emit(OpCodes.Ldc_I4, value);
      }
      else if (value < -1 || value > 8)
      {
        il.Emit(OpCodes.Ldc_I4_S, (byte) value);
      }
      else
      {
        il.Emit(OpLoadConst[value+1]);
      }
    }

    // ReSharper restore InconsistentNaming

    #endregion
    #region StaticData

    const BindingFlags PublicStatic =
      BindingFlags.Public | BindingFlags.Static;

    const BindingFlags PublicInstance =
      BindingFlags.Public | BindingFlags.Instance;

    static readonly OpCode[] OpOperators =
      {
        OpCodes.Sub,
        OpCodes.Add,
        OpCodes.Mul,
        OpCodes.Div,
        OpCodes.Rem,
        OpCodes.Nop,
        OpCodes.Neg
      };

    static readonly OpCode[] OpOperatorsChecked =
      {
        OpCodes.Sub_Ovf,
        OpCodes.Add_Ovf,
        OpCodes.Mul_Ovf,
        OpCodes.Div,
        OpCodes.Rem,
        OpCodes.Nop,
        OpCodes.Neg
      };

    static readonly OpCode[] OpLoadConst =
      {
        OpCodes.Ldc_I4_M1,
        OpCodes.Ldc_I4_0,
        OpCodes.Ldc_I4_1,
        OpCodes.Ldc_I4_2,
        OpCodes.Ldc_I4_3,
        OpCodes.Ldc_I4_4,
        OpCodes.Ldc_I4_5,
        OpCodes.Ldc_I4_6,
        OpCodes.Ldc_I4_7,
        OpCodes.Ldc_I4_8
      };

    #endregion
  }
}