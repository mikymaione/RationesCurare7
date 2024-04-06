using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Web.SessionState;

namespace RationesCurare.DB.DataWrapper
{
    public class CMovimenti
    {

        public List<string> GetMacroAree(HttpSessionState session)
        {
            var macroaree = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSourceMA))
                if (dr.HasRows)
                    while (dr.Read())
                        macroaree.Add(dr["MacroArea"] as string);

            return macroaree;
        }

        public List<string> GetDescrizioni(HttpSessionState session)
        {
            var descrizioni = new List<string>();

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Movimenti_AutoCompleteSource))
                if (dr.HasRows)
                    while (dr.Read())
                        descrizioni.Add(dr["descrizione"] as string);

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