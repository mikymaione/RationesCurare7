using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace RationesCurare7.DB
{
    public class cDB : IDisposable
    {

        private string dateFile;

        public cDB(string path_db)
        {
            dateFile = System.IO.Path.ChangeExtension(path_db, ".date");
            ApriConnessione(path_db);
        }

        public enum Queries
        {
            Aggiornamenti,
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
            Calendario_Ricerca,
            Calendario_Inserisci,
            Calendario_Aggiorna,
            Calendario_AggiornaSerie,
            Calendario_Elimina,
            Calendario_EliminaSerie,
            Calendario_Dettaglio
        }

        private static Dictionary<Queries, string> QueriesGiaLette = new Dictionary<Queries, string>();

        private DbConnection Connessione;
        public DateTime UltimaModifica = DateTime.MinValue;

        private static string QueriesToString(Queries q)
        {
            return ConvertQueriesToString(q) + ".sql";
        }

        private static string ConvertQueriesToString(Queries q)
        {
            return q.ToString();
        }

        public DbTransaction BeginTransaction() =>
            Connessione.BeginTransaction();

        public int EseguiSQLNoQuery(Queries q, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, q, param);
        }

        public int EseguiSQLNoQuery(ref DbTransaction Trans, Queries q, DbParameter[] param)
        {
            return EseguiSQLNoQuery(ref Trans, LeggiQuery(q), param);
        }

        public int EseguiSQLNoQuery(string sql)
        {
            return EseguiSQLNoQuery(sql, null);
        }

        public int EseguiSQLNoQuery(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, sql, param);
        }

        public int EseguiSQLNoQuery(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (Trans != null)
                    cm.Transaction = Trans;

                var i = cm.ExecuteNonQuery();

                if (i > 0)
                {
                    UltimaModifica = DBNow();
                    AggiornaDataDB();
                    AggiornaDataFile();
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

        public DataTable EseguiSQLDataTable(string sql)
        {
            return EseguiSQLDataTable(sql, null);
        }

        public DataTable EseguiSQLDataTable(Queries q, DbParameter[] param, int MaxRows = -1)
        {
            return EseguiSQLDataTable(LeggiQuery(q), param, null, MaxRows);
        }

        public DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null, int MaxRows = -1)
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
            var cm = new SQLiteCommand(sql, (SQLiteConnection)Connessione);

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
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, null);
        }

        public DbDataReader EseguiSQLDataReader(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLDataReader(ref tr, sql, param);
        }

        public DbDataReader EseguiSQLDataReader(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            DbDataReader dr = null;

            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (Trans != null)
                    cm.Transaction = Trans;

                dr = cm.ExecuteReader();
            }

            return dr;
        }

        public string LeggiQuery(Queries q)
        {
            if (!QueriesGiaLette.ContainsKey(q))
            {
                var z = "";

                using (var sr = new System.IO.StreamReader(System.IO.Path.Combine(RationesCurare.GB.DBW, QueriesToString(q))))
                {
                    while (sr.Peek() != -1)
                        z += sr.ReadLine() + Environment.NewLine;

                    sr.Close();
                }

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

        public static DbParameter NewPar(string Nome, object Valore)
        {
            if (Valore is DateTime time)
            {
                return new SQLiteParameter(Nome, DbType.String)
                {
                    Value = DateToSQLite(time)
                };
            }
            else
            {
                return new SQLiteParameter(Nome, Valore);
            }
        }

        public static DbParameter NewPar(string Nome, object Valore, DbType tipo)
        {
            if (tipo == DbType.Date || tipo == DbType.DateTime)
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

        public void ChiudiConnessione()
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

        public static string DammiStringaConnessione(string path_db)
        {
            return "Data Source=" + path_db;
        }

        public void ApriConnessione(string path_db, bool ForceClose = false)
        {
            var s = DammiStringaConnessione(path_db);

            if (ForceClose)
                ChiudiConnessione();

            Connessione = new SQLiteConnection(s);
            Connessione.Open();
        }

        private void AggiornaDataFile()
        {
            var ora = DateTime.Now.ToString("yyyyMMddHHmmss");

            using (var sw = new System.IO.StreamWriter(dateFile, false))
                sw.WriteLine(ora);
        }
        private void AggiornaDataDB()
        {
            const string sql = "update DBInfo set UltimaModifica = @UltimaModifica";

            var pars = new DbParameter[] {
                NewPar("UltimaModifica", UltimaModifica)
            };

            using (var cm = CreaCommandNoConnection(sql, pars))
            {
                var r = cm.ExecuteNonQuery();

                if (r < 1)
                    throw new Exception("Can not update last modification date on user DB!");
            }
        }

        public void Dispose()
        {
            ChiudiConnessione();
        }

    }
}