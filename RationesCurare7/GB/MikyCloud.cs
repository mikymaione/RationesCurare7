/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare7.GB
{
    public sealed class MikyCloud
    {

        private string PathDB, Email, Psw;
        private maionemikyWS.CredenzialiDiAccessoRC Credenziali;


        public MikyCloud(string PathDB, string Email, string Psw)
        {
            this.PathDB = PathDB;
            this.Email = Email;
            this.Psw = Psw;

            this.Credenziali = new maionemikyWS.CredenzialiDiAccessoRC()
            {
                Utente = Email,
                Psw = Psw
            };
        }

        public MikyCloud(cGB.sUtente UtenteConnesso) : this(UtenteConnesso.PathDB, UtenteConnesso.Email, UtenteConnesso.Psw) { }


        public bool MandaDBSulSito(DateTime yyyyMMddHHmmss, bool Force = false)
        {
            return MandaDBSulSito(yyyyMMddHHmmss.ToString("yyyyMMddHHmmss"), Force);
        }

        public bool MandaDBSulSito(string yyyyMMddHHmmss, bool Force = false)
        {
            var ok = false;

            if ((DB.cDB.UltimaModifica > DateTime.MinValue) || Force)
            {
                using (var e = new maionemikyWS.EmailSending())
                {
                    var comparazione = e.ComparaDBRC(yyyyMMddHHmmss, Email, Psw);

                    if (comparazione == maionemikyWS.Comparazione.Server)
                        if (cGB.MsgBox("Il database sul server è più aggiornato di quello locale; Vuoi sovrascrivere quello sul server?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                            return false;
                }

                while (!ok)
                {
                    cGB.CreaIcona("Sincronizzazione del DataBase");
                    ok = MandaDBSulSito__(yyyyMMddHHmmss);

                    if (!ok)
                        if (cGB.MsgBox("Riprovo?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                            break;
                }
            }

            cGB.MyNotifyIcon.Visible = false;

            return ok;
        }

        private bool MandaDBSulSito__(string yyyyMMddHHmmss)
        {
            var ok = false;
            cGB.MsgI("Sincronizzazione in corso...");

            try
            {
                using (var c = new maionemikyWS.EmailSending())
                {
                    //NON FATE I CAZZONI ! IO IL SITO LO PAGO E IL SERVIZIO LO OFFRO GRATUITAMENTE
                    var resu = c.ControllaCredenzialiRC(Credenziali);

                    switch (resu)
                    {
                        case maionemikyWS.CredenzialiRisultato.Assente:
                            if (cGB.MsgBox("Il nome utente non è presente nell'archivio! Vuoi crearlo?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                if (c.CreaDBPerRC(Credenziali))
                                {
                                    var tyy = "Adesso puoi accedere ai tuoi dati direttamente dal cellulare da questa pagina http://www.maionemiky.it/mLogin.aspx con le seguenti credenziali:";
                                    tyy += Environment.NewLine + "Utente: " + Email;
                                    tyy += Environment.NewLine + "Password: " + Psw;

                                    c.MandaMail("RationesCurare7", tyy, Email);

                                    ok = MandaDBSulSito(yyyyMMddHHmmss);
                                }
                                else
                                {
                                    cGB.MsgBox("Non sono riuscito a creare il nuovo utente! Riprovare!", System.Windows.Forms.MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            break;

                        case maionemikyWS.CredenzialiRisultato.Errore:
                            cGB.MsgBox("Non riesco a collegarmi al sito!", System.Windows.Forms.MessageBoxIcon.Exclamation);
                            break;

                        case maionemikyWS.CredenzialiRisultato.TuttoOK:
                            ok = MandaIlFile(yyyyMMddHHmmss);
                            break;

                        case maionemikyWS.CredenzialiRisultato.Presente_PasswordErrata:
                            cGB.MsgBox("Nome utente o password non validi!", System.Windows.Forms.MessageBoxIcon.Exclamation);
                            break;
                    }
                }
            }
            catch
            {
                cGB.MsgBox("Non riesco a collegarmi al sito!", System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            return ok;
        }

        private bool MandaIlFile(string yyyyMMddHHmmss)
        {
            //TODO: [M] GB.CompattaDB();
            var okko = false;
            var Zipped = false;
            var s = MettiDBInTempPath(PathDB);

            try
            {
                var zs = ZippaDB(s);

                if (!string.IsNullOrEmpty(zs))
                    if (System.IO.File.Exists(zs))
                    {
                        s = zs;
                        Zipped = true;
                    }
            }
            catch
            {
                s = PathDB;
            }

            try
            {
                if (Zipped)
                {
                    okko = UploadFile(yyyyMMddHHmmss, s);

                    if (okko)
                        using (var dez = new maionemikyWS.EmailSending())
                            okko = dez.DeZippaDBRC(Email + ".zip");
                }
                else
                {
                    okko = UploadFile(yyyyMMddHHmmss, s);
                }
            }
            catch
            {
                okko = false;
            }

            if (okko)
                cGB.MsgI("Sincronizzazione completata!");
            else
                cGB.MsgBox("Non sono riuscito a sincronizzare il DataBase!", System.Windows.Forms.MessageBoxIcon.Exclamation);

            return okko;
        }

        private bool UploadFile(string yyyyMMddHHmmss, string filename)
        {
            var b = false;

            try
            {
                var strFile = System.IO.Path.GetFileName(filename);

                using (var srv = new maionemikyWS.EmailSending())
                {
                    var fInfo = new System.IO.FileInfo(filename);
                    var numBytes = fInfo.Length;

                    try
                    {
                        using (var fStream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        using (var br = new System.IO.BinaryReader(fStream))
                        {
                            var data = br.ReadBytes(Convert.ToInt32(numBytes));

                            br.Close();
                            fStream.Close();

                            var R = srv.UploadFileRC_simple(yyyyMMddHHmmss, Credenziali.Utente, Credenziali.Psw, data, strFile);
                            //var R = srv.UploadFileRC(Credenziali, data, strFile);

                            try
                            {
                                switch (R.CredenzialiRisultato_)
                                {
                                    case maionemikyWS.CredenzialiRisultato.TuttoOK:
                                        return true;
                                    case maionemikyWS.CredenzialiRisultato.FileInviato:
                                        return true;
                                    case maionemikyWS.CredenzialiRisultato.Presente_PasswordErrata:
                                        throw new Exception("Password errata!");
                                    case maionemikyWS.CredenzialiRisultato.Assente:
                                        throw new Exception("DB assente!");
                                    case maionemikyWS.CredenzialiRisultato.Errore:
                                        throw new Exception("Errore!");
                                    case maionemikyWS.CredenzialiRisultato.ProgrammaNonAutorizzato:
                                        throw new Exception("Programma non autorizzato!");
                                    case maionemikyWS.CredenzialiRisultato.DBSulServerEPiuRecente:
                                        throw new Exception("DB sul server è più recente!");
                                }
                            }
                            catch (Exception exv1)
                            {
                                cGB.MsgBox(exv1.Message, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch
                    {
                        //error
                        b = false;
                    }
                }
            }
            catch
            {
                // display an error message to the user
                b = false;
            }

            return b;
        }

        private string ZippaDB(string p)
        {
            var zp = "";

            try
            {
                zp = System.IO.Path.ChangeExtension(p, ".zip");

                var cart = System.IO.Path.GetDirectoryName(p);
                var ext = System.IO.Path.GetExtension(p);

                var z = new ICSharpCode.SharpZipLib.Zip.FastZip();
                z.CreateZip(zp, cart, false, ext);
            }
            catch
            {
                zp = "";
            }

            return zp;
        }

        private string MettiDBInTempPath(string s)
        {
            var z = "";

            try
            {
                z = System.IO.Path.GetTempPath();
                z = System.IO.Path.Combine(z, Email + System.IO.Path.GetExtension(s));

                System.IO.File.Copy(s, z, true);
            }
            catch
            {
                z = "";
            }

            return z;
        }


    }
}