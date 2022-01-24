/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;

namespace RationesCurare7.DB.DataWrapper
{
    public class cUtenti
    {
        public int ID = -1;
        public string Nome, Email, Psw, Path, TipoDB;

        public cUtenti() : this(-1) { }

        public cUtenti(int ID_)
        {
            if (ID_ > -1)
                CaricaByID(ID_);
        }

        public void SetTipoDBByExtension(string ext)
        {
            ext = ext.Replace(".", "");
            TipoDB = ext.Equals("rqd8", StringComparison.OrdinalIgnoreCase) ? "S" : "A";
        }

        public int Salva() =>
            ID < 0 ? Inserisci() : Aggiorna();

        private int Aggiorna() =>
            cGB.sPC.EseguiSQLNoQuery(
                cGB.sPC.LeggiQuery(cDB.Queries.Utenti_Aggiorna),
                new[] {
                    cGB.sPC.NewPar("Nome", Nome),
                    cGB.sPC.NewPar("Psw", Psw),
                    cGB.sPC.NewPar("Path", Path),
                    cGB.sPC.NewPar("Email", Email),
                    cGB.sPC.NewPar("TipoDB", TipoDB),
                    cGB.sPC.NewPar("ID", ID)
                }
            );

        private int Inserisci() =>
            cGB.sPC.EseguiSQLNoQuery(
                cGB.sPC.LeggiQuery(cDB.Queries.Utenti_Inserisci),
                new[] {
                    cGB.sPC.NewPar("Nome", Nome),
                    cGB.sPC.NewPar("Psw", Psw),
                    cGB.sPC.NewPar("Path", Path),
                    cGB.sPC.NewPar("Email", Email),
                    cGB.sPC.NewPar("TipoDB", TipoDB)
                }
            );

        public int Elimina(int ID__) =>
            cGB.sPC.EseguiSQLNoQuery(
                cGB.sPC.LeggiQuery(cDB.Queries.Utenti_Elimina),
                new[]
                {
                    cGB.sPC.NewPar("ID", ID__)
                }
            );

        public List<cUtenti> ListaUtenti()
        {
            var cc = new List<cUtenti>();

            using (var dr = cGB.sPC.EseguiSQLDataReader(cGB.sPC.LeggiQuery(cDB.Queries.Utenti_Lista)))
                if (dr.HasRows)
                    while (dr.Read())
                        cc.Add(new cUtenti
                        {
                            ID = cGB.ObjectToInt(dr["ID"], -1),
                            Nome = Convert.ToString(dr["Nome"]),
                            Psw = Convert.ToString(dr["Psw"]),
                            Path = Convert.ToString(dr["path"]),
                            Email = Convert.ToString(dr["Email"]),
                            TipoDB = dr["TipoDB"].ToString()
                        });

            return cc;
        }

        private void CaricaByID(int ID__) =>
            CaricaBy(ID__, "");

        public void CaricaByPath(string z) =>
            CaricaBy(-1, z);

        private void CaricaBy(int ID__, string path_)
        {
            var q = cGB.sPC.LeggiQuery(ID__ == -1 ? cDB.Queries.Utenti_ByPath : cDB.Queries.Utenti_Dettaglio);

            var p = new[]
            {
                ID__ == -1 ? cGB.sPC.NewPar("path", path_) : cGB.sPC.NewPar("ID", ID__)
            };

            using (var dr = cGB.sPC.EseguiSQLDataReader(q, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ID = cGB.ObjectToInt(dr["ID"], -1);
                        Nome = Convert.ToString(dr["nome"]);
                        Psw = Convert.ToString(dr["Psw"]);
                        Email = Convert.ToString(dr["Email"]);
                        Path = Convert.ToString(dr["path"]);
                        TipoDB = Convert.ToString(dr["TipoDB"]);
                    }
        }

    }
}