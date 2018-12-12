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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace ILCalc
{
  using State = DebuggerBrowsableState;

  /// <summary>
  /// Manages the pairs list of names and attached
  /// function groups available to an expression.
  /// Function names are unique, but they can be overloaded
  /// by arguments count and the parameters array presence.<br/>
  /// This class cannot be inherited.</summary>
  /// <typeparam name="T">Functions parameters
  /// and return value type.</typeparam>
  /// <threadsafety instance="false" static="true"/>
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof(FunctionDebugView<>))]
  [Serializable]
  public sealed class FunctionCollection<T>
    : IEnumerable<KeyValuePair<string, FunctionGroup<T>>>,
      IListEnumerable, ICollection
  {
    #region Fields

    [DebuggerBrowsable(State.Never)]
    readonly List<string> namesList;

    [DebuggerBrowsable(State.Never)]
    readonly List<FunctionGroup<T>> funcsList;

    #endregion
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="FunctionCollection{T}"/> class that is empty
    /// and has the default initial capacity.</summary>
    /// <overloads>Initializes a new instance of the
    /// <see cref="FunctionCollection{T}"/> class.</overloads>
    public FunctionCollection()
    {
      this.namesList = new List<string>();
      this.funcsList = new List<FunctionGroup<T>>();
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="FunctionCollection{T}"/> class from the other
    /// <see cref="FunctionCollection{T}"/> instance.</summary>
    /// <param name="collection">
    /// <see cref="FunctionCollection{T}"/> instance.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="collection"/> is null.</exception>
    public FunctionCollection(FunctionCollection<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");

      this.namesList = new List<string>(collection.namesList);
      this.funcsList = new List<FunctionGroup<T>>(collection.Count);

      foreach (var group in collection.funcsList)
      {
        this.funcsList.Add(
          new FunctionGroup<T>(group));
      }
    }

    #endregion
    #region Properties

    /// <summary>
    /// Gets a collection containing the names
    /// of the <see cref="FunctionCollection{T}"/>.
    /// </summary>
    [DebuggerBrowsable(State.Never)]
    public ReadOnlyCollection<string> Names
    {
      get { return this.namesList.AsReadOnly(); }
    }

    /// <summary>
    /// Gets a collection containing the function
    /// groups of the <see cref="FunctionCollection{T}"/>.
    /// </summary>
    [DebuggerBrowsable(State.Never)]
    public ReadOnlyCollection<FunctionGroup<T>> Functions
    {
      get { return this.funcsList.AsReadOnly(); }
    }

    /// <summary>
    /// Gets the number of functions actually
    /// contained in the <see cref="FunctionCollection{T}"/>.
    /// </summary>
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
    /// Gets the <see cref="FunctionGroup{T}"/> associated
    /// with the specified function name.</summary>
    /// <overloads>Gets the <see cref="FunctionGroup{T}"/>
    /// with the specified function name or index.</overloads>
    /// <param name="key">The name of the function,
    /// which <see cref="FunctionGroup{T}"/> to get.</param>
    /// <exception cref="KeyNotFoundException">The property is
    /// retrieved and name does not exist in the collection.</exception>
    /// <exception cref="ArgumentNullException">The property
    /// is setted and <paramref name="key"/> is null.</exception>
    /// <returns>The <see cref="FunctionGroup{T}"/> associated with
    /// the specified function name. If the specified name is not
    /// found throws a <see cref="KeyNotFoundException"/>.</returns>
    [DebuggerBrowsable(State.Never)]
    public FunctionGroup<T> this[string key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException("key");

        int index = this.namesList.IndexOf(key);
        if (index < 0)
        {
          throw new KeyNotFoundException(string.Format(
            Resource.errFunctionNotExist, key));
        }

        return this.funcsList[index];
      }
    }

    /// <summary>
    /// Gets the <see cref="FunctionGroup{T}"/>
    /// at the specified index.</summary>
    /// <param name="index">The index of the function,
    /// which <see cref="FunctionGroup{T}"/> to get.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> is less than 0.<br/>-or-<br/>
    /// <paramref name="index"/> is equal to or greater
    /// than <see cref="Count"/></exception>
    /// <returns>The <see cref="FunctionGroup{T}"/>
    /// at the specified index.</returns>
    public FunctionGroup<T> this[int index]
    {
      get { return this.funcsList[index]; }
    }

    #endregion
    #region Methods

    #region Add

    /// <summary>
    /// Adds the <see cref="FunctionInfo{T}"/>
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <overloads>Adds the function to the
    /// <see cref="FunctionCollection{T}"/>.</overloads>
    /// <param name="function">
    /// <see cref="FunctionInfo{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="function"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <see cref="FunctionInfo{T}"/> with the same name
    /// and the same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(FunctionInfo<T> function)
    {
      if (function == null)
        throw new ArgumentNullException("function");

      AddFunc(function);
    }

    /// <summary>
    /// Adds the <see cref="FunctionInfo{T}"/>
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Funtion group name.</param>
    /// <param name="function">
    /// <see cref="FunctionInfo{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="name"/> is null.<br/>-or-<br/>
    /// <paramref name="function"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <see cref="FunctionInfo{T}"/> with the same name
    /// and the same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(string name, FunctionInfo<T> function)
    {
      if (function == null)
        throw new ArgumentNullException("function");

      AddFunc(name, function);
    }

    #region Delegates

    /// <summary>
    /// Adds the <see cref="EvalFunc0{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="target">
    /// <see cref="EvalFunc0{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="name"/> is null.<br/>-or-<br/>
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and the same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(string name, EvalFunc0<T> target)
    {
      AddFunc(name, FunctionFactory<T>
        .FromKnownDelegate(target, 0, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFunc1{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="target">
    /// <see cref="EvalFunc1{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="name"/> is null.<br/>-or-<br/>
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(string name, EvalFunc1<T> target)
    {
      AddFunc(name, FunctionFactory<T>
        .FromKnownDelegate(target, 1, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFunc2{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="target">
    /// <see cref="EvalFunc2{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="name"/> is null.<br/>-or-<br/>
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(string name, EvalFunc2<T> target)
    {
      AddFunc(name, FunctionFactory<T>
        .FromKnownDelegate(target, 2, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFuncN{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="target">
    /// <see cref="EvalFuncN{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add(string name, EvalFuncN<T> target)
    {
      AddFunc(name, FunctionFactory<T>
        .FromDelegate(target, true));
    }

    /// <summary>
    /// Adds the <typeparamref name="TDelegate"/> delegate
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <typeparam name="TDelegate">Delegate type.</typeparam>
    /// <param name="name">Function group name.</param>
    /// <param name="target">
    /// <typeparamref name="TDelegate"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <typeparamref name="TDelegate"/>
    /// is not delegate type.<br/>-or-<br/>
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void AddDel<TDelegate>(string name, TDelegate target)
    {
      Type type = typeof(TDelegate);
      if (!typeof(Delegate).IsAssignableFrom(type))
      {
        throw new ArgumentException();
      }

      Validator.CheckVisible(type);

      AddFunc(name, FunctionFactory<T>
        .FromDelegate((Delegate) (object) target, true));
    }

    #if !CF2

    /// <summary>
    /// Adds the <see cref="EvalFunc0{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="target">
    /// <see cref="EvalFunc0{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and the same arguments count already exist
    /// in the collection (overload impossible).</exception>
    /// <remarks>Not available on .NET CF 2.0 because
    /// it's impossible to resolve method name.</remarks>
    public void Add(EvalFunc0<T> target)
    {
      AddFunc(FunctionFactory<T>
        .FromKnownDelegate(target, 0, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFunc1{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="target">
    /// <see cref="EvalFunc1{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    /// <remarks>Not available on .NET CF 2.0 because
    /// it's impossible to resolve method name.</remarks>
    public void Add(EvalFunc1<T> target)
    {
      AddFunc(FunctionFactory<T>
        .FromKnownDelegate(target, 1, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFunc2{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="target">
    /// <see cref="EvalFunc2{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    /// <remarks>Not available on .NET CF 2.0 because
    /// it's impossible to resolve method name.</remarks>
    public void Add(EvalFunc2<T> target)
    {
      AddFunc(FunctionFactory<T>
        .FromKnownDelegate(target, 2, true));
    }

    /// <summary>
    /// Adds the <see cref="EvalFuncN{T}"/> delegate
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="target">
    /// <see cref="EvalFuncN{T}"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    /// <remarks>Not available on .NET CF 2.0 because
    /// it's impossible to resolve method name.</remarks>
    public void Add(EvalFuncN<T> target)
    {
      AddFunc(FunctionFactory<T>
        .FromDelegate(target, true));
    }

    /// <summary>
    /// Adds the <typeparamref name="TDelegate"/> delegate
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <typeparam name="TDelegate">Delegate type.</typeparam>
    /// <param name="target">
    /// <typeparamref name="TDelegate"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <typeparamref name="TDelegate"/>
    /// is not delegate type.<br/>-or-<br/>
    /// <paramref name="target"/> is not valid delegate
    /// to be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void Add<TDelegate>(TDelegate target)
    {
      var delType = typeof(TDelegate);
      if (!typeof(Delegate).IsAssignableFrom(delType))
      {
        throw new ArgumentException();
      }

      AddFunc(FunctionFactory<T>
        .FromDelegate((Delegate) (object) target, true));
    }

#endif

    #endregion

    /// <summary>
    /// Adds the static method reflection
    /// to the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="method">
    /// <see cref="MethodInfo"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="method"/> is not valid method to be
    /// added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void AddStatic(MethodInfo method)
    {
      if (method == null)
        throw new ArgumentNullException("method");

      Validator.CheckVisible(method);

      AddFunc(FunctionFactory<T>
        .FromReflection(method, null, true));
    }

    /// <summary>
    /// Adds the static method reflection
    /// to the <see cref="FunctionCollection{T}"/>
    /// with the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="method">
    /// <see cref="MethodInfo"/> instance to add.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="method"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="method"/> is not valid method to
    /// be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void AddStatic(string name, MethodInfo method)
    {
      if (method == null)
        throw new ArgumentNullException("method");

      Validator.CheckVisible(method);

      AddFunc(name, FunctionFactory<T>
        .FromReflection(method, null, true));
    }

    /// <summary>
    /// Adds the instance method reflection to the
    /// <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="method">
    /// <see cref="MethodInfo"/> instance to add.</param>
    /// <param name="target">Instance method target object.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="method"/> is null.<br/>-or-<br/>
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="method"/> is not valid method to
    /// be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void AddInstance(MethodInfo method, object target)
    {
      if (method == null) throw new ArgumentNullException("method");
      if (target == null) throw new ArgumentNullException("target");

      Validator.CheckVisible(method);

      AddFunc(FunctionFactory<T>
        .FromReflection(method, target, true));
    }

    /// <summary>
    /// Adds the instance method reflection to the
    /// <see cref="FunctionCollection{T}"/> with
    /// the specified function name.</summary>
    /// <param name="name">Function group name.</param>
    /// <param name="method">
    /// <see cref="MethodInfo"/> instance to add.</param>
    /// <param name="target">Instance method target object.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="method"/> is null.<br/>-or-<br/>
    /// <paramref name="target"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="method"/> is not valid method to
    /// be added to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with the
    /// same name and same arguments count already exist
    /// in the collection (overload impossible).</exception>
    public void AddInstance(
      string name, MethodInfo method, object target)
    {
      if (method == null) throw new ArgumentNullException("method");
      if (target == null) throw new ArgumentNullException("target");

      Validator.CheckVisible(method);

      AddFunc(name, FunctionFactory<T>
        .FromReflection(method, target, true));
    }

    #endregion
    #region Import

    /// <summary>
    /// Imports all public static methods
    /// of the specified type that is suitable to be added
    /// into the <see cref="FunctionCollection{T}"/>.</summary>
    /// <overloads>Imports static methods
    /// of the specified type(s) that is suitable to be added
    /// into this <see cref="FunctionCollection{T}"/>.</overloads>
    /// <param name="type">Type object.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some of importing methods has the same name and
    /// the same arguments count as the function that is already
    /// in the collection (overload impossible).</exception>
    public void Import(Type type)
    {
      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      Validator.CheckVisible(type);

      InternalImport(type, Flags);
    }

#if !SILVERLIGHT

    /// <summary>
    /// Imports all static methods of the specified
    /// type that is suitable to be added into the
    /// <see cref="FunctionCollection{T}"/>.</summary>
    /// <param name="type">Type object.</param>
    /// <param name="nonpublic">Include non public
    /// member methods in the search.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Some of importing methods has the same name and the
    /// same arguments count as the function that is already
    /// in the collection (overload impossible).</exception>
    /// <remarks>Doesn't supported in Silverlight
    /// because of reflection limitations.</remarks>
    public void Import(Type type, bool nonpublic)
    {
      const BindingFlags Flags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.FlattenHierarchy;

      InternalImport(type,
        Flags | (nonpublic ? BindingFlags.NonPublic : 0));
    }

#endif

    /// <summary>
    /// Imports all static methods
    /// of the specified types that is suitable to be added
    /// into the <see cref="FunctionCollection{T}"/>.</summary>
    /// <param name="types">Array of <see cref="Type"/> objects.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="types"/> is null.</exception><br/>-or-<br/>
    /// Some Type of <paramref name="types"/> is null.
    /// <exception cref="ArgumentException">
    /// Some of importing methods has the same name and
    /// the same arguments count as the function that is already
    /// in the collection (overload impossible).</exception>
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

    /// <summary>
    /// Adds the static method reflection taken from the specified
    /// <paramref name="type"/> by the <paramref name="methodName"/>
    /// in the <see cref="FunctionCollection{T}"/> with the
    /// function name, taken from real method name.</summary>
    /// <param name="methodName">
    /// Type's method name to be imported.</param>
    /// <param name="type">Type object.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.<br/>-or-<br/>
    /// <paramref name="methodName"/>is null.</exception>
    /// <exception cref="ArgumentException">Method with
    /// <paramref name="methodName"/> is not founded.
    /// <br/>-or-<br/>Founded method is not valid to be
    /// added into this <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/> with
    /// same name and the same arguments count already exist
    /// in the collection (overload impossible).</exception>
    /// <exception cref="System.Reflection.AmbiguousMatchException">
    /// <paramref name="type"/> contains more than one methods
    /// matching the specified <paramref name="methodName"/>.
    /// </exception>
    public void Import(string methodName, Type type)
    {
      var method = FunctionFactory<T>
        .TryResolve(type, methodName, -1);

      Validator.CheckVisible(method);

      AddFunc(FunctionFactory<T>
        .FromReflection(method, null, true));
    }

    /// <summary>
    /// Adds the static method reflection taken from the specified
    /// <paramref name="type"/> by the <paramref name="methodName"/>
    /// and arguments count to the <see cref="FunctionCollection{T}"/>
    /// with the function name, taken from real method name.</summary>
    /// <param name="type">Type object.</param>
    /// <param name="methodName">Type's method name to be imported.</param>
    /// <param name="parametersCount">Method parameters count.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="type"/> is null.<br/>-or-<br/>
    /// <paramref name="methodName"/>is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="parametersCount"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">
    /// Method with <paramref name="methodName"/> is not founded.
    /// <br/>-or-<br/>Founded method is not valid to be added
    /// to the <see cref="FunctionCollection{T}"/>.
    /// <br/>-or-<br/><see cref="FunctionInfo{T}"/>
    /// with the same name and the same arguments count already
    /// exist in the collection (overload impossible).</exception>
    public void Import(
      string methodName, Type type, int parametersCount)
    {
      if (parametersCount < 0)
        throw new ArgumentOutOfRangeException("parametersCount");

      var method = FunctionFactory<T>.TryResolve(
        type, methodName, parametersCount);

      Validator.CheckVisible(method);

      AddFunc(FunctionFactory<T>
        .FromReflection(method, null, true));
    }

    #endregion

    /// <summary>
    /// Removes the function specified by name 
    /// from the <see cref="FunctionCollection{T}"/>.</summary>
    /// <overloads>Removes the function from the 
    /// <see cref="FunctionCollection{T}"/>.</overloads>
    /// <param name="name">The function name to be removed.</param>
    /// <returns><b>true</b> if function is successfully removed;
    /// otherwise, <b>false</b>.</returns>
    public bool Remove(string name)
    {
      int index = this.namesList.IndexOf(name);
      if (index >= 0)
      {
        this.namesList.RemoveAt(index);
        this.funcsList.RemoveAt(index);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Removes the function overload specified by name,
    /// arguments count and params arguments usage from the
    /// <see cref="FunctionCollection{T}"/>.</summary>
    /// <param name="name">The function name.</param>
    /// <param name="argsCount">Overload arguments count.</param>
    /// <param name="hasParamArray">Is overload has params.</param>
    /// <returns><b>true</b> if function overload is successfully
    /// removed; otherwise, <b>false</b>.</returns>
    public bool Remove(
      string name, int argsCount, bool hasParamArray)
    {
      int index = this.namesList.IndexOf(name);
      if (index >= 0 && this.funcsList[index]
         .Remove(argsCount, hasParamArray))
      {
        if (this.funcsList[index].Count == 0)
        {
          this.namesList.RemoveAt(index);
          this.funcsList.RemoveAt(index);
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// Determines whether the <see cref="FunctionCollection{T}"/>
    /// contains the specified name.</summary>
    /// <param name="name">Function name to locate in the
    /// <see cref="FunctionCollection{T}"/>.</param>
    /// <returns><b>true</b> if name is found in the list;
    /// otherwise, <b>false</b>.</returns>
    public bool ContainsName(string name)
    {
      return this.namesList.Contains(name);
    }

    /// <summary>
    /// Removes all functions
    /// from the <see cref="FunctionCollection{T}"/>.
    /// </summary>
    public void Clear()
    {
      this.namesList.Clear();
      this.funcsList.Clear();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      throw new NotSupportedException();
    }

    #endregion
    #region IEnumerable<>

    /// <summary>
    /// Returns an enumerator that iterates through the pairs
    /// in the <see cref="FunctionCollection{T}"/>.</summary>
    /// <returns>An enumerator object for pair items
    /// in the <see cref="FunctionCollection{T}"/>.</returns>
    IEnumerator<KeyValuePair<string, FunctionGroup<T>>>
      IEnumerable<KeyValuePair<string, FunctionGroup<T>>>
      .GetEnumerator()
    {
      for (int i = 0; i < this.namesList.Count; i++)
      {
        yield return
          new KeyValuePair<string, FunctionGroup<T>>(
            this.namesList[i],
            this.funcsList[i]);
      }

      yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<KeyValuePair<string,
        FunctionGroup<T>>>) this).GetEnumerator();
    }

    #endregion
    #region Internals

    void AddFunc(string name, FunctionInfo<T> function)
    {
      if (name == null)
        throw new ArgumentNullException("name");

      Debug.Assert(function != null);
      Debug.Assert(name != null);

      Validator.CheckName(name);

      int index = this.namesList.IndexOf(name);
      if (index >= 0)
      {
        if (!this.funcsList[index].Append(function))
        {
          throw OverloadImpossible(function);
        }
      }
      else
      {
        this.namesList.Add(name);
        this.funcsList.Add(new FunctionGroup<T>(function));
      }
    }

    void AddFunc(FunctionInfo<T> function)
    {
      // TryResolve name from real method name
      AddFunc(function.MethodName, function);
    }

    void InternalImport(Type type, BindingFlags flags)
    {
      if (type == null)
        throw new ArgumentNullException("type");

      foreach (var method in type.GetMethods(flags))
      {
        var func = FunctionFactory<T>
          .FromReflection(method, null, false);

        if (func != null) AddFunc(func);
      }
    }

    static ArgumentException OverloadImpossible(FunctionInfo<T> func)
    {
      return new ArgumentException(string.Format(
        Resource.errOverloadImpossible, func.ArgsString));
    }

    List<string>.Enumerator IListEnumerable.GetEnumerator()
    {
      return this.namesList.GetEnumerator();
    }

    #endregion
    #region ImportBuiltIns

    [DebuggerBrowsable(State.Never)]
    static readonly FunctionCollection<T>
      BuiltIns = ImportHelper.ResolveFunctions<T>();

    [DebuggerBrowsable(State.Never)]
    static readonly object SyncRoot = new object();

    /// <summary>
    /// Imports standart built-in functions from the <c>System.Math</c>
    /// type to the <see cref="FunctionCollection{T}"/>.</summary>
    /// <remarks>Currently this method imports this methods:<br/>
    /// Abs, Sin, Cos, Tan, Sinh, Cosh, Tanh, Acos, Asin, Atan, Atan2,
    /// Ceil, Floor, Round, Trunc (not available in CF/Silverlight),
    /// Log, Log10, Min, Max, Exp, Pow and Sqrt.</remarks>
    /// <exception cref="ArgumentException">Some of
    /// importing methods has the same name and the same
    /// arguments count as the function that is already
    /// in the collection (overload impossible).</exception>
    public void ImportBuiltIn()
    {
      if (BuiltIns != null)
      lock(SyncRoot)
      {
        foreach (var pair in BuiltIns)
        foreach (var func in pair.Value)
        {
          Add(pair.Key, func);
        }
      }
    }

    /// <summary>
    /// Set the custom collection as built-ins imports for the
    /// FunctionCollection of type <typeparamref name="T"/>.</summary>
    /// <param name="collection">Collection to set.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="collection"/> is null.</exception>
    public static void SetBuiltIns(FunctionCollection<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");

      lock(SyncRoot)
      {
        BuiltIns.Clear();

        foreach (var pair in collection)
        foreach (var func in pair.Value)
        {
          BuiltIns.Add(pair.Key, func);
        }
      }
    }

    #endregion
  }

  #region Debug View

  sealed class FunctionDebugView<T>
  {
    [DebuggerBrowsable(State.RootHidden)]
    readonly ViewItem[] items;

    public FunctionDebugView(FunctionCollection<T> list)
    {
      this.items = new ViewItem[list.Count];
      int i = 0;
      foreach (var item in list)
      {
        this.items[i].Name = item.Key;
        this.items[i].Funcs = item.Value;
        i++;
      }
    }

    [DebuggerDisplay("{Funcs.Count} functions", Name = "{Name}")]
    struct ViewItem
    {
      // ReSharper disable UnaccessedField.Local

      [DebuggerBrowsable(State.Never)]
      public string Name;

      [DebuggerBrowsable(State.RootHidden)]
      public FunctionGroup<T> Funcs;

      // ReSharper restore UnaccessedField.Local
    }
  }

  #endregion
}