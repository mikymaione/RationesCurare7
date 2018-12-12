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
using System.Reflection.Emit;

namespace ILCalc
{
  sealed class TabulatorCompiler<T> : CompilerBase<T>
  {
    #region Fields

    readonly List<LocalBuilder> argsLocals;
    readonly Stack<LocalBuilder> stepLocals;
    readonly Stack<LocalBuilder> locals;
    readonly Stack<Label> labels;
    readonly int argsCount;

    #endregion
    #region Constructor

    public TabulatorCompiler(int argsCount, bool checks)
      : base(checks)
    {
      Debug.Assert(argsCount > 0);

      this.labels = new Stack<Label>(argsCount * 2);
      this.argsLocals = new List<LocalBuilder>(argsCount);

      if (argsCount > 2)
      {
        this.stepLocals = new Stack<LocalBuilder>(argsCount);
        this.locals = new Stack<LocalBuilder>(2 * argsCount);
      }
      else
      {
        this.locals = new Stack<LocalBuilder>(
          argsCount == 1 ? 1 : 3);
      }

      this.argsCount = argsCount;
    }

    #endregion
    #region Properties

    List <LocalBuilder> ArgsLocs { get { return this.argsLocals; } }
    Stack<LocalBuilder> StepLocs { get { return this.stepLocals; } }
    Stack<LocalBuilder> Locals   { get { return this.locals; } }
    Stack<Label> Labels { get { return this.labels; } }
    int ArgsCount    { get { return this.argsCount; } }

    #endregion
    #region Methods

    protected override void EmitLoadArg(ILGenerator il, int index)
    {
      Debug.Assert(index >= 0);
      Debug.Assert(index < ArgsLocs.Count);

      il.Emit(OpCodes.Ldloc, ArgsLocs[index]);
    }

    public Delegate CreateDelegate()
    {
      int args = ArgsCount - 1;
      if (args > 2) args = 2;

      Type[] argsTypes = ArgsTypes[args];
      object owner = OwnerFixup(ref argsTypes);

#if SILVERLIGHT

      var method = new DynamicMethod(
        "tabulator", ArgsTypes[args][0], argsTypes);

#else

      var method = new DynamicMethod(
        "tabulator", ArgsTypes[args][0],
        argsTypes, OwnerType, true);

#endif

      // ======================================================

      ILGenerator il = method.GetILGenerator();

      if (args < 2)
      {
        BeginSimple(il);
        CodeGen(il);
        EndSimple(il);
      }
      else
      {
        BeginMulti(il);
        CodeGen(il);
        EndMulti(il);
      }

      il_EmitLoadArg(il, 0);
      il.Emit(OpCodes.Ret);

      // DynamicMethodVisualizer.Visualizer.Show(method);
      // ======================================================

      return GetDelegate(method, DelegateTypes[args], owner);
    }

    #endregion
    #region Emitters

    void BeginSimple(ILGenerator il)
    {
      Debug.Assert(il != null);
      Debug.Assert(ArgsCount > 0);
      Debug.Assert(ArgsCount < 3);

      var index = il.DeclareLocal(IndexType);
      var var = il.DeclareLocal(TypeHelper<T>.ValueType);

      // T x = beginx;
      if (ArgsCount == 1)
           il_EmitLoadArg(il, 2);
      else il_EmitLoadArg(il, 3);
      il.Emit(OpCodes.Stloc, var);

      // int i = 0;
      il.Emit(OpCodes.Ldc_I4_0);
      il.Emit(OpCodes.Stloc, index);

      EmitLoopBegin(il);

      ArgsLocs.Add(var);
      Locals.Push(index);

      if (ArgsCount == 2)
      {
        var index2 = il.DeclareLocal(IndexType);
        var array = il.DeclareLocal(TypeHelper<T>.ArrayType);
        var var2 = il.DeclareLocal(TypeHelper<T>.ValueType);

        // T b = a[i];
        il_EmitLoadArg(il, 0);
        il.Emit(OpCodes.Ldloc, index);
        il.Emit(OpCodes.Ldelem_Ref);
        il.Emit(OpCodes.Stloc, array);

        // T y = begin2;
        il_EmitLoadArg(il, 4);
        il.Emit(OpCodes.Stloc, var2);

        // int j = 0;
        il.Emit(OpCodes.Ldc_I4_0);
        il.Emit(OpCodes.Stloc, index2);

        EmitLoopBegin(il);

        ArgsLocs.Add(var2);
        Locals.Push(index2);
        Locals.Push(array);

        // b[i] = 
        il.Emit(OpCodes.Ldloc, array);
        il.Emit(OpCodes.Ldloc, index2);
        il_EmitLoadAdress(il);
      }
      else
      {
        // a[i] = 
        il_EmitLoadArg(il, 0);
        il.Emit(OpCodes.Ldloc, index);
        il_EmitLoadAdress(il);
      }
    }

