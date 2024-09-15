/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2024 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare
{
    public partial class index : System.Web.UI.Page
    {

        private string Richiesta => GB.GetQueryString(Request, "C");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                Login_(nome, psw, true);
            }
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

                if (redirect)
                    Response.Redirect("mMenu.aspx");
            }
        }

        protected void bEntra_Click(object sender, EventArgs e)
        {
            Response.Redirect("mLogin.aspx");
        }

    }
}
