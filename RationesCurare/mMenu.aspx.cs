﻿/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Security.Policy;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mMenu : CulturePage
    {

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            Response.Redirect($"mMovimento.aspx?ID=-1");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.Instance.getCurrentSession(Session) != null)
            {
                Title = "RationesCurare - " + GB.Instance.getCurrentSession(Session).UserName;

                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_SaldoPerCassa);
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var tipo = DataBinder.Eval(e.Row.DataItem, "Tipo").ToString();
                var url = $"mSaldo.aspx?T={tipo}";

                e.Row.Attributes["onmousedown"] = $@"
                    if (event.button === 0) {{
                        window.location.href = '{url}';
                    }} else if (event.button === 1) {{
                        window.open('{url}', '_blank');
                    }}
                ";
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_Saldo))
                    if (dr.HasRows)
                    {
                        var lbl = (Label)e.Row.FindControl("lblTotal");

                        while (dr.Read())
                            lbl.Text = GB.ObjectToMoneyString(dr["Saldo"]);
                    }
            }
        }

    }
}