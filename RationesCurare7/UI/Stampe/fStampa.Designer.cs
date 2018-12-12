namespace RationesCurare7.UI.Stampe
{
    partial class fStampa
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fStampa));
            this.movimentiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsDB = new RationesCurare7.UI.Stampe.dsDB();
            #if __MonoCS__
            #else
            this.ReportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            #endif

            ((System.ComponentModel.ISupportInitialize)(this.movimentiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // movimentiBindingSource
            // 
            this.movimentiBindingSource.DataMember = "Movimenti";
            this.movimentiBindingSource.DataSource = this.dsDB;
            // 
            // dsDB
            // 
            this.dsDB.DataSetName = "dsDB";
            this.dsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportViewer1
            // 
            reportDataSource1.Name = "dsMovimenti";
            reportDataSource1.Value = this.movimentiBindingSource;

            #if __MonoCS__
            #else
            this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewer1.LocalReport.ReportEmbeddedResource = "RationesCurare7.UI.Stampe.Saldo.rdlc";
            this.ReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer1.Name = "ReportViewer1";
            this.ReportViewer1.Size = new System.Drawing.Size(661, 366);
            this.ReportViewer1.TabIndex = 1;
            #endif
                       
            // 
            // fStampa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 366);

            #if __MonoCS__
            #else
            this.Controls.Add(this.ReportViewer1);
            #endif

            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fStampa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stampa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fStampa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.movimentiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #if __MonoCS__
        #else
        private Microsoft.Reporting.WinForms.ReportViewer ReportViewer1;
        #endif
        private System.Windows.Forms.BindingSource movimentiBindingSource;
        private dsDB dsDB;

    }
}