/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Globalization;
using ILCalc.Custom;

namespace ILCalc
{
  static class LiteralParser
  {
    #region Resolve

    static readonly SupportCollection<object> Support;

    static LiteralParser()
    {
      Support = new SupportCollection<object>();

      var realParser = new RealLiteralParser();
      Support.Add<Double>(realParser);
      Support.Add<Single>(realParser);
      Support.Add<Decimal>(realParser);

      var integralParser = new IntegralLiteralParser();
      Support.Add<Int32>(integralParser);
      Support.Add<Int64>(integralParser);
    }

    public static ILiteralParser<T> Resolve<T>()
    {
      var parser = Support.Find<T>();
      if (parser == null)
        return new UnknownLiteralParser<T>();

      return (ILiteralParser<T>) parser;
    }

    public static bool IsUnknown<T>(ILiteralParser<T> parser)
    {
      return parser is UnknownLiteralParser<T>;
    }

    #endregion
    #region Parsers

    sealed class RealLiteralParser
      : ILiteralParser<Double>,
        ILiteralParser<Single>,
        ILiteralParser<Decimal>
    {
      #region ILiterlParser

      public int TryParse(int i, IParserSupport<float> p)
      {
        string expr = p.Expression;

        if ((expr[i] < '0' || expr[i] > '9')
          && expr[i] != p.DecimalDot) return -1;

        return Parse(i, p, Single.Parse);
      }

      public int TryParse(int i, IParserSupport<double> p)
      {
        string expr = p.Expression;

        if ((expr[i] < '0' || expr[i] > '9')
          && expr[i] != p.DecimalDot) return -1;

        return Parse(i, p, Double.Parse);
      }

      public int TryParse(int i, IParserSupport<decimal> p)
      {
        string expr = p.Expression;

        if ((expr[i] < '0' || expr[i] > '9')
          && expr[i] != p.DecimalDot) return -1;

        return Parse(i, p, Decimal.Parse);
      }

      #endregion
      #region CommonPart

      static int Parse<T>(int i, IParserSupport<T> p, Parser<T> parser)
      {
        var str = ScanNumber(i, p);
        if (str == null) return -1;

        bool neg = p.DiscardNegate();

        try
        {
          if (neg)
          {
            p.ParsedValue = parser(
              p.NumberFormat.NegativeSign + str,
              NumberStyles.AllowLeadingSign  |
              NumberStyles.AllowDecimalPoint |
              NumberStyles.AllowExponent,
              p.NumberFormat);
          }
          else
          {
            p.ParsedValue = parser(str,
              NumberStyles.AllowDecimalPoint |
              NumberStyles.AllowExponent,
              p.NumberFormat);
          }

          return str.Length;
        }
        catch (FormatException e)
        {
          throw p.InvalidNumberFormat(
            Resource.errNumberFormat, str, e);
        }
        catch (OverflowException e)
        {
          throw p.InvalidNumberFormat(
            Resource.errNumberOverflow, str, e);
        }
      }

      static string ScanNumber<T>(int i, IParserSupport<T> p)
      {
        string expr = p.Expression;

        // Fractal part: ==========================

        // numbers not like ".123":
        if (expr[i] != p.DecimalDot)
        {
          // skip digits and decimal point:
          for (; i < expr.Length; i++)
          {
            if (expr[i] >= '0' && expr[i] <= '9') continue;
            if (expr[i] == p.DecimalDot) i++;
            break;
          }
        }
        else
        {
          if (!IsDigit(expr, ++i)) return null;
        }

        // skip digits:
        while (IsDigit(expr, i)) i++;

        // Exponental part: =======================

        // at least 2 chars:
        if (i+1 < expr.Length)
        {
          // E character:
          char c = expr[i];
          if (c == 'e' || c == 'E')
          {
            int j = i;

            // exponetal sign:
            c = expr[++j];
            if (c == '-' || c == '+') j++;

            // exponental value:
            if (IsDigit(expr, j++))
            {
              while (IsDigit(expr, j)) j++;
              i = j;
            }
          }
        }

        return expr.Substring(p.BeginPos, i - p.BeginPos);
      }

      #endregion
    }

