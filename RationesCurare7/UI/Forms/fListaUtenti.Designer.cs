namespace RationesCurare7.UI.Forms
{
    partial class fListaUtenti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fListaUtenti));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bNuovo = new System.Windows.Forms.ToolStripButton();
            this.bNuovoDaWeb = new System.Windows.Forms.ToolStripButton();
            this.bModifica = new System.Windows.Forms.ToolStripButton();
            this.bNascondi = new System.Windows.Forms.ToolStripButton();
            this.bCerca = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bOk = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNuovo,
            this.bNuovoDaWeb,
            this.bModifica,
            this.bNascondi,
            this.bCerca});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(465, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bNuovo
            // 
            this.bNuovo.Image = global::RationesCurare7.Properties.Resources.Add32;
            this.bNuovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNuovo.Name = "bNuovo";
            this.bNuovo.Size = new System.Drawing.Size(70, 35);
            this.bNuovo.Text = "(F1) Nuovo";
            this.bNuovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNuovo.Click += new System.EventHandler(this.bNuovo_Click);
            // 
            // bNuovoDaWeb
            // 
            this.bNuovoDaWeb.Image = global::RationesCurare7.Properties.Resources.world_add;
            this.bNuovoDaWeb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNuovoDaWeb.Name = "bNuovoDaWeb";
            this.bNuovoDaWeb.Size = new System.Drawing.Size(113, 35);
            this.bNuovoDaWeb.Text = "(F7) Nuovo da Web";
            this.bNuovoDaWeb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNuovoDaWeb.Click += new System.EventHandler(this.bNuovoDaWeb_Click);
            // 
            // bModifica
            // 
            this.bModifica.Image = global::RationesCurare7.Properties.Resources.edit32;
            this.bModifica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bModifica.Name = "bModifica";
            this.bModifica.Size = new System.Drawing.Size(81, 35);
            this.bModifica.Text = "(F2) Modifica";
            this.bModifica.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bModifica.Click += new System.EventHandler(this.bModifica_Click);
            // 
            // bNascondi
            // 
            this.bNascondi.Image = global::RationesCurare7.Properties.Resources.delete32;
            this.bNascondi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNascondi.Name = "bNascondi";
            this.bNascondi.Size = new System.Drawing.Size(84, 35);
            this.bNascondi.Text = "(F4) Nascondi";
            this.bNascondi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNascondi.Click += new System.EventHandler(this.bNascondi_Click);
            // 
            // bCerca
            // 
            this.bCerca.Image = global::RationesCurare7.Properties.Resources.find32;
            this.bCerca.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCerca.Name = "bCerca";
            this.bCerca.Size = new System.Drawing.Size(64, 35);
            this.bCerca.Text = "(F3) Cerca";
            this.bCerca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bCerca.Click += new System.EventHandler(this.bCerca_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 37);
            this.panel1.TabIndex = 2;
            // 
            // bOk
            // 
            this.bOk.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOk.Location = new System.Drawing.Point(12, 6);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 25);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "Entra";
            this.bOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 38);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(465, 367);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseWheel);
            // 
            // fListaUtenti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 442);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fListaUtenti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RationesCurare7 - Utenti";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fListaUtenti_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fListaUtenti_KeyUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.fListaUtenti_MouseWheel);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }        

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bNuovo;
        private System.Windows.Forms.ToolStripButton bModifica;
        private System.Windows.Forms.ToolStripButton bNascondi;
        private System.Windows.Forms.ToolStripButton bCerca;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Panel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripButton bNuovoDaWeb;
    }
}