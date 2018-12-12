/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fCassa : fMyForm
    {
        private bool ImmagineOK = false;
        private string ImageFile = "";
        private string ID__ = "";
        private byte[] ImageByte = null;

        public string ID_
        {
            set
            {
                ID__ = value;

                if (ID__ != "")
                {
                    var c = new DB.DataWrapper.cCasse(ID__, "", "", null);
                    eNome.Text = c.nomenuovo;
                    eValuta.SelectedValue = c.Valuta;
                    CaricaIMG(c.imgName);
                }
            }
        }

        public fCassa()
        {
            InitializeComponent();
            Carica();
        }

        private void Carica()
        {
            var c = new DB.DataWrapper.cCasse();

            eValuta.DisplayMember = "Descrizione";
            eValuta.ValueMember = "Valuta";
            eValuta.DataSource = c.ListaValute();
        }

        private void bCercaImmagine_Click(object sender, EventArgs e)
        {
            var k = System.Reflection.Assembly.GetEntryAssembly().Location;
            k = System.IO.Path.GetDirectoryName(k);
            k = System.IO.Path.Combine(k, "Casse_Immagini");

            ImageFile = "";
            ImmagineOK = false;

            using (var op = new OpenFileDialog()
            {
                Title = "Selezionare un immagine per la cassa",
                Multiselect = false,
                InitialDirectory = k,
                Filter = "Immagini|*.jpeg; *.jpg; *.png; *.bmp"
            })
                if (op.ShowDialog() == DialogResult.OK)
                    ImageFile = op.FileName;

            CaricaIMG();
        }

        private void CaricaIMG()
        {
            CaricaIMG(null);
        }

        private void CaricaIMG(byte[] b)
        {
            ImmagineOK = false;

            try
            {
                if (b != null)
                {
                    ImageByte = b;
                    pbCassa.Image = byteArrayToImage(b);

                    ImmagineOK = true;
                }
                else
                {
                    if (System.IO.File.Exists(ImageFile))
                    {
                        ImageByte = imageToByteArray(ImageFile);
                        //pbCassa.Image = Image.FromFile(ImageFile);
                        cGB.LoadImage_Try(ImageFile, ref pbCassa);

                        ImmagineOK = true;
                    }
                }
            }
            catch (Exception ex)
            {
                cGB.MsgBox(ex.Message, MessageBoxIcon.Exclamation);
            }
        }

        public byte[] imageToByteArray(string filename_)
        {
            using (var fs = new System.IO.FileStream(filename_, System.IO.FileMode.Open))
            {
                var MyData = new byte[fs.Length];

                fs.Read(MyData, 0, (int)fs.Length);
                fs.Close();

                return MyData;
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new System.IO.MemoryStream(byteArrayIn))
                return Image.FromStream(ms);
        }

        private int NumeroMovimentiContenuti()
        {
            var tot = 0;

            if (!ID__.Equals(eNome.Text, StringComparison.OrdinalIgnoreCase))
            {
                var m = new DB.DataWrapper.cMovimenti();
                tot += m.NumeroMovimentiPerCassa(ID__);
                tot += m.NumeroMovimentiPerCassa(eNome.Text);
            }

            return tot;
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            var ok = false;

            if (eNome.Text.Length > 0)
                if (ImmagineOK)
                    ok = true;

            if (ok)
            {
                if (NumeroMovimentiContenuti() > 0)
                {
                    cGB.MsgBox("Non posso apportare modifiche a questa cassa perché contiene movimenti!", MessageBoxIcon.Exclamation);
                }
                else
                {
                    var c = new DB.DataWrapper.cCasse(ID__, eNome.Text, eValuta.SelectedValue as string, ImageByte);

                    if (c.Salva() <= 0)
                    {
                        MsgErroreSalvataggio();
                    }
                    else
                    {
                        AggiornaAlbero();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
            else
            {
                cGB.MsgBox("Riempire tutti i campi!", MessageBoxIcon.Exclamation);
            }
        }

        private void AggiornaAlbero()
        {
            cGB.RationesCurareMainForm.AggiungiCasseExtra();
            cGB.RationesCurareMainForm.LoadAllCash();
        }

        private void fCassa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Salva();
        }


    }
}