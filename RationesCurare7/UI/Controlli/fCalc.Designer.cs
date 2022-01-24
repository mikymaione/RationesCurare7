using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class fCalc
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.bSott = new System.Windows.Forms.Button();
            this.bEurom = new System.Windows.Forms.Button();
            this.bDiv = new System.Windows.Forms.Button();
            this.bMul = new System.Windows.Forms.Button();
            this.bEq = new System.Windows.Forms.Button();
            this.textBox1 = new RationesCurare7.UI.Controlli.cMathEdit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 225);
            this.listBox1.TabIndex = 1;
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(190, 12);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(25, 25);
            this.bAdd.TabIndex = 2;
            this.bAdd.Text = "+";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bSott
            // 
            this.bSott.Location = new System.Drawing.Point(190, 57);
            this.bSott.Name = "bSott";
            this.bSott.Size = new System.Drawing.Size(25, 25);
            this.bSott.TabIndex = 3;
            this.bSott.Text = "-";
            this.bSott.UseVisualStyleBackColor = true;
            this.bSott.Click += new System.EventHandler(this.bSott_Click);
            // 
            // bEurom
            // 
            this.bEurom.Location = new System.Drawing.Point(190, 102);
            this.bEurom.Name = "bEurom";
            this.bEurom.Size = new System.Drawing.Size(25, 25);
            this.bEurom.TabIndex = 4;
            this.bEurom.Text = "-€";
            this.bEurom.UseVisualStyleBackColor = true;
            this.bEurom.Click += new System.EventHandler(this.bEurom_Click);
            // 
            // bDiv
            // 
            this.bDiv.Location = new System.Drawing.Point(190, 147);
            this.bDiv.Name = "bDiv";
            this.bDiv.Size = new System.Drawing.Size(25, 25);
            this.bDiv.TabIndex = 5;
            this.bDiv.Text = "/";
            this.bDiv.UseVisualStyleBackColor = true;
            this.bDiv.Click += new System.EventHandler(this.bDiv_Click);
            // 
            // bMul
            // 
            this.bMul.Location = new System.Drawing.Point(190, 192);
            this.bMul.Name = "bMul";
            this.bMul.Size = new System.Drawing.Size(25, 25);
            this.bMul.TabIndex = 6;
            this.bMul.Text = "*";
            this.bMul.UseVisualStyleBackColor = true;
            this.bMul.Click += new System.EventHandler(this.bMul_Click);
            // 
            // bEq
            // 
            this.bEq.Location = new System.Drawing.Point(190, 238);
            this.bEq.Name = "bEq";
            this.bEq.Size = new System.Drawing.Size(25, 25);
            this.bEq.TabIndex = 7;
            this.bEq.Text = "=";
            this.bEq.UseVisualStyleBackColor = true;
            this.bEq.Click += new System.EventHandler(this.bEq_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ShowCalcButton = false;
            this.textBox1.Size = new System.Drawing.Size(172, 22);
            this.textBox1.TabIndex = 0;            
            // 
            // fCalc
            // 
            this.ClientSize = new System.Drawing.Size(227, 276);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bEq);
            this.Controls.Add(this.bMul);
            this.Controls.Add(this.bDiv);
            this.Controls.Add(this.bEurom);
            this.Controls.Add(this.bSott);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fCalc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Calcolatrice";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fCalc_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBox1;
        private Button bAdd;
        private Button bSott;
        private Button bEurom;
        private Button bDiv;
        private Button bMul;
        private Button bEq;
        private cMathEdit textBox1;
    }
}