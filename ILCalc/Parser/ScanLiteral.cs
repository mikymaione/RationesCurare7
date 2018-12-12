/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using ILCalc.Custom;

namespace ILCalc
{
  sealed partial class Parser<T> : IParserSupport<T>
  {
    #region CultureSupport

    char dotSymbol;
    char sepSymbol;

    NumberFormatInfo numFormat;

    public void InitCulture()
    {
      CultureInfo culture = Context.Culture;
      if (culture == null)
      {
        this.dotSymbol = '.';
        this.sepSymbol = ',';
        this.numFormat = new NumberFormatInfo();
      }
      else
      {
        try
        {
          this.dotSymbol = culture.NumberFormat.NumberDecimalSeparator[0];
          this.sepSymbol = culture.TextInfo.ListSeparator[0];
        }
        catch (IndexOutOfRangeException)
        {
          throw new ArgumentException(Resource.errCultureExtract);
        }

        this.numFormat = culture.NumberFormat;
      }
    }

    #endregion
    #region IParserSupport

    public int BeginPos { get { return this.curPos; } }
    
    public string Expression { get { return this.expr; } }
    
    public T ParsedValue { set { this.value = value; } }

    public char DecimalDot { get { return this.dotSymbol; } }

    public NumberFormatInfo NumberFormat
    {
      get { return this.numFormat; }
    }

    public Exception InvalidNumberFormat(
      string message, string literal, Exception exc)
    {
      var msg = new StringBuilder(message)
        .Append(" \"").Append(literal)
        .Append("\".").ToString();

      return new SyntaxException(
        msg, this.expr, this.curPos,
        literal.Length, exc);
    }

    public bool DiscardNegate()
    {
      Debug.Assert(this.curStack != null);

      if (this.curStack.Count == 0 ||
          this.curStack.Peek() != Code.Neg)
      {
        return false;
      }

      bool neg = false;
      while (this.curStack.Count > 0 &&
             this.curStack.Peek() == Code.Neg)
      {
        neg = !neg;
        this.curStack.Pop();
      }

      return neg;
    }

    #endregion
  }
}