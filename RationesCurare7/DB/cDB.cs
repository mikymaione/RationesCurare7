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
            Calendario_Dettaglio
        }

        private struct sQueriesGiaLette
        {
            public Queries Query;
            public string SQL;
        }

        private static sQueriesGiaLette[] QueriesGiaLette = {
            new sQueriesGiaLette(){ Query = Queries.Aggiornamenti },
            new sQueriesGiaLette(){ Query = Queries.AggiornamentiDBUtente },
            new sQueriesGiaLette(){ Query = Queries.Casse_Aggiorna },
            new sQueriesGiaLette(){ Query = Queries.Casse_Carica },
            new sQueriesGiaLette(){ Query = Queries.Casse_Elimina },
            new sQueriesGiaLette(){ Query = Queries.Casse_Inserisci },
            new sQueriesGiaLette(){ Query = Queries.Casse_Lista },
            new sQueriesGiaLette(){ Query = Queries.Casse_Valute },
            new sQueriesGiaLette(){ Query = Queries.Casse_ListaEX },
            new sQueriesGiaLette(){ Query = Queries.Casse_Ricerca },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GetMacroAree_E_Descrizioni },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Aggiorna },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_AggiornaMacroAree },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_AutoCompleteSource },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_AutoCompleteSourceMA },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Dettaglio },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Elimina },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Inserisci },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_MovimentiPerCassa },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Ricerca },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Saldo },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_Data },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Dettaglio },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Ricerca },
            new sQueriesGiaLette(){ Query = Queries.Periodici_RicercaAccess },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Scadenza },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Elimina },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Inserisci },
            new sQueriesGiaLette(){ Query = Queries.Periodici_Aggiorna },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoSplineAnnuale },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoTorta },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoAnnuale },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoMensile },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoSaldo },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoSaldoSpline },
            new sQueriesGiaLette(){ Query = Queries.Movimenti_GraficoTortaSaldo },
            new sQueriesGiaLette(){ Query = Queries.Utenti_Lista },
            new sQueriesGiaLette(){ Query = Queries.Utenti_Inserisci },
            new sQueriesGiaLette(){ Query = Queries.Utenti_Elimina },
            new sQueriesGiaLette(){ Query = Queries.Utenti_Dettaglio },
            new sQueriesGiaLette(){ Query = Queries.Utenti_ByPath },
            new sQueriesGiaLette(){ Query = Queries.Utenti_Aggiorna },
            new sQueriesGiaLette(){ Query = Queries.Calendario_Ricerca },
            new sQueriesGiaLette(){ Query = Queries.Calendario_Inserisci },
            new sQueriesGiaLette(){ Query = Queries.Calendario_Aggiorna },
            new sQueriesGiaLette(){ Query = Queries.Calendario_AggiornaSerie },
            new sQueriesGiaLette(){ Query = Queries.Calendario_Elimina },
            new sQueriesGiaLette(){ Query = Queries.Calendario_EliminaSerie },
            new sQueriesGiaLette(){ Query = Queries.Calendario_Dettaglio }
        };

        private static DbConnection Connessione;
        public static DateTime UltimaModifica = DateTime.MinValue;
        private static DataBase DataBaseAttuale = DataBase.Access;
        public static Dictionary<int, DateTime> UltimaDataAggiornamentiUtenti = new Dictionary<int, DateTime>();
        public static Dictionary<int, DateTime> DBUtentiAggiornati = new Dictionary<int, DateTime>();

        private static string QueriesToString(Queries q)
        {
            var s = "";

            switch (q)
            {
                case Queries.Aggiornamenti:
                    s = "Aggiornamenti";
                    break;
                case Queries.AggiornamentiDBUtente:
                    s = "AggiornamentiDBUtente";
                    break;
                case Queries.Movimenti_Inserisci:
                    s = "Movimenti_Inserisci";
                    break;
                case Queries.Movimenti_Aggiorna:
                    s = "Movimenti_Aggiorna";
                    break;
                case Queries.Movimenti_AggiornaMacroAree:
                    s = "Movimenti_AggiornaMacroAree";
                    break;
                case Queries.Movimenti_Ricerca:
                    s = "Movimenti_Ricerca";
                    break;
                case Queries.Movimenti_GetMacroAree_E_Descrizioni:
                    s = "Movimenti_GetMacroAree_E_Descrizioni";
                    break;
                case Queries.Movimenti_Saldo:
                    s = "Movimenti_Saldo";
                    break;
                case Queries.Movimenti_Dettaglio:
                    s = "Movimenti_Dettaglio";
                    break;
                case Queries.Movimenti_Elimina:
                    s = "Movimenti_Elimina";
                    break;
                case Queries.Movimenti_AutoCompleteSource:
                    s = "Movimenti_AutoCompleteSource";
                    break;
                case Queries.Movimenti_AutoCompleteSourceMA:
                    s = "Movimenti_AutoCompleteSourceMA";
                    break;
                case Queries.Movimenti_MovimentiPerCassa:
                    s = "Movimenti_MovimentiPerCassa";
                    break;
                case Queries.Movimenti_GraficoTorta:
                    s = "Movimenti_GraficoTorta";
                    break;
                case Queries.Movimenti_GraficoAnnuale:
                    s = "Movimenti_GraficoAnnuale";
                    break;
                case Queries.Movimenti_GraficoMensile:
                    s = "Movimenti_GraficoMensile";
                    break;
                case Queries.Movimenti_GraficoSaldo:
                    s = "Movimenti_GraficoSaldo";
                    break;
                case Queries.Movimenti_GraficoSaldoSpline:
                    s = "Movimenti_GraficoSaldoSpline";
                    break;
                case Queries.Movimenti_GraficoTortaSaldo:
                    s = "Movimenti_GraficoTortaSaldo";
                    break;
                case Queries.Movimenti_GraficoSplineAnnuale:
                    s = "Movimenti_GraficoSplineAnnuale";
                    break;
                case Queries.Movimenti_Data:
                    s = "Movimenti_Data";
                    break;
                case Queries.Casse_Ricerca:
                    s = "Casse_Ricerca";
                    break;
                case Queries.Casse_Lista:
                    s = "Casse_Lista";
                    break;
                case Queries.Casse_ListaEX:
                    s = "Casse_ListaEX";
                    break;
                case Queries.Casse_Valute:
                    s = "Casse_Valute";
                    break;
                case Queries.Casse_Inserisci:
                    s = "Casse_Inserisci";
                    break;
                case Queries.Casse_Aggiorna:
                    s = "Casse_Aggiorna";
                    break;
                case Queries.Casse_Carica:
                    s = "Casse_Carica";
                    break;
                case Queries.Casse_Elimina:
                    s = "Casse_Elimina";
                    break;
                case Queries.Periodici_Dettaglio:
                    s = "Periodici_Dettaglio";
                    break;
                case Queries.Periodici_Ricerca:
                    s = "Periodici_Ricerca";
                    break;
                case Queries.Periodici_RicercaAccess:
                    s = "Periodici_RicercaAccess";
                    break;
                case Queries.Periodici_Scadenza:
                    s = "Periodici_Scadenza";
                    break;
                case Queries.Periodici_Elimina:
                    s = "Periodici_Elimina";
                    break;
                case Queries.Periodici_Inserisci:
                    s = "Periodici_Inserisci";
                    break;
                case Queries.Periodici_Aggiorna:
                    s = "Periodici_Aggiorna";
                    break;
                case Queries.Utenti_Lista:
                    s = "Utenti_Lista";
                    break;
                case Queries.Utenti_Inserisci:
                    s = "Utenti_Inserisci";
                    break;
                case Queries.Utenti_Aggiorna:
                    s = "Utenti_Aggiorna";
                    break;
                case Queries.Utenti_Elimina:
                    s = "Utenti_Elimina";
                    break;
                case Queries.Utenti_Dettaglio:
                    s = "Utenti_Dettaglio";
                    break;
                case Queries.Utenti_ByPath:
                    s = "Utenti_ByPath";
                    break;
                case Queries.Calendario_Ricerca:
                    s = "Calendario_Ricerca";
                    break;
                case Queries.Calendario_Inserisci:
                    s = "Calendario_Inserisci";
                    break;
                case Queries.Calendario_Aggiorna:
                    s = "Calendario_Aggiorna";
                    break;
                case Queries.Calendario_AggiornaSerie:
                    s = "Calendario_AggiornaSerie";
                    break;
                case Queries.Calendario_Elimina:
                    s = "Calendario_Elimina";
                    break;
                case Queries.Calendario_EliminaSerie:
                    s = "Calendario_EliminaSerie";
                    break;
                case Queries.Calendario_Dettaglio:
                    s = "Calendario_Dettaglio";
                    break;
            }

            return s + ".sql";
        }

        private static string DammiStringaConnessione(string path_db = "")
        {
            var s = "";
            var p = "";

            if (path_db == "")
                p = cGB.UtenteConnesso.PathDB;
            else
                p = path_db;

            if (DataBaseAttuale == DataBase.Access)
                s = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p + ";";
            else if (DataBaseAttuale == DataBase.SQLite)
                s = "Version=3;Data Source=" + p + ";";

            return s;
        }

        public static void AggiornaDataDB()
        {
            const string sql = "update utenti set UltimaModifica = @UltimaModifica";

            var cm = CreaCommandNoConnection(sql, new DbParameter[] {
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

        public static int EseguiSQLNoQuery(ref DbTransaction Trans, string sql, DbParameter[] param, bool AggiornaDataDiUltimaModifica = true)
        {
            var i = -1;
            var cm = CreaCommandNoConnection(sql, param);

            if ((Trans != null))
                cm.Transaction = Trans;

            try
            {
                i = cm.ExecuteNonQuery();

                if (i > 0 && AggiornaDataDiUltimaModifica)
                {
                    UltimaModifica = cGB.DBNow();
                    AggiornaDataDB();

                    if (DBUtentiAggiornati.ContainsKey(cGB.UtenteConnesso.ID))
                        DBUtentiAggiornati[cGB.UtenteConnesso.ID] = UltimaModifica;
                    else
                        DBUtentiAggiornati.Add(cGB.UtenteConnesso.ID, UltimaModifica);
                }
            }
            catch //(Exception ex)
            {
                i = -1;
                //cGB.MsgBox(ex);
            }

            return i;
        }

        public static int EseguiSQLNoQuery(string sql)
        {
            return EseguiSQLNoQuery(sql, null);
        }

        public static int EseguiSQLNoQuery(Queries q, DbParameter[] param)
        {
            return EseguiSQLNoQuery(LeggiQuery(q), param);
        }

        public static int EseguiSQLNoQuery(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, sql, param);
        }

        public static DataTable EseguiSQLDataTable(Queries q)
        {
            return EseguiSQLDataTable(q, null);
        }

        public static DataTable EseguiSQLDataTable(string sql)
        {
            return EseguiSQLDataTable(sql, null);
        }

        public static DataTable EseguiSQLDataTable(Queries q, DbParameter[] param)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param);
        }

        public static DataTable EseguiSQLDataTable(Queries q, DbParameter[] param, DataColumn[] colonne = null)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param, colonne);
        }

        public static DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null)
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

        private static DbCommand CreaCommandNoConnection(string sql, DbParameter[] param)
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

        public static DbDataReader EseguiSQLDataReader(string sql)
        {
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, null);
        }

        public static DbDataReader EseguiSQLDataReader(Queries q, DbParameter[] param)
        {
            return EseguiSQLDataReader(LeggiQuery(q), param);
        }

        public static DbDataReader EseguiSQLDataReader(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, param);
        }

        public static DbDataReader EseguiSQLDataReader(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (Trans != null)
                    cm.Transaction = Trans;

                return cm.ExecuteReader();
            }
        }

        public static string LeggiQuery(Queries q)
        {
            var q2 = q;

            if (q == Queries.Periodici_Ricerca && DataBaseAttuale == DataBase.Access)
                q2 = Queries.Periodici_RicercaAccess;

            return LeggiQuery_(q2);
        }

        private static string LeggiQuery_(Queries q)
        {
            var iq = -1;
            var z = "";

            for (var i = 0; i < QueriesGiaLette.Length; i++)
                if (QueriesGiaLette[i].Query == q)
                {
                    iq = i;
                    z = QueriesGiaLette[i].SQL;
                    break;
                }

            if (cGB.StringIsNullorEmpty(z))
            {
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
                    z = z.Replace("Format(m.data, 'yyyy')", "strftime('%Y',m.data)");
                    z = z.Replace("Format(m.data, 'yyyy/mm')", "strftime('%Y/%m',m.data)");
                }

                QueriesGiaLette[iq].SQL = z;
            }

            return z;
        }

        public static DbParameter[] NewPars(Dictionary<string, object> Valori)
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

        public static DbParameter NewPar(string Nome, object Valore)
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

        public static DbParameter NewPar(string Nome, object Valore, DbType tipo)
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

        public static void ChiudiConnessione()
        {
            try
            {
                Connessione.Close();
                Connessione.Dispose();
            }
            catch
            {
                //cannot close               
            }
        }

        public static void ApriConnessione(bool ForceClose)
        {
            ApriConnessione(DataBase.Access, "", ForceClose);
        }

        public static void ApriConnessione(DataBase db_, bool ForceClose)
        {
            ApriConnessione(db_, "", ForceClose);
        }

        public static void ApriConnessione(string path_db = "", bool ForceClose = false)
        {
            ApriConnessione(DataBase.Access, path_db, ForceClose);
        }

        public static void ApriConnessione(DataBase db_, string path_db = "", bool ForceClose = false)
        {
            DataBaseAttuale = db_;
            var s = DammiStringaConnessione(path_db);

            if (ForceClose)
                ChiudiConnessione();

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

        public static DbTransaction CreaTransazione()
        {
            return Connessione.BeginTransaction();
        }

        public static int GetColumnType(string TName, string CName)
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

        public static HashSet<string> ParseParameters(string query_)
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