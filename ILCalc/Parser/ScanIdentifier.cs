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
using System.Globalization;

namespace ILCalc
{
  sealed partial class Parser<T>
  {
    Item ScanIdenifier(ref int i)
    {
      int len = 0;

      List<Capture> matches =
        (Context.Culture == null) ?
        GetMatchesOrdinal(ref len) :
        GetMatchesCulture(ref len) ;

      if (len == 0)
        throw UnresolvedIdentifier(1);

      if (this.curPos + len < this.expr.Length)
      {
        // if there is a letter or _ after identifier:
        char c = this.expr[this.curPos + len];
        if (Char.IsLetterOrDigit(c) || c == '_')
        {
          throw UnresolvedIdentifier(len + 1);
        }
      }

      i += len - 1; // skip identifier

      return (matches.Count == 1) ?
        SimpleMatch(matches[0], ref i, len) :
        AmbiguousMatch(matches, ref i, len);
    }

    #region Matches

    Item SimpleMatch(Capture match, ref int i, int len)
    {
      if (match.Type == IdenType.Argument)
      {
        Debug.Assert(Context.Arguments.Count > match.Index);

        Output.PutArgument(match.Index);
        return Item.Identifier;
      }

      if (match.Type == IdenType.Constant)
      {
        Debug.Assert(Context.Constants.Count > match.Index);

        Output.PutConstant(Context.Constants[match.Index]);
        return Item.Identifier;
      }

      if (match.Type == IdenType.Function)
      {
        int funcPos = this.curPos;
        if (!SkipBrace(ref i))
        {
          throw NoOpenBrace(funcPos, len);
        }

        FunctionGroup<T> group = GetGroup(match);

        Output.PutBeginCall();
        int argsCount = ParseNested(ref i, true);

        FunctionInfo<T> func = group.GetOverload(argsCount);
        if (func == null)
        {
          // there is no valid overload to use:
          throw WrongArgsCount(
            funcPos, len, argsCount, group);
        }

        Output.PutCall(func, argsCount);
        return Item.End;
      }

      throw new NotSupportedException();
    }

    Item AmbiguousMatch(
      List<Capture> matches, ref int i, int len)
    {
      Debug.Assert(matches != null);

      var funcs = new List<Capture>();
      var idens = new List<Capture>();

      // count matches:
      foreach (Capture match in matches)
      {
        if (IsFunc(match))
             funcs.Add(match);
        else idens.Add(match);
      }

      // ===================================== > 0 identifiers ==
      if (funcs.Count == 0)
      {
        // no difference in any identifiers,
        // there is no way to resolve match:
        throw AmbiguousMatchException(this.curPos, idens);
      }

      int funcPos = this.curPos;

      // ===================== > 0 functions & > 0 identifiers ==
      if (!SkipBrace(ref i))
      {
        if (idens.Count == 0)
          throw NoOpenBrace(funcPos, len);

        if (idens.Count > 1)
          throw AmbiguousMatchException(funcPos, idens);

        return SimpleMatch(idens[0], ref i, len);
      }

      // ========================================================

      // count arguments and resolve suitable overloads:
      int argsCount = ArgsLookup(i);
      var overloads = GetOverloads(funcs, argsCount);

      // one argument: maybe identifier?
      if (idens.Count > 0 && argsCount == 1 && Context.ImplicitMul)
      {
        // if there is function overload with 1 argument:
        if (overloads.Count != 0)
        {
          // TODO: wrong? (maybe args? 'matches' here?)
          throw AmbiguousMatchException(funcPos, funcs);
        }

        // or more than one idenifier matched:
        if (idens.Count > 1)
        {
          throw AmbiguousMatchException(funcPos, idens);
        }

        i--; // move back to the brace
        return SimpleMatch(idens[0], ref i, len);
      }

      if (overloads.Count == 0)
      {
        // TODO: provide more detailed error message
        throw WrongArgsCount(funcPos, len, argsCount, null);
      }

      if (overloads.Count > 1)
      {
        throw AmbiguousMatchException(funcPos, funcs);
      }

      Output.PutBeginCall();

      // parse call body
      int realArgs = ParseNested(ref i, true);
      Debug.Assert(realArgs == argsCount);

      Output.PutCall(overloads[0], argsCount);
      return Item.End;
    }

    #endregion
    #region Overloads

