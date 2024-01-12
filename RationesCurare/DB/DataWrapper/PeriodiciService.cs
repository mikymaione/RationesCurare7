using RationesCurare7.DB;
using System;
using System.Web.SessionState;

namespace RationesCurare.DB.DataWrapper
{
    public class PeriodiciService
    {

        public void ControllaPeriodici(HttpSessionState session)
        {
            var CiSono = false;
            var c = new cPeriodici(session);
            var mov_periodici_entro_oggi = c.RicercaScadenzeEntroOggi(session);

            if (mov_periodici_entro_oggi != null)
                if (mov_periodici_entro_oggi.Count > 0)
                    CiSono = true;

            if (CiSono)
                using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
                    foreach (var pi in mov_periodici_entro_oggi)
                    {
                        var tran = db.BeginTransaction();

                        var dtd = DateTime.Now;
                        var MeseDaAggiungere_ = 0;

                        switch (pi.TipoGiornoMese)
                        {
                            case cPeriodici.ePeriodicita.G:
                                if (pi.PartendoDalGiorno.Year < 1900)
                                    dtd = GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddDays(pi.NumeroGiorni));
                                else
                                    dtd = GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.PartendoDalGiorno.Day).AddDays(pi.NumeroGiorni));
                                break;
                            case cPeriodici.ePeriodicita.M:
                                MeseDaAggiungere_ = 1;
                                break;
                            case cPeriodici.ePeriodicita.B:
                                MeseDaAggiungere_ = 2;
                                break;
                            case cPeriodici.ePeriodicita.T:
                                MeseDaAggiungere_ = 3;
                                break;
                            case cPeriodici.ePeriodicita.Q:
                                MeseDaAggiungere_ = 4;
                                break;
                            case cPeriodici.ePeriodicita.S:
                                MeseDaAggiungere_ = 6;
                                break;
                            case cPeriodici.ePeriodicita.A:
                                MeseDaAggiungere_ = 12;
                                break;
                        }

                        switch (pi.TipoGiornoMese)
                        {
                            case cPeriodici.ePeriodicita.M:
                            case cPeriodici.ePeriodicita.B:
                            case cPeriodici.ePeriodicita.T:
                            case cPeriodici.ePeriodicita.Q:
                            case cPeriodici.ePeriodicita.S:
                            case cPeriodici.ePeriodicita.A:
                                dtd = GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddMonths(MeseDaAggiungere_));
                                break;
                        }

                        var param = new System.Data.Common.DbParameter[] {
                            cDB.NewPar("nome", pi.nome, System.Data.DbType.String),
                            cDB.NewPar("tipo", pi.tipo, System.Data.DbType.String),
                            cDB.NewPar("descrizione", pi.descrizione, System.Data.DbType.String),
                            cDB.NewPar("soldi", pi.soldi, System.Data.DbType.Double),
                            cDB.NewPar("data",pi.GiornoDelMese, System.Data.DbType.DateTime),
                            cDB.NewPar("MacroArea", pi.MacroArea, System.Data.DbType.String)
                        };

                        var m = db.EseguiSQLNoQuery(ref tran, cDB.Queries.Movimenti_Inserisci, param);

                        if (pi.TipoGiorniMese == 'G')
                        {
                            pi.GiornoDelMese = new DateTime();
                            pi.PartendoDalGiorno = dtd;
                        }
                        else
                        {
                            pi.PartendoDalGiorno = new DateTime();
                            pi.GiornoDelMese = dtd;
                        }

                        var vuota = new DateTime(1, 1, 1);

                        var param2 = new[] {
                            cDB.NewPar("nome", pi.nome),
                            cDB.NewPar("tipo", pi.tipo),
                            cDB.NewPar("descrizione", pi.descrizione),
                            cDB.NewPar("soldi", pi.soldi),
                            cDB.NewPar("NumeroGiorni", pi.NumeroGiorni),
                            cDB.NewPar("GiornoDelMese", GB.ValueToDBNULL(pi.GiornoDelMese == vuota, GB.DateToOnlyDate(pi.GiornoDelMese))),
                            cDB.NewPar("PartendoDalGiorno", GB.ValueToDBNULL(pi.PartendoDalGiorno == vuota, GB.DateToOnlyDate(pi.PartendoDalGiorno))),
                            cDB.NewPar("Scadenza", GB.ValueToDBNULL(pi.Scadenza == vuota, GB.DateToOnlyDate(pi.Scadenza))),
                            cDB.NewPar("TipoGiorniMese", $"{pi.TipoGiorniMese}"),
                            cDB.NewPar("MacroArea", pi.MacroArea),
                            cDB.NewPar("ID", pi.ID)
                        };

                        var p = db.EseguiSQLNoQuery(ref tran, cDB.Queries.Periodici_Aggiorna, param2);

                        if (m + p == 2)
                            tran.Commit();
                        else
                            tran.Rollback();
                    }
        }

    }
}