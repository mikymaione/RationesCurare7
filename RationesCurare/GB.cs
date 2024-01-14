using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RationesCurare
{
    public class GB
    {
        private GB()
        {
            // singleton pattern
        }

        public static string DBW =>
           System.IO.Path.Combine(HttpContext.Current.Request.MapPath("DB"), "DBW");

        public static GB Instance { get; protected set; } = new GB();

        public static string GetQueryString(HttpRequest request, string name) =>
            request.QueryString.AllKeys.Contains(name) ? request.QueryString[name] : "";

        public cSession getCurrentSession(HttpSessionState session)
        {
            return (cSession)session["CurrentSession"];
        }
        public void setCurrentSession(HttpSessionState session, cSession _cSession)
        {
            session["CurrentSession"] = _cSession;
        }

        public static string getDBPathByName(string nome)
        {
            var p = HttpContext.Current.Request.MapPath("App_Data");
            var f = System.IO.Path.Combine(p, nome);

            return f + ".rqd8";
        }

        public static bool SetCookie(HttpResponse Response, string[] cookies, string[] values)
        {
            var b = false;

            try
            {
                var myCookie = new HttpCookie("RCWEB");
                myCookie.Expires = DateTime.Now.AddDays(7);

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

        public static double HTMLDoubleToDouble(string o)
        {
            return double.Parse(o.Replace(".", ","));
        }

        public static string ObjectToHTMLDouble(object o, double default_)
        {
            var d = ObjectToDouble(o, default_);
            var s = d.ToString(System.Globalization.CultureInfo.InvariantCulture);

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

        public static object ValueToDBNULL(bool PutNull, object ElseValue) => PutNull ? DBNull.Value : ElseValue;

        public static DateTime DateToOnlyDate(DateTime d) => new DateTime(d.Year, d.Month, d.Day);

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

        public static string ObjectToDateTimeString(object o)
        {
            var d = ObjectToDateTime(o, DateTime.Now);

            return d?.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ObjectToDateTimeStringHTML(object o)
        {
            var d = ObjectToDateTime(o, DateTime.Now);

            return d?.ToString("yyyy-MM-ddTHH:mm");
        }

        public static DateTime StringHTMLToDateTime(string o)
        {
            return DateTime.ParseExact(o, "yyyy-MM-ddTHH:mm", null);
        }

        public static DateTime? ObjectToDateTime(object o, DateTime? default_)
        {
            if (o is DateTime)
            {
                return (DateTime)o;
            }
            else if (o is string)
            {
                var x = o.ToString();

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
                return (DateTime)default_;
            else
                return null;
        }


    }
}