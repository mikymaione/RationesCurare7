using RationesCurare7.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mSaldo : Page
    {

        public string SottoTitolo = "";

        private string Tipo => GB.GetQueryString(Request, "T");

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            var tipo = Tipo.Length == 0 ? "" : $"&T={Tipo}";

            Response.Redirect($"mMovimento.aspx?ID=-1{tipo}");
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var nome = r.ItemArray[0];
            var tipo = Tipo.Length == 0 ? "" : $"&T={Tipo}";

            Response.Redirect($"mMovimento.aspx?ID={nome}{tipo}");
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
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_RicercaSaldo, ParametriRicerca()))
                    if (dr.HasRows)
                    {
                        var lbl = (Label)e.Row.FindControl("lblTotal");

                        while (dr.Read())
                            lbl.Text = GB.ObjectToDouble(dr["Saldo"], 0).ToString("c");
                    }
            }
        }

        private System.Data.Common.DbParameter[] ParametriRicerca() =>
            new System.Data.Common.DbParameter[] {
                cDB.NewPar("tipo1", Tipo),
                cDB.NewPar("tipo2", Tipo),
                cDB.NewPar("descrizione", "%" + eDescrizione.Text + "%"),
                cDB.NewPar("MacroArea", "%" + eMacroarea.Text +"%"),
                cDB.NewPar("bSoldi", bSoldi.Checked ? 1 : 0),
                cDB.NewPar("bData", bData.Checked ? 1 : 0),
                cDB.NewPar("SoldiDa", GB.ObjectToDouble(eSoldiDa.Text, 0)),
                cDB.NewPar("SoldiA", GB.ObjectToDouble(eSoldiA.Text, 0)),
                cDB.NewPar("DataDa", GB.StringToDate(eDataDa.Text, new DateTime(1986, 1, 1))),
                cDB.NewPar("DataA", GB.StringToDate(eDataA.Text, DateTime.Now.AddYears(20)))
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            var TipoDesc = string.IsNullOrEmpty(Tipo) ? "Saldo" : Tipo;
            Title = $"RationesCurare - {TipoDesc}";
            SottoTitolo = TipoDesc;

            Form.DefaultButton = bCerca.UniqueID;

            if (GB.Instance.getCurrentSession(Session) != null)
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_Ricerca, ParametriRicerca(), GB.ObjectToInt(eMax.Text, 50));
                    GridView1.DataBind();
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