﻿using RationesCurare7.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mSaldo : Page
    {

        public string SottoTitolo = "";

        private double grdTotal = 0;

        private string Tipo => Request.QueryString.HasKeys()
                    ? Request.QueryString["T"]
                    : "Saldo";

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var nome = r.ItemArray[0];

            Response.Redirect("mMovimento.aspx?ID=" + nome);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "soldi"));
                grdTotal += rowTotal;

                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = grdTotal.ToString("c");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "RationesCurare - " + Tipo;
            SottoTitolo = Tipo;

            Form.DefaultButton = bCerca.UniqueID;

            if (GB.Instance.getCurrentSession(Session) != null)
            {
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
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

                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_Ricerca, p, GB.ObjectToInt(eMax.Text, 50));
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