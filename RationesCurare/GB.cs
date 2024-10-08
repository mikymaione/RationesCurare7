﻿/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Globalization;

namespace RationesCurare
{
    public class GB
    {

        private GB()
        {
            // singleton pattern
        }

        public static GB Instance { get; protected set; } = new GB();

        public static string DBW =>
           System.IO.Path.Combine(HttpContext.Current.Request.MapPath("DB"), "DBW");

        public static string GetQueryString(HttpRequest request, string name) =>
            request.QueryString.AllKeys.Contains(name) ? request.QueryString[name] : "";

        public cSession getCurrentSession(HttpSessionState session) =>
            (cSession)session["CurrentSession"];

        public void setCurrentSession(HttpSessionState session, cSession _cSession) =>
            session["CurrentSession"] = _cSession;

        public static string getDBPathByName(string nome)
        {
            var p = HttpContext.Current.Request.MapPath("App_Data");
            var f = System.IO.Path.Combine(p, nome);

            return f + ".rqd8";
        }

        public static bool ControllaCredenziali(string App_Data, string nome, string psw)
        {
            if (nome != null && nome.Length > 4 && psw != null && psw.Length > 1)
            {
                var f = System.IO.Path.Combine(App_Data, nome);

                if (System.IO.File.Exists(f + ".rqd8"))
                    if (System.IO.File.Exists(f + ".psw"))
                        try
                        {
                            using (var sr = new System.IO.StreamReader(f + ".psw"))
                            {
                                var psw_ = sr.ReadLine();

                                if (psw.Equals(psw_, StringComparison.OrdinalIgnoreCase))
                                    return true;
                            }
                        }
                        catch
                        {
                            //cannot access file
                        }
            }

            return false;
        }

        public static bool SetCookie(HttpResponse Response, string[] cookies, string[] values)
        {
            var b = false;

            try
            {
                var myCookie = new HttpCookie("RCWEB")
                {
                    Expires = DateTime.Now.AddDays(7)
                };

                for (var i = 0; i < cookies.Length; i++)
                    myCookie[cookies[i]] = values[i];

                Response.Cookies.Add(myCookie);

                b = true;
            }
            catch
            {
                //error   
            }

            return b;
        }

        public static string GetCookie(HttpRequest Request, string cookie)
        {
            var c = "";

            try
            {
                c = Request.Cookies["RCWEB"][cookie];
            }
            catch
            {
                //error
            }

            return c;
        }

        public static bool ObjectToBool(object o)
        {
            var i = false;

            try
            {
                i = (bool)o;
            }
            catch
            {
                //error
            }

            return i;
        }

        public static long ObjectToInt(string o, long default_)
        {
            var i = default_;

            try
            {
                i = long.Parse(o);
            }
            catch
            {
                //error
            }

            return i;
        }

        public static long ObjectToInt(object o, long default_)
        {
            var i = default_;

            try
            {
                i = (long)o;
            }
            catch
            {
                //error
            }

            return i;
        }

        public static string ObjectToMoneyStringNoDecimal(object o) =>
           ObjectToMoneyString(o, "{0:C0}");

        public static string ObjectToMoneyString(object o) =>
            ObjectToMoneyString(o, "{0:C}");

        private static string ObjectToMoneyString(object o, string format) =>
          string.Format(format, ObjectToMoney(o));

        public static double ObjectToMoney(object o) =>
            ObjectToMoney(o, 0);

        public static double ObjectToMoney(object o, double default_)
        {
            var d = default_;

            try
            {
                d = (double)o;
            }
            catch (InvalidCastException)
            {
                //error cast
                d = ObjectToDouble(o, default_);
            }
            catch (Exception)
            {
                //error generic                           
            }

            return d;
        }

        public static int HTMLIntToInt(string o, int default_)
        {
            try
            {
                return int.Parse(o.Replace(".", ","));
            }
            catch
            {
                return default_;
            }
        }

        public static double HTMLDoubleToDouble(string o)
        {
            try
            {
                return double.Parse(o.Replace(".", ","));
            }
            catch
            {
                return 0;
            }
        }

