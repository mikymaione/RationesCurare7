namespace RationesCurare7.UI.Forms
{
    partial class fInserimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInserimento));
            this.eNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bSalva = new System.Windows.Forms.Button();
            this.eSoldi = new RationesCurare7.UI.Controlli.cMathEdit();
            this.eData = new RationesCurare7.UI.Controlli.cDateControl();
            this.label5 = new System.Windows.Forms.Label();
            this.eMacroArea = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lCassaGiroconto = new System.Windows.Forms.Label();
            this.lCassa = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // eNome
            // 
            this.eNome.Location = new System.Drawing.Point(85, 12);
            this.eNome.MaxLength = 255;
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(272, 20);
            this.eNome.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nome :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Soldi :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Data :";
            // 
            // eDescrizione
            // 
            this.eDescrizione.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eDescrizione.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eDescrizione.Location = new System.Drawing.Point(85, 90);
            this.eDescrizione.MaxLength = 255;
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.Size = new System.Drawing.Size(272, 20);
            this.eDescrizione.TabIndex = 2;
            this.eDescrizione.Leave += new System.EventHandler(this.eDescrizione_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Descrizione :";
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.Location = new System.Drawing.Point(265, 177);
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(92, 25);
            this.bSalva.TabIndex = 4;
            this.bSalva.Text = "(F5) Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bSalva.UseVisualStyleBackColor = true;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // eSoldi
            // 
            this.eSoldi.Location = new System.Drawing.Point(85, 36);
            this.eSoldi.Name = "eSoldi";
            this.eSoldi.ShowCalcButton = true;
            this.eSoldi.Size = new System.Drawing.Size(122, 24);
            this.eSoldi.TabIndex = 0;
            this.eSoldi.Testo = "0";
            this.eSoldi.Value = 0;
            this.eSoldi.Leave += eSoldi_Leave;
            // 
            // eData
            // 
            this.eData.Checked = true;
            this.eData.Location = new System.Drawing.Point(85, 64);
            this.eData.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.eData.Name = "eData";
            this.eData.ShowCheckBox = false;
            this.eData.Size = new System.Drawing.Size(272, 23);
            this.eData.TabIndex = 1;
            this.eData.Value_ = new System.DateTime(2012, 9, 2, 9, 12, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Macro area :";
            // 
            // eMacroArea
            // 
            this.eMacroArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eMacroArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eMacroArea.Location = new System.Drawing.Point(85, 116);
            this.eMacroArea.MaxLength = 255;
            this.eMacroArea.Name = "eMacroArea";
            this.eMacroArea.Size = new System.Drawing.Size(272, 20);
            this.eMacroArea.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Cassa :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lCassaGiroconto);
            this.panel1.Controls.Add(this.lCassa);
            this.panel1.Location = new System.Drawing.Point(85, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 27);
            this.panel1.TabIndex = 11;
            // 
            // lCassaGiroconto
            // 
            this.lCassaGiroconto.AutoSize = true;
            this.lCassaGiroconto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lCassaGiroconto.Location = new System.Drawing.Point(0, 13);
            this.lCassaGiroconto.Name = "lCassaGiroconto";
            this.lCassaGiroconto.Size = new System.Drawing.Size(34, 13);
            this.lCassaGiroconto.TabIndex = 17;
            this.lCassaGiroconto.Text = "Saldo";
            this.lCassaGiroconto.Visible = false;
            // 
            // lCassa
            // 
            this.lCassa.AutoSize = true;
            this.lCassa.Dock = System.Windows.Forms.DockStyle.Top;
            this.lCassa.Location = new System.Drawing.Point(0, 0);
            this.lCassa.Name = "lCassa";
            this.lCassa.Size = new System.Drawing.Size(34, 13);
            this.lCassa.TabIndex = 15;
            this.lCassa.Text = "Saldo";
            // 
            // fInserimento
            // 
            this.AcceptButton = this.bSalva;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 210);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eMacroArea);
            this.Controls.Add(this.eData);
            this.Controls.Add(this.eSoldi);
            this.Controls.Add(this.bSalva);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eDescrizione);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fInserimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dettaglio movimento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fInserimento_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox eNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox eDescrizione;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bSalva;
        public UI.Controlli.cMathEdit eSoldi;
        public Controlli.cDateControl eData;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox eMacroArea;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lCassaGiroconto;
        private System.Windows.Forms.Label lCassa;
    }
}