using RationesCurare7.DB;
using System;
using System.Web.UI;

namespace RationesCurare
{
    public partial class mCassa : Page
    {

        protected string IDCassa = "";
        public string SottoTitolo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                try
                {
                    IDCassa = Request.QueryString["ID"] ?? "";
                }
                catch
                {
                    // no id
                }
            }

            SottoTitolo = "".Equals(IDCassa)
                  ? "Nuova cassa"
                  : $"Cassa {IDCassa}";

            if (!Page.IsPostBack)
                if (!"".Equals(IDCassa))
                    using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                    {
                        var par = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", IDCassa)
                    };

                        using (var dr = db.EseguiSQLDataReader(cDB.Queries.Casse_Carica, par))
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    idNome.Value = dr["nome"] as string;
                                    idNascondi.Value = GB.ObjectToBool(dr["Nascondi"]) ? "1" : "0";
                                }
                    }
        }

        protected void bSalva_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var param = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", idNome.Value, System.Data.DbType.String),
                        cDB.NewPar("Nascondi", "1".Equals(idNascondi.Value), System.Data.DbType.Boolean),
                    };

                    var r = db.EseguiSQLNoQuery("".Equals(IDCassa) ? cDB.Queries.Casse_Inserisci : cDB.Queries.Casse_Aggiorna, param);

                    lErrore.Text = $"{r} elementi salvati!";
                    idNome.Disabled = true;
                }
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Errore: {ex.Message}";
            }
        }

    }
}