﻿using RationesCurare7.DB;
using System;
using System.Drawing.Text;
using System.Drawing;
using System.Data;
using System.Linq;

namespace RationesCurare
{
    public partial class mGraficoTorta : System.Web.UI.Page
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

        private Font loadUbuntuFont()
        {
            var css = MapPath("css");
            var css_rc = System.IO.Path.Combine(css, "rc");
            var font_file = System.IO.Path.Combine(css_rc, "UbuntuMono-Regular.ttf");

            var collection = new PrivateFontCollection();
            collection.AddFontFile(font_file);

            var fontFamily = new FontFamily("Ubuntu Mono", collection);
            var font = new Font(fontFamily, 14);

            return font;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var ubuntuFont = loadUbuntuFont();

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
                            r[1] descending
                        select
                            r;

                    var neg =
                        from r in enu
                        where
                            r[1] as double? <= 0
                        orderby
                            r[1] ascending
                        select
                            r;

                    var sorted = pos.Union(neg);

                    Chart1.DataSource = sorted.CopyToDataTable();
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