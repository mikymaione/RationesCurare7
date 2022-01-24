/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2017 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RationesCurare7.Properties;

namespace RationesCurare7.UI.Forms
{
    public class fDBDate : fMyForm
    {
        private Label lLocale;
        private Button bOk;
        private Button bNo;
        private Label lServer;
        private Label label1;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(fDBDate));
            label1 = new Label();
            lLocale = new Label();
            bOk = new Button();
            bNo = new Button();
            lServer = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                             | AnchorStyles.Right;
            label1.Location = new Point(13, 59);
            label1.Name = "label1";
            label1.Size = new Size(273, 32);
            label1.TabIndex = 0;
            label1.Text = "Il database sul server è più aggiornato di quello locale; Vuoi scaricare la versione aggiornata?";
            // 
            // lLocale
            // 
            lLocale.AutoSize = true;
            lLocale.Location = new Point(12, 9);
            lLocale.Name = "lLocale";
            lLocale.Size = new Size(85, 13);
            lLocale.TabIndex = 1;
            lLocale.Text = "Versione locale: ";
            // 
            // bOk
            // 
            bOk.DialogResult = DialogResult.Yes;
            bOk.Image = Resources.accept;
            bOk.Location = new Point(12, 98);
            bOk.Name = "bOk";
            bOk.Size = new Size(60, 25);
            bOk.TabIndex = 2;
            bOk.Text = "Sì";
            bOk.TextImageRelation = TextImageRelation.ImageBeforeText;
            bOk.UseVisualStyleBackColor = true;
            // 
            // bNo
            // 
            bNo.DialogResult = DialogResult.No;
            bNo.Image = Resources.delete32;
            bNo.Location = new Point(78, 98);
            bNo.Name = "bNo";
            bNo.Size = new Size(60, 25);
            bNo.TabIndex = 3;
            bNo.Text = "No";
            bNo.TextImageRelation = TextImageRelation.ImageBeforeText;
            bNo.UseVisualStyleBackColor = true;
            // 
            // lServer
            // 
            lServer.AutoSize = true;
            lServer.Location = new Point(13, 32);
            lServer.Name = "lServer";
            lServer.Size = new Size(86, 13);
            lServer.TabIndex = 4;
            lServer.Text = "Versione server: ";
            // 
            // fDBDate
            // 
            AcceptButton = bOk;
            CancelButton = bNo;
            ClientSize = new Size(298, 133);
            Controls.Add(lServer);
            Controls.Add(bNo);
            Controls.Add(bOk);
            Controls.Add(lLocale);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "fDBDate";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aggiornamento DataBase";
            ResumeLayout(false);
            PerformLayout();

        }


        public fDBDate(DateTime VersioneLocale, DateTime VersioneServer)
        {
            InitializeComponent();

            lLocale.Text += VersioneLocale.ToString("dd/MM/yyyy HH:mm:ss");
            lServer.Text += VersioneServer.ToString("dd/MM/yyyy HH:mm:ss");
        }


    }
}