    sealed class IntegralLiteralParser
      : ILiteralParser<Int32>,
        ILiteralParser<Int64>
    {
      #region ILiteralParser

      public int TryParse(int i, IParserSupport<int> p)
      {
        string expr = p.Expression;

        if (expr[i] < '0' || expr[i] > '9') return -1;

        return Parse(i, p, Int32.Parse, ScanInt32Hex, ScanInt32Bin);
      }

      public int TryParse(int i, IParserSupport<long> p)
      {
        string expr = p.Expression;

        if (expr[i] < '0' || expr[i] > '9') return -1;

        return Parse(i, p, Int64.Parse, ScanInt64Hex, ScanInt64Bin);
      }

      #endregion
      #region CommonPart

      static int Parse<T>(
        int i, IParserSupport<T> p, Parser<T> parser,
        HexBinScan<T> hex, HexBinScan<T> bin)
      {
        string expr = p.Expression;

        if (expr[i] < '0' || expr[i] > '9') return -1;

        // hex/bin literal support:
        if (expr[i] == '0' && i+1 < expr.Length)
        {
          i++;
          char c = expr[i++];
          if (c == 'x' || c == 'X') return hex(i, p);
          if (c == 'b' || c == 'B') return bin(i, p);

          p.ParsedValue = default(T);
          return 1;
        }

        // skip digits and parse:
        while (IsDigit(expr, i)) i++;

        string str = Substring(i, p);
        bool neg = p.DiscardNegate();

        try
        {
          if (neg)
          {
            p.ParsedValue = parser(
              p.NumberFormat.NegativeSign + str,
              NumberStyles.AllowLeadingSign  |
              NumberStyles.AllowDecimalPoint |
              NumberStyles.AllowExponent,
              p.NumberFormat);
          }
          else
          {
            p.ParsedValue = parser(str,
              NumberStyles.AllowDecimalPoint |
              NumberStyles.AllowExponent,
              p.NumberFormat);
          }

          return str.Length;
        }
        catch (FormatException e)
        {
          throw p.InvalidNumberFormat(
            Resource.errNumberFormat, str, e);
        }
        catch (OverflowException e)
        {
          throw p.InvalidNumberFormat(
            Resource.errNumberOverflow, str, e);
        }
      }

      #endregion
      #region HexBinParser

      static int ScanInt32Hex(int i, IParserSupport<int> p)
      {
        return ScanHexBin(i, p, 8,
          (char c, ref int x) =>
        {
          int digit = HexDigit(c);
          if (digit < 0) return false;

          x *= 0x10;
          x += digit;
          return true;
        });
      }

      static int ScanInt32Bin(int i, IParserSupport<int> p)
      {
        return ScanHexBin(i, p, 32,
          (char c, ref int x) =>
        {
          if (c == '0') { x <<= 1; return true; }
          if (c == '1') { x <<= 1; x |= 1; return true; }
          return false;
        });
      }

      static int ScanInt64Hex(int i, IParserSupport<long> p)
      {
        return ScanHexBin(i, p, 16,
          (char c, ref long x) =>
        {
          int digit = HexDigit(c);
          if (digit < 0) return false;

          x *= 0x10;
          x += digit;
          return true;
        });
      }

      static int ScanInt64Bin(int i, IParserSupport<long> p)
      {
        return ScanHexBin(i, p, 64,
          (char c, ref long x) =>
        {
          if (c == '0') { x <<= 1; return true; }
          if (c == '1') { x <<= 1; x |= 1; return true; }
          return false;
        });
      }

      delegate int HexBinScan<T>(int i, IParserSupport<T> p);
      delegate bool HexBinParser<T>(char c, ref T value);

      static int ScanHexBin<T>(int i,
        IParserSupport<T> p, int maxDigits,
        HexBinParser<T> parser)
      {
        string expr = p.Expression;

        T value = default(T);
        int begin = i;
        for (; i < expr.Length; i++)
        {
          if (!parser(expr[i], ref value)) break;
        }

        int len = i - begin;
        if (len == 0)
        {
          p.ParsedValue = value;
          return 1;
        }

        if (len > maxDigits)
        {
          throw p.InvalidNumberFormat(
            Resource.errNumberOverflow,
            Substring(i, p), null);
        }

        p.ParsedValue = value;
        return len + 2;
      }

      static int HexDigit(char c)
      {
        if (c >= '0')
        {
          if (c <= '9') return c - '0';
          if (c >= 'a' && c <= 'f') return c-'\x57';
          if (c >= 'A' && c <= 'F') return c-'\x37';
        }

        return -1;
      }

      #endregion
    }

    sealed class UnknownLiteralParser<T> : ILiteralParser<T>
    {
      public int TryParse(int i, IParserSupport<T> p)
      {
        return -1;
      }
    }

    #endregion
    #region Common

    delegate T Parser<T>(
      string s, NumberStyles style, IFormatProvider format);

    static string Substring<T>(int i, IParserSupport<T> p)
    {
      return p.Expression.Substring(
        p.BeginPos, i - p.BeginPos);
    }

    static bool IsDigit(string s, int i)
    {
      return i < s.Length
        && s[i] >= '0'
        && s[i] <= '9';
    }

    #endregion
  }
}
