﻿/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.GB;
using RationesCurare7.Properties;
using RationesCurare7.UI.Controlli;
using cCalendario = RationesCurare7.UI.Controlli.cCalendario;
using cCasse = RationesCurare7.DB.DataWrapper.cCasse;

namespace RationesCurare7.UI.Forms
{
    public partial class fMain : Form
    {
        public enum eActions
        {
            NuovoMovimento,
            NuovoGiroconto,
            MacroAree,
            Cerca,
            Grafico,
            GraficoSpline,
            Torta,
            Calendario,
            Calcolatrice,
            OpzioniDB,
            About,
            Novita,
            Casse,
            CosaNePensi,
            MovimentiPeriodici,
            ControllaPeriodici,
            ControllaPeriodiciSoloAlert,
            ControllaPromemoria
        }

        private TreeNode nCasse;
        private List<cCasse> CasseAggiuntive;
        private TreeNode LastSelectedNode;

        public fMain()
        {
            InitializeComponent();

            if (!cGB.DesignTime)
            {
                Text = "RationesCurare7 - " + cGB.CopyrightHolder;

                Init();
                Action(eActions.ControllaPromemoria);
                Action(eActions.ControllaPeriodiciSoloAlert);
                Action(eActions.ControllaPeriodici);
            }
        }

        private void Init()
        {
            foreach (TreeNode item in cAlbero.Nodes)
                if (item.Name.Equals("nCasse"))
                {
                    nCasse = item;
                    nCasse.Expand();
                    break;
                }

            AggiungiCasseExtra();
            LoadAllCash();
        }

        private bool FirstTimeResize = true;
        private void fMain_Resize(object sender, EventArgs e)
        {
            if (FirstTimeResize)
            {
                if (Height > 966)
                {
                    cAlbero.ExpandAll();
                }
                else
                {
                    cAlbero.Select();
                    cAlbero.SelectedNode = cAlbero.Nodes[0];
                    cAlbero.Nodes[0].Expand();
                }
            }

            FirstTimeResize = false;
        }

        private Color SaldoToColor(double s)
        {
            var c = Color.Black;
            var R = Math.Round(s, 2);

            if (R != 0)
            {
                if (s > 0)
                    c = Color.DarkGreen;
                else if (s < 0)
                    c = Color.Red;
            }

            return c;
        }

        public void LoadAllCash()
        {
            var m = new cMovimenti();

            cUtente1.lSaldo.Text = cGB.DoubleToMoneyStringValuta(m.Saldo("Saldo"));
            cUtente1.lSaldo.ForeColor = SaldoToColor(m.Saldo("Saldo"));

            if (CasseAggiuntive != null)
                if (CasseAggiuntive.Count > 0)
                    foreach (var caz in CasseAggiuntive)
                        foreach (TreeNode item in nCasse.Nodes)
                            if (item.Name.Equals(caz.nome))
                            {
                                var saldC = m.Saldo(caz.nome);

                                item.Text = caz.nome + ": " + cGB.DoubleToMoneyStringValuta(saldC);
                                item.ForeColor = SaldoToColor(saldC);
                            }

            cAlbero.Select();
        }

        public void SvuotaAlberoCasse()
        {
            CasseAggiuntive = null;
            var NotDelete = new List<string>(new[] { "nGestioneCasse", "nMovimentiPeriodici" });

            while (cAlbero.Nodes[0].Nodes.Count > NotDelete.Count)
                for (var i = 0; i < cAlbero.Nodes[0].Nodes.Count; i++)
                    if (!NotDelete.Contains(cAlbero.Nodes[0].Nodes[i].Name))
                        cAlbero.Nodes[0].Nodes[i].Remove();
        }

        public void AggiungiCasseExtra()
        {
            var cas = new cCasse();
            CasseAggiuntive = cas.CasseAggiuntive(false);

            if (CasseAggiuntive != null)
            {
                if (CasseAggiuntive.Count > 0)
                    foreach (var caz in CasseAggiuntive)
                        if (!nCasse.Nodes.ContainsKey(caz.nome))
                        {
                            if (!ilAlbero.Images.ContainsKey(caz.nome))
                                ilAlbero.Images.Add(caz.nome, new Bitmap(ImageFromByte(caz.imgName)));

                            var n = new TreeNode(caz.nome)
                            {
                                Name = caz.nome,
                                Text = caz.nome,
                                Tag = caz.nome,
                                ImageKey = caz.nome,
                                SelectedImageKey = caz.nome
                            };

                            nCasse.Nodes.Add(n);
                        }
            }
        }

