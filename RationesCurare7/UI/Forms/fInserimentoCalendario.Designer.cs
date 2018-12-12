namespace RationesCurare7.UI.Forms
{
    partial class fInserimentoCalendario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInserimentoCalendario));
            this.bSalva = new System.Windows.Forms.Button();
            this.eData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.Location = new System.Drawing.Point(194, 261);
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(90, 25);
            this.bSalva.TabIndex = 1;
            this.bSalva.Text = "(F5) Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSalva.UseVisualStyleBackColor = true;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // eData
            // 
            this.eData.Location = new System.Drawing.Point(15, 25);
            this.eData.Name = "eData";
            this.eData.ReadOnly = true;
            this.eData.Size = new System.Drawing.Size(82, 20);
            this.eData.TabIndex = 1;
            this.eData.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descrizione";
            // 
            // eDescrizione
            // 
            this.eDescrizione.Location = new System.Drawing.Point(15, 64);
            this.eDescrizione.MaxLength = 255;
            this.eDescrizione.Multiline = true;
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eDescrizione.Size = new System.Drawing.Size(269, 191);
            this.eDescrizione.TabIndex = 0;
            // 
            // fInserimentoCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 297);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eDescrizione);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eData);
            this.Controls.Add(this.bSalva);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fInserimentoCalendario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio evento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fInserimentoCalendario_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSalva;
        private System.Windows.Forms.TextBox eData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eDescrizione;
    }
}