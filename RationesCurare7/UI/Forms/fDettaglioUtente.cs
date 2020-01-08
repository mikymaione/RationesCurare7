/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fDettaglioUtente : fMyForm
    {
        private string ImgPath = "";
        private int ID__ = -1;
        private string tipoDB = "S";
        private string RecuperaDB = "";

        public string DBPath
        {
            set
            {
                DB.DataWrapper.cUtente u = new DB.DataWrapper.cUtente();
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
            ImgPath = cGB.LoadImage_Casuale_Try(ref pbImmagine);
        }

        public fDettaglioUtente(DB.DataWrapper.cUtente default_) : this()
        {
            LoadControls(default_, true);
        }

        private void LoadControls(DB.DataWrapper.cUtente u, bool forza = false)
        {
            if (forza || u.ID > -1)
            {
                var p = "";

                bScegliDB.Enabled = false;

                try
                {
                    p = System.IO.Path.GetDirectoryName(u.path);
                }
                catch
                {
                    //not found
                }

                tipoDB = u.TipoDB;
                ePsw.Text = u.psw;
                eNome.Text = u.nome;
                eEmail.Text = u.Email;
                ePathDB.Text = p;
                RecuperaDB = u.path;

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
                DB.DataWrapper.cUtente u = new DB.DataWrapper.cUtente(ID_);
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

            if (ID__ <= -1)
            {
                DB.DataWrapper.cUtente u = new DB.DataWrapper.cUtente();
                List<DB.DataWrapper.cUtente> us = u.ListaUtenti();

                if (us != null)
                if (us.Count > 0)
                    foreach (DB.DataWrapper.cUtente usi in us)
                        if (usi.nome.Equals(eNome.Text, StringComparison.OrdinalIgnoreCase))
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
            using (FolderBrowserDialog s = new FolderBrowserDialog())
                if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

                a = System.IO.Path.Combine(this.ePathDB.Text, this.eEmail.Text + ".rqd" + (tipoDB.Equals("S") ? "8" : ""));

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
                    var u = new DB.DataWrapper.cUtente()
                    {
                        ID = ID_,
                        nome = eNome.Text,
                        psw = ePsw.Text,
                        Email = eEmail.Text,
                        path = a,
                        TipoDB = tipoDB,
                        UltimaModifica = DateTime.Now
                    };

                    if (u.Salva() <= 0)
                    {
                        MsgErroreSalvataggio();
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
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