/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Data;
using System.Windows.Forms;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.UI.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cCasse : cMyUCRicerca
    {

        private new string SelectedID
        {
            get
            {
                var i = "";

                try
                {
                    i = Convert.ToString(((DataRowView)bindingSource1.Current).Row.ItemArray[0]);
                }
                catch
                {
                    //cannot convert
                }

                return i;
            }
        }

        public cCasse()
        {
            InitializeComponent();

            if (!DesignTime)
            {
                MyBindingSource = bindingSource1;
                dataGridView1.AutoGenerateColumns = false;
                RicercaAbilitata = false;
                StampaAbilitata = false;

                LoadData();
            }
        }

        public void LoadData()
        {
            var m = new DB.DataWrapper.cCasse();
            bindingSource1.DataSource = m.Ricerca();
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            Dettaglio(true);
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            Dettaglio(false);
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            Elimina();
        }

        private int NumeroMovimentiContenuti(string nome)
        {
            var m = new cMovimenti();

            return m.NumeroMovimentiPerCassa(nome);
        }

        private void Elimina()
        {
            if (MsgElimina())
            {
                var i = SelectedID;

                if (i != "")
                    if (NumeroMovimentiContenuti(i) > 0)
                    {
                        cGB.MsgBox("Non posso eliminare questa cassa perché contiene movimenti!", MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        var m = new DB.DataWrapper.cCasse();
                        m.Elimina(i);

                        LoadData();
                        AggiornaAlbero();
                    }
            }
        }

        private void AggiornaAlbero()
        {
            cGB.RationesCurareMainForm.AggiungiCasseExtra();
            cGB.RationesCurareMainForm.LoadAllCash();
        }

        private void Dettaglio(bool Nuovo)
        {
            using (var fi = new fCassa())
            {
                if (!Nuovo)
                    fi.ID_ = SelectedID;

                if (fi.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dettaglio(false);
        }


    }
}