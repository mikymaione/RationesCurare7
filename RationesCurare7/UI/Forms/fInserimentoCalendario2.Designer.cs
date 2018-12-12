namespace RationesCurare7.UI.Forms
{
    partial class fInserimentoCalendario2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInserimentoCalendario2));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRipetiOgni = new System.Windows.Forms.ComboBox();
            this.lRipetiOgniN = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTermina = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.eTerminaData = new System.Windows.Forms.DateTimePicker();
            this.bSalva = new System.Windows.Forms.Button();
            this.eData = new System.Windows.Forms.TextBox();
            this.eRipetiOgniN = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.eNumeroOccorrenze = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.eRipetiOgniN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNumeroOccorrenze)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descrizione";
            // 
            // eDescrizione
            // 
            this.eDescrizione.Location = new System.Drawing.Point(258, 25);
            this.eDescrizione.Multiline = true;
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eDescrizione.Size = new System.Drawing.Size(269, 178);
            this.eDescrizione.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ripeti";
            // 
            // cbRipetiOgni
            // 
            this.cbRipetiOgni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRipetiOgni.FormattingEnabled = true;
            this.cbRipetiOgni.Location = new System.Drawing.Point(21, 64);
            this.cbRipetiOgni.Name = "cbRipetiOgni";
            this.cbRipetiOgni.Size = new System.Drawing.Size(200, 21);
            this.cbRipetiOgni.TabIndex = 1;
            this.cbRipetiOgni.SelectedIndexChanged += new System.EventHandler(this.cbRipetiOgni_SelectedIndexChanged);
            // 
            // lRipetiOgniN
            // 
            this.lRipetiOgniN.AutoSize = true;
            this.lRipetiOgniN.Location = new System.Drawing.Point(18, 88);
            this.lRipetiOgniN.Name = "lRipetiOgniN";
            this.lRipetiOgniN.Size = new System.Drawing.Size(67, 13);
            this.lRipetiOgniN.TabIndex = 6;
            this.lRipetiOgniN.Text = "Ripeti ogni #";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Termina";
            // 
            // cbTermina
            // 
            this.cbTermina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTermina.Enabled = false;
            this.cbTermina.FormattingEnabled = true;
            this.cbTermina.Location = new System.Drawing.Point(21, 143);
            this.cbTermina.Name = "cbTermina";
            this.cbTermina.Size = new System.Drawing.Size(200, 21);
            this.cbTermina.TabIndex = 3;
            this.cbTermina.SelectedIndexChanged += new System.EventHandler(this.cbTermina_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Termina in data";
            // 
            // eTerminaData
            // 
            this.eTerminaData.Enabled = false;
            this.eTerminaData.Location = new System.Drawing.Point(21, 183);
            this.eTerminaData.Name = "eTerminaData";
            this.eTerminaData.Size = new System.Drawing.Size(200, 20);
            this.eTerminaData.TabIndex = 4;
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.Location = new System.Drawing.Point(438, 217);
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(89, 25);
            this.bSalva.TabIndex = 6;
            this.bSalva.Text = "(F5) Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSalva.UseVisualStyleBackColor = true;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // eData
            // 
            this.eData.Location = new System.Drawing.Point(21, 25);
            this.eData.Name = "eData";
            this.eData.ReadOnly = true;
            this.eData.Size = new System.Drawing.Size(82, 20);
            this.eData.TabIndex = 0;
            this.eData.TabStop = false;
            // 
            // eRipetiOgniN
            // 
            // 
            // 
            //             
            this.eRipetiOgniN.Enabled = false;
            this.eRipetiOgniN.Location = new System.Drawing.Point(21, 104);
            this.eRipetiOgniN.Maximum = 10000;
            this.eRipetiOgniN.Minimum = 1;
            this.eRipetiOgniN.Name = "eRipetiOgniN";            
            this.eRipetiOgniN.Size = new System.Drawing.Size(80, 20);
            this.eRipetiOgniN.TabIndex = 2;
            this.eRipetiOgniN.Value = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Numero occorrenze";
            // 
            // eNumeroOccorrenze
            // 
            // 
            // 
            //             
            this.eNumeroOccorrenze.Enabled = false;
            this.eNumeroOccorrenze.Location = new System.Drawing.Point(21, 222);
            this.eNumeroOccorrenze.Maximum = 10000;
            this.eNumeroOccorrenze.Minimum = 1;
            this.eNumeroOccorrenze.Name = "eNumeroOccorrenze";            
            this.eNumeroOccorrenze.Size = new System.Drawing.Size(80, 20);
            this.eNumeroOccorrenze.TabIndex = 5;
            this.eNumeroOccorrenze.Value = 1;
            // 
            // fInserimentoCalendario2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 256);
            this.Controls.Add(this.eNumeroOccorrenze);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eRipetiOgniN);
            this.Controls.Add(this.eData);
            this.Controls.Add(this.bSalva);
            this.Controls.Add(this.eTerminaData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbTermina);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lRipetiOgniN);
            this.Controls.Add(this.cbRipetiOgni);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.eDescrizione);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fInserimentoCalendario2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio evento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fInserimentoCalendario2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.eRipetiOgniN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNumeroOccorrenze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eDescrizione;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRipetiOgni;
        private System.Windows.Forms.Label lRipetiOgniN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTermina;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker eTerminaData;
        private System.Windows.Forms.Button bSalva;
        private System.Windows.Forms.TextBox eData;
        private System.Windows.Forms.NumericUpDown eRipetiOgniN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown eNumeroOccorrenze;
    }
}