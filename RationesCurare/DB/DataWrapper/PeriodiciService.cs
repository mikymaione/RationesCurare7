/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Web.SessionState;

namespace RationesCurare.DB.DataWrapper
{
    public class PeriodiciService
    {

        public void ControllaPeriodici(HttpSessionState session)
        {
            var c = new cPeriodici(session);
            var mov_periodici_entro_oggi = c.RicercaScadenzeEntroOggi(session);

            if (mov_periodici_entro_oggi != null && mov_periodici_entro_oggi.Count > 0)
                using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
                {
                    var done = 0;
                    var tot = mov_periodici_entro_oggi.Count;

                    using (var trans = db.BeginTransaction())
                    {
                        foreach (var pi in mov_periodici_entro_oggi)
                        {
                            var dtd = pi.DTD;

                            var param = new System.Data.Common.DbParameter[] {
                                cDB.NewPar("nome", pi.nome, System.Data.DbType.String),
                                cDB.NewPar("tipo", pi.tipo, System.Data.DbType.String),
                                cDB.NewPar("descrizione", pi.descrizione, System.Data.DbType.String),
                                cDB.NewPar("soldi", pi.soldi, System.Data.DbType.Double),
                                cDB.NewPar("data", pi.GiornoDelMese, System.Data.DbType.DateTime),
                                cDB.NewPar("MacroArea", pi.MacroArea, System.Data.DbType.String),
                                cDB.NewPar("IDGiroconto", -1),
                            };

                            var m = db.EseguiSQLNoQuery(trans, cDB.Queries.Movimenti_Inserisci, param);

                            if (pi.TipoGiorniMese == 'G')
                            {
                                pi.GiornoDelMese = null;
                                pi.PartendoDalGiorno = dtd;
                            }
                            else
                            {
                                pi.PartendoDalGiorno = null;
                                pi.GiornoDelMese = dtd;
                            }

                            var param2 = new[] {
                                cDB.NewPar("nome", pi.nome),
                                cDB.NewPar("tipo", pi.tipo),
                                cDB.NewPar("descrizione", pi.descrizione),
                                cDB.NewPar("soldi", pi.soldi),
                                cDB.NewPar("NumeroGiorni", pi.NumeroGiorni),
                                cDB.NewPar("GiornoDelMese", GB.ValueToDBNULL(!pi.GiornoDelMese.HasValue, ()=> GB.DateToOnlyDate(pi.GiornoDelMese.GetValueOrDefault(DateTime.Now)))),
                                cDB.NewPar("PartendoDalGiorno", GB.ValueToDBNULL(!pi.PartendoDalGiorno.HasValue, ()=> GB.DateToOnlyDate(pi.PartendoDalGiorno.GetValueOrDefault(DateTime.Now)))),
                                cDB.NewPar("Scadenza", GB.ValueToDBNULL(!pi.Scadenza.HasValue, ()=> GB.DateToOnlyDate(pi.Scadenza.GetValueOrDefault(DateTime.Now)))),
                                cDB.NewPar("TipoGiorniMese", $"{pi.TipoGiorniMese}"),
                                cDB.NewPar("MacroArea", pi.MacroArea),
                                cDB.NewPar("ID", pi.ID)
                            };

                            var p = db.EseguiSQLNoQuery(trans, cDB.Queries.Periodici_Aggiorna, param2);

                            done += m + p;
                        }

                        if (done == tot * 2)
                            trans.Commit();
                        else
                            trans.Rollback();
                    }
                }
        }

    }
}