/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RationesCurare7.DB.DataWrapper
{
    public class cUtente
    {
        public int ID = -1;
        public string nome, psw, path, Email, TipoDB;
        public DateTime UltimoAggiornamentoDB = DateTime.Now.AddYears(-10);
        public DateTime UltimaModifica = DateTime.Now.AddYears(-5);

        public cUtente() : this(-1) { }

        public cUtente(int ID_)
        {
            if (ID_ > -1)
                CaricaByID(ID_);
        }

        public void SetTipoDBByExtension(String ext)
        {
            ext = ext.Replace(".", "");
            TipoDB = (ext.Equals("rqd8", StringComparison.OrdinalIgnoreCase) ? "S" : "A");
        }

        public int Salva()
        {
            var i = 0;

            if (ID < 0)
            {
                i = Inserisci();
            }
            else
            {
                i = Aggiorna();
            }

            return i;
        }

        private int Aggiorna()
        {
            var p = new DbParameter[] {
                DB.cDB.NewPar("nome", nome),
                DB.cDB.NewPar("psw", psw),
                DB.cDB.NewPar("path", path),
                DB.cDB.NewPar("Email", Email),
                DB.cDB.NewPar("TipoDB", TipoDB),
                DB.cDB.NewPar("ID", ID)
            };

            return DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Utenti_Aggiorna), p);
        }

        private int Inserisci()
        {
            var p = new DbParameter[] {
                DB.cDB.NewPar("nome", nome),
                DB.cDB.NewPar("psw", psw),
                DB.cDB.NewPar("path", path),
                DB.cDB.NewPar("Email", Email),
                DB.cDB.NewPar("TipoDB", TipoDB),
                DB.cDB.NewPar("UltimoAggiornamentoDB", UltimoAggiornamentoDB)
            };

            return DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Utenti_Inserisci), p);
        }

        public int Elimina(int ID__)
        {
            var p = new DbParameter[] {
                cDB.NewPar("ID", ID__)
            };

            return cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Utenti_Elimina), p);
        }

        public List<cUtente> ListaUtenti()
        {
            var cc = new List<cUtente>();

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Utenti_Lista)))
            {
                if (dr.HasRows)
                    while (dr.Read())
                        cc.Add(new cUtente()
                        {
                            ID = cGB.ObjectToInt(dr["ID"], -1),
                            nome = Convert.ToString(dr["nome"]),
                            psw = Convert.ToString(dr["psw"]),
                            path = Convert.ToString(dr["path"]),
                            Email = Convert.ToString(dr["Email"]),
                            UltimaModifica = cGB.ObjectToDateTime(dr["UltimaModifica"], DateTime.Now.AddYears(-5)),
                            TipoDB = dr["TipoDB"].ToString()
                        });

                dr.Close();
            }

            return cc;
        }

        private void CaricaByID(int ID__)
        {
            CaricaBy(ID__, "");
        }

        public void CaricaByPath(string z)
        {
            CaricaBy(-1, z);
        }

        private void CaricaBy(int ID__, string path_)
        {
            var q = cDB.LeggiQuery(ID__ == -1 ? cDB.Queries.Utenti_ByPath : cDB.Queries.Utenti_Dettaglio);

            var p = new DbParameter[] {
                (ID__ == -1 ? cDB.NewPar("path", path_) : cDB.NewPar("ID", ID__))
            };

            using (var dr = cDB.EseguiSQLDataReader(q, p))
            {
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ID = cGB.ObjectToInt(dr["ID"], -1);
                        nome = Convert.ToString(dr["nome"]);
                        psw = Convert.ToString(dr["psw"]);
                        Email = Convert.ToString(dr["Email"]);
                        path = Convert.ToString(dr["path"]);
                        UltimaModifica = cGB.ObjectToDateTime(dr["UltimaModifica"], DateTime.Now.AddYears(-5));
                        UltimoAggiornamentoDB = cGB.ObjectToDateTime(dr["UltimoAggiornamentoDB"], DateTime.Now.AddYears(-50));
                        TipoDB = Convert.ToString(dr["TipoDB"]);
                    }

                dr.Close();
            }
        }


    }
}