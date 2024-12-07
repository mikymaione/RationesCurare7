/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Web.SessionState;

namespace RationesCurare.DB.DataWrapper
{
    public class CMovimenti
    {

        private string RemoveChar(string s, char oldChar, char newChar) =>
            s.Replace(oldChar, newChar);

        public List<string> GetMacroAree(HttpSessionState session, char oldChar, char newChar)
        {
            var macroaree = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSourceMA))
                if (dr.HasRows)
                    while (dr.Read())
                        macroaree.Add(RemoveChar(dr["MacroArea"] as string, oldChar, newChar));

            return macroaree;
        }

        public List<string> GetDescrizioni(string userName, string s, char oldChar, char newChar)
        {
            var descrizioni = new List<string>();

            if (s != null && s.Length > 1) 
            {
                var PathDB = GB.getDBPathByName(userName);

                var p = new[] {
                    cDB.NewPar("descrizione", $"%{s}%"),                
                };

                using (var db = new cDB(PathDB))
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSource, p))
                    if (dr.HasRows)
                        while (dr.Read())                        
                            descrizioni.Add(RemoveChar(dr["descrizione"] as string, oldChar, newChar));                
            }

            return descrizioni;
        }

        public string GetMacroAreaByDescrizione(string userName, string descrizione_)
        {
            var descrizione = descrizione_.TrimEnd();

            if (descrizione.Length > 0)
            {
                var PathDB = GB.getDBPathByName(userName);

                using (var db = new cDB(PathDB))
                using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_GetMacroAree_E_Descrizioni))
                    if (dr.HasRows)
                        while (dr.Read())
                            if (descrizione.Equals(dr["descrizione"] as string, StringComparison.OrdinalIgnoreCase))
                                return dr["MacroArea"] as string;
            }

            return "";
        }

    }
}