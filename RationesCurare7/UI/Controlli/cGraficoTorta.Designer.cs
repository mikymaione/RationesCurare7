using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RationesCurare7.UI.Controlli
{
    partial class cGraficoTorta
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bSuccessivo = new System.Windows.Forms.Button();
            this.bPrecedente = new System.Windows.Forms.Button();
            this.bStampa = new System.Windows.Forms.Button();
            this.cbImporti = new System.Windows.Forms.ComboBox();
            this.bCerca = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eDa = new System.Windows.Forms.DateTimePicker();
            this.eA = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gbGrafico = new System.Windows.Forms.GroupBox();
            this.grafico = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.gbGrafico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grafico)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bSuccessivo);
            this.panel1.Controls.Add(this.bPrecedente);
            this.panel1.Controls.Add(this.bStampa);
            this.panel1.Controls.Add(this.cbImporti);
            this.panel1.Controls.Add(this.bCerca);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.eDa);
            this.panel1.Controls.Add(this.eA);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 33);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // bSuccessivo
            // 
            this.bSuccessivo.Image = global::RationesCurare7.Properties.Resources.resultset_next;
            this.bSuccessivo.Location = new System.Drawing.Point(656, 4);
            this.bSuccessivo.Name = "bSuccessivo";
            this.bSuccessivo.Size = new System.Drawing.Size(25, 25);
            this.bSuccessivo.TabIndex = 6;
            this.bSuccessivo.UseVisualStyleBackColor = true;
            this.bSuccessivo.Click += new System.EventHandler(this.bSuccessivo_Click);
            // 
            // bPrecedente
            // 
            this.bPrecedente.Image = global::RationesCurare7.Properties.Resources.resultset_previous;
            this.bPrecedente.Location = new System.Drawing.Point(625, 4);
            this.bPrecedente.Name = "bPrecedente";
            this.bPrecedente.Size = new System.Drawing.Size(25, 25);
            this.bPrecedente.TabIndex = 5;
            this.bPrecedente.UseVisualStyleBackColor = true;
            this.bPrecedente.Click += new System.EventHandler(this.bPrecedente_Click);
            // 
            // bStampa
            // 
            this.bStampa.Image = global::RationesCurare7.Properties.Resources.printer32;
            this.bStampa.Location = new System.Drawing.Point(544, 4);
            this.bStampa.Name = "bStampa";
            this.bStampa.Size = new System.Drawing.Size(75, 25);
            this.bStampa.TabIndex = 4;
            this.bStampa.Text = "Stampa";
            this.bStampa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bStampa.UseVisualStyleBackColor = true;
            this.bStampa.Click += new System.EventHandler(this.bStampa_Click);
            // 
            // cbImporti
            // 
            this.cbImporti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImporti.FormattingEnabled = true;
            this.cbImporti.Items.AddRange(new object[] {
            "Negativi",
            "Positivi",
            "Tutti"});
            this.cbImporti.Location = new System.Drawing.Point(330, 6);
            this.cbImporti.Name = "cbImporti";
            this.cbImporti.Size = new System.Drawing.Size(127, 21);
            this.cbImporti.TabIndex = 2;
            // 
            // bCerca
            // 
            this.bCerca.Image = global::RationesCurare7.Properties.Resources.zoom;
            this.bCerca.Location = new System.Drawing.Point(463, 4);
            this.bCerca.Name = "bCerca";
            this.bCerca.Size = new System.Drawing.Size(75, 25);
            this.bCerca.TabIndex = 3;
            this.bCerca.Text = "&Cerca";
            this.bCerca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCerca.UseVisualStyleBackColor = true;
            this.bCerca.Click += new System.EventHandler(this.bCerca_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 10);
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
            this.eA.Location = new System.Drawing.Point(170, 6);
            this.eA.Name = "eA";
            this.eA.Size = new System.Drawing.Size(103, 20);
            this.eA.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Importi";
            // 
            // gbGrafico
            // 
            this.gbGrafico.Controls.Add(this.grafico);
            this.gbGrafico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGrafico.Location = new System.Drawing.Point(0, 33);
            this.gbGrafico.Name = "gbGrafico";
            this.gbGrafico.Size = new System.Drawing.Size(894, 416);
            this.gbGrafico.TabIndex = 2;
            this.gbGrafico.TabStop = false;
            // 
            // grafico
            // 
            this.grafico.BackColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Angle = -90;
            chartArea2.AxisY.LabelStyle.Format = "c";
            chartArea2.Name = "grafico_area";
            this.grafico.ChartAreas.Add(chartArea2);
            this.grafico.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "legenda";
            legend2.Title = "Legenda";
            this.grafico.Legends.Add(legend2);
            this.grafico.Location = new System.Drawing.Point(3, 16);
            this.grafico.Name = "grafico";
            this.grafico.Size = new System.Drawing.Size(888, 397);
            this.grafico.TabIndex = 1;
            // 
            // cGraficoTorta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbGrafico);
            this.Controls.Add(this.panel1);
            this.Name = "cGraficoTorta";
            this.Size = new System.Drawing.Size(894, 449);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbGrafico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grafico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DateTimePicker eA;
        private Label label3;
        private Label label2;
        private DateTimePicker eDa;
        private Button bCerca;
        private GroupBox gbGrafico;
        private Chart grafico;        
        private ComboBox cbImporti;
        private Button bStampa;
        private Button bSuccessivo;
        private Button bPrecedente;
    }
}