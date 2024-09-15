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
using System.Web.UI;
using System.Xml.Linq;

namespace RationesCurare
{
    public partial class mPeriodico : CulturePage
    {
        protected long IDMovimento = -1;
        public string SottoTitolo = "";

        protected string userName => GB.Instance.getCurrentSession(Session).UserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                try
                {
                    IDMovimento = GB.ObjectToInt(GB.GetQueryString(Request, "ID"), -1);
                }
                catch
                {
                    // no id
                }
            }

            bElimina.Visible = IDMovimento != -1;

            SottoTitolo = IDMovimento == -1
                ? "New recurring transaction"
                : $"Recurring transaction #{IDMovimento}";

            if (!Page.IsPostBack)
            {
                ViewState["PreviousPage"] = Request.UrlReferrer;

                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var casse = db.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
                    idCassa.DataSource = casse;
                    idCassa.DataBind();

                    if (IDMovimento > -1)
                    {
                        var par = new System.Data.Common.DbParameter[] {
                            cDB.NewPar("ID", IDMovimento)
                        };

                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Periodici_Dettaglio, par))
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    var TipoGiorniMese = dr["TipoGiorniMese"] as string;
                                    var Scadenza = dr["Scadenza"];

                                    idNome.Value = dr["Nome"] as string;
                                    idDescrizione.Value = dr["Descrizione"] as string;
                                    idMacroarea.Value = dr["Macroarea"] as string;
                                    idCassa.SelectedValue = GB.ComboBoxItemsByValue(idCassa, dr["Tipo"] as string);
                                    idSoldi.Value = GB.ObjectToHTMLDouble(dr["Soldi"], 0);
                                    idData.Value = GB.ObjectToDateStringHTML(
                                        TipoGiorniMese == "G" ? dr["PartendoDalGiorno"] : dr["GiornoDelMese"]
                                    );
                                    idNumeroGiorni.Value = dr["NumeroGiorni"] as string;
                                    idScadenza.Value = GB.ObjectToDateStringHTML(Scadenza);
                                    bScadenza.Checked = !(Scadenza is DBNull);
                                    idTipoGiorniMese.Value = TipoGiorniMese;
                                }
                    }
                    else
                    {
                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Utente_Carica))
                            if (dr.HasRows)
                                while (dr.Read())
                                    idNome.Value = dr["Nome"] as string;
                    }
                }
            }

            Title = SottoTitolo;
        }

        private System.Data.Common.DbParameter[] getParamsForSave()
        {
            var Scadenza = GB.ValueToDBNULL(!bScadenza.Checked, () => GB.StringHTMLToDate(idScadenza.Value));
            var TipoGiorniMese = idTipoGiorniMese.Value;
            var PartendoDalGiorno = GB.ValueToDBNULL(TipoGiorniMese != "G", () => GB.StringHTMLToDate(idData.Value));
            var GiornoDelMese = GB.ValueToDBNULL(TipoGiorniMese == "G", () => GB.StringHTMLToDate(idData.Value));
            var NumeroGiorni = GB.HTMLIntToInt(idNumeroGiorni.Value, 0);

            if (IDMovimento > -1)
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("soldi", GB.HTMLDoubleToDouble(idSoldi.Value), System.Data.DbType.Double),
                    cDB.NewPar("NumeroGiorni", NumeroGiorni, System.Data.DbType.Int32),
                    cDB.NewPar("GiornoDelMese", GiornoDelMese, System.Data.DbType.DateTime),
                    cDB.NewPar("PartendoDalGiorno", PartendoDalGiorno, System.Data.DbType.DateTime),
                    cDB.NewPar("Scadenza", Scadenza, System.Data.DbType.DateTime),
                    cDB.NewPar("TipoGiorniMese", TipoGiorniMese, System.Data.DbType.String),
                    cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32)
                };
            }
            else
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value, System.Data.DbType.String),
                    cDB.NewPar("soldi", GB.HTMLDoubleToDouble(idSoldi.Value), System.Data.DbType.Double),
                    cDB.NewPar("NumeroGiorni", NumeroGiorni, System.Data.DbType.Int32),
                    cDB.NewPar("GiornoDelMese", GiornoDelMese, System.Data.DbType.DateTime),
                    cDB.NewPar("PartendoDalGiorno", PartendoDalGiorno, System.Data.DbType.DateTime),
                    cDB.NewPar("Scadenza", Scadenza, System.Data.DbType.DateTime),
                    cDB.NewPar("TipoGiorniMese", TipoGiorniMese, System.Data.DbType.String),
                    cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String),
                };
            }
        }

        private int SalvaMovimento()
        {
            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var param1 = getParamsForSave();
                return db.EseguiSQLNoQueryAutoCommit(IDMovimento > -1 ? cDB.Queries.Periodici_Aggiorna : cDB.Queries.Periodici_Inserisci, param1);
            }
        }

        protected void bSalva_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaMovimento();
                Response.Redirect(ViewState["PreviousPage"].ToString());
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Error: {ex.Message}";
            }
        }

        [System.Web.Services.WebMethod]
        public static string getMacroAreaByDescrizione(string userName, string descrizione_)
        {
            var m = new CMovimenti();

            return m.GetMacroAreaByDescrizione(userName, descrizione_);
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

        protected void bElimina_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var param = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32)
                    };

                    db.EseguiSQLNoQueryAutoCommit(cDB.Queries.Periodici_Elimina, param);
                }

                Response.Redirect(ViewState["PreviousPage"].ToString());
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Error: {ex.Message}";
            }
        }

    }
}