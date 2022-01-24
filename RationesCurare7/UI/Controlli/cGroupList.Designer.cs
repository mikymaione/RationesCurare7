using System.ComponentModel;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    partial class cGroupList
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
            this.components = new System.ComponentModel.Container();
            this.G = new System.Windows.Forms.GroupBox();
            this.Lista = new RationesCurare7.UI.Controlli.DragDropListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bDelFiltro = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.eFiltro = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.eNome = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.G.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // G
            // 
            this.G.Controls.Add(this.Lista);
            this.G.Controls.Add(this.panel2);
            this.G.Controls.Add(this.bOK);
            this.G.Controls.Add(this.eNome);
            this.G.Controls.Add(this.panel1);
            this.G.Dock = System.Windows.Forms.DockStyle.Fill;
            this.G.Location = new System.Drawing.Point(0, 0);
            this.G.Name = "G";
            this.G.Size = new System.Drawing.Size(478, 262);
            this.G.TabIndex = 0;
            this.G.TabStop = false;
            this.G.Text = "groupBox1";
            this.G.Click += new System.EventHandler(this.G_Click);
            // 
            // Lista
            // 
            this.Lista.AllowDrop = true;
            this.Lista.BackColor = System.Drawing.Color.White;
            this.Lista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lista.FormattingEnabled = true;
            this.Lista.Location = new System.Drawing.Point(3, 22);
            this.Lista.Name = "Lista";
            this.Lista.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Lista.Size = new System.Drawing.Size(472, 213);
            this.Lista.Sorted = true;
            this.Lista.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bDelFiltro);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.eFiltro);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 24);
            this.panel2.TabIndex = 4;
            // 
            // bDelFiltro
            // 
            this.bDelFiltro.Image = global::RationesCurare7.Properties.Resources.giroconto;
            this.bDelFiltro.Location = new System.Drawing.Point(147, 1);
            this.bDelFiltro.Name = "bDelFiltro";
            this.bDelFiltro.Size = new System.Drawing.Size(22, 22);
            this.bDelFiltro.TabIndex = 2;
            this.toolTip1.SetToolTip(this.bDelFiltro, "Ricarica la lista");
            this.bDelFiltro.UseVisualStyleBackColor = true;
            this.bDelFiltro.Click += new System.EventHandler(this.bDelFiltro_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtra:";
            // 
            // eFiltro
            // 
            this.eFiltro.Location = new System.Drawing.Point(41, 2);
            this.eFiltro.Name = "eFiltro";
            this.eFiltro.Size = new System.Drawing.Size(100, 20);
            this.eFiltro.TabIndex = 0;
            this.eFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.eFiltro_KeyUp);
            // 
            // bOK
            // 
            this.bOK.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOK.Location = new System.Drawing.Point(131, 0);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(22, 22);
            this.bOK.TabIndex = 3;
            this.toolTip1.SetToolTip(this.bOK, "Usa questo nome");
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Visible = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // eNome
            // 
            this.eNome.Location = new System.Drawing.Point(9, 1);
            this.eNome.MaxLength = 250;
            this.eNome.Name = "eNome";
            this.eNome.Size = new System.Drawing.Size(119, 20);
            this.eNome.TabIndex = 1;
            this.eNome.Visible = false;
            this.eNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eNome_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 6);
            this.panel1.TabIndex = 2;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Informazione";
            // 
            // cGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.G);
            this.Name = "cGroupList";
            this.Size = new System.Drawing.Size(478, 262);
            this.G.ResumeLayout(false);
            this.G.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }        

        #endregion

        private GroupBox G;
        private TextBox eNome;
        private Panel panel1;
        private Button bOK;
        private Panel panel2;
        private Button bDelFiltro;
        private Label label1;
        private TextBox eFiltro;
        private DragDropListBox Lista;
        private ToolTip toolTip1;
    }
}
