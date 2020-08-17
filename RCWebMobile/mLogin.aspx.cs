using System;

namespace RCWebMobile
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
                            GB.CurrentSession = new cSession(Session);
                            GB.CurrentSession.LoggedIN = true;
                            GB.CurrentSession.UserName = nome;

                            if (System.IO.File.Exists(f + ".rqd8"))
                            {
                                GB.CurrentSession.ProviderName = "System.Data.SQLite";
                                GB.CurrentSession.PathDB = f + ".rqd8";
                                GB.CurrentSession.TipoDB = RationesCurare7.DB.cDB.DataBase.SQLite;
                            }
                            else if (System.IO.File.Exists(f + ".rqd"))
                            {
                                GB.CurrentSession.ProviderName = "System.Data.OleDb";
                                GB.CurrentSession.PathDB = f + ".rqd";
                                GB.CurrentSession.TipoDB = RationesCurare7.DB.cDB.DataBase.Access;
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

            GB.SetCookie(Response, n, c);
        }

        protected void bEntra_Click(object sender, EventArgs e)
        {
            DoLogin();
        }


    }
}