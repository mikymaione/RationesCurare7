/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

namespace ILCalc.Custom
{
  interface IArithmetic<T>
  {
    T Zero { get; }
    T One  { get; }

    T Neg(T x);
    T Add(T x, T y); T Sub(T x, T y);
    T Mul(T x, T y); T Div(T x, T y);
    T Mod(T x, T y); T Pow(T x, T y);

    int? IsIntergal(T value);
  }

  interface IRangeSupport<T>
  {
    T StepFromCount(ValueRange<T> r, int count);
    int GetCount(ValueRange<T> r);
    ValueRangeValidness Validate(ValueRange<T> r);
  }
}