        private Image ImageFromByte(byte[] img)
        {
            try
            {
                return Image.FromStream(new MemoryStream(img));
            }
            catch
            {
                //no img
                return Resources.saldo32;
            }
        }

        private void ShowCash(TreeNode button)
        {
            ShowCash(button.Tag.ToString(), ilAlbero.Images[button.ImageIndex]);
        }

        private Bitmap ResizeImageForTab(Image i)
        {
            try
            {
                return new Bitmap(i, 16, 16);
            }
            catch
            {
                //cannot convert
                return null;
            }
        }

        private void AddNewTabGrafico(string NomeClasse, string Title, Image i)
        {
#if __MonoCS__
            cGB.MsgBox("Funzioni di grafico non ancora supportate per il Mono Framework.", MessageBoxIcon.Exclamation);
            return;
#endif

            try
            {
                var s = Activator.CreateInstance(null, NomeClasse);
                var z = s.Unwrap() as cMyUserControl;

                AddNewTab(z, Title, i);
            }
            catch (Exception ex)
            {
                cGB.MsgBox($"Errore durante la creazione della finestra del grafico: {ex.Message}", MessageBoxIcon.Error);

                if (cGB.MsgBox("Vuoi provare ad installare la libreria Microsoft Chart (potrebbe risolvere il problema)?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    try
                    {
                        Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=14422");
                    }
                    catch (Exception ex2)
                    {
                        cGB.MsgBox(ex2.Message, MessageBoxIcon.Error);
                    }
            }
        }

        private void AddNewTab(UserControl s, string Title, Image i)
        {
            pQuickInserimento.Visible = false;

            if (!ilTabs.Images.ContainsKey(Title))
                ilTabs.Images.Add(Title, new Bitmap(i, 16, 16));

            var newPag = new TabPage();
            newPag.Controls.Add(s);
            s.Dock = DockStyle.Fill;

            tcSchede.TabPages.Add(newPag);
            tcSchede.SelectedIndex = tcSchede.TabPages.Count - 1;

            newPag.Parent = tcSchede;
            newPag.ImageKey = Title;
            newPag.Text = Title;
            newPag.BackColor = Color.White;
            newPag.BorderStyle = BorderStyle.None;

            s.BorderStyle = BorderStyle.None;
            s.Focus();
        }

        public void ShowCerca(string cassa)
        {
            var s = new cRicerca(cassa);
            AddNewTab(s, "Ricerca", Resources.find32);
        }

        public void ShowCash(string titolo, Image i, cFiltriRicerca filtri)
        {
            var s = new cSaldo(titolo, filtri);
            AddNewTab(s, titolo, i);
        }

        private void ShowCash(string titolo, string img)
        {
            for (var i = 0; i < ilAlbero.Images.Keys.Count; i++)
                if (ilAlbero.Images.Keys[i].Equals(img))
                {
                    ShowCash(titolo, ilAlbero.Images[i]);
                    break;
                }
        }

        private void bNuovoMovimento_Click(object sender, EventArgs e)
        {
            Action(eActions.NuovoMovimento);
        }

        private void bNuovoGiroconto_Click(object sender, EventArgs e)
        {
            Action(eActions.NuovoGiroconto);
        }

        private void ShowCash(string titolo, Image i)
        {
            ShowCash(titolo, i, new cFiltriRicerca());
        }

        private void bCassaQualsiasi_Click(TreeNode s)
        {
            var z = s.Tag.ToString();
            var i = s.ImageKey;

            ShowCash(z, i);
        }

        public void Action(eActions a, bool ByUser = false)
        {
            LastSelectedNode = cAlbero.SelectedNode;

            switch (a)
            {
                case eActions.NuovoMovimento:
                    {
                        using (var sce = new fGiroconto
                        {
                            Titolo = "Scegli la cassa in cui vuoi inserire"
                        })
                            if (sce.ShowDialog() == DialogResult.OK)
                            {
                                var mov = new cMovimenti();

                                using (var fi = new fInserimento
                                {
                                    Tipo = sce.CassaSelezionata,
                                    Saldo = mov.Saldo(sce.CassaSelezionata)
                                })
                                    if (fi.ShowDialog() == DialogResult.OK)
                                        cGB.RationesCurareMainForm.LoadAllCash();
                            }

                        break;
                    }

                case eActions.NuovoGiroconto:
                    {
                        using (var sce = new fGiroconto
                        {
                            Titolo = "Scegli la cassa in cui vuoi inserire"
                        })
                            if (sce.ShowDialog() == DialogResult.OK)
                                using (var g = new fGiroconto(sce.CassaSelezionata))
                                    if (g.ShowDialog() == DialogResult.OK)
                                    {
                                        var mov = new cMovimenti();

                                        using (var fi = new fInserimento
                                        {
                                            Modalita = fInserimento.eModalita.Giroconto,
                                            TipoGiroconto = g.CassaSelezionata,
                                            Tipo = sce.CassaSelezionata,
                                            Saldo = mov.Saldo(sce.CassaSelezionata)
                                        })
                                            if (fi.ShowDialog() == DialogResult.OK)
                                                cGB.RationesCurareMainForm.LoadAllCash();
                                    }

                        break;
                    }

                case eActions.Calendario:
                    {
                        var c = new cCalendario();
                        AddNewTab(c, "Calendario", Resources.calendario32);
                        break;
                    }

                case eActions.MacroAree:
                    {
                        var c = new cMacroAree();
                        AddNewTab(c, "Macro aree", Resources.MacroAree);
                        break;
                    }

                case eActions.OpzioniDB:
                    {
                        using (var d = new fOpzioniDb())
                            d.ShowDialog();
                        break;
                    }

                case eActions.Calcolatrice:
                    {
                        var d = new fCalc
                        {
                            StartPosition = FormStartPosition.CenterScreen,
                            TopMost = true,
                            ShowInTaskbar = true
                        };

                        d.Show();
                        break;
                    }

                case eActions.ControllaPromemoria:
                    {
                        var c = new DB.DataWrapper.cCalendario();

                        if (c.PresenzaPromemoria())
                        {
                            using (fPromemoria p = new fPromemoria())
                                p.ShowDialog();
                        }
                        else
                        {
                            if (ByUser)
                                cGB.MsgBox("Nessun promemoria a breve scadenza.");
                        }

                        break;
                    }

                case eActions.ControllaPeriodiciSoloAlert:
                    {
                        var c = new cPeriodici();
                        var mov_periodici_entro_oggi = c.RicercaScadenzeEntroOggi_plus_X_Giorni(5);

                        if (mov_periodici_entro_oggi != null)
                            if (mov_periodici_entro_oggi.Count > 0)
                            {
                                foreach (var pi in mov_periodici_entro_oggi)
                                {
                                    var dtd = DateTime.Now;
                                    var MeseDaAggiungere = 0;

                                    switch (pi.TipoGiornoMese)
                                    {
                                        case cPeriodici.ePeriodicita.G:
                                            if (pi.PartendoDalGiorno.Year < 1900)
                                                dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddDays(pi.NumeroGiorni));
                                            else
                                                dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.PartendoDalGiorno.Day).AddDays(pi.NumeroGiorni));
                                            break;
                                        case cPeriodici.ePeriodicita.M:
                                            MeseDaAggiungere = 1;
                                            break;
                                        case cPeriodici.ePeriodicita.B:
                                            MeseDaAggiungere = 2;
                                            break;
                                        case cPeriodici.ePeriodicita.T:
                                            MeseDaAggiungere = 3;
                                            break;
                                        case cPeriodici.ePeriodicita.Q:
                                            MeseDaAggiungere = 4;
                                            break;
                                        case cPeriodici.ePeriodicita.S:
                                            MeseDaAggiungere = 6;
                                            break;
                                        case cPeriodici.ePeriodicita.A:
                                            MeseDaAggiungere = 12;
                                            break;
                                    }

                                    switch (pi.TipoGiornoMese)
                                    {
                                        case cPeriodici.ePeriodicita.M:
                                        case cPeriodici.ePeriodicita.B:
                                        case cPeriodici.ePeriodicita.T:
                                        case cPeriodici.ePeriodicita.Q:
                                        case cPeriodici.ePeriodicita.S:
                                        case cPeriodici.ePeriodicita.A:
                                            dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddMonths(MeseDaAggiungere));
                                            break;
                                    }

                                    pi.GiornoDelMese = dtd;
                                }

                                mov_periodici_entro_oggi.Sort();

                                using (var f = new fPromemoriaPeriodici { Movimenti = mov_periodici_entro_oggi })
                                    f.ShowDialog();
                            }

                        break;
                    }

