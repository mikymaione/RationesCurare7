using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cMovimentoInfo
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
            this.eCassa = new System.Windows.Forms.Label();
            this.eMacroArea = new System.Windows.Forms.Label();
            this.eDescrizione = new System.Windows.Forms.Label();
            this.eData = new System.Windows.Forms.Label();
            this.eSoldi = new System.Windows.Forms.Label();
            this.eNome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // eCassa
            // 
            this.eCassa.AutoSize = true;
            this.eCassa.Location = new System.Drawing.Point(31, 136);
            this.eCassa.Name = "eCassa";
            this.eCassa.Size = new System.Drawing.Size(42, 13);
            this.eCassa.TabIndex = 22;
            this.eCassa.Text = "Cassa: ";
            // 
            // eMacroArea
            // 
            this.eMacroArea.AutoSize = true;
            this.eMacroArea.Location = new System.Drawing.Point(6, 110);
            this.eMacroArea.Name = "eMacroArea";
            this.eMacroArea.Size = new System.Drawing.Size(67, 13);
            this.eMacroArea.TabIndex = 21;
            this.eMacroArea.Text = "Macro area: ";
            // 
            // eDescrizione
            // 
            this.eDescrizione.AutoSize = true;
            this.eDescrizione.Location = new System.Drawing.Point(5, 85);
            this.eDescrizione.Name = "eDescrizione";
            this.eDescrizione.Size = new System.Drawing.Size(68, 13);
            this.eDescrizione.TabIndex = 20;
            this.eDescrizione.Text = "Descrizione: ";
            // 
            // eData
            // 
            this.eData.AutoSize = true;
            this.eData.Location = new System.Drawing.Point(37, 60);
            this.eData.Name = "eData";
            this.eData.Size = new System.Drawing.Size(36, 13);
            this.eData.TabIndex = 18;
            this.eData.Text = "Data: ";
            // 
            // eSoldi
            // 
            this.eSoldi.AutoSize = true;
            this.eSoldi.Location = new System.Drawing.Point(37, 33);
            this.eSoldi.Name = "eSoldi";
            this.eSoldi.Size = new System.Drawing.Size(36, 13);
            this.eSoldi.TabIndex = 17;
            this.eSoldi.Text = "Soldi: ";
            // 
            // eNome
            // 
            this.eNome.AutoSize = true;
            this.eNome.Location = new System.Drawing.Point(32, 7);
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(41, 13);
            this.eNome.TabIndex = 16;
            this.eNome.Text = "Nome: ";
            // 
            // cMovimentoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.eCassa);
            this.Controls.Add(this.eMacroArea);
            this.Controls.Add(this.eDescrizione);
            this.Controls.Add(this.eData);
            this.Controls.Add(this.eSoldi);
            this.Controls.Add(this.eNome);
            this.Name = "cMovimentoInfo";
            this.Size = new System.Drawing.Size(358, 158);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Label eCassa;
        public Label eMacroArea;
        public Label eDescrizione;
        public Label eData;
        public Label eSoldi;
        public Label eNome;
    }
}
