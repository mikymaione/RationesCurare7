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
    public partial class fInserimentoCalendario : fMyForm
    {
        private string IDGruppo_ = "";
        private string ID__ = "";
        private DateTime DataSelezionata_;
        public bool ModificaSerie = false;

        public DateTime DataSelezionata
        {
            get
            {
                return DataSelezionata_;
            }
            set
            {
                DataSelezionata_ = value;
                eData.Text = value.ToShortDateString();
            }
        }

        public string ID_
        {
            set
            {
                ID__ = value;

                if (ID__ != "")
                {
                    DB.DataWrapper.cCalendario m = new DB.DataWrapper.cCalendario(ID__);

                    DataSelezionata = m.Giorno;
                    eDescrizione.Text = m.Descrizione;
                    IDGruppo_ = m.IDGruppo;
                }
            }
        }

        public fInserimentoCalendario()
        {
            InitializeComponent();
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            int j = -1;

            DB.DataWrapper.cCalendario m = new DB.DataWrapper.cCalendario()
            {
                ID = ID__,
                IDGruppo = IDGruppo_,
                Descrizione = eDescrizione.Text
            };

            if (ModificaSerie)
                j = m.AggiornaSerie();
            else
                j = m.Aggiorna();

            if (j <= 0)
                MsgErroreSalvataggio();
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void fInserimentoCalendario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Salva();
        }


    }
}