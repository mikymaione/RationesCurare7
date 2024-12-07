/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare.DB.DataWrapper;
using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RationesCurare
{
    public partial class mSaldo : CulturePage
    {

        public string SottoTitolo = "";

        public bool TipoNonSettato => Tipo.Length == 0;

        public bool TipoVisibile => TipoNonSettato;

        private string Tipo => GB.GetQueryString(Request, "T");

        protected string userName => GB.Instance.getCurrentSession(Session).UserName;

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            var tipo = TipoNonSettato ? "" : $"&T={Tipo}";

            Response.Redirect($"mMovimento.aspx?ID=-1{tipo}");
        }     

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var id = DataBinder.Eval(e.Row.DataItem, "ID");
                var tipo = TipoNonSettato ? "" : $"&T={Tipo}";
                var url = $"mMovimento.aspx?ID={id}{tipo}";

                e.Row.Attributes["onmousedown"] = $@"
                    if (event.button === 0) {{
                        window.location.href = '{url}';
                    }} else if (event.button === 1) {{
                        window.open('{url}', '_blank');
                    }}
                ";
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_RicercaSaldo, ParametriRicerca()))
                    if (dr.HasRows)
                    {
                        var lbl = (Label)e.Row.FindControl("lblTotal");

                        while (dr.Read())
                            lbl.Text = GB.ObjectToMoneyString(dr["Saldo"]);
                    }
            }
        }

        private System.Data.Common.DbParameter[] ParametriRicerca() =>
            new System.Data.Common.DbParameter[] {
                cDB.NewPar("tipo1", idCassa.SelectedValue),
                cDB.NewPar("tipo2", idCassa.SelectedValue),
                cDB.NewPar("descrizione", "%" + idDescrizione.Value + "%"),
                cDB.NewPar("MacroArea", "%" + idMacroarea.Value + "%"),
                cDB.NewPar("bSoldi", bSoldi.Checked ? 1 : 0),
                cDB.NewPar("bData", bData.Checked ? 1 : 0),
                cDB.NewPar("SoldiDa", GB.HTMLDoubleToDouble(eSoldiDa.Value)),
                cDB.NewPar("SoldiA", GB.HTMLDoubleToDouble(eSoldiA.Value)),
                cDB.NewPar("DataDa", GB.DateStartOfTheDay(GB.StringToDate(eDataDa.Text, new DateTime(1986, 1, 1)))),
                cDB.NewPar("DataA", GB.DateEndOfTheDay(GB.StringToDate(eDataA.Text, DateTime.Now.AddYears(20))))
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            Cerca();
        }

        private void Cerca()
        {
            var TipoDesc = string.IsNullOrEmpty(Tipo) ? "Balance" : Tipo;
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

            eMax.SelectedIndex = 0;

            if (TipoNonSettato)
                idCassa.SelectedIndex = 0;

            Cerca();
        }

        protected List<string> getMacroAree()
        {
            var m = new CMovimenti();

            return m.GetMacroAree(Session, '"', '”');
        }

        [System.Web.Services.WebMethod]
        public static List<string> getDescrizioni(string userName, string s)
        {
            var m = new CMovimenti();

            return m.GetDescrizioni(userName, s, '"', '”');
        }

    }
}