                case eActions.ControllaPeriodici:
                    {
                        var CiSono = false;
                        var c = new cPeriodici();
                        var mov_periodici_entro_oggi = c.RicercaScadenzeEntroOggi();

                        if (mov_periodici_entro_oggi != null)
                            if (mov_periodici_entro_oggi.Count > 0)
                                CiSono = true;

                        if (CiSono)
                        {
                            var QualcosaInserito = false;

                            if (cGB.MsgBox("Ci sono dei movimenti periodici da inserire, vuoi visualizzarli ora?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (var pi in mov_periodici_entro_oggi)
                                    using (var inz = new fInserimento())
                                    {
                                        var dtd = DateTime.Now;
                                        var MeseDaAggiungere_ = 0;

                                        switch (pi.TipoGiornoMese)
                                        {
                                            case cPeriodici.ePeriodicita.G:
                                                if (pi.PartendoDalGiorno.Year < 1900)
                                                    dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddDays(pi.NumeroGiorni));
                                                else
                                                    dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.PartendoDalGiorno.Day).AddDays(pi.NumeroGiorni));
                                                break;
                                            case cPeriodici.ePeriodicita.M:
                                                MeseDaAggiungere_ = 1;
                                                break;
                                            case cPeriodici.ePeriodicita.B:
                                                MeseDaAggiungere_ = 2;
                                                break;
                                            case cPeriodici.ePeriodicita.T:
                                                MeseDaAggiungere_ = 3;
                                                break;
                                            case cPeriodici.ePeriodicita.Q:
                                                MeseDaAggiungere_ = 4;
                                                break;
                                            case cPeriodici.ePeriodicita.S:
                                                MeseDaAggiungere_ = 6;
                                                break;
                                            case cPeriodici.ePeriodicita.A:
                                                MeseDaAggiungere_ = 12;
                                                break;
                                        }

                                        switch (pi.TipoGiornoMese)
                                        {
                                            case cPeriodici.ePeriodicita.M:
                                            case cPeriodici.ePeriodicita.B:
                                            case cPeriodici.ePeriodicita.T:
                                            case cPeriodici.ePeriodicita.Q:
                                            case cPeriodici.ePeriodicita.S:
                                            case cPeriodici.ePeriodicita.A:
                                                dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddMonths(MeseDaAggiungere_));
                                                break;
                                        }

                                        inz.Tipo = pi.tipo;
                                        inz.eDescrizione.Text = pi.descrizione;
                                        inz.eMacroArea.Text = pi.MacroArea == null || pi.MacroArea == "" ? inz.GetMacroArea4Descrizione(pi.descrizione) : pi.MacroArea;
                                        inz.eNome.Text = pi.nome;
                                        inz.eSoldi.Value = pi.soldi;
                                        inz.eData.Value_ = pi.GiornoDelMese;

                                        if (inz.ShowDialog() == DialogResult.OK)
                                        {
                                            if (pi.TipoGiorniMese == 'G')
                                            {
                                                pi.GiornoDelMese = new DateTime();
                                                pi.PartendoDalGiorno = dtd;
                                            }
                                            else
                                            {
                                                pi.PartendoDalGiorno = new DateTime();
                                                pi.GiornoDelMese = dtd;
                                            }

                                            pi.Salva();
                                            QualcosaInserito = true;
                                        }
                                    }

