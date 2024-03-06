using RationesCurare7.DB;
using System;
using System.Drawing.Text;
using System.Drawing;

namespace RationesCurare
{
    public partial class mGraficoTorta : System.Web.UI.Page
    {

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
                idData.Value = DateTime.Now.ToString("yyyy-MM");

            FindFor();
        }

        private void FindFor()
        {
            using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var m = GB.StringToMonth(idData.Value, DateTime.Now);
                var inizio = GB.DateStartOfMonth(m);
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
            var m = GB.StringToMonth(idData.Value, DateTime.Now);
            idData.Value = m.AddMonths(-1).ToString("yyyy-MM");
            FindFor();
        }

        protected void bNext_Click(object sender, EventArgs e)
        {
            var m = GB.StringToMonth(idData.Value, DateTime.Now);
            idData.Value = m.AddMonths(1).ToString("yyyy-MM");
            FindFor();
        }

    }
}