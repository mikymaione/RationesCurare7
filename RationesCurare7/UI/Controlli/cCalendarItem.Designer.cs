namespace RationesCurare7.UI.Controlli
{
    partial class cCalendarItem
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
            this.lTesto = new System.Windows.Forms.Label();
            this.lGiorno = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lTesto);
            this.panel1.Controls.Add(this.lGiorno);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 107);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.Item_Click);
            // 
            // lTesto
            // 
            this.lTesto.BackColor = System.Drawing.Color.White;
            this.lTesto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTesto.ForeColor = System.Drawing.Color.Black;
            this.lTesto.Location = new System.Drawing.Point(0, 0);
            this.lTesto.Name = "lTesto";
            this.lTesto.Size = new System.Drawing.Size(207, 92);
            this.lTesto.TabIndex = 3;
            this.lTesto.Click += new System.EventHandler(this.Item_Click);
            // 
            // lGiorno
            // 
            this.lGiorno.BackColor = System.Drawing.Color.White;
            this.lGiorno.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lGiorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lGiorno.ForeColor = System.Drawing.Color.Black;
            this.lGiorno.Location = new System.Drawing.Point(0, 92);
            this.lGiorno.Name = "lGiorno";
            this.lGiorno.Size = new System.Drawing.Size(207, 15);
            this.lGiorno.TabIndex = 2;
            this.lGiorno.Text = "label1";
            this.lGiorno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lGiorno.Click += new System.EventHandler(this.Item_Click);
            // 
            // cCalendarItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "cCalendarItem";
            this.Size = new System.Drawing.Size(213, 113);
            this.Click += new System.EventHandler(this.Item_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lTesto;
        public System.Windows.Forms.Label lGiorno;

    }
}