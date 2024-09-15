/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Web.UI;

namespace RationesCurare
{
    public partial class mCassa : CulturePage
    {

        protected string IDCassa = "";
        public string SottoTitolo = "";

        public bool isNewRecord => string.IsNullOrEmpty(IDCassa);

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

            if (isNewRecord)
                idNome.Attributes.Remove("readonly");
            else
                idNome.Attributes.Add("readonly", "readonly");

            SottoTitolo = isNewRecord
                  ? "New account"
                  : $"Account {IDCassa}";

            if (!Page.IsPostBack)
            {
                ViewState["PreviousPage"] = Request.UrlReferrer;

                if (!isNewRecord)
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

            Title = SottoTitolo;
        }

        protected void bSalva_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var param = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", idNome.Value.TrimEnd(), System.Data.DbType.String),
                        cDB.NewPar("Nascondi", "1".Equals(idNascondi.Value), System.Data.DbType.Boolean),
                    };

                    db.EseguiSQLNoQueryAutoCommit("".Equals(IDCassa) ? cDB.Queries.Casse_Inserisci : cDB.Queries.Casse_Aggiorna, param);

                    Response.Redirect(ViewState["PreviousPage"].ToString());
                }
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Error: {ex.Message}";
            }
        }

    }
}