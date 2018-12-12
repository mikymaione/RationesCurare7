/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ILCalc
{
  // TODO: unit tests!
  static class FunctionFactory<T>
  {
    #region Fields

    // standard runtime method type:
    static readonly Type RuntimeMethodType =
      typeof(FunctionFactory<T>).GetMethod("TryResolve").GetType();

    #endregion
    #region Methods

    public static FunctionInfo<T> FromReflection(
      MethodInfo method, object target, bool throwOnFailure)
    {
      // DynamicMethod shouldn't pass here:
      if (method.GetType() != RuntimeMethodType)
      {
        if (!throwOnFailure) return null;
        throw MethodImportFailure(
          method, Resource.errMethodNotRuntimeMethod);
      }

      // validate static methods:
      if (target == null && !method.IsStatic)
      {
        if (!throwOnFailure) return null;
        throw MethodImportFailure(
          method, Resource.errMethodNotStatic);
      }

      // validate target type:
      if (target != null)
      {
        if (method.IsStatic)
        {
          if (!throwOnFailure) return null;
          throw MethodImportFailure(
            method, Resource.errMethodNotInstance);
        }

        Debug.Assert(!method.IsStatic);

        Type thisType = method.DeclaringType;
        Type targetType = target.GetType();

        if (!thisType.IsAssignableFrom(targetType))
        {
          if (!throwOnFailure) return null;

          throw InvalidMethodTarget(
            thisType, targetType);
        }
      }

      bool hasParams;
      int argsCount = IsApplicable(
        method, out hasParams, throwOnFailure);
      
      if (argsCount >= 0)
      {
        return new ReflectionMethodInfo<T>(
          method, target, argsCount, hasParams);
      }

      return null;
    }

    public static FunctionInfo<T> FromDelegate(
      Delegate target, bool throwOnFailure)
    {
      // common callers argument check:
      if (target == null)
        throw new ArgumentNullException("target");

      // check the invocation count:
      if (target.GetInvocationList().Length != 1)
      {
        if (!throwOnFailure) return null;
        throw new ArgumentException(
          Resource.errDelegateInvCount);
      }

#if CF2

      MethodInfo method = target.GetType().GetMethod("Invoke");
      object methTarget = target;

#else

      MethodInfo method = target.Method;
      object methTarget = target.Target;

      // detect DynamicMethod here:
      if (method.GetType() != RuntimeMethodType)
      {
        // cool way to call delegate here =))
        // get the Delegate invoker:
        method = target.GetType().GetMethod("Invoke");
        methTarget = target;
      }

#endif

      bool hasParams;
      int argsCount = IsApplicable(
        method, out hasParams, throwOnFailure);
      if (argsCount == -1) return null;

      return new ReflectionMethodInfo<T>(
        method, methTarget, argsCount, hasParams);
    }

    public static FunctionInfo<T> FromKnownDelegate(
      Delegate target, int argsCount, bool throwOnFailure)
    {
      // common callers argument check:
      if (target == null)
        throw new ArgumentNullException("target");

      // check the invocation count:
      if (target.GetInvocationList().Length != 1)
      {
        if (throwOnFailure)
        {
          throw new ArgumentException(
            Resource.errDelegateInvCount);
        }

        return null;
      }

#if !CF

      // detect DynamicMethod here:
      if (target.Method.GetType() != RuntimeMethodType)
      {
        var invoker = target.GetType().GetMethod("Invoke");

        return new KnownDelegateInfo<T>(
          target, invoker, argsCount);
      }

#endif

      return new KnownDelegateInfo<T>(
        target, null, argsCount);
    }

    public static MethodInfo TryResolve(
      Type type, string name, int argsCount)
    {
      // common arguments checks:
      if (type == null) throw new ArgumentNullException("type");
      if (name == null) throw new ArgumentNullException("name");

      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      MethodInfo method = (argsCount < 0) ?
        type.GetMethod(name, Flags) :
        type.GetMethod(name, Flags,
          null, MakeArgs(argsCount), null);

      if (method == null)
      {
        throw new ArgumentException(
          string.Format(
            Resource.errMethodNotFounded, name));
      }

      return method;
    }

    static int IsApplicable(MethodInfo method,
      out bool hasParams, bool throwOnFailure)
    {
      hasParams = false;

      // validate return type:
      if (method.ReturnType != TypeHelper<T>.ValueType)
      {
        if (!throwOnFailure) return -1;
        throw InvalidMethodReturn(method);
      }

      // and method parameters types:
      var parameters = method.GetParameters();
      foreach (ParameterInfo p in parameters)
      {
        if (p.ParameterType != TypeHelper<T>.ValueType)
        {
          // maybe this is params method?
          if (p.Position == parameters.Length - 1 &&
#if !CF
              !p.IsOptional &&
              !p.IsOut &&
#endif
              p.ParameterType == TypeHelper<T>.ArrayType)
          {
            hasParams = true;
            return parameters.Length - 1;
          }

          if (!throwOnFailure) return -1;
          throw InvalidParamType(method, p);
        }

#if !CF
        if (p.IsOut || p.IsOptional)
        {
          if (!throwOnFailure) return -1;
          throw InvalidParamType(method, p);
        }
#endif
      }

      return parameters.Length;
    }

    #endregion
    #region Helpers

    static Type[] MakeArgs(int count)
    {
      var types = new Type[count];
      for (int i = 0; i < types.Length; i++)
      {
        types[i] = TypeHelper<T>.ValueType;
      }

      return types;
    }

    static Exception InvalidMethodReturn(MethodInfo method)
    {
      Debug.Assert(method != null);

      return MethodImportFailure(
        method,
        Resource.errMethodBadReturn,
        method.ReturnType.FullName,
        TypeHelper<T>.ValueType.Name);
    }

    static Exception InvalidParamType(
      MethodInfo method, ParameterInfo param)
    {
      Debug.Assert(method != null);
      Debug.Assert(param != null);

      return MethodImportFailure(
        method,
        Resource.errMethodBadParam,
        param.Position,
        param.ParameterType.Name,
        TypeHelper<T>.ValueType.Name);
    }

    static Exception InvalidMethodTarget(
      Type thisType, Type targetType)
    {
      return new ArgumentException(
        string.Format(
          Resource.errWrongTargetType,
          targetType.FullName,
          thisType.FullName));
    }

    static Exception MethodImportFailure(
      MethodInfo method,
      string format,
      params object[] arguments)
    {
      Debug.Assert(format != null);
      Debug.Assert(arguments != null);

      var msg = new StringBuilder()
        .Append(Resource.errMethodImportFailed)
        .Append(" \"")
        .Append(method)
        .Append("\" ")
        .AppendFormat(format, arguments)
        .ToString();

      return new ArgumentException(msg);
    }

    #endregion
  }
}