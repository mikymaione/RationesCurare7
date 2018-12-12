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
using System.Text;

namespace ILCalc
{
  static class Validator
  {
    public static void CheckName(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentException(
          Resource.errIdentifierEmpty);
      }

      char first = name[0];
      if (!char.IsLetter(first) && first != '_')
      {
        throw InvalidFirstSymbol(name, first);
      }

      for (int i = 1; i < name.Length; i++)
      {
        char c = name[i];
        if (!char.IsLetterOrDigit(c) && c != '_')
        {
          throw new ArgumentException(
            string.Format(
              Resource.errIdentifierSymbol, c, name));
        }
      }
    }

    [DebuggerHidden]
    static Exception InvalidFirstSymbol(string name, char first)
    {
      var buf = new StringBuilder();
      buf.AppendFormat(
        Resource.errIdentifierStartsWith, name);

      if (first == '<')
      {
        buf.Append(' ')
           .Append(Resource.errIdentifierFromLambda);
      }

      return new ArgumentException(buf.ToString());
    }

    [Conditional("SILVERLIGHT")]
    public static void CheckVisible(Type type)
    {
      Debug.Assert(type != null);

      if (!type.IsVisible)
      {
        throw new ArgumentException(string.Format(
          Resource.errTypeNonPublic, type.FullName));
      }
    }

    [Conditional("SILVERLIGHT")]
    public static void CheckVisible(
      System.Reflection.MethodInfo method)
    {
      Debug.Assert(method != null);

      if (!method.IsPublic)
      {
        throw new ArgumentException(string.Format(
          Resource.errMethodNonPublic, method));
      }

      if (method.DeclaringType != null)
      {
        CheckVisible(method.DeclaringType);
      }
    }
  }

  interface IListEnumerable
  {
    List<string>.Enumerator GetEnumerator();
  }
}