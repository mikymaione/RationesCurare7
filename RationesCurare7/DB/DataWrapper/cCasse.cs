/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace RationesCurare7.DB.DataWrapper
{
    public class cCasse
    {
        public double Saldo = 0;
        public string nome, nomenuovo, Valuta;
        public byte[] imgName;


        public cCasse() : this("", "", "", null) { }

        public cCasse(string nome_, string nomenuovo_, string Valuta_, byte[] imgName_)
        {
            nomenuovo = nomenuovo_;
            nome = nome_;
            Valuta = Valuta_;
            imgName = imgName_;

            if (nome != "")
                if (nomenuovo_ == "")
                    CaricaByID();
        }

        public System.Data.DataTable ListaCasse(string cassa_da_escludere)
        {
            int x = -1;
            DbParameter[] p = new DbParameter[1];

            p[x += 1] = DB.cDB.NewPar("Nome", cassa_da_escludere);

            return DB.cDB.EseguiSQLDataTable(DB.cDB.LeggiQuery(cDB.Queries.Casse_ListaEX), p);
        }

        public System.Data.DataTable ListaValute()
        {
            return DB.cDB.EseguiSQLDataTable(DB.cDB.LeggiQuery(cDB.Queries.Casse_Valute));
        }

        public Dictionary<string, string> ListaCasseValute()
        {
            var r = new Dictionary<string, string>();

            using (var dr = DB.cDB.EseguiSQLDataReader(DB.cDB.LeggiQuery(cDB.Queries.Casse_Ricerca)))
            {
                while (dr.HasRows && dr.Read())
                    r.Add(dr["nome"] as string, dr["Valuta"] as string);

                dr.Close();
            }

            return r;
        }

        public System.Data.DataTable ListaCasse()
        {
            return DB.cDB.EseguiSQLDataTable(DB.cDB.LeggiQuery(cDB.Queries.Casse_Lista));
        }

        public System.Data.DataTable Ricerca()
        {
            return DB.cDB.EseguiSQLDataTable(DB.cDB.LeggiQuery(cDB.Queries.Casse_Ricerca));
        }

        public int Elimina(string ID_)
        {
            int i = -1;
            int x = -1;
            DbParameter[] p = new DbParameter[1];

            p[x += 1] = DB.cDB.NewPar("nome", ID_);

            i = DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Casse_Elimina), p);

            return i;
        }

        public int Salva()
        {
            int i = 0;

            if (nome == "")
            {
                i = Inserisci();
            }
            else
            {
                i = Modifica();
            }

            return i;
        }

        private int Inserisci()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nomenuovo", nomenuovo),
                cDB.NewPar("Valuta", Valuta),
                cDB.NewPar("imgName", imgName)
            };

            return cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Casse_Inserisci), p);
        }

        private int Modifica()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nomenuovo", nomenuovo),
                cDB.NewPar("Valuta", Valuta),
                cDB.NewPar("imgName", imgName),
                cDB.NewPar("nome", nome)
            };

            return cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Casse_Aggiorna), p);
        }

        public List<cCasse> CasseAggiuntive()
        {
            var cc = new List<cCasse>();

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Casse_Ricerca)))
            {
                if (dr.HasRows)
                {
                    var n = "";
                    var defa = new string[] { "Cassaforte", "Saldo", "Avere", "Dare", "Salvadanaio", "Portafogli" };

                    while (dr.Read())
                    {
                        var finded_ = false;
                        n = Convert.ToString(dr["nome"]);

                        for (var i = 0; i < defa.Length; i++)
                            if (defa[i].Equals(n, StringComparison.OrdinalIgnoreCase))
                            {
                                finded_ = true;
                                break;
                            }

                        if (!finded_)
                            cc.Add(
                                new cCasse(
                                    n,
                                    n,
                                    dr["Valuta"] as string,
                                    dr["imgName"] as byte[]
                                )
                            );
                    }
                }

                dr.Close();
            }

            return cc;
        }

        private void CaricaByID()
        {
            var p = new DbParameter[] {
                cDB.NewPar("nome", nome)
            };

            using (var dr = DB.cDB.EseguiSQLDataReader(DB.cDB.LeggiQuery(cDB.Queries.Casse_Carica), p))
            {
                while (dr.HasRows && dr.Read())
                {
                    imgName = dr["imgName"] as byte[];
                    nome = dr["nome"] as string;
                    Valuta = dr["Valuta"] as string;
                    nomenuovo = nome;
                }

                dr.Close();
            }
        }


    }
}