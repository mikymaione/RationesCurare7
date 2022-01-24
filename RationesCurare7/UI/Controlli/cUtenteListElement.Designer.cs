using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cUtenteListElement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lPathDB = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lNomeUtente = new System.Windows.Forms.Label();
            this.pUtente = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pUtente)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pUtente);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 101);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lPathDB);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(100, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 101);
            this.panel2.TabIndex = 4;
            // 
            // lPathDB
            // 
            this.lPathDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPathDB.Location = new System.Drawing.Point(0, 37);
            this.lPathDB.Name = "lPathDB";
            this.lPathDB.Size = new System.Drawing.Size(219, 64);
            this.lPathDB.TabIndex = 4;
            this.lPathDB.Click += new System.EventHandler(this.lPathDB_Click);
            this.lPathDB.DoubleClick += new System.EventHandler(this.lPathDB_DoubleClick);
            this.lPathDB.MouseEnter += new System.EventHandler(this.lPathDB_MouseEnter);
            this.lPathDB.MouseLeave += new System.EventHandler(this.lPathDB_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lNomeUtente);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(219, 37);
            this.panel3.TabIndex = 5;
            this.panel3.Click += new System.EventHandler(this.panel3_Click);
            this.panel3.DoubleClick += new System.EventHandler(this.panel3_DoubleClick);
            this.panel3.MouseEnter += new System.EventHandler(this.panel3_MouseEnter);
            this.panel3.MouseLeave += new System.EventHandler(this.panel3_MouseLeave);
            // 
            // lNomeUtente
            // 
            this.lNomeUtente.AutoSize = true;
            this.lNomeUtente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNomeUtente.Location = new System.Drawing.Point(0, 10);
            this.lNomeUtente.Name = "lNomeUtente";
            this.lNomeUtente.Size = new System.Drawing.Size(0, 13);
            this.lNomeUtente.TabIndex = 4;
            // 
            // pUtente
            // 
            this.pUtente.Image = global::RationesCurare7.Properties.Resources.url;
            this.pUtente.Location = new System.Drawing.Point(10, 10);
            this.pUtente.Name = "pUtente";
            this.pUtente.Size = new System.Drawing.Size(80, 80);
            this.pUtente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pUtente.TabIndex = 1;
            this.pUtente.TabStop = false;
            this.pUtente.Click += new System.EventHandler(this.pUtente_Click);
            this.pUtente.DoubleClick += new System.EventHandler(this.pUtente_DoubleClick);
            this.pUtente.MouseEnter += new System.EventHandler(this.pUtente_MouseEnter);
            this.pUtente.MouseLeave += new System.EventHandler(this.pUtente_MouseLeave);
            // 
            // cUtenteListElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "cUtenteListElement";
            this.Size = new System.Drawing.Size(319, 101);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pUtente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private PictureBox pUtente;
        private Panel panel2;
        private Label lPathDB;
        private Panel panel3;
        private Label lNomeUtente;

    }
}
