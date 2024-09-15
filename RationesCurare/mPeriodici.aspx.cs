/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mPeriodici : CulturePage
    {

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            Response.Redirect($"mPeriodico.aspx?ID=-1");
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var id = r.ItemArray[0];

            Response.Redirect($"mPeriodico.aspx?ID={id}");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.Instance.getCurrentSession(Session) != null)
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Periodici_Ricerca);
                    GridView1.DataBind();
                }
        }

    }
}