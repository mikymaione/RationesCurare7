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

namespace RationesCurare7.DB
{
    public class cDB
    {
        //TODO: [M] Inserire sul DB controparte, linkedID

        public enum DataBase
        {
            Access,
            SQLite
        }

        public enum Queries
        {
            Aggiornamenti,
            AggiornamentiDBUtente,
            Movimenti_Inserisci,
            Movimenti_Aggiorna,
            Movimenti_AggiornaMacroAree,
            Movimenti_Ricerca,
            Movimenti_Saldo,
            Movimenti_Dettaglio,
            Movimenti_Elimina,
            Movimenti_AutoCompleteSource,
            Movimenti_AutoCompleteSourceMA,
            Movimenti_MovimentiPerCassa,
            Movimenti_GetMacroAree_E_Descrizioni,
            Movimenti_GraficoTorta,
            Movimenti_GraficoAnnuale,
            Movimenti_GraficoMensile,
            Movimenti_GraficoSaldo,
            Movimenti_GraficoSaldoSpline,
            Movimenti_GraficoTortaSaldo,
            Movimenti_GraficoSplineAnnuale,
            Movimenti_Data,
            Casse_Ricerca,
            Casse_Elimina,
            Casse_Valute,
            Casse_Lista,
            Casse_ListaEX,
            Casse_Inserisci,
            Casse_Aggiorna,
            Casse_Carica,
            Periodici_Dettaglio,
            Periodici_Ricerca,
            Periodici_RicercaAccess,
            Periodici_Scadenza,
            Periodici_Elimina,
            Periodici_Inserisci,
            Periodici_Aggiorna,
            Utenti_Lista,
            Utenti_Inserisci,
            Utenti_Aggiorna,
            Utenti_Elimina,
            Utenti_Dettaglio,
            Utenti_ByPath,
            Calendario_Ricerca,
            Calendario_Inserisci,
            Calendario_Aggiorna,
            Calendario_AggiornaSerie,
            Calendario_Elimina,
            Calendario_EliminaSerie,
            Calendario_Dettaglio,
            DBInfo_Dettaglio,
            DBInfo_Inserisci,
            DBInfo_Aggiorna
        }

        private Dictionary<Queries, string> QueriesGiaLette = new Dictionary<Queries, string>();

        public DbConnection Connessione;

        public readonly bool DeveAggiornareData;
        public DateTime UltimaModifica = DateTime.MinValue;
        private DataBase DataBaseAttuale = DataBase.Access;
        public Dictionary<string, DateTime> UltimaDataAggiornamentiUtenti = new Dictionary<string, DateTime>();
        public Dictionary<string, DateTime> DBUtentiAggiornati = new Dictionary<string, DateTime>();


        public cDB(bool DeveAggiornareData_) : this(DeveAggiornareData_, DataBase.Access, "") { }

        public cDB(bool DeveAggiornareData_, DataBase db_) : this(DeveAggiornareData_, db_, "") { }

        public cDB(bool DeveAggiornareData_, string path_db = "") : this(DeveAggiornareData_, DataBase.Access, path_db) { }

        public cDB(bool DeveAggiornareData_, DataBase db_, string path_db = "")
        {
            DataBaseAttuale = db_;
            DeveAggiornareData = DeveAggiornareData_;

            var s = DammiStringaConnessione(path_db);

            try
            {
                if (DataBaseAttuale == DataBase.Access)
                    Connessione = new System.Data.OleDb.OleDbConnection(s);
                else if (DataBaseAttuale == DataBase.SQLite)
#if __MonoCS__
                    Connessione = new Mono.Data.Sqlite.SqliteConnection(s);
#else
                    Connessione = new System.Data.SQLite.SQLiteConnection(s);
#endif

                Connessione.Open();
            }
            catch (Exception ex)
            {
                cGB.MsgBox("Non riesco a connettermi al DB (" + ex.Message + ")", System.Windows.Forms.MessageBoxIcon.Error);
            }

            try
            {
                var ca = new DataWrapper.cAggiornamenti();
                ca.EseguiUpdate();
            }
            catch
            {
                //errore
            }
        }


        private string QueriesToString(Queries q)
        {
            var s = q.ToString();

            return s + ".sql";
        }

        private string DammiStringaConnessione(string path_db = "")
        {
            var s = "";
            var p = "";

            if (path_db == "")
                p = cGB.DatiDBFisico.Path;
            else
                p = path_db;

            if (DataBaseAttuale == DataBase.Access)
                s = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p + ";";
            else if (DataBaseAttuale == DataBase.SQLite)
                s = "Version=3;Data Source=" + p + ";";

            return s;
        }

