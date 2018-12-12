namespace RationesCurare7.UI.Controlli
{
    partial class cCalendario
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bNuovo = new System.Windows.Forms.ToolStripButton();
            this.bModifica = new System.Windows.Forms.ToolStripButton();
            this.bElimina = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bMesePrecedente = new System.Windows.Forms.ToolStripButton();
            this.bMeseSuccessivo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bVaiAdOggi = new System.Windows.Forms.ToolStripButton();
            this.bVaiA = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cCalendar1 = new RationesCurare7.UI.Controlli.cCalendar();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 40);
            this.panel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNuovo,
            this.bModifica,
            this.bElimina,
            this.toolStripSeparator1,
            this.bMesePrecedente,
            this.bMeseSuccessivo,
            this.toolStripSeparator2,
            this.bVaiAdOggi,
            this.bVaiA});
            this.toolStrip1.Location = new System.Drawing.Point(1, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(688, 40);
            this.toolStrip1.TabIndex = 4;
            // 
            // bNuovo
            // 
            this.bNuovo.Image = global::RationesCurare7.Properties.Resources.Add32;
            this.bNuovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNuovo.Name = "bNuovo";
            this.bNuovo.Size = new System.Drawing.Size(53, 37);
            this.bNuovo.Text = "&Inserisci";
            this.bNuovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNuovo.Click += new System.EventHandler(this.bNuovo_Click);
            // 
            // bModifica
            // 
            this.bModifica.Image = global::RationesCurare7.Properties.Resources.edit32;
            this.bModifica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bModifica.Name = "bModifica";
            this.bModifica.Size = new System.Drawing.Size(58, 37);
            this.bModifica.Text = "&Modifica";
            this.bModifica.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bModifica.Click += new System.EventHandler(this.bModifica_Click);
            // 
            // bElimina
            // 
            this.bElimina.Image = global::RationesCurare7.Properties.Resources.delete32;
            this.bElimina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bElimina.Name = "bElimina";
            this.bElimina.Size = new System.Drawing.Size(50, 37);
            this.bElimina.Text = "&Elimina";
            this.bElimina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bElimina.Click += new System.EventHandler(this.bElimina_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // bMesePrecedente
            // 
            this.bMesePrecedente.Image = global::RationesCurare7.Properties.Resources.resultset_previous;
            this.bMesePrecedente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMesePrecedente.Name = "bMesePrecedente";
            this.bMesePrecedente.Size = new System.Drawing.Size(101, 37);
            this.bMesePrecedente.Text = "Mese &precedente";
            this.bMesePrecedente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bMesePrecedente.Click += new System.EventHandler(this.bMesePrecedente_Click);
            // 
            // bMeseSuccessivo
            // 
            this.bMeseSuccessivo.Image = global::RationesCurare7.Properties.Resources.resultset_next;
            this.bMeseSuccessivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMeseSuccessivo.Name = "bMeseSuccessivo";
            this.bMeseSuccessivo.Size = new System.Drawing.Size(98, 37);
            this.bMeseSuccessivo.Text = "Mese &successivo";
            this.bMeseSuccessivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bMeseSuccessivo.Click += new System.EventHandler(this.bMeseSuccessivo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // bVaiAdOggi
            // 
            this.bVaiAdOggi.Image = global::RationesCurare7.Properties.Resources.calendar_view_day;
            this.bVaiAdOggi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bVaiAdOggi.Name = "bVaiAdOggi";
            this.bVaiAdOggi.Size = new System.Drawing.Size(70, 37);
            this.bVaiAdOggi.Text = "Vai ad &oggi";
            this.bVaiAdOggi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bVaiAdOggi.Click += new System.EventHandler(this.bVaiAdOggi_Click);
            // 
            // bVaiA
            // 
            this.bVaiA.Image = global::RationesCurare7.Properties.Resources.calendar_view_day;
            this.bVaiA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bVaiA.Name = "bVaiA";
            this.bVaiA.Size = new System.Drawing.Size(48, 37);
            this.bVaiA.Text = "&Vai a ...";
            this.bVaiA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bVaiA.Click += new System.EventHandler(this.bVaiA_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.divider;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // cCalendar1
            // 
            this.cCalendar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cCalendar1.Location = new System.Drawing.Point(0, 40);
            this.cCalendar1.Name = "cCalendar1";
            this.cCalendar1.Size = new System.Drawing.Size(689, 416);
            this.cCalendar1.TabIndex = 1;
            // 
            // cCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cCalendar1);
            this.Controls.Add(this.panel1);
            this.Name = "cCalendario";
            this.Size = new System.Drawing.Size(689, 456);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public cCalendar cCalendar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bNuovo;
        private System.Windows.Forms.ToolStripButton bElimina;
        private System.Windows.Forms.ToolStripButton bMesePrecedente;
        private System.Windows.Forms.ToolStripButton bMeseSuccessivo;
        private System.Windows.Forms.ToolStripButton bVaiAdOggi;
        private System.Windows.Forms.ToolStripButton bVaiA;
        private System.Windows.Forms.ToolStripButton bModifica;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
