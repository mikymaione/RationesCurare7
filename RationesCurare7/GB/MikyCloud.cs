/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using RationesCurare7.WS;

namespace RationesCurare7.GB
{
    public sealed class MikyCloud
    {

        private string PathDB, Email, Psw;
        private CredenzialiDiAccessoRC Credenziali;


        public MikyCloud(string PathDB, string Email, string Psw)
        {
            this.PathDB = PathDB;
            this.Email = Email;
            this.Psw = Psw;

            Credenziali = new CredenzialiDiAccessoRC
            {
                Utente = Email,
                Psw = Psw
            };
        }


        public bool MandaDBSulSito(DateTime yyyyMMddHHmmss, bool Force = false)
        {
            return MandaDBSulSito(yyyyMMddHHmmss.ToString("yyyyMMddHHmmss"), Force);
        }

        public bool MandaDBSulSito(string yyyyMMddHHmmss, bool Force = false)
        {
            var ok = false;

            if (cGB.sDB.UltimaModifica > DateTime.MinValue || Force)
            {
                using (var e = new EmailSending())
                {
                    var comparazione = e.ComparaDBRC(yyyyMMddHHmmss, Email, Psw);

                    if (comparazione == Comparazione.Server)
                        if (cGB.MsgBox("Il database sul server è più aggiornato di quello locale; Vuoi sovrascrivere quello sul server?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return false;
                }

                while (!ok)
                {
                    cGB.CreaIcona("Sincronizzazione del DataBase");
                    ok = MandaDBSulSito__(yyyyMMddHHmmss);

                    if (!ok)
                        if (cGB.MsgBox("Riprovo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                using (var c = new EmailSending())
                {
                    //NON FATE I CAZZONI ! IO IL SITO LO PAGO E IL SERVIZIO LO OFFRO GRATUITAMENTE
                    var resu = c.ControllaCredenzialiRC(Credenziali);

                    switch (resu)
                    {
                        case CredenzialiRisultato.Assente:
                            if (cGB.MsgBox("Il nome utente non è presente nell'archivio! Vuoi crearlo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                                    cGB.MsgBox("Non sono riuscito a creare il nuovo utente! Riprovare!", MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            break;

                        case CredenzialiRisultato.Errore:
                            cGB.MsgBox("Non riesco a collegarmi al sito!", MessageBoxIcon.Exclamation);
                            break;

                        case CredenzialiRisultato.TuttoOK:
                            ok = MandaIlFile(yyyyMMddHHmmss);
                            break;

                        case CredenzialiRisultato.Presente_PasswordErrata:
                            cGB.MsgBox("Nome utente o password non validi!", MessageBoxIcon.Exclamation);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                cGB.MsgBox(ex.Message, MessageBoxIcon.Hand);
            }

            return ok;
        }

        private bool MandaIlFile(string yyyyMMddHHmmss)
        {
            //TODO: [M] GB.CompattaDB();                        
            var zs = ZippaDB(MettiDBInTempPath(PathDB));

            if (UploadFile(yyyyMMddHHmmss, zs))
            {
                using (var dez = new EmailSending())
                    if (dez.DeZippaDBRC(Email + ".zip"))
                    {
                        cGB.MsgI("Sincronizzazione completata!");

                        return true;
                    }
                    else
                    {
                        cGB.MsgBox("Non sono riuscito a sincronizzare il DataBase!", MessageBoxIcon.Exclamation);
                        return false;
                    }
            }

            cGB.MsgBox("Non sono riuscito a inviare il DataBase!", MessageBoxIcon.Exclamation);
            return false;
        }

        private bool UploadFile(string yyyyMMddHHmmss, string filename)
        {
            var b = false;

            try
            {
                var strFile = Path.GetFileName(filename);

                using (var srv = new EmailSending())
                {
                    var fInfo = new FileInfo(filename);
                    var numBytes = fInfo.Length;

                    try
                    {
                        using (var fStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                        using (var br = new BinaryReader(fStream))
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
                                    case CredenzialiRisultato.TuttoOK:
                                        return true;
                                    case CredenzialiRisultato.FileInviato:
                                        return true;
                                    case CredenzialiRisultato.Presente_PasswordErrata:
                                        throw new Exception("Password errata!");
                                    case CredenzialiRisultato.Assente:
                                        throw new Exception("DB assente!");
                                    case CredenzialiRisultato.Errore:
                                        throw new Exception("Errore!");
                                    case CredenzialiRisultato.ProgrammaNonAutorizzato:
                                        throw new Exception("Programma non autorizzato!");
                                    case CredenzialiRisultato.DBSulServerEPiuRecente:
                                        throw new Exception("DB sul server è più recente!");
                                }
                            }
                            catch (Exception exv1)
                            {
                                cGB.MsgBox(exv1.Message, MessageBoxIcon.Error);
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
            var zp = Path.ChangeExtension(p, ".zip");

            var cart = Path.GetDirectoryName(p);
            var ext = Path.GetExtension(p);

            // ZipConstants.DefaultCodePage = 850;

            var z = new FastZip();
            z.CreateZip(zp, cart, false, ext);

            return zp;
        }

        private string MettiDBInTempPath(string s)
        {
            var z = Path.GetTempPath();
            z = Path.Combine(z, Email + Path.GetExtension(s));

            File.Copy(s, z, true);

            return z;
        }


    }
}