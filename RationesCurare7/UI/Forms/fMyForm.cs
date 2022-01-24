/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public class fMyForm : Form
    {
        public fMyForm()
        {
            KeyPreview = true;
            KeyDown += fMyForm_KeyDown;
        }

        private void fMyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
        }

        protected void MsgErroreSalvataggio()
        {
            cGB.MsgBox("Errore durante il salvataggio, riprovare!", MessageBoxIcon.Error);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(fMyForm));
            SuspendLayout();
            // 
            // fMyForm
            // 
            ClientSize = new Size(284, 262);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "fMyForm";
            ResumeLayout(false);

        }


    }
}