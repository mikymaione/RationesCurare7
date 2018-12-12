namespace RationesCurare7.UI.Controlli
{
    partial class cCosaNePensi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cCosaNePensi));
            this.eOggetto = new System.Windows.Forms.TextBox();
            this.eTesto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGiudizio = new System.Windows.Forms.ComboBox();
            this.bInvia = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // eOggetto
            // 
            this.eOggetto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eOggetto.Location = new System.Drawing.Point(15, 95);
            this.eOggetto.Name = "eOggetto";
            this.eOggetto.Size = new System.Drawing.Size(500, 20);
            this.eOggetto.TabIndex = 0;
            // 
            // eTesto
            // 
            this.eTesto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eTesto.Location = new System.Drawing.Point(15, 139);
            this.eTesto.Multiline = true;
            this.eTesto.Name = "eTesto";
            this.eTesto.Size = new System.Drawing.Size(500, 293);
            this.eTesto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oggetto :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Testo :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giudizio riguardo al programma :";
            // 
            // cbGiudizio
            // 
            this.cbGiudizio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGiudizio.FormattingEnabled = true;
            this.cbGiudizio.Items.AddRange(new object[] {
            "Ottimo",
            "Discreto",
            "Inutile"});
            this.cbGiudizio.Location = new System.Drawing.Point(15, 456);
            this.cbGiudizio.Name = "cbGiudizio";
            this.cbGiudizio.Size = new System.Drawing.Size(157, 21);
            this.cbGiudizio.TabIndex = 2;
            // 
            // bInvia
            // 
            this.bInvia.Image = global::RationesCurare7.Properties.Resources.email;
            this.bInvia.Location = new System.Drawing.Point(15, 495);
            this.bInvia.Name = "bInvia";
            this.bInvia.Size = new System.Drawing.Size(61, 25);
            this.bInvia.TabIndex = 3;
            this.bInvia.Text = "&Invia";
            this.bInvia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bInvia.UseVisualStyleBackColor = true;
            this.bInvia.Click += new System.EventHandler(this.bInvia_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.mail32;
            this.pictureBox1.Location = new System.Drawing.Point(15, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(53, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(462, 55);
            this.label4.TabIndex = 6;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // cCosaNePensi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbGiudizio);
            this.Controls.Add(this.bInvia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eTesto);
            this.Controls.Add(this.eOggetto);
            this.Name = "cCosaNePensi";
            this.Size = new System.Drawing.Size(531, 537);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eOggetto;
        private System.Windows.Forms.TextBox eTesto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bInvia;
        private System.Windows.Forms.ComboBox cbGiudizio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}
