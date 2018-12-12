/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

namespace ILCalc
{
  enum Code
  {
    // operators:
    Sub = 0, Add = 1,
    Mul = 2, Div = 3,
    Mod = 4, Pow = 5,
    Neg = 6,

    // elements:
    Number    = 8,
    Argument  = 9,
    Function  = 10,
    Separator = 11,
    ParamCall = 12,
    BeginCall = 13,

    // for Interpret:
    Delegate0 = 16,
    Delegate1 = 17,
    Delegate2 = 18,

    Return = int.MaxValue,
  }

  static class CodeHelper
  {
    public static bool IsOp(Code code)
    {
      return code < Code.Number;
    }
  }
}