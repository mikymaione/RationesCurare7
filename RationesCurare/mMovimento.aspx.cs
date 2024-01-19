using RationesCurare7.DB;
using System;
using System.Collections.Generic;

namespace RationesCurare
{
    public partial class mMovimento : System.Web.UI.Page
    {

        protected long IDMovimento = -1;

        private string Tipo = "";

        public string SottoTitolo = "";

        public bool isNewRecord => IDMovimento == -1;

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
                ? "Nuovo importo"
                : $"Importo {IDMovimento}";

            if (!Page.IsPostBack)
            {
                ViewState["PreviousPage"] = Request.UrlReferrer;

                idCassa.Focus();

                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var casse = db.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
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
                                }
                    }
                }
            }
        }

        private System.Data.Common.DbParameter[] getParamsForSave(double soldi, DateTime data)
        {
            if (isNewRecord)
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                    cDB.NewPar("soldi", soldi, System.Data.DbType.Double),
                    cDB.NewPar("data", data, System.Data.DbType.DateTime),
                    cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String)
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
                    cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32)
                };
            }
        }

        private int SalvaMovimento()
        {
            var soldi = GB.HTMLDoubleToDouble(idSoldi.Value);
            var data = GB.StringHTMLToDateTime(idData.Value);

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var tran = db.BeginTransaction();

                var param1 = getParamsForSave(soldi, data);
                var m1 = db.EseguiSQLNoQuery(ref tran, IDMovimento > -1 ? cDB.Queries.Movimenti_Aggiorna : cDB.Queries.Movimenti_Inserisci, param1);

                // con giroconto
                if (isNewRecord && idGiroconto.SelectedIndex > 0)
                {
                    var param2 = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                        cDB.NewPar("tipo", idGiroconto.SelectedValue.TrimEnd(), System.Data.DbType.String),
                        cDB.NewPar("descrizione", idDescrizione.Value.TrimEnd(), System.Data.DbType.String),
                        cDB.NewPar("soldi", -soldi, System.Data.DbType.Double),
                        cDB.NewPar("data", data, System.Data.DbType.DateTime),
                        cDB.NewPar("MacroArea", idMacroarea.Value.TrimEnd(), System.Data.DbType.String)
                    };

                    var m2 = db.EseguiSQLNoQuery(ref tran, cDB.Queries.Movimenti_Inserisci, param2);

                    if (m1 + m2 == 2)
                    {
                        tran.Commit();
                        return 2;
                    }
                    else
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
                else
                {
                    tran.Commit();
                    return m1;
                }
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
        public static string getMacroAreaByDescrizione(string userName, string descrizione_)
        {
            var descrizione = descrizione_.TrimEnd();

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

                    db.EseguiSQLNoQuery(cDB.Queries.Movimenti_Elimina, param);
                }

                Response.Redirect(ViewState["PreviousPage"].ToString());
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Errore: {ex.Message}";
            }
        }

        protected void idGiroconto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isNotGiroconto = idGiroconto.SelectedIndex == 0;

            divDescrizione.Visible = isNotGiroconto;
            divMacroarea.Visible = isNotGiroconto;

            idSoldi.Focus();
        }
    }
}