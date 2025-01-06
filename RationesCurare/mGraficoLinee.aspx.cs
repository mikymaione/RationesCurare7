/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Drawing;

namespace RationesCurare
{
    public partial class mGraficoLinee : CulturePage
    {

        private static readonly Color goodColor = ColorTranslator.FromHtml("#F79E10");
        private static readonly Color badColor = ColorTranslator.FromHtml("#464453");

        protected void Page_Load(object sender, EventArgs e)
        {
            var ubuntuFont = GB.LoadUbuntuFont(this);

            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = ubuntuFont;
            Chart1.ChartAreas[0].AxisY.LabelStyle.Font = ubuntuFont;

            FindFor();
        }

        private void FindFor()
        {
            using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            using (var dt = d.EseguiSQLDataTable(cDB.Queries.Movimenti_GraficoSplineAnnuale))
            {
                Chart1.DataSource = dt;
                Chart1.DataBind();
            }
        }

        protected void Chart1_PrePaint(object sender, System.Web.UI.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            foreach (var v in e.Chart.Series[0].Points)
                if (v.YValues[0] < 0)
                    v.Color = badColor;
                else
                    v.Color = goodColor;
        }

    }
}