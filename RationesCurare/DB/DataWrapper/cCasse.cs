using System.Collections.Generic;

namespace RationesCurare7.DB.DataWrapper
{
    public class cCasse
    {
        public double Saldo = 0;
        public string nome;

        public cCasse(string nome_)
        {
            nome = nome_;
        }

        public static List<cCasse> CasseAggiuntive(cDB cdb, string mappath)
        {
            var casse = new List<cCasse>();

            using (var dr = cdb.EseguiSQLDataReader(cdb.LeggiQuery(mappath, cDB.Queries.Casse_Ricerca)))
                if (dr.HasRows)
                    while (dr.Read())
                        casse.Add(new cCasse(dr["nome"] as string));

            return casse;
        }


    }
}