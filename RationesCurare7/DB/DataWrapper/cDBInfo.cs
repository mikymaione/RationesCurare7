/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;

namespace RationesCurare7.DB.DataWrapper
{
    public class cDBInfo
    {
        public string Nome, Psw, Email, Valuta;
        public bool SincronizzaDB;
        public DateTime UltimaModifica = DateTime.Now.AddYears(-15);
        public DateTime UltimoAggiornamentoDB = DateTime.Now.AddYears(-15);

        public readonly bool DatiCaricati;

        public cDBInfo(string Email)
        {
            DatiCaricati = !"".Equals(Email) && CaricaByID(Email);
        }

        public int Aggiorna() =>
            cGB.sDB.EseguiSQLNoQuery(
                cDB.Queries.DBInfo_Aggiorna,
                new[]
                {
                    cGB.sDB.NewPar("Nome", Nome),
                    cGB.sDB.NewPar("Psw", Psw),
                    cGB.sDB.NewPar("Email", Email),
                    cGB.sDB.NewPar("SincronizzaDB", SincronizzaDB),
                    cGB.sDB.NewPar("UltimaModifica", UltimaModifica),
                    cGB.sDB.NewPar("UltimoAggiornamentoDB", UltimoAggiornamentoDB),
                    cGB.sDB.NewPar("Valuta", Valuta),
                },
                true
            );

        public int Inserisci() =>
            cGB.sDB.EseguiSQLNoQuery(
                cDB.Queries.DBInfo_Inserisci,
                new[]
                {
                    cGB.sDB.NewPar("Nome", Nome),
                    cGB.sDB.NewPar("Psw", Psw),
                    cGB.sDB.NewPar("Email", Email),
                    cGB.sDB.NewPar("SincronizzaDB", SincronizzaDB),
                    cGB.sDB.NewPar("UltimaModifica", UltimaModifica),
                    cGB.sDB.NewPar("UltimoAggiornamentoDB", UltimoAggiornamentoDB),
                    cGB.sDB.NewPar("Valuta", Valuta),
                },
                false
            );

        private bool CaricaByID(string Email_)
        {
            var p = new[]
            {
                cGB.sDB.NewPar("Email", Email_)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cDB.Queries.DBInfo_Dettaglio, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        Email = Convert.ToString(dr["Email"]);
                        Nome = Convert.ToString(dr["nome"]);
                        Psw = Convert.ToString(dr["Psw"]);
                        Valuta = Convert.ToString(dr["Valuta"]);
                        SincronizzaDB = Convert.ToBoolean(dr["SincronizzaDB"]);
                        UltimaModifica = cGB.ObjectToDateTime(dr["UltimaModifica"]);
                        UltimoAggiornamentoDB = cGB.ObjectToDateTime(dr["UltimoAggiornamentoDB"]);

                        return true;
                    }

            return false;
        }

    }
}