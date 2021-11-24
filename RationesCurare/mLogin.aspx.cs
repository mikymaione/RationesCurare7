using System;

namespace RationesCurare
{
    public partial class mLogin : System.Web.UI.Page
    {
        private string Richiesta
        {
            get
            {
                var t = "";

                try
                {
                    t = Request.QueryString["C"];
                }
                catch
                {
                    //error                    
                }

                if (t == null)
                    t = "";

                return t;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Richiesta.Equals("L", StringComparison.OrdinalIgnoreCase))
            {
                DeleteCookieLogin();
            }
            else
            {
                if (!IsPostBack)
                    AutoLogin();
            }
        }

        private void AutoLogin()
        {
            var auto = GB.GetCookie(Request, "AutoLogin");
            var nome = GB.GetCookie(Request, "AutoLogin_UserName");
            var psw = GB.GetCookie(Request, "AutoLogin_UserPassword");

            if (auto.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
            {
                cbMemorizza.Checked = true;
                Login(nome, psw);
            }
        }

        private void DoLogin()
        {
            var nome = eUtente.Text;
            var psw = ePsw.Text;

            Login(nome, psw);
        }

        private void Login(string nome, string psw)
        {
            if (nome != null)
                if (nome.Length > 2)
                    if (psw != null)
                        Login_(nome, psw);
        }

        private void Login_(string nome, string psw)
        {
            var ok = false;
            var psw_ = "";
            var p = MapPath("App_Data");
            var f = System.IO.Path.Combine(p, nome);

            if (System.IO.File.Exists(f + ".rqd") || System.IO.File.Exists(f + ".rqd8"))
                if (System.IO.File.Exists(f + ".psw"))
                    try
                    {
                        using (var sr = new System.IO.StreamReader(f + ".psw"))
                        {
                            psw_ = sr.ReadLine();
                            sr.Close();
                        }

                        if (psw.Equals(psw_, StringComparison.OrdinalIgnoreCase))
                        {
                            GB.Instance.setCurrentSession(Session, new cSession());
                            GB.Instance.getCurrentSession(Session).LoggedIN = true;
                            GB.Instance.getCurrentSession(Session).UserName = nome;

                            if (System.IO.File.Exists(f + ".rqd8"))
                            {
                                GB.Instance.getCurrentSession(Session).ProviderName = "System.Data.SQLite";
                                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd8";
                                GB.Instance.getCurrentSession(Session).TipoDB = RationesCurare7.DB.cDB.DataBase.SQLite;
                            }
                            else if (System.IO.File.Exists(f + ".rqd"))
                            {
                                GB.Instance.getCurrentSession(Session).ProviderName = "System.Data.OleDb";
                                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd";
                                GB.Instance.getCurrentSession(Session).TipoDB = RationesCurare7.DB.cDB.DataBase.Access;
                            }

                            if (cbMemorizza.Checked)
                            {
                                var n = new string[] { "AutoLogin_UserName", "AutoLogin_UserPassword", "AutoLogin" };
                                var c = new string[] { nome, psw, "TRUE" };

                                GB.SetCookie(Response, n, c);
                            }
                            else
                            {
                                DeleteCookieLogin();
                            }

                            ok = true;
                            lErrore.Text = "";

                            Response.Redirect("mMenu.aspx");
                        }
                    }
                    catch
                    {
                        //cannot access file
                    }

            if (!ok)
                lErrore.Text = "Credenziali d'accesso errate!";
        }

        private void DeleteCookieLogin()
        {
            var n = new string[] { "AutoLogin_UserName", "AutoLogin_UserPassword", "AutoLogin" };
            var c = new string[] { "", "", "FALSE" };

            eUtente.Text = "";
            ePsw.Text = "";
            cbMemorizza.Checked = false;

            GB.SetCookie(Response, n, c);
            GB.Instance.setCurrentSession(Session, null);
        }

        protected void bEntra_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

    }
}