        public static string ObjectToHTMLDouble(object o, double default_)
        {
            var d = ObjectToDouble(o, default_);
            var s = d.ToString(CultureInfo.InvariantCulture);

            return s;
        }

        public static double ObjectToDouble(object o, double default_)
        {
            var i = default_;

            try
            {
                i = Convert.ToDouble(o);
            }
            catch
            {
                //error
            }

            return i;
        }

        public static object ValueToDBNULL(bool PutNull, Func<object> ElseValue) =>
            PutNull ? DBNull.Value : ElseValue();

        public static DateTime DateEndOfMonth(DateTime d) =>
            DateStartOfMonth(d).AddMonths(1).AddSeconds(-1);

        public static DateTime DateStartOfMonth(DateTime d) =>
            new DateTime(d.Year, d.Month, 1);

        public static DateTime DateStartOfTheDay(DateTime d) =>
            new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);

        public static DateTime DateEndOfTheDay(DateTime d) =>
            new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);

        public static DateTime DateToOnlyDate(DateTime d) =>
            new DateTime(d.Year, d.Month, d.Day);

        public static string DateTimeToSQLiteTimeStamp(DateTime dateTime) =>
            dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        public static DateTime StringToDate(string o, DateTime default_)
        {
            var i = default_;

            try
            {
                i = DateTime.Parse(o);
                // i = DateTime.ParseExact(o, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                //error
            }

            return i;
        }

        public static DateTime StringToMonth(string o, DateTime default_)
        {
            var i = default_;

            try
            {
                i = DateTime.ParseExact(o, "yyyy-MM", CultureInfo.InvariantCulture);
            }
            catch
            {
                //error
            }

            return i;
        }

        public static string ObjectToDateTimeString(object o)
        {
            var d = ObjectToDateTime(o, DateTime.Now);

            return d?.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ObjectToDateStringHTML(object o)
        {
            var d = ObjectToDateTime(o, DateTime.Now);

            return d?.ToString("yyyy-MM-dd");
        }

        public static string ObjectToDateTimeStringHTML(object o)
        {
            var d = ObjectToDateTime(o, DateTime.Now);

            return d?.ToString("yyyy-MM-ddTHH:mm");
        }

        public static DateTime StringHTMLToDate(string o) =>
            DateTime.ParseExact(o, "yyyy-MM-dd", null);

        public static DateTime StringHTMLToDateTime(string o) =>
            DateTime.ParseExact(o, "yyyy-MM-ddTHH:mm", null);

        public static DateTime? ObjectToDateTime(object o, DateTime? default_)
        {
            if (o is DateTime dt)
            {
                return dt;
            }
            else if (o is string x)
            {
                if (x.Length == 10)
                {
                    return DateTime.ParseExact(x, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    x = x.Substring(0, 19);
                    return DateTime.ParseExact(x, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (default_.HasValue)
            {
                return (DateTime)default_;
            }
            else
            {
                return null;
            }
        }

        public static string ComboBoxItemsByValue(DropDownList d, string value)
        {
            foreach (ListItem li in d.Items)
                if (string.Compare(li.Value, value, true) == 0)
                    return li.Value;

            return null;
        }

        private static Font UbuntuMono;
        public static Font LoadUbuntuFont(System.Web.UI.Page page)
        {
            if (UbuntuMono == null)
            {
                var css = page.MapPath("css");
                var css_rc = System.IO.Path.Combine(css, "rc");
                var font_file = System.IO.Path.Combine(css_rc, "UbuntuMono-Regular.ttf");

                var collection = new PrivateFontCollection();
                collection.AddFontFile(font_file);

                var fontFamily = new FontFamily("Ubuntu Mono", collection);
                UbuntuMono = new Font(fontFamily, 14);
            }

            return UbuntuMono;
        }

        public static string GetColor(object o)
        {
            var d = ObjectToDouble(o, 0);

            if (d > -0.001 && d < 0.001)
                return "moneyNeutro";
            else if (d < 0)
                return "moneyBad";
            else
                return "moneyGood";
        }

    }
}