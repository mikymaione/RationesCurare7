/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RationesCurare7.DB.DataWrapper
{
    public class cCalendario
    {
        public string ID, IDGruppo, Descrizione;
        public DateTime Giorno;

        public cCalendario() : this("") { }

        public cCalendario(string ID_)
        {
            if (StringIDToInt(ID_) != -1)
                CaricaByID(ID_);
        }

        public bool PresenzaPromemoria()
        {
            var Qualcosa = false;

            var n = DateTime.Now;
            var da = new DateTime(n.Year, n.Month, n.Day);
            var a = new DateTime(n.Year, n.Month, n.Day, 23, 59, 59);

            var oggi = Ricerca(da, a);

            da = da.AddDays(1);
            a = a.AddDays(1);

            var doma = Ricerca(da, a);

            if (oggi != null)
                if (oggi.Count > 0)
                    Qualcosa = true;

            if (doma != null)
                if (doma.Count > 0)
                    Qualcosa = true;

            return Qualcosa;
        }

        public List<cCalendario> Ricerca(DateTime da, DateTime a)
        {
            var x = -1;
            var c = new List<cCalendario>();
            var p = new DbParameter[2];

            p[x += 1] = cGB.sDB.NewPar("GiornoDa", da);
            p[x += 1] = cGB.sDB.NewPar("GiornoA", a);

            using (var dr = cGB.sDB.EseguiSQLDataReader(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_Ricerca), p))
            {
                if (dr.HasRows)
                    while (dr.Read())
                        c.Add(
                            new cCalendario
                            {
                                ID = ObjToIntString(dr["ID"]),
                                Giorno = cGB.ObjectToDateTime(dr["Giorno"]),
                                Descrizione = Convert.ToString(dr["Descrizione"]),
                                IDGruppo = Convert.ToString(dr["IDGruppo"])
                            }
                        );

                dr.Close();
            }

            return c;
        }

        public int Inserisci(cCalendario[] elems)
        {
            var i = 0;
            var t = cGB.sDB.CreaTransazione();

            for (var z = 0; z < elems.Length; z++)
            {
                var x = -1;
                var p = new DbParameter[4];

                p[x += 1] = cGB.sDB.NewPar("Descrizione", elems[z].Descrizione);
                p[x += 1] = cGB.sDB.NewPar("Giorno", cGB.DateToOnlyDate(elems[z].Giorno));
                p[x += 1] = cGB.sDB.NewPar("IDGruppo", elems[z].IDGruppo);
                p[x += 1] = cGB.sDB.NewPar("Inserimento", cGB.DBNow());

                i += cGB.sDB.EseguiSQLNoQuery(ref t, cGB.sDB.LeggiQuery(cDB.Queries.Calendario_Inserisci), p);
            }

            if (i >= elems.Length)
                t.Commit();
            else
                t.Rollback();

            return i;
        }

        public int Aggiorna()
        {
            var i = 0;
            var x = -1;
            var p = new DbParameter[2];

            p[x += 1] = cGB.sDB.NewPar("Descrizione", Descrizione);
            p[x += 1] = cGB.sDB.NewPar("ID", StringIDToInt(ID));

            i = cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_Aggiorna), p);

            return i;
        }

        public int AggiornaSerie()
        {
            var i = 0;
            var x = -1;
            var p = new DbParameter[2];

            p[x += 1] = cGB.sDB.NewPar("Descrizione", Descrizione);
            p[x += 1] = cGB.sDB.NewPar("IDGruppo", IDGruppo);

            i = cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_AggiornaSerie), p);

            return i;
        }

        public int Elimina(string ID_)
        {
            var i = -1;
            var x = -1;
            var p = new DbParameter[1];

            p[x += 1] = cGB.sDB.NewPar("ID", StringIDToInt(ID_));

            i = cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_Elimina), p);

            return i;
        }

        public int EliminaSerie(string IDGruppo_)
        {
            var i = -1;
            var x = -1;
            var p = new DbParameter[1];

            p[x += 1] = cGB.sDB.NewPar("IDGruppo", IDGruppo_);

            i = cGB.sDB.EseguiSQLNoQuery(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_EliminaSerie), p);

            return i;
        }

        private void CaricaByID(string ID_)
        {
            var x = -1;
            var p = new DbParameter[1];

            p[x += 1] = cGB.sDB.NewPar("ID", StringIDToInt(ID_));

            using (var dr = cGB.sDB.EseguiSQLDataReader(cGB.sDB.LeggiQuery(cDB.Queries.Calendario_Dettaglio), p))
            {
                if (dr.HasRows)
                {
                    dr.Read();

                    ID = ObjToIntString(dr["ID"]);
                    IDGruppo = Convert.ToString(dr["IDGruppo"]);
                    Giorno = Convert.ToDateTime(dr["Giorno"]);
                    Descrizione = Convert.ToString(dr["Descrizione"]);
                }

                dr.Close();
            }
        }

        private int StringIDToInt(string s)
        {
            var i = -1;

            try
            {
                s = s.Replace("A", "");
                i = Convert.ToInt32(s);
            }
            catch
            {
                //error
            }

            return i;
        }

        private string ObjToIntString(object o)
        {
            var z = "";
            var i = -1;

            try
            {
                i = Convert.ToInt32(o);

                if (i > -1)
                    z = "A" + i;
            }
            catch
            {
                //error
            }

            return z;
        }


    }
}