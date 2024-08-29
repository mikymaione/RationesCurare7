using RationesCurare7.DB;
using System;
using System.Globalization;

namespace RationesCurare
{
    public partial class mLogin : System.Web.UI.Page
    {
        private string Richiesta => GB.GetQueryString(Request, "C");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Richiesta.Equals("L", StringComparison.OrdinalIgnoreCase))
                {
                    DeleteCookieLogin(true);
                }
                else
                {
                    AutoLogin();
                }
            }
        }

        private void AutoLogin()
        {
            var auto = GB.GetCookie(Request, "AutoLogin");
            var nome = GB.GetCookie(Request, "AutoLogin_UserName");
            var psw = GB.GetCookie(Request, "AutoLogin_UserPassword");

            if (auto.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
            {
                cbMemorizza.Value = "1";
                Login_(nome, psw, true);
            }
        }

        private void DoLogin()
        {
            Login_(eUtente.Value.TrimEnd(), ePsw.Value.TrimEnd(), true);
        }

        private bool ControllaCredenziali(string nome, string psw)
        {
            var p = MapPath("App_Data");
            var f = System.IO.Path.Combine(p, nome);

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

            return false;
        }

        private void Login_(string nome, string psw, bool redirect)
        {
            if (nome != null && nome.Length > 4
                && psw != null && psw.Length > 1
                && ControllaCredenziali(nome, psw))
            {
                var p = MapPath("App_Data");
                var f = System.IO.Path.Combine(p, nome);

                GB.Instance.setCurrentSession(Session, new cSession());
                GB.Instance.getCurrentSession(Session).LoggedIN = true;
                GB.Instance.getCurrentSession(Session).UserName = nome;
                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd8";
                GB.Instance.getCurrentSession(Session).Culture = LeggiValutaInDbInfo(nome);

                if ("1".Equals(cbMemorizza.Value))
                {
                    var n = new string[] { "AutoLogin_UserName", "AutoLogin_UserPassword", "AutoLogin" };
                    var c = new string[] { nome, psw, "TRUE" };

                    GB.SetCookie(Response, n, c);
                }
                else
                {
                    DeleteCookieLogin(false);
                }

                if (redirect)
                    Response.Redirect("mMenu.aspx");
            }
            else
            {
                lErrore.Text = "Credenziali d'accesso errate!";
            }
        }

        private void DeleteCookieLogin(bool clearSession)
        {
            var n = new string[] { "AutoLogin_UserName", "AutoLogin_UserPassword", "AutoLogin" };
            var c = new string[] { "", "", "FALSE" };

            eUtente.Value = "";
            ePsw.Value = "";
            cbMemorizza.Value = "0";

            GB.SetCookie(Response, n, c);

            if (clearSession)
                GB.Instance.setCurrentSession(Session, null);
        }

        protected void bEntra_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        CultureInfo LeggiValutaInDbInfo(string email)
        {
            var maybeValuta = "";
            var maybeLanguage = "";

            var p = new System.Data.SQLite.SQLiteParameter[] {
                cDB.NewPar("Email", email)
            };

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.DBInfo_Dettaglio, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        maybeValuta = Convert.ToString(dr["Valuta"]);
                        maybeLanguage = Convert.ToString(dr["Lingua"]);
                    }

            var valuta = string.IsNullOrEmpty(maybeValuta) ? "EUR" : maybeValuta;
            var language = string.IsNullOrEmpty(maybeLanguage) ? "it-IT" : maybeLanguage;

            var culturesByValuta = GB.GetCultureByValuta(valuta);
            var culturesByLanguage = GB.GetCultureByLanguage(language);

            if (culturesByLanguage.Count > 0)
                return culturesByLanguage[0];
            else if (culturesByValuta.Count > 0)
                return culturesByValuta[0];
            else
                return CultureInfo.CurrentCulture;
        }

    }
}