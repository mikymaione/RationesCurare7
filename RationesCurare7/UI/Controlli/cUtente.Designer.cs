namespace RationesCurare7.UI.Controlli
{
    partial class cUtente
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bDisconnetti = new System.Windows.Forms.LinkLabel();
            this.iUtente = new System.Windows.Forms.PictureBox();
            this.bUtente = new System.Windows.Forms.LinkLabel();
            this.tOrologio = new System.Windows.Forms.Timer(this.components);
            this.lOrario = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iUtente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bDisconnetti
            // 
            this.bDisconnetti.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.bDisconnetti.Image = global::RationesCurare7.Properties.Resources.user_log_out16;
            this.bDisconnetti.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDisconnetti.LinkColor = System.Drawing.SystemColors.ControlText;
            this.bDisconnetti.Location = new System.Drawing.Point(75, 23);
            this.bDisconnetti.Name = "bDisconnetti";
            this.bDisconnetti.Size = new System.Drawing.Size(79, 20);
            this.bDisconnetti.TabIndex = 2;
            this.bDisconnetti.TabStop = true;
            this.bDisconnetti.Text = "Disconnetti";
            this.bDisconnetti.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bDisconnetti.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.bDisconnetti.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bDisconnetti_LinkClicked);
            // 
            // iUtente
            // 
            this.iUtente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iUtente.Image = global::RationesCurare7.Properties.Resources.user50;
            this.iUtente.Location = new System.Drawing.Point(5, 5);
            this.iUtente.Name = "iUtente";
            this.iUtente.Size = new System.Drawing.Size(60, 60);
            this.iUtente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iUtente.TabIndex = 0;
            this.iUtente.TabStop = false;
            this.iUtente.Click += new System.EventHandler(this.iUtente_Click);
            // 
            // bUtente
            // 
            this.bUtente.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.bUtente.AutoSize = true;
            this.bUtente.LinkColor = System.Drawing.SystemColors.ControlText;
            this.bUtente.Location = new System.Drawing.Point(75, 5);
            this.bUtente.Name = "bUtente";
            this.bUtente.Size = new System.Drawing.Size(39, 13);
            this.bUtente.TabIndex = 3;
            this.bUtente.TabStop = true;
            this.bUtente.Text = "Utente";
            this.bUtente.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.bUtente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bUtente_LinkClicked);
            // 
            // tOrologio
            // 
            this.tOrologio.Enabled = true;
            this.tOrologio.Interval = 900;
            this.tOrologio.Tick += new System.EventHandler(this.tOrologio_Tick);
            // 
            // lOrario
            // 
            this.lOrario.AutoSize = true;
            this.lOrario.Location = new System.Drawing.Point(97, 50);
            this.lOrario.Name = "lOrario";
            this.lOrario.Size = new System.Drawing.Size(0, 13);
            this.lOrario.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.time;
            this.pictureBox1.Location = new System.Drawing.Point(75, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // cUtente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lOrario);
            this.Controls.Add(this.bUtente);
            this.Controls.Add(this.bDisconnetti);
            this.Controls.Add(this.iUtente);
            this.Name = "cUtente";
            this.Size = new System.Drawing.Size(349, 70);
            ((System.ComponentModel.ISupportInitialize)(this.iUtente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iUtente;
        private System.Windows.Forms.LinkLabel bDisconnetti;
        private System.Windows.Forms.LinkLabel bUtente;
        private System.Windows.Forms.Timer tOrologio;
        private System.Windows.Forms.Label lOrario;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
