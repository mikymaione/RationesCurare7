using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace RationesCurare7.DB
{
    public class cDB : IDisposable
    {
        //TODO: [M] Inserire sul DB controparte, linkedID

        public enum DataBase
        {
            Access,
            SQLite
        }

        public cDB(DataBase db_, string path_db)
        {
            ApriConnessione(db_, path_db);
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
        private DataBase DataBaseAttuale = DataBase.Access;
        public DateTime UltimaModifica = DateTime.MinValue;

        private static string QueriesToString(Queries q)
        {
            var s = "";

            if (q == Queries.Aggiornamenti)
                s = "Aggiornamenti";
            else if (q == Queries.Movimenti_Inserisci)
                s = "Movimenti_Inserisci";
            else if (q == Queries.Movimenti_Aggiorna)
                s = "Movimenti_Aggiorna";
            else if (q == Queries.Movimenti_AggiornaMacroAree)
                s = "Movimenti_AggiornaMacroAree";
            else if (q == Queries.Movimenti_Ricerca)
                s = "Movimenti_Ricerca";
            else if (q == Queries.Movimenti_GetMacroAree_E_Descrizioni)
                s = "Movimenti_GetMacroAree_E_Descrizioni";
            else if (q == Queries.Movimenti_Saldo)
                s = "Movimenti_Saldo";
            else if (q == Queries.Movimenti_Dettaglio)
                s = "Movimenti_Dettaglio";
            else if (q == Queries.Movimenti_Elimina)
                s = "Movimenti_Elimina";
            else if (q == Queries.Movimenti_AutoCompleteSource)
                s = "Movimenti_AutoCompleteSource";
            else if (q == Queries.Movimenti_AutoCompleteSourceMA)
                s = "Movimenti_AutoCompleteSourceMA";
            else if (q == Queries.Movimenti_MovimentiPerCassa)
                s = "Movimenti_MovimentiPerCassa";
            else if (q == Queries.Movimenti_GraficoTorta)
                s = "Movimenti_GraficoTorta";
            else if (q == Queries.Movimenti_GraficoAnnuale)
                s = "Movimenti_GraficoAnnuale";
            else if (q == Queries.Movimenti_GraficoMensile)
                s = "Movimenti_GraficoMensile";
            else if (q == Queries.Movimenti_GraficoSaldo)
                s = "Movimenti_GraficoSaldo";
            else if (q == Queries.Movimenti_GraficoTortaSaldo)
                s = "Movimenti_GraficoTortaSaldo";
            else if (q == Queries.Movimenti_Data)
                s = "Movimenti_Data";
            else if (q == Queries.Casse_Ricerca)
                s = "Casse_Ricerca";
            else if (q == Queries.Casse_Lista)
                s = "Casse_Lista";
            else if (q == Queries.Casse_ListaEX)
                s = "Casse_ListaEX";
            else if (q == Queries.Casse_Inserisci)
                s = "Casse_Inserisci";
            else if (q == Queries.Casse_Aggiorna)
                s = "Casse_Aggiorna";
            else if (q == Queries.Casse_Carica)
                s = "Casse_Carica";
            else if (q == Queries.Casse_Elimina)
                s = "Casse_Elimina";
            else if (q == Queries.Periodici_Dettaglio)
                s = "Periodici_Dettaglio";
            else if (q == Queries.Periodici_Ricerca)
                s = "Periodici_Ricerca";
            else if (q == Queries.Periodici_Scadenza)
                s = "Periodici_Scadenza";
            else if (q == Queries.Periodici_Elimina)
                s = "Periodici_Elimina";
            else if (q == Queries.Periodici_Inserisci)
                s = "Periodici_Inserisci";
            else if (q == Queries.Periodici_Aggiorna)
                s = "Periodici_Aggiorna";
            else if (q == Queries.Utenti_Lista)
                s = "Utenti_Lista";
            else if (q == Queries.Utenti_Inserisci)
                s = "Utenti_Inserisci";
            else if (q == Queries.Utenti_Aggiorna)
                s = "Utenti_Aggiorna";
            else if (q == Queries.Utenti_Elimina)
                s = "Utenti_Elimina";
            else if (q == Queries.Utenti_Dettaglio)
                s = "Utenti_Dettaglio";
            else if (q == Queries.Utenti_ByPath)
                s = "Utenti_ByPath";
            else if (q == Queries.Calendario_Ricerca)
                s = "Calendario_Ricerca";
            else if (q == Queries.Calendario_Inserisci)
                s = "Calendario_Inserisci";
            else if (q == Queries.Calendario_Aggiorna)
                s = "Calendario_Aggiorna";
            else if (q == Queries.Calendario_AggiornaSerie)
                s = "Calendario_AggiornaSerie";
            else if (q == Queries.Calendario_Elimina)
                s = "Calendario_Elimina";
            else if (q == Queries.Calendario_EliminaSerie)
                s = "Calendario_EliminaSerie";
            else if (q == Queries.Calendario_Dettaglio)
                s = "Calendario_Dettaglio";

            return s + ".sql";
        }

        public int EseguiSQLNoQuery(string sql)
        {
            return EseguiSQLNoQuery(sql, null);
        }

        public int EseguiSQLNoQuery(ref DbTransaction Trans, string sql, DbParameter[] param)
        {
            var i = -1;
            var cm = CreaCommandNoConnection(sql, param);

            if ((Trans != null))
                cm.Transaction = Trans;

            try
            {
                i = cm.ExecuteNonQuery();
            }
            catch
            {
                i = -1;
            }

            return i;
        }

        public int EseguiSQLNoQuery(string sql, DbParameter[] param)
        {
            DbTransaction tr = null;

            return EseguiSQLNoQuery(ref tr, sql, param);
        }

        public DataTable EseguiSQLDataTable(string mappath, Queries q)
        {
            return EseguiSQLDataTable(mappath, q, null);
        }

        public DataTable EseguiSQLDataTable(string sql)
        {
            return EseguiSQLDataTable(sql, null);
        }

        public DataTable EseguiSQLDataTable(string mappath, Queries q, DbParameter[] param, int MaxRows = -1)
        {
            return EseguiSQLDataTable(LeggiQuery(mappath, q), param, null, MaxRows);
        }

        public DataTable EseguiSQLDataTable(string sql, DbParameter[] param, DataColumn[] colonne = null, int MaxRows = -1)
        {
            var t = new DataTable();

            if (colonne != null)
                t.Columns.AddRange(colonne);

            if (MaxRows > -1)
                if (DataBaseAttuale == DataBase.Access)
                    sql = Regex.Replace(sql, "select", "select top " + MaxRows, RegexOptions.IgnoreCase);
                else if (DataBaseAttuale == DataBase.SQLite)
                    sql += " limit " + MaxRows;

            using (var cm = CreaCommandNoConnection(sql, param))
            {
                if (DataBaseAttuale == DataBase.Access)
                    using (var a = new System.Data.OleDb.OleDbDataAdapter((System.Data.OleDb.OleDbCommand)cm))
                        a.Fill(t);
                else if (DataBaseAttuale == DataBase.SQLite)
                    using (var a = new System.Data.SQLite.SQLiteDataAdapter((System.Data.SQLite.SQLiteCommand)cm))
                        a.Fill(t);
            }

            return t;
        }

        private DbCommand CreaCommandNoConnection(string sql, DbParameter[] param)
        {
            DbCommand cm = null;

            if (DataBaseAttuale == DataBase.Access)
                cm = new System.Data.OleDb.OleDbCommand(sql, (System.Data.OleDb.OleDbConnection)Connessione);
            else if (DataBaseAttuale == DataBase.SQLite)
                cm = new System.Data.SQLite.SQLiteCommand(sql, (System.Data.SQLite.SQLiteConnection)Connessione);

            if ((param != null))
                for (int x = 0; x <= param.Length - 1; x++)
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
                if ((Trans != null))
                    cm.Transaction = Trans;

                dr = cm.ExecuteReader();
            }

            return dr;
        }

        public string LeggiQuery(string mappath, Queries q)
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

            if ((z == "") || (z == null))
            {
                using (var sr = new System.IO.StreamReader(System.IO.Path.Combine(mappath, QueriesToString(q))))
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
            DbParameter o = null;

            if (Valore is DateTime)
            {
                if (DataBaseAttuale == DataBase.Access)
                    o = new System.Data.OleDb.OleDbParameter(Nome, DbType.DateTime);
                else if (DataBaseAttuale == DataBase.SQLite)
                {
                    Valore = DateToSQLite((DateTime)Valore);
                    //Valore = ((DateTime)Valore).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    o = new System.Data.SQLite.SQLiteParameter(Nome, DbType.String);
                }

                o.Value = Valore;
            }
            else
            {
                if (DataBaseAttuale == DataBase.Access)
                    o = new System.Data.OleDb.OleDbParameter(Nome, Valore);
                else if (DataBaseAttuale == DataBase.SQLite)
                    o = new System.Data.SQLite.SQLiteParameter(Nome, Valore);
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

                o = new System.Data.SQLite.SQLiteParameter(Nome, tipo);
            }

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

        public static string DammiStringaConnessione(DataBase db_, string path_db)
        {
            var s = "";

            if (db_ == DataBase.Access)
                s = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path_db + ";";
            else if (db_ == DataBase.SQLite)
                s = "Data Source=" + path_db;

            return s;
        }

        public void ApriConnessione(DataBase db_, string path_db, bool ForceClose = false)
        {
            DataBaseAttuale = db_;
            var s = DammiStringaConnessione(db_, path_db);

            if (ForceClose)
                ChiudiConnessione();

            if (DataBaseAttuale == DataBase.Access)
                Connessione = new System.Data.OleDb.OleDbConnection(s);
            else if (DataBaseAttuale == DataBase.SQLite)
                Connessione = new System.Data.SQLite.SQLiteConnection(s);

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