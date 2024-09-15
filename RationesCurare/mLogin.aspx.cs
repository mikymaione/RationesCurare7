/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
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

        private void Login_(string nome, string psw, bool redirect)
        {
            var App_Data = MapPath("App_Data");

            if (GB.ControllaCredenziali(App_Data, nome, psw))
            {
                var f = System.IO.Path.Combine(App_Data, nome);

                GB.Instance.setCurrentSession(Session, new cSession());
                GB.Instance.getCurrentSession(Session).LoggedIN = true;
                GB.Instance.getCurrentSession(Session).UserName = nome;
                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd8";
                GB.Instance.getCurrentSession(Session).Culture = GestioneValute.LeggiValutaInDbInfo(Session, nome);

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

    }
}