/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.IO;
using System.Windows.Forms;
using RationesCurare7.GB;

namespace RationesCurare7.UI.Forms
{
    public partial class fOpzioniDb : fMyForm
    {
        public fOpzioniDb()
        {
            InitializeComponent();
            inipath();
        }

        private void inipath()
        {
            lLocazione.Text = cGB.DatiDBFisico.Path;
            eUtente.Text = cGB.DatiUtente.Email;
            ePsw.Text = cGB.DatiUtente.Psw;
            cbSync.Checked = cGB.DatiUtente.SincronizzaDB;
        }

        private void lLocazione_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cGB.StartExplorer(cGB.PathFolderDB());
        }

        private void bCopia_Click(object sender, EventArgs e)
        {
            Copia();
        }

        private void bRipristina_Click(object sender, EventArgs e)
        {
            Recupera();
        }

        private void fOpzioniDb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
                Copia(true);
            else if (e.KeyCode == Keys.F5)
                Salva();
        }

        private void Copia(bool daTastiera = false)
        {
            try
            {
                File.Copy(cGB.DatiDBFisico.Path, cGB.PathDBBackup(), true);

                if (daTastiera)
                    Close();
                else
                    cGB.MsgBox("Copia di backup eseguita!");
            }
            catch
            {
                cGB.MsgBox("Impossibile effettuare la copia di backup!", MessageBoxIcon.Exclamation);
            }
        }

        private void Recupera()
        {
            if (cGB.MsgBox("Sicuro di voler effettuare il ripristino dalla copia di backup?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    cGB.RationesCurareMainForm.ChiudiTutteSchede();

                    cGB.sDB.Connessione.Close();

                    File.Copy(cGB.PathDBBackup(), cGB.DatiDBFisico.Path, true);

                    cGB.sDB.Connessione.Open();

                    cGB.RationesCurareMainForm.LoadAllCash();

                    cGB.MsgBox("Recupero effettuato!");
                    Close();
                }
                catch
                {
                    cGB.MsgBox("Impossibile ripristinare dalla copia di backup!", MessageBoxIcon.Exclamation);
                }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            cGB.DatiUtente.SincronizzaDB = cbSync.Checked;

            if (cGB.DatiUtente.Aggiorna() > 0)
                Close();
            else
                MsgErroreSalvataggio();
        }

        private void bSync_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void Sync()
        {
            var y = bSync.Text;
            bSync.Text = "Sincronizzazione ...";

            Enabled = false;

            if (cGB.DatiUtente.SincronizzaDB)
            {
                if (cGB.sDB.UltimaModifica > DateTime.MinValue)
                {
                    var mc = new MikyCloud(cGB.DatiDBFisico.Path, cGB.DatiUtente.Email, cGB.DatiUtente.Psw);
                    mc.MandaDBSulSito(cGB.sDB.UltimaModifica, true);
                }
                else
                {
                    cGB.MsgBox("Non sono state apportate modifiche al DB locale, quindi non verrà sincronizzato!", MessageBoxIcon.Exclamation);
                }
            }

            bSync.Text = y;
            Enabled = true;
        }


    }
}