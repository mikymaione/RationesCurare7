using System;
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

        public static int ObjectToInt(object o, int default_)
        {
            var i = default_;

            try
            {
                i = Convert.ToInt32(o);
            }
            catch
            {
                //error
            }

            return i;
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
            var d = ObjectToDateTime(o);

            return d.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ObjectToDateTimeStringHTML(object o)
        {
            var d = ObjectToDateTime(o);

            return d.ToString("yyyy-MM-ddTHH:mm");
        }

        public static DateTime ObjectToDateTime(object o)
        {
            var d = DateTime.Now;

            if (o is DateTime)
            {
                d = (DateTime)o;
            }
            else if (o is string)
            {
                var x = o.ToString();

                if (x.Length == 10)
                {
                    d = DateTime.ParseExact(x, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    x = x.Substring(0, 19);
                    d = DateTime.ParseExact(x, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            return d;
        }


    }
}