/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System.Collections.Generic;
using System.Data.Common;

namespace RationesCurare7.DB.DataWrapper
{
    public class cCasse
    {
        public bool Nascondi;
        public double Saldo = 0;
        public string nome, nomenuovo, Valuta;
        public byte[] imgName;

        public static HashSet<string> CasseDefault = new HashSet<string>() { "cassaforte", "saldo", "avere", "dare", "salvadanaio", "portafogli" };


        public cCasse() : this("", "", "", null, false) { }

        public cCasse(string nome_, string nomenuovo_, string Valuta_, byte[] imgName_, bool Nascondi_)
        {
            Nascondi = Nascondi_;
            nomenuovo = nomenuovo_;
            nome = nome_;
            Valuta = Valuta_;
            imgName = imgName_;

            if (!nome.Equals(""))
                if (nomenuovo_.Equals(""))
                    CaricaByID();
        }


        public System.Data.DataTable ListaCasse(string cassa_da_escludere)
        {
            var p = new DbParameter[] {
                cDB.NewPar("Nome", cassa_da_escludere)
            };

            return cDB.EseguiSQLDataTable(cDB.LeggiQuery(cDB.Queries.Casse_ListaEX), p);
        }

        public System.Data.DataTable ListaValute() =>
            cDB.EseguiSQLDataTable(cDB.LeggiQuery(cDB.Queries.Casse_Valute));

        public Dictionary<string, string> ListaCasseValute()
        {
            var r = new Dictionary<string, string>();

            var p = new DbParameter[] {
                cDB.NewPar("MostraTutte", 1)
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Casse_Ricerca), p))
                if (dr.HasRows)
                    while (dr.Read())
                        r.Add(dr["nome"] as string, dr["Valuta"] as string);

            return r;
        }

        public System.Data.DataTable ListaCasse() =>
            cDB.EseguiSQLDataTable(cDB.LeggiQuery(cDB.Queries.Casse_Lista));

        public System.Data.DataTable Ricerca()
        {
            var p = new DbParameter[] {
                cDB.NewPar("MostraTutte", 1)
            };

            return cDB.EseguiSQLDataTable(cDB.LeggiQuery(cDB.Queries.Casse_Ricerca), p);
        }

        public int Elimina(string ID_)
        {
            var p = new DbParameter[] {
                cDB.NewPar("nome", ID_)
            };

            return cDB.EseguiSQLNoQuery(cDB.LeggiQuery(cDB.Queries.Casse_Elimina), p);
        }

        public int Salva() =>
            nome.Equals("") ? Inserisci() : Modifica();

        private int Inserisci()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nomenuovo", nomenuovo),
                cDB.NewPar("Valuta", Valuta),
                cDB.NewPar("imgName", imgName)
            };

            return cDB.EseguiSQLNoQuery(cDB.LeggiQuery(cDB.Queries.Casse_Inserisci), p);
        }

        private int Modifica()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nomenuovo", nomenuovo),
                cDB.NewPar("Valuta", Valuta),
                cDB.NewPar("imgName", imgName),
                cDB.NewPar("nome", nome),
                cDB.NewPar("Nascondi", Nascondi)
            };

            return cDB.EseguiSQLNoQuery(cDB.LeggiQuery(cDB.Queries.Casse_Aggiorna), p);
        }

        public List<cCasse> CasseAggiuntive(bool MostraTutte)
        {
            var cc = new List<cCasse>();

            var p = new DbParameter[] {
                cDB.NewPar("MostraTutte", MostraTutte? 1 : 0)
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Casse_Ricerca), p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        var n = dr["nome"] as string;

                        if (!CasseDefault.Contains(n))
                            cc.Add(new cCasse(
                                n,
                                n,
                                dr["Valuta"] as string,
                                dr["imgName"] as byte[],
                                System.Convert.ToBoolean(dr["Nascondi"])
                            ));
                    }

            return cc;
        }

        private void CaricaByID()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nome", nome)
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Casse_Carica), p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        imgName = dr["imgName"] as byte[];
                        nome = dr["nome"] as string;
                        Valuta = dr["Valuta"] as string;
                        Nascondi = System.Convert.ToBoolean(dr["Nascondi"]);
                        nomenuovo = nome;
                    }
        }

    }
}