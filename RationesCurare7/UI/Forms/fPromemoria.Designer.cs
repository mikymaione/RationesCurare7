using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    partial class fPromemoria
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPromemoria));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lOggi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lDomani = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lTodoDomani = new System.Windows.Forms.TextBox();
            this.lTodoOggi = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::RationesCurare7.Properties.Resources.gradient1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lOggi);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 42);
            this.panel1.TabIndex = 0;
            // 
            // lOggi
            // 
            this.lOggi.BackColor = System.Drawing.Color.Transparent;
            this.lOggi.Location = new System.Drawing.Point(93, 26);
            this.lOggi.Name = "lOggi";
            this.lOggi.Size = new System.Drawing.Size(122, 13);
            this.lOggi.TabIndex = 2;
            this.lOggi.Text = "Oggi";
            this.lOggi.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Oggi";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::RationesCurare7.Properties.Resources.clock_red;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImage = global::RationesCurare7.Properties.Resources.gradient1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lDomani);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 217);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(223, 42);
            this.panel2.TabIndex = 2;
            // 
            // lDomani
            // 
            this.lDomani.BackColor = System.Drawing.Color.Transparent;
            this.lDomani.Location = new System.Drawing.Point(93, 26);
            this.lDomani.Name = "lDomani";
            this.lDomani.Size = new System.Drawing.Size(119, 13);
            this.lDomani.TabIndex = 2;
            this.lDomani.Text = "Oggi";
            this.lDomani.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Domani";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::RationesCurare7.Properties.Resources.clock;
            this.pictureBox2.Location = new System.Drawing.Point(6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lTodoDomani
            // 
            this.lTodoDomani.BackColor = System.Drawing.SystemColors.Control;
            this.lTodoDomani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTodoDomani.Location = new System.Drawing.Point(0, 259);
            this.lTodoDomani.Multiline = true;
            this.lTodoDomani.Name = "lTodoDomani";
            this.lTodoDomani.ReadOnly = true;
            this.lTodoDomani.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lTodoDomani.Size = new System.Drawing.Size(223, 175);
            this.lTodoDomani.TabIndex = 3;
            this.lTodoDomani.TabStop = false;
            // 
            // lTodoOggi
            // 
            this.lTodoOggi.BackColor = System.Drawing.SystemColors.Control;
            this.lTodoOggi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lTodoOggi.Location = new System.Drawing.Point(0, 42);
            this.lTodoOggi.Multiline = true;
            this.lTodoOggi.Name = "lTodoOggi";
            this.lTodoOggi.ReadOnly = true;
            this.lTodoOggi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lTodoOggi.Size = new System.Drawing.Size(223, 175);
            this.lTodoOggi.TabIndex = 1;
            this.lTodoOggi.TabStop = false;
            // 
            // fPromemoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 434);
            this.Controls.Add(this.lTodoDomani);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lTodoOggi);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPromemoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promemoria";            
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label lOggi;
        private Label label1;
        private Panel panel2;
        private Label lDomani;
        private Label label3;
        private PictureBox pictureBox2;
        private TextBox lTodoDomani;
        private TextBox lTodoOggi;
    }
}