namespace RationesCurare7.UI.Forms
{
    partial class fInserimentoPeriodico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInserimentoPeriodico));
            this.eData = new RationesCurare7.UI.Controlli.cDateControl();
            this.bSalva = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.TextBox();
            this.lTipo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.eNome = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPeriodicita = new System.Windows.Forms.ComboBox();
            this.eNumGiorni = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCassa = new System.Windows.Forms.ComboBox();
            this.eSoldi = new RationesCurare7.UI.Controlli.cMathEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.eMacroArea = new System.Windows.Forms.TextBox();
            this.eScadenza = new RationesCurare7.UI.Controlli.cDateControl();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eNumGiorni)).BeginInit();
            this.SuspendLayout();
            // 
            // eData
            // 
            this.eData.Checked = true;
            this.eData.Location = new System.Drawing.Point(128, 171);
            this.eData.Name = "eData";
            this.eData.ShowCheckBox = false;
            this.eData.Size = new System.Drawing.Size(272, 23);
            this.eData.TabIndex = 5;
            this.eData.Value_ = new System.DateTime(2012, 9, 2, 9, 12, 0, 0);
            this.eData.Leave += new System.EventHandler(this.eData_Leave);
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.Location = new System.Drawing.Point(308, 266);
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(92, 25);
            this.bSalva.TabIndex = 8;
            this.bSalva.Text = "(F5) Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSalva.UseVisualStyleBackColor = true;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Descrizione :";
            // 
            // eDescrizione
            // 
            this.eDescrizione.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eDescrizione.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eDescrizione.Location = new System.Drawing.Point(128, 64);
            this.eDescrizione.MaxLength = 255;
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.Size = new System.Drawing.Size(272, 20);
            this.eDescrizione.TabIndex = 1;
            this.eDescrizione.Leave += new System.EventHandler(this.eDescrizione_Leave);
            // 
            // lTipo
            // 
            this.lTipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lTipo.Location = new System.Drawing.Point(9, 174);
            this.lTipo.Name = "lTipo";
            this.lTipo.Size = new System.Drawing.Size(113, 13);
            this.lTipo.TabIndex = 15;
            this.lTipo.Text = "Partendo dal giorno :";
            this.lTipo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Soldi :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nome :";
            // 
            // eNome
            // 
            this.eNome.Location = new System.Drawing.Point(128, 12);
            this.eNome.MaxLength = 255;
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(272, 20);
            this.eNome.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Periodicità :";
            // 
            // cbPeriodicita
            // 
            this.cbPeriodicita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriodicita.FormattingEnabled = true;
            this.cbPeriodicita.Location = new System.Drawing.Point(128, 144);
            this.cbPeriodicita.Name = "cbPeriodicita";
            this.cbPeriodicita.Size = new System.Drawing.Size(272, 21);
            this.cbPeriodicita.TabIndex = 4;
            this.cbPeriodicita.SelectedIndexChanged += new System.EventHandler(this.cbPeriodicita_SelectedIndexChanged);
            // 
            // eNumGiorni
            // 
            this.eNumGiorni.Location = new System.Drawing.Point(128, 196);
            this.eNumGiorni.Name = "eNumGiorni";
            this.eNumGiorni.Size = new System.Drawing.Size(66, 20);
            this.eNumGiorni.TabIndex = 6;
            this.eNumGiorni.Minimum = 0;
            this.eNumGiorni.Maximum = 3650;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(93, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Ogni";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "giorni";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Cassa :";
            // 
            // cbCassa
            // 
            this.cbCassa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCassa.FormattingEnabled = true;
            this.cbCassa.Location = new System.Drawing.Point(128, 117);
            this.cbCassa.Name = "cbCassa";
            this.cbCassa.Size = new System.Drawing.Size(272, 21);
            this.cbCassa.TabIndex = 3;
            // 
            // eSoldi
            // 
            this.eSoldi.Location = new System.Drawing.Point(128, 36);
            this.eSoldi.Name = "eSoldi";
            this.eSoldi.ShowCalcButton = true;
            this.eSoldi.Size = new System.Drawing.Size(122, 24);
            this.eSoldi.TabIndex = 0;
            this.eSoldi.Testo = "0";
            this.eSoldi.Value = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Macro area :";
            // 
            // eMacroArea
            // 
            this.eMacroArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eMacroArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eMacroArea.Location = new System.Drawing.Point(128, 90);
            this.eMacroArea.MaxLength = 255;
            this.eMacroArea.Name = "eMacroArea";
            this.eMacroArea.Size = new System.Drawing.Size(272, 20);
            this.eMacroArea.TabIndex = 2;
            // 
            // eScadenza
            // 
            this.eScadenza.Checked = false;
            this.eScadenza.Location = new System.Drawing.Point(128, 223);
            this.eScadenza.Name = "eScadenza";
            this.eScadenza.ShowCheckBox = true;
            this.eScadenza.Size = new System.Drawing.Size(272, 23);
            this.eScadenza.TabIndex = 7;
            this.eScadenza.Value_ = new System.DateTime(2012, 9, 2, 9, 12, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Scadenza :";
            // 
            // fInserimentoPeriodico
            // 
            this.AcceptButton = this.bSalva;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 299);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.eScadenza);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.eMacroArea);
            this.Controls.Add(this.eSoldi);
            this.Controls.Add(this.cbCassa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.eNumGiorni);
            this.Controls.Add(this.cbPeriodicita);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eData);
            this.Controls.Add(this.bSalva);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eDescrizione);
            this.Controls.Add(this.lTipo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fInserimentoPeriodico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio movimento periodico";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fInserimentoPeriodico_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.eNumGiorni)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controlli.cDateControl eData;
        private System.Windows.Forms.Button bSalva;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox eDescrizione;
        private System.Windows.Forms.Label lTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eNome;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPeriodicita;
        private System.Windows.Forms.NumericUpDown eNumGiorni;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCassa;
        private Controlli.cMathEdit eSoldi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox eMacroArea;
        private Controlli.cDateControl eScadenza;
        private System.Windows.Forms.Label label9;
    }
}