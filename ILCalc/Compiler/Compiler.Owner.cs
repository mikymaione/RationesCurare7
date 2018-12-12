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

namespace ILCalc
{
  static class OwnerSupport
  {
    #region Fields

    public static readonly Type OwnerNType = typeof(Closure);
    public static readonly Type Owner2Type = typeof(Closure<,>);
    public static readonly Type Owner3Type = typeof(Closure<,,>);

    public static readonly BindingFlags FieldFlags =
      BindingFlags.Instance | BindingFlags.NonPublic;

    public static readonly FieldInfo OwnerNArray =
      OwnerNType.GetField("closure", FieldFlags);

    #endregion
    #region Closures

    [Serializable]
    public sealed class Closure
    {
      object[] closure;

      public Closure(object[] closure)
      {
        Debug.Assert(closure != null);
        this.closure = closure;
      }
    }

    [Serializable]
    public sealed class Closure<T1, T2>
    {
      readonly T1 obj0;
      readonly T2 obj1;

      public Closure(T1 o0, T2 o1)
      {
        this.obj0 = o0;
        this.obj1 = o1;
      }
    }

    [Serializable]
    public sealed class Closure<T1, T2, T3>
    {
      readonly T1 obj0;
      readonly T2 obj1;
      readonly T3 obj2;

      public Closure(T1 o0, T2 o1, T3 o2)
      {
        this.obj0 = o0;
        this.obj1 = o1;
        this.obj2 = o2;
      }
    }

    #endregion
  }
}