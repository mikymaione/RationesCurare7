/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Drawing;
using System.Reflection;

namespace RationesCurare7
{
    public static class cGB
    {
        public static bool AggiornamentiDisponibili = false;
        public static bool RestartMe = false;
        public static UI.Forms.fMain RationesCurareMainForm;
        public static sUtente UtenteConnesso = new sUtente();
        public static cOpzioniProgramma OpzioniProgramma = null;
        public static Pen myPenLeft = new Pen(Color.FromArgb(137, 140, 149));
        public static Pen myPenBottom = new Pen(Color.FromArgb(160, 160, 160));
        public static Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond + DateTime.Now.Hour + DateTime.Now.Minute);
        public static System.Windows.Forms.NotifyIcon MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
        private static System.Collections.Generic.Dictionary<string, System.Globalization.CultureInfo> ListaCasseValute = null;


        public struct Time
        {
            public int Ora, Minuto;
        }

        public struct sUtente
        {
            public int ID;
            public string PathDB, UserName, Email, Psw, TipoDB;
        }

        public static bool DesignTime
        {
            get
            {
                return (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
            }
        }

        public static string PathDBUtenti
        {
            get
            {
                //string z = System.IO.Path.Combine(PathDBUtenti_Cartella, "RC.rqd");
                var z2 = System.IO.Path.Combine(PathDBUtenti_Cartella, "RC.rqd8");

                //if (System.IO.File.Exists(z))                
                //  return z;

                if (System.IO.File.Exists(z2))
                    return z2;
                else
                {
                    var j = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    j = System.IO.Path.Combine(j, "standard.rqd8");

                    var p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    p = System.IO.Path.Combine(p, @"[MAIONE MIKY]\RationesCurare7\");

                    if (!System.IO.Directory.Exists(p))
                        try
                        {
                            System.IO.Directory.CreateDirectory(p);
                        }
                        catch
                        {
                            //cannot create
                        }

                    p = System.IO.Path.Combine(p, "RC.rqd8");

                    if (!System.IO.File.Exists(p))
                        try
                        {
                            System.IO.File.Copy(j, p, false);
                        }
                        catch
                        {
                            //cannot copy
                        }

                    z2 = p;
                }

                return z2;
            }
        }

        public static string PathDBUtenti_Cartella
        {
            get
            {
                var p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                p = System.IO.Path.Combine(p, @"[MAIONE MIKY]\RationesCurare_Six\1.0.0.0\");

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
                        if (System.IO.Path.GetExtension(p[i]).Equals(".rqd") || System.IO.Path.GetExtension(p[i]).Equals(".rqd8"))
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
                var s = System.Windows.Forms.Application.ProductVersion;
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

                return ((attributes?.Length ?? 0) == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright);
            }
        }

        public static string PathFolderDB()
        {
            return System.IO.Path.GetDirectoryName(UtenteConnesso.PathDB);
        }

        public static string PathDBBackup()
        {
            return System.IO.Path.ChangeExtension(UtenteConnesso.PathDB, System.IO.Path.GetExtension(UtenteConnesso.PathDB) + "b");
        }

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

        public static string TimeToString(DateTime t)
        {
            return TimeToString(new Time()
            {
                Ora = t.Hour,
                Minuto = t.Minute
            });
        }

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

            return (h + "." + m);
        }

        public static System.Windows.Forms.DialogResult MsgBox(Exception s)
        {
            return MsgBox(s.Message, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        public static System.Windows.Forms.DialogResult MsgBox(string s)
        {
            return MsgBox(s, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public static System.Windows.Forms.DialogResult MsgBox(string s, System.Windows.Forms.MessageBoxIcon ico)
        {
            return MsgBox(s, System.Windows.Forms.MessageBoxButtons.OK, ico);
        }

        public static System.Windows.Forms.DialogResult MsgBox(string s, System.Windows.Forms.MessageBoxButtons but, System.Windows.Forms.MessageBoxIcon ico, bool TopMost = false)
        {
            return System.Windows.Forms.MessageBox.Show(new System.Windows.Forms.Form() { TopMost = true }, s, "RationesCurare7", but, ico);
        }

        public static DateTime ObjectToDateTime(object o, DateTime defa)
        {
            var d = defa;

            try
            {
                if (o is DateTime)
                    d = (DateTime)o;
                else if (o is string)
                    d = DateTime.ParseExact(o.ToString().Substring(0, 19), "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                //empty                
            }

            return d;
        }

        public static DateTime ObjectToDateTime(object o)
        {
            return ObjectToDateTime(o, DateTime.MinValue);
        }

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

        public static double ObjectToMoney(object o)
        {
            return ObjectToMoney(o, 0);
        }

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

        public static string QQ(string s, bool active)
        {
            if (active)
                return "%" + s + "%";
            else
                return "%%";
        }

        public static string QQ(string s)
        {
            return QQ(s, true);
        }

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

            return (
                (m == 1 & d == 1) ||
                (m == 1 & d == 6) ||
                (m == 4 & d == 25) ||
                (m == 5 & d == 1) ||
                (m == 6 & d == 2) ||
                (m == 8 & d == 15) ||
                (m == 11 & d == 1) ||
                (m == 12 & d == 8) ||
                (m == 12 & d == 25) ||
                (m == 12 & d == 26) ||
                (data.Equals(EasterDate(y).AddDays(1)))
            );
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
                H = ((C - (C / 4) - ((8 * C + 13) / 25) + (19 * G) + 15) % 30);
                i = H - ((H / 28) * (1 - (H / 28) * (29 / (H + 1)) * ((21 - G) / 11)));
                j = ((year + (year / 4) + i + 2 - C + (C / 4)) % 7);
                L = i - j;

                m = 3 + ((L + 40) / 44);
                d = L + 28 - (31 * (m / 4));
                dt = new DateTime(year, m, d);
            }

            return dt;
        }

        public static void RegistraUtenteSito()
        {
            try
            {
                using (var e = new maionemikyWS.EmailSending())
                    e.AggiornaUtente(
                        new maionemikyWS.UtenteProgramma()
                        {
                            Programma = "RationesCurare7",
                            Utente = UtenteConnesso.UserName,
                            Versione = MyProductVersion
                        }
                    );
            }
            catch
            {
                //no connection
            }
        }

        public static void StartExplorer(string z)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", z);
            }
            catch
            {
                //cannot open
            }
        }

        public static void CreaIcona(string titolo)
        {
            MyNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            MyNotifyIcon.BalloonTipTitle = titolo;
            MyNotifyIcon.Icon = Properties.Resources.rc;
        }

        public static void MsgI(string testo, int durata = 5000)
        {
            MyNotifyIcon.BalloonTipText = testo;
            MyNotifyIcon.Text = MyNotifyIcon.BalloonTipText;
            MyNotifyIcon.Visible = true;
            MyNotifyIcon.ShowBalloonTip(durata);
        }

        public static bool ScaricaUltimoDBDalWeb(maionemikyWS.EmailSending e, string yyyyMMddHHmmss, string PathDB, string email_, string psw, bool CreaNuovo)
        {
            var ok = false;
            var db_path = System.IO.Path.GetDirectoryName(PathDB);
            var zip_path = System.IO.Path.Combine(db_path, email_ + ".zip");

            try
            {
                var db_bytes = e.OttieniUltimoDBRC(yyyyMMddHHmmss, email_, psw);

                if ((db_bytes?.Length ?? 0) > 0)
                {
                    System.IO.File.WriteAllBytes(zip_path, db_bytes);

                    if (System.IO.File.Exists(zip_path))
                    {
                        var guid = System.IO.Path.Combine(db_path, Guid.NewGuid().ToString());

                        try
                        {
                            if (!CreaNuovo)
                                System.IO.File.Move(PathDB, guid);

                            try
                            {
                                var zip = new ICSharpCode.SharpZipLib.Zip.ZipFile(zip_path);

                                foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry zipEntry in zip)
                                {
                                    if (!zipEntry.IsFile)
                                        continue; // Ignore directories

                                    var entryFileName = System.IO.Path.GetFileName(zipEntry.Name);
                                    var buffer = new byte[4096]; // 4K is optimum
                                    var zipStream = zip.GetInputStream(zipEntry);

                                    // Manipulate the output filename here as desired.
                                    var fullZipToPath = System.IO.Path.Combine(db_path, entryFileName);

                                    using (var streamWriter = System.IO.File.Create(fullZipToPath))
                                    {
                                        Copy(zipStream, streamWriter, buffer);
                                        streamWriter.Close();
                                    }
                                }

                                zip.Close();

                                System.IO.File.Delete(guid);
                                System.IO.File.Delete(zip_path);

                                ok = true;
                            }
                            catch (Exception ex4)
                            {
                                System.IO.File.Move(guid, PathDB);
                                MsgBox(ex4);
                            }
                        }
                        catch (Exception ex3)
                        {
                            MsgBox(ex3);
                        }
                    }
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
                var cUte = new DB.DataWrapper.cUtente(UtenteConnesso.ID);

                using (var e = new maionemikyWS.EmailSending())
                {
                    var yyyyMMddHHmmss = cUte.UltimoAggiornamentoDB.ToString("yyyyMMddHHmmss");
                    var yyyyMMddHHmmss_WEB = e.VersioneDB(UtenteConnesso.Email, UtenteConnesso.Psw);
                    var comparazione = e.ComparaDBRC(yyyyMMddHHmmss, UtenteConnesso.Email, UtenteConnesso.Psw);

                    if (comparazione == maionemikyWS.Comparazione.Server)
                        using (var fdbd = new UI.Forms.fDBDate(cUte.UltimoAggiornamentoDB, yyyyMMddHHmmss_WEB))
                            if (fdbd.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                                ok = ScaricaUltimoDBDalWeb(e, yyyyMMddHHmmss, UtenteConnesso.PathDB, UtenteConnesso.Email, UtenteConnesso.Psw, false);
                }
            }
            catch (Exception ex)
            {
                if (MsgBox("Errore: " + ex.Message + Environment.NewLine + "Riprovo?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
        public static void Copy(System.IO.Stream source, System.IO.Stream destination, byte[] buffer)
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

        public static void DoTheAutoUpdate()
        {
            new System.Threading.Thread(DoTheAutoUpdate_t).Start();
            //DoTheAutoUpdate_t();
        }

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
            //yyyy-MM-dd HH:mm:ss.fff
            var h = "";

            h += d.Year + "-" + (d.Month < 10 ? "0" : "") + d.Month + "-" + (d.Day < 10 ? "0" : "") + d.Day + " ";
            h += (d.Hour < 10 ? "0" : "") + d.Hour + ":" + (d.Minute < 10 ? "0" : "") + d.Minute + ":" + (d.Second < 10 ? "0" : "") + d.Second + ".000";

            return h;
        }

        public static object DateToDBDateNullable(DateTime d)
        {
            if (d.Date > DateTime.MinValue)
                return d;
            else
                return DBNull.Value;
        }

        public static string DoubleToMoneyStringValuta(double a, string cassa = "")
        {
            if (cassa == "")
                return a.ToString("C");

            if (ListaCasseValute == null)
            {
                ListaCasseValute = new System.Collections.Generic.Dictionary<string, System.Globalization.CultureInfo>();

                var c = new DB.DataWrapper.cCasse();
                var li = c.ListaCasseValute();

                var cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);

                foreach (var cultura in cultures)
                    try
                    {
                        var ri = new System.Globalization.RegionInfo(cultura.Name);

                        foreach (var l in li)
                            if (l.Value == ri.ISOCurrencySymbol)
                                ListaCasseValute.Add(l.Key, cultura);
                    }
                    catch
                    {
                        //non disponibile
                    }
            }

            if (ListaCasseValute.ContainsKey(cassa))
                return a.ToString("C", ListaCasseValute[cassa]);
            else
                return a.ToString("C");
        }

        public static string DoubleToMoneyString(double d)
        {
            var a = Math.Round(d, 2);

            return a.ToString();
        }

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

        public static DateTime DateToOnlyDate(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day);
        }

        public static bool StringIsNullorEmpty(string s)
        {
            return ("".Equals(s ?? ""));
        }

        public static string NullStringToEmpty(string s)
        {
            if (s == null)
                s = "";

            return s;
        }

        public static DateTime DateTo00000(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }

        public static DateTime DateTo235959(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
        }

        public static bool DateSonoUgualiPer_YYYYMMDD(DateTime a, DateTime b)
        {
            return (a.Year == b.Year) && (a.Month == b.Month) && (a.Day == b.Day);
        }

        public static void EliminaRC6()
        {
            new System.Threading.Thread(EliminaRC6_t).Start();
        }

        private static void EliminaRC6_t()
        {
            var program = "RationesCurare_Six";

            try
            {
                var UninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
                var r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(UninstallPath);
                var s = r.GetSubKeyNames();

                for (var i = 0; i < (s?.Length ?? 0); i++)
                    try
                    {
                        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(UninstallPath + s[i]).GetValue("DisplayName").ToString().Equals(program, StringComparison.OrdinalIgnoreCase))
                        {
                            var u = "";

                            try
                            {
                                u = (string)Microsoft.Win32.Registry.LocalMachine.OpenSubKey(UninstallPath + s[i]).GetValue("UninstallString");
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
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(s);
            }
            catch
            {
                //errore cancellazione
            }
        }

        public static object ValueToDBNULL(bool PutNull, object ElseValue)
        {
            if (PutNull)
                return DBNull.Value;
            else
                return ElseValue;
        }

        public static string LoadImage_Casuale_Try(ref System.Windows.Forms.PictureBox pb)
        {
            var j = System.Windows.Forms.Application.ExecutablePath;
            j = System.IO.Path.GetDirectoryName(j);
            j = System.IO.Path.Combine(j, "Utenti");

            if (System.IO.Directory.Exists(j))
            {
                var fil = System.IO.Directory.GetFiles(j, "*.png", System.IO.SearchOption.AllDirectories);

                if ((fil?.Length ?? 0) > 0)
                {
                    var num = random.Next(0, fil.Length - 1);
                    j = fil[num];

                    try
                    {
                        cGB.LoadImage(j, ref pb);

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

        public static Exception LoadImage_Try(string p, ref System.Windows.Forms.PictureBox pb)
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

        public static void LoadImage(string p, ref System.Windows.Forms.PictureBox pb)
        {
            using (var stream = System.IO.File.Open(p, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Delete))
            {
                pb.Image = Image.FromStream(stream);
                stream.Close();
            }
        }

        public static bool PathDBUtentiIsAccess()
        {
            return System.IO.Path.GetExtension(PathDBUtenti).Equals(".rqd", StringComparison.OrdinalIgnoreCase);
        }

        public static DateTime DBNow()
        {
            var d = DateTime.Now;

            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }


    }
}