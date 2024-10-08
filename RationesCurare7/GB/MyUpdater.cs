﻿/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace RationesCurare7
{
    public static class MyUpdater
    {
        private struct VersioneInfo
        {
            public string Programma;
            public DateTime Versione;
            public string NomeZip;
        }

        private static VersioneInfo MioV;
        private static string Mio;
        private static string PathFileVersioni = Path.Combine(Application.UserAppDataPath, "versioni.txt");

        public static DateTime _Versione;


        private static bool DeZippaEdEsegui(string s)
        {
            var resu = false;
            var k = Path.Combine(Path.GetDirectoryName(s), Application.ProductName);

            try
            {
                if (Directory.Exists(k))
                    Directory.Delete(k, true);
            }
            catch
            {
                //some error
            }
            finally
            {
                try
                {
                    var z = new FastZip();
                    z.ExtractZip(s, k, "");

                    var exex = Directory.GetFiles(k, "*.exe", SearchOption.AllDirectories);
                    var msi = Directory.GetFiles(k, "*.msi", SearchOption.AllDirectories);
                    var trov = false;
                    var il_setup = "";

                    //setup.exe
                    if (!trov)
                    {
                        var maxExex = exex?.Length ?? 0;

                        for (var j = 0; j < maxExex; j++)
                        {
                            var nj = Path.GetFileName(exex[j]);

                            if (nj.Equals("setup.exe", StringComparison.OrdinalIgnoreCase))
                            {
                                il_setup = exex[j];
                                trov = true;
                                break;
                            }
                        }
                    }

                    //*.msi
                    if (!trov)
                        if (msi != null && msi.Length > 0)
                        {
                            il_setup = msi[0];
                            trov = true;
                        }

                    if (trov && File.Exists(il_setup))
                    {
                        if (cGB.MsgBox($"Vuoi installare la nuova versione di {Application.ProductName}?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, true) == DialogResult.Yes)
                        {
                            cGB.MsgI("Il programma verrà aggiornato alla nuova versione!");

                            Process.Start(il_setup);
                            Environment.Exit(0);

                            resu = true;
                        }
                        else
                        {
                            cGB.MsgI("Il programma non verrà aggiornato!");
                        }
                    }
                    else
                    {
                        cGB.MsgI("Il programma di installazione non è stato trovato!");
                    }
                }
                catch (Exception ex)
                {
                    //some error
                    cGB.MsgI($"Errore: {ex.Message}");
                }
            }

            return resu;
        }

        private static bool DownloadFileFromInternet(string http_, string path_salvataggio)
        {
            if (File.Exists(path_salvataggio))
                try
                {
                    File.Delete(path_salvataggio);
                }
                catch
                {
                    //cannot delete
                }

            try
            {
                using (var c = new WebClient())
                    c.DownloadFile(http_, path_salvataggio);

                return true;
            }
            catch
            {
                //error                
                return false;
            }
        }

        public static bool AggiornaQuestoProgramma(bool VerificaSolo, DateTime Versione_, bool MostraPopup)
        {
            var b = AggiornaQuestoProgramma_(VerificaSolo, Versione_, MostraPopup);

            cGB.MyNotifyIcon.Visible = false;

            return b;
        }

        private static bool AggiornaQuestoProgramma_(bool VerificaSolo, DateTime Versione_, bool MostraPopup)
        {
            Mio = Application.ProductName + ".exe";
            _Versione = Versione_;

            cGB.CreaIcona("Aggiornamenti automatici");

            if (MostraPopup)
                cGB.MsgI("Collegamento al sito");

            try
            {
                if (MostraPopup)
                    cGB.MsgI("Controllo versione");

                DownloadFileFromInternet("http://www.maionemiky.it/public/programmi/versioni.txt", PathFileVersioni);

                if (ControllaVersione(PathFileVersioni, MostraPopup))
                {
                    var MioV_NomeZip = Path.Combine(Application.UserAppDataPath, MioV.NomeZip);

                    if (File.Exists(PathFileVersioni))
                        try
                        {
                            File.Delete(PathFileVersioni);
                        }
                        catch
                        {
                            //cannot delete
                        }

                    try
                    {
                        if (File.Exists(MioV_NomeZip))
                            try
                            {
                                File.Delete(MioV_NomeZip);
                            }
                            catch
                            {
                                return false;
                            }

                        if (VerificaSolo)
                        {
                            if (MostraPopup)
                                cGB.MsgI($"Per {Application.ProductName} è disponibile una nuova versione!");

                            cGB.MsgBox($"Per {Application.ProductName} è disponibile una nuova versione! Cliccare sul pulsante Update!");
                            cGB.AggiornamentiDisponibili = true;
                        }
                        else
                        {
                            cGB.MsgI("Download della nuova versione in corso... attendere!");
                            DownloadFileFromInternet("http://www.maionemiky.it/public/programmi/" + MioV.NomeZip, MioV_NomeZip);

                            if (File.Exists(MioV_NomeZip))
                            {
                                cGB.MsgI("Il setup del programma è stato scaricato!");

                                return DeZippaEdEsegui(MioV_NomeZip);
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private static VersioneInfo LeggiRigaVersione(string s)
        {
            //22=23
            //31=32
            //XXXXXXXXXXXXXXXXXXXXXX0123456789
            //RationesCurare_Six.exe=21/11/07;RCV_Small.zip
            var c = default(VersioneInfo);

            var i = s.IndexOf("=");
            c.Programma = s.Substring(0, i);

            var m = s.IndexOf(";");
            c.NomeZip = s.Substring(m, s.Length - m);
            c.NomeZip = c.NomeZip.Replace(";", "");

            var d = s.Substring(i, 9);
            d = d.Replace("=", "");
            d = d.Replace(";", "");

            try
            {
                c.Versione = DateTime.ParseExact(d, "dd/MM/yy", CultureInfo.InvariantCulture);
            }
            catch
            {
                //cannot convert to date
            }

            return c;
        }

        private static bool ControllaVersione(string p, bool mostraPopup)
        {
            var ok = false;

            try
            {
                if (File.Exists(p))
                    using (var f = new StreamReader(p))
                    {
                        while (!f.EndOfStream)
                        {
                            var g = f.ReadLine();
                            var v = LeggiRigaVersione(g);

                            if (v.Programma.Equals(Mio, StringComparison.OrdinalIgnoreCase))
                                if (v.Versione > _Versione)
                                {
                                    MioV = v;
                                    ok = true;

                                    break;
                                }
                        }

                        f.Close();
                    }
            }
            catch
            {
                //error
            }

            if (mostraPopup)
                if (!ok)
                    cGB.MsgI("Nessuna versione nuova disponibile!", 15000);

            return ok;
        }


    }
}