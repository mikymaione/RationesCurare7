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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cUtente : UserControl
    {
        public cUtente()
        {
            InitializeComponent();

            if (!cGB.DesignTime)
            {
                AggiornaOrario();
                Carica();
            }
        }

        private void Carica()
        {
            string h = System.IO.Path.Combine(cGB.PathFolderDB(), cGB.UtenteConnesso.Email + ".jpg");
            bUtente.Text = cGB.UtenteConnesso.UserName;

            //iUtente.Image = Image.FromFile(h);
            cGB.LoadImage_Try(h, ref iUtente);
        }

        private void iUtente_Click(object sender, EventArgs e)
        {
            MostraUtente();
        }

        private void bUtente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MostraUtente();
        }

        private void MostraUtente()
        {
            using (UI.Forms.fDettaglioUtente u = new Forms.fDettaglioUtente())
            {
                DB.cDB.ApriConnessione(DB.cDB.DataBase.SQLite, cGB.PathDBUtenti, true);

                u.ID_ = cGB.UtenteConnesso.ID;
                u.ShowDialog();

                Carica();
                DB.cDB.ApriConnessione(DB.cDB.DataBase.SQLite, true);
            }
        }

        private void bDisconnetti_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Application.Restart();       
            cGB.RestartMe = true;
            cGB.RationesCurareMainForm.Close();
        }

        private void tOrologio_Tick(object sender, EventArgs e)
        {
            AggiornaOrario();
        }

        private void AggiornaOrario()
        {
            lOrario.Text = DateTime.Now.ToString();
        }


    }
}