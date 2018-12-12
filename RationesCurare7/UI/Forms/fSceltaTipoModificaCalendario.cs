/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
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
    public partial class fSceltaTipoModificaCalendario : Form
    {
        public enum eTipo
        {
            Serie, Singolo
        }

        public eTipo Tipo = eTipo.Singolo;

        public fSceltaTipoModificaCalendario()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Tipo = (rbSingolo.Checked ? eTipo.Singolo : eTipo.Serie);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


    }
}