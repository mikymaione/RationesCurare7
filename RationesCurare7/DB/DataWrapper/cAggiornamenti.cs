/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2017 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Data.Common;

namespace RationesCurare7.DB.DataWrapper
{
    public class cAggiornamenti
    {

        public int EseguiUpdate()
        {
            if (cGB.UtenteConnesso.ID > 0) //utente
                return EseguiUpdateUtente();
            else
                return EseguiUpdateSistema();
        }

        private int EseguiUpdateUtente()
        {
            var i = 0;
            var sql = cDB.LeggiQuery(cDB.Queries.Aggiornamenti);
            var queries = sql.Split(new char[] { ';' });
            var UltimaDataQ = DateTime.MinValue;
            var UltimaDataU = DateTime.MinValue;

            if (cDB.UltimaDataAggiornamentiUtenti.ContainsKey(cGB.UtenteConnesso.ID))
                UltimaDataU = cDB.UltimaDataAggiornamentiUtenti[cGB.UtenteConnesso.ID];

            for (var n = 0; n < (queries?.Length ?? 0); n++)
            {
                if (queries[n].IndexOf("--") > -1)
                {
                    //0123456789
                    //--20170211
                    var h = queries[n].Substring(2, 8);
                    UltimaDataQ = DateTime.ParseExact(h, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (UltimaDataQ.CompareTo(UltimaDataU) > 0)
                    i += cDB.EseguiSQLNoQuery(queries[n]);
            }

            if (cDB.DBUtentiAggiornati.ContainsKey(cGB.UtenteConnesso.ID))
                cDB.DBUtentiAggiornati[cGB.UtenteConnesso.ID] = cGB.DBNow();
            else
                cDB.DBUtentiAggiornati.Add(cGB.UtenteConnesso.ID, cGB.DBNow());

            return i;
        }

        private int EseguiUpdateSistema()
        {
            var i = 0;
            var sql = cDB.LeggiQuery(cDB.Queries.Aggiornamenti);
            var queries = sql.Split(new char[] { ';' });
            var UltimaDataQ = DateTime.MinValue;
            var UltimaDataU = DateTime.MinValue;
            DbTransaction trans = null;

            ultimoAggiornamento();

            foreach (var ulti in cDB.UltimaDataAggiornamentiUtenti)
                if (ulti.Value > UltimaDataU)
                    UltimaDataU = ulti.Value;

            for (var n = 0; n < (queries?.Length ?? 0); n++)
            {
                if (queries[n].IndexOf("--") > -1)
                {
                    //0123456789
                    //--20170211
                    var h = queries[n].Substring(2, 8);
                    UltimaDataQ = DateTime.ParseExact(h, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (UltimaDataQ.CompareTo(UltimaDataU) > 0)
                    i += cDB.EseguiSQLNoQuery(ref trans, queries[n], null, false);
            }

            return i;
        }

        public void AggiornaDataDB()
        {
            AggiornaDataDB(DateTime.MinValue);
        }

        public void AggiornaDataDB(DateTime forza_data)
        {
            DbTransaction trans = null;
            var sql = "update utenti set UltimoAggiornamentoDB = @UltimoAggiornamentoDB where ID = @ID";

            foreach (var u in cDB.DBUtentiAggiornati)
            {
                var laD = (forza_data > DateTime.MinValue ? forza_data : u.Value);

                var p = new DbParameter[] {
                    cDB.NewPar("UltimoAggiornamentoDB", laD),
                    cDB.NewPar("ID", u.Key)
                };

                cDB.EseguiSQLNoQuery(ref trans, sql, p, false);
            }
        }

        private void ultimoAggiornamento()
        {
            try
            {
                var sql = "select ID, UltimoAggiornamentoDB from utenti";

                using (var dr = cDB.EseguiSQLDataReader(sql))
                {
                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            var id = cGB.ObjectToInt(dr["ID"], -1);
                            var d = cGB.ObjectToDateTime(dr["UltimoAggiornamentoDB"], DateTime.MinValue);
                            cDB.UltimaDataAggiornamentiUtenti.Add(id, d);
                        }

                    dr.Close();
                }
            }
            catch
            {
                //eror
            }
        }


    }
}