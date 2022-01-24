using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cUtente
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.bUtente = new System.Windows.Forms.LinkLabel();
            this.lSaldo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bDisconnetti = new System.Windows.Forms.LinkLabel();
            this.iUtente = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iUtente)).BeginInit();
            this.SuspendLayout();
            // 
            // bUtente
            // 
            this.bUtente.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.bUtente.AutoSize = true;
            this.bUtente.LinkColor = System.Drawing.SystemColors.ControlText;
            this.bUtente.Location = new System.Drawing.Point(75, 5);
            this.bUtente.Name = "bUtente";            
            this.bUtente.TabIndex = 3;
            this.bUtente.TabStop = true;
            this.bUtente.Text = "Utente";
            this.bUtente.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lSaldo
            // 
            this.lSaldo.AutoSize = true;
            this.lSaldo.Location = new System.Drawing.Point(95, 50);
            this.lSaldo.Name = "lSaldo";            
            this.lSaldo.TabIndex = 4;
            this.lSaldo.Text = "0 €";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.money;
            this.pictureBox1.Location = new System.Drawing.Point(77, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // bDisconnetti
            // 
			this.bDisconnetti.AutoSize = true;
            this.bDisconnetti.ActiveLinkColor = System.Drawing.SystemColors.ControlText;            
            this.bDisconnetti.LinkColor = System.Drawing.SystemColors.ControlText;
            this.bDisconnetti.Location = new System.Drawing.Point(75, 23);
            this.bDisconnetti.Name = "bDisconnetti";            
            this.bDisconnetti.TabIndex = 2;
            this.bDisconnetti.TabStop = true;
            this.bDisconnetti.Text = "Disconnetti";            
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
            // 
            // cUtente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lSaldo);
            this.Controls.Add(this.bUtente);
            this.Controls.Add(this.bDisconnetti);
            this.Controls.Add(this.iUtente);
            this.Name = "cUtente";
            this.Size = new System.Drawing.Size(349, 70);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iUtente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox iUtente;
        private LinkLabel bDisconnetti;
        private LinkLabel bUtente;
        public Label lSaldo;
        private PictureBox pictureBox1;
    }
}