        public void AggiornaDataDB()
        {
            if (DeveAggiornareData)
            {
                const string sql = "update DBInfo set UltimaModifica = @UltimaModifica";

                var cm = CreaCommandNoConnection(sql, new DbParameter[]
                {
                    NewPar("UltimaModifica", UltimaModifica)
                });

                try
                {
                    var r = cm.ExecuteNonQuery();

                    if (r < 1)
                        throw new Exception("Can not update last modification date on user DB!");
                }
                catch (Exception ex)
                {
                    //non aggiornata
                    cGB.MsgBox(ex);
                }
            }
        }

        public int EseguiSQLNoQuery(ref DbTransaction Trans, string sql, DbParameter[] param, bool AggiornaDataDiUltimaModifica = true)
        {
            var i = -1;
            var cm = CreaCommandNoConnection(sql, param);

            if ((Trans != null))
                cm.Transaction = Trans;

            try
            {
                i = cm.ExecuteNonQuery();

                if (i > 0 && AggiornaDataDiUltimaModifica && DeveAggiornareData)
                {
                    UltimaModifica = cGB.DBNow();
                    AggiornaDataDB();

                    if (DBUtentiAggiornati.ContainsKey(cGB.DatiUtente.Email))
                        DBUtentiAggiornati[cGB.DatiUtente.Email] = UltimaModifica;
                    else
                        DBUtentiAggiornati.Add(cGB.DatiUtente.Email, UltimaModifica);
                }
            }
            catch //(Exception ex)
            {
                i = -1;
                //cGB.MsgBox(ex);
            }

            return i;
        }

        public int EseguiSQLNoQuery(string sql)
        {
            return EseguiSQLNoQuery(sql, null);
        }

