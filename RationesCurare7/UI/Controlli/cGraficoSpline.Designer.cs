namespace RationesCurare7.UI.Controlli
{
    partial class cGraficoSpline
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.grafico = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bStampa = new System.Windows.Forms.Button();
            this.bCerca = new System.Windows.Forms.Button();
            this.gbGrafico = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grafico)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbGrafico.SuspendLayout();
            this.SuspendLayout();
            // 
            // grafico
            // 
            this.grafico.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Angle = -90;
            chartArea1.AxisY.LabelStyle.Format = "c";
            chartArea1.Name = "grafico_area";
            this.grafico.ChartAreas.Add(chartArea1);
            this.grafico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grafico.Location = new System.Drawing.Point(3, 16);
            this.grafico.Name = "grafico";
            this.grafico.Size = new System.Drawing.Size(994, 626);
            this.grafico.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bStampa);
            this.panel1.Controls.Add(this.bCerca);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 55);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // bStampa
            // 
            this.bStampa.Image = global::RationesCurare7.Properties.Resources.printer32;
            this.bStampa.Location = new System.Drawing.Point(82, 5);
            this.bStampa.Name = "bStampa";
            this.bStampa.Size = new System.Drawing.Size(74, 25);
            this.bStampa.TabIndex = 7;
            this.bStampa.Text = "&Stampa";
            this.bStampa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bStampa.UseVisualStyleBackColor = true;
            this.bStampa.Click += new System.EventHandler(this.bStampa_Click);
            // 
            // bCerca
            // 
            this.bCerca.Image = global::RationesCurare7.Properties.Resources.zoom;
            this.bCerca.Location = new System.Drawing.Point(9, 5);
            this.bCerca.Name = "bCerca";
            this.bCerca.Size = new System.Drawing.Size(67, 25);
            this.bCerca.TabIndex = 6;
            this.bCerca.Text = "&Cerca";
            this.bCerca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCerca.UseVisualStyleBackColor = true;
            this.bCerca.Click += new System.EventHandler(this.bCerca_Click);
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
            // cGraficoSpline
            // 
            this.Controls.Add(this.gbGrafico);
            this.Controls.Add(this.panel1);
            this.Name = "cGraficoSpline";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grafico)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbGrafico.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCerca;
        private System.Windows.Forms.GroupBox gbGrafico;
        private System.Windows.Forms.DataVisualization.Charting.Chart grafico;
        private System.Windows.Forms.Button bStampa;
    }
}