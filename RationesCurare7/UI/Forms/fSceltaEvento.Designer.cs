using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    partial class fSceltaEvento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSceltaEvento));
            this.label1 = new System.Windows.Forms.Label();
            this.cbEventi = new System.Windows.Forms.ComboBox();
            this.bOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Evento che si vuole modificare";
            // 
            // cbEventi
            // 
            this.cbEventi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEventi.FormattingEnabled = true;
            this.cbEventi.Location = new System.Drawing.Point(15, 25);
            this.cbEventi.Name = "cbEventi";
            this.cbEventi.Size = new System.Drawing.Size(257, 21);
            this.cbEventi.TabIndex = 1;
            // 
            // bOK
            // 
            this.bOK.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOK.Location = new System.Drawing.Point(15, 52);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 25);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "Ok";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // fSceltaEvento
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbEventi);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fSceltaEvento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista eventi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ComboBox cbEventi;
        private Button bOK;
    }
}