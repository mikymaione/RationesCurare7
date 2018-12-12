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
using System.Reflection;

namespace ILCalc
{
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Manages the collection from pairs
  /// of unique names and values of constants
  /// available to an expression.<br/>
  /// This class cannot be inherited.
  /// </summary>
  /// <typeparam name="T">
  /// The type of the constant values
  /// in the dictionary.</typeparam>
  /// <threadsafety instance="false" static="true"/>
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof(ConstantsDebugView<>))]
  [Serializable]
  public sealed class ConstantDictionary<T>
    : IDictionary<string, T>,
      IListEnumerable,
      ICollection
  {
    #region Fields

    [DebuggerBrowsable(State.Never)]
    readonly List<string> namesList;

    [DebuggerBrowsable(State.Never)]
    readonly List<T> valuesList;

    #endregion
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ConstantDictionary{T}"/> class that is
    /// empty and has the default initial capacity.</summary>
    /// <overloads>
    /// Initializes a new instance of the
    /// <see cref="ConstantDictionary{T}"/> class.
    /// </overloads>
    public ConstantDictionary()
    {
      this.namesList = new List<string>();
      this.valuesList = new List<T>();
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ConstantDictionary{T}"/> class
    /// from the instance of <see cref="ICollection{T}"/>
    /// containing pairs of constant names and values.
    /// </summary>
    /// <param name="collection"><see cref="ICollection"/>
    /// of the name/value pairs.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="collection"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some name of <paramref name="collection"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some name of <paramref name="collection"/>
    /// is already exist in the dictionary.</exception>
    public ConstantDictionary(
      ICollection<KeyValuePair<string, T>> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");

      this.namesList = new List<string>(collection.Count);
      this.valuesList = new List<T>(collection.Count);

      foreach (var pair in collection)
      {
        Add(pair.Key, pair.Value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ConstantDictionary{T}"/> class from the
    /// other <see cref="ConstantDictionary{T}"/> instance.</summary>
    /// <param name="dictionary">
    /// <see cref="ConstantDictionary{T}"/> instance.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dictionary"/> is null.</exception>
    public ConstantDictionary(ConstantDictionary<T> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");

      this.namesList = new List<string>(dictionary.namesList);
      this.valuesList = new List<T>(dictionary.valuesList);
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets a collection containing the constant names
    /// of the <see cref="ConstantDictionary{T}"/>.</summary>
    [DebuggerBrowsable(State.Never)]
    public ICollection<string> Keys
    {
      get { return this.namesList.AsReadOnly(); }
    }

    /// <summary>
    /// Gets a collection containing the constant values
    /// of the <see cref="ConstantDictionary{T}"/>.</summary>
    [DebuggerBrowsable(State.Never)]
    public ICollection<T> Values
    {
      get { return this.valuesList.AsReadOnly(); }
    }

    /// <summary>
    /// Gets a value indicating whether the 
    /// <see cref="ICollection{T}"/> is read-only.</summary>
    /// <value>Always <b>false</b>.</value>
    [DebuggerBrowsable(State.Never)]
    public bool IsReadOnly
    {
      get { return false; }
    }

    /// <summary>
    /// Gets the number of constants actually contained
    /// in the <see cref="ConstantDictionary{T}"/>.</summary>
    [DebuggerBrowsable(State.Never)]
    public int Count
    {
      get { return this.namesList.Count; }
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
    /// Gets or sets the constant value associated
    /// with the specified name.</summary>
    /// <overloads>Gets or sets the value associated with
    /// the specified constant name or index.</overloads>
    /// <param name="key">The name of the constant,
    /// which value to get or set.</param>
    /// <exception cref="KeyNotFoundException">
    /// The property is retrieved and name
    /// does not exist in the dicitonary.</exception>
    /// <exception cref="ArgumentException">
    /// The property is setted and <paramref name="key"/>
    /// is not valid identifier name.</exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="key"/> is null.</exception>
    /// <returns>The value associated with the specified name.
    /// If the specified name is not found, a get operation throws
    /// a <see cref="KeyNotFoundException"/>, and a set operation
    /// creates a new element with the specified name.</returns>
    [DebuggerBrowsable(State.Never)]
    public T this[string key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException("key");

        int index = this.namesList.IndexOf(key);
        if (index < 0)
        {
          throw new KeyNotFoundException(
            string.Format(
            Resource.errConstantNotExist, key));
        }

        return this.valuesList[index];
      }
      set
      {
        int index = this.namesList.IndexOf(key);

        if (index < 0) Add(key, value);
        else this.valuesList[index] = value;
      }
    }

    /// <summary>
    /// Gets or sets the constant value
    /// at the specified index.</summary>
    /// <param name="index">
    /// The list index of the constant,
    /// which value to get or set.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0.<br/>-or-<br/>
    /// <paramref name="index"/> is equal to or greater
    /// than <see cref="Count"/>.</exception>
    /// <returns>The constant value
    /// at the specified index.</returns>
    public T this[int index]
    {
      get { return this.valuesList[index]; }
      set { this.valuesList[index] = value; }
    }

    #endregion
    #region Methods

    /// <summary>
    /// Adds the constant with the provided name and value
    /// to the <see cref="ConstantDictionary{T}"/>.</summary>
    /// <param name="key">Constant name.</param>
    /// <param name="value">Constant value.</param>
    /// <exception cref="ArgumentException"><paramref name="key"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// <paramref name="key"/> name is already
    /// exist in the dictionary.</exception>
    public void Add(string key, T value)
    {
      Validator.CheckName(key);
      if (this.namesList.Contains(key))
      {
        throw new ArgumentException(
          string.Format(
            Resource.errConstantExist, key));
      }

      this.namesList.Add(key);
      this.valuesList.Add(value);
    }

    /// <summary>
    /// Adds the elements of the specified collection to the end
    /// of the <see cref="ConstantDictionary{T}"/>.</summary>
    /// <param name="collection">
    /// Enumerable of the name and value pairs.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="collection"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some name of <paramref name="collection"/>
    /// is not valid identifier name.<br/>-or-<br/>
    /// Some name of <paramref name="collection"/>
    /// is already exist in the dictionary.</exception>
    public void AddRange(
      IEnumerable<KeyValuePair<string,T>> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");

      foreach (var pair in collection)
      {
        Add(pair.Key, pair.Value);
      }
    }

    /// <summary>
    /// Determines whether the <see cref="ConstantDictionary{T}"/>
    /// contains the specified name.</summary>
    /// <param name="key">Constant name to locate in the
    /// <see cref="ConstantDictionary{T}"/>.</param>
    /// <returns><b>true</b> if name is found in the dictionary;
    /// otherwise, <b>false</b>.</returns>
    public bool ContainsKey(string key)
    {
      return this.namesList.Contains(key);
    }

    /// <summary>
    /// Removes the constant specified by name from the
    /// <see cref="ConstantDictionary{T}"/>.</summary>
    /// <param name="key">The function name to be removed.</param>
    /// <returns><b>true</b> if constant is successfully removed;
    /// otherwise, <b>false</b>.</returns>
    public bool Remove(string key)
    {
      int index = this.namesList.IndexOf(key);
      if (index >= 0)
      {
        this.namesList.RemoveAt(index);
        this.valuesList.RemoveAt(index);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Tries to get the value of constant
    /// with the specified name.</summary>
    /// <param name="key">The name of the constant,
    /// which value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value of constant
    /// with the specified name, if the name is found; otherwise,
    /// the default value for the type of the value parameter.
    /// This parameter is passed uninitialized.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="key"/> is null.</exception>
    /// <returns><b>true</b> if the
    /// <see cref="ConstantDictionary{T}"/>
    /// contains an element with the specified name;
    /// otherwise, <b>false</b>.</returns>
    public bool TryGetValue(string key, out T value)
    {
      if (key == null)
        throw new ArgumentNullException("key");

      int index = this.namesList.IndexOf(key);
      if (index < 0)
      {
        value = default(T);
        return false;
      }

      value = this.valuesList[index];
      return true;
    }

    void ICollection<KeyValuePair<string,T>>
      .Add(KeyValuePair<string,T> item)
    {
      Add(item.Key, item.Value);
    }

    bool ICollection<KeyValuePair<string,T>>
      .Contains(KeyValuePair<string,T> item)
    {
      int index = this.namesList.IndexOf(item.Key);
      var comparer = EqualityComparer<T>.Default;

      return index >= 0 &&
        comparer.Equals(
          this.valuesList[index], item.Value);
    }

    void ICollection<KeyValuePair<string,T>>
      .CopyTo(KeyValuePair<string,T>[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");

      if (arrayIndex < 0 || arrayIndex > array.Length)
        throw new ArgumentOutOfRangeException("arrayIndex");

      if (array.Length - arrayIndex < Count)
        throw new ArgumentOutOfRangeException("arrayIndex");

      for (int i = 0; i < Count; i++)
      {
        array[arrayIndex + i] =
          new KeyValuePair<string,T>(
            this.namesList[i],
            this.valuesList[i]);
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection<KeyValuePair<string,T>>) this)
        .CopyTo((KeyValuePair<string,T>[]) array, index);
    }

    /// <summary>
    /// Removes all constants from
    /// the <see cref="ConstantDictionary{T}"/>.</summary>
    public void Clear()
    {
      this.namesList.Clear();
      this.valuesList.Clear();
    }

    bool ICollection<KeyValuePair<string,T>>
      .Remove(KeyValuePair<string,T> item)
    {
      int index = this.namesList.IndexOf(item.Key);
      var comparer = EqualityComparer<T>.Default;

      if (index >= 0 &&
        comparer.Equals(this.valuesList[index], item.Value))
      {
        this.namesList.RemoveAt(index);
        this.valuesList.RemoveAt(index);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Returns an enumerator that iterates through
    /// the pairs of names and values in
    /// <see cref="ConstantDictionary{T}"/>.</summary>
    /// <returns>An enumerator object for the pair items
    /// in the <see cref="ConstantDictionary{T}"/>.</returns>
    IEnumerator<KeyValuePair<string,T>>
      IEnumerable<KeyValuePair<string,T>>
      .GetEnumerator()
    {
      for (int i = 0; i < Count; i++)
      {
        yield return new KeyValuePair<string,T>(
          this.namesList[i],
          this.valuesList[i]);
      }

      yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<KeyValuePair<string,T>>)
        this).GetEnumerator();
    }

    /// <summary>
    /// Imports all public static fields and
    /// constants of the specified type into this
    /// <see cref="ConstantDictionary{T}"/>.</summary>
    /// <overloads>
    /// Imports static fields and constants
    /// of the specified type(s) into this
    /// <see cref="ConstantDictionary{T}"/>.
    /// </overloads>
    /// <param name="type">Type object.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some of the importing constants has a name
    /// that is already exist in the dictionary.
    /// </exception>
    public void Import(Type type)
    {
      if (type == null)
        throw new ArgumentNullException("type");

      Validator.CheckVisible(type);

      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      InternalImport(type, Flags);
    }

#if !SILVERLIGHT

    /// <summary>
    /// Imports all public static fields and
    /// constants of the specified type into this
    /// <see cref="ConstantDictionary{T}"/>.</summary>
    /// <param name="type">Type object.</param>
    /// <param name="nonpublic">
    /// Include non public member methods in search.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some of the importing constants has a name
    /// that is already exist in the dictionary.</exception>
    /// <remarks>Doesn't supported in Silverlight
    /// because of reflection limitations.</remarks>
    public void Import(Type type, bool nonpublic)
    {
      if (type == null)
        throw new ArgumentNullException("type");

      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      InternalImport(type, Flags |
        (nonpublic ? BindingFlags.NonPublic : 0));
    }

#endif

    /// <summary>
    /// Imports all public static fields and
    /// constants of the specified types into this
    /// <see cref="ConstantDictionary{T}"/>.</summary>
    /// <param name="types">
    /// Params array of <see cref="Type"/> objects.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="types"/> is null.<br>-or-</br>
    /// Some element of <paramref name="types"/>
    /// array is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some of the importing constants has a name
    /// that is already exist in the dictionary.</exception>
    public void Import(params Type[] types)
    {
      if (types == null)
        throw new ArgumentNullException("types");

      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      foreach (Type type in types)
      {
        InternalImport(type, Flags);
      }
    }

    void InternalImport(Type type, BindingFlags flags)
    {
      if (type == null)
        throw new ArgumentNullException("type");

      foreach (FieldInfo field in type.GetFields(flags))
      {
        // look for "const T" fields:
        if (field.FieldType == TypeHelper<T>.ValueType &&
           (field.IsLiteral ||
           (field.IsInitOnly && field.IsStatic)))
        {
          Add(field.Name, (T) field.GetValue(null));
        }
      }
    }

    #endregion
    #region IListEnumerable

    List<string>.Enumerator IListEnumerable.GetEnumerator()
    {
      return this.namesList.GetEnumerator();
    }

    #endregion
    #region ImportBuiltIn

    [DebuggerBrowsable(State.Never)]
    static readonly ConstantDictionary<T>
      BuiltIns = ImportHelper.ResolveConstants<T>();

    [DebuggerBrowsable(State.Never)]
    static readonly object SyncRoot = new object();

    /// <summary>
    /// Imports standart builtin constants into 
    /// this <see cref="ConstantDictionary{T}"/>.</summary>
    /// <exception cref="ArgumentException">
    /// Some of names is already exist in the dictionary.
    /// </exception>
    public void ImportBuiltIn()
    {
      if (BuiltIns != null)
      lock(SyncRoot)
      {
        AddRange(BuiltIns);
      }
    }

    /// <summary>
    /// Set the custom dictionary as built-ins imports for the
    /// ConstantDictionary of type <typeparamref name="T"/>.</summary>
    /// <param name="dictionary">Dictionary to set.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dictionary"/> is null.</exception>
    public static void SetBuiltIns(ConstantDictionary<T> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");

      lock(SyncRoot)
      {
        BuiltIns.Clear();
        BuiltIns.AddRange(dictionary);
      }
    }

    #endregion
  }

  #region Debug View

  sealed class ConstantsDebugView<T>
  {
    [DebuggerBrowsable(State.RootHidden)]
    readonly ViewItem[] items;

    public ConstantsDebugView(ConstantDictionary<T> list)
    {
      this.items = new ViewItem[list.Count];
      int i = 0;
      foreach (var item in list)
      {
        this.items[i].Name  = item.Key;
        this.items[i].Value = item.Value;
        i++;
      }
    }

    [DebuggerDisplay("{Value}", Name = "{Name}")]
    struct ViewItem
    {
      // ReSharper disable UnaccessedField.Local

      [DebuggerBrowsable(State.Never)] public string Name;
      [DebuggerBrowsable(State.RootHidden)] public T Value;

      // ReSharper restore UnaccessedField.Local
    }
  }

  #endregion
}