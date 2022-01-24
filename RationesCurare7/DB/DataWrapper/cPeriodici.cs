/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;
using System.Data;
using RationesCurare7.GB;

namespace RationesCurare7.DB.DataWrapper
{
    public class cPeriodici : ICloneable, IComparable
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

        public int ID = -1;
        public int NumeroGiorni;
        public double soldi;
        public char TipoGiorniMese;
        public string nome, tipo, descrizione, MacroArea;
        public DateTime GiornoDelMese, PartendoDalGiorno, Scadenza;


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

        public cPeriodici() : this(-1) { }

        public cPeriodici(int ID_)
        {
            foreach (var p in Periodicita.Keys)
            {
                var c = $"{p}"[0];

                PeriodicitaS.Add(c, p);
                PeriodicitaC.Add(p, c);
            }

            if (ID_ > -1)
                CaricaByID(ID_);
        }

        public char Periodicita_cComboItems_Index(int index_periodicita)
        {
            var e = (ePeriodicita)index_periodicita;

            return $"{e}"[0];
        }

        public int Periodicita_cComboItems_Index(char periodicita)
        {
            var p = PeriodicitaS[periodicita];

            return (int)p;
        }

        public cComboItem[] Periodicita_cComboItems()
        {
            var x = -1;
            var v = new cComboItem[Periodicita.Count];

            foreach (var p in Periodicita.Keys)
                v[x += 1] = new cComboItem($"{p}", Periodicita[p]);

            return v;
        }

        public int Elimina(int ID_)
        {
            var p = new[] {
                cGB.sDB.NewPar("ID", ID_)
            };

            return cGB.sDB.EseguiSQLNoQuery(cDB.Queries.Periodici_Elimina, p);
        }

        public int Salva()
        {
            if (cGB.StringIsNullorEmpty(tipo))
            {
                return -1;
            }

            tipo = tipo.ToLower();

            return ID < 0 ? Inserisci() : Aggiorna();
        }

        private int Inserisci()
        {
            var vuota = new DateTime(1, 1, 1);

            var p = new[] {
                cGB.sDB.NewPar("nome", nome),
                cGB.sDB.NewPar("tipo", tipo),
                cGB.sDB.NewPar("descrizione", descrizione),
                cGB.sDB.NewPar("soldi", soldi),
                cGB.sDB.NewPar("NumeroGiorni", NumeroGiorni),
                cGB.sDB.NewPar("GiornoDelMese", cGB.ValueToDBNULL(GiornoDelMese == vuota, cGB.DateToOnlyDate(GiornoDelMese))),
                cGB.sDB.NewPar("PartendoDalGiorno", cGB.ValueToDBNULL(PartendoDalGiorno == vuota, cGB.DateToOnlyDate(PartendoDalGiorno))),
                cGB.sDB.NewPar("Scadenza", cGB.ValueToDBNULL(Scadenza == vuota, cGB.DateToOnlyDate(Scadenza))),
                cGB.sDB.NewPar("TipoGiorniMese", $"{TipoGiorniMese}"),
                cGB.sDB.NewPar("MacroArea", MacroArea)
            };

            return cGB.sDB.EseguiSQLNoQuery(cDB.Queries.Periodici_Inserisci, p);
        }

        private int Aggiorna()
        {
            var vuota = new DateTime(1, 1, 1);

            var p = new[] {
                cGB.sDB.NewPar("nome", nome),
                cGB.sDB.NewPar("tipo", tipo),
                cGB.sDB.NewPar("descrizione", descrizione),
                cGB.sDB.NewPar("soldi", soldi),
                cGB.sDB.NewPar("NumeroGiorni", NumeroGiorni),
                cGB.sDB.NewPar("GiornoDelMese", cGB.ValueToDBNULL(GiornoDelMese == vuota, cGB.DateToOnlyDate(GiornoDelMese))),
                cGB.sDB.NewPar("PartendoDalGiorno", cGB.ValueToDBNULL(PartendoDalGiorno == vuota, cGB.DateToOnlyDate(PartendoDalGiorno))),
                cGB.sDB.NewPar("Scadenza", cGB.ValueToDBNULL(Scadenza == vuota, cGB.DateToOnlyDate(Scadenza))),
                cGB.sDB.NewPar("TipoGiorniMese", $"{TipoGiorniMese}"),
                cGB.sDB.NewPar("MacroArea", MacroArea),
                cGB.sDB.NewPar("ID", ID)
            };

            return cGB.sDB.EseguiSQLNoQuery(cDB.Queries.Periodici_Aggiorna, p);
        }

        private void CaricaByID(int ID_)
        {
            var p = new[] {
                cGB.sDB.NewPar("ID", ID_)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cDB.Queries.Periodici_Dettaglio, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ID = cGB.ObjectToInt(dr["ID"], -1);
                        NumeroGiorni = cGB.ObjectToInt(dr["NumeroGiorni"], 0);
                        soldi = cGB.ObjectToMoney(dr["soldi"], 0D);
                        nome = Convert.ToString(dr["nome"]);
                        tipo = Convert.ToString(dr["tipo"]);
                        descrizione = Convert.ToString(dr["descrizione"]);
                        MacroArea = Convert.ToString(dr["MacroArea"]);
                        TipoGiorniMese = Convert.ToChar(dr["TipoGiorniMese"]);
                        GiornoDelMese = cGB.ObjectToDateTime(dr["GiornoDelMese"]);
                        PartendoDalGiorno = cGB.ObjectToDateTime(dr["PartendoDalGiorno"]);
                        Scadenza = cGB.ObjectToDateTime(dr["Scadenza"]);
                    }
        }

