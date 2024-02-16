/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RationesCurare7.DB;
using RationesCurare7.DB.DataWrapper;

namespace RationesCurare7.UI.Forms
{
    public partial class fDettaglioUtente : fMyForm
    {
        private string ImgPath = "";
        private int ID__ = -1;
        private string RecuperaDB = "";

        public string DBPath
        {
            set
            {
                var u = new cUtenti();
                u.CaricaByPath(value);
                ID__ = u.ID;

                if (u.ID > -1) //c'è
                {
                    LoadControls(u);
                }
                else
                {
                    bScegliDB.Enabled = false;

                    eNome.Text = Path.GetFileNameWithoutExtension(value);

                    ePathDB.Text = Path.GetDirectoryName(value);
                    CaricaImg(Path.ChangeExtension(value, ".jpg"));
                }
            }
        }

        public int ID_
        {
            get
            {
                return ID__;
            }
            set
            {
                ID__ = value;
                Carica();
            }
        }

        public fDettaglioUtente()
        {
            InitializeComponent();
            CaricaValute();
            ImgPath = cGB.LoadImage_Casuale_Try(ref pbImmagine);
        }

        public fDettaglioUtente(cUtenti default_) : this()
        {
            LoadControls(default_, true);
        }

        private void CaricaValute()
        {
            var c = new cValute();

            eValuta.DisplayMember = "Descrizione";
            eValuta.ValueMember = "Valuta";
            eValuta.DataSource = c.ListaValute();
            eValuta.SelectedIndex = -1;
        }

        private void LoadControls(cUtenti u, bool forza = false)
        {
            if (forza || u.ID > -1)
            {
                var p = "";

                bScegliDB.Enabled = false;

                try
                {
                    p = Path.GetDirectoryName(u.Path);
                }
                catch
                {
                    //not found
                }

                if (cGB.sDB == null)
                {
                    cGB.DatiDBFisico = new cUtenti(u.ID);
                    cGB.sDB = new cDB(true, cDB.DataBase.SQLite);
                }

                var dbU = new cDBInfo(u.Email);

                ePsw.Text = dbU.Psw;
                eNome.Text = dbU.Nome;
                eEmail.Text = dbU.Email;
                ePathDB.Text = p;
                RecuperaDB = u.Path;
                eValuta.SelectedValue = dbU.Valuta;

                var jem = u.Email;
                if (jem == null || jem.Equals(""))
                    jem = eNome.Text;

                CaricaImg(Path.Combine(ePathDB.Text, jem + ".jpg"));
            }
        }

        private void Carica()
        {
            if (ID_ > -1)
            {
                var u = new cUtenti(ID_);
                LoadControls(u);
            }
        }

        private bool Controlla()
        {
            if (!Regex.IsMatch(eEmail.Text, "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$"))
            {
                cGB.MsgBox("Campo email non corretto!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!Regex.IsMatch(eEmail.Text, "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$"))
            {
                cGB.MsgBox("Campo nome non corretto!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!Directory.Exists(ePathDB.Text))
            {
                cGB.MsgBox("Selezionare una cartella di salvataggio per il DB!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!File.Exists(ImgPath))
            {
                cGB.MsgBox("Selezionare un'immagine per l'utente!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (eValuta.SelectedIndex < 0)
            {
                cGB.MsgBox("Selezionare una valuta!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (ID__ <= -1)
            {
                var u = new cUtenti();
                var us = u.ListaUtenti();

                if (us != null)
                    if (us.Count > 0)
                        foreach (var usi in us)
                            if (usi.Nome.Equals(eNome.Text, StringComparison.OrdinalIgnoreCase))
                            {
                                cGB.MsgBox("È già presente un utente con lo stesso nome!", MessageBoxIcon.Exclamation);
                                return false;
                            }
            }

            return true;
        }

        private void bScegliDB_Click(object sender, EventArgs e)
        {
            bScegliDBClick();
        }

        private void bScegliDBClick()
        {
            using (var s = new FolderBrowserDialog())
                if (s.ShowDialog() == DialogResult.OK)
                {
                    if (!Directory.Exists(s.SelectedPath))
                        try
                        {
                            Directory.CreateDirectory(s.SelectedPath);
                        }
                        catch (Exception ex)
                        {
                            cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                        }

                    if (Directory.Exists(s.SelectedPath))
                    {
                        ePathDB.Text = s.SelectedPath;
                        bApriPathDB.Enabled = true;
                    }
                }
        }

        private void CaricaImg(string z)
        {
            try
            {
                //this.pbImmagine.Image = Image.FromFile(z);
                cGB.LoadImage(z, ref pbImmagine);

                ImgPath = z;
            }
            catch (Exception ex)
            {
                ImgPath = cGB.LoadImage_Casuale_Try(ref pbImmagine);
                cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            var erro = false;

            if (cGB.MsgBox("Tutto corretto?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (Controlla())
                {
                    var dbda = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    dbda = Path.Combine(dbda, "standard.rqd8");

                    var a = Path.Combine(ePathDB.Text, eEmail.Text + ".rqd8");

                    if (RecuperaDB.Equals(""))
                    {
                        //crea DB nuovo 
                        if (ID__ <= -1)
                            try
                            {
                                if (!Directory.Exists(ePathDB.Text))
                                    Directory.CreateDirectory(ePathDB.Text);

                                if (!File.Exists(a))
                                    File.Copy(dbda, a, false);
                            }
                            catch (Exception ex)
                            {
                                erro = true;
                                cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                            }
                    }
                    else
                    {
                        // recupero DB
                        try
                        {
                            if (!Directory.Exists(ePathDB.Text))
                                Directory.CreateDirectory(ePathDB.Text);

                            if (!File.Exists(a))
                                File.Copy(RecuperaDB, a, false);
                        }
                        catch (Exception ex)
                        {
                            erro = true;
                            cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                        }
                    }

                    //copia img
                    try
                    {
                        var t = Path.GetExtension(ImgPath);
                        var imga = Path.Combine(ePathDB.Text, eEmail.Text);

                        File.Copy(ImgPath, Path.ChangeExtension(imga + t, ".jpg"), true);
                    }
                    catch //(Exception ex)
                    {
                        //erro = true;
                        //cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                    }

                    if (!erro)
                    {
                        cGB.sDB = new cDB(true, cDB.DataBase.SQLite, a);

                        var x = new cUtenti(ID_)
                        {
                            Nome = eNome.Text,
                            Psw = ePsw.Text,
                            Email = eEmail.Text,
                            Path = a,
                            TipoDB = "S"
                        };

                        var u = new cDBInfo(eEmail.Text)
                        {
                            Nome = eNome.Text,
                            Psw = ePsw.Text,
                            Email = eEmail.Text,
                            UltimaModifica = DateTime.Now,
                            Valuta = eValuta.SelectedValue as string
                        };

                        if (x.Salva() > 0 && (u.DatiCaricati && u.Aggiorna() > 0 || u.Inserisci() > 0))
                            DialogResult = DialogResult.OK;
                        else
                            MsgErroreSalvataggio();
                    }
                }
        }

        private void eNome_Leave(object sender, EventArgs e)
        {
            var arr = eNome.Text.ToCharArray();
            arr = Array.FindAll(arr, c => char.IsLetterOrDigit(c) || char.IsNumber(c) || c == '_' || c == ' ');
            eNome.Text = new string(arr);
        }

        private void pbImmagine_Click(object sender, EventArgs e)
        {
            pbImmagineClick();
        }

        private void pbImmagineClick()
        {
            var j = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            j = Path.Combine(j, "Utenti");

            using (var op = new OpenFileDialog
            {
                Title = "Selezionare un immagine per l'utente",
                Multiselect = false,
                Filter = "Immagini|*.jpg;*.png",
                InitialDirectory = j
            })
                if (op.ShowDialog() == DialogResult.OK)
                    if (File.Exists(op.FileName))
                        CaricaImg(op.FileName);
        }

        private void bApriPathDB_Click(object sender, EventArgs e)
        {
            cGB.StartExplorer(ePathDB.Text);
        }

        private void fDettaglioUtente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                bScegliDBClick();
            else if (e.KeyCode == Keys.F2)
                pbImmagineClick();
            else if (e.KeyCode == Keys.F5)
                Salva();
        }


    }
}