    void BeginMulti(ILGenerator il)
    {
      Debug.Assert(il != null);
      Debug.Assert(ArgsCount > 2);

      for (int i = 0; i < ArgsCount; i++)
      {
        LocalBuilder step =
          il.DeclareLocal(TypeHelper<T>.ValueType);

        il_EmitLoadArg(il, 1);
        il_EmitLoadI4(il, i);
        il_EmitLoadElem(il);
        il.Emit(OpCodes.Stloc, step);

        StepLocs.Push(step);
      }

      LocalBuilder lastIndex = null;
      LocalBuilder lastArray = null;

      for (int i = 0, t = ArgsCount; i < ArgsCount; i++, t--)
      {
        Type arrayType = TypeHelper<T>.GetArrayType(t);

        var array = il.DeclareLocal(arrayType);
        var index = il.DeclareLocal(IndexType);
        var var = il.DeclareLocal(TypeHelper<T>.ValueType);

        if (i == 0)
        {
          // a = (T[][][]) ar;
          il_EmitLoadArg(il, 0);
          il.Emit(OpCodes.Castclass, arrayType);
          il.Emit(OpCodes.Stloc, array);
        }
        else
        {
          Debug.Assert(lastArray != null);
          Debug.Assert(lastIndex != null);

          // T[] b = a[i];  
          il.Emit(OpCodes.Ldloc, lastArray);
          il.Emit(OpCodes.Ldloc, lastIndex);
          il.Emit(OpCodes.Ldelem_Ref);
          il.Emit(OpCodes.Stloc, array);
        }

        // T x = begins[0];
        il_EmitLoadArg(il, 1);
        il_EmitLoadI4(il, i + ArgsCount);
        il_EmitLoadElem(il);
        il.Emit(OpCodes.Stloc, var);

        // i++;
        il.Emit(OpCodes.Ldc_I4_0);
        il.Emit(OpCodes.Stloc, index);

        EmitLoopBegin(il);

        ArgsLocs.Add(var);
        Locals.Push(array);
        Locals.Push(index);

        lastArray = array;
        lastIndex = index;
      }

      Debug.Assert(lastIndex != null);
      Debug.Assert(lastArray != null);

      // c[z] = 
      il.Emit(OpCodes.Ldloc, lastArray);
      il.Emit(OpCodes.Ldloc, lastIndex);
      il_EmitLoadAdress(il);
    }

    void EndSimple(ILGenerator il)
    {
      Debug.Assert(il != null);
      Debug.Assert(ArgsCount > 0);
      Debug.Assert(ArgsCount < 3);

      il_EmitSaveElem(il);

      if (ArgsCount == 2)
      {
        var array = Locals.Pop();
        var index2 = Locals.Pop();
        var var2 = ArgsLocs[1];

        // x += step;
        il.Emit(OpCodes.Ldloc, var2);
        il_EmitLoadArg(il, 2);
        Generic.Operation(il, 1);
        il.Emit(OpCodes.Stloc, var2);

        EmitLoopEnd(il, index2, array);
      }

      var index = Locals.Pop();
      var var = ArgsLocs[0];

      il.Emit(OpCodes.Ldloc, var);
      il_EmitLoadArg(il, 1);
      Generic.Operation(il, 1);
      il.Emit(OpCodes.Stloc, var);

      EmitLoopEnd(il, index, null);
    }

    void EndMulti(ILGenerator il)
    {
      Debug.Assert(il != null);
      Debug.Assert(ArgsCount > 2);

      il_EmitSaveElem(il);

      for (int i = 0, j = ArgsCount - 1; i < ArgsCount; i++, j--)
      {
        var index = Locals.Pop();
        var array = Locals.Pop();
        var var = ArgsLocs[j];

        // x += xstep;
        il.Emit(OpCodes.Ldloc, var);
        il.Emit(OpCodes.Ldloc, StepLocs.Pop());
        Generic.Operation(il, 1);
        il.Emit(OpCodes.Stloc, var);

        EmitLoopEnd(il, index, array);
      }
    }

    void EmitLoopBegin(ILGenerator il)
    {
      Label condition = il.DefineLabel();
      Label loopBegin = il.DefineLabel();

      il.Emit(OpCodes.Br, condition);
      il.MarkLabel(loopBegin);

      Labels.Push(loopBegin);
      Labels.Push(condition);
    }

    void EmitLoopEnd(ILGenerator il, LocalBuilder index, LocalBuilder array)
    {
      Debug.Assert(index != null);

      // i++;
      il.Emit(OpCodes.Ldloc, index);
      il.Emit(OpCodes.Ldc_I4_1);
      il.Emit(OpCodes.Add);
      il.Emit(OpCodes.Stloc, index);

      Label condition = Labels.Pop();
      Label loopBegin = Labels.Pop();

      // while(i < a.Length)
      il.MarkLabel(condition);
      il.Emit(OpCodes.Ldloc, index);

      if (array == null)
           il_EmitLoadArg(il, 0);
      else il.Emit(OpCodes.Ldloc, array);

      il.Emit(OpCodes.Ldlen);
      il.Emit(OpCodes.Conv_I4);
      il.Emit(OpCodes.Blt, loopBegin);
    }