        public int EseguiSQLNoQuery(Queries q, DbParameter[] param, bool AggiornaDataDiUltimaModifica)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, LeggiQuery(q), param, AggiornaDataDiUltimaModifica);
        }

        public int EseguiSQLNoQuery(Queries q, DbParameter[] param)
        {
            return EseguiSQLNoQuery(LeggiQuery(q), param);
        }

        public int EseguiSQLNoQuery(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, sql, param);
        }

        public DataTable EseguiSQLDataTable(Queries q)
        {
            return EseguiSQLDataTable(q, null);
        }

        public DataTable EseguiSQLDataTable(string sql)
        {
            return EseguiSQLDataTable(sql, null);
        }

        public DataTable EseguiSQLDataTable(Queries q, DbParameter[] param)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param);
        }

        public DataTable EseguiSQLDataTable(Queries q, DbParameter[] param, DataColumn[] colonne = null)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param, colonne);
        }

        public DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null)
        {
            var t = new DataTable();

            if (colonne != null)
                t.Columns.AddRange(colonne);

            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (DataBaseAttuale == DataBase.Access)
                    using (var a = new System.Data.OleDb.OleDbDataAdapter((System.Data.OleDb.OleDbCommand)cm))
                        a.Fill(t);
                else if (DataBaseAttuale == DataBase.SQLite)
#if __MonoCS__
                    using (var a = new Mono.Data.Sqlite.SqliteDataAdapter((Mono.Data.Sqlite.SqliteCommand)cm))
                        a.Fill(t);
#else
                    using (var a = new System.Data.SQLite.SQLiteDataAdapter((System.Data.SQLite.SQLiteCommand)cm))
                        a.Fill(t);
#endif
            }

            return t;
        }

        private DbCommand CreaCommandNoConnection(string sql, DbParameter[] param)
        {
            DbCommand cm = null;

            if (DataBaseAttuale == DataBase.Access)
                cm = new System.Data.OleDb.OleDbCommand(sql, (System.Data.OleDb.OleDbConnection)Connessione);
            else if (DataBaseAttuale == DataBase.SQLite)
#if __MonoCS__
                cm = new Mono.Data.Sqlite.SqliteCommand(sql, (Mono.Data.Sqlite.SqliteConnection)Connessione);
#else
                cm = new System.Data.SQLite.SQLiteCommand(sql, (System.Data.SQLite.SQLiteConnection)Connessione);
#endif

            if (param != null)
                for (var x = 0; x < param.Length; x++)
                {
                    if (param[x].DbType == DbType.Decimal)
                        param[x].DbType = DbType.Currency;

                    cm.Parameters.Add(param[x]);
                }

            return cm;
        }

        public DbDataReader EseguiSQLDataReader(string sql)
        {
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, null);
        }

        public DbDataReader EseguiSQLDataReader(Queries q, DbParameter[] param)
        {
            return EseguiSQLDataReader(LeggiQuery(q), param);
        }

        public DbDataReader EseguiSQLDataReader(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, param);
        }

        public DbDataReader EseguiSQLDataReader(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (Trans != null)
                    cm.Transaction = Trans;

                return cm.ExecuteReader();
            }
        }

        public string LeggiQuery(Queries q)
        {
            var q2 = q;

            if (q == Queries.Periodici_Ricerca && DataBaseAttuale == DataBase.Access)
                q2 = Queries.Periodici_RicercaAccess;

            return LeggiQuery_(q2);
        }

        private string LeggiQuery_(Queries q)
        {
            if (!QueriesGiaLette.ContainsKey(q))
            {
                var z = "";
                var path_to_sql = "";
                path_to_sql = System.Windows.Forms.Application.StartupPath;
                path_to_sql = System.IO.Path.Combine(path_to_sql, "DB");
                path_to_sql = System.IO.Path.Combine(path_to_sql, "DBW");
                path_to_sql = System.IO.Path.Combine(path_to_sql, QueriesToString(q));

                using (var sr = new System.IO.StreamReader(path_to_sql))
                {
                    while (sr.Peek() != -1)
                        z += sr.ReadLine() + Environment.NewLine;

                    sr.Close();
                }

                if (DataBaseAttuale == DataBase.SQLite)
                {
                    z = z.Replace("Datepart('d',", "strftime('%d',");
                    z = z.Replace("Datepart('yyyy',", "strftime('%Y',");
                    z = z.Replace("Format(m.data, 'yyyy')", "strftime('%Y', m.data)");
                    z = z.Replace("Format(m.data, 'yyyy/mm')", "strftime('%Y/%m', m.data)");
                }

                QueriesGiaLette.Add(q, z);
            }

            return QueriesGiaLette[q];
        }

        public DbParameter[] NewPars(Dictionary<string, object> Valori)
        {
            if (Valori != null && Valori.Count > 0)
            {
                var X = -1;
                var D = new DbParameter[Valori.Count];

                foreach (var v in Valori)
                    D[X += 1] = NewPar(v.Key, v.Value);

                return D;
            }
            else
            {
                return null;
            }
        }

        public DbParameter NewPar(string Nome, object Valore)
        {
            DbParameter o = null;

            if (Valore is DateTime)
            {
                if (DataBaseAttuale == DataBase.Access)
                    o = new System.Data.OleDb.OleDbParameter(Nome, DbType.DateTime);
                else if (DataBaseAttuale == DataBase.SQLite)
                {
                    Valore = cGB.DateToSQLite((DateTime)Valore);
                    //Valore = ((DateTime)Valore).ToString("yyyy-MM-dd HH:mm:ss.fff");
#if __MonoCS__
                    o = new Mono.Data.Sqlite.SqliteParameter(Nome, DbType.String);
#else
                    o = new System.Data.SQLite.SQLiteParameter(Nome, DbType.String);
#endif
                }

                o.Value = Valore;
            }
            else
            {
                if (DataBaseAttuale == DataBase.Access)
                    o = new System.Data.OleDb.OleDbParameter(Nome, Valore);
                else if (DataBaseAttuale == DataBase.SQLite)
#if __MonoCS__
                    o = new Mono.Data.Sqlite.SqliteParameter(Nome, Valore);
#else
                    o = new System.Data.SQLite.SQLiteParameter(Nome, Valore);
#endif
            }

            return o;
        }

        public DbParameter NewPar(string Nome, object Valore, DbType tipo)
        {
            DbParameter o = null;

            if (DataBaseAttuale == DataBase.Access)
                o = new System.Data.OleDb.OleDbParameter(Nome, tipo);
            else if (DataBaseAttuale == DataBase.SQLite)
            {
                if (tipo == DbType.Date || tipo == DbType.DateTime)
                {
                    //"YYYY-MM-DD HH:MM:SS.SSS"
                    Valore = ((DateTime)Valore).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    tipo = DbType.String;
                }

#if __MonoCS__
                o = new Mono.Data.Sqlite.SqliteParameter(Nome, tipo);
#else
                o = new System.Data.SQLite.SQLiteParameter(Nome, tipo);
#endif
            }

            o.Value = Valore;

            return o;
        }

        public DbTransaction CreaTransazione()
        {
            return Connessione.BeginTransaction();
        }

        public int GetColumnType(string TName, string CName)
        {
            try
            {
                var sc = Connessione.GetSchema("Columns");

                foreach (DataRow r in sc.Rows)
                    if (r["TABLE_NAME"].Equals(TName))
                        if (r["COLUMN_NAME"].Equals(CName))
                            return Convert.ToInt32(r["DATA_TYPE"]);
            }
            catch
            {
                //error
            }

            return -1;
        }

        public HashSet<string> ParseParameters(string query_)
        {
            var paras = new HashSet<string>();
            var rxPattern = @"\@\w+";

            foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(query_, rxPattern))
                if (!paras.Contains(item.Value))
                    paras.Add(item.Value);

            return paras;
        }


    }
}