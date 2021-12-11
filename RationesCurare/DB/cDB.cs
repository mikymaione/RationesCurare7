using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace RationesCurare7.DB
{
    public class cDB : IDisposable
    {

        public cDB(string path_db)
        {
            ApriConnessione(path_db);
        }

        public enum Queries
        {
            Aggiornamenti,
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
            Movimenti_GraficoTortaSaldo,
            Movimenti_Data,
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

        private struct sQueriesGiaLette
        {
            public Queries Query;
            public string SQL;
        }

        private static sQueriesGiaLette[] QueriesGiaLette = {
            new sQueriesGiaLette(){Query=Queries.Aggiornamenti},
            new sQueriesGiaLette(){Query=Queries.Casse_Aggiorna},
            new sQueriesGiaLette(){Query=Queries.Casse_Carica},
            new sQueriesGiaLette(){Query=Queries.Casse_Elimina},
            new sQueriesGiaLette(){Query=Queries.Casse_Inserisci},
            new sQueriesGiaLette(){Query=Queries.Casse_Lista},
            new sQueriesGiaLette(){Query=Queries.Casse_ListaEX},
            new sQueriesGiaLette(){Query=Queries.Casse_Ricerca},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GetMacroAree_E_Descrizioni},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Aggiorna},
            new sQueriesGiaLette(){Query=Queries.Movimenti_AggiornaMacroAree},
            new sQueriesGiaLette(){Query=Queries.Movimenti_AutoCompleteSource},
            new sQueriesGiaLette(){Query=Queries.Movimenti_AutoCompleteSourceMA},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Dettaglio},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Elimina},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Inserisci},
            new sQueriesGiaLette(){Query=Queries.Movimenti_MovimentiPerCassa},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Ricerca},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Saldo},
            new sQueriesGiaLette(){Query=Queries.Movimenti_Data},
            new sQueriesGiaLette(){Query=Queries.Periodici_Dettaglio},
            new sQueriesGiaLette(){Query=Queries.Periodici_Ricerca},
            new sQueriesGiaLette(){Query=Queries.Periodici_Scadenza},
            new sQueriesGiaLette(){Query=Queries.Periodici_Elimina},
            new sQueriesGiaLette(){Query=Queries.Periodici_Inserisci},
            new sQueriesGiaLette(){Query=Queries.Periodici_Aggiorna},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GraficoTorta},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GraficoAnnuale},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GraficoMensile},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GraficoSaldo},
            new sQueriesGiaLette(){Query=Queries.Movimenti_GraficoTortaSaldo},
            new sQueriesGiaLette(){Query=Queries.Utenti_Lista},
            new sQueriesGiaLette(){Query=Queries.Utenti_Inserisci},
            new sQueriesGiaLette(){Query=Queries.Utenti_Elimina},
            new sQueriesGiaLette(){Query=Queries.Utenti_Dettaglio},
            new sQueriesGiaLette(){Query=Queries.Utenti_ByPath},
            new sQueriesGiaLette(){Query=Queries.Utenti_Aggiorna},
            new sQueriesGiaLette(){Query=Queries.Calendario_Ricerca},
            new sQueriesGiaLette(){Query=Queries.Calendario_Inserisci},
            new sQueriesGiaLette(){Query=Queries.Calendario_Aggiorna},
            new sQueriesGiaLette(){Query=Queries.Calendario_AggiornaSerie},
            new sQueriesGiaLette(){Query=Queries.Calendario_Elimina},
            new sQueriesGiaLette(){Query=Queries.Calendario_EliminaSerie},
            new sQueriesGiaLette(){Query=Queries.Calendario_Dettaglio}
        };

        private DbConnection Connessione;
        public DateTime UltimaModifica = DateTime.MinValue;

        private static string QueriesToString(Queries q)
        {
            return ConvertQueriesToString(q) + ".sql";
        }

        private static string ConvertQueriesToString(Queries q)
        {
            switch (q)
            {
                case Queries.Aggiornamenti:
                    return "Aggiornamenti";

                case Queries.Movimenti_Inserisci:
                    return "Movimenti_Inserisci";

                case Queries.Movimenti_Aggiorna:
                    return "Movimenti_Aggiorna";

                case Queries.Movimenti_AggiornaMacroAree:
                    return "Movimenti_AggiornaMacroAree";

                case Queries.Movimenti_Ricerca:
                    return "Movimenti_Ricerca";

                case Queries.Movimenti_GetMacroAree_E_Descrizioni:
                    return "Movimenti_GetMacroAree_E_Descrizioni";

                case Queries.Movimenti_Saldo:
                    return "Movimenti_Saldo";

                case Queries.Movimenti_Dettaglio:
                    return "Movimenti_Dettaglio";

                case Queries.Movimenti_Elimina:
                    return "Movimenti_Elimina";

                case Queries.Movimenti_AutoCompleteSource:
                    return "Movimenti_AutoCompleteSource";

                case Queries.Movimenti_AutoCompleteSourceMA:
                    return "Movimenti_AutoCompleteSourceMA";

                case Queries.Movimenti_MovimentiPerCassa:
                    return "Movimenti_MovimentiPerCassa";

                case Queries.Movimenti_GraficoTorta:
                    return "Movimenti_GraficoTorta";

                case Queries.Movimenti_GraficoAnnuale:
                    return "Movimenti_GraficoAnnuale";

                case Queries.Movimenti_GraficoMensile:
                    return "Movimenti_GraficoMensile";

                case Queries.Movimenti_GraficoSaldo:
                    return "Movimenti_GraficoSaldo";

                case Queries.Movimenti_GraficoTortaSaldo:
                    return "Movimenti_GraficoTortaSaldo";

                case Queries.Movimenti_Data:
                    return "Movimenti_Data";

                case Queries.Casse_Ricerca:
                    return "Casse_Ricerca";

                case Queries.Casse_Lista:
                    return "Casse_Lista";

                case Queries.Casse_ListaEX:
                    return "Casse_ListaEX";

                case Queries.Casse_Inserisci:
                    return "Casse_Inserisci";

                case Queries.Casse_Aggiorna:
                    return "Casse_Aggiorna";

                case Queries.Casse_Carica:
                    return "Casse_Carica";

                case Queries.Casse_Elimina:
                    return "Casse_Elimina";

                case Queries.Periodici_Dettaglio:
                    return "Periodici_Dettaglio";

                case Queries.Periodici_Ricerca:
                    return "Periodici_Ricerca";

                case Queries.Periodici_Scadenza:
                    return "Periodici_Scadenza";

                case Queries.Periodici_Elimina:
                    return "Periodici_Elimina";

                case Queries.Periodici_Inserisci:
                    return "Periodici_Inserisci";

                case Queries.Periodici_Aggiorna:
                    return "Periodici_Aggiorna";

                case Queries.Utenti_Lista:
                    return "Utenti_Lista";

                case Queries.Utenti_Inserisci:
                    return "Utenti_Inserisci";

                case Queries.Utenti_Aggiorna:
                    return "Utenti_Aggiorna";

                case Queries.Utenti_Elimina:
                    return "Utenti_Elimina";

                case Queries.Utenti_Dettaglio:
                    return "Utenti_Dettaglio";

                case Queries.Utenti_ByPath:
                    return "Utenti_ByPath";

                case Queries.Calendario_Ricerca:
                    return "Calendario_Ricerca";

                case Queries.Calendario_Inserisci:
                    return "Calendario_Inserisci";

                case Queries.Calendario_Aggiorna:
                    return "Calendario_Aggiorna";

                case Queries.Calendario_AggiornaSerie:
                    return "Calendario_AggiornaSerie";

                case Queries.Calendario_Elimina:
                    return "Calendario_Elimina";

                case Queries.Calendario_EliminaSerie:
                    return "Calendario_EliminaSerie";

                case Queries.Calendario_Dettaglio:
                    return "Calendario_Dettaglio";

                default:
                    throw new NotImplementedException($"Non esiste la query {q}");
            }
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

        public int EseguiSQLNoQuery(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            var cm = CreaCommandNoConnection(sql, param);

            if (Trans != null)
                cm.Transaction = Trans;

            try
            {
                return cm.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
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
            var iq = -1;
            var z = "";

            for (int i = 0; i < QueriesGiaLette.Length; i++)
                if (QueriesGiaLette[i].Query == q)
                {
                    iq = i;
                    z = QueriesGiaLette[i].SQL;
                    break;
                }

            if (z == "" || z == null)
            {
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

                QueriesGiaLette[iq].SQL = z;
            }

            return z;
        }
        public static string DateToSQLite(DateTime d)
        {
            //yyyy-MM-dd HH:mm:ss.fff
            string h = "";

            h += d.Year + "-" + (d.Month < 10 ? "0" : "") + d.Month + "-" + (d.Day < 10 ? "0" : "") + d.Day + " ";
            h += (d.Hour < 10 ? "0" : "") + d.Hour + ":" + (d.Minute < 10 ? "0" : "") + d.Minute + ":" + (d.Second < 10 ? "0" : "") + d.Second + ".000";

            return h;
        }

        public DbParameter NewPar(string Nome, object Valore)
        {
            if (Valore is DateTime)
            {
                Valore = DateToSQLite((DateTime)Valore);

                var o = new SQLiteParameter(Nome, DbType.String);
                o.Value = Valore;

                return o;
            }
            else
            {
                return new SQLiteParameter(Nome, Valore);
            }
        }

        public DbParameter NewPar(string Nome, object Valore, DbType tipo)
        {
            if (tipo == DbType.Date || tipo == DbType.DateTime)
            {
                //"YYYY-MM-DD HH:MM:SS.SSS"
                Valore = ((DateTime)Valore).ToString("yyyy-MM-dd HH:mm:ss");
                tipo = DbType.String;
            }

            var o = new SQLiteParameter(Nome, tipo);

            o.Value = Valore;

            return o;
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

        private void AggiornaDataDB()
        {
            var sql = "update utenti set UltimaModifica = @UltimaModifica";
            var cm = CreaCommandNoConnection(sql, new DbParameter[] { NewPar("UltimaModifica", UltimaModifica) });

            try
            {
                cm.ExecuteNonQuery();
            }
            catch
            {
                //non aggiornata
            }
        }

        public void Dispose()
        {
            ChiudiConnessione();
        }

    }
}