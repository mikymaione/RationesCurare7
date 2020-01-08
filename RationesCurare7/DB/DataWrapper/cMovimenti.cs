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
using System.Data.Common;

namespace RationesCurare7.DB.DataWrapper
{
    public class cMovimenti
    {
        public struct sMacroArea_e_Descrizione
        {
            public string Descrizione, MacroArea;
        }

        public int ID = -1;
        public string nome, descrizione, tipo, MacroArea;
        public double soldi = 0;
        public DateTime data = DateTime.Now;


        //filtri
        public DateTime DataDa, DataA;
        public double SoldiDa, SoldiA;
        public bool bData = false, bSoldi = false;

        public cMovimenti() : this(-1) { }

        public cMovimenti(int ID_)
        {
            if (ID_ > -1)
                CaricaByID(ID_);
        }

        public int Elimina(int ID_)
        {
            var i = -1;
            var x = -1;
            var p = new DbParameter[1];

            p[x += 1] = DB.cDB.NewPar("ID", ID_);

            i = DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Movimenti_Elimina), p);

            return i;
        }

        public int Salva()
        {
            var j = -1;

            if (tipo != null)
                if (tipo != "")
                {
                    tipo = tipo.ToLower();

                    if (ID < 0)
                        j = Inserisci();
                    else
                        j = Aggiorna();
                }

            return j;
        }

        private int Inserisci()
        {
            var i = 0;
            var x = -1;
            var p = new DbParameter[6];

            p[x += 1] = DB.cDB.NewPar("nome", nome);
            p[x += 1] = DB.cDB.NewPar("tipo", tipo);
            p[x += 1] = DB.cDB.NewPar("descrizione", descrizione);
            p[x += 1] = DB.cDB.NewPar("soldi", soldi);
            p[x += 1] = DB.cDB.NewPar("data", data);
            p[x += 1] = DB.cDB.NewPar("MacroArea", MacroArea);

            i = DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Movimenti_Inserisci), p);

            return i;
        }

        private int Aggiorna()
        {
            var x = -1;
            var p = new DbParameter[7];

            p[x += 1] = DB.cDB.NewPar("nome", nome);
            p[x += 1] = DB.cDB.NewPar("tipo", tipo);
            p[x += 1] = DB.cDB.NewPar("descrizione", descrizione);
            p[x += 1] = DB.cDB.NewPar("soldi", soldi);
            p[x += 1] = DB.cDB.NewPar("data", data);
            p[x += 1] = DB.cDB.NewPar("MacroArea", MacroArea);
            p[x += 1] = DB.cDB.NewPar("ID", ID);

            return DB.cDB.EseguiSQLNoQuery(DB.cDB.LeggiQuery(cDB.Queries.Movimenti_Aggiorna), p);
        }

        public double Saldo(string tipo_)
        {
            var s = 0D;

            if (!tipo_.Equals("Movimenti Periodici", StringComparison.OrdinalIgnoreCase))
                s = Saldo_(tipo_);

            return s;
        }

        private double Saldo_(string tipo_)
        {
            var p = new DbParameter[] {
                cDB.NewPar("tipo", tipo_),
                cDB.NewPar("descrizione", (descrizione == null ? "%%" : descrizione)),
                cDB.NewPar("MacroArea", (MacroArea == null ? "%%" : MacroArea)),
                cDB.NewPar("bSoldi", (bSoldi ? 1 : 0)),
                cDB.NewPar("SoldiDa", SoldiDa),
                cDB.NewPar("SoldiA", SoldiA),
                cDB.NewPar("bData", (bData ? 1 : 0)),
                cDB.NewPar("DataDa", DataDa, DbType.Date),
                cDB.NewPar("DataA", DataA, DbType.Date),
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_Saldo), p))
                if (dr.HasRows)
                    while (dr.Read())
                        try
                        {
                            return dr.GetDouble(0);
                        }
                        catch
                        {
                            // vuoto
                        }

            return 0;
        }

        public int NumeroMovimentiPerCassa(string cassa)
        {
            var p = new DbParameter[] {
                cDB.NewPar("tipo", cassa)
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_MovimentiPerCassa), p))
                if (dr.HasRows)
                    while (dr.Read())
                        return cGB.ObjectToInt(dr["Tot"], 0);

            return 0;
        }

        public string[] TutteLeDescrizioni_Array_ToArray(bool Tutti = true) =>
            TutteLeDescrizioni_Array(Tutti).ToArray();

        public List<string> TutteLeDescrizioni_Array(bool Tutti = true)
        {
            var z = new List<string>();
            var p = new DbParameter[]
            {
                cDB.NewPar("MacroArea", Tutti ? "FALSE" : "TRUE")
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_AutoCompleteSource), p))
                if (dr.HasRows)
                    while (dr.Read())
                        z.Add(dr["descrizione"].ToString());

            return z;
        }

        public System.Windows.Forms.AutoCompleteStringCollection TutteLeDescrizioni()
        {
            var z = new System.Windows.Forms.AutoCompleteStringCollection();
            var c = TutteLeDescrizioni_Array_ToArray();

            if (c != null)
                if (c.Length > 0)
                    z.AddRange(c);

            return z;
        }

        public System.Windows.Forms.AutoCompleteStringCollection TutteLeMacroAree()
        {
            var z = new System.Windows.Forms.AutoCompleteStringCollection();

            using (var dr = DB.cDB.EseguiSQLDataReader(DB.cDB.LeggiQuery(cDB.Queries.Movimenti_AutoCompleteSourceMA)))
                if (dr.HasRows)
                    while (dr.Read())
                        z.Add(Convert.ToString(dr["MacroArea"]));

            return z;
        }

        public List<sMacroArea_e_Descrizione> MacroAree_e_Descrizioni()
        {
            var z = new List<sMacroArea_e_Descrizione>();

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_GetMacroAree_E_Descrizioni)))
                if (dr.HasRows)
                    while (dr.Read())
                        z.Add(new sMacroArea_e_Descrizione()
                        {
                            Descrizione = dr["descrizione"].ToString(),
                            MacroArea = dr["MacroArea"].ToString()
                        });

            return z;
        }

        public DataTable Ricerca()
        {
            string query;

            return Ricerca(out query);
        }

        public DataTable Ricerca(out string query)
        {
            var p = new DbParameter[] {
                cDB.NewPar("tipo", tipo),
                cDB.NewPar("descrizione", (descrizione == null ? "%%" : descrizione)),
                cDB.NewPar("MacroArea", (MacroArea == null ? "%%" : MacroArea)),
                cDB.NewPar("bSoldi", (bSoldi ? 1 : 0)),
                cDB.NewPar("SoldiDa", SoldiDa),
                cDB.NewPar("SoldiA", SoldiA),
                cDB.NewPar("bData", (bData ? 1 : 0)),
                cDB.NewPar("DataDa", cGB.DateToDBDateNullable(DataDa)),
                cDB.NewPar("DataA", cGB.DateToDBDateNullable(DataA)),
            };

            var colonne = new DataColumn[] {
                new DataColumn("ID", typeof(Int64)),
                new DataColumn("nome", typeof(String)),
                new DataColumn("data", typeof(DateTime)),
                new DataColumn("tipo", typeof(String)),
                new DataColumn("descrizione", typeof(String)),
                new DataColumn("MacroArea", typeof(String)),
                new DataColumn("soldi", typeof(Double))
            };

            query = cDB.LeggiQuery(cDB.Queries.Movimenti_Ricerca);

            return cDB.EseguiSQLDataTable(query, p, colonne);
        }

        public double RicercaGraficoSaldo(bool UsaParametri = true)
        {
            var p = UsaParametri ?
                new DbParameter[] {
                    cDB.NewPar("descrizione", descrizione == null ? "%%" : descrizione),
                    cDB.NewPar("MacroArea", MacroArea == null ? "%%" : MacroArea),
                    cDB.NewPar("DataDa", DataDa),
                    cDB.NewPar("DataA", DataA),
                } :
                new DbParameter[0];

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(UsaParametri ? cDB.Queries.Movimenti_GraficoSaldo : cDB.Queries.Movimenti_GraficoSaldoSpline), p))
                if (dr.HasRows)
                    while (dr.Read())
                        return cGB.ObjectToMoney(dr["Soldini_TOT"]);

            return 0;
        }

        public double RicercaGraficoTortaSaldo(int positivita)
        {
            var p = new DbParameter[] {
                cDB.NewPar("sPos", positivita, DbType.Int32),
                cDB.NewPar("DataDa", DataDa, DbType.Date),
                cDB.NewPar("DataA", DataA, DbType.Date),
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_GraficoTortaSaldo), p))
                if (dr.HasRows)
                    while (dr.Read())
                        return cGB.ObjectToMoney(dr["Soldini_TOT"]);

            return 0;
        }

        public DbDataReader RicercaGraficoSpline() =>
            cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_GraficoSplineAnnuale));

        public DbDataReader RicercaGrafico(bool Annuale)
        {
            var s = cDB.LeggiQuery(Annuale ? cDB.Queries.Movimenti_GraficoAnnuale : cDB.Queries.Movimenti_GraficoMensile);

            var p = new DbParameter[] {
                cDB.NewPar("descrizione", (descrizione == null ? "%%" : descrizione)),
                cDB.NewPar("MacroArea", (MacroArea == null ? "%%" : MacroArea)),
                cDB.NewPar("DataDa", DataDa),
                cDB.NewPar("DataA", DataA)
            };

            return cDB.EseguiSQLDataReader(s, p);
        }

        public DbDataReader RicercaTorta(int positivita)
        {
            var s = cDB.LeggiQuery(cDB.Queries.Movimenti_GraficoTorta);

            var p = new DbParameter[] {
                cDB.NewPar("sPos", positivita, DbType.Int32),
                cDB.NewPar("DataDa", DataDa, DbType.Date),
                cDB.NewPar("DataA", DataA, DbType.Date)
            };

            return cDB.EseguiSQLDataReader(s, p);
        }

        private void CaricaByID(int ID_)
        {
            var p = new DbParameter[] {
                cDB.NewPar("ID", ID_)
            };

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_Dettaglio), p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        ID = cGB.ObjectToInt(dr["ID"], -1);
                        data = Convert.ToDateTime(dr["data"]);
                        descrizione = Convert.ToString(dr["descrizione"]);
                        MacroArea = Convert.ToString(dr["MacroArea"]);
                        tipo = Convert.ToString(dr["tipo"]);
                        nome = Convert.ToString(dr["nome"]);
                        soldi = cGB.ObjectToMoney(dr["soldi"]);
                    }
        }

        public int AggiornaMacroAree(List<sMacroArea_e_Descrizione> m)
        {
            var r = 0;

            if (m != null)
                if (m.Count > 0)
                {
                    var z = cDB.LeggiQuery(cDB.Queries.Movimenti_AggiornaMacroAree);
                    var t = cDB.CreaTransazione();

                    foreach (var e in m)
                    {
                        var p = new DbParameter[] {
                            cDB.NewPar("MacroArea", e.MacroArea),
                            cDB.NewPar("descrizione", e.Descrizione)
                        };

                        r += cDB.EseguiSQLNoQuery(ref t, z, p);
                    }

                    t.Commit();
                }

            return r;
        }

        public void IntervalloDate(out DateTime Da, out DateTime A)
        {
            var defa1 = new DateTime(2005, 1, 1);
            var defa2 = DateTime.Now.AddYears(1);

            Da = defa1;
            A = defa2;

            using (var dr = cDB.EseguiSQLDataReader(cDB.LeggiQuery(cDB.Queries.Movimenti_Data)))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        Da = cGB.ObjectToDateTime(dr["minData"], defa1, -1);
                        A = cGB.ObjectToDateTime(dr["maxData"], defa2, 1);
                    }
        }

    }
}