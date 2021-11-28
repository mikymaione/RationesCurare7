using RationesCurare7.DB;
using System;
using System.Collections.Generic;

namespace RationesCurare
{
    public partial class mMovimento : System.Web.UI.Page
    {

        private int IDMovimento = -1;
        private string Tipo = "";

        public string SottoTitolo = "";

        protected string userName => GB.Instance.getCurrentSession(Session).UserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                try
                {
                    IDMovimento = GB.ObjectToInt(Request.QueryString["ID"], -1);
                }
                catch
                {
                    // no id
                }

                try
                {
                    Tipo = Request.QueryString["T"];
                }
                catch
                {
                    // no tipo
                }
            }

            SottoTitolo = IDMovimento == -1
                ? "Nuovo importo"
                : $"Importo {IDMovimento}";

            var master = (RC)Master;
            master.nav_mMovimento.InnerHtml = SottoTitolo;

            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {
                var casse = db.EseguiSQLDataTable(cDB.Queries.Casse_Lista);
                idCassa.DataSource = casse;
                idCassa.DataBind();
                idCassa.SelectedValue = Tipo;

                idGiroconto.DataSource = casse;
                idGiroconto.DataBind();

                if (IDMovimento > -1)
                {
                    var par = new System.Data.Common.DbParameter[] {
                        db.NewPar("ID", IDMovimento)
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

        protected void bSalva_Click(object sender, EventArgs e)
        {
            using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
            {

            }
        }

    }
}