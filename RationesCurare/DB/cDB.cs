using RationesCurare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace RationesCurare7.DB
{
    public class cDB : IDisposable
    {

        private readonly Dictionary<Queries, string> QueriesGiaLette = new Dictionary<Queries, string>();

        private readonly string dateFile;
        private readonly SQLiteConnection Connessione;

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
            DBInfo_Dettaglio,
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

        public int EseguiSQLNoQuery(SQLiteTransaction trans, Queries q, DbParameter[] param) =>
            EseguiSQLNoQuery(trans, LeggiQuery(q), param);

        private int EseguiSQLNoQuery(SQLiteTransaction trans, string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(trans, sql, param))
            {
                var i = cm.ExecuteNonQuery();

                if (i > 0)
                {
                    var a = AggiornaDataDB(trans);

                    if (a > 0)
                        AggiornaDataFile();
                    else
                        return -1;
                }

                return i;
            }
        }

        public DataTable EseguiSQLDataTable(Queries q) =>
            EseguiSQLDataTable(q, null);

        public DataTable EseguiSQLDataTable(Queries q, DbParameter[] param, long MaxRows = -1) =>
            EseguiSQLDataTable(LeggiQuery(q), param, null, MaxRows);

        private DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null, long MaxRows = -1)
        {
            var t = new DataTable();

            if (colonne != null)
                t.Columns.AddRange(colonne);

            if (MaxRows > -1)
                sql += " limit " + MaxRows;

            using (var cm = CreaCommandNoConnection(sql, param))
            using (var a = new SQLiteDataAdapter(cm))
                a.Fill(t);

            return t;
        }

        private SQLiteCommand CreaCommandNoConnection(string sql, DbParameter[] param) =>
            CreaCommandNoConnection(null, sql, param);

        private SQLiteCommand CreaCommandNoConnection(SQLiteTransaction trans, string sql, DbParameter[] param)
        {
            var cm = new SQLiteCommand(sql, Connessione, trans);

            if (param != null)
                for (int x = 0; x < param.Length; x++)
                {
                    if (param[x].DbType == DbType.Decimal)
                        param[x].DbType = DbType.Currency;

                    cm.Parameters.Add(param[x]);
                }

            return cm;
        }

        public DbDataReader EseguiSQLDataReader(Queries q) =>
            EseguiSQLDataReader(LeggiQuery(q));

        public DbDataReader EseguiSQLDataReader(Queries q, DbParameter[] param) =>
            EseguiSQLDataReader(LeggiQuery(q), param);

        private DbDataReader EseguiSQLDataReader(string sql) =>
            EseguiSQLDataReader(sql, null);

        private DbDataReader EseguiSQLDataReader(string sql, DbParameter[] param)
        {
            using (var cm = CreaCommandNoConnection(sql, param))
                return cm.ExecuteReader();
        }

        public long LastInsertRowId(SQLiteTransaction trans)
        {
            const string sql = "SELECT last_insert_rowid()";

            using (var cm = CreaCommandNoConnection(trans, sql, null))
            using (var dr = cm.ExecuteReader())
                if (dr.HasRows)
                    while (dr.Read())
                        return dr.GetInt64(0);

            return -1;
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
                var queryPath = System.IO.Path.Combine(GB.DBW, queryName);
                var z = ReadAllFile(queryPath);

                QueriesGiaLette.Add(q, z);
            }

            return QueriesGiaLette[q];
        }

        public static SQLiteParameter NewPar(string Nome, object Valore)
        {
            if (Valore is DateTime time)
                return new SQLiteParameter(Nome, DbType.String)
                {
                    Value = GB.DateTimeToSQLiteTimeStamp(time)
                };
            else
                return new SQLiteParameter(Nome, Valore);
        }

        public static SQLiteParameter NewPar(string Nome, object Valore, DbType tipo)
        {
            if (!(Valore is DBNull) && (tipo == DbType.Date || tipo == DbType.DateTime))
                return new SQLiteParameter(Nome, DbType.String)
                {
                    Value = GB.DateTimeToSQLiteTimeStamp((DateTime)Valore)
                };
            else
                return new SQLiteParameter(Nome, tipo)
                {
                    Value = Valore
                };
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
                NewPar("UltimaModifica", DateTime.Now)
            };

            using (var cm = CreaCommandNoConnection(trans, sql, pars))
                return cm.ExecuteNonQuery();
        }

    }
}