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
using System.Data;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

namespace RationesCurare
{
    public partial class mGraficoTorta : CulturePage
    {

        private static readonly Color[] KELLY_COLORS = {
            ColorTranslator.FromHtml("0xFFB300"),    // Vivid Yellow
            ColorTranslator.FromHtml("0x803E75"),    // Strong Purple
            ColorTranslator.FromHtml("0xFF6800"),    // Vivid Orange
            ColorTranslator.FromHtml("0xA6BDD7"),    // Very Light Blue
            ColorTranslator.FromHtml("0xC10020"),    // Vivid Red
            ColorTranslator.FromHtml("0xCEA262"),    // Grayish Yellow
            ColorTranslator.FromHtml("0x817066"),    // Medium Gray

            ColorTranslator.FromHtml("0x007D34"),    // Vivid Green
            ColorTranslator.FromHtml("0xF6768E"),    // Strong Purplish Pink
            ColorTranslator.FromHtml("0x00538A"),    // Strong Blue
            ColorTranslator.FromHtml("0xFF7A5C"),    // Strong Yellowish Pink
            ColorTranslator.FromHtml("0x53377A"),    // Strong Violet
            ColorTranslator.FromHtml("0xFF8E00"),    // Vivid Orange Yellow
            ColorTranslator.FromHtml("0xB32851"),    // Strong Purplish Red
            ColorTranslator.FromHtml("0xF4C800"),    // Vivid Greenish Yellow
            ColorTranslator.FromHtml("0x7F180D"),    // Strong Reddish Brown
            ColorTranslator.FromHtml("0x93AA00"),    // Vivid Yellowish Green
            ColorTranslator.FromHtml("0x593315"),    // Deep Yellowish Brown
            ColorTranslator.FromHtml("0xF13A13"),    // Vivid Reddish Orange
            ColorTranslator.FromHtml("0x232C16"),    // Dark Olive Green
        };

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

            Chart1.Series[0]["PieLabelStyle"] = "Disabled";
            Chart1.Legends[0].Font = ubuntuFont;

            Chart1.Palette = ChartColorPalette.None;
            Chart1.PaletteCustomColors = KELLY_COLORS;

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

                var dt = d.EseguiSQLDataTable(cDB.Queries.Movimenti_GraficoTorta, p);

                if (dt.Rows.Count == 0)
                {
                    Chart1.DataSource = dt;
                }
                else
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

                    Chart1.DataSource = pos
                        .Union(neg)
                        .CopyToDataTable();
                }

                Chart1.DataBind();
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