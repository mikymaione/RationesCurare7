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

                    var tran = db.BeginTransaction();

                    foreach (var pi in mov_periodici_entro_oggi)
                    {
                        var dtd = pi.DTD;

                        var param = new System.Data.Common.DbParameter[] {
                            cDB.NewPar("nome", pi.nome, System.Data.DbType.String),
                            cDB.NewPar("tipo", pi.tipo, System.Data.DbType.String),
                            cDB.NewPar("descrizione", pi.descrizione, System.Data.DbType.String),
                            cDB.NewPar("soldi", pi.soldi, System.Data.DbType.Double),
                            cDB.NewPar("data", pi.GiornoDelMese, System.Data.DbType.DateTime),
                            cDB.NewPar("MacroArea", pi.MacroArea, System.Data.DbType.String)
                        };

                        var m = db.EseguiSQLNoQuery(ref tran, cDB.Queries.Movimenti_Inserisci, param);

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
                            cDB.NewPar("GiornoDelMese", GB.ValueToDBNULL(!pi.GiornoDelMese.HasValue, GB.DateToOnlyDate(pi.GiornoDelMese.GetValueOrDefault(DateTime.Now)))),
                            cDB.NewPar("PartendoDalGiorno", GB.ValueToDBNULL(!pi.PartendoDalGiorno.HasValue, GB.DateToOnlyDate(pi.PartendoDalGiorno.GetValueOrDefault(DateTime.Now)))),
                            cDB.NewPar("Scadenza", GB.ValueToDBNULL(!pi.Scadenza.HasValue, GB.DateToOnlyDate(pi.Scadenza.GetValueOrDefault(DateTime.Now)))),
                            cDB.NewPar("TipoGiorniMese", $"{pi.TipoGiorniMese}"),
                            cDB.NewPar("MacroArea", pi.MacroArea),
                            cDB.NewPar("ID", pi.ID)
                        };

                        var p = db.EseguiSQLNoQuery(ref tran, cDB.Queries.Periodici_Aggiorna, param2);

                        done += m + p;
                    }

                    if (done == tot * 2)
                        tran.Commit();
                    else
                        tran.Rollback();
                }
        }

    }
}