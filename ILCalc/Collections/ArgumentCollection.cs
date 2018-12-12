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

namespace ILCalc
{
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Manages the unique arguments names
  /// available to an expression.<br/>
  /// This class cannot be inherited.</summary>
  /// <remarks>
  /// When any of methods for evaluating expression
  /// is calling, the arguments should be passed in
  /// the same order as their names are presented
  /// in the context's <see cref="ArgumentCollection"/>.
  /// </remarks>
  /// <threadsafety instance="false"/>
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public sealed class ArgumentCollection
    : IList<string>,
      ICollection,
      IListEnumerable
  {
    #region Fields

    [DebuggerBrowsable(State.RootHidden)]
    readonly List<string> namesList;

    #endregion
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ArgumentCollection"/> class
    /// that is empty and has the default
    /// initial capacity.</summary>
    /// <overloads>
    /// Initializes a new instance of the
    /// <see cref="ArgumentCollection"/> class.
    /// </overloads>
    public ArgumentCollection()
    {
      this.namesList = new List<string>();
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ArgumentCollection"/> class that
    /// has one specific argument name inside.</summary>
    /// <param name="name">Argument name.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="name"/> is not
    /// valid identifier name.</exception>
    public ArgumentCollection(string name)
    {
      Validator.CheckName(name);
      this.namesList = new List<string>(1) { name };
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ArgumentCollection"/> class that
    /// has specified arguments names inside.</summary>
    /// <param name="names">Arguments names.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="names"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some item of <paramref name="names"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some item of <paramref name="names"/>
    /// is already exist in the list.</exception>
    public ArgumentCollection(params string[] names)
    {
      if (names == null)
        throw new ArgumentNullException("names");

      this.namesList = new List<string>(names.Length);
      foreach (string name in names) Add(name);
    }

    /// <summary>
    /// Initializes a new instance of
    /// the <see cref="ArgumentCollection"/> class
    /// that has specified arguments names inside.
    /// </summary>
    /// <param name="names">
    /// Enumerable with arguments names.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="names"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some item of <paramref name="names"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some item of <paramref name="names"/>
    /// is already exist in the list.</exception>
    public ArgumentCollection(IEnumerable<string> names)
    {
      if (names == null)
        throw new ArgumentNullException("names");

      this.namesList = new List<string>();
      foreach (string name in names)
      {
        Add(name);
      }
    }

    /// <summary>
    /// Initializes a new instance of
    /// the <see cref="ArgumentCollection"/> class from the
    /// other <see cref="ArgumentCollection"/> instance.
    /// </summary>
    /// <param name="list">ArgumentCollection instance.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="list"/> is null.</exception>
    public ArgumentCollection(ArgumentCollection list)
    {
      if (list == null)
        throw new ArgumentNullException("list");

      this.namesList = new List<string>(list.namesList);
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets the number of names actually contained
    /// in the <see cref="ArgumentCollection"/>.
    /// </summary>
    [DebuggerBrowsable(State.Never)]
    public int Count
    {
      get { return this.namesList.Count; }
    }

    /// <summary>
    /// Gets a value indicating whether the 
    /// <see cref="ICollection{T}"/> is read-only.
    /// </summary>
    /// <value>Always <b>false</b>.</value>
    [DebuggerBrowsable(State.Never)]
    public bool IsReadOnly
    {
      get { return false; }
    }

    [DebuggerBrowsable(State.Never)]
    bool ICollection.IsSynchronized
    {
      get { return false; }
    }

    [DebuggerBrowsable(State.Never)]
    object ICollection.SyncRoot
    {
      get 
      {
        return ((ICollection)
          this.namesList).SyncRoot;
      }
    }

    /// <summary>
    /// Gets or sets the argument name
    /// at the specified index.</summary>
    /// <param name="index">The zero-based
    /// index of the name to get or set.</param>
    /// <returns>The name at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0, equal to or
    /// greater than <see cref="Count"/> value.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is not valid identifier
    /// name<br/>-or-<br/><paramref name="value"/> name
    /// is already exist in the list.</exception>
    public string this[int index]
    {
      get
      {
        return this.namesList[index];
      }

      set
      {
        if (this.namesList[index] == value) return;

        Validator.CheckName(value);
        if (this.namesList.Contains(value))
        {
          throw new ArgumentException(
            string.Format(
              Resource.errArgumentExist, value));
        }

        this.namesList[index] = value;
      }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Searches for the specified name
    /// and returns the zero-based index of name
    /// in the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="item">The name to locate
    /// in the <see cref="ArgumentCollection"/>.</param>
    /// <returns>The zero-based index of the
    /// first occurrence of name within the
    /// entire <see cref="ArgumentCollection"/>,
    /// if found; otherwise, –1.</returns>
    public int IndexOf(string item)
    {
      return this.namesList.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the
    /// <see cref="ArgumentCollection"/>
    /// at the specified index.</summary>
    /// <param name="index">The zero-based index
    /// at which name should be inserted.</param>
    /// <param name="item">The name to insert</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="item"/> is not
    /// valid identifier name.<br/>-or-<br/>
    /// Argument with <paramref name="item"/>
    /// name is already exist.</exception>
    public void Insert(int index, string item)
    {
      Validator.CheckName(item);
      if (this.namesList.Contains(item))
      {
        throw new ArgumentException(
          string.Format(
            Resource.errArgumentExist, item));
      }

      this.namesList.Insert(index, item);
    }

    /// <summary>
    /// Removes the name at the specified index
    /// of the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="index">The zero-based index
    /// of the name to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0,
    /// equal to or greater than Count.</exception>
    public void RemoveAt(int index)
    {
      this.namesList.RemoveAt(index);
    }

    /// <summary>Adds name to the end
    /// of the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="item">Argument name to add.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="item"/> is not
    /// valid identifier name.<br/>-or-<br/>
    /// <paramref name="item"/> name is already
    /// exist in the list.</exception>
    public void Add(string item)
    {
      Validator.CheckName(item);
      if (this.namesList.Contains(item))
      {
        throw new ArgumentException(
          string.Format(
            Resource.errArgumentExist, item));
      }

      this.namesList.Add(item);
    }

    /// <summary>
    /// Adds the elements of the specified collection
    /// to the end of the <see cref="ArgumentCollection"/>.
    /// </summary>
    /// <param name="names">
    /// Enumerable with argument names.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="names"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some item of <paramref name="names"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some item of <paramref name="names"/>
    /// is already exist in the list.</exception>
    public void AddRange(IEnumerable<string> names)
    {
      if (names == null)
        throw new ArgumentNullException("names");

      foreach (string name in names)
      {
        Add(name);
      }
    }

    /// <summary>
    /// Adds the specified elements to the end
    /// of the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="names">Array of the argument names.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="names"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some item of <paramref name="names"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some item of <paramref name="names"/>
    /// is already exist in the list.</exception>
    public void AddRange(params string[] names)
    {
      if (names == null)
        throw new ArgumentNullException("names");

      foreach (string name in names)
      {
        Add(name);
      }
    }

    /// <summary>
    /// Removes all names from the
    /// <see cref="ArgumentCollection"/>.
    /// </summary>
    public void Clear()
    {
      this.namesList.Clear();
    }

    /// <summary>
    /// Determines whether a name is contains in
    /// the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="item">Argument name to locate
    /// in <see cref="ArgumentCollection"/>.</param>
    /// <returns><b>true</b> if name is found in the list;
    /// otherwise, <b>false</b>.</returns>
    public bool Contains(string item)
    {
      return this.namesList.Contains(item);
    }

    /// <summary>
    /// Copies the entire list of arguments
    /// names to a one-dimensional array of strings,
    /// starting at the specified index
    /// of the target array.</summary>
    /// <param name="array">The one-dimensional
    /// <see cref="Array"/> of strings that is
    /// the destination of the names copied
    /// from <see cref="ArgumentCollection"/>.
    /// The <see cref="Array"/> must
    /// have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index
    /// in array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex"/> is less than zero.</exception>
    /// <exception cref="ArgumentException"><paramref name="arrayIndex"/>
    /// is equal to or greater than the length of array.<br/>-or-<br/>
    /// Number of names in the source <see cref="ArgumentCollection"/>
    /// is greater than the available space from <paramref name="arrayIndex"/>
    /// to the end of the destination <paramref name="array"/>.</exception>
    public void CopyTo(string[] array, int arrayIndex)
    {
      this.namesList.CopyTo(array, arrayIndex);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection) this.namesList)
        .CopyTo(array, index);
    }

    /// <summary>
    /// Removes the specific name from
    /// the <see cref="ArgumentCollection"/>.</summary>
    /// <param name="item">The name to be removed.</param>
    /// <returns><b>true</b> if name is successfully removed;
    /// otherwise, <b>false</b>.</returns>
    public bool Remove(string item)
    {
      return this.namesList.Remove(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the names
    /// in <see cref="ArgumentCollection"/>.</summary>
    /// <returns>An enumerator for the all names
    /// in <see cref="ArgumentCollection"/>.</returns>
    public IEnumerator<string> GetEnumerator()
    {
      return this.namesList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.namesList.GetEnumerator();
    }

    #endregion
    #region IListEnumerable

    List<string>.Enumerator
      IListEnumerable.GetEnumerator()
    {
      return this.namesList.GetEnumerator();
    }

    #endregion
  }
}