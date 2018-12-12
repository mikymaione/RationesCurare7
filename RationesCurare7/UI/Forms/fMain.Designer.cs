namespace RationesCurare7.UI.Forms
{
    partial class fMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            System.Windows.Forms.TreeNode tnGestioneCasse = new System.Windows.Forms.TreeNode("Gestione casse", 13, 13);
            System.Windows.Forms.TreeNode tnCassaforte = new System.Windows.Forms.TreeNode("(F9) Cassaforte", 6, 6);
            System.Windows.Forms.TreeNode tnSalvadanaio = new System.Windows.Forms.TreeNode("(F12) Salvadanaio", 21, 21);
            System.Windows.Forms.TreeNode tnPortafoglio = new System.Windows.Forms.TreeNode("(F4) Portafoglio", 19, 19);
            System.Windows.Forms.TreeNode tnDare = new System.Windows.Forms.TreeNode("(F1) Dare", 9, 9);
            System.Windows.Forms.TreeNode tnAvere = new System.Windows.Forms.TreeNode("(F2) Avere", 2, 2);
            System.Windows.Forms.TreeNode tnSaldo = new System.Windows.Forms.TreeNode("(F5) Saldo", 20, 20);
            System.Windows.Forms.TreeNode tnMovimentiPeriodici = new System.Windows.Forms.TreeNode("Movimenti Periodici", 17, 17);
            System.Windows.Forms.TreeNode tnCasse = new System.Windows.Forms.TreeNode("(Alt +1) Casse", 5, 5, new System.Windows.Forms.TreeNode[] {
                tnGestioneCasse,
                tnCassaforte,
                tnSalvadanaio,
                tnPortafoglio,
                tnDare,
                tnAvere,
                tnSaldo,
                tnMovimentiPeriodici
            });
            System.Windows.Forms.TreeNode tnCerca = new System.Windows.Forms.TreeNode("(F3) Cerca", 11, 11);
            System.Windows.Forms.TreeNode tnTorta = new System.Windows.Forms.TreeNode("(F10) Torta", 18, 18);
            System.Windows.Forms.TreeNode tnGrafico = new System.Windows.Forms.TreeNode("(F11) Grafico", 12, 12);
            System.Windows.Forms.TreeNode tnCalendario = new System.Windows.Forms.TreeNode("(F6) Calendario", 4, 4);
            System.Windows.Forms.TreeNode tnCalcolatrice = new System.Windows.Forms.TreeNode("(F7) Calcolatrice", 3, 3);
            System.Windows.Forms.TreeNode tnMacroaree = new System.Windows.Forms.TreeNode("Macro aree", 14, 14);
            System.Windows.Forms.TreeNode tnControllaMovimentiPeriodici = new System.Windows.Forms.TreeNode("Controlla movimenti periodici", 8, 8);
            System.Windows.Forms.TreeNode tnControllaPromemoria = new System.Windows.Forms.TreeNode("Controlla promemoria", 7, 7);
            System.Windows.Forms.TreeNode tnStrumenti = new System.Windows.Forms.TreeNode("(Alt + 2) Strumenti", 23, 23, new System.Windows.Forms.TreeNode[] {
                tnCerca,
                tnTorta,
                tnGrafico,
                tnCalendario,
                tnCalcolatrice,
                tnMacroaree,
                tnControllaMovimentiPeriodici,
                tnControllaPromemoria
            });
            System.Windows.Forms.TreeNode tnOpzioniDB = new System.Windows.Forms.TreeNode("(F8) Opzioni DB", 10, 10);
            System.Windows.Forms.TreeNode tnCosaNePensi = new System.Windows.Forms.TreeNode("Cosa ne pensi?", 15, 15);
            System.Windows.Forms.TreeNode tnNovita = new System.Windows.Forms.TreeNode("Novità", 22, 22);
            System.Windows.Forms.TreeNode tnAbout = new System.Windows.Forms.TreeNode("About", 0, 0);
            System.Windows.Forms.TreeNode tnOpzioni = new System.Windows.Forms.TreeNode("(Alt + 3) Opzioni", 16, 16, new System.Windows.Forms.TreeNode[] {
                tnOpzioniDB,
                tnCosaNePensi,
                tnNovita,
                tnAbout
            });

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));

            this.pLeft = new System.Windows.Forms.Panel();
            this.cAlbero = new System.Windows.Forms.TreeView();
            this.ilAlbero = new System.Windows.Forms.ImageList(this.components);
            this.cUtente1 = new RationesCurare7.UI.Controlli.cUtente();
            this.tcSchede = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilTabs = new System.Windows.Forms.ImageList(this.components);
            this.pQuickInserimento = new System.Windows.Forms.Panel();
            this.bNuovoGiroconto = new System.Windows.Forms.Button();
            this.bNuovoMovimento = new System.Windows.Forms.Button();
            this.pLeft.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pQuickInserimento.SuspendLayout();
            this.SuspendLayout();
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.cAlbero);
            this.pLeft.Controls.Add(this.cUtente1);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 0);            
            this.pLeft.Size = new System.Drawing.Size(257, 661);
            this.pLeft.TabIndex = 5;
            // 
            // cAlbero
            // 
            this.cAlbero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cAlbero.FullRowSelect = true;
            this.cAlbero.HideSelection = false;
            this.cAlbero.ImageIndex = 0;
            this.cAlbero.ImageList = this.ilAlbero;
            this.cAlbero.Location = new System.Drawing.Point(0, 70);            
            tnGestioneCasse.ImageIndex = 13;
            tnGestioneCasse.Name = "nGestioneCasse";
            tnGestioneCasse.SelectedImageIndex = 13;
            tnGestioneCasse.Text = "Gestione casse";
            tnCassaforte.ImageIndex = 6;
            tnCassaforte.Name = "nCassaforte";
            tnCassaforte.SelectedImageIndex = 6;
            tnCassaforte.Tag = "Cassaforte";
            tnCassaforte.Text = "(F9) Cassaforte";
            tnSalvadanaio.ImageIndex = 21;
            tnSalvadanaio.Name = "nSalvadanaio";
            tnSalvadanaio.SelectedImageIndex = 21;
            tnSalvadanaio.Tag = "Salvadanaio";
            tnSalvadanaio.Text = "(F12) Salvadanaio";
            tnPortafoglio.ImageIndex = 19;
            tnPortafoglio.Name = "nPortafoglio";
            tnPortafoglio.SelectedImageIndex = 19;
            tnPortafoglio.Tag = "Portafogli";
            tnPortafoglio.Text = "(F4) Portafoglio";
            tnDare.ImageIndex = 9;
            tnDare.Name = "nDare";
            tnDare.SelectedImageIndex = 9;
            tnDare.Tag = "Dare";
            tnDare.Text = "(F1) Dare";
            tnAvere.ImageIndex = 2;
            tnAvere.Name = "nAvere";
            tnAvere.SelectedImageIndex = 2;
            tnAvere.Tag = "Avere";
            tnAvere.Text = "(F2) Avere";
            tnSaldo.ImageIndex = 20;
            tnSaldo.Name = "nSaldo";
            tnSaldo.SelectedImageIndex = 20;
            tnSaldo.Tag = "Saldo";
            tnSaldo.Text = "(F5) Saldo";
            tnMovimentiPeriodici.ImageIndex = 17;
            tnMovimentiPeriodici.Name = "nMovimentiPeriodici";
            tnMovimentiPeriodici.SelectedImageIndex = 17;
            tnMovimentiPeriodici.Text = "Movimenti Periodici";
            tnCasse.ImageIndex = 5;
            tnCasse.Name = "nCasse";            
            tnCasse.SelectedImageIndex = 5;
            tnCasse.Text = "(Alt +1) Casse";
            tnCerca.ImageIndex = 11;
            tnCerca.Name = "nCerca";
            tnCerca.SelectedImageIndex = 11;
            tnCerca.Text = "(F3) Cerca";
            tnTorta.ImageIndex = 18;
            tnTorta.Name = "nTorta";
            tnTorta.SelectedImageIndex = 18;
            tnTorta.Text = "(F10) Torta";
            tnGrafico.ImageIndex = 12;
            tnGrafico.Name = "nGrafico";
            tnGrafico.SelectedImageIndex = 12;
            tnGrafico.Text = "(F11) Grafico";
            tnCalendario.ImageIndex = 4;
            tnCalendario.Name = "nCalendario";
            tnCalendario.SelectedImageIndex = 4;
            tnCalendario.Text = "(F6) Calendario";
            tnCalcolatrice.ImageIndex = 3;
            tnCalcolatrice.Name = "nCalcolatrice";
            tnCalcolatrice.SelectedImageIndex = 3;
            tnCalcolatrice.Text = "(F7) Calcolatrice";
            tnMacroaree.ImageIndex = 14;
            tnMacroaree.Name = "nMacroAree";
            tnMacroaree.SelectedImageIndex = 14;
            tnMacroaree.Text = "Macro aree";
            tnControllaMovimentiPeriodici.ImageIndex = 8;
            tnControllaMovimentiPeriodici.Name = "nControllaMovimentiPeriodici";
            tnControllaMovimentiPeriodici.SelectedImageIndex = 8;
            tnControllaMovimentiPeriodici.Text = "Controlla movimenti periodici";
            tnControllaPromemoria.ImageIndex = 7;
            tnControllaPromemoria.Name = "nControllaPromemoria";
            tnControllaPromemoria.SelectedImageIndex = 7;
            tnControllaPromemoria.Text = "Controlla promemoria";
            tnStrumenti.ImageIndex = 23;
            tnStrumenti.Name = "nStrumenti";
            tnStrumenti.SelectedImageIndex = 23;
            tnStrumenti.Text = "(Alt + 2) Strumenti";
            tnOpzioniDB.ImageIndex = 10;
            tnOpzioniDB.Name = "nOpzioniDB";
            tnOpzioniDB.SelectedImageIndex = 10;
            tnOpzioniDB.Text = "(F8) Opzioni DB";
            tnCosaNePensi.ImageIndex = 15;
            tnCosaNePensi.Name = "nCosaNePensi";
            tnCosaNePensi.SelectedImageIndex = 15;
            tnCosaNePensi.Text = "Cosa ne pensi?";
            tnNovita.ImageIndex = 22;
            tnNovita.Name = "nNovita";
            tnNovita.SelectedImageIndex = 22;
            tnNovita.Text = "Novità";
            tnAbout.ImageIndex = 0;
            tnAbout.Name = "nAbout";
            tnAbout.SelectedImageIndex = 0;
            tnAbout.Text = "About";
            tnOpzioni.ImageIndex = 16;
            tnOpzioni.Name = "nOpzioni";
            tnOpzioni.SelectedImageIndex = 16;
            tnOpzioni.Text = "(Alt + 3) Opzioni";
            this.cAlbero.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            tnCasse,
            tnStrumenti,
            tnOpzioni});
            this.cAlbero.SelectedImageIndex = 0;
            this.cAlbero.Size = new System.Drawing.Size(257, 591);
            this.cAlbero.TabIndex = 7;
            this.cAlbero.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.advTree1_NodeMouseClick);
            // 
            // ilAlbero
            // 
            this.ilAlbero.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilAlbero.ImageStream")));
            this.ilAlbero.TransparentColor = System.Drawing.Color.Transparent;
            this.ilAlbero.Images.SetKeyName(0, "about32.png");
            this.ilAlbero.Images.SetKeyName(1, "Aggiornamenti32.png");
            this.ilAlbero.Images.SetKeyName(2, "Avere32.png");
            this.ilAlbero.Images.SetKeyName(3, "calcolatrice32.png");
            this.ilAlbero.Images.SetKeyName(4, "calendario32.png");
            this.ilAlbero.Images.SetKeyName(5, "cash-register-icon32.png");
            this.ilAlbero.Images.SetKeyName(6, "cassaforte32.png");
            this.ilAlbero.Images.SetKeyName(7, "controllaAlert32.png");
            this.ilAlbero.Images.SetKeyName(8, "controllaperiodici32.png");
            this.ilAlbero.Images.SetKeyName(9, "dare32.png");
            this.ilAlbero.Images.SetKeyName(10, "db32.png");
            this.ilAlbero.Images.SetKeyName(11, "find321.png");
            this.ilAlbero.Images.SetKeyName(12, "grafico32.png");
            this.ilAlbero.Images.SetKeyName(13, "ingranaggio32.png");
            this.ilAlbero.Images.SetKeyName(14, "MacroAree.png");
            this.ilAlbero.Images.SetKeyName(15, "mail32.png");
            this.ilAlbero.Images.SetKeyName(16, "opzioni.png");
            this.ilAlbero.Images.SetKeyName(17, "perdioci32.png");
            this.ilAlbero.Images.SetKeyName(18, "PieChart.png");
            this.ilAlbero.Images.SetKeyName(19, "portafoglio32.png");
            this.ilAlbero.Images.SetKeyName(20, "saldo32.png");
            this.ilAlbero.Images.SetKeyName(21, "salvadanaio32.png");
            this.ilAlbero.Images.SetKeyName(22, "star32.png");
            this.ilAlbero.Images.SetKeyName(23, "Strumenti32.png");
            this.ilAlbero.Images.SetKeyName(24, "sync32.png");
            // 
            // cUtente1
            // 
            this.cUtente1.BackColor = System.Drawing.Color.White;
            this.cUtente1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cUtente1.Location = new System.Drawing.Point(0, 0);
            this.cUtente1.Name = "cUtente1";
            this.cUtente1.Size = new System.Drawing.Size(257, 70);
            this.cUtente1.TabIndex = 6;
            // 
            // tcSchede
            // 
            this.tcSchede.ContextMenuStrip = this.contextMenuStrip1;
            this.tcSchede.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSchede.ImageList = this.ilTabs;
            this.tcSchede.Location = new System.Drawing.Point(257, 0);
            this.tcSchede.Name = "tcSchede";
            this.tcSchede.SelectedIndex = 0;
            this.tcSchede.Size = new System.Drawing.Size(727, 624);
            this.tcSchede.TabIndex = 6;            
            this.tcSchede.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tcSchede_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chiudiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 26);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.Image = global::RationesCurare7.Properties.Resources.application_delete;
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi scheda corrente";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // ilTabs
            // 
            this.ilTabs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ilTabs.ImageSize = new System.Drawing.Size(16, 16);
            this.ilTabs.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pQuickInserimento
            // 
            this.pQuickInserimento.Controls.Add(this.bNuovoGiroconto);
            this.pQuickInserimento.Controls.Add(this.bNuovoMovimento);
            this.pQuickInserimento.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pQuickInserimento.Location = new System.Drawing.Point(257, 624);
            this.pQuickInserimento.Name = "pQuickInserimento";
            this.pQuickInserimento.Size = new System.Drawing.Size(727, 37);
            this.pQuickInserimento.TabIndex = 7;
            // 
            // bNuovoGiroconto
            // 
            this.bNuovoGiroconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bNuovoGiroconto.Image = global::RationesCurare7.Properties.Resources.giroconto;
            this.bNuovoGiroconto.Location = new System.Drawing.Point(595, 6);
            this.bNuovoGiroconto.Name = "bNuovoGiroconto";
            this.bNuovoGiroconto.Size = new System.Drawing.Size(120, 25);
            this.bNuovoGiroconto.TabIndex = 1;
            this.bNuovoGiroconto.Text = "Nuovo &giroconto";
            this.bNuovoGiroconto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bNuovoGiroconto.UseVisualStyleBackColor = true;
            this.bNuovoGiroconto.Click += new System.EventHandler(this.bNuovoGiroconto_Click);
            // 
            // bNuovoMovimento
            // 
            this.bNuovoMovimento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bNuovoMovimento.Image = global::RationesCurare7.Properties.Resources.Add32;
            this.bNuovoMovimento.Location = new System.Drawing.Point(469, 6);
            this.bNuovoMovimento.Name = "bNuovoMovimento";
            this.bNuovoMovimento.Size = new System.Drawing.Size(120, 25);
            this.bNuovoMovimento.TabIndex = 0;
            this.bNuovoMovimento.Text = "Nuovo &movimento";
            this.bNuovoMovimento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bNuovoMovimento.UseVisualStyleBackColor = true;
            this.bNuovoMovimento.Click += new System.EventHandler(this.bNuovoMovimento_Click);
            // 
            // fMain
            // 
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tcSchede);
            this.Controls.Add(this.pQuickInserimento);
            this.Controls.Add(this.pLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RationesCurare7 - [MAIONE MIKY]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fMain_KeyDown);
            this.pLeft.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pQuickInserimento.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pLeft;
        private UI.Controlli.cUtente cUtente1;
        public System.Windows.Forms.TabControl tcSchede;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chiudiToolStripMenuItem;
        private System.Windows.Forms.ImageList ilTabs;
        private System.Windows.Forms.TreeView cAlbero;
        private System.Windows.Forms.ImageList ilAlbero;
        private System.Windows.Forms.Panel pQuickInserimento;
        private System.Windows.Forms.Button bNuovoGiroconto;
        private System.Windows.Forms.Button bNuovoMovimento;
    }
}