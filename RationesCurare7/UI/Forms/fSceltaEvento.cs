/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Windows.Forms;
using RationesCurare7.GB;

namespace RationesCurare7.UI.Forms
{
    public partial class fSceltaEvento : fMyForm
    {
        public cComboItem Selezionato;

        public cComboItem[] Elementi
        {
            set
            {
                cbEventi.Items.Clear();

                cbEventi.ValueMember = "ID";
                cbEventi.DisplayMember = "Valore";

                if (value != null)
                    if (value.Length > 0)
                    {
                        cbEventi.Items.AddRange(value);
                        cbEventi.SelectedIndex = 0;
                    }
            }
        }

        public fSceltaEvento()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Selezionato = (cComboItem)cbEventi.SelectedItem;
            DialogResult = DialogResult.OK;
        }


    }
}