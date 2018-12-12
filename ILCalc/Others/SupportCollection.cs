/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;

namespace ILCalc
{
  sealed class SupportCollection<TItem>
    where TItem : class
  {
    readonly List<Type> types;
    readonly List<TItem> items;

    public SupportCollection() : this(0) { }

    public SupportCollection(int capacity)
    {
      this.types = new List<Type>(capacity);
      this.items = new List<TItem>(capacity);
    }

    public void Add<T>(TItem item)
    {
      lock (this.types)
      {
        this.types.Add(typeof(T));
        this.items.Add(item);
      }
    }

    public TItem Find<T>()
    {
      Type type = typeof(T);

      lock (this.types)
      for (int i = 0; i < this.types.Count; i++)
      {
        if (this.types[i] == type)
        {
          return this.items[i];
        }
      }

      return null;
    }
  }
}