    List<FunctionInfo<T>> GetOverloads(
      List<Capture> matches, int argsCount)
    {
      Debug.Assert(matches != null);
      Debug.Assert(argsCount >= 0);

      bool hasParams = false;
      int fixCount = -1;

      var overloads = new List<FunctionInfo<T>>();

      foreach (Capture match in matches)
      {
        var group = GetGroup(match);
        var func = group.GetOverload(argsCount);

        if (func == null) continue;

        // simple overload resolution:
        if (func.ArgsCount > fixCount)
        {
          overloads.Clear();
          overloads.Add(func);

          fixCount = func.ArgsCount;
          hasParams = func.HasParamArray;
        }
        else if (func.ArgsCount == fixCount)
        {
          if (func.HasParamArray)
          {
            if (hasParams) overloads.Add(func);
          }
          else
          {
            if (hasParams)
            {
              hasParams = false;
              overloads.Clear();
            }

            overloads.Add(func);
          }
        }
      }

      return overloads;
    }

    #endregion
    #region Helpers

    int ArgsLookup(int fromPos)
    {
      int depth = 1, args = 0;
      string ex = this.expr;

      for (int i = fromPos; i < ex.Length; i++)
      {
        if (args == 0)
        {
          if (!char.IsWhiteSpace(expr[i])) args++;
        }
        else
        {
          char c = expr[i];

          if (c == '(') depth++;
          else if (c == ')')
          {
            if (--depth == 0) return args;
          }
          else if (depth == 1 &&
            c == this.sepSymbol) args++;
        }
      }

      return args;
    }

    // NOTE: don't like it
    bool SkipBrace(ref int i)
    {
      var str = this.expr;

      while (i < str.Length &&
        char.IsWhiteSpace(str[i])) i++;

      if (i >= str.Length) return false;

      this.curPos = i;
      this.prePos = this.curPos; // TODO: is it right?

      if (str[i] == '(') { i++; return true; }
      return false;
    }

    // TODO: merge with Parse, destroy!
    int ParseNested(ref int i, bool func)
    {
      int beginPos = this.curPos;
      this.prePos = this.curPos;
      this.exprDepth++;

      int argsCount = Parse(ref i, func);
      if (argsCount == -1 && this.exprDepth > 0)
      {
        throw BraceDisbalance(beginPos, false);
      }

      this.exprDepth--;
      return argsCount;
    }

    FunctionGroup<T> GetGroup(Capture match)
    {
      return Context.Functions[match.Index];
    }

    #endregion
    #region Search

    enum IdenType
    {
      Argument,
      Constant,
      Function
    }

    static bool IsFunc(Capture match)
    {
      return match.Type == IdenType.Function;
    }

    List<Capture> GetMatchesCulture(ref int max)
    {
      CultureInfo culture = Context.Culture;
      var matches = new List<Capture>();

#if SILVERLIGHT
      var compare = Context.IgnoreCase?
        CompareOptions.IgnoreCase : CompareOptions.None;
#else
      bool ignCase = Context.IgnoreCase;
#endif

      IdenType idenType = IdenType.Argument;
      foreach (IListEnumerable list in this.literals)
      {
        int id = 0;
        foreach (string name in list)
        {
          int length = name.Length;
          if (length >= max &&
#if SILVERLIGHT
              String.Compare(this.expr, this.curPos, name, 0, length, culture, compare) == 0)
#else
              String.Compare(this.expr, this.curPos, name, 0, length, ignCase, culture) == 0)
#endif
          {
            if (length != max) matches.Clear();

            matches.Add(new Capture(idenType, id));
            max = length;
          }

          id++;
        }

        idenType++;
      }

      return matches;
    }

    List<Capture> GetMatchesOrdinal(ref int max)
    {
      var match = new List<Capture>();
      var strCmp = Context.IgnoreCase ?
        StringComparison.OrdinalIgnoreCase :
        StringComparison.Ordinal;

      IdenType idenType = IdenType.Argument;
      foreach (IListEnumerable list in this.literals)
      {
        int id = 0;
        foreach (string name in list)
        {
          int length = name.Length;
          if (length >= max && String.Compare(
            this.expr, this.curPos, name, 0, length, strCmp) == 0)
          {
            if (length != max) match.Clear();
            match.Add(new Capture(idenType, id));
            max = length;
          }

          id++;
        }

        idenType++;
      }

      return match;
    }

    struct Capture
    {
      readonly IdenType type;
      readonly int index;

      public Capture(IdenType type, int index)
      {
        Debug.Assert(index >= 0);

        this.index = index;
        this.type = type;
      }

      public IdenType Type { get { return this.type;  } }
      public int Index     { get { return this.index; } }
    }

    #endregion
  }
}