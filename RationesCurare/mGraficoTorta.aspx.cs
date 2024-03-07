using RationesCurare7.DB;
using System;
using System.Drawing.Text;
using System.Drawing;

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

        public void FindFor()
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
                    while (dr.Read())
                        lTotale.Text = dr.GetDouble(0).ToString("C");

                Chart1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_GraficoTorta, p);
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