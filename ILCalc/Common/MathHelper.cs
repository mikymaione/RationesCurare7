/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;

namespace ILCalc.Custom
{
  #if SILVERLIGHT
  /// <summary>
  /// Static class for helper methods.<br/>
  /// This class only public in Silverlight build.
  /// </summary>
  public
#endif
  static class MathHelper
  {
    /// <summary>
    /// Returns a specified number raised to the specified power.</summary>
    /// <param name="x">A 32-bit signed integer to be raised to a power.</param>
    /// <param name="y">A 32-bit signed integer that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public static int Pow(int x, int y)
    {
      if (y < 0) return 0;

      int res = 1;
      while (y != 0)
      {
        if ((y & 1) == 1) res *= x;
        y >>= 1;
        x *= x;
      }

      return res;
    }

    /// <summary>
    /// Returns a specified number raised to the specified power.</summary>
    /// <param name="x">A 64-bit signed integer to be raised to a power.</param>
    /// <param name="y">A 64-bit signed integer that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public static long Pow(long x, long y)
    {
      if (y < 0) return 0;

      long res = 1;
      while (y != 0)
      {
        if ((y & 1) == 1) res *= x;
        y >>= 1;
        x *= x;
      }

      return res;
    }

    /// <summary>
    /// Returns a specified number raised to the specified power.</summary>
    /// <param name="x">A single-precision floating-
    /// point number to be raised to a power.</param>
    /// <param name="y">A single-precision floating-
    /// point number that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public static float Pow(float x, float y)
    {
      return (float) Math.Pow(x, y);
    }

    /// <summary>
    /// Returns a specified number raised to the specified power.</summary>
    /// <param name="x">A <see cref="Decimal"/> number to be raised to a power.</param>
    /// <param name="y">A <see cref="Decimal"/> number that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public static decimal Pow(decimal x, decimal y)
    {
      if (y == Decimal.Floor(y) && Math.Abs(y) < 1000m)
      {
        decimal res = 1m;
        bool sign = y < 0m;
        y = Math.Abs(y);

        while (y > 0)
        {
          res *= x;
          y--;
        }

        return sign ? 1m / res : res;
      }
      
      return (decimal)
        Math.Pow((double) x, (double) y);
    }

    /// <summary>
    /// Returns a specified number raised to the specified
    /// power with overflow checking.</summary>
    /// <param name="x">A 32-bit signed integer to be raised to a power.</param>
    /// <param name="y">A 32-bit signed integer that specifies a power.</param>
    /// <exception cref="OverflowException">
    /// Arithmetic operations results in an overflow.</exception>
    /// <returns>The number x raised to the power y.</returns>
    public static int PowChecked(int x, int y)
    {
      if (y < 0) return 0;

      int res = 1;
      while (y != 0)
      {
        if ((y & 1) == 1)
          checked { res *= x; }

        y >>= 1;
        if (y != 0)
          checked { x *= x; }
      }

      return res;
    }

    /// <summary>
    /// Returns a specified number raised to the specified power.</summary>
    /// <param name="x">A 64-bit signed integer to be raised to a power.</param>
    /// <param name="y">A 64-bit signed integer that specifies a power.</param>
    /// <exception cref="OverflowException">
    /// Arithmetic operations results in an overflow.</exception>
    /// <returns>The number x raised to the power y.</returns>
    public static long PowChecked(long x, long y)
    {
      if (y < 0) return 0;

      long res = 1;
      while (y != 0)
      {
        if ((y & 1) == 1)
          checked { res *= x; }

        y >>= 1;
        if (y != 0)
          checked { x *= x; }
      }

      return res;
    }

    /// <summary>
    /// Returns a specified number raised to the specified
    /// power with overflow checking.</summary>
    /// <param name="x">A single-precision floating-
    /// point number to be raised to a power.</param>
    /// <param name="y">A single-precision floating-
    /// point number that specifies a power.</param>
    /// <exception cref="OverflowException">
    /// Arithmetic operations results in an overflow.</exception>
    /// <returns>The number x raised to the power y.</returns>
    public static float PowChecked(float x, float y)
    {
      var temp = (float) Math.Pow(x, y);
      if (float.IsInfinity(temp) || float.IsNaN(temp))
      {
        throw new NotFiniteNumberException(temp.ToString());
      }

      return temp;
    }
  }
}