    #endregion
    #region Allocator

    static readonly Dictionary<int, Allocator<T>>
        Cache = new Dictionary<int, Allocator<T>>();

    public static Allocator<T> GetAllocator(int rank)
    {
      Debug.Assert(rank >= 2);
      Allocator<T> alloc;

      lock (Cache)
      if (!Cache.TryGetValue(rank, out alloc))
      {
        alloc = CompileAllocator(rank);
        Cache.Add(rank, alloc);
      }

      return alloc;
    }

    static Allocator<T> CompileAllocator(int rank)
    {
#if SILVERLIGHT

      var method = new DynamicMethod(
        "alloc" + rank, SystemArrayType, AllocArgs);

#else

      var method = new DynamicMethod(
        "alloc" + rank, SystemArrayType,
        AllocArgs, AllocType, true);

#endif

      ILGenerator il = method.GetILGenerator();

      var locals = new Stack<LocalBuilder>();
      var labels = new Stack<Label>();

      for (int i = rank - 1, j = 0; i > 0; i--, j++)
      {
        LocalBuilder array = il.DeclareLocal(
          TypeHelper<T>.GetArrayType(i+1));

        LocalBuilder index = il.DeclareLocal(IndexType);

        // a = new T[count[i]][]..;
        il.Emit(OpCodes.Ldarg_0);
        il_EmitLoadI4(il, j);
        il.Emit(OpCodes.Ldelem_I4);
        il.Emit(OpCodes.Newarr,
          TypeHelper<T>.GetArrayType(i));
        il.Emit(OpCodes.Stloc, array);

        // int i = 0;
        il.Emit(OpCodes.Ldc_I4_0);
        il.Emit(OpCodes.Stloc, index);

        Label lbCond = il.DefineLabel();
        Label lbBegin = il.DefineLabel();

        il.Emit(OpCodes.Br, lbCond);
        il.MarkLabel(lbBegin);

        locals.Push(index);
        locals.Push(array);
        labels.Push(lbBegin);
        labels.Push(lbCond);
      }

      LocalBuilder lastLocal = null;

      for (int i = 0; i < rank - 1; i++)
      {
        LocalBuilder array = locals.Pop();
        LocalBuilder index = locals.Pop();

        if (i == 0)
        {
          // arr[i] =
          il.Emit(OpCodes.Ldloc, array);
          il.Emit(OpCodes.Ldloc, index);

          // = new T[counts[<rank-1>]]
          il.Emit(OpCodes.Ldarg_0);
          il_EmitLoadI4(il, rank - 1);
          il.Emit(OpCodes.Ldelem_I4);
          il.Emit(OpCodes.Newarr,
            TypeHelper<T>.ValueType);
          il.Emit(OpCodes.Stelem_Ref);
        }
        else
        {
          Debug.Assert(lastLocal != null);

          // b[j] = a;
          il.Emit(OpCodes.Ldloc, array);
          il.Emit(OpCodes.Ldloc, index);
          il.Emit(OpCodes.Ldloc, lastLocal);
          il.Emit(OpCodes.Stelem_Ref);
        }

        // i++
        il.Emit(OpCodes.Ldloc, index);
        il.Emit(OpCodes.Ldc_I4_1);
        il.Emit(OpCodes.Add);
        il.Emit(OpCodes.Stloc, index);

        // while(i < a.Lengh)
        il.MarkLabel(labels.Pop());
        il.Emit(OpCodes.Ldloc, index);
        il.Emit(OpCodes.Ldloc, array);
        il.Emit(OpCodes.Ldlen);
        il.Emit(OpCodes.Conv_I4);
        il.Emit(OpCodes.Blt, labels.Pop());

        lastLocal = array;
      }

      Debug.Assert(lastLocal != null);

      il.Emit(OpCodes.Ldloc, lastLocal);
      il.Emit(OpCodes.Ret);

      return (Allocator<T>)
        method.CreateDelegate(AllocType);
    }

    #endregion
    #region Static Data

    // Types ================================================================

    static readonly Type IndexType = typeof(Int32);
    static readonly Type SystemArrayType = typeof(Array);
    static readonly Type Array2DType = typeof(T[][]);

    static readonly Type[][] ArgsTypes = new[]
    {
      new[]
      {
        TypeHelper<T>.ArrayType,
        TypeHelper<T>.ValueType,
        TypeHelper<T>.ValueType
      },
      new[]
      {
        Array2DType,
        TypeHelper<T>.ValueType,
        TypeHelper<T>.ValueType,
        TypeHelper<T>.ValueType,
        TypeHelper<T>.ValueType
      },
      new[]
      {
        SystemArrayType,
        TypeHelper<T>.ArrayType
      }
    };

    static readonly Type[] DelegateTypes = new[]
    {
      typeof(TabFunc1<T>),
      typeof(TabFunc2<T>),
      typeof(TabFuncN<T>)
    };

    static readonly Type AllocType = typeof(Allocator<T>);
    static readonly Type[] AllocArgs = new[] { typeof(Int32[]) };

    #endregion
  }
}