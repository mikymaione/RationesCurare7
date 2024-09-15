/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Globalization;

namespace RationesCurare
{
    public partial class mRegister : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                eCurrency.DataSource = GestioneValute.Currencies;
                eCurrency.DataBind();

                riempiValute();
            }
        }

        protected void bRegistrati_Click(object sender, EventArgs e)
        {
            var utente = eNickName.Value.TrimEnd();
            var nome = eUtente.Value.TrimEnd();
            var psw = ePsw.Value.TrimEnd();
            var language = eLanguage.SelectedValue;

            if (nome != null && nome.Length > 1 && psw != null && psw.Length > 1)
            {
                var p = MapPath("App_Data");
                var f = System.IO.Path.Combine(p, nome);

                if (System.IO.File.Exists(f + ".rqd8") || System.IO.File.Exists(f + ".psw"))
                {
                    lErrore.Text = "Utente già esistente!";
                }
                else
                {
                    var standard = System.IO.Path.Combine(p, "standard.rqd8");

                    try
                    {
                        System.IO.File.Copy(standard, f + ".rqd8");

                        using (var sw = new System.IO.StreamWriter(f + ".psw"))
                            sw.WriteLine(psw);

                        CreaUtenteInDbInfo(nome, utente, psw, language);
                    }
                    catch (Exception ex)
                    {
                        lErrore.Text = ex.Message;
                    }
                }
            }
            else
            {
                lErrore.Text = "Email o password non valide!";
            }
        }

        private void Login_(string nome, string psw, string lingua)
        {
            var App_Data = MapPath("App_Data");

            if (GB.ControllaCredenziali(App_Data, nome, psw))
            {
                var f = System.IO.Path.Combine(App_Data, nome);

                var cultures = GestioneValute.GetCultureByLanguage(lingua);

                GB.Instance.setCurrentSession(Session, new cSession());
                GB.Instance.getCurrentSession(Session).LoggedIN = true;
                GB.Instance.getCurrentSession(Session).UserName = nome;
                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd8";
                GB.Instance.getCurrentSession(Session).Culture = cultures.Count == 0 ? CultureInfo.CurrentCulture : cultures[0];
            }
            else
            {
                lErrore.Text = "Credenziali d'accesso errate!";
            }
        }

        void CreaUtenteInDbInfo(string email, string nome, string psw, string lingua)
        {
            try
            {
                Login_(email, psw, lingua);

                var regionInfo = GestioneValute.GetRegionInfoByLingua(lingua);
                var valuta = regionInfo.Count == 0 ? "EUR" : regionInfo[0].ISOCurrencySymbol;

                using (var db = new cDB(GB.Instance.getCurrentSession(Session).PathDB))
                {
                    var UltimaModifica = DateTime.Now.AddYears(-15);
                    var UltimoAggiornamentoDB = DateTime.Now.AddYears(-15);

                    var param = new System.Data.Common.DbParameter[] {
                        cDB.NewPar("nome", nome, System.Data.DbType.String),
                        cDB.NewPar("Psw", psw, System.Data.DbType.String),
                        cDB.NewPar("Email", email, System.Data.DbType.String),
                        cDB.NewPar("SincronizzaDB", true, System.Data.DbType.Boolean),
                        cDB.NewPar("UltimaModifica", UltimaModifica, System.Data.DbType.DateTime),
                        cDB.NewPar("UltimoAggiornamentoDB", UltimoAggiornamentoDB, System.Data.DbType.DateTime),
                        cDB.NewPar("Valuta", valuta, System.Data.DbType.String),
                        cDB.NewPar("Lingua", lingua, System.Data.DbType.String),
                    };

                    db.EseguiSQLNoQueryAutoCommit(cDB.Queries.DBInfo_Inserisci, param);

                    Response.Redirect("mMenu.aspx");
                }
            }
            catch (Exception ex)
            {
                lErrore.Text = $"Error: {ex.Message}";
            }
        }

        private void riempiValute()
        {
            eLanguage.DataSource = GestioneValute.GetLanguageCurrenciesByValuta(eCurrency.SelectedValue);
            eLanguage.DataBind();
        }

        protected void eCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            riempiValute();
        }

    }
}