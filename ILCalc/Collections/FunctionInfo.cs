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
  using State = DebuggerBrowsableState;
  using Br = DebuggerBrowsableAttribute;

  /// <summary>
  /// Represents the imported function that has
  /// ability to be used in some expression.</summary>
  /// <typeparam name="T">Function's parameters
  /// and return value type.</typeparam>
  /// <remarks>
  /// Instance of this class is an immutable.<br/>
  /// Class has no public constructors.</remarks>
  /// <threadsafety instance="true"/>
  [DebuggerDisplay("{ArgsCount} args", Name = "{MethodName}")]
  [Serializable]
  public abstract class FunctionInfo<T>
  {
    #region Fields

    [Br(State.Never)] readonly int fixCount;
    [Br(State.Never)] readonly bool hasParams;

    #endregion
    #region Constructor

    internal FunctionInfo(int argsCount, bool hasParams)
    {
      Debug.Assert(argsCount >= 0);

      this.hasParams = hasParams;
      this.fixCount = argsCount;
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the function arguments count.</summary>
    public int ArgsCount { get { return this.fixCount; } }

    /// <summary>
    /// Gets a value indicating whether
    /// function has an parameters array.</summary>
    public bool HasParamArray { get { return this.hasParams; } }

    /// <summary>
    /// Gets the method reflection this
    /// <see cref="FunctionInfo{T}"/> represents.
    /// </summary>
    public abstract MethodInfo Method { get; }

    /// <summary>
    /// Gets the method target for instance methods.
    /// For static methods this property will return null.
    /// </summary>
    public abstract object Target { get; }

    /// <summary>Gets the method name.</summary>
    public string MethodName
    {
      get { return Method == null? string.Empty: Method.Name; }
    }

    /// <summary>
    /// Gets the method full name (including declaring type name).
    /// </summary>
    public string FullName
    {
      get
      {
        if (Method == null) return string.Empty;

        var buf = new StringBuilder();

        buf.Append(Method.DeclaringType.FullName);
        buf.Append('.');
        buf.Append(Method.Name);

        return buf.ToString();
      }
    }

    [Br(State.Never)]
    internal string ArgsString
    {
      get
      {
        return HasParamArray ?
          this.fixCount.ToString() + '+' :
          this.fixCount.ToString();
      }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Determine the ability of function to take specified
    /// <paramref name="count"/> of arguments.</summary>
    /// <param name="count">Arguments count.</param>
    /// <returns><b>true</b> if function can takes
    /// <paramref name="count"/> arguments;
    /// otherwise <b>false</b>.</returns>
    public bool CanTake(int count)
    {
      return count >= 0 &&
        HasParamArray ?
        count >= this.fixCount :
        count == this.fixCount;
    }

    /// <summary>
    /// Invokes this <see cref="FunctionInfo{T}"/>
    /// via reflection.</summary>
    /// <param name="arguments">Function arguments.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="arguments"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="arguments"/> doesn't contains
    /// needed arguments count.</exception>
    /// <returns>Returned value.</returns>
    public T Evaluate(params T[] arguments)
    {
      if (arguments == null)
        throw new ArgumentNullException("arguments");

      if (!CanTake(arguments.Length))
      {
        throw new ArgumentException(string.Format(
          Resource.errWrongArgsCount,
          arguments.Length,
          ArgsString));
      }

      int argsCount = arguments.Length;
      return Invoke(arguments, argsCount - 1, argsCount);
    }

    #endregion
    #region Internals

    internal abstract Delegate MakeDelegate();

    internal abstract T Invoke(
      T[] stack, int pos, int argsCount);

    #endregion
  }

  [Serializable]
  sealed class ReflectionMethodInfo<T> : FunctionInfo<T>
  {
    #region Fields

    [Br(State.Never)] readonly MethodInfo method;
    [Br(State.Never)] readonly object target;

    #endregion
    #region Constructor

    public ReflectionMethodInfo(
      MethodInfo method, object target, int argsCount, bool hasParams)
      : base(argsCount, hasParams)
    {
      Debug.Assert(method != null);
      Debug.Assert(method.IsStatic == (target == null));
      Debug.Assert(argsCount >= 0);

      this.method = method;
      this.target = target;
    }

    #endregion
    #region Properties

    [Br(State.Never)]
    public override MethodInfo Method
    {
      get { return this.method; }
    }

    [Br(State.Never)]
    public override object Target
    {
      get { return this.target; }
    }

    #endregion
    #region Internals

    internal override T Invoke(T[] stack, int pos, int argsCount)
    {
      Debug.Assert(stack != null);
      Debug.Assert(stack.Length > pos);
      Debug.Assert(stack.Length >= argsCount);

      object[] fixArgs;

      if (HasParamArray)
      {
        int varCount = argsCount - ArgsCount;
        var varArgs = new T[varCount];

        // TODO: perform loop, based on varArgs.Length

        // fill params array:
        for (int i = varCount - 1; i >= 0; i--)
        {
          varArgs[i] = stack[pos--];
        }

        fixArgs = new object[ArgsCount+1];
        fixArgs[ArgsCount] = varArgs;
      }
      else
      {
        fixArgs = new object[ArgsCount];
      }

      // fill arguments array:
      for (int i = ArgsCount-1; i >= 0; i--)
      {
        fixArgs[i] = stack[pos--];
      }

      // invoke via reflection
      try
      {
        return (T)
          this.method.Invoke(this.target, fixArgs);
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    internal override Delegate MakeDelegate()
    {

#if CF2

      return null;

#else

      if (HasParamArray || ArgsCount > 2) return null;

      Type delType;
      switch (ArgsCount)
      {
        case 0: delType = TypeHelper<T>.Func0; break;
        case 1: delType = TypeHelper<T>.Func1; break;
        case 2: delType = TypeHelper<T>.Func2; break;
        default: return null;
      }

      Validator.CheckVisible(Method);

      return Delegate.CreateDelegate(delType, Target, Method);

#endif

    }

    #endregion
  }

  [Serializable]
  sealed class KnownDelegateInfo<T> : FunctionInfo<T>
  {
    #region Fields

    [Br(State.Never)] readonly Delegate deleg;
    [Br(State.Never)] readonly MethodInfo method;

    #endregion
    #region Constructor

    public KnownDelegateInfo(
      Delegate deleg, MethodInfo method, int argsCount)
      : base(argsCount, false)
    {
      Debug.Assert(deleg != null);
      Debug.Assert(argsCount >= 0 && argsCount <= 2);

      this.deleg = deleg;
      this.method = method;
    }

    #endregion
    #region Properties

#if CF2

    [Br(State.Never)]
    public override MethodInfo Method
    {
      get { return null; }
    }

    [Br(State.Never)]
    public override object Target
    {
      get { return null; }
    }

#else

    [Br(State.Never)]
    public override MethodInfo Method
    {
      get { return this.method ?? this.deleg.Method; }
    }

    [Br(State.Never)]
    public override object Target
    {
      get
      {
        return this.method == null ?
          this.deleg.Target : this.deleg;
      }
    }

#endif

    #endregion
    #region Internals

    internal override T Invoke(T[] stack, int pos, int argsCount)
    {
      Debug.Assert(stack != null);
      Debug.Assert(stack.Length > pos);
      Debug.Assert(stack.Length >= argsCount);

      switch (ArgsCount)
      {
        case 0: return ((EvalFunc0<T>) this.deleg)();
        case 1: return ((EvalFunc1<T>) this.deleg)(stack[pos]);
        case 2: return ((EvalFunc2<T>) this.deleg)(stack[pos-1], stack[pos]);
        default: return default(T);
      }
    }

    internal override Delegate MakeDelegate()
    {
      return this.deleg;
    }

    #endregion
  }
}