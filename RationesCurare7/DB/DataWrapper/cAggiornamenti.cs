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
            if ("".Equals(cGB.DatiUtente?.Email ?? ""))
                return EseguiUpdateSistema();
            else
                return EseguiUpdateUtente(); //utente
        }

        private int EseguiUpdateUtente()
        {
            var i = 0;
            var sql = cGB.sDB.LeggiQuery(cDB.Queries.Aggiornamenti);
            var queries = sql.Split(new char[] { ';' });
            var UltimaDataQ = DateTime.MinValue;
            var UltimaDataU = DateTime.MinValue;

            if (cGB.sDB.UltimaDataAggiornamentiUtenti.ContainsKey(cGB.DatiUtente.Email))
                UltimaDataU = cGB.sDB.UltimaDataAggiornamentiUtenti[cGB.DatiUtente.Email];

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
                    i += cGB.sDB.EseguiSQLNoQuery(queries[n]);
            }

            if (cGB.sDB.DBUtentiAggiornati.ContainsKey(cGB.DatiUtente.Email))
                cGB.sDB.DBUtentiAggiornati[cGB.DatiUtente.Email] = cGB.DBNow();
            else
                cGB.sDB.DBUtentiAggiornati.Add(cGB.DatiUtente.Email, cGB.DBNow());

            return i;
        }

        private int EseguiUpdateSistema()
        {
            var i = 0;
            var sql = cGB.sPC.LeggiQuery(cDB.Queries.Aggiornamenti);
            var queries = sql.Split(new char[] { ';' });
            var UltimaDataQ = DateTime.MinValue;
            var UltimaDataU = DateTime.MinValue;
            DbTransaction trans = null;

            ultimoAggiornamento();

            foreach (var ulti in cGB.sPC.UltimaDataAggiornamentiUtenti)
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
                    i += cGB.sPC.EseguiSQLNoQuery(ref trans, queries[n], null, false);
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
            var sql = "update DBInfo set UltimoAggiornamentoDB = @UltimoAggiornamentoDB where ID = @ID";

            foreach (var u in cGB.sDB.DBUtentiAggiornati)
            {
                var laD = (forza_data > DateTime.MinValue ? forza_data : u.Value);

                var p = new DbParameter[] {
                    cGB.sDB.NewPar("UltimoAggiornamentoDB", laD),
                    cGB.sDB.NewPar("ID", u.Key)
                };

                cGB.sDB.EseguiSQLNoQuery(ref trans, sql, p, false);
            }
        }

        private void ultimoAggiornamento()
        {
            var sql = "select Email, UltimoAggiornamentoDB from DBInfo";

            using (var dr = cGB.sDB.EseguiSQLDataReader(sql))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        var id = Convert.ToString(dr["Email"]);
                        var d = cGB.ObjectToDateTime(dr["UltimoAggiornamentoDB"], DateTime.MinValue);

                        cGB.sDB.UltimaDataAggiornamentiUtenti.Add(id, d);
                    }
        }


    }
}