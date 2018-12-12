namespace RationesCurare7.UI.Forms
{
    partial class fOpzioniDb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fOpzioniDb));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bCopia = new System.Windows.Forms.ToolStripButton();
            this.bRipristina = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lLocazione = new System.Windows.Forms.LinkLabel();
            this.cbSync = new System.Windows.Forms.CheckBox();
            this.groupCredenziali = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ePsw = new System.Windows.Forms.TextBox();
            this.eUtente = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bSync = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupCredenziali.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bCopia,
            this.bRipristina});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(372, 38);
            this.toolStrip1.TabIndex = 0;
            // 
            // bCopia
            // 
            this.bCopia.Image = global::RationesCurare7.Properties.Resources.database_copy;
            this.bCopia.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bCopia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCopia.Name = "bCopia";
            this.bCopia.Size = new System.Drawing.Size(65, 35);
            this.bCopia.Text = "(F8) Copia";
            this.bCopia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bCopia.Click += new System.EventHandler(this.bCopia_Click);
            // 
            // bRipristina
            // 
            this.bRipristina.Image = global::RationesCurare7.Properties.Resources.database_refresh;
            this.bRipristina.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bRipristina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRipristina.Name = "bRipristina";
            this.bRipristina.Size = new System.Drawing.Size(60, 35);
            this.bRipristina.Text = "Ripristina";
            this.bRipristina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bRipristina.Click += new System.EventHandler(this.bRipristina_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Locazione database: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "La copia di backup è una copia che serve per rirpistinare il database";
            // 
            // lLocazione
            // 
            this.lLocazione.Location = new System.Drawing.Point(43, 183);
            this.lLocazione.Name = "lLocazione";
            this.lLocazione.Size = new System.Drawing.Size(317, 61);
            this.lLocazione.TabIndex = 3;
            this.lLocazione.TabStop = true;
            this.lLocazione.Text = "linkLabel1";
            this.lLocazione.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLocazione_LinkClicked);
            // 
            // cbSync
            // 
            this.cbSync.AutoSize = true;
            this.cbSync.Location = new System.Drawing.Point(37, 71);
            this.cbSync.Name = "cbSync";
            this.cbSync.Size = new System.Drawing.Size(162, 17);
            this.cbSync.TabIndex = 1;
            this.cbSync.Text = "Sincronizza database sul sito";
            this.cbSync.UseVisualStyleBackColor = true;
            // 
            // groupCredenziali
            // 
            this.groupCredenziali.Controls.Add(this.label4);
            this.groupCredenziali.Controls.Add(this.label3);
            this.groupCredenziali.Controls.Add(this.ePsw);
            this.groupCredenziali.Controls.Add(this.eUtente);
            this.groupCredenziali.Location = new System.Drawing.Point(37, 94);
            this.groupCredenziali.Name = "groupCredenziali";
            this.groupCredenziali.Size = new System.Drawing.Size(323, 62);
            this.groupCredenziali.TabIndex = 2;
            this.groupCredenziali.TabStop = false;
            this.groupCredenziali.Text = "Credenziali";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Utente:";
            // 
            // ePsw
            // 
            this.ePsw.Location = new System.Drawing.Point(158, 34);
            this.ePsw.Name = "ePsw";
            this.ePsw.ReadOnly = true;
            this.ePsw.Size = new System.Drawing.Size(142, 20);
            this.ePsw.TabIndex = 1;
            // 
            // eUtente
            // 
            this.eUtente.Location = new System.Drawing.Point(10, 34);
            this.eUtente.Name = "eUtente";
            this.eUtente.ReadOnly = true;
            this.eUtente.Size = new System.Drawing.Size(142, 20);
            this.eUtente.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bOk.Location = new System.Drawing.Point(275, 247);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(88, 25);
            this.bOk.TabIndex = 4;
            this.bOk.Text = "(F5) Salva";
            this.bOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::RationesCurare7.Properties.Resources.weather_clouds;
            this.pictureBox3.Location = new System.Drawing.Point(12, 71);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::RationesCurare7.Properties.Resources.folder_database;
            this.pictureBox2.Location = new System.Drawing.Point(12, 162);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.information;
            this.pictureBox1.Location = new System.Drawing.Point(12, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // bSync
            // 
            this.bSync.Image = global::RationesCurare7.Properties.Resources.world_connect;
            this.bSync.Location = new System.Drawing.Point(142, 247);
            this.bSync.Name = "bSync";
            this.bSync.Size = new System.Drawing.Size(127, 25);
            this.bSync.TabIndex = 7;
            this.bSync.Text = "Sincronizza ora";
            this.bSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSync.UseVisualStyleBackColor = true;
            this.bSync.Click += new System.EventHandler(this.bSync_Click);
            // 
            // fOpzioniDb
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 280);
            this.Controls.Add(this.bSync);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.groupCredenziali);
            this.Controls.Add(this.cbSync);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lLocazione);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fOpzioniDb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opzioni DB";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fOpzioniDb_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupCredenziali.ResumeLayout(false);
            this.groupCredenziali.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bCopia;
        private System.Windows.Forms.ToolStripButton bRipristina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel lLocazione;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox cbSync;
        private System.Windows.Forms.GroupBox groupCredenziali;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ePsw;
        private System.Windows.Forms.TextBox eUtente;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bSync;
    }
}