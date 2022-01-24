using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RationesCurare7.UI.Controlli
{
    partial class cGrafico
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
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grafico = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grafico_area = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bSuccessivo = new System.Windows.Forms.Button();
            this.bPrecedente = new System.Windows.Forms.Button();
            this.bStampa = new System.Windows.Forms.Button();
            this.cbPrevisti = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPeriodicita = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.eMacroArea = new System.Windows.Forms.TextBox();
            this.bCerca = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eDa = new System.Windows.Forms.DateTimePicker();
            this.eA = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.TextBox();
            this.gbGrafico = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.gbGrafico.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.grafico.BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bSuccessivo);
            this.panel1.Controls.Add(this.bPrecedente);
            this.panel1.Controls.Add(this.bStampa);
            this.panel1.Controls.Add(this.cbPrevisti);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbPeriodicita);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.eMacroArea);
            this.panel1.Controls.Add(this.bCerca);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.eDa);
            this.panel1.Controls.Add(this.eA);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.eDescrizione);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 55);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // bSuccessivo
            // 
            this.bSuccessivo.Image = global::RationesCurare7.Properties.Resources.resultset_next;
            this.bSuccessivo.Location = new System.Drawing.Point(620, 28);
            this.bSuccessivo.Name = "bSuccessivo";
            this.bSuccessivo.Size = new System.Drawing.Size(25, 25);
            this.bSuccessivo.TabIndex = 9;
            this.bSuccessivo.UseVisualStyleBackColor = true;
            this.bSuccessivo.Click += new System.EventHandler(this.bSuccessivo_Click);
            // 
            // bPrecedente
            // 
            this.bPrecedente.Image = global::RationesCurare7.Properties.Resources.resultset_previous;
            this.bPrecedente.Location = new System.Drawing.Point(589, 28);
            this.bPrecedente.Name = "bPrecedente";
            this.bPrecedente.Size = new System.Drawing.Size(25, 25);
            this.bPrecedente.TabIndex = 8;
            this.bPrecedente.UseVisualStyleBackColor = true;
            this.bPrecedente.Click += new System.EventHandler(this.bPrecedente_Click);
            // 
            // bStampa
            // 
            this.bStampa.Image = global::RationesCurare7.Properties.Resources.printer32;
            this.bStampa.Location = new System.Drawing.Point(509, 28);
            this.bStampa.Name = "bStampa";
            this.bStampa.Size = new System.Drawing.Size(74, 25);
            this.bStampa.TabIndex = 7;
            this.bStampa.Text = "&Stampa";
            this.bStampa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bStampa.UseVisualStyleBackColor = true;
            this.bStampa.Click += new System.EventHandler(this.bStampa_Click);
            // 
            // cbPrevisti
            // 
            this.cbPrevisti.AutoSize = true;
            this.cbPrevisti.Checked = true;
            this.cbPrevisti.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrevisti.Location = new System.Drawing.Point(370, 32);
            this.cbPrevisti.Name = "cbPrevisti";
            this.cbPrevisti.Size = new System.Drawing.Size(60, 17);
            this.cbPrevisti.TabIndex = 5;
            this.cbPrevisti.Text = "Previsti";
            this.cbPrevisti.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(367, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Periodicità";
            // 
            // cbPeriodicita
            // 
            this.cbPeriodicita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriodicita.FormattingEnabled = true;
            this.cbPeriodicita.Items.AddRange(new object[] { "Mensile", "Annuale" });
            this.cbPeriodicita.Location = new System.Drawing.Point(436, 6);
            this.cbPeriodicita.Name = "cbPeriodicita";
            this.cbPeriodicita.Size = new System.Drawing.Size(147, 21);
            this.cbPeriodicita.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Macro area";
            // 
            // eMacroArea
            // 
            this.eMacroArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eMacroArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eMacroArea.Location = new System.Drawing.Point(211, 32);
            this.eMacroArea.Name = "eMacroArea";
            this.eMacroArea.Size = new System.Drawing.Size(150, 20);
            this.eMacroArea.TabIndex = 3;
            // 
            // bCerca
            // 
            this.bCerca.Image = global::RationesCurare7.Properties.Resources.zoom;
            this.bCerca.Location = new System.Drawing.Point(436, 28);
            this.bCerca.Name = "bCerca";
            this.bCerca.Size = new System.Drawing.Size(67, 25);
            this.bCerca.TabIndex = 6;
            this.bCerca.Text = "&Cerca";
            this.bCerca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCerca.UseVisualStyleBackColor = true;
            this.bCerca.Click += new System.EventHandler(this.bCerca_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Da";
            // 
            // eDa
            // 
            this.eDa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eDa.Location = new System.Drawing.Point(34, 6);
            this.eDa.Name = "eDa";
            this.eDa.Size = new System.Drawing.Size(103, 20);
            this.eDa.TabIndex = 0;
            // 
            // eA
            // 
            this.eA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eA.Location = new System.Drawing.Point(34, 32);
            this.eA.Name = "eA";
            this.eA.Size = new System.Drawing.Size(103, 20);
            this.eA.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Descrizione";
            // 
            // eDescrizione
            // 
            this.eDescrizione.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.eDescrizione.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.eDescrizione.Location = new System.Drawing.Point(211, 6);
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.Size = new System.Drawing.Size(150, 20);
            this.eDescrizione.TabIndex = 2;
            // 
            // chartArea1
            // 
            this.grafico_area.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            this.grafico_area.AxisX.IsLabelAutoFit = false;
            this.grafico_area.AxisX.LabelStyle.Angle = -90;
            this.grafico_area.AxisY.LabelStyle.Format = "c";
            this.grafico_area.Name = "grafico_area";
            // 
            // chart1
            // 
            this.grafico.BackColor = System.Drawing.Color.Transparent;
            this.grafico.ChartAreas.Add(grafico_area);
            this.grafico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grafico.Name = "grafico";
            this.grafico.TabIndex = 1;
            // 
            // gbGrafico
            // 
            this.gbGrafico.Controls.Add(this.grafico);
            this.gbGrafico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGrafico.Location = new System.Drawing.Point(0, 55);
            this.gbGrafico.Name = "gbGrafico";
            this.gbGrafico.Size = new System.Drawing.Size(1000, 645);
            this.gbGrafico.TabIndex = 2;
            this.gbGrafico.TabStop = false;
            // 
            // cGrafico
            // 
            this.Controls.Add(this.gbGrafico);
            this.Controls.Add(this.panel1);
            this.Name = "cGrafico";
            this.Size = new System.Drawing.Size(1000, 700);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbGrafico.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.grafico.EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox eDescrizione;
        private DateTimePicker eA;
        private Label label3;
        private Label label2;
        private DateTimePicker eDa;
        private Button bCerca;
        private GroupBox gbGrafico;
        private Chart grafico;
        private ChartArea grafico_area;
        private Label label4;
        private TextBox eMacroArea;
        private Label label5;
        private ComboBox cbPeriodicita;
        private CheckBox cbPrevisti;
        private Button bStampa;
        private Button bSuccessivo;
        private Button bPrecedente;
    }
}