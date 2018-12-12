/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2017 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare7.UI.Forms
{
    public class fDBDate : fMyForm
    {
        private System.Windows.Forms.Label lLocale;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bNo;
        private System.Windows.Forms.Label lServer;
        private System.Windows.Forms.Label label1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDBDate));
            this.label1 = new System.Windows.Forms.Label();
            this.lLocale = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.bNo = new System.Windows.Forms.Button();
            this.lServer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Il database sul server è più aggiornato di quello locale; Vuoi scaricare la versione aggiornata?";
            // 
            // lLocale
            // 
            this.lLocale.AutoSize = true;
            this.lLocale.Location = new System.Drawing.Point(12, 9);
            this.lLocale.Name = "lLocale";
            this.lLocale.Size = new System.Drawing.Size(85, 13);
            this.lLocale.TabIndex = 1;
            this.lLocale.Text = "Versione locale: ";
            // 
            // bOk
            // 
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.bOk.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOk.Location = new System.Drawing.Point(12, 98);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(60, 25);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "Sì";
            this.bOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // bNo
            // 
            this.bNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.bNo.Image = global::RationesCurare7.Properties.Resources.delete32;
            this.bNo.Location = new System.Drawing.Point(78, 98);
            this.bNo.Name = "bNo";
            this.bNo.Size = new System.Drawing.Size(60, 25);
            this.bNo.TabIndex = 3;
            this.bNo.Text = "No";
            this.bNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bNo.UseVisualStyleBackColor = true;
            // 
            // lServer
            // 
            this.lServer.AutoSize = true;
            this.lServer.Location = new System.Drawing.Point(13, 32);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(86, 13);
            this.lServer.TabIndex = 4;
            this.lServer.Text = "Versione server: ";
            // 
            // fDBDate
            // 
            this.AcceptButton = this.bOk;
            this.CancelButton = this.bNo;
            this.ClientSize = new System.Drawing.Size(298, 133);
            this.Controls.Add(this.lServer);
            this.Controls.Add(this.bNo);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.lLocale);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fDBDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aggiornamento DataBase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        public fDBDate(DateTime VersioneLocale, DateTime VersioneServer)
        {
            InitializeComponent();

            lLocale.Text += VersioneLocale.ToString("dd/MM/yyyy HH:mm:ss");
            lServer.Text += VersioneServer.ToString("dd/MM/yyyy HH:mm:ss");
        }


    }
}