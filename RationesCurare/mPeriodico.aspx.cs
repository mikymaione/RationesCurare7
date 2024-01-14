using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace RationesCurare
{
    public partial class mPeriodico : Page
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
                ? "Nuovo periodico"
                : $"Periodico {IDMovimento}";

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
                                    idNome.Value = dr["Nome"] as string;
                                    idDescrizione.Value = dr["Descrizione"] as string;
                                    idMacroarea.Value = dr["Macroarea"] as string;
                                    idCassa.SelectedValue = GB.ComboBoxItemsByValue(idCassa, dr["Tipo"] as string);
                                    idSoldi.Value = GB.ObjectToHTMLDouble(dr["Soldi"], 0);
                                    idPartendoDalGiorno.Value = GB.ObjectToDateStringHTML(dr["PartendoDalGiorno"]);
                                    idScadenza.Value = GB.ObjectToDateStringHTML(dr["Scadenza"]);
                                    idTipoGiorniMese.Value = dr["TipoGiorniMese"] as string;
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
        }

        private System.Data.Common.DbParameter[] getParamsForSave()
        {
            if (IDMovimento > -1)
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value, System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue, System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value, System.Data.DbType.String),
                    cDB.NewPar("soldi", GB.HTMLDoubleToDouble(idSoldi.Value), System.Data.DbType.Double),
                    cDB.NewPar("NumeroGiorni", GB.HTMLIntToInt (idNumeroGiorni.Value), System.Data.DbType.Int32),
                    cDB.NewPar("GiornoDelMese", DateTime.Now, System.Data.DbType.DateTime),
                    cDB.NewPar("PartendoDalGiorno", GB.StringHTMLToDate(idPartendoDalGiorno.Value), System.Data.DbType.DateTime),
                    cDB.NewPar("Scadenza", GB.StringHTMLToDate(idScadenza.Value), System.Data.DbType.DateTime),
                    cDB.NewPar("TipoGiorniMese", idTipoGiorniMese.Value, System.Data.DbType.String),
                    cDB.NewPar("MacroArea", idMacroarea.Value, System.Data.DbType.String),
                    cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32)
                };
            }
            else
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value, System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue, System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value, System.Data.DbType.String),
                    cDB.NewPar("soldi", GB.HTMLDoubleToDouble(idSoldi.Value), System.Data.DbType.Double),
                    cDB.NewPar("NumeroGiorni", GB.HTMLIntToInt (idNumeroGiorni.Value), System.Data.DbType.Int32),
                    cDB.NewPar("GiornoDelMese", DateTime.Now, System.Data.DbType.DateTime),
                    cDB.NewPar("PartendoDalGiorno", GB.StringHTMLToDateTime(idPartendoDalGiorno.Value), System.Data.DbType.DateTime),
                    cDB.NewPar("Scadenza", GB.StringHTMLToDateTime(idScadenza.Value), System.Data.DbType.DateTime),
                    cDB.NewPar("TipoGiorniMese", idTipoGiorniMese.Value, System.Data.DbType.String),
                    cDB.NewPar("MacroArea", idMacroarea.Value, System.Data.DbType.String),
                };
            }
        }

        private int SalvaMovimento()
        {
            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var param1 = getParamsForSave();
                return db.EseguiSQLNoQuery(IDMovimento > -1 ? cDB.Queries.Periodici_Aggiorna : cDB.Queries.Periodici_Inserisci, param1);
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
                lErrore.Text = $"Errore: {ex.Message}";
            }
        }

        [System.Web.Services.WebMethod]
        public static string getMacroAreaByDescrizione(string userName, string descrizione)
        {
            var PathDB = GB.getDBPathByName(userName);

            using (var db = new cDB(PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_GetMacroAree_E_Descrizioni))
                if (dr.HasRows)
                    while (dr.Read())
                        if (descrizione.Equals(dr["descrizione"] as string, StringComparison.OrdinalIgnoreCase))
                            return dr["MacroArea"] as string;

            return "";
        }

        protected List<string> getMacroAree()
        {
            var macroaree = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSourceMA))
                if (dr.HasRows)
                    while (dr.Read())
                        macroaree.Add(dr["MacroArea"] as string);

            return macroaree;
        }

        protected List<string> getDescrizioni()
        {
            var descrizioni = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSource))
                if (dr.HasRows)
                    while (dr.Read())
                        descrizioni.Add(dr["descrizione"] as string);

            return descrizioni;
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

                    db.EseguiSQLNoQuery(cDB.Queries.Periodici_Elimina, param);
                }

                Response.Redirect(ViewState["PreviousPage"].ToString());
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Errore: {ex.Message}";
            }
        }

    }
}