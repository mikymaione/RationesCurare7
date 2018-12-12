/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.Reflection.Emit;

namespace ILCalc.Custom
{
  interface ICompiler<T>
  {
    void LoadConst(ILGenerator il, T value);
    void Operation(ILGenerator il, int op);
    void CheckedOp(ILGenerator il, int op);
    void LoadElem(ILGenerator il);
    void SaveElem(ILGenerator il);
  }
}
