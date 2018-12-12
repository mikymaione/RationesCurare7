﻿namespace RationesCurare7.UI.Forms
{
    partial class fDettaglioUtente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDettaglioUtente));
            this.eNome = new System.Windows.Forms.TextBox();
            this.ePathDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ePsw = new System.Windows.Forms.TextBox();
            this.eEmail = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bApriPathDB = new System.Windows.Forms.Button();
            this.pbImmagine = new System.Windows.Forms.PictureBox();
            this.bOk = new System.Windows.Forms.Button();
            this.bScegliDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImmagine)).BeginInit();
            this.SuspendLayout();
            // 
            // eNome
            // 
            this.eNome.Location = new System.Drawing.Point(15, 25);
            this.eNome.MaxLength = 255;
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(200, 20);
            this.eNome.TabIndex = 0;
            this.eNome.Leave += new System.EventHandler(this.eNome_Leave);
            // 
            // ePathDB
            // 
            this.ePathDB.Location = new System.Drawing.Point(15, 142);
            this.ePathDB.Name = "ePathDB";
            this.ePathDB.ReadOnly = true;
            this.ePathDB.Size = new System.Drawing.Size(322, 20);
            this.ePathDB.TabIndex = 3;
            this.ePathDB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "E-Mail (per poter recuperare la password)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Locazione DB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "(F2) Immagine";
            // 
            // ePsw
            // 
            this.ePsw.Location = new System.Drawing.Point(15, 64);
            this.ePsw.MaxLength = 255;
            this.ePsw.Name = "ePsw";
            this.ePsw.UseSystemPasswordChar = true;
            this.ePsw.Size = new System.Drawing.Size(200, 20);
            this.ePsw.TabIndex = 1;
            // 
            // eEmail
            // 
            this.eEmail.Location = new System.Drawing.Point(15, 103);
            this.eEmail.MaxLength = 80;
            this.eEmail.Name = "eEmail";
            this.eEmail.Size = new System.Drawing.Size(200, 20);
            this.eEmail.TabIndex = 2;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Informazione";
            // 
            // bApriPathDB
            // 
            this.bApriPathDB.Enabled = false;
            this.bApriPathDB.Image = global::RationesCurare7.Properties.Resources.folder_up;
            this.bApriPathDB.Location = new System.Drawing.Point(343, 140);
            this.bApriPathDB.Name = "bApriPathDB";
            this.bApriPathDB.Size = new System.Drawing.Size(25, 25);
            this.bApriPathDB.TabIndex = 4;
            this.toolTip1.SetToolTip(this.bApriPathDB, "Vai alla cartella del DB");
            this.bApriPathDB.UseVisualStyleBackColor = true;
            this.bApriPathDB.Click += new System.EventHandler(this.bApriPathDB_Click);
            // 
            // pbImmagine
            // 
            this.pbImmagine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbImmagine.Location = new System.Drawing.Point(257, 25);
            this.pbImmagine.Name = "pbImmagine";
            this.pbImmagine.Size = new System.Drawing.Size(111, 111);
            this.pbImmagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImmagine.TabIndex = 11;
            this.pbImmagine.TabStop = false;
            this.toolTip1.SetToolTip(this.pbImmagine, "Clicca per modificare");
            this.pbImmagine.Click += new System.EventHandler(this.pbImmagine_Click);
            // 
            // bOk
            // 
            this.bOk.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOk.Location = new System.Drawing.Point(296, 199);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(72, 25);
            this.bOk.TabIndex = 6;
            this.bOk.Text = "(F5) Ok";
            this.bOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bScegliDB
            // 
            this.bScegliDB.Image = global::RationesCurare7.Properties.Resources.folder_database;
            this.bScegliDB.Location = new System.Drawing.Point(15, 168);
            this.bScegliDB.Name = "bScegliDB";
            this.bScegliDB.Size = new System.Drawing.Size(200, 25);
            this.bScegliDB.TabIndex = 5;
            this.bScegliDB.Text = "(F3) Scegli dove salvare il DB";
            this.bScegliDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bScegliDB.UseVisualStyleBackColor = true;
            this.bScegliDB.Click += new System.EventHandler(this.bScegliDB_Click);
            // 
            // fDettaglioUtente
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 232);
            this.Controls.Add(this.bApriPathDB);
            this.Controls.Add(this.eEmail);
            this.Controls.Add(this.ePsw);
            this.Controls.Add(this.bScegliDB);
            this.Controls.Add(this.pbImmagine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ePathDB);
            this.Controls.Add(this.eNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fDettaglioUtente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio Utente";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fDettaglioUtente_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbImmagine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eNome;
        private System.Windows.Forms.TextBox ePathDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbImmagine;
        private System.Windows.Forms.Button bScegliDB;
        private System.Windows.Forms.TextBox ePsw;
        private System.Windows.Forms.TextBox eEmail;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button bApriPathDB;
    }
}