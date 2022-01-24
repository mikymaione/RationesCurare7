using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cMathEdit
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
            this.eMathTextBox = new System.Windows.Forms.TextBox();
            this.bOpenCalc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.eMathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eMathTextBox.Location = new System.Drawing.Point(0, 2);
            this.eMathTextBox.Name = "textBox1";
            this.eMathTextBox.ShortcutsEnabled = false;
            this.eMathTextBox.Size = new System.Drawing.Size(100, 20);
            this.eMathTextBox.TabIndex = 2;
            this.eMathTextBox.TextChanged += new System.EventHandler(this.eMathTextBox_TextChanged);
            this.eMathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eMathTextBox_KeyDown);
            this.eMathTextBox.Leave += new System.EventHandler(this.eMathTextBox_Leave);
            // 
            // button1
            // 
            this.bOpenCalc.Dock = System.Windows.Forms.DockStyle.Right;
            this.bOpenCalc.Image = global::RationesCurare7.Properties.Resources.calculator;
            this.bOpenCalc.Location = new System.Drawing.Point(100, 0);
            this.bOpenCalc.Name = "button1";
            this.bOpenCalc.Size = new System.Drawing.Size(22, 24);
            this.bOpenCalc.TabIndex = 3;
            this.bOpenCalc.UseVisualStyleBackColor = true;
            this.bOpenCalc.Click += new System.EventHandler(this.bOpenCalc_Click);
            // 
            // cMathEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eMathTextBox);
            this.Controls.Add(this.bOpenCalc);
            this.Name = "cMathEdit";
            this.Size = new System.Drawing.Size(122, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox eMathTextBox;
        private Button bOpenCalc;
    }
}
