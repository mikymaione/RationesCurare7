/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fInserimentoPeriodico : fMyForm
    {
        private string Tipo = "";
        private int ID__ = -1;

        public int ID_
        {
            set
            {
                ID__ = value;

                if (ID__ > -1)
                {
                    var m = new DB.DataWrapper.cPeriodici(ID__);

                    ID__ = m.ID;
                    cbCassa.SelectedValue = m.tipo;
                    eNome.Text = m.nome;
                    eDescrizione.Text = m.descrizione;
                    eMacroArea.Text = m.MacroArea;
                    eSoldi.Value = m.soldi;
                    eNumGiorni.Value = m.NumeroGiorni;
                    cbPeriodicita.SelectedIndex = m.Periodicita_cComboItems_Index(m.TipoGiorniMese);
                    eData.Value_ = (m.TipoGiorniMese == 'G' ? m.PartendoDalGiorno : m.GiornoDelMese);
                    eScadenza.Value_ = m.Scadenza;
                    eScadenza.Checked = m.Scadenza > eData.Value_;

                    AbilitaPeriodicita();
                }
            }
        }

        public fInserimentoPeriodico() : this("") { }

        public fInserimentoPeriodico(string Tipo_)
        {
            InitializeComponent();

            if (!cGB.DesignTime)
            {
                Tipo = Tipo_;

                var p = new DB.DataWrapper.cPeriodici();
                var m = new DB.DataWrapper.cMovimenti();
                var c = new DB.DataWrapper.cCasse();

                cbCassa.ValueMember = "Nome";
                cbCassa.DisplayMember = "Nome";
                cbCassa.DataSource = c.ListaCasse();
                cbCassa.SelectedValue = Tipo;

                cbPeriodicita.ValueMember = "ID";
                cbPeriodicita.DisplayMember = "Valore";
                cbPeriodicita.Items.AddRange(p.Periodicita_cComboItems());

                eDescrizione.AutoCompleteCustomSource = m.TutteLeDescrizioni();
                eMacroArea.AutoCompleteCustomSource = m.TutteLeMacroAree();
                eData.Value_ = DateTime.Now;
                eNome.Text = cGB.DatiUtente.Nome;
                cbPeriodicita.SelectedIndex = 0;
            }
        }

        private void fInserimentoPeriodico_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    Salva();
                    break;
            }
        }

        private void Salva()
        {
            if (eSoldi.Selected)
            {
                eSoldi.DoCalc();
            }
            else
            {
                if (eSoldi.Value == 0)
                {
                    cGB.MsgBox("Campo importo vuoto!", MessageBoxIcon.Exclamation);
                }
                else
                {
                    var m = new DB.DataWrapper.cPeriodici();

                    m.ID = ID__;
                    m.tipo = cbCassa.Text;
                    m.nome = eNome.Text;
                    m.descrizione = eDescrizione.Text;
                    m.MacroArea = eMacroArea.Text;
                    m.soldi = eSoldi.Value;
                    m.NumeroGiorni = (int)eNumGiorni.Value;
                    m.TipoGiorniMese = m.Periodicita_cComboItems_Index(cbPeriodicita.SelectedIndex);

                    if (eScadenza.Checked)
                        m.Scadenza = eScadenza.Value_;

                    if (m.TipoGiorniMese == 'G')
                        m.PartendoDalGiorno = eData.Value_;
                    else
                        m.GiornoDelMese = eData.Value_;

                    if (m.Salva() <= 0)
                        MsgErroreSalvataggio();
                    else
                        DialogResult = DialogResult.OK;
                }
            }
        }

        private void AbilitaPeriodicita()
        {
            eNumGiorni.Enabled = (cbPeriodicita.SelectedIndex == 0);
        }

        private void cbPeriodicita_SelectedIndexChanged(object sender, EventArgs e)
        {
            AbilitaPeriodicita();
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            if (eSoldi.Selected)
                eSoldi.DoCalc();
            else
                Salva();
        }

        private void eDescrizione_Leave(object sender, EventArgs e)
        {
            if ((eMacroArea.Text == "") || (eMacroArea.Text == null))
            {
                var m = new DB.DataWrapper.cMovimenti();
                var a = m.MacroAree_e_Descrizioni();

                foreach (var i in a)
                    if (i.Descrizione.Equals(eDescrizione.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        eMacroArea.Text = i.MacroArea;
                        break;
                    }
            }
        }

        private void eData_Leave(object sender, EventArgs e)
        {
            eScadenza.MinDate = eData.Value_.AddDays(1);
        }


    }
}