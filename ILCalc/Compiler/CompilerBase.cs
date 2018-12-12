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
using System.Reflection;
using System.Reflection.Emit;
using ILCalc.Custom;

namespace ILCalc
{
  // TODO: Reuse BufferWriter if Compiler + Optimizer

  abstract class CompilerBase<T> : BufferOutput<T>,
                              IExpressionOutput<T>
  {
    #region Fields

    readonly List<FuncCall> calls;
    readonly bool emitChecks;
    Type ownerType;

    protected static readonly ICompiler<T> Generic =
      CompilerSupport.Resolve<T>();

    static readonly bool SupportLiterals =
      CompilerSupport.SupportLiterals(Generic);

    readonly List<object> closure = new List<object>();

    #endregion
    #region Constructor

    protected CompilerBase(bool checks)
    {
      this.emitChecks = checks;
      this.calls = new List<FuncCall>();
    }

    #endregion
    #region Properties

    protected Type OwnerType
    {
      get { return this.ownerType; }
    }

    private int TargetsCount
    {
      get { return this.closure.Count; }
    }

    #endregion
    #region CodeGen

    protected void CodeGen(ILGenerator il)
    {
      int n = 0, d = 0, c = 0;
      Stack<FuncCall> stack = null;
      FuncCall call = null;

      for (int i = 0; ; i++)
      {
        Code op = this.code[i];

        if (CodeHelper.IsOp(op))
        {
          if (this.emitChecks)
               Generic.CheckedOp(il, (int) op);
          else Generic.Operation(il, (int) op);
        }
        else if (op == Code.Number) // ================================
        {
          if (SupportLiterals)
          {
            Generic.LoadConst(il, this.numbers[n++]);
          }
          else
          {
            EmitLoadObj(il, this.data[d++]);
          }
        }
        else if (op == Code.Argument) // ==============================
        {
          EmitLoadArg(il, this.data[d++]);
        }
        else if (op == Code.Separator) // =============================
        {
          // separator needed only for params calls:
          Debug.Assert(call != null);
          if (call.VarCount >= 0)
          {
            EmitSeparator(il, call);
          }
        }
        else if (op == Code.Function) // ==============================
        {
          EmitFunctionCall(il, call);

          // parent call info:
          if (stack == null || stack.Count == 0) call = null;
          else call = stack.Pop();
        }
        else if (op == Code.BeginCall) // =============================
        {
          if (call != null)
          {
            // allocate if needed:
            if (stack == null) stack = new Stack<FuncCall>();
            stack.Push(call);
          }

          call = this.calls[c++];

          // need for local to store params array:
          if (call.VarCount > 0)
          {
            call.Local = il.DeclareLocal(
              TypeHelper<T>.ArrayType);
          }

          if (call.TargetID >= 0)
          {
            EmitLoadObj(il, call.TargetID);
          }

          if (call.Current == 0 && call.VarCount > 0)
          {
            EmitParamArr(il, call);
          }
        }
        else // =======================================================
        {
          break;
        }
      }
    }

    #endregion
    #region Emitters

    protected abstract
      void EmitLoadArg(ILGenerator il, int index);

    void EmitLoadObj(ILGenerator il, int index)
    {
      Debug.Assert(il != null);
      Debug.Assert(index < TargetsCount);

      // loads this
      il.Emit(OpCodes.Ldarg_0);

      if (TargetsCount == 1)
      {
        // if expr constains only one target,
        // this target will be this
      }
      else if (TargetsCount <= 3)
      {
        Debug.Assert(this.ownerType != null);

        FieldInfo field = OwnerType.GetField(
          "obj" + index, OwnerSupport.FieldFlags);

        Debug.Assert(field != null);

        // 2 hours of debugging to find the difference :(
        if (field.FieldType.IsValueType)
             il.Emit(OpCodes.Ldflda, field);
        else il.Emit(OpCodes.Ldfld,  field);
      }
      else
      {
        il.Emit(OpCodes.Ldfld, OwnerSupport.OwnerNArray);
        il_EmitLoadI4(il, index);
        il.Emit(OpCodes.Ldelem_Ref);

        Type targetType = this.closure[index].GetType();

        if (targetType.IsValueType)
             il.Emit(OpCodes.Unbox_Any, targetType);
        else il.Emit(OpCodes.Castclass, targetType);
      }
    }

    static void EmitSeparator(ILGenerator il, FuncCall call)
    {
      Debug.Assert(il != null);
      Debug.Assert(call != null);

      if (call.NextIsLastFixed())
      {
        EmitParamArr(il, call);
      }
      else if (call.Current > 0)
      {
        il_EmitSaveElem(il);
        il.Emit(OpCodes.Ldloc, call.Local);
        il_EmitLoadI4(il, call.Current);
        il_EmitLoadAdress(il);
      }
    }

    static void EmitFunctionCall(ILGenerator il, FuncCall call)
    {
      Debug.Assert(il   != null);
      Debug.Assert(call != null);

      if (call.VarCount >= 0)
      {
        if (call.VarCount > 0)
        {
          il_EmitSaveElem(il);
          il.Emit(OpCodes.Ldloc, call.Local);
        }
        else
        {
          il_EmitLoadI4(il, 0);
          il.Emit(
            OpCodes.Newarr,
            TypeHelper<T>.ValueType);
        }
      }

      if (call.TargetID < 0)
      {
        il.Emit(OpCodes.Call, call.Method);
      }
      else
      {
        il.Emit(OpCodes.Callvirt, call.Method);
      }
    }

    static void EmitParamArr(ILGenerator il, FuncCall call)
    {
      il_EmitLoadI4(il, call.VarCount);
      il.Emit(OpCodes.Newarr, TypeHelper<T>.ValueType);
      il.Emit(OpCodes.Stloc, call.Local);
      il.Emit(OpCodes.Ldloc, call.Local);
      il_EmitLoadI4(il, 0);
      il_EmitLoadAdress(il);
    }

    // ReSharper disable InconsistentNaming

    protected void il_EmitLoadArg(ILGenerator il, int index)
    {
      Debug.Assert(index >= 0);

      if (TargetsCount > 0) index++;

      if (index <= 3)
           il.Emit(OpArgsLoad[index]);
      else il.Emit(OpCodes.Ldarg_S, (byte) index);
    }

    public static void il_EmitLoadI4(ILGenerator il, int value)
    {
      CompilerSupport.il_EmitLoadI4(il, value);
    }

    protected static void il_EmitLoadElem(ILGenerator il)
    {
      Type type = TypeHelper<T>.ValueType;

      if (type.IsPrimitive)
      {
        Generic.LoadElem(il);
      }
      else if (type.IsValueType)
      {
        il.Emit(OpCodes.Ldelema, type);
        il.Emit(OpCodes.Ldobj, type);
      }
      else
      {
        il.Emit(OpCodes.Ldelem_Ref);
      }
    }

    protected static void il_EmitLoadAdress(ILGenerator il)
    {
      Type type = TypeHelper<T>.ValueType;

      if (!type.IsPrimitive && type.IsValueType)
      {
        il.Emit(OpCodes.Ldelema, type);
      }
    }

    protected static void il_EmitSaveElem(ILGenerator il)
    {
      Type type = TypeHelper<T>.ValueType;

      if (type.IsPrimitive)
      {
        Generic.SaveElem(il);
      }
      else if(type.IsValueType)
      {
        il.Emit(OpCodes.Stobj, TypeHelper<T>.ValueType);
      }
    }

    // ReSharper restore InconsistentNaming

    #endregion
    #region FuncCall

    sealed class FuncCall
    {
      #region Fields

      private int current;

      public int VarCount { get; private set; }
      public int TargetID { get; private set; }
      public MethodInfo Method { get; private set; }
      public LocalBuilder Local { get; set; }

      public int Current { get { return this.current; } }

      #endregion
      #region Constructor

      public FuncCall(
        FunctionInfo<T> func, int argsCount, int targetId)
      {
        Method = func.Method;
        TargetID = targetId;

        if (func.HasParamArray)
        {
          this.current = -func.ArgsCount;
          VarCount = argsCount - func.ArgsCount;
        }
        else
        {
          this.current = 0;
          VarCount = -1;
        }
      }

      #endregion
      #region Fields

      public bool NextIsLastFixed()
      {
        return (++this.current == 0);
      }

      #endregion
    }

    #endregion
    #region IExpressionOutput

    public new void PutConstant(T value)
    {
      if (SupportLiterals)
      {
        base.PutConstant(value);
      }
      else
      {
        int id = PutClosure(value);
        this.code.Add(Code.Number);
        this.data.Add(id);
      }
    }

    public new void PutCall(
      FunctionInfo<T> func, int argz)
    {
      Validator.CheckVisible(func.Method);

      int i = this.calls.Count;
      while (this.calls[--i] != null)
      {
        Debug.Assert(i >= 0);
      }

      int targetId = -1;
      if (func.Target != null)
      {
        targetId = PutClosure(func.Target);
      }

      this.calls[i] = new FuncCall(func, argz, targetId);
      this.code.Add(Code.Function);

      // DO NOT call base impl!
    }

    public new void PutBeginCall()
    {
      this.calls.Add(null);
      base.PutBeginCall();
    }

    #endregion
    #region Helpers

    int PutClosure(object target)
    {
      int targetId = -1;
      for (int j = 0; j < this.closure.Count; j++)
      {
        if (ReferenceEquals(this.closure[j], target))
        {
          targetId = j;
        }
      }

      if (targetId < 0)
      {
        targetId = this.closure.Count;
        this.closure.Add(target);
      }

      return targetId;
    }

    object CreateOwner()
    {
      Debug.Assert(TargetsCount > 0);

      var c = this.closure.ToArray();
      switch (c.Length)
      {
        case 1:
          this.ownerType = c[0].GetType();
          return c[0];

        case 2:
          this.ownerType = OwnerSupport
            .Owner2Type.MakeGenericType(
              c[0].GetType(),
              c[1].GetType());

         return Activator.CreateInstance(this.ownerType, c);

        case 3:
          this.ownerType = OwnerSupport
            .Owner3Type.MakeGenericType(
              c[0].GetType(),
              c[1].GetType(),
              c[2].GetType());

          return Activator.CreateInstance(this.ownerType, c);

        default:
          this.ownerType = OwnerSupport.OwnerNType;
          return new OwnerSupport.Closure(c);
      }
    }

    protected object OwnerFixup(ref Type[] args)
    {
      if (TargetsCount > 0)
      {
        object owner = CreateOwner();

        var ownerArgs = new Type[args.Length + 1];
        Array.Copy(args, 0, ownerArgs, 1, args.Length);

        ownerArgs[0] = OwnerType;
        args = ownerArgs;

        return owner;
      }

      this.ownerType = OwnerSupport.OwnerNType;
      return null;
    }

    protected static Delegate GetDelegate(
      DynamicMethod method, Type delegateType, object owner)
    {
      return owner == null ?
        method.CreateDelegate(delegateType) :
        method.CreateDelegate(delegateType, owner);
    }

    #endregion
    #region StaticData

    static readonly OpCode[] OpArgsLoad =
    {
      OpCodes.Ldarg_0,
      OpCodes.Ldarg_1,
      OpCodes.Ldarg_2,
      OpCodes.Ldarg_3
    };

    #endregion
  }
}