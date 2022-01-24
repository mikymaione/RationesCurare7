using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    partial class fPsw
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPsw));
            this.label1 = new System.Windows.Forms.Label();
            this.ePsw = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bOK = new System.Windows.Forms.Button();
            this.bRecupera = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Password per l\'account:";
            // 
            // ePsw
            // 
            this.ePsw.Location = new System.Drawing.Point(12, 34);
            this.ePsw.Name = "ePsw";
            this.ePsw.Size = new System.Drawing.Size(170, 20);
            this.ePsw.TabIndex = 0;
            this.ePsw.UseSystemPasswordChar = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Informazione";
            // 
            // bOK
            // 
            this.bOK.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOK.Location = new System.Drawing.Point(118, 60);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(64, 25);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "Ok";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bRecupera
            // 
            this.bRecupera.Image = global::RationesCurare7.Properties.Resources.email_star;
            this.bRecupera.Location = new System.Drawing.Point(12, 60);
            this.bRecupera.Name = "bRecupera";
            this.bRecupera.Size = new System.Drawing.Size(100, 25);
            this.bRecupera.TabIndex = 3;
            this.bRecupera.Text = "Recupera";
            this.bRecupera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.bRecupera, "Inviami la password via email");
            this.bRecupera.UseVisualStyleBackColor = true;
            this.bRecupera.Click += new System.EventHandler(this.bRecupera_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.key;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // fPsw
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 99);
            this.Controls.Add(this.bRecupera);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.ePsw);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPsw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox ePsw;
        private Button bOK;
        private PictureBox pictureBox1;
        private Button bRecupera;
        private ToolTip toolTip1;
    }
}