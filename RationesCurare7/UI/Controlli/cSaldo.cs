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
    public partial class cSaldo : cMyUCRicerca
    {

        private string Query = "select * from movimenti"; //default
        private double CurSaldo = 0;
        private string CashName = "Saldo";
        private GB.cFiltriRicerca FiltriAttuali = new GB.cFiltriRicerca();
        private System.Data.DataTable UltimaRicerca = null;


        public cSaldo() : this("Saldo") { }

        public cSaldo(string CashName_) : this(CashName_, new GB.cFiltriRicerca()) { }

        public cSaldo(string CashName_, GB.cFiltriRicerca f)
        {
            InitializeComponent();

            if (!DesignTime)
            {
                MyBindingSource = bindingSource1;
                dataGridView1.AutoGenerateColumns = false;

                CashName = CashName_;
                LoadData(false, f);
            }
        }


        private void LoadData(bool ReloadCashInTreeView)
        {
            LoadData(ReloadCashInTreeView, FiltriAttuali);
        }

        private void LoadData(bool ReloadCashInTreeView, GB.cFiltriRicerca f)
        {
            var ImSaldo = (CashName == "Saldo");
            var cc = (f.bCassa ? f.Cassa : CashName);

            FiltriAttuali = f;

            var m = new DB.DataWrapper.cMovimenti()
            {
                tipo = cc,
                descrizione = cGB.QQ(f.Descrizione, f.bDescrizione),
                MacroArea = cGB.QQ(f.MacroAree, f.bMacroAree),
                SoldiDa = f.SoldiDa,
                SoldiA = f.SoldiA,
                bSoldi = f.bSoldi,
                bData = f.bData,
                DataA = f.DataA,
                DataDa = f.DataDa
            };

            UltimaRicerca = m.Ricerca(out Query);
            bindingSource1.DataSource = UltimaRicerca;
            CurSaldo = m.Saldo(cc);
            lSaldo.Text = cGB.DoubleToMoneyStringValuta(CurSaldo, CashName);

            var R = Math.Round(CurSaldo, 2);

            if (R != 0)
            {
                if (CurSaldo > 0)
                    iSaldo.Image = Properties.Resources.arrowGreen;
                else if (CurSaldo < 0)
                    iSaldo.Image = Properties.Resources.arrowRed;
            }

            bNuovo.Enabled = !ImSaldo;
            bGiroconto.Enabled = !ImSaldo;
            bPeriodico.Enabled = !ImSaldo;

            if (ReloadCashInTreeView)
                cGB.RationesCurareMainForm.LoadAllCash();
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            Modifica(true);
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            Modifica(false);
        }

        private void bGiroconto_Click(object sender, EventArgs e)
        {
            using (var g = new Forms.fGiroconto(CashName))
                if (g.ShowDialog() == DialogResult.OK)
                    using (var fi = new Forms.fInserimento())
                    {
                        fi.Modalita = Forms.fInserimento.eModalita.Giroconto;
                        fi.TipoGiroconto = g.CassaSelezionata;
                        fi.Tipo = CashName;
                        fi.Saldo = CurSaldo;

                        if (fi.ShowDialog() == DialogResult.OK)
                            LoadData(true);
                    }

            dataGridView1.Focus();
        }

        private void bPeriodico_Click(object sender, EventArgs e)
        {
            using (var fi = new Forms.fInserimentoPeriodico(CashName))
                fi.ShowDialog();

            dataGridView1.Focus();
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            if (MsgElimina())
            {
                var i = SelectedID;

                if (i > -1)
                {
                    var m = new DB.DataWrapper.cMovimenti();
                    m.Elimina(i);

                    LoadData(true);
                }
            }

            dataGridView1.Focus();
        }

        private void bSQL_Click(object sender, EventArgs e)
        {
            using (var f = new Forms.fSQL(Query))
                f.ShowDialog();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            if (RicercaAbilitata)
                cGB.RationesCurareMainForm.ShowCerca(CashName);

            dataGridView1.Focus();
        }

        private void bStampa_Click(object sender, EventArgs e)
        {
#if __MonoCS__
            cGB.MsgBox("Funzionalità di stampa non ancora supportate per il Mono Framework.", MessageBoxIcon.Exclamation);
#else
            Stampa();
#endif
        }

        private void Stampa()
        {
            if (StampaAbilitata && UltimaRicerca != null)
                using (var s = new UI.Stampe.fStampa(UltimaRicerca))
                    s.ShowDialog();

            dataGridView1.Focus();
        }

        private void Modifica(bool Nuovo)
        {
            using (var fi = new Forms.fInserimento())
            {
                if (!Nuovo)
                    fi.ID_ = SelectedID;

                fi.Tipo = CashName;
                fi.Saldo = CurSaldo;

                if (fi.ShowDialog() == DialogResult.OK)
                    LoadData(true);
            }

            dataGridView1.Focus();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modifica(false);
        }


    }
}