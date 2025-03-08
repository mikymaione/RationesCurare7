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
using System.Linq;

namespace RationesCurare
{
    public partial class mGraficoTorta : CulturePage
    {
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

            var session = GB.Instance.getCurrentSession(Session);

            var folder = $"~/public/ChartImages/{session.UserName}";
            System.IO.Directory.CreateDirectory(Server.MapPath(folder));
            
            Chart1.ImageLocation = $"{folder}/PieChart.png";
            Chart1.Series[0]["PieLabelStyle"] = "Disabled";
            Chart1.Legends[0].Font = ubuntuFont;

            if (!IsPostBack)
                CurrentData = GB.DateStartOfMonth(DateTime.Now);

            FindFor();
        }

        private void FindFor()
        {
            using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var inizio = GB.DateStartOfMonth(GB.StringToDate(idDataDa.Value, DateTime.Now));
                var fine = GB.DateEndOfMonth(GB.StringToDate(idDataA.Value, DateTime.Now));

                var p = new System.Data.Common.DbParameter[] {
                    cDB.NewPar("da", inizio),
                    cDB.NewPar("a", fine)
                };

                using (var dr = d.EseguiSQLDataReader(cDB.Queries.Movimenti_GraficoTortaSaldo, p))
                    if (dr.HasRows)
                        while (dr.Read())
                            lTotale.Text = GB.ObjectToMoneyString(dr[0]);

                using (var dt = d.EseguiSQLDataTable(cDB.Queries.Movimenti_GraficoTorta, p))
                {
                    foreach (DataRow r in dt.Rows)
                        r[0] = $"{GB.ObjectToMoneyStringNoDecimal(r[1])} - {r[0]}";

                    var enu = dt.AsEnumerable();

                    var pos =
                        from r in enu
                        where
                            r[1] as double? > 0
                        orderby
                            r[1] as double? descending
                        select
                            r;

                    var neg =
                        from r in enu
                        where
                            r[1] as double? <= 0
                        orderby
                            r[1] as double? ascending
                        select
                            r;

                    var union = pos.Union(neg);

                    if (union.Any())
                    {
                        using (var sDt = union.CopyToDataTable())
                        {
                            Chart1.DataSource = sDt;
                            Chart1.DataBind();
                        }
                    }
                    else
                    {
                        Chart1.DataSource = dt;
                        Chart1.DataBind();
                    }
                }
            }
        }

        protected void bPrev_Click(object sender, EventArgs e)
        {
            CurrentData = CurrentData.AddMonths(-1);
            FindFor();
        }

        protected void bNext_Click(object sender, EventArgs e)
        {
            CurrentData = CurrentData.AddMonths(1);
            FindFor();
        }

        protected void bCerca_Click(object sender, EventArgs e)
        {
            FindFor();
        }

    }
}