/*
EmailSending
Copyright (C) 2017 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Web.Services;

namespace maionemiky
{
    [WebService(Namespace = "http://www.maionemiky.it/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    public class EmailSending : WebService
    {

        public enum Comparazione
        {
            Uguale,
            Server,
            Client,
            AccessoNonAutizzato
        }

        public enum CredenzialiRisultato
        {
            TuttoOK,
            Presente_PasswordErrata,
            Assente,
            Errore,
            ProgrammaNonAutorizzato,
            FileInviato,
            DBSulServerEPiuRecente
        }

        public enum Licenza
        {
            Fallito,
            RegistratoOK,
            CodiceInesistente,
            GiaRegistrato
        }

        public struct CredenzialiRisultatoFull
        {
            public CredenzialiRisultato CredenzialiRisultato_;
            public string Errore;
        }

        public struct CredenzialiDiAccesso
        {
            public string NomeApplicazione;
        }

        public struct CredenzialiDiAccessoRC
        {
            public string Utente, Psw;
        }

        public struct UtenteProgramma
        {
            public string Programma, Utente;
            public DateTime Versione;
        }

        private struct DatiLicenza
        {
            public string Utente, Chiave, Stato;
            public bool Ok;

            public DatiLicenza(bool Ok_false)
            {
                Utente = "";
                Chiave = "";
                Stato = "";

                Ok = Ok_false;
            }
        }

        [WebMethod]
        public Comparazione ComparaDBRC(string yyyyMMddHHmmss, string email, string psw)
        {
            return DBSulServerEPiuNuovo(yyyyMMddHHmmss, email, psw);
        }

        [WebMethod]
        public DateTime VersioneDB(string email, string psw)
        {
            return VersioneDB_pri(email, psw);
        }

        [WebMethod]
        public byte[] OttieniUltimoDBRC(string yyyyMMddHHmmss, string email, string psw)
        {
            return OttieniUltimoDBRC_p(yyyyMMddHHmmss, email, psw);
        }

        [WebMethod]
        public Licenza SonoLicenziato(string Utente, string Codice)
        {
            return SonoLicenziato_pri(Utente, Codice);
        }

        [WebMethod]
        public bool RecuperaPswRC_Six(string Psw, string Email)
        {
            return RecuperaPswRC_Six_pri(Psw, Email);
        }

        [WebMethod]
        public bool GiaEsisteArchivioFoto(string descrizione)
        {
            return GiaEsisteArchivioFoto_pri(descrizione);
        }

        [WebMethod]
        public bool AggiornaUtente(UtenteProgramma u)
        {
            return AggiornaUtente_pri(u);
        }

        [WebMethod]
        public bool DeZippaDBRC(string PathFile)
        {
            return DeZippaDBRC_pr(PathFile);
        }

        [WebMethod]
        public CredenzialiRisultatoFull UploadFileFull(string MapPath_, string Directory_, CredenzialiDiAccesso Credenziali, CredenzialiDiAccessoRC CredenzialiRC, byte[] f, string fileName)
        {
            return UploadFile_pri(MapPath_, Directory_, Credenziali, CredenzialiRC, f, fileName);
        }

        [WebMethod]
        public CredenzialiRisultato UploadFile(CredenzialiDiAccesso Credenziali, byte[] f, string fileName)
        {
            return UploadFile_pri(Credenziali, new CredenzialiDiAccessoRC(), f, fileName).CredenzialiRisultato_;
        }

        [WebMethod]
        public CredenzialiRisultato UploadFileRC(CredenzialiDiAccessoRC Credenziali, byte[] f, string fileName)
        {
            return UploadFile_pri(new CredenzialiDiAccesso(), Credenziali, f, fileName).CredenzialiRisultato_;
        }

        [WebMethod]
        public CredenzialiRisultatoFull UploadFileRC_simple(string yyyyMMddHHmmss, string Utente, string Psw, byte[] f, string fileName)
        {
            if (DBSulServerEPiuNuovo(yyyyMMddHHmmss, Utente, Psw) == Comparazione.Server)
                return new CredenzialiRisultatoFull()
                {
                    CredenzialiRisultato_ = CredenzialiRisultato.DBSulServerEPiuRecente
                };
            else
            {
                var Credenziali = new CredenzialiDiAccessoRC();
                Credenziali.Utente = Utente;
                Credenziali.Psw = Psw;

                return UploadFile_pri(yyyyMMddHHmmss, new CredenzialiDiAccesso(), Credenziali, f, fileName);
            }
        }

        [WebMethod]
        public bool CreaDBPerRC(CredenzialiDiAccessoRC CredenzialiRC)
        {
            return CreaDBPerRC_pri(CredenzialiRC);
        }

        [WebMethod]
        public bool CreaDBPerRC_simple(string Utente, string Psw)
        {
            var CredenzialiRC = new CredenzialiDiAccessoRC();
            CredenzialiRC.Utente = Utente;
            CredenzialiRC.Psw = Psw;

            return CreaDBPerRC(CredenzialiRC);
        }

        public bool RecuperaPswRC_Six_pri(string Psw, string Email)
        {
            return MandaMailP("Recupero Password di RationesCurare_Six", "La tua password è : " + Psw, Email).Equals("OK");
        }

        private bool GiaEsisteArchivioFoto_pri(string descrizione)
        {
            var bb = false;

            try
            {
                var p = System.IO.Path.Combine(Server.MapPath("public"), "/files/files.mdb");

                using (var c = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p))
                {
                    c.Open();

                    try
                    {
                        var n = 0;
                        var s = "SELECT count(*) Num FROM File where nome = :nome";

                        var pr = new System.Data.OleDb.OleDbParameter[] {
                            new System.Data.OleDb.OleDbParameter("nome", descrizione + ".zip")
                        };

                        using (var m = new System.Data.OleDb.OleDbCommand(s, c))
                        {
                            m.Parameters.AddRange(pr);

                            using (var dr = m.ExecuteReader())
                            {
                                while (dr.HasRows && dr.Read())
                                    try
                                    {
                                        n = Convert.ToInt32(dr["Num"]);
                                    }
                                    catch
                                    {
                                        n = 0;
                                    }

                                dr.Close();
                            }
                        }

                        bb = n > 0;
                    }
                    catch
                    {
                        //error
                    }

                    c.Close();
                }
            }
            catch
            {
                //error
            }

            return bb;
        }

        public bool AggiornaUtente_pri(UtenteProgramma u)
        {
            var s = "";
            var i = 0;
            var n = 0;

            try
            {
                var p = System.IO.Path.Combine(Server.MapPath("public"), "/files/files.mdb");

                using (var c = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p))
                {
                    try
                    {
                        c.Open();

                        try
                        {
                            s = "SELECT count(*) Num FROM Utenti where Programma=:Programma and Nome=:Nome";

                            var pr = new System.Data.OleDb.OleDbParameter[] {
                                new System.Data.OleDb.OleDbParameter("Programma", u.Programma),
                                new System.Data.OleDb.OleDbParameter("Nome", u.Utente)
                            };

                            using (var m = new System.Data.OleDb.OleDbCommand(s, c))
                            {
                                m.Parameters.AddRange(pr);

                                using (var dr = m.ExecuteReader())
                                {
                                    while (dr.HasRows && dr.Read())
                                        try
                                        {
                                            n = Convert.ToInt32(dr["Num"]);
                                        }
                                        catch
                                        {
                                            //error
                                            n = 0;
                                        }

                                    dr.Close();
                                }
                            }
                        }
                        catch
                        {
                            //error
                        }

                        try
                        {
                            if (n > 0)
                                s = "update Utenti set Versione=:Versione where Nome=:Nome and Programma=:Programma";
                            else
                                s = "insert into Utenti (Versione,Nome,Programma) values (:Versione,:Nome,:Programma)";

                            var pr = new System.Data.OleDb.OleDbParameter[] {
                                new System.Data.OleDb.OleDbParameter("Versione", u.Versione),
                                new System.Data.OleDb.OleDbParameter("Nome", u.Utente),
                                new System.Data.OleDb.OleDbParameter("Programma", u.Programma)
                            };

                            using (var m = new System.Data.OleDb.OleDbCommand(s, c))
                            {
                                m.Parameters.AddRange(pr);
                                i = m.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            //error
                        }

                        c.Close();
                    }
                    catch
                    {
                        //error

                    }
                }
            }
            catch
            {
                //error

            }

            return (i > 0);
        }

        public bool CreaDBPerRC_pri(CredenzialiDiAccessoRC CredenzialiRC)
        {
            var b = false;
            var p = "";
            var cre = ControllaCredenzialiRC(CredenzialiRC);

            switch (cre)
            {
                case CredenzialiRisultato.TuttoOK:
                    b = true;
                    break;
                case CredenzialiRisultato.Presente_PasswordErrata:
                    b = true;
                    break;
                default:
                    b = false;
                    break;
            }

            if (!b)
            {
                try
                {
                    p = Server.MapPath("App_Data");
                }
                catch
                {
                    p = "";
                }

                if (!p.Equals(""))
                    try
                    {
                        using (var sw = new System.IO.StreamWriter(System.IO.Path.Combine(p, CredenzialiRC.Utente + ".psw"), false))
                        {
                            sw.WriteLine(CredenzialiRC.Psw);
                            sw.Close();
                            b = true;
                        }
                    }
                    catch
                    {
                        b = false;
                    }
            }

            return b;
        }

        [WebMethod]
        public CredenzialiRisultato ControllaCredenzialiProg(CredenzialiDiAccesso Credenziali)
        {
            var cre = CredenzialiRisultato.ProgrammaNonAutorizzato;
            var s = Credenziali.NomeApplicazione;

            if (s != null && s.Equals("RedBird"))
                cre = CredenzialiRisultato.TuttoOK;

            return cre;
        }

        [WebMethod]
        public CredenzialiRisultato ControllaCredenzialiRC_simple(string Utente, string Psw)
        {
            var CredenzialiRC = new CredenzialiDiAccessoRC();
            CredenzialiRC.Utente = Utente;
            CredenzialiRC.Psw = Psw;

            return ControllaCredenzialiRC(CredenzialiRC);
        }

        [WebMethod]
        public CredenzialiRisultato ControllaCredenzialiRC(CredenzialiDiAccessoRC CredenzialiRC)
        {
            var cre = CredenzialiRisultato.Errore;
            var p = "";

            try
            {
                p = Server.MapPath("App_Data");
            }
            catch
            {
                p = "";
            }

            if (!p.Equals(""))
            {
                cre = CredenzialiRisultato.Assente;

                var ps = "";
                var f = System.IO.Directory.GetFiles(p, CredenzialiRC.Utente + ".psw");

                if (f != null)
                    if (f.Length > 0)
                    {
                        using (var sr = new System.IO.StreamReader(f[0]))
                        {
                            ps = sr.ReadLine();
                            sr.Close();
                        }

                        if (ps == CredenzialiRC.Psw)
                            cre = CredenzialiRisultato.TuttoOK;
                        else
                            cre = CredenzialiRisultato.Presente_PasswordErrata;
                    }
            }

            return cre;
        }

        private CredenzialiRisultatoFull UploadFile_pri(CredenzialiDiAccesso Credenziali, CredenzialiDiAccessoRC CredenzialiRC, byte[] f, string fileName)
        {
            return UploadFile_pri("", Credenziali, CredenzialiRC, f, fileName);
        }

        private CredenzialiRisultatoFull UploadFile_pri(string yyyyMMddHHmmss, CredenzialiDiAccesso Credenziali, CredenzialiDiAccessoRC CredenzialiRC, byte[] f, string fileName)
        {
            var ora = DateTime.Now.ToString("yyyyMMddHHmmss");
            var r = UploadFile_pri("App_Data", "", Credenziali, CredenzialiRC, f, fileName);

            if (r.CredenzialiRisultato_ == CredenzialiRisultato.FileInviato)
            {
                var dira = Server.MapPath("App_Data");

                if (System.IO.Directory.Exists(dira))
                    using (var sw = new System.IO.StreamWriter(System.IO.Path.Combine(dira, System.IO.Path.GetFileNameWithoutExtension(fileName) + ".date"), false))
                    {
                        if (yyyyMMddHHmmss.Equals(""))
                            sw.WriteLine(ora);
                        else
                            sw.WriteLine(yyyyMMddHHmmss);

                        sw.Close();
                    }
            }

            return r;
        }

        private CredenzialiRisultatoFull UploadFile_pri(string MapPath_, string Directory_, CredenzialiDiAccesso Credenziali, CredenzialiDiAccessoRC CredenzialiRC, byte[] f, string fileName)
        {
            var BB = CredenzialiRisultato.Errore;
            var Resu = new CredenzialiRisultatoFull();
            Resu.Errore = "";

            var CRC = ControllaCredenzialiRC(CredenzialiRC);
            var CPR = ControllaCredenzialiProg(Credenziali);

            if (CRC != CredenzialiRisultato.TuttoOK)
            {
                BB = CRC;

                if (CPR != CredenzialiRisultato.TuttoOK)
                {
                    BB = CPR;
                    Resu.CredenzialiRisultato_ = BB;

                    return Resu;
                }
                else
                    BB = CPR;
            }
            else
                BB = CRC;

            try
            {
                var dira = System.IO.Path.Combine(Server.MapPath(MapPath_), Directory_);

                try
                {
                    if (!System.IO.Directory.Exists(dira))
                        System.IO.Directory.CreateDirectory(dira);
                }
                catch (Exception ex)
                {
                    Resu.Errore = ex.Message;
                }

                using (var ms = new System.IO.MemoryStream(f))
                using (var fs = new System.IO.FileStream(System.IO.Path.Combine(dira, fileName), System.IO.FileMode.Create))
                {
                    ms.WriteTo(fs);

                    ms.Close();
                    fs.Close();
                }

                BB = CredenzialiRisultato.FileInviato;
                Resu.Errore = "";
            }
            catch (Exception ex)
            {
                BB = CredenzialiRisultato.Errore;
                Resu.Errore = ex.Message;
            }

            Resu.CredenzialiRisultato_ = BB;

            return Resu;
        }

        public bool DeZippaDBRC_pr(string PathFile)
        {
            var b = false;
            var p = "";

            try
            {
                p = Server.MapPath("App_Data");
            }
            catch
            {
                p = "";
            }

            try
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(p, PathFile)))
                    try
                    {
                        var zi = new ICSharpCode.SharpZipLib.Zip.FastZip();

                        zi.ExtractZip(System.IO.Path.Combine(p, PathFile), p, "");
                        b = true;
                    }
                    catch
                    {
                        b = false;
                    }
            }
            catch
            {
                b = false;
            }

            return b;
        }

        [WebMethod]
        public string MandaMail(string Oggetto, string Testo, string Destinatario)
        {
            return MandaMailP(Oggetto, Testo, Destinatario);
        }

        private string MandaMailP(string Oggetto, string Testo, string Destinatario)
        {
            var fatto = "";

            try
            {
                using (var m = new System.Net.Mail.MailMessage("postmaster@maionemiky.it", Destinatario, Oggetto, Testo))
                {
                    var s = new System.Net.Mail.SmtpClient("smtp.maionemiky.it");
                    s.Send(m);
                }

                fatto = "OK";
            }
            catch (Exception ex)
            {
                fatto = ex.Message;
            }

            return fatto;
        }

        private bool CisonoFileNellaCartella(string s)
        {
            var b = false;

            try
            {
                var j = System.IO.Directory.GetFiles(s);

                b = (j.Length > 0);
            }
            catch
            {
                b = false;
            }

            return b;
        }

        private void CreaPsw(string NomeFile, string Psw)
        {
            var p = System.IO.Path.Combine(Server.MapPath("public"), "Files");

            using (var w = new System.IO.StreamWriter(System.IO.Path.Combine(p, NomeFile + ".psw"), true))
            {
                w.WriteLine(Psw);
                w.Close();
            }
        }

        private string FaiZip(string Descrizione, string Psw)
        {
            var nome = Descrizione;
            var p = System.IO.Path.Combine(Server.MapPath("public"), "Files");
            var m = System.IO.Path.Combine(p, nome);
            var cartella = System.IO.Path.Combine(Server.MapPath("public"), @"files/" + Descrizione);
            var f = System.IO.Directory.GetFiles(m);

            Array.Sort(f);

            m += ".zip";
            using (var z = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(m))
            {
                if (!Psw.Equals(""))
                {
                    z.Password = Psw;
                    CreaPsw(Descrizione, Psw);
                }

                z.BeginUpdate();

                foreach (var ff in f)
                    z.Add(System.IO.Path.Combine(cartella, System.IO.Path.GetFileName(ff)), ICSharpCode.SharpZipLib.Zip.CompressionMethod.Stored);

                z.CommitUpdate();
                z.Close();
            }

            return m;
        }

        private Licenza SonoLicenziato_pri(string Utente, string Codice)
        {
            var l = Licenza.Fallito;
            var p = System.IO.Path.Combine(Server.MapPath("public"), "/files/files.mdb");

            using (var c = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p))
            {
                try
                {
                    c.Open();

                    var s = "SELECT top 1 * FROM Licenze where Chiave = @Chiave";
                    var dl = new DatiLicenza(false);

                    using (var cm = new System.Data.OleDb.OleDbCommand(s, c))
                    {
                        var p1 = new System.Data.OleDb.OleDbParameter("Chiave", Codice);
                        cm.Parameters.Add(p1);

                        using (var dr = cm.ExecuteReader())
                        {
                            while (dr.HasRows && dr.Read())
                            {
                                dl.Chiave = ObjectToString(dr["Chiave"]);
                                dl.Stato = ObjectToString(dr["Stato"]);
                                dl.Utente = ObjectToString(dr["Utente"]);
                                dl.Ok = true;
                            }

                            dr.Close();
                        }
                    }

                    if (dl.Ok)
                    {
                        if (dl.Utente.Equals(Utente, StringComparison.OrdinalIgnoreCase))
                        {
                            l = Licenza.GiaRegistrato;

                            if (dl.Stato == "OK")
                                l = Licenza.RegistratoOK;
                        }
                        else
                        {
                            if (dl.Stato == "OK")
                            {
                                l = Licenza.GiaRegistrato;
                            }
                            else
                            {
                                var i = 0;
                                var z = "update Licenze set Utente = @Utente, Stato = @Stato where Chiave = @Chiave";

                                var p3 = new System.Data.OleDb.OleDbParameter[] {
                                    new System.Data.OleDb.OleDbParameter("Utente", Utente),
                                    new System.Data.OleDb.OleDbParameter("Stato", "OK"),
                                    new System.Data.OleDb.OleDbParameter("Chiave", Codice)
                                };

                                using (var cm2 = new System.Data.OleDb.OleDbCommand(z, c))
                                {
                                    cm2.Parameters.AddRange(p3);

                                    try
                                    {
                                        i = cm2.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                        i = 0;
                                    }
                                }

                                if (i > 0)
                                    l = Licenza.RegistratoOK;
                            }
                        }
                    }
                    else
                    {
                        l = Licenza.CodiceInesistente;
                    }
                }
                catch
                {
                    //error
                }

                if (c.State == System.Data.ConnectionState.Open)
                    c.Close();
            }

            return l;
        }

        private string ObjectToString(object o)
        {
            var s = "";

            try
            {
                s = (string)o;
            }
            catch
            {
                s = "";
            }

            return s;
        }

        private DateTime VersioneDB_pri(string email, string psw)
        {
            var dSERVER = DateTime.MinValue;

            if ((ControllaCredenzialiRC_simple(email, psw) == CredenzialiRisultato.TuttoOK))
                try
                {
                    var dira = Server.MapPath("App_Data");

                    if (System.IO.Directory.Exists(dira))
                    {
                        var f = System.IO.Path.Combine(dira, email + ".date");

                        if ((System.IO.File.Exists(f)))
                            using (var sr = new System.IO.StreamReader(f))
                            {
                                try
                                {
                                    dSERVER = DateTime.ParseExact(sr.ReadLine(), "yyyyMMddHHmmss", null);
                                }
                                catch
                                {
                                    //cannot parse
                                }

                                sr.Close();
                            }
                    }
                }
                catch
                {
                    //errore
                }

            return dSERVER;
        }


        private Comparazione DBSulServerEPiuNuovo(string yyyyMMddHHmmss, string email, string psw)
        {
            var c = Comparazione.AccessoNonAutizzato;

            if ((ControllaCredenzialiRC_simple(email, psw) == CredenzialiRisultato.TuttoOK))
                try
                {
                    var vSERVER = VersioneDB_pri(email, psw);
                    var vCLIENT = DateTime.ParseExact(yyyyMMddHHmmss, "yyyyMMddHHmmss", null);

                    var differenzaS = vSERVER - vCLIENT;
                    var differenzaC = vCLIENT - vSERVER;

                    if (differenzaS.TotalSeconds > 60)
                        c = Comparazione.Server;
                    else if (differenzaC.TotalSeconds > 60)
                        c = Comparazione.Client;
                    else
                        c = Comparazione.Uguale;
                }
                catch
                {
                    //errore
                }

            return c;
        }

        private byte[] OttieniUltimoDBRC_p(string yyyyMMddHHmmss, string email, string psw)
        {
            if ((ControllaCredenzialiRC_simple(email, psw) == CredenzialiRisultato.TuttoOK))
                if ((DBSulServerEPiuNuovo(yyyyMMddHHmmss, email, psw) == Comparazione.Server))
                {
                    var dira = Server.MapPath("App_Data");
                    var FName = System.IO.Path.Combine(dira, email + ".zip");

                    if ((System.IO.File.Exists(FName)))
                        try
                        {
                            using (var fs1 = System.IO.File.Open(FName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                            {
                                var b1 = new byte[fs1.Length];

                                fs1.Read(b1, 0, Convert.ToInt32(fs1.Length));
                                fs1.Close();

                                return b1;
                            }
                        }
                        catch
                        {
                            //errore
                        }
                }

            return null;
        }


    }
}