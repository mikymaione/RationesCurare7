/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Data;
using System.Drawing;
using System.Linq;

namespace RationesCurare
{
    public partial class mGrafico : CulturePage
    {

        private static readonly Color goodColor = ColorTranslator.FromHtml("#F79E10");
        private static readonly Color badColor = ColorTranslator.FromHtml("#464453");

        protected void Page_Load(object sender, EventArgs e)
        {
            var ubuntuFont = GB.LoadUbuntuFont(this);

            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = ubuntuFont;
            Chart1.ChartAreas[0].AxisY.LabelStyle.Font = ubuntuFont;

            try
            {
                var t = Request["T"];

                FindFor(t == "Y" || t == "M" ? t : "");
            }
            catch
            {
                //no type
            }
        }

        private void FindFor(string T)
        {
            if (!"".Equals(T))
            {
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    cDB.Queries q;

                    switch (T)
                    {
                        case "Y":
                            q = cDB.Queries.Movimenti_GraficoAnnuale;
                            bGraficoY.Attributes["class"] = "not-active";
                            bGraficoM.Attributes["class"] = "";
                            break;

                        case "M":
                        default:
                            q = cDB.Queries.Movimenti_GraficoMensile;
                            bGraficoY.Attributes["class"] = "";
                            bGraficoM.Attributes["class"] = "not-active";
                            break;
                    }

                    var dt = d.EseguiSQLDataTable(q);

                    if (dt.Rows.Count > 0)
                    {
                        var enu = dt.AsEnumerable();

                        switch (T)
                        {
                            case "Y":
                                var years = enu.Select(r => int.Parse(r[0] as string));

                                var startY = years.First();
                                var endY = years.Last();

                                for (int y = startY; y <= endY; y++)
                                    if (!years.Contains(y))
                                        dt.Rows.Add(
                                            new object[]
                                            {
                                                y, 0
                                            }
                                        );
                                break;

                            case "M":
                            default:
                                var dates = enu.Select(r => DateTime.ParseExact(r[0] as string, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture));

                                var startD = dates.First();
                                var endD = dates.Last();

                                for (var date = startD; date <= endD; date = date.AddMonths(1))
                                    if (!dates.Contains(date))
                                        dt.Rows.Add(
                                            new object[]
                                            {
                                                date.ToString("yyyy/MM"), 0
                                            }
                                        );
                                break;
                        }
                    }

                    dt.DefaultView.Sort = "Mese asc";

                    Chart1.DataSource = dt;
                    Chart1.DataBind();
                }
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