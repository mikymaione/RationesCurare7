/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Threading;

namespace ILCalc
{
  [Serializable]
  sealed partial class FuncCall<T>
  {
    #region Fields

    readonly FunctionInfo<T> func;
    readonly int lastIndex;

    readonly object[] fixArgs;
    readonly T[] varArgs;

    readonly object syncRoot;
    readonly int argsCount;

    #endregion
    #region Constructor

    public FuncCall(FunctionInfo<T> f, int argsCount)
    {
      Debug.Assert(f != null);
      Debug.Assert(argsCount >= 0);
      Debug.Assert(
        ( f.HasParamArray && f.ArgsCount <= argsCount) ||
        (!f.HasParamArray && f.ArgsCount == argsCount));

      int fixCount = f.ArgsCount;

      if (f.HasParamArray)
      {
        this.varArgs = new T[argsCount - fixCount];
        this.fixArgs = new object[fixCount + 1];
        this.fixArgs[fixCount] = this.varArgs;
      }
      else this.fixArgs = new object[fixCount];

      this.func = f;
      this.lastIndex = fixCount - 1;
      this.argsCount = argsCount;
      this.syncRoot = new object();
    }

    #endregion
    #region Methods

    public void Invoke(T[] stack, ref int pos)
    {
      Debug.Assert(stack != null);
      Debug.Assert(stack.Length > pos);

      if (Monitor.TryEnter(this.syncRoot))
      {
        Debug.Assert(func is ReflectionMethodInfo<T>);

        try
        {
          // fill parameters array:
          if (this.varArgs != null)
          {
            for (int i = this.varArgs.Length - 1; i >= 0; i--)
            {
              this.varArgs[i] = stack[pos--];
            }
          }

          // fill arguments:
          object[] fixTemp = this.fixArgs;
          for (int i = this.lastIndex; i >= 0; i--)
          {
            fixTemp[i] = stack[pos--];
          }

          // invoke via reflection:
          Debug.Assert(fixTemp != null);

          stack[++pos] = (T) this.func
            .Method.Invoke(this.func.Target, fixTemp);
        }
        finally
        {
          Monitor.Exit(this.syncRoot);
        }
      }
      else //TODO: test!
      {
        T result = this.func.Invoke(stack, pos, this.argsCount);

        pos -= this.argsCount - 1;
        stack[pos] = result; // TODO: is all right here?
      }
    }

    #endregion
  }
}