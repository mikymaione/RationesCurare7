namespace RationesCurare7.UI.Controlli
{
    partial class cMacroAree
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
            this.pArea = new System.Windows.Forms.FlowLayoutPanel();
            this.gNoCat = new RationesCurare7.UI.Controlli.cGroupList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bNuovo = new System.Windows.Forms.ToolStripButton();
            this.bElimina = new System.Windows.Forms.ToolStripButton();
            this.bSalva = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pArea
            // 
            this.pArea.AutoScroll = true;
            this.pArea.BackColor = System.Drawing.Color.White;
            this.pArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pArea.Location = new System.Drawing.Point(0, 40);
            this.pArea.Name = "pArea";
            this.pArea.Size = new System.Drawing.Size(571, 410);
            this.pArea.TabIndex = 1;
            // 
            // gNoCat
            // 
            this.gNoCat.BackColor = System.Drawing.Color.White;
            this.gNoCat.Dock = System.Windows.Forms.DockStyle.Right;
            this.gNoCat.Location = new System.Drawing.Point(571, 0);
            this.gNoCat.Name = "gNoCat";
            this.gNoCat.Selected = false;
            this.gNoCat.Size = new System.Drawing.Size(200, 450);
            this.gNoCat.TabIndex = 3;
            this.gNoCat.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 40);
            this.panel1.TabIndex = 4;
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNuovo,
            this.bElimina,
            this.bSalva});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(571, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bNuovo
            // 
            this.bNuovo.Image = global::RationesCurare7.Properties.Resources.Add32;
            this.bNuovo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bNuovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNuovo.Name = "bNuovo";
            this.bNuovo.Size = new System.Drawing.Size(46, 37);
            this.bNuovo.Text = "&Nuova";
            this.bNuovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNuovo.Click += new System.EventHandler(this.bNuovo_Click);
            // 
            // bElimina
            // 
            this.bElimina.Image = global::RationesCurare7.Properties.Resources.delete32;
            this.bElimina.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bElimina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bElimina.Name = "bElimina";
            this.bElimina.Size = new System.Drawing.Size(50, 37);
            this.bElimina.Text = "&Elimina";
            this.bElimina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bElimina.Click += new System.EventHandler(this.bElimina_Click);
            // 
            // bSalva
            // 
            this.bSalva.Image = global::RationesCurare7.Properties.Resources.disk;
            this.bSalva.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bSalva.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSalva.Name = "bSalva";
            this.bSalva.Size = new System.Drawing.Size(38, 37);
            this.bSalva.Text = "&Salva";
            this.bSalva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bSalva.Click += new System.EventHandler(this.bSalva_Click);
            // 
            // cMacroAree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pArea);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gNoCat);
            this.Name = "cMacroAree";
            this.Size = new System.Drawing.Size(771, 450);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pArea;
        private cGroupList gNoCat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bNuovo;
        private System.Windows.Forms.ToolStripButton bElimina;
        private System.Windows.Forms.ToolStripButton bSalva;
        private System.Windows.Forms.PictureBox pictureBox1;        
    }
}
