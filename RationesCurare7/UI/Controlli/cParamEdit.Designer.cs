using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cParamEdit
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
            this.eValore = new System.Windows.Forms.TextBox();
            this.lNome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // eValore
            // 
            this.eValore.Dock = System.Windows.Forms.DockStyle.Top;
            this.eValore.Location = new System.Drawing.Point(0, 13);
            this.eValore.Name = "eValore";
            this.eValore.Size = new System.Drawing.Size(200, 20);
            this.eValore.TabIndex = 0;
            // 
            // lNome
            // 
            this.lNome.AutoSize = true;
            this.lNome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lNome.Location = new System.Drawing.Point(0, 0);
            this.lNome.Name = "lNome";
            this.lNome.Size = new System.Drawing.Size(35, 13);
            this.lNome.TabIndex = 1;
            this.lNome.Text = "label1";
            // 
            // cParamEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eValore);
            this.Controls.Add(this.lNome);
            this.Name = "cParamEdit";
            this.Size = new System.Drawing.Size(200, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox eValore;
        private Label lNome;
    }
}
