using RationesCurare7.DB;
using System;

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
            Login_(eUtente.Value, ePsw.Value, true);
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

        protected void bRegistrati_Click(object sender, EventArgs e)
        {
            if (divNickName.Visible)
            {
                var utente = eNickName.Value;
                var nome = eUtente.Value;
                var psw = ePsw.Value;

                if (nome != null && nome.Length > 4 && psw != null && psw.Length > 1)
                {
                    var p = MapPath("App_Data");
                    var f = System.IO.Path.Combine(p, nome);

                    if (System.IO.File.Exists(f + ".rqd8") || System.IO.File.Exists(f + ".psw"))
                    {
                        lErrore.Text = "Utente già esistente!";
                    }
                    else
                    {
                        var standard = System.IO.Path.Combine(p, "standard.rqd8");

                        try
                        {
                            System.IO.File.Copy(standard, f + ".rqd8");

                            using (var sw = new System.IO.StreamWriter(f + ".psw"))
                                sw.WriteLine(psw);

                            CreaUtenteInDbInfo(nome, utente, psw);
                        }
                        catch (Exception ex)
                        {
                            lErrore.Text = ex.Message;
                        }
                    }
                }
                else
                {
                    lErrore.Text = "Email o password non valide!";
                }
            }
            else
            {
                divNickName.Visible = true;
            }
        }

        void CreaUtenteInDbInfo(string email, string nome, string psw)
        {
            try
            {
                Login_(email, psw, false);

                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var UltimaModifica = DateTime.Now.AddYears(-15);
                    var UltimoAggiornamentoDB = DateTime.Now.AddYears(-15);

                    var param = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", nome, System.Data.DbType.String),
                        cDB.NewPar("Psw", psw, System.Data.DbType.String),
                        cDB.NewPar("Email", email, System.Data.DbType.String),
                        cDB.NewPar("SincronizzaDB", true, System.Data.DbType.Boolean),
                        cDB.NewPar("UltimaModifica", UltimaModifica, System.Data.DbType.DateTime),
                        cDB.NewPar("UltimoAggiornamentoDB", UltimoAggiornamentoDB, System.Data.DbType.DateTime),
                        cDB.NewPar("Valuta", "EUR", System.Data.DbType.String),
                    };

                    db.EseguiSQLNoQuery(cDB.Queries.DBInfo_Inserisci, param);

                    Response.Redirect("mMenu.aspx");
                }
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Errore: {ex.Message}";
            }
        }

    }
}