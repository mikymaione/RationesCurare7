/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ILCalc
{
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Represents the function overload group that contains
  /// <see cref="FunctionInfo{T}"/> items with different
  /// values of <see cref="FunctionInfo{T}.ArgsCount"/> and
  /// <see cref="FunctionInfo{T}.HasParamArray"/> properties.<br/>
  /// This class cannot be inherited.</summary>
  /// <typeparam name="T">Functions parameters
  /// and return value type.</typeparam>
  /// <threadsafety instance="false"/>
  [DebuggerDisplay("{Count} functions")]
  [DebuggerTypeProxy(typeof(GroupDebugView<>))]
  [Serializable]
  public sealed class FunctionGroup<T>
    : IEnumerable<FunctionInfo<T>>
  {
    #region Fields

    [DebuggerBrowsable(State.RootHidden)]
    readonly List<FunctionInfo<T>> funcList;

    #endregion
    #region Constructors

    internal FunctionGroup(FunctionInfo<T> function)
    {
      Debug.Assert(function != null);

      this.funcList = new
        List<FunctionInfo<T>>(1) { function };
    }

    // For clone
    internal FunctionGroup(FunctionGroup<T> other)
    {
      Debug.Assert(other != null);

      this.funcList = new List<
        FunctionInfo<T>>(other.funcList);
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the count of <see cref="FunctionInfo{T}">
    /// functions</see> that this group represents.</summary>
    [DebuggerBrowsable(State.Never)]
    public int Count
    {
      get { return this.funcList.Count; }
    }

    /// <summary>
    /// Gets the <see cref="FunctionInfo{T}"/>
    /// at the specified index.</summary>
    /// <param name="index">The index of the
    /// <see cref="FunctionInfo{T}"/> to get.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0.<br/>-or-<br/>
    /// <paramref name="index"/> is equal to or
    /// greater than <see cref="Count"/></exception>
    /// <returns>The <see cref="FunctionInfo{T}"/>
    /// at the specified index.</returns>
    public FunctionInfo<T> this[int index]
    {
      get { return this.funcList[index]; }
    }

    #endregion
    #region Methods

    // TODO: maybe Add & ICollection<T>?

    /// <summary>
    /// Removes the <see cref="FunctionInfo{T}"/> with
    /// the specified <paramref name="argsCount"/> and
    /// <paramref name="hasParamArray"/> values from
    /// the <see cref="FunctionGroup{T}"/>.</summary>
    /// <param name="argsCount">
    /// <see cref="FunctionInfo{T}"/> arguments count.</param>
    /// <param name="hasParamArray">Indicates that
    /// <see cref="FunctionInfo{T}"/> has an parameters array.</param>
    /// <returns><b>true</b> if specified <see cref="FunctionInfo{T}"/>
    /// is founded in the group and was removed;
    /// otherwise, <b>false</b>.</returns>
    public bool Remove(int argsCount, bool hasParamArray)
    {
      for (int i = 0; i < Count; i++)
      {
        FunctionInfo<T> func = this.funcList[i];

        if (func.ArgsCount == argsCount &&
            func.HasParamArray == hasParamArray)
        {
          this.funcList.RemoveAt(i);
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Removes the <see cref="FunctionInfo{T}"/> at the specified
    /// index of the <see cref="FunctionGroup{T}"/>.</summary>
    /// <param name="index">The zero-based index
    /// of the <see cref="FunctionInfo{T}"/> to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0,
    /// equal to or greater than Count.</exception>
    public void RemoveAt(int index)
    {
      this.funcList.RemoveAt(index);
    }

    /// <summary>
    /// Removes all <see cref="FunctionInfo{T}">functions</see>
    /// from the <see cref="FunctionGroup{T}"/>.</summary>
    public void Clear()
    {
      this.funcList.Clear();
    }

    /// <summary>
    /// Determines whether a <see cref="FunctionInfo{T}"/>
    /// with the specified <paramref name="argsCount"/> and
    /// <paramref name="hasParamArray"/> values is contains
    /// in the <see cref="FunctionGroup{T}"/>.</summary>
    /// <param name="argsCount">
    /// <see cref="FunctionInfo{T}"/> arguments count.</param>
    /// <param name="hasParamArray">Indicates that
    /// <see cref="FunctionInfo{T}"/> has an parameters array.</param>
    /// <returns><b>true</b> if function is found in the group;
    /// otherwise, <b>false</b>.</returns>
    public bool Contains(int argsCount, bool hasParamArray)
    {
      foreach (FunctionInfo<T> func in this.funcList)
      {
        if (func.ArgsCount == argsCount &&
            func.HasParamArray == hasParamArray)
        {
          return true;
        }
      }

      return false;
    }

    #endregion
    #region IEnumerable<>

    /// <summary>
    /// Returns an enumerator that iterates through
    /// the <see cref="FunctionInfo{T}">functions</see>
    /// in <see cref="FunctionGroup{T}"/>.</summary>
    /// <returns>An enumerator for the all <see cref="FunctionInfo{T}">
    /// functions</see> in <see cref="FunctionGroup{T}"/>.</returns>
    public IEnumerator<FunctionInfo<T>> GetEnumerator()
    {
      return this.funcList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.funcList.GetEnumerator();
    }

    #endregion
    #region Internals

    internal bool Append(FunctionInfo<T> func)
    {
      Debug.Assert(func != null);

      foreach (FunctionInfo<T> f in this.funcList)
      {
        if (func.ArgsCount == f.ArgsCount &&
            func.HasParamArray == f.HasParamArray)
        {
          return false;
        }
      }

      this.funcList.Add(func);
      return true;
    }

    internal string MakeMethodsArgsList()
    {
      switch (this.funcList.Count)
      {
        case 0: return string.Empty;
        case 1: return this.funcList[0].ArgsString;
      }

      var buf = new StringBuilder();
      this.funcList.Sort(ArgsCountComparator);

      // output first:
      buf.Append(this.funcList[0].ArgsString);

      // and others:
      for (int i = 1, last = Count - 1; i < Count; i++)
      {
        FunctionInfo<T> func = this.funcList[i];

        if (i == last)
        {
          buf.Append(' ')
             .Append(Resource.sAnd)
             .Append(' ');
        }
        else buf.Append(", ");

        buf.Append(func.ArgsString);
      }

      return buf.ToString();
    }

    internal FunctionInfo<T> GetOverload(int argsCount)
    {
      Debug.Assert(argsCount >= 0);

      FunctionInfo<T> best = null;
      int fixCount = -1;

      foreach (var func in this.funcList)
      {
        if (func.HasParamArray)
        {
          if (func.ArgsCount <= argsCount &&
              func.ArgsCount > fixCount)
          {
            best = func;
            fixCount = func.ArgsCount;
          }
        }
        else if (func.ArgsCount == argsCount)
        {
          return func;
        }
      }

      return best;
    }

    static int ArgsCountComparator(
      FunctionInfo<T> a, FunctionInfo<T> b)
    {
      if (a.ArgsCount == b.ArgsCount)
      {
        if (a.HasParamArray == b.HasParamArray)
          return 0;

        return a.HasParamArray ? 1 : -1;
      }

      return a.ArgsCount < b.ArgsCount ? -1 : 1;
    }

    #endregion
  }

  #region Debug View

  sealed class GroupDebugView<T>
  {
    [DebuggerBrowsable(State.RootHidden)]
    readonly FunctionInfo<T>[] items;

    public GroupDebugView(FunctionGroup<T> list)
    {
      this.items = new FunctionInfo<T>[list.Count];
      int i = 0;
      foreach (var f in list)
      {
        this.items[i++] = f;
      }
    }
  }

  #endregion
}