                                if (QualcosaInserito)
                                    LoadAllCash();
                            }
                        }
                        else
                        {
                            if (ByUser)
                                cGB.MsgBox("Non ricorre nessun movimento periodico.");
                        }

                        break;
                    }

                case eActions.GraficoSpline:
                    AddNewTabGrafico("RationesCurare7.UI.Controlli.cGraficoSpline", "Grafico a linee", Resources.grafico32);
                    break;
                case eActions.Grafico:
                    AddNewTabGrafico("RationesCurare7.UI.Controlli.cGrafico", "Grafico", Resources.grafico32);
                    break;
                case eActions.Torta:
                    AddNewTabGrafico("RationesCurare7.UI.Controlli.cGraficoTorta", "Torta", Resources.PieChart);
                    break;

                case eActions.MovimentiPeriodici:
                    {
                        var c = new cMovimentiPeriodici();
                        AddNewTab(c, "Movimenti periodici", Resources.perdioci32);
                        break;
                    }

                case eActions.Casse:
                    {
                        var c = new Controlli.cCasse();
                        AddNewTab(c, "Casse", Resources.ingranaggio32);
                        break;
                    }

                case eActions.Novita:
                    {
                        var c = new cNovita();
                        AddNewTab(c, "Novità", Resources.star32);
                        break;
                    }

                case eActions.Cerca:
                    {
                        var c = new cRicerca();
                        AddNewTab(c, "Ricerca", Resources.find32);
                        break;
                    }

                case eActions.About:
                    {
                        var c = new cAbout();
                        AddNewTab(c, "About", Resources.about32);
                        break;
                    }

                case eActions.CosaNePensi:
                    {
                        var c = new cCosaNePensi();
                        AddNewTab(c, "Cosa ne pensi?", Resources.mail32);
                        break;
                    }
            }

            cAlbero.SelectedNode = LastSelectedNode;
        }

        public void ChiudiTutteSchede()
        {
            while (tcSchede.TabPages.Count > 0)
                ChiudiSchedaCorrente();
        }

        public void ChiudiSchedaCorrente()
        {
            if (tcSchede.SelectedIndex > -1)
            {
                var t = tcSchede.TabPages[tcSchede.SelectedIndex];

                if (tcSchede.TabPages.Count > 1)
                {
                    var c = tcSchede.SelectedIndex;

                    while (c == tcSchede.SelectedIndex)
                    {
                        c--;

                        if (c < 0)
                            c = tcSchede.TabPages.Count - 1;
                    }

                    tcSchede.SelectedIndex = c;
                }
                else
                {
                    tcSchede.SelectedIndex = -1;
                }

                tcSchede.TabPages.Remove(t);
            }

            pQuickInserimento.Visible = tcSchede.TabPages.Count < 1;

            cAlbero.Select();
            cAlbero.SelectedNode = LastSelectedNode;
        }

        private void fMain_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO: [A] Testare tutta l'applicazione da tastiera senza mouse
            if (e.Alt)
            {
                var treeNodeName = "";
                cAlbero.Select();

                switch (e.KeyCode)
                {
                    case Keys.D1:
                        treeNodeName = "nCasse";
                        break;
                    case Keys.D2:
                        treeNodeName = "nStrumenti";
                        break;
                    case Keys.D3:
                        treeNodeName = "nOpzioni";
                        break;
                }

                if (!treeNodeName.Equals(""))
                {
                    cAlbero.CollapseAll();

                    foreach (TreeNode item in cAlbero.Nodes)
                        if (item.Name.Equals(treeNodeName))
                        {
                            item.Expand();
                            cAlbero.SelectedNode = item;
                            break;
                        }
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        ChiudiSchedaCorrente();
                        break;
                    case Keys.Enter:
                        if (cAlbero.SelectedNode != null)
                        {
                            LastSelectedNode = cAlbero.SelectedNode;
                            button_NodeClick(cAlbero.SelectedNode);
                        }
                        break;
                    case Keys.F3:
                        Action(eActions.Cerca);
                        break;
                    case Keys.F6:
                        Action(eActions.Calendario);
                        break;
                    case Keys.F7:
                        Action(eActions.Calcolatrice);
                        break;
                    case Keys.F8:
                        Action(eActions.OpzioniDB);
                        break;
                    case Keys.F10:
                        Action(eActions.Torta);
                        break;
                    case Keys.F11:
                        Action(eActions.Grafico);
                        break;
                }
            }
        }

        private void button_NodeClick(TreeNode s)
        {
            if (s.Name.Equals("nAbout"))
                Action(eActions.About);
            else if (s.Name.Equals("nCalcolatrice"))
                Action(eActions.Calcolatrice);
            else if (s.Name.Equals("nCalendario"))
                Action(eActions.Calendario);
            else if (s.Name.Equals("nGestioneCasse"))
                Action(eActions.Casse);
            else if (s.Name.Equals("nCerca"))
                Action(eActions.Cerca);
            else if (s.Name.Equals("nControllaPromemoria"))
                Action(eActions.ControllaPromemoria, true);
            else if (s.Name.Equals("nControllaPeriodici"))
                Action(eActions.ControllaPeriodici, true);
            else if (s.Name.Equals("nCosaNePensi"))
                Action(eActions.CosaNePensi);
            else if (s.Name.Equals("nGrafico"))
                Action(eActions.Grafico);
            else if (s.Name.Equals("nGraficoSpline"))
                Action(eActions.GraficoSpline);
            else if (s.Name.Equals("nTorta"))
                Action(eActions.Torta);
            else if (s.Name.Equals("nMovimentiPeriodici"))
                Action(eActions.MovimentiPeriodici);
            else if (s.Name.Equals("nNovita"))
                Action(eActions.Novita);
            else if (s.Name.Equals("nOpzioniDB"))
                Action(eActions.OpzioniDB);
            else if (s.Name.Equals("nMacroAree"))
                Action(eActions.MacroAree);
            else if (s.Tag != null) //casse extra
                bCassaQualsiasi_Click(s);
        }

        private void tcSchede_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                ChiudiSchedaCorrente();
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChiudiSchedaCorrente();
        }

        private void advTree1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            button_NodeClick(e.Node);
        }


    }
}