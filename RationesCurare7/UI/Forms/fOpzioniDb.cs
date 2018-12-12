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
    public partial class fOpzioniDb : fMyForm
    {
        public fOpzioniDb()
        {
            InitializeComponent();
            inipath();
        }

        private void inipath()
        {
            lLocazione.Text = cGB.UtenteConnesso.PathDB;
            eUtente.Text = cGB.UtenteConnesso.Email;
            ePsw.Text = cGB.UtenteConnesso.Psw;
            cbSync.Checked = cGB.OpzioniProgramma.SincronizzaDB;
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
                System.IO.File.Copy(cGB.UtenteConnesso.PathDB, cGB.PathDBBackup(), true);

                if (daTastiera)
                    this.Close();
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
            if (cGB.MsgBox("Sicuro di voler effettuare il ripristino dalla copia di backup?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                try
                {
                    cGB.RationesCurareMainForm.ChiudiTutteSchede();

                    DB.cDB.ChiudiConnessione();

                    System.IO.File.Copy(cGB.PathDBBackup(), cGB.UtenteConnesso.PathDB, true);

                    DB.cDB.ApriConnessione();
                    cGB.RationesCurareMainForm.LoadAllCash();

                    cGB.MsgBox("Recupero effettuato!");
                    this.Close();
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
            cGB.OpzioniProgramma.SincronizzaDB = cbSync.Checked;
            cGB.OpzioniProgramma.Salva();

            if (cGB.OpzioniProgramma.FileSavedCorrectly)
                this.Close();
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

            this.Enabled = false;

            if (cGB.OpzioniProgramma.SincronizzaDB)
            {
                var mc = new GB.MikyCloud(cGB.UtenteConnesso);
                mc.MandaDBSulSito(DB.cDB.UltimaModifica, true);
            }

            bSync.Text = y;
            this.Enabled = true;
        }


    }
}