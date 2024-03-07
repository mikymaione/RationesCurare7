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
            get => GB.DateStartOfMonth(GB.StringToDate(idData.Text, DateTime.Now));
            set => idData.Text = GB.ObjectToDateStringHTML(value);
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
                var inizio = GB.DateStartOfMonth(CurrentData);
                var fine = GB.DateEndOfMonth(inizio);

                var p = new System.Data.Common.DbParameter[] {
                    cDB.NewPar("da", inizio),
                    cDB.NewPar("a", fine)
                };

                var q = cDB.Queries.Movimenti_GraficoTorta;
                Chart1.DataSource = d.EseguiSQLDataTable(q, p);
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

        protected void idData_TextChanged(object sender, EventArgs e)
        {
            FindFor();
        }

    }
}