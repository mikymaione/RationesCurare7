using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    partial class fSceltaTipoModificaCalendario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSceltaTipoModificaCalendario));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSingolo = new System.Windows.Forms.RadioButton();
            this.rbSerie = new System.Windows.Forms.RadioButton();
            this.bOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSerie);
            this.groupBox1.Controls.Add(this.rbSingolo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cosa vuoi modificare?";
            // 
            // rbSingolo
            // 
            this.rbSingolo.AutoSize = true;
            this.rbSingolo.Checked = true;
            this.rbSingolo.Location = new System.Drawing.Point(11, 22);
            this.rbSingolo.Name = "rbSingolo";
            this.rbSingolo.Size = new System.Drawing.Size(127, 17);
            this.rbSingolo.TabIndex = 0;
            this.rbSingolo.TabStop = true;
            this.rbSingolo.Text = "Solo questo elemento";
            this.rbSingolo.UseVisualStyleBackColor = true;
            // 
            // rbSerie
            // 
            this.rbSerie.AutoSize = true;
            this.rbSerie.Location = new System.Drawing.Point(144, 22);
            this.rbSerie.Name = "rbSerie";
            this.rbSerie.Size = new System.Drawing.Size(86, 17);
            this.rbSerie.TabIndex = 1;
            this.rbSerie.Text = "Tutta la serie";
            this.rbSerie.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOK.Location = new System.Drawing.Point(12, 68);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 25);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "Ok";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // fSceltaTipoModificaCalendario
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 101);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fSceltaTipoModificaCalendario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scelta modalità";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton rbSerie;
        private RadioButton rbSingolo;
        private Button bOK;
    }
}