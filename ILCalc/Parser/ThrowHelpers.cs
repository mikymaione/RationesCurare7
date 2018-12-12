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
  sealed partial class Parser<T>
  {
    Exception IncorrectConstr(Item prev, Item next, int i)
    {
      int len = i - this.prePos;
      Debug.Assert(len >= 0);

      var msg =
        new StringBuilder(Resource.errIncorrectConstr)
        .Append(" (").Append(prev).Append(")(")
        .Append(next).Append("): \"")
        .Append(this.expr, this.prePos, len)
        .Append("\".").ToString();

      return new SyntaxException(
        msg, this.expr, this.prePos, len);
    }

    Exception BraceDisbalance(int pos, bool mode)
    {
      string msg = mode ?
        Resource.errDisbalanceOpen :
        Resource.errDisbalanceClose;

      return new SyntaxException(msg, this.expr, pos, 1);
    }

    Exception IncorrectIden(int i)
    {
      for (i++; i < this.expr.Length; i++)
      {
        char c = this.expr[i];
        if (!char.IsLetterOrDigit(c) && c != '_') break;
      }

      return IncorrectConstr(
        Item.Identifier, Item.Identifier, i);
    }

    Exception NoOpenBrace(int pos, int len)
    {
      var msg =
        new StringBuilder(Resource.errFunctionNoBrace)
        .Append(" \"")
        .Append(this.expr, pos, len)
        .Append("\".")
        .ToString();

      return new SyntaxException(msg, this.expr, pos, len);
    }

    Exception InvalidSeparator()
    {
      return new SyntaxException(
        Resource.errInvalidSeparator, this.expr, this.curPos, 1);
    }

    Exception UnresolvedIdentifier(int shift)
    {
      int end = this.curPos;
      for (end += shift; end < this.expr.Length; end++)
      {
        char c = this.expr[end];
        if (!Char.IsLetterOrDigit(c) && c != '_') break;
      }

      int len = end - this.curPos;

      var msg =
        new StringBuilder(
          Resource.errUnresolvedIdentifier)
        .Append(" \"")
        .Append(this.expr, this.curPos, len)
        .Append("\".")
        .ToString();

      return new SyntaxException(
        msg, this.expr, this.curPos, len);
    }

    Exception UnresolvedSymbol(int i)
    {
      var buf = new StringBuilder(Resource.errUnresolvedSymbol);

      buf.Append(" '");
      buf.Append(this.expr[i]);
      buf.Append("'.");

      if (LiteralParser.IsUnknown(Literal))
      {
        buf.Append(string.Format(
          Resource.errParseLiteralHint, typeof(T)));
      }

      return new SyntaxException(
        buf.ToString(), this.expr, i, 1);
    }

    Exception WrongArgsCount(
      int pos, int len, int args, FunctionGroup<T> group)
    {
      var buf =
        new StringBuilder(Resource.sFunction)
        .Append(" \"")
        .Append(this.expr, pos, len)
        .Append("\" ")
        .AppendFormat(Resource.errWrongOverload, args);

      // NOTE: improve this?
      // NOTE: may be empty FunctionGroup! Show actual message
      // NOTE: FunctionGroup => IEnumerable<FunctionInfo>
      if (group != null)
      {
        buf
          .Append(' ')
          .AppendFormat(
            Resource.errExistOverload,
            group.MakeMethodsArgsList());
      }

      return new SyntaxException(
        buf.ToString(), this.expr, pos, len);
    }

    Exception AmbiguousMatchException(int pos, List<Capture> matches)
    {
      Debug.Assert(matches != null);
      Debug.Assert(matches.Count > 0);

      var names = new List<string>(matches.Count);

      foreach (Capture match in matches)
      {
        IdenType idenType = IdenType.Argument;
        foreach (var list in this.literals)
        {
          if (idenType == match.Type)
          {
            int i = 0, id = match.Index;
            foreach (string name in list)
            {
              if (i++ == id) { names.Add(name); break; }
            }
          }

          idenType++;
        }
      }

      Debug.Assert(matches.Count == names.Count);

      var buf = new StringBuilder(
        Resource.errAmbiguousMatch);

      for (int i = 0; i < matches.Count; i++)
      {
        string type = string.Empty;

        switch (matches[i].Type)
        {
          case IdenType.Argument: type = Resource.sArgument; break;
          case IdenType.Constant: type = Resource.sConstant; break;
          case IdenType.Function: type = Resource.sFunction; break;
        }

        buf
          .Append(' ')
          .Append(type.ToLowerInvariant())
          .Append(" \"")
          .Append(names[i])
          .Append('\"');

        if (i + 1 == matches.Count)
        {
          buf.Append(' ').Append(Resource.sAnd);
        }
        else
        {
          buf.Append(i == matches.Count ? '.' : ',');
        }
      }

      int len = names[0].Length;
      return new SyntaxException(
        buf.ToString(), this.expr, pos, len);
    }
  }
}