using RationesCurare7.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RCWebMobile
{
    public partial class mMenu : System.Web.UI.Page
    {
        private double grdTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.CurrentSession != null)
            {
                Title = "RC Web - " + GB.CurrentSession.UserName;

                using (var d = new cDB(GB.CurrentSession.TipoDB, GB.CurrentSession.PathDB))
                {
                    var q = "select Tipo, sum(soldi) as Saldo from Movimenti group by Tipo order by Tipo";

                    GridView1.DataSource = d.EseguiSQLDataTable(q);
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Saldo"));
                grdTotal = grdTotal + rowTotal;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = grdTotal.ToString("c");
            }
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var nome = r.ItemArray[0];

            Response.Redirect("mSaldo.aspx?T=" + nome);
        }

    }
}