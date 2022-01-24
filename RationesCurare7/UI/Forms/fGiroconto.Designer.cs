using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    partial class fGiroconto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fGiroconto));
            this.cbCasse = new System.Windows.Forms.ComboBox();
            this.lTitolo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbCasse
            // 
            this.cbCasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCasse.FormattingEnabled = true;
            this.cbCasse.Location = new System.Drawing.Point(15, 25);
            this.cbCasse.Name = "cbCasse";
            this.cbCasse.Size = new System.Drawing.Size(185, 21);
            this.cbCasse.TabIndex = 0;
            // 
            // lTitolo
            // 
            this.lTitolo.AutoSize = true;
            this.lTitolo.Location = new System.Drawing.Point(12, 9);
            this.lTitolo.Name = "lTitolo";
            this.lTitolo.Size = new System.Drawing.Size(188, 13);
            this.lTitolo.TabIndex = 1;
            this.lTitolo.Text = "Scegli dove vuoi effettuare il giroconto";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::RationesCurare7.Properties.Resources.accept;
            this.button1.Location = new System.Drawing.Point(133, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fGiroconto
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 87);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lTitolo);
            this.Controls.Add(this.cbCasse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fGiroconto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selezione cassa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cbCasse;
        private Label lTitolo;
        private Button button1;
    }
}