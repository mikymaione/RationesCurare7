/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using RationesCurare7.DB;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.maionemikyWS;
using RationesCurare7.Properties;
using RationesCurare7.UI.Forms;

namespace RationesCurare7
{
    public static class cGB
    {
        public static cDB sDB, sPC;

        public static cUtenti DatiDBFisico;
        public static cDBInfo DatiUtente;

        public static bool AggiornamentiDisponibili = false;
        public static bool RestartMe = false;
        public static fMain RationesCurareMainForm;
        public static Pen myPenLeft = new Pen(Color.FromArgb(137, 140, 149));
        public static Pen myPenBottom = new Pen(Color.FromArgb(160, 160, 160));
        public static Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond + DateTime.Now.Hour + DateTime.Now.Minute);
        public static NotifyIcon MyNotifyIcon = new NotifyIcon();
        internal static CultureInfo valutaCorrente = Application.CurrentCulture;


        public struct Time
        {
            public int Ora, Minuto;
        }


        public static bool IAmInDebug
        {
            get
            {
                try
                {
                    return Process.GetCurrentProcess().ProcessName.Equals("RationesCurare7.vshost", StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool DesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        public static string PathDBUtenti
        {
            get
            {
                const string RCDBName = "RC.rqd8";

                var z = Path.Combine(PathDBUtenti_Cartella, RCDBName);

                if (File.Exists(z))
                {
                    return z;
                }

                var j = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                j = Path.Combine(j, RCDBName);

                var p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                p = Path.Combine(p, @"[MAIONE MIKY]\RationesCurare7\");

                if (!Directory.Exists(p))
                    try
                    {
                        Directory.CreateDirectory(p);
                    }
                    catch
                    {
                        //cannot create
                    }

                p = Path.Combine(p, RCDBName);

                if (!File.Exists(p))
                    try
                    {
                        File.Copy(j, p, false);
                    }
                    catch
                    {
                        //cannot copy
                    }

                z = p;

                return z;
            }
        }

        public static string PathDBUtenti_Cartella
        {
            get
            {
                var p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                p = Path.Combine(p, @"[MAIONE MIKY]\RationesCurare_Six\1.0.0.0\");

                return p;
            }
        }

        public static string Parametri
        {
            get
            {
                var z = "";

                try
                {
                    var p = Environment.GetCommandLineArgs();

                    for (var i = 0; i < (p?.Length ?? 0); i++)
                        if (Path.GetExtension(p[i]).Equals(".rqd") || Path.GetExtension(p[i]).Equals(".rqd8"))
                        {
                            z = p[i];
                            break;
                        }
                }
                catch
                {
                    //
                }

                return z;
            }
        }

        public static DateTime MyProductVersion
        {
            get
            {
                var s = Application.ProductVersion;
                s = s.Replace(".", "");

                //0123 45 67
                //2016 12 04
                var a = Convert.ToInt32(s.Substring(0, 4));
                var m = Convert.ToInt32(s.Substring(4, 2));
                var g = Convert.ToInt32(s.Substring(6, 2));

                return new DateTime(a, m, g);
            }
        }

        public static string CopyrightHolder
        {
            get
            {
                var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                return (attributes?.Length ?? 0) == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string PathFolderDB() =>
            Path.GetDirectoryName(DatiDBFisico.Path);

        public static string PathDBBackup() =>
            Path.ChangeExtension(DatiDBFisico.Path, Path.GetExtension(DatiDBFisico.Path) + "b");

        public static bool StringInArray(string s, string[] a)
        {
            for (var i = 0; i < (a?.Length ?? 0); i++)
                if (s.Equals(a[i], StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        public static bool String_For_Time_IsValid(string s)
        {
            var b = false;

            try
            {
                var t = new Time();

                t.Ora = Convert.ToInt32(s.Substring(0, 2));
                t.Minuto = Convert.ToInt32(s.Substring(3, 2));

                b = true;
            }
            catch
            {
                //error
            }

            return b;
        }

        public static Time StringToTime(string s)
        {
            var t = new Time();

            try
            {
                t.Ora = Convert.ToInt32(s.Substring(0, 2));
                t.Minuto = Convert.ToInt32(s.Substring(3, 2));
            }
            catch
            {
                //error
            }

            return t;
        }

        public static string TimeToString(DateTime t) => TimeToString(new Time
        {
            Ora = t.Hour,
            Minuto = t.Minute
        });

        public static string TimeToString(Time t)
        {
            var h = "";
            var m = "";

            if (t.Ora < 10)
                h = "0";

            h += t.Ora;

            if (t.Minuto < 10)
                m = "0";

            m += t.Minuto;

            return h + "." + m;
        }

        public static DialogResult MsgBox(Exception s) =>
            MsgBox(s.Message, MessageBoxIcon.Exclamation);

        public static DialogResult MsgBox(string s) =>
            MsgBox(s, MessageBoxIcon.Information);

        public static DialogResult MsgBox(string s, MessageBoxIcon ico) =>
            MsgBox(s, MessageBoxButtons.OK, ico);

        public static DialogResult MsgBox(string s, MessageBoxButtons but, MessageBoxIcon ico, bool TopMost = false) =>
            MessageBox.Show(new Form { TopMost = true }, s, "RationesCurare7", but, ico);

        public static DateTime ObjectToDateTime(object o, DateTime defa, int addDays) =>
            ObjectToDateTime(o, defa).AddDays(addDays);

        public static DateTime ObjectToDateTime(object o, DateTime defa)
        {
            var d = defa;

            try
            {
                if (o is DateTime)
                    d = (DateTime)o;
                else if (o is string)
                {
                    var ds = o.ToString().Substring(0, 19);
                    d = DateTime.ParseExact(ds, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                //empty                
            }

            return d;
        }

        public static DateTime ObjectToDateTime(object o) =>
            ObjectToDateTime(o, DateTime.MinValue);

        public static double ObjectToDouble(object o, double Default_ = 0)
        {
            var d = Default_;

            try
            {
                d = Convert.ToDouble(o);
            }
            catch
            {
                //erorr
            }

            return d;
        }

        public static double ObjectToMoney(object o) =>
            ObjectToMoney(o, 0);

        public static double ObjectToMoney(object o, double default_)
        {
            var d = default_;

            try
            {
                d = (double)o;
            }
            catch (InvalidCastException)
            {
                //error cast
                d = ObjectToDouble(o, default_);
            }
            catch (Exception)
            {
                //error generic                           
            }

            return d;
        }

        public static int ObjectToInt(object o, int default_)
        {
            var i = default_;

            try
            {
                i = Convert.ToInt32(o);
            }
            catch
            {
                //error
            }

            return i;
        }

        public static string QQ(string s, bool active) => active ? "%" + s + "%" : "%%";

        public static string QQ(string s) => QQ(s, true);

        public static string HolidayName(DateTime data)
        {
            var z = "";
            var y = data.Year;
            var m = data.Month;
            var d = data.Day;

            if (m == 1 & d == 1)
                z = "Capodanno";
            else if (m == 1 & d == 6)
                z = "Befana";
            else if (m == 4 & d == 25)
                z = "Festa della liberazione";
            else if (m == 5 & d == 1)
                z = "Festa del lavoro";
            else if (m == 6 & d == 2)
                z = "Nascita della Repubblica Italiana";
            else if (m == 8 & d == 15)
                z = "Ferragosto";
            else if (m == 11 & d == 1)
                z = "Ognissanti";
            else if (m == 12 & d == 8)
                z = "Immacolata";
            else if (m == 12 & d == 25)
                z = "Natale";
            else if (m == 12 & d == 26)
                z = "Santo Stefano";
            else if (data.Equals(EasterDate(y).AddDays(1)))
                z = "Pasqua";

            return z;
        }

        public static bool IsHoliday(DateTime data)
        {
            var y = data.Year;
            var m = data.Month;
            var d = data.Day;

            return m == 1 & d == 1 ||
                   m == 1 & d == 6 ||
                   m == 4 & d == 25 ||
                   m == 5 & d == 1 ||
                   m == 6 & d == 2 ||
                   m == 8 & d == 15 ||
                   m == 11 & d == 1 ||
                   m == 12 & d == 8 ||
                   m == 12 & d == 25 ||
                   m == 12 & d == 26 ||
                   data.Equals(EasterDate(y).AddDays(1));
        }

        private static DateTime EasterDate(int year = 0)
        {
            var dt = DateTime.Now;
            var G = 0;
            var C = 0;
            var H = 0;
            var i = 0;
            var j = 0;
            var L = 0;
            var m = 0;
            var d = 0;

            if (year == 0)
                year = DateTime.Today.Year;

            if (dt.Year != year)
            {
                G = year % 19;
                C = year / 100;
                H = (C - C / 4 - (8 * C + 13) / 25 + 19 * G + 15) % 30;
                i = H - H / 28 * (1 - H / 28 * (29 / (H + 1)) * ((21 - G) / 11));
                j = (year + year / 4 + i + 2 - C + C / 4) % 7;
                L = i - j;

                m = 3 + (L + 40) / 44;
                d = L + 28 - 31 * (m / 4);
                dt = new DateTime(year, m, d);
            }

            return dt;
        }

        public static void RegistraUtenteSito()
        {
            try
            {
                using (var e = new EmailSending())
                    e.AggiornaUtente(
                        new UtenteProgramma
                        {
                            Programma = "RationesCurare7",
                            Utente = DatiDBFisico.Nome,
                            Versione = MyProductVersion
                        }
                    );
            }
            catch
            {
                //no connection
            }
        }

        public static void StartExplorer(string z) => Process.Start("explorer.exe", z);

        public static void CreaIcona(string titolo)
        {
            MyNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            MyNotifyIcon.BalloonTipTitle = titolo;
            MyNotifyIcon.Icon = Resources.rc;
        }

        public static void MsgI(string testo, int durata = 5000)
        {
            MyNotifyIcon.BalloonTipText = testo;
            MyNotifyIcon.Text = MyNotifyIcon.BalloonTipText;
            MyNotifyIcon.Visible = true;
            MyNotifyIcon.ShowBalloonTip(durata);
        }

        public static bool ScaricaUltimoDBDalWeb(EmailSending e, string yyyyMMddHHmmss, string PathDB, string email_, string psw, bool CreaNuovo)
        {
            var ok = false;
            var db_path = Path.GetDirectoryName(PathDB);
            var rqd8_path = Path.Combine(db_path, email_ + ".rqd8");

            try
            {
                var db_bytes = e.OttieniUltimoDBRCRqd8(yyyyMMddHHmmss, email_, psw);

                if ((db_bytes?.Length ?? 0) > 0)
                {
                    File.WriteAllBytes(rqd8_path, db_bytes);
                }
            }
            catch (Exception ex2)
            {
                MsgBox(ex2);
            }

            return ok;
        }

        public static bool ControllaDBSulServer()
        {
            var ok = false;

            try
            {
                using (var e = new EmailSending())
                {
                    var yyyyMMddHHmmss = DatiUtente.UltimaModifica.ToString("yyyyMMddHHmmss");
                    var yyyyMMddHHmmss_WEB = e.VersioneDB(DatiUtente.Email, DatiUtente.Psw);
                    var comparazione = e.ComparaDBRC(yyyyMMddHHmmss, DatiUtente.Email, DatiUtente.Psw);

                    if (comparazione == Comparazione.Server)
                        using (var fdbd = new fDBDate(DatiUtente.UltimaModifica, yyyyMMddHHmmss_WEB))
                            if (fdbd.ShowDialog() == DialogResult.Yes)
                            {
                                sDB.Connessione.Close();
                                ok = ScaricaUltimoDBDalWeb(e, yyyyMMddHHmmss, DatiDBFisico.Path, DatiUtente.Email, DatiUtente.Psw, false);
                            }
                }
            }
            catch (Exception ex)
            {
                var erMsg = ex.Message;

                if (erMsg.Length > 1500)
                    erMsg = erMsg.Substring(0, 1500) + " [...]";

                if (MsgBox($"Errore: {erMsg}.{Environment.NewLine}Riprovo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ok = ControllaDBSulServer();
            }

            return ok;
        }

        /// <summary>
        /// Copy the contents of one <see cref="Stream"/> to another.
        /// </summary>
        /// <param name="source">The stream to source data from.</param>
        /// <param name="destination">The stream to write data to.</param>
        /// <param name="buffer">The buffer to use during copying.</param>
        public static void Copy(Stream source, Stream destination, byte[] buffer)
        {
            if (source == null)
                throw new ArgumentNullException();

            if (destination == null)
                throw new ArgumentNullException();

            if (buffer == null)
                throw new ArgumentNullException();

            // Ensure a reasonable size of buffer is used without being prohibitive.
            if (buffer.Length < 128)
                throw new ArgumentException("Buffer is too small");

            var copying = true;

            while (copying)
            {
                var bytesRead = source.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    destination.Write(buffer, 0, bytesRead);
                }
                else
                {
                    destination.Flush();
                    copying = false;
                }
            }
        }

        public static void DoTheAutoUpdate() => new Thread(DoTheAutoUpdate_t).Start();

        private static void DoTheAutoUpdate_t()
        {
            try
            {
                MyUpdater.AggiornaQuestoProgramma(false, MyProductVersion, false);
            }
            catch
            {
                //error
            }
        }

        public static string DateToSQLite(DateTime d)
        {
            //yyyy-MM-dd HH:mm:ss
            var h = "";

            h += d.Year + "-" + (d.Month < 10 ? "0" : "") + d.Month + "-" + (d.Day < 10 ? "0" : "") + d.Day + " ";
            h += (d.Hour < 10 ? "0" : "") + d.Hour + ":" + (d.Minute < 10 ? "0" : "") + d.Minute + ":" + (d.Second < 10 ? "0" : "") + d.Second;

            return h;
        }

        public static object DateToDBDateNullable(DateTime d)
        {
            if (d.Date > DateTime.MinValue)
                return d;
            return DBNull.Value;
        }

        public static void initCulture()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var cultura in cultures)
                try
                {
                    var ri = new RegionInfo(cultura.Name);

                    if (DatiUtente.Valuta.Equals(ri.ISOCurrencySymbol, StringComparison.InvariantCultureIgnoreCase))
                    {
                        valutaCorrente = cultura;
                        break;
                    }
                }
                catch
                {
                    //non disponibile
                }
        }

        public static string DoubleToMoneyStringValuta(double d) =>
            d.ToString("C");

        public static string DoubleToMoneyString(double d) =>
            Math.Round(d, 2).ToString();

        public static double DoubleToMoney(string sa)
        {
            var punta = sa.IndexOf(".");
            var virga = sa.IndexOf(",");
            var max_v = sa.Length - virga;
            var max_p = sa.Length - punta;

            if (max_v > 3)
                max_v = 3;

            if (max_p > 3)
                max_p = 3;

            if (punta > -1)
                sa = sa.Substring(0, punta + max_p);
            if (virga > -1)
                sa = sa.Substring(0, virga + max_v);

            return Math.Round(ObjectToDouble(sa), 2);
        }

        public static DateTime DateToOnlyDate(DateTime d) => new DateTime(d.Year, d.Month, d.Day);

        public static bool StringIsNullorEmpty(string s) => "".Equals(s ?? "");

        public static string NullStringToEmpty(string s) => s ?? "";

        public static DateTime DateTo00000(DateTime d) => new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);

        public static DateTime DateTo235959(DateTime d) => new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);

        public static bool DateSonoUgualiPer_YYYYMMDD(DateTime a, DateTime b) => a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;

        public static void EliminaRC6() => new Thread(EliminaRC6_t).Start();

        private static void EliminaRC6_t()
        {
            var program = "RationesCurare_Six";

            try
            {
                var UninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
                var r = Registry.LocalMachine.OpenSubKey(UninstallPath);
                var s = r.GetSubKeyNames();

                for (var i = 0; i < (s?.Length ?? 0); i++)
                    try
                    {
                        if (Registry.LocalMachine.OpenSubKey(UninstallPath + s[i]).GetValue("DisplayName").ToString().Equals(program, StringComparison.OrdinalIgnoreCase))
                        {
                            var u = "";

                            try
                            {
                                u = (string)Registry.LocalMachine.OpenSubKey(UninstallPath + s[i]).GetValue("UninstallString");
                            }
                            catch
                            {
                                //not found
                            }

                            //Esegue eliminazione MSI
                            //if (u != null)
                            //    if (u != "")
                            //        System.Diagnostics.Process.Start(u);

                            EliminaChiaveRegistro(UninstallPath + s[i]);

                            break;
                        }
                    }
                    catch
                    {
                        //chiave senza valore DisplayName
                    }
            }
            catch
            {
                //errore apertura chiave
            }
        }

        private static void EliminaChiaveRegistro(string s)
        {
            try
            {
                Registry.LocalMachine.DeleteSubKey(s);
            }
            catch
            {
                //errore cancellazione
            }
        }

        public static object ValueToDBNULL(bool PutNull, object ElseValue) => PutNull ? DBNull.Value : ElseValue;

        public static string LoadImage_Casuale_Try(ref PictureBox pb)
        {
            var j = Application.ExecutablePath;
            j = Path.GetDirectoryName(j);
            j = Path.Combine(j, "Utenti");

            if (Directory.Exists(j))
            {
                var fil = Directory.GetFiles(j, "*.png", SearchOption.AllDirectories);

                if ((fil?.Length ?? 0) > 0)
                {
                    var num = random.Next(0, fil.Length - 1);
                    j = fil[num];

                    try
                    {
                        LoadImage(j, ref pb);

                        return j;
                    }
                    catch
                    {
                        //error
                        return "";
                    }
                }
            }

            return "";
        }

        public static Exception LoadImage_Try(string p, ref PictureBox pb)
        {
            try
            {
                LoadImage(p, ref pb);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        public static void LoadImage(string p, ref PictureBox pb)
        {
            using (var stream = File.Open(p, FileMode.Open, FileAccess.Read, FileShare.Delete))
            {
                pb.Image = Image.FromStream(stream);
                stream.Close();
            }
        }

        public static bool PathDBUtentiIsAccess() => Path.GetExtension(PathDBUtenti).Equals(".rqd", StringComparison.OrdinalIgnoreCase);

        public static DateTime DBNow()
        {
            var d = DateTime.Now;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        public static bool ControllaGiaInEsecuzione_SeContinuare()
        {
            try
            {
                var p = Process.GetProcessesByName("RationesCurare7");

                if ((p?.Length ?? 0) > 1)
                    if (MsgBox("RationesCurare è già in esecuzione, vuoi avviarne un altro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
            }
            catch
            {
                //errore
            }

            return true;
        }


    }
}