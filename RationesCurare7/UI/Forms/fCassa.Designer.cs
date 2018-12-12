namespace RationesCurare7.UI.Forms
{
    partial class fCassa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCassa));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bCercaImmagine = new System.Windows.Forms.Button();
            this.bSalva = new System.Windows.Forms.Button();
            this.eNome = new System.Windows.Forms.TextBox();
            this.pbCassa = new System.Windows.Forms.PictureBox();
            this.eValuta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCassa)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Immagine";
            // 
            // bCercaImmagine
            // 
            this.bCercaImmagine.Image = global::RationesCurare7.Properties.Resources.zoom;
            this.bCercaImmagine.Location = new System.Drawing.Point(82, 129);
            this.bCercaImmagine.Name = "bCercaImmagine";
            this.bCercaImmagine.Size = new System.Drawing.Size(84, 25);
            this.bCercaImmagine.TabIndex = 2;
            this.bCercaImmagine.Text = "Scegli ...";
            this.bCercaImmagine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCercaImmagine.UseVisualStyleBackColor = true;
            this.bCercaImmagine.Click += new System.EventHandler(this.bCercaImmagine_Click);
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.Location = new System.Drawing.Point(15, 177);
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(84, 25);
            this.bSalva.TabIndex = 3;
            this.bSalva.Text = "(F5) Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSalva.UseVisualStyleBackColor = true;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // eNome
            // 
            this.eNome.Location = new System.Drawing.Point(15, 25);
            this.eNome.MaxLength = 20;
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(237, 20);
            this.eNome.TabIndex = 0;
            // 
            // pbCassa
            // 
            this.pbCassa.Location = new System.Drawing.Point(15, 122);
            this.pbCassa.Name = "pbCassa";
            this.pbCassa.Size = new System.Drawing.Size(32, 32);
            this.pbCassa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCassa.TabIndex = 5;
            this.pbCassa.TabStop = false;
            // 
            // eValuta
            // 
            this.eValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eValuta.FormattingEnabled = true;
            this.eValuta.Location = new System.Drawing.Point(15, 70);
            this.eValuta.Name = "eValuta";
            this.eValuta.Size = new System.Drawing.Size(237, 21);
            this.eValuta.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Valuta";
            // 
            // fCassa
            // 
            this.AcceptButton = this.bSalva;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 215);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.eValuta);
            this.Controls.Add(this.pbCassa);
            this.Controls.Add(this.eNome);
            this.Controls.Add(this.bSalva);
            this.Controls.Add(this.bCercaImmagine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fCassa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio cassa";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fCassa_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbCassa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bCercaImmagine;
        private System.Windows.Forms.Button bSalva;
        private System.Windows.Forms.TextBox eNome;
        private System.Windows.Forms.PictureBox pbCassa;
        private System.Windows.Forms.ComboBox eValuta;
        private System.Windows.Forms.Label label3;
    }
}