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

        private enum GraphType
        {
            month, year
        }

        private GraphType CurrentGraphType
        {
            get
            {
                return (GraphType)(ViewState["CurrentGraphType"] ?? GraphType.year);
            }
            set
            {
                ViewState["CurrentGraphType"] = value;
            }
        }

        private DateTime CurrentData
        {
            get
            {
                var da = GB.DateStartOfMonth(GB.StringToDate(idDataDa.Value, DateTime.Now));
                var a = GB.DateStartOfMonth(GB.StringToDate(idDataA.Value, DateTime.Now));

                var m = DateTime.Compare(da, a) > 0 ? da : a;

                return m;
            }
            set
            {
                var inizio = GB.DateStartOfMonth(value);
                var fine = GB.DateEndOfMonth(inizio);

                idDataDa.Value = GB.ObjectToDateStringHTML(inizio);
                idDataA.Value = GB.ObjectToDateStringHTML(fine);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var ubuntuFont = GB.LoadUbuntuFont(this);

            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = ubuntuFont;
            Chart1.ChartAreas[0].AxisY.LabelStyle.Font = ubuntuFont;

            if (!IsPostBack)
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                using (var dr = d.EseguiSQLDataReader(cDB.Queries.Movimenti_Data))
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            idDataDa.Value = GB.ObjectToDateStringHTML(dr.GetDateTime(0));
                            idDataA.Value = GB.ObjectToDateStringHTML(dr.GetDateTime(1));
                        }

            FindFor();
        }

        private void FindFor()
        {
            using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var inizio = GB.DateStartOfMonth(GB.StringToDate(idDataDa.Value, DateTime.Now));
                var fine = GB.DateEndOfMonth(GB.StringToDate(idDataA.Value, DateTime.Now));

                var p = new System.Data.Common.DbParameter[] {
                    cDB.NewPar("dataDa", inizio),
                    cDB.NewPar("dataA", fine)
                };

                using (var dr = d.EseguiSQLDataReader(cDB.Queries.Movimenti_GraficoSaldo, p))
                    if (dr.HasRows)
                        while (dr.Read())
                            lTotale.Text = GB.ObjectToMoneyString(dr[0]);

                var q = CurrentGraphType == GraphType.year ? cDB.Queries.Movimenti_GraficoAnnuale : cDB.Queries.Movimenti_GraficoMensile;

                using (var dt = d.EseguiSQLDataTable(q, p))
                {
                    if (dt.Rows.Count > 0)
                    {
                        var enu = dt.AsEnumerable();

                        switch (CurrentGraphType)
                        {
                            case GraphType.year:
                                var years = enu.Select(r => int.Parse(r[0] as string));

                                var startY = years.First();
                                var endY = years.Last();

                                for (int y = startY; y <= endY; y++)
                                    if (!years.Contains(y))
                                        dt.Rows.Add(new object[] { y, 0 });
                                break;

                            case GraphType.month:
                                var dates = enu.Select(r => DateTime.ParseExact(r[0] as string, "yyyy/MM", System.Globalization.CultureInfo.InvariantCulture));

                                var startD = dates.First();
                                var endD = dates.Last();

                                for (var date = startD; date <= endD; date = date.AddMonths(1))
                                    if (!dates.Contains(date))
                                        dt.Rows.Add(new object[] { date.ToString("yyyy/MM"), 0 });
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

        protected void bCerca_Click(object sender, EventArgs e)
        {
            FindFor();
        }

        protected void bType_Click(object sender, EventArgs e)
        {
            switch (CurrentGraphType)
            {
                case GraphType.year:
                    // next state                    
                    bType.Text = "calendar_month";
                    bType.ToolTip = "Annual";

                    // go to year mode
                    CurrentGraphType = GraphType.month;
                    break;

                case GraphType.month:
                    // next state
                    bType.Text = "date_range";
                    bType.ToolTip = "Monthly";

                    // go to month mode
                    CurrentGraphType = GraphType.year;
                    break;
            }

            FindFor();
        }

    }
}