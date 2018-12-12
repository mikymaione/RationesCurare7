/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Globalization;

namespace ILCalc.Custom
{
  interface IParserSupport<T>
  {
    string Expression { get; }

    int BeginPos { get; }

    char DecimalDot { get; } // => DecimalSeparator
    NumberFormatInfo NumberFormat { get; }

    T ParsedValue { set; }
    bool DiscardNegate();

    Exception InvalidNumberFormat(
      string message, string literal, Exception exc);
  }

  interface ILiteralParser<T>
  {
    int TryParse(int i, IParserSupport<T> p);
  }
}
