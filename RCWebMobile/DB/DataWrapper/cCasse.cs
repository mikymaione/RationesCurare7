using System;
using System.Collections.Generic;

namespace RationesCurare7.DB.DataWrapper
{
    public class cCasse
    {
        public double Saldo = 0;
        public string nome, nomenuovo;
        public byte[] imgName;

        public cCasse() : this("", "", null) { }

        public cCasse(string nome_, string nomenuovo_, byte[] imgName_)
        {
            nomenuovo = nomenuovo_;
            nome = nome_;
            imgName = imgName_;
        }

        public List<cCasse> CasseAggiuntive(cDB cdb, string mappath)
        {
            var casse = new List<cCasse>();

            using (var dr = cdb.EseguiSQLDataReader(cdb.LeggiQuery(mappath, cDB.Queries.Casse_Ricerca)))
            {
                if (dr.HasRows)
                {
                    var n = "";
                    var defa = new string[] { "Cassaforte", "Saldo", "Avere", "Dare", "Salvadanaio", "Portafogli" };

                    while (dr.Read())
                    {
                        var finded_ = false;
                        n = Convert.ToString(dr["nome"]);

                        for (int i = 0; i < defa.Length; i++)
                            if (defa[i].Equals(n, StringComparison.OrdinalIgnoreCase))
                            {
                                finded_ = true;
                                break;
                            }

                        if (!finded_)
                            casse.Add(new cCasse(n, n, (byte[])dr["imgName"]));
                    }
                }

                dr.Close();
            }

            return casse;
        }


    }
}