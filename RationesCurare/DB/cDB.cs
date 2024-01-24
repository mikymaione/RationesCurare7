using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;

namespace RationesCurare7.DB
{
    public class cDB : IDisposable
    {

        private readonly Dictionary<Queries, string> QueriesGiaLette = new Dictionary<Queries, string>();

        private readonly string dateFile;
        private readonly SQLiteConnection Connessione;
        private DateTime UltimaModifica = DateTime.MinValue;

        public cDB(string path_db)
        {
            var connectionString = $"Data Source={path_db}";
            Connessione = new SQLiteConnection(connectionString);
            Connessione.Open();

            dateFile = System.IO.Path.ChangeExtension(path_db, ".date");
        }

        public void Dispose()
        {
            Connessione?.Close();
            Connessione?.Dispose();
        }

        public enum Queries
        {
            Aggiornamenti,
            DBInfo_Inserisci,
            Movimenti_Inserisci,
            Movimenti_Aggiorna,
            Movimenti_AggiornaMacroAree,
            Movimenti_Ricerca,
            Movimenti_RicercaSaldo,
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
            Movimenti_GraficoTortaSaldo,
            Movimenti_Data,
            Movimenti_SaldoPerCassa,
            Casse_Ricerca,
            Casse_Elimina,
            Casse_Lista,
            Casse_ListaEX,
            Casse_Inserisci,
            Casse_Aggiorna,
            Casse_Carica,
            Periodici_Dettaglio,
            Periodici_Ricerca,
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
            Utente_Carica,
            Calendario_Ricerca,
            Calendario_Inserisci,
            Calendario_Aggiorna,
            Calendario_AggiornaSerie,
            Calendario_Elimina,
            Calendario_EliminaSerie,
            Calendario_Dettaglio
        }

        public SQLiteTransaction BeginTransaction() =>
            Connessione.BeginTransaction();

        public int EseguiSQLNoQueryAutoCommit(Queries q, DbParameter[] param)
        {
            using (var trans = BeginTransaction())
            {
                var r = EseguiSQLNoQuery(trans, LeggiQuery(q), param);

                if (r > 0)
                    trans.Commit();
                else
                    trans.Rollback();

                return r;
            }
        }

        public int EseguiSQLNoQuery(SQLiteTransaction trans, Queries q, DbParameter[] param)
        {
            return EseguiSQLNoQuery(trans, LeggiQuery(q), param);
        }

        public int EseguiSQLNoQuery(SQLiteTransaction trans, string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(trans, sql, param))
            {
                var i = cm.ExecuteNonQuery();

                if (i > 0)
                {
                    UltimaModifica = DBNow();

                    var a = AggiornaDataDB(trans);

                    if (a > 0)
                        AggiornaDataFile();
                    else
                        return -1;
                }

                return i;
            }
        }

        public static DateTime DBNow()
        {
            var d = DateTime.Now;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        public DataTable EseguiSQLDataTable(Queries q)
        {
            return EseguiSQLDataTable(q, null);
        }

        public DataTable EseguiSQLDataTable(Queries q, DbParameter[] param, long MaxRows = -1)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param, null, MaxRows);
        }

        public DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null, long MaxRows = -1)
        {
            var t = new DataTable();

            if (colonne != null)
                t.Columns.AddRange(colonne);

            if (MaxRows > -1)
                sql += " limit " + MaxRows;

            using (var cm = CreaCommandNoConnection(sql, param))
            using (var a = new SQLiteDataAdapter((SQLiteCommand)cm))
                a.Fill(t);

            return t;
        }

        private DbCommand CreaCommandNoConnection(string sql, DbParameter[] param)
        {
            return CreaCommandNoConnection(null, sql, param);
        }

        private DbCommand CreaCommandNoConnection(SQLiteTransaction trans, string sql, DbParameter[] param)
        {
            var cm = new SQLiteCommand(sql, Connessione, trans);

            if (param != null)
                for (int x = 0; x <= param.Length - 1; x++)
                {
                    if (param[x].DbType == DbType.Decimal)
                        param[x].DbType = DbType.Currency;

                    cm.Parameters.Add(param[x]);
                }

            return cm;
        }

        public DbDataReader EseguiSQLDataReader(Queries q)
        {
            return EseguiSQLDataReader(LeggiQuery(q));
        }

        public DbDataReader EseguiSQLDataReader(Queries q, DbParameter[] param)
        {
            return EseguiSQLDataReader(LeggiQuery(q), param);
        }

        public DbDataReader EseguiSQLDataReader(string sql)
        {
            return EseguiSQLDataReader(sql, null);
        }

        public DbDataReader EseguiSQLDataReader(string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(sql, param))
                return cm.ExecuteReader();
        }

        private string ReadAllFile(string path)
        {
            using (var sr = new System.IO.StreamReader(path))
                return sr.ReadToEnd();
        }

        private string LeggiQuery(Queries q)
        {
            if (!QueriesGiaLette.ContainsKey(q))
            {
                var queryName = $"{q}.sql";
                var queryPath = System.IO.Path.Combine(RationesCurare.GB.DBW, queryName);

                var z = ReadAllFile(queryPath);
                z = z.Replace("Datepart('d',", "strftime('%d',");
                z = z.Replace("Datepart('yyyy',", "strftime('%Y',");
                z = z.Replace("Format(m.data, 'yyyy')", "strftime('%Y',m.data)");
                z = z.Replace("Format(m.data, 'yyyy/mm')", "strftime('%Y/%m',m.data)");

                QueriesGiaLette.Add(q, z);
            }

            return QueriesGiaLette[q];
        }

        public static string DateToSQLite(DateTime d)
        {
            //yyyy-MM-dd HH:mm:ss.fff
            string h = "";

            h += d.Year + "-" + (d.Month < 10 ? "0" : "") + d.Month + "-" + (d.Day < 10 ? "0" : "") + d.Day + " ";
            h += (d.Hour < 10 ? "0" : "") + d.Hour + ":" + (d.Minute < 10 ? "0" : "") + d.Minute + ":" + (d.Second < 10 ? "0" : "") + d.Second + ".000";

            return h;
        }

        public static SQLiteParameter NewPar(string Nome, object Valore)
        {
            if (Valore is DateTime time)
                return new SQLiteParameter(Nome, DbType.String)
                {
                    Value = DateToSQLite(time)
                };
            else
                return new SQLiteParameter(Nome, Valore);
        }

        public static SQLiteParameter NewPar(string Nome, object Valore, DbType tipo)
        {
            if (!(Valore is DBNull) && (tipo == DbType.Date || tipo == DbType.DateTime))
            {
                //"YYYY-MM-DD HH:MM:SS.SSS"                
                return new SQLiteParameter(Nome, DbType.String)
                {
                    Value = ((DateTime)Valore).ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
            else
            {
                return new SQLiteParameter(Nome, tipo)
                {
                    Value = Valore
                };
            }
        }

        private void AggiornaDataFile()
        {
            var ora = DateTime.Now.ToString("yyyyMMddHHmmss");

            using (var sw = new System.IO.StreamWriter(dateFile, false))
                sw.WriteLine(ora);
        }

        private int AggiornaDataDB(SQLiteTransaction trans)
        {
            const string sql = "update DBInfo set UltimaModifica = @UltimaModifica";

            var pars = new SQLiteParameter[] {
                NewPar("UltimaModifica", UltimaModifica)
            };

            using (var cm = CreaCommandNoConnection(trans, sql, pars))
                return cm.ExecuteNonQuery();
        }

    }
}