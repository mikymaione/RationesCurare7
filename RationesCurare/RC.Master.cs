/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare.DB.DataWrapper;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace RationesCurare
{
    public partial class RC : MasterPage
    {

        private string CurrentPage => Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "");

        protected void Page_Load(object sender, EventArgs e)
        {
            var s = GB.Instance.getCurrentSession(Session);

            if (s == null)
            {
                Response.Redirect("index.aspx");
            }
            else if (!s.LoggedIN)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                DisableNavs();

                var p = new PeriodiciService();
                p.ControllaPeriodici(Session);
            }
        }

        private void DisableNavs()
        {
            var me = "nav_" + CurrentPage;

            var navs = new HtmlAnchor[] {
                nav_mMenu,
                nav_mCasse,
                nav_mSaldo,
                nav_mPeriodici,
                nav_mGrafico,
                nav_mGraficoTorta,
                nav_mGraficoLinee,
                nav_mLogout
            };

            foreach (var nav in navs)
                nav.Attributes["class"] = me.Equals(nav.ID)
                    ? "not-active"
                    : "";
        }

    }
}