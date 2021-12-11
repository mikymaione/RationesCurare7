using RationesCurare7.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mCasse : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.Instance.getCurrentSession(Session) != null)
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
                    GridView1.DataBind();
                }
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var nome = r.ItemArray[0];

            Response.Redirect("mCassa.aspx?ID=" + nome);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
        }

    }
}