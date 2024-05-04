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

        public List<string> GetDescrizioni(HttpSessionState session, char oldChar, char newChar)
        {
            var descrizioni = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSource))
                if (dr.HasRows)
                    while (dr.Read())
                        descrizioni.Add(RemoveChar(dr["descrizione"] as string, oldChar, newChar));

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