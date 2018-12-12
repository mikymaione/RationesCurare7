/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cMovimentiPeriodici : cMyUCRicerca
    {
        private string CashName = "Saldo";


        public cMovimentiPeriodici()
        {
            InitializeComponent();

            if (!DesignTime)
            {
                MyBindingSource = bindingSource1;
                dataGridView1.AutoGenerateColumns = false;
                LoadData();
            }
        }

        private void LoadData()
        {
            var p = new DB.DataWrapper.cPeriodici();
            bindingSource1.DataSource = p.Ricerca();
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            Modifica(true);
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            Modifica(false);
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            if (MsgElimina())
            {
                var i = SelectedID;

                if (i > -1)
                {
                    var m = new DB.DataWrapper.cPeriodici();
                    m.Elimina(i);

                    LoadData();
                }
            }
        }

        private void Modifica(bool Nuovo)
        {
            using (var fi = new Forms.fInserimentoPeriodico(CashName))
            {
                if (!Nuovo)
                    fi.ID_ = SelectedID;

                if (fi.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modifica(false);
        }


    }
}