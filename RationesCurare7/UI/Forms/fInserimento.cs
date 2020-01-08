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
    public partial class fInserimento : fMyForm
    {
        public enum eModalita
        {
            Normale,
            Giroconto
        }

        private int ID__ = -1;
        public eModalita Modalita = eModalita.Normale;
        private string TipoLoaded = "";
        private string Tipo_ = "Saldo";
        public string TipoGiroconto_ = "Saldo";

        public string Tipo
        {
            get
            {
                return Tipo_;
            }
            set
            {
                var z = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                lCassa.Text = z;
                Tipo_ = value;
            }
        }

        public string TipoGiroconto
        {
            get
            {
                return TipoGiroconto_;
            }
            set
            {
                var z = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);

                lCassaGiroconto.Text = z;
                lCassaGiroconto.Visible = true;
                TipoGiroconto_ = value;
            }
        }

        public int ID_
        {
            set
            {
                ID__ = value;

                if (ID__ > -1)
                {
                    var m = new DB.DataWrapper.cMovimenti(ID__);

                    eNome.Text = m.nome;
                    eSoldi.Value = m.soldi;
                    eData.Value_ = m.data;
                    eDescrizione.Text = m.descrizione;
                    eMacroArea.Text = m.MacroArea;
                    Tipo = m.tipo;
                    TipoLoaded = m.tipo;
                }
            }
        }

        public double Saldo
        {
            set
            {
                eSoldi.setSaldo = value;
            }
        }

        public fInserimento()
        {
            InitializeComponent();

            var m = new DB.DataWrapper.cMovimenti();
            eDescrizione.AutoCompleteCustomSource = m.TutteLeDescrizioni();
            eMacroArea.AutoCompleteCustomSource = m.TutteLeMacroAree();
            eData.Value_ = DateTime.Now;
            eNome.Text = cGB.UtenteConnesso.UserName;
        }

        private void fInserimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Salva();
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            if (eSoldi.Selected && eSoldi.Modificato)
            {
                eSoldi.DoCalc();
            }
            else if (eSoldi.Value == 0)
            {
                cGB.MsgBox("Campo importo vuoto!", MessageBoxIcon.Exclamation);
            }
            else
            {
                if (Modalita == eModalita.Normale)
                    SalvaNormale();
                else
                    SalvaGiroconto();
            }
        }

        private void SalvaGiroconto()
        {
            var s = 0;

            var m1 = new DB.DataWrapper.cMovimenti()
            {
                ID = ID__,
                nome = eNome.Text,
                soldi = eSoldi.Value,
                data = eData.Value_,
                descrizione = eDescrizione.Text,
                MacroArea = eMacroArea.Text,
                tipo = (Tipo.Equals("Saldo", StringComparison.OrdinalIgnoreCase) ? TipoLoaded : Tipo)
            };

            var m2 = new DB.DataWrapper.cMovimenti()
            {
                ID = ID__,
                nome = eNome.Text,
                soldi = -1 * eSoldi.Value,
                data = eData.Value_,
                descrizione = eDescrizione.Text,
                MacroArea = eMacroArea.Text,
                tipo = TipoGiroconto
            };

            s += m1.Salva();
            s += m2.Salva();

            if (s == 2)
                this.DialogResult = DialogResult.OK;
            else
                MsgErroreSalvataggio();
        }

        private void SalvaNormale()
        {
            var m = new DB.DataWrapper.cMovimenti()
            {
                ID = ID__,
                nome = eNome.Text,
                soldi = eSoldi.Value,
                data = eData.Value_,
                descrizione = eDescrizione.Text,
                MacroArea = eMacroArea.Text,
                tipo = (Tipo.Equals("Saldo", StringComparison.OrdinalIgnoreCase) ? TipoLoaded : Tipo)
            };

            if (m.Salva() < 1)
                MsgErroreSalvataggio();
            else
                this.DialogResult = DialogResult.OK;
        }

        public string GetMacroArea4Descrizione(string desc_)
        {
            var m = new DB.DataWrapper.cMovimenti();
            var a = m.MacroAree_e_Descrizioni();

            foreach (var i in a)
                if (i.Descrizione.Equals(desc_, StringComparison.OrdinalIgnoreCase))
                    return eMacroArea.Text = i.MacroArea;

            return ("");
        }

        private void eSoldi_Leave(object sender, EventArgs e)
        {
            var r = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TipoGiroconto_);
            lCassaGiroconto.Text = $"{r}: {cGB.DoubleToMoneyStringValuta(eSoldi.Value * -1)}";

            var l = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Tipo_);
            lCassa.Text = $"{l}: {cGB.DoubleToMoneyStringValuta(eSoldi.Value)}";
        }

        private void eDescrizione_Leave(object sender, EventArgs e)
        {
            if ((eMacroArea.Text.Equals("")) || (eMacroArea.Text == null))
                eMacroArea.Text = GetMacroArea4Descrizione(eDescrizione.Text);
        }


    }
}