using RationesCurare7.DB;
using System;
using System.Collections.Generic;

namespace RationesCurare
{
    public partial class mMovimento : System.Web.UI.Page
    {

        protected int IDMovimento = -1;
        private string Tipo = "";

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

                try
                {
                    Tipo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(GB.GetQueryString(Request, "T"));
                }
                catch
                {
                    // no tipo                   
                }
            }

            SottoTitolo = IDMovimento == -1
                ? "Nuovo importo"
                : $"Importo {IDMovimento}";

            if (!Page.IsPostBack)
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var casse = db.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
                    idCassa.DataSource = casse;
                    idCassa.DataBind();

                    idGiroconto.DataSource = casse;
                    idGiroconto.DataBind();

                    if (Tipo.Length > 0)
                        idCassa.SelectedValue = Tipo;

                    if (IDMovimento > -1)
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
                                    idCassa.SelectedValue = dr["Tipo"] as string;
                                    idSoldi.Value = GB.ObjectToHTMLDouble(dr["Soldi"], 0);
                                    idData.Value = GB.ObjectToDateTimeStringHTML(dr["Data"]);
                                }
                    }
                    else
                    {
                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Utente_Carica))
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    idNome.Value = dr["Nome"] as string;
                                }

                        idData.Value = GB.ObjectToDateTimeStringHTML(DateTime.Now);
                    }
                }
        }

        private System.Data.Common.DbParameter[] getParamsForSave(double soldi, DateTime data)
        {
            if (IDMovimento > -1)
            {
                return new System.Data.Common.DbParameter[] {
                    cDB.NewPar("nome", idNome.Value, System.Data.DbType.String),
                    cDB.NewPar("tipo", idCassa.SelectedValue, System.Data.DbType.String),
                    cDB.NewPar("descrizione", idDescrizione.Value, System.Data.DbType.String),
                    cDB.NewPar("soldi", soldi, System.Data.DbType.Double),
                    cDB.NewPar("data", data, System.Data.DbType.DateTime),
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
                    cDB.NewPar("soldi", soldi, System.Data.DbType.Double),
                    cDB.NewPar("data", data, System.Data.DbType.DateTime),
                    cDB.NewPar("MacroArea", idMacroarea.Value, System.Data.DbType.String)
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
                if (IDMovimento == -1 && idGiroconto.SelectedIndex > 0)
                {
                    var param2 = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", idNome.Value, System.Data.DbType.String),
                        cDB.NewPar("tipo", idGiroconto.SelectedValue, System.Data.DbType.String),
                        cDB.NewPar("descrizione", idDescrizione.Value, System.Data.DbType.String),
                        cDB.NewPar("soldi", -soldi, System.Data.DbType.Double),
                        cDB.NewPar("data", data, System.Data.DbType.DateTime),
                        cDB.NewPar("MacroArea", idMacroarea.Value, System.Data.DbType.String)
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
                lErrore.Text = $"{SalvaMovimento()} elementi salvati!";
                DisableUI();
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

        private void DisableUI()
        {
            idNome.Disabled = true;
            idDescrizione.Disabled = true;
            idMacroarea.Disabled = true;
            idSoldi.Disabled = true;
            idData.Disabled = true;
            idCassa.Enabled = false;
            idGiroconto.Enabled = false;
            bSalva.Enabled = false;
            bElimina.Enabled = false;
        }

        protected void bElimina_Click(object sender, EventArgs e)
        {
            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var param = new System.Data.Common.DbParameter[] {
                    cDB.NewPar("ID", IDMovimento, System.Data.DbType.Int32)
                };

                var r = db.EseguiSQLNoQuery(cDB.Queries.Movimenti_Elimina, param);
                lErrore.Text = $"{r} elementi eliminati!";

                DisableUI();
            }
        }

    }
}