/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
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
        public string nome, nomenuovo;
        public byte[] imgName;

        public cCasse() : this("", "", null, false) { }

        public cCasse(string nome_, string nomenuovo_, byte[] imgName_, bool Nascondi_)
        {
            Nascondi = Nascondi_;
            nomenuovo = nomenuovo_;
            nome = nome_;
            imgName = imgName_;

            if (!nome.Equals(""))
                if (nomenuovo_.Equals(""))
                    CaricaByID();
        }


        public System.Data.DataTable ListaCasse(string cassa_da_escludere)
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("Nome", cassa_da_escludere)
            };

            return cGB.sDB.EseguiSQLDataTable(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_ListaEX), p);
        }

        public HashSet<string> ListaCasseTree()
        {
            var r = new HashSet<string>();

            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("MostraTutte", 1)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cGB.sDB.LeggiQuery(cDB.Queries.Casse_Ricerca), p))
                if (dr.HasRows)
                    while (dr.Read())
                        r.Add(dr["nome"] as string);

            return r;
        }

        public System.Data.DataTable ListaCasse() =>
            cGB.sDB.EseguiSQLDataTable(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Lista));

        public System.Data.DataTable Ricerca()
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("MostraTutte", 1)
            };

            return cGB.sDB.EseguiSQLDataTable(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Ricerca), p);
        }

        public int Elimina(string ID_)
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("nome", ID_)
            };

            return cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Elimina), p);
        }

        public int Salva() =>
            nome.Equals("") ? Inserisci() : Modifica();

        private int Inserisci()
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("nomenuovo", nomenuovo),
                cGB.sDB.NewPar("imgName", imgName),
                cGB.sDB.NewPar("Nascondi", Nascondi)
            };

            return cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Inserisci), p);
        }

        private int Modifica()
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("nomenuovo", nomenuovo),
                cGB.sDB.NewPar("imgName", imgName),
                cGB.sDB.NewPar("nome", nome),
                cGB.sDB.NewPar("Nascondi", Nascondi)
            };

            return cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Aggiorna), p);
        }

        public List<cCasse> CasseAggiuntive(bool MostraTutte)
        {
            var cc = new List<cCasse>();

            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("MostraTutte", MostraTutte? 1 : 0)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Ricerca), p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        var n = dr["nome"] as string;

                        cc.Add(new cCasse(
                            n,
                            n,
                            dr["imgName"] as byte[],
                            System.Convert.ToBoolean(dr["Nascondi"])
                        ));
                    }

            return cc;
        }

        private void CaricaByID()
        {
            var p = new DbParameter[]
            {
                cGB.sDB.NewPar("nome", nome)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cGB.sDB.LeggiQuery(DB.cDB.Queries.Casse_Carica), p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        imgName = dr["imgName"] as byte[];
                        nome = dr["nome"] as string;
                        Nascondi = System.Convert.ToBoolean(dr["Nascondi"]);
                        nomenuovo = nome;
                    }
        }

    }
}