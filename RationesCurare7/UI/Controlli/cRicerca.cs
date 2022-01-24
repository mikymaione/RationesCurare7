/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.GB;
using RationesCurare7.Properties;

namespace RationesCurare7.UI.Controlli
{
    public partial class cRicerca : cMyUserControl
    {

        private string AutoSetCassa = "";


        public cRicerca() : this("") { }

        public cRicerca(string AutoSetCassa_)
        {
            InitializeComponent();

            if (!DesignTime)
            {
                LatoDaDisegnare = new sLatoBordo
                {
                    Setted = true
                };

                AutoSetCassa = AutoSetCassa_;
                AcceptButton = bCerca;
                AutoFocus = eDataDa;

                var m = new cMovimenti();
                eDescrizione.AutoCompleteCustomSource = m.TutteLeDescrizioni();
                eMacroArea.AutoCompleteCustomSource = m.TutteLeMacroAree();
                eDataDa.Value = new DateTime(2005, 1, 1);
                eDataA.Value = cGB.DateTo235959(DateTime.Now.AddYears(1));

                Carica();
            }
        }


        void Carica()
        {
            var c = new DB.DataWrapper.cCasse();

            cbCassa.DisplayMember = "Nome";
            cbCassa.ValueMember = "Nome";
            cbCassa.DataSource = c.ListaCasse();

            if (!cGB.StringIsNullorEmpty(AutoSetCassa))
            {
                cbAttivaCassa.Checked = true;
                cbCassa.SelectedValue = AutoSetCassa;
            }
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            cGB.RationesCurareMainForm.ShowCash("Saldo", Resources.saldo32, new cFiltriRicerca
            {
                bCassa = cbAttivaCassa.Checked,
                bData = cbAttivaData.Checked,
                bDescrizione = cbAttivaDescrizione.Checked,
                bMacroAree = cbAttivaMacroArea.Checked,
                bSoldi = cbAttivaImporto.Checked,
                Cassa = cbCassa.Text,
                DataDa = cGB.DateTo00000(eDataDa.Value),
                DataA = cGB.DateTo235959(eDataA.Value),
                Descrizione = eDescrizione.Text,
                MacroAree = eMacroArea.Text,
                SoldiDa = eSoldiDa.Value,
                SoldiA = eSoldiA.Value
            });
        }


    }
}