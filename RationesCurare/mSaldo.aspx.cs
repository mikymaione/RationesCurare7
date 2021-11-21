using RationesCurare7.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mSaldo : System.Web.UI.Page
    {
        private double grdTotal = 0;

        private string Tipo
        {
            get
            {
                var t = "Saldo";

                try
                {
                    t = Request.QueryString["T"];
                }
                catch
                {
                    //error                    
                }

                return t;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "soldi"));
                grdTotal = grdTotal + rowTotal;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = grdTotal.ToString("c");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "RC Web - " + Tipo;
            Form.DefaultButton = bCerca.UniqueID;

            if (GB.CurrentSession != null)
            {
                using (var d = new cDB(GB.CurrentSession.TipoDB, GB.CurrentSession.PathDB))
                {
                    var m = MapPath("DB");
                    m = System.IO.Path.Combine(m, "DBW");

                    var p = new System.Data.Common.DbParameter[] {
                       d.NewPar("tipo1", Tipo),
                       d.NewPar("tipo2", Tipo),
                       d.NewPar("descrizione", "%" + eDescrizione.Text + "%"),
                       d.NewPar("MacroArea", "%" + eMacroarea.Text +"%"),
                       d.NewPar("bSoldi", bSoldi.Checked ? 1 : 0),
                       d.NewPar("bData", bData.Checked ? 1 : 0),
                       d.NewPar("SoldiDa", GB.ObjectToDouble(eSoldiDa.Text, 0)),
                       d.NewPar("SoldiA", GB.ObjectToDouble(eSoldiA.Text, 0)),
                       d.NewPar("DataDa", GB.StringToDate(eDataDa.Text, new DateTime(1986, 1, 1))),
                       d.NewPar("DataA", GB.StringToDate(eDataA.Text, DateTime.Now.AddYears(20)))
                    };

                    GridView1.DataSource = d.EseguiSQLDataTable(m, cDB.Queries.Movimenti_Ricerca, p, GB.ObjectToInt(eMax.Text, 50));
                    GridView1.DataBind();
                }
            }
        }

        protected void bResetta_Click(object sender, EventArgs e)
        {
            eDescrizione.Text = "";
            eMacroarea.Text = "";

            eSoldiDa.Text = "";
            eSoldiA.Text = "";

            eDataDa.Text = "";
            eDataA.Text = "";

            bSoldi.Checked = false;
            bData.Checked = false;
        }

    }
}