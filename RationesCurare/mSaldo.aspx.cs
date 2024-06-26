﻿using RationesCurare.DB.DataWrapper;
using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mSaldo : Page
    {

        public string SottoTitolo = "";

        public bool TipoNonSettato => Tipo.Length == 0;

        public bool TipoVisibile => TipoNonSettato;

        private string Tipo => GB.GetQueryString(Request, "T");

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            var tipo = TipoNonSettato ? "" : $"&T={Tipo}";

            Response.Redirect($"mMovimento.aspx?ID=-1{tipo}");
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var dt = GridView1.DataSource as System.Data.DataTable;
            var r = dt.Rows[e.NewSelectedIndex];
            var nome = r.ItemArray[0];
            var tipo = TipoNonSettato ? "" : $"&T={Tipo}";

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
                cDB.NewPar("tipo1", idCassa.SelectedValue),
                cDB.NewPar("tipo2", idCassa.SelectedValue),
                cDB.NewPar("descrizione", "%" + idDescrizione.Value + "%"),
                cDB.NewPar("MacroArea", "%" + idMacroarea.Value +"%"),
                cDB.NewPar("bSoldi", bSoldi.Checked ? 1 : 0),
                cDB.NewPar("bData", bData.Checked ? 1 : 0),
                cDB.NewPar("SoldiDa", GB.HTMLDoubleToDouble(eSoldiDa.Value)),
                cDB.NewPar("SoldiA", GB.HTMLDoubleToDouble(eSoldiA.Value)),
                cDB.NewPar("DataDa", GB.DateStartOfMonth(GB.StringToDate(eDataDa.Text, new DateTime(1986, 1, 1)))),
                cDB.NewPar("DataA", GB.DateEndOfMonth(GB.StringToDate(eDataA.Text, DateTime.Now.AddYears(20))))
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            var TipoDesc = string.IsNullOrEmpty(Tipo) ? "Saldo" : Tipo;
            var TipoDescTitle = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(TipoDesc);

            Title = $"RationesCurare - {TipoDescTitle}";
            SottoTitolo = TipoDescTitle;

            Form.DefaultButton = bCerca.UniqueID;

            if (GB.Instance.getCurrentSession(Session) != null)
                using (var d = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    if (!Page.IsPostBack)
                    {
                        var casse = d.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
                        idCassa.DataSource = casse;
                        idCassa.DataBind();

                        if (Tipo.Length > 0)
                            idCassa.SelectedValue = GB.ComboBoxItemsByValue(idCassa, Tipo);
                    }

                    var p = ParametriRicerca();

                    GridView1.DataSource = d.EseguiSQLDataTable(cDB.Queries.Movimenti_Ricerca, p, GB.ObjectToInt(eMax.Text, 50));
                    GridView1.DataBind();
                }
        }

        protected void bResetta_Click(object sender, EventArgs e)
        {
            idDescrizione.Value = "";
            idMacroarea.Value = "";

            eSoldiDa.Value = "";
            eSoldiA.Value = "";

            eDataDa.Text = "";
            eDataA.Text = "";

            bSoldi.Checked = false;
            bData.Checked = false;
        }

        protected List<string> getMacroAree()
        {
            var m = new CMovimenti();

            return m.GetMacroAree(Session, '"', '”');
        }

        protected List<string> getDescrizioni()
        {
            var m = new CMovimenti();

            return m.GetDescrizioni(Session, '"', '”');
        }

    }
}