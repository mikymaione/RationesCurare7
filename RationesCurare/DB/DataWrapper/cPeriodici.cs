using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Web.SessionState;

namespace RationesCurare.DB.DataWrapper
{
    public class cPeriodici
    {

        public enum ePeriodicita
        {
            G,
            M,
            B,
            T,
            Q,
            S,
            A,
            NOTSETTED
        }

        public long ID = -1;
        public long NumeroGiorni;
        public double soldi;
        public char TipoGiorniMese;
        public string nome, tipo, descrizione, MacroArea;
        public DateTime? GiornoDelMese, PartendoDalGiorno, Scadenza;


        private Dictionary<ePeriodicita, char> PeriodicitaC = new Dictionary<ePeriodicita, char>();
        private Dictionary<char, ePeriodicita> PeriodicitaS = new Dictionary<char, ePeriodicita>();
        private Dictionary<ePeriodicita, string> Periodicita = new Dictionary<ePeriodicita, string>
        {
            { ePeriodicita.G ,"Giornaliero" },
            { ePeriodicita.M ,"Mensile" },
            { ePeriodicita.B ,"Bimestrale" },
            { ePeriodicita.T ,"Trimestrale" },
            { ePeriodicita.Q ,"Quadrimestrale" },
            { ePeriodicita.S ,"Semestrale" },
            { ePeriodicita.A ,"Annuale" },
        };

        public DateTime DTD
        {
            get
            {
                switch (TipoGiornoMese)
                {
                    case ePeriodicita.G:
                        if (!PartendoDalGiorno.HasValue || PartendoDalGiorno.Value.Year < 1900)
                            return GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, GiornoDelMese.Value.Day).AddDays(NumeroGiorni));
                        else
                            return GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, PartendoDalGiorno.Value.Day).AddDays(NumeroGiorni));

                    default:
                        return GB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, GiornoDelMese.Value.Day).AddMonths(MeseDaAggiungere));
                }
            }
        }

        public int MeseDaAggiungere
        {
            get
            {
                switch (TipoGiornoMese)
                {
                    case ePeriodicita.M:
                        return 1;
                    case ePeriodicita.B:
                        return 2;
                    case ePeriodicita.T:
                        return 3;
                    case ePeriodicita.Q:
                        return 4;
                    case ePeriodicita.S:
                        return 6;
                    case ePeriodicita.A:
                        return 12;
                    default:
                        return 0;
                }
            }
        }

        public ePeriodicita TipoGiornoMese
        {
            get
            {
                try
                {
                    return PeriodicitaS[TipoGiorniMese];
                }
                catch
                {
                    return ePeriodicita.NOTSETTED;
                }
            }
        }

        public double soldi_d
        {
            get
            {
                return Convert.ToDouble(soldi);
            }
        }

        public cPeriodici(HttpSessionState session) : this(session, -1) { }

        public cPeriodici(HttpSessionState session, int ID_)
        {
            foreach (var p in Periodicita.Keys)
            {
                var c = $"{p}"[0];

                PeriodicitaS.Add(c, p);
                PeriodicitaC.Add(p, c);
            }

            if (ID_ > -1)
                CaricaByID(session, ID_);
        }

        private void CaricaByID(HttpSessionState session, int ID_)
        {
            var p = new[] {
                cDB.NewPar("ID", ID_)
            };

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Periodici_Dettaglio, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ID = GB.ObjectToInt(dr["ID"], -1);
                        NumeroGiorni = GB.ObjectToInt(dr["NumeroGiorni"], 0);
                        soldi = GB.ObjectToMoney(dr["soldi"], 0D);
                        nome = Convert.ToString(dr["nome"]);
                        tipo = Convert.ToString(dr["tipo"]);
                        descrizione = Convert.ToString(dr["descrizione"]);
                        MacroArea = Convert.ToString(dr["MacroArea"]);
                        TipoGiorniMese = Convert.ToChar(dr["TipoGiorniMese"]);
                        GiornoDelMese = GB.ObjectToDateTime(dr["GiornoDelMese"], null);
                        PartendoDalGiorno = GB.ObjectToDateTime(dr["PartendoDalGiorno"], null);
                        Scadenza = GB.ObjectToDateTime(dr["Scadenza"], null);
                    }
        }

        public List<cPeriodici> RicercaScadenzeEntroOggi(HttpSessionState session)
        {
            return RicercaScadenzeEntroOggi_plus_X_Giorni(session, 0);
        }

        public List<cPeriodici> RicercaScadenzeEntroOggi_plus_X_Giorni(HttpSessionState session, int X_Giorni)
        {
            var n = DateTime.Now;
            var da = DateTime.MinValue;
            var a = new DateTime(n.Year, n.Month, n.Day, 23, 59, 59);

            if (X_Giorni != 0)
                a = a.AddDays(X_Giorni);

            return RicercaScadenze(session, da, a);
        }

        public List<cPeriodici> RicercaScadenze(HttpSessionState session, DateTime da, DateTime a)
        {
            var c = new List<cPeriodici>();
            var p = new[] {
                cDB.NewPar("GiornoDa", da),
                cDB.NewPar("GiornoA", a)
            };

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.Periodici_Scadenza, p))
                if (dr.HasRows)
                    while (dr.Read())
                        c.Add(new cPeriodici(session)
                        {
                            ID = GB.ObjectToInt(dr["ID"], -1),
                            NumeroGiorni = GB.ObjectToInt(dr["NumeroGiorni"], 0),
                            soldi = GB.ObjectToMoney(dr["soldi"], 0),
                            nome = Convert.ToString(dr["nome"]),
                            tipo = Convert.ToString(dr["tipo"]),
                            descrizione = Convert.ToString(dr["descrizione"]),
                            MacroArea = Convert.ToString(dr["MacroArea"]),
                            TipoGiorniMese = Convert.ToChar(dr["TipoGiorniMese"]),
                            GiornoDelMese = GB.ObjectToDateTime(dr["GiornoDelMese"], null),
                            PartendoDalGiorno = GB.ObjectToDateTime(dr["PartendoDalGiorno"], null),
                            Scadenza = GB.ObjectToDateTime(dr["Scadenza"], null)
                        });

            return c;
        }

    }
}