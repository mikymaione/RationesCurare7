/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using RationesCurare7.maionemikyWS;

namespace RationesCurare7.UI.Forms
{
    public partial class fPsw : Form
    {
        public string Email = "";
        public string PswC = "";

        public fPsw() : this("", "") { }

        public fPsw(string Email_, string PswC_)
        {
            Email = Email_;
            PswC = PswC_;

            InitializeComponent();
        }

        private void bRecupera_Click(object sender, EventArgs ea)
        {
            Enabled = false;

            try
            {
                using (EmailSending e = new EmailSending())
                    if (e.RecuperaPswRC_Six(PswC, Email))
                    {
                        cGB.MsgBox("Ti ho inviato la password via email!");
                    }
                    else
                    {
                        cGB.MsgBox("Invio non riuscito!", MessageBoxIcon.Exclamation);
                    }
            }
            catch (Exception ex)
            {
                cGB.MsgBox("Errore : " + ex.Message, MessageBoxIcon.Exclamation);
            }

            Enabled = true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (ePsw.Text == PswC)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                ePsw.BackColor = Color.Red;
            }
        }


    }
}