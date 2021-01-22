using RationesCurare7.DB;
using System;

namespace RCWebMobile
{
    public partial class mGrafico : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var w = 0;
            var h = 0;

            try
            {
                w = Convert.ToInt32(Request.Cookies["HiddenField_ChartSizeW"].Value);
                h = Convert.ToInt32(Request.Cookies["HiddenField_ChartSizeH"].Value);
            }
            catch
            {
                //not setted		
            }

            if (w <= 0)
                w = Request.Browser.ScreenPixelsWidth;

            if (h <= 0)
                h = Request.Browser.ScreenPixelsHeight;

            w -= 10;
            h -= 80;

            Chart1.Width = GB.ObjectToInt(w, Request.Browser.ScreenPixelsWidth);
            Chart1.Height = GB.ObjectToInt(h, Request.Browser.ScreenPixelsHeight);
            Chart1.Series[0]["DrawingStyle"] = "LightToDark";

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
            if (T != "")
            {
                var m = MapPath("DB");
                m = System.IO.Path.Combine(m, "DBW");

                var q = cDB.Queries.Movimenti_GraficoMensile;

                if (T.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    q = cDB.Queries.Movimenti_GraficoAnnuale;

                using (var d = new cDB(GB.CurrentSession.TipoDB, GB.CurrentSession.PathDB))
                {
                    Chart1.DataSource = d.EseguiSQLDataTable(m, q);
                    Chart1.DataBind();
                }
            }
        }

        protected void Chart1_PrePaint(object sender, System.Web.UI.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            for (int i = 0; i < e.Chart.Series[0].Points.Count; i++)
                if (e.Chart.Series[0].Points[i].YValues[0] < 0)
                    e.Chart.Series[0].Points[i].Color = System.Drawing.Color.Red;
        }


    }
}