        public DataTable Ricerca()
        {
            var colonne = new[] {
                new DataColumn("ID", typeof(long)),
                new DataColumn("nome", typeof(string)),
                new DataColumn("tipo", typeof(string)),
                new DataColumn("descrizione", typeof(string)),
                new DataColumn("NumeroGiorni", typeof(int)),
                new DataColumn("GiornoDelMese", typeof(DateTime)),
                new DataColumn("TipoGiorniMese", typeof(char)),
                new DataColumn("PartendoDalGiorno", typeof(DateTime)),
                new DataColumn("MacroArea", typeof(string)),
                new DataColumn("Scadenza", typeof(DateTime)),
                new DataColumn("soldi", typeof(double)),
                new DataColumn("Periodo_H", typeof(string))
            };

            return cGB.sDB.EseguiSQLDataTable(cDB.Queries.Periodici_Ricerca, null, colonne);
        }

        public List<cPeriodici> RicercaScadenzeEntroOggi()
        {
            return RicercaScadenzeEntroOggi_plus_X_Giorni(0);
        }

        public List<cPeriodici> RicercaScadenzeEntroOggi_plus_X_Giorni(int X_Giorni)
        {
            var n = DateTime.Now;
            var da = DateTime.MinValue;
            var a = new DateTime(n.Year, n.Month, n.Day, 23, 59, 59);

            if (X_Giorni != 0)
                a = a.AddDays(X_Giorni);

            return RicercaScadenze(da, a);
        }

        public List<cPeriodici> RicercaScadenzeCalcolate(DateTime da, DateTime a)
        {
            var vuota = new DateTime(1, 1, 1);
            var n = new List<cPeriodici>();
            var c = RicercaScadenze(da, a);

            if (c != null)
            {
                foreach (var i in c)
                {
                    var data = i.TipoGiorniMese.Equals('G') ? i.PartendoDalGiorno : i.GiornoDelMese;

                    while (data < a && (data < i.Scadenza || i.Scadenza <= vuota))
                    {
                        var MeseDaAggiungere = 0;
                        var z = i.Clone() as cPeriodici;
                        z.GiornoDelMese = data;

                        n.Add(z);

                        switch (i.TipoGiornoMese)
                        {
                            case ePeriodicita.G:
                                data = data.AddDays(i.NumeroGiorni);
                                break;
                            case ePeriodicita.M:
                                MeseDaAggiungere = 1;
                                break;
                            case ePeriodicita.B:
                                MeseDaAggiungere = 2;
                                break;
                            case ePeriodicita.T:
                                MeseDaAggiungere = 3;
                                break;
                            case ePeriodicita.Q:
                                MeseDaAggiungere = 4;
                                break;
                            case ePeriodicita.S:
                                MeseDaAggiungere = 6;
                                break;
                            case ePeriodicita.A:
                                MeseDaAggiungere = 12;
                                break;
                        }

                        switch (i.TipoGiornoMese)
                        {
                            case ePeriodicita.M:
                            case ePeriodicita.B:
                            case ePeriodicita.T:
                            case ePeriodicita.Q:
                            case ePeriodicita.S:
                            case ePeriodicita.A:
                                data = data.AddMonths(MeseDaAggiungere);
                                break;
                        }
                    }
                }

                if (n.Count > 0)
                    n.Sort();
            }

            return n;
        }

        public List<cPeriodici> RicercaScadenze(DateTime da, DateTime a)
        {
            var c = new List<cPeriodici>();
            var p = new[] {
                cGB.sDB.NewPar("GiornoDa", da),
                cGB.sDB.NewPar("GiornoA", a)
            };

            using (var dr = cGB.sDB.EseguiSQLDataReader(cDB.Queries.Periodici_Scadenza, p))
                if (dr.HasRows)
                    while (dr.Read())
                        c.Add(new cPeriodici
                        {
                            ID = cGB.ObjectToInt(dr["ID"], -1),
                            NumeroGiorni = cGB.ObjectToInt(dr["NumeroGiorni"], 0),
                            soldi = cGB.ObjectToMoney(dr["soldi"], 0),
                            nome = Convert.ToString(dr["nome"]),
                            tipo = Convert.ToString(dr["tipo"]),
                            descrizione = Convert.ToString(dr["descrizione"]),
                            MacroArea = Convert.ToString(dr["MacroArea"]),
                            TipoGiorniMese = Convert.ToChar(dr["TipoGiorniMese"]),
                            GiornoDelMese = cGB.ObjectToDateTime(dr["GiornoDelMese"]),
                            PartendoDalGiorno = cGB.ObjectToDateTime(dr["PartendoDalGiorno"]),
                            Scadenza = cGB.ObjectToDateTime(dr["Scadenza"])
                        });

            return c;
        }

        public object Clone()
        {
            return new cPeriodici
            {
                descrizione = descrizione,
                GiornoDelMese = GiornoDelMese,
                ID = ID,
                MacroArea = MacroArea,
                nome = nome,
                NumeroGiorni = NumeroGiorni,
                PartendoDalGiorno = PartendoDalGiorno,
                Scadenza = Scadenza,
                soldi = soldi,
                tipo = tipo,
                TipoGiorniMese = TipoGiorniMese
            };
        }

        public int CompareTo(cPeriodici obj)
        {
            if (obj.GiornoDelMese > GiornoDelMese)
                return -1;
            if (obj.GiornoDelMese < GiornoDelMese)
                return 1;
            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is cPeriodici)
                return CompareTo((cPeriodici)obj);
            return 0;
        }


    }
}