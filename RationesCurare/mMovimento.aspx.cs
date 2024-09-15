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
using System.Linq;

namespace RationesCurare
{
    public partial class mMovimento : CulturePage
    {

        protected long IDMovimento = -1;

        private long CurIDGiroconto = -1;

        private string Tipo = "";

        public string SottoTitolo = "";

        private bool isNewRecord => IDMovimento == -1;

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

                try
                {
                    Tipo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(GB.GetQueryString(Request, "T"));
                }
                catch
                {
                    // no tipo                   
                }
            }

            divGiroconto.Visible = isNewRecord;
            bElimina.Visible = !isNewRecord;

            SottoTitolo = isNewRecord
                ? "New amount"
                : $"Transaction #{IDMovimento}";

            if (!Page.IsPostBack)
            {
                ViewState["PreviousPage"] = Request.UrlReferrer;

                idCassa.Focus();

                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var casse = db.EseguiSQLDataTable(cDB.Queries.Casse_Ricerca);
                    idCassa.DataSource = casse;
                    idCassa.DataBind();

                    idGiroconto.DataSource = casse;
                    idGiroconto.DataBind();

                    if (Tipo.Length > 0)
                        idCassa.SelectedValue = GB.ComboBoxItemsByValue(idCassa, Tipo);

                    if (isNewRecord)
                    {
                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Utente_Carica))
                            if (dr.HasRows)
                                while (dr.Read())
                                    idNome.Value = dr["Nome"] as string;

                        idData.Value = GB.ObjectToDateTimeStringHTML(DateTime.Now);
                    }
                    else
                    {
                        var par = new System.Data.Common.DbParameter[] {
                            cDB.NewPar("ID", IDMovimento)
                        };

                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_Dettaglio, par))
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    idNome.Value = dr["Nome"] as string;
                                    idDescrizione.Value = dr["Descrizione"] as string;
                                    idMacroarea.Value = dr["Macroarea"] as string;
                                    idCassa.SelectedValue = GB.ComboBoxItemsByValue(idCassa, dr["Tipo"] as string);
                                    idSoldi.Value = GB.ObjectToHTMLDouble(dr["Soldi"], 0);
                                    idData.Value = GB.ObjectToDateTimeStringHTML(dr["Data"]);
                                    CurIDGiroconto = GB.ObjectToInt(dr["IDGiroconto"], -1);

                                    setRequired();
                                }
                    }
                }
            }
        }

        private void setRequired()
        {
            var isGiroconto = idGiroconto.SelectedIndex > 0 || CurIDGiroconto > 0;

            if (isGiroconto)
            {
                idDescrizione.Attributes.Remove("required");
                idMacroarea.Attributes.Remove("required");
                lDescrizione.Attributes.Remove("class");
                lMacroarea.Attributes.Remove("class");
            }
            else
            {
                idDescrizione.Attributes.Add("required", "required");
                idMacroarea.Attributes.Add("required", "required");
                lDescrizione.Attributes.Add("class", "required");
                lMacroarea.Attributes.Add("class", "required");
            }
        }

        private System.Data.Common.DbParameter[] getParamsForSave(double soldi, DateTime data, long IDGiroconto)
        {
            if (isNewRecord)
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("soldi", soldi, System.Data.DbType.Double),
                    cDB.NewPar("data", data, System.Data.DbType.DateTime),
                    cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("IDGiroconto", IDGiroconto)
                };
            }
            else
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("soldi", soldi, System.Data.DbType.Double),
                    cDB.NewPar("data", data, System.Data.DbType.DateTime),
                    cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32),
                    cDB.NewPar("IDGiroconto", IDGiroconto)
                };
            }
        }

        private int SalvaMovimento()
        {
            var soldi = GB.HTMLDoubleToDouble(idSoldi.Value);
            var data = GB.StringHTMLToDateTime(idData.Value);

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            using (var tran = db.BeginTransaction())
                try
                {
                    var param1 = getParamsForSave(soldi, data, CurIDGiroconto);
                    var m1 = db.EseguiSQLNoQuery(tran, IDMovimento > -1 ? cDB.Queries.Movimenti_Aggiorna : cDB.Queries.Movimenti_Inserisci, param1);

                    if (m1 < 1)
                        throw new Exception("Record non inserito!");

                    // con giroconto
                    if (isNewRecord && idGiroconto.SelectedIndex > 0)
                    {
                        var IDGiroconto = db.LastInsertRowId(tran);

                        if (IDGiroconto < 0)
                            throw new Exception("ID giroconto non fornito!");

                        var param2 = new System.Data.Common.DbParameter[] {
                            cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                            cDB.NewPar("tipo", idGiroconto.SelectedValue.TrimEnd(), System.Data.DbType.String),
                            cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                            cDB.NewPar("soldi", -soldi, System.Data.DbType.Double),
                            cDB.NewPar("data", data, System.Data.DbType.DateTime),
                            cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String),
                            cDB.NewPar("IDGiroconto", IDGiroconto),
                        };

                        var m2 = db.EseguiSQLNoQuery(tran, cDB.Queries.Movimenti_Inserisci, param2);
                        var IDGiroconto2 = db.LastInsertRowId(tran);

                        if (IDGiroconto2 < 0)
                            throw new Exception("ID giroconto non fornito!");

                        if (m2 < 1)
                            throw new Exception("Record non inserito!");

                        var idP = cDB.NewPar("ID", IDGiroconto);
                        var idGP = cDB.NewPar("IDGiroconto", IDGiroconto2);

                        var param3 = param1
                            .Append(idP)
                            .Append(idGP)
                            .ToArray();

                        var m3 = db.EseguiSQLNoQuery(tran, cDB.Queries.Movimenti_Aggiorna, param3);

                        if (m3 < 0)
                            throw new Exception("Record non aggiornabile!");

                        tran.Commit();
                        return 2;
                    }
                    else
                    {
                        tran.Commit();
                        return 1;
                    }
                }
                catch
                {
                    tran.Rollback();
                    throw;
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

                    db.EseguiSQLNoQueryAutoCommit(cDB.Queries.Movimenti_Elimina, param);
                }

                Response.Redirect(ViewState["PreviousPage"].ToString());
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Error: {ex.Message}";
            }
        }

        protected void idGiroconto_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRequired();
            idSoldi.Focus();
        }
    }
}