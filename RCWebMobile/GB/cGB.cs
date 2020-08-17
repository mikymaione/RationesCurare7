using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace RationesCurare7
{
    public static class cGB
    {
        public static string UserName = "Michele";
        public static System.Drawing.Pen myPenLeft = new System.Drawing.Pen(System.Drawing.Color.FromArgb(137, 140, 149));
        public static System.Drawing.Pen myPenBottom = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 160, 160));

        public struct Time
        {
            public int Ora, Minuto;
        }

        public static string CopyrightHolder
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static bool StringInArray(string s, string[] a)
        {
            if (a != null)
                if (a.Length > 0)
                    for (int i = 0; i < a.Length; i++)
                        if (s.Equals(a[i], StringComparison.OrdinalIgnoreCase))
                            return true;

            return false;
        }

        public static bool String_For_Time_IsValid(string s)
        {
            bool b = false;

            try
            {
                Time t = new Time();

                t.Ora = Convert.ToInt32(s.Substring(0, 2));
                t.Minuto = Convert.ToInt32(s.Substring(3, 2));

                b = true;
            }
            catch
            {
                //error
            }

            return b;
        }

        public static Time StringToTime(string s)
        {
            Time t = new Time();

            try
            {
                t.Ora = Convert.ToInt32(s.Substring(0, 2));
                t.Minuto = Convert.ToInt32(s.Substring(3, 2));
            }
            catch
            {
                //error
            }

            return t;
        }

        public static string TimeToString(DateTime t)
        {
            return TimeToString(new Time()
            {
                Ora = t.Hour,
                Minuto = t.Minute
            });
        }

        public static string TimeToString(Time t)
        {
            string h = "";
            string m = "";

            if (t.Ora < 10)
                h = "0";

            h += t.Ora;

            if (t.Minuto < 10)
                m = "0";

            m += t.Minuto;

            return (h + "." + m);
        }

        public static DateTime ObjectToDateTime(object o)
        {
            DateTime d = default(DateTime);

            try
            {
                d = (DateTime)o;
            }
            catch
            {
                //empty
            }

            return d;
        }

        public static double ObjectToMoney(object o)
        {
            return ObjectToMoney(o, 0);
        }

        public static double ObjectToMoney(object o, double default_)
        {
            double d = default_;

            try
            {
                d = (double)o;
                d = d * 100;

                int i = (int)d;

                d = i;
                d = d / 100;
            }
            catch
            {
                //error                
            }

            return d;
        }

        public static int ObjectToInt(object o, int default_)
        {
            int i = default_;

            try
            {
                i = (int)o;
            }
            catch
            {
                //error
            }

            return i;
        }

        public static string HolidayName(DateTime data)
        {
            string z = "";
            int y = data.Year;
            int m = data.Month;
            int d = data.Day;

            if (m == 1 & d == 1)
                z = "Capodanno";
            if (m == 1 & d == 6)
                z = "Befana";
            if (m == 4 & d == 25)
                z = "Festa della liberazione";
            if (m == 5 & d == 1)
                z = "Festa del lavoro";
            if (m == 6 & d == 2)
                z = "Nascita della Repubblica Italiana";
            if (m == 8 & d == 15)
                z = "Ferragosto";
            if (m == 11 & d == 1)
                z = "Ognissanti";
            if (m == 12 & d == 8)
                z = "Immacolata";
            if (m == 12 & d == 25)
                z = "Natale";
            if (m == 12 & d == 26)
                z = "Santo Stefano";
            if (data.Equals(EasterDate(y).AddDays(1)))
                z = "Pasqua";

            return z;
        }

        public static bool IsHoliday(DateTime data)
        {
            int y = data.Year;
            int m = data.Month;
            int d = data.Day;

            if (
                m == 1 & d == 1 |
                m == 1 & d == 6 |
                m == 4 & d == 25 |
                m == 5 & d == 1 |
                m == 6 & d == 2 |
                m == 8 & d == 15 |
                m == 11 & d == 1 |
                m == 12 & d == 8 |
                m == 12 & d == 25 |
                m == 12 & d == 26 |
                data.Equals(EasterDate(y).AddDays(1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static DateTime EasterDate(int year = 0)
        {
            DateTime dt = DateTime.Now;
            int G = 0;
            int C = 0;
            int H = 0;
            int i = 0;
            int j = 0;
            int L = 0;
            int m = 0;
            int d = 0;

            if (year == 0)
                year = DateTime.Today.Year;

            if (dt.Year != year)
            {
                G = year % 19;
                C = year / 100;
                H = ((C - (C / 4) - ((8 * C + 13) / 25) + (19 * G) + 15) % 30);
                i = H - ((H / 28) * (1 - (H / 28) * (29 / (H + 1)) * ((21 - G) / 11)));
                j = ((year + (year / 4) + i + 2 - C + (C / 4)) % 7);
                L = i - j;

                m = 3 + ((L + 40) / 44);
                d = L + 28 - (31 * (m / 4));
                dt = new DateTime(year, m, d);
            }

            return dt;
        }

        public static string DateToSQLite(DateTime d)
        {
            //yyyy-MM-dd HH:mm:ss.fff
            string h = "";

            h += d.Year + "-" + (d.Month < 10 ? "0" : "") + d.Month + "-" + (d.Day < 10 ? "0" : "") + d.Day + " ";
            h += (d.Hour < 10 ? "0" : "") + d.Hour + ":" + (d.Minute < 10 ? "0" : "") + d.Minute + ":" + (d.Second < 10 ? "0" : "") + d.Second + ".000";

            return h;
        }


    }
}