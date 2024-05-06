using RationesCurare7.DB;
using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mMenu : Page
    {
        protected void bNuovo_Click(object sender, EventArgs e)
        {
            Response.Redirect($"mMovimento.aspx?ID=-1");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.Instance.getCurrentSession(Session) != null)
            {
                Title = "RationesCurare - " + GB.Instance.getCurrentSession(Session).UserName;

                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_SaldoPerCassa);
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_Saldo))
                    if (dr.HasRows)
                    {
                        var lbl = (Label)e.Row.FindControl("lblTotal");

                        while (dr.Read())
                            lbl.Text = GB.ObjectToDouble(dr["Saldo"], 0).ToString("c");
                    }
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