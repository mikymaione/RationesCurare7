/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

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
                DB.DataWrapper.cUtenti u = new DB.DataWrapper.cUtenti();
                u.CaricaByPath(value);
                ID__ = u.ID;

                if (u.ID > -1) //c'è
                {
                    LoadControls(u);
                }
                else
                {
                    bScegliDB.Enabled = false;

                    eNome.Text = System.IO.Path.GetFileNameWithoutExtension(value);

                    ePathDB.Text = System.IO.Path.GetDirectoryName(value);
                    CaricaImg(System.IO.Path.ChangeExtension(value, ".jpg"));
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

        public fDettaglioUtente(DB.DataWrapper.cUtenti default_) : this()
        {
            LoadControls(default_, true);
        }

        private void CaricaValute()
        {
            var c = new DB.DataWrapper.cValute();

            eValuta.DisplayMember = "Descrizione";
            eValuta.ValueMember = "Valuta";
            eValuta.DataSource = c.ListaValute();
            eValuta.SelectedIndex = -1;
        }

        private void LoadControls(DB.DataWrapper.cUtenti u, bool forza = false)
        {
            if (forza || u.ID > -1)
            {
                var p = "";

                bScegliDB.Enabled = false;

                try
                {
                    p = System.IO.Path.GetDirectoryName(u.Path);
                }
                catch
                {
                    //not found
                }

                var dbU = new DB.DataWrapper.cDBInfo(u.Email);

                ePsw.Text = dbU.Psw;
                eNome.Text = dbU.Nome;
                eEmail.Text = dbU.Email;
                ePathDB.Text = p;
                RecuperaDB = u.Path;
                eValuta.SelectedValue = dbU.Valuta;

                var jem = u.Email;
                if (jem == null || jem.Equals(""))
                    jem = eNome.Text;

                CaricaImg(System.IO.Path.Combine(ePathDB.Text, jem + ".jpg"));
            }
        }

        private void Carica()
        {
            if (ID_ > -1)
            {
                var u = new DB.DataWrapper.cUtenti(ID_);
                LoadControls(u);
            }
        }

        private bool Controlla()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(eEmail.Text, "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$"))
            {
                cGB.MsgBox("Campo email non corretto!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(eEmail.Text, "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$"))
            {
                cGB.MsgBox("Campo nome non corretto!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!System.IO.Directory.Exists(this.ePathDB.Text))
            {
                cGB.MsgBox("Selezionare una cartella di salvataggio per il DB!", MessageBoxIcon.Exclamation);
                return false;
            }

            if (!System.IO.File.Exists(ImgPath))
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
                var u = new DB.DataWrapper.cUtenti();
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
                    if (!System.IO.Directory.Exists(s.SelectedPath))
                        try
                        {
                            System.IO.Directory.CreateDirectory(s.SelectedPath);
                        }
                        catch (Exception ex)
                        {
                            cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                        }

                    if (System.IO.Directory.Exists(s.SelectedPath))
                    {
                        this.ePathDB.Text = s.SelectedPath;
                        this.bApriPathDB.Enabled = true;
                    }
                }
        }

        private void CaricaImg(string z)
        {
            try
            {
                //this.pbImmagine.Image = Image.FromFile(z);
                cGB.LoadImage(z, ref this.pbImmagine);

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
            var a = "";

            if (cGB.MsgBox("Tutto corretto?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                if (Controlla())
                {
                    var dbda = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    dbda = System.IO.Path.Combine(dbda, "standard.rqd8");

                    a = System.IO.Path.Combine(this.ePathDB.Text, this.eEmail.Text + ".rqd8");

                    if (RecuperaDB.Equals(""))
                    {
                        //crea DB nuovo 
                        if (ID__ <= -1)
                            try
                            {
                                if (!System.IO.Directory.Exists(this.ePathDB.Text))
                                    System.IO.Directory.CreateDirectory(this.ePathDB.Text);

                                if (!System.IO.File.Exists(a))
                                    System.IO.File.Copy(dbda, a, false);
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
                            if (!System.IO.Directory.Exists(this.ePathDB.Text))
                                System.IO.Directory.CreateDirectory(this.ePathDB.Text);

                            if (!System.IO.File.Exists(a))
                                System.IO.File.Copy(RecuperaDB, a, false);
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
                        var t = System.IO.Path.GetExtension(ImgPath);
                        var imga = System.IO.Path.Combine(this.ePathDB.Text, this.eEmail.Text);

                        System.IO.File.Copy(ImgPath, System.IO.Path.ChangeExtension(imga + t, ".jpg"), true);
                    }
                    catch //(Exception ex)
                    {
                        //erro = true;
                        //cGB.MsgBox("Errore! : " + ex.Message, MessageBoxIcon.Error);
                    }

                    if (!erro)
                    {
                        cGB.sDB = new DB.cDB(true, DB.cDB.DataBase.SQLite, a);

                        var x = new DB.DataWrapper.cUtenti(ID_)
                        {
                            Nome = eNome.Text,
                            Psw = ePsw.Text,
                            Email = eEmail.Text,
                            Path = a,
                            TipoDB = "S"
                        };

                        var u = new DB.DataWrapper.cDBInfo(eEmail.Text)
                        {
                            Nome = eNome.Text,
                            Psw = ePsw.Text,
                            Email = eEmail.Text,
                            UltimaModifica = DateTime.Now,
                            Valuta = eValuta.SelectedValue as string
                        };

                        if (x.Salva() > 0 && (u.DatiCaricati && u.Aggiorna() > 0 || u.Inserisci() > 0))
                            this.DialogResult = DialogResult.OK;
                        else
                            MsgErroreSalvataggio();
                    }
                }
        }

        private void eNome_Leave(object sender, EventArgs e)
        {
            var arr = eNome.Text.ToCharArray();
            arr = Array.FindAll(arr, (c => (char.IsLetterOrDigit(c) || char.IsNumber(c) || c == '_' || c == ' ')));
            eNome.Text = new string(arr);
        }

        private void pbImmagine_Click(object sender, EventArgs e)
        {
            pbImmagineClick();
        }

        private void pbImmagineClick()
        {
            var j = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            j = System.IO.Path.Combine(j, "Utenti");

            using (var op = new OpenFileDialog()
            {
                Title = "Selezionare un immagine per l'utente",
                Multiselect = false,
                Filter = "Immagini|*.jpg;*.png",
                InitialDirectory = j
            })
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    if (System.IO.File.Exists(op.FileName))
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