/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ILCalc
{
  sealed partial class FuncCall<T> : ISerializable
  {
    static readonly Type
      FunctionType = typeof(FunctionInfo<T>);

    FuncCall(SerializationInfo info, StreamingContext context)
    {
      int fixCount = info.GetInt32("fix");
      int varCount = info.GetInt32("var");

      this.fixArgs = new object[fixCount];

      if (varCount >= 0)
      {
        this.varArgs = new T[varCount];
        this.fixArgs[--fixCount] = this.varArgs;
      }

      this.func = (FunctionInfo<T>)
        info.GetValue("func", FunctionType);

      this.argsCount = fixCount + varCount;
      this.lastIndex = fixCount - 1;
      this.syncRoot = new object();
    }

    [SecurityPermission(SecurityAction.LinkDemand,
      Flags = SecurityPermissionFlag.SerializationFormatter)]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("fix", fixArgs.Length);
      info.AddValue("var", varArgs == null ? -1 : varArgs.Length);
      info.AddValue("func", func, FunctionType);
    }
  }
}