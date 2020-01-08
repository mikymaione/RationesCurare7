/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public class fMyForm : Form
    {
        public fMyForm()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.fMyForm_KeyDown);
        }

        private void fMyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        protected void MsgErroreSalvataggio()
        {
            cGB.MsgBox("Errore durante il salvataggio, riprovare!", MessageBoxIcon.Error);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMyForm));
            this.SuspendLayout();
            // 
            // fMyForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMyForm";
            this.ResumeLayout(false);

        }


    }
}