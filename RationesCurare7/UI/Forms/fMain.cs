/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fMain : Form
    {
        public enum eActions
        {
            NuovoMovimento,
            NuovoGiroconto,
            Saldo,
            Avere,
            Dare,
            MacroAree,
            Cassaforte,
            Portafogli,
            Salvadanaio,
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

        private TreeNode nCasse = null;
        private TreeNode bSaldo, bAvere, bDare, bCassaforte, bPortafoglio, bSalvadanaio;
        private List<DB.DataWrapper.cCasse> CasseAggiuntive = null;
        private string bSaldo_Text, bAvere_Text, bDare_Text, bCassaforte_Text, bPortafoglio_Text, bSalvadanaio_Text;
        private TreeNode LastSelectedNode = null;

        public fMain()
        {
            InitializeComponent();

            if (!cGB.DesignTime)
            {
                this.Text = "RationesCurare7 - " + cGB.CopyrightHolder;

                Init();
                Action(eActions.ControllaPromemoria);
                Action(eActions.ControllaPeriodiciSoloAlert);
                Action(eActions.ControllaPeriodici);
            }
        }

        private void Init()
        {
            bSaldo = cAlbero.Nodes[0].Nodes[6];
            bAvere = cAlbero.Nodes[0].Nodes[5];
            bDare = cAlbero.Nodes[0].Nodes[4];
            bCassaforte = cAlbero.Nodes[0].Nodes[1];
            bPortafoglio = cAlbero.Nodes[0].Nodes[3];
            bSalvadanaio = cAlbero.Nodes[0].Nodes[2];

            bSaldo_Text = bSaldo.Text;
            bAvere_Text = bAvere.Text;
            bDare_Text = bDare.Text;
            bCassaforte_Text = bCassaforte.Text;
            bPortafoglio_Text = bPortafoglio.Text;
            bSalvadanaio_Text = bSalvadanaio.Text;

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
                if (this.Height > 966)
                    cAlbero.ExpandAll();
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
            var m = new DB.DataWrapper.cMovimenti();

            bSaldo.Text = bSaldo_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Saldo"), "saldo");
            bAvere.Text = bAvere_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Avere"), "avere");
            bDare.Text = bDare_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Dare"), "dare");
            bCassaforte.Text = bCassaforte_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Cassaforte"), "cassaforte");
            bPortafoglio.Text = bPortafoglio_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Portafogli"), "portafogli");
            bSalvadanaio.Text = bSalvadanaio_Text + ": " + cGB.DoubleToMoneyStringValuta(m.Saldo("Salvadanaio"), "salvadanaio");

            bSaldo.ForeColor = (SaldoToColor(m.Saldo("Saldo")));
            bAvere.ForeColor = (SaldoToColor(m.Saldo("Avere")));
            bDare.ForeColor = (SaldoToColor(m.Saldo("Dare")));
            bCassaforte.ForeColor = (SaldoToColor(m.Saldo("Cassaforte")));
            bPortafoglio.ForeColor = (SaldoToColor(m.Saldo("Portafogli")));
            bSalvadanaio.ForeColor = (SaldoToColor(m.Saldo("Salvadanaio")));

            if (CasseAggiuntive != null)
                if (CasseAggiuntive.Count > 0)
                    foreach (var caz in CasseAggiuntive)
                        foreach (TreeNode item in nCasse.Nodes)
                            if (item.Name.Equals(caz.nome))
                            {
                                var saldC = m.Saldo(caz.nome);

                                item.Text = caz.nome + ": " + cGB.DoubleToMoneyStringValuta(saldC, caz.nome);
                                item.ForeColor = (SaldoToColor(saldC));
                            }

            cAlbero.Select();
        }

        public void AggiungiCasseExtra()
        {
            var cas = new DB.DataWrapper.cCasse();
            CasseAggiuntive = cas.CasseAggiuntive(false);

            if (CasseAggiuntive != null)
            {
                var sette = 7;

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

                            nCasse.Nodes.Insert(sette, n);
                            sette++;
                        }

                foreach (TreeNode nodo in nCasse.Nodes)
                    if (nodo.Index > 6)
                    {
                        var trovato = false;

                        foreach (var caz in CasseAggiuntive)
                            if (caz.nome.Equals(nodo.Tag))
                            {
                                trovato = true;
                                break;
                            }

                        if (!trovato)
                            nodo.Remove();
                    }
            }
        }

        private Image ImageFromByte(byte[] img)
        {
            try
            {
                return Image.FromStream(new System.IO.MemoryStream(img));
            }
            catch
            {
                //no img
                return Properties.Resources.saldo32;
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
                var z = s.Unwrap() as Controlli.cMyUserControl;

                AddNewTab(z, Title, i);
            }
            catch (Exception ex)
            {
                cGB.MsgBox($"Errore durante la creazione della finestra del grafico: {ex.Message}", MessageBoxIcon.Error);

                if (cGB.MsgBox($"Vuoi provare ad installare la libreria Microsoft Chart (potrebbe risolvere il problema)?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    try
                    {
                        System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=14422");
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
            var s = new Controlli.cRicerca(cassa);
            AddNewTab(s, "Ricerca", Properties.Resources.find32);
        }

        public void ShowCash(string titolo, Image i, GB.cFiltriRicerca filtri)
        {
            var s = new Controlli.cSaldo(titolo, filtri);
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
            ShowCash(titolo, i, new GB.cFiltriRicerca());
        }

        private void bCassaQualsiasi_Click(TreeNode s)
        {
            var i = "";
            var z = "Saldo";

            try
            {
                z = s.Tag.ToString();
                i = s.ImageKey;
            }
            catch
            {
                //cannot convert
            }

            ShowCash(z, i);
        }

        public void Action(eActions a, bool ByUser = false)
        {
            LastSelectedNode = cAlbero.SelectedNode;

            if (a == eActions.Cassaforte)
            {
                ShowCash(bCassaforte);
            }
            else if (a == eActions.Salvadanaio)
            {
                ShowCash(bSalvadanaio);
            }
            else if (a == eActions.Dare)
            {
                ShowCash(bDare);
            }
            else if (a == eActions.Avere)
            {
                ShowCash(bAvere);
            }
            else if (a == eActions.Portafogli)
            {
                ShowCash(bPortafoglio);
            }
            else if (a == eActions.Saldo)
            {
                ShowCash(bSaldo);
            }
            else if (a == eActions.NuovoMovimento)
            {
                using (var sce = new fGiroconto()
                {
                    Titolo = "Scegli la cassa in cui vuoi inserire"
                })
                    if (sce.ShowDialog() == DialogResult.OK)
                    {
                        var mov = new DB.DataWrapper.cMovimenti();

                        using (var fi = new fInserimento()
                        {
                            Tipo = sce.CassaSelezionata,
                            Saldo = mov.Saldo(sce.CassaSelezionata)
                        })
                            if (fi.ShowDialog() == DialogResult.OK)
                                cGB.RationesCurareMainForm.LoadAllCash();
                    }
            }
            else if (a == eActions.NuovoGiroconto)
            {
                using (var sce = new fGiroconto()
                {
                    Titolo = "Scegli la cassa in cui vuoi inserire"
                })
                    if (sce.ShowDialog() == DialogResult.OK)
                        using (var g = new fGiroconto(sce.CassaSelezionata))
                            if (g.ShowDialog() == DialogResult.OK)
                            {
                                var mov = new DB.DataWrapper.cMovimenti();

                                using (var fi = new fInserimento()
                                {
                                    Modalita = fInserimento.eModalita.Giroconto,
                                    TipoGiroconto = g.CassaSelezionata,
                                    Tipo = sce.CassaSelezionata,
                                    Saldo = mov.Saldo(sce.CassaSelezionata)
                                })
                                    if (fi.ShowDialog() == DialogResult.OK)
                                        cGB.RationesCurareMainForm.LoadAllCash();
                            }
            }
            else if (a == eActions.Calendario)
            {
                var c = new Controlli.cCalendario();
                AddNewTab(c, "Calendario", Properties.Resources.calendario32);
            }
            else if (a == eActions.MacroAree)
            {
                var c = new Controlli.cMacroAree();
                AddNewTab(c, "Macro aree", Properties.Resources.MacroAree);
            }
            else if (a == eActions.OpzioniDB)
            {
                using (var d = new fOpzioniDb())
                    d.ShowDialog();
            }
            else if (a == eActions.Calcolatrice)
            {
                var d = new Controlli.fCalc()
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true,
                    ShowInTaskbar = true
                };

                d.Show();
            }
            else if (a == eActions.ControllaPromemoria)
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
            }
            else if (a == eActions.ControllaPeriodiciSoloAlert)
            {
                var c = new DB.DataWrapper.cPeriodici();
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
                                case DB.DataWrapper.cPeriodici.ePeriodicita.G:
                                    if (pi.PartendoDalGiorno.Year < 1900)
                                        dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddDays(pi.NumeroGiorni));
                                    else
                                        dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.PartendoDalGiorno.Day).AddDays(pi.NumeroGiorni));
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.M:
                                    MeseDaAggiungere = 1;
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.B:
                                    MeseDaAggiungere = 2;
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.T:
                                    MeseDaAggiungere = 3;
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.Q:
                                    MeseDaAggiungere = 4;
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.S:
                                    MeseDaAggiungere = 6;
                                    break;
                                case DB.DataWrapper.cPeriodici.ePeriodicita.A:
                                    MeseDaAggiungere = 12;
                                    break;
                            }

                            switch (pi.TipoGiornoMese)
                            {
                                case DB.DataWrapper.cPeriodici.ePeriodicita.M:
                                case DB.DataWrapper.cPeriodici.ePeriodicita.B:
                                case DB.DataWrapper.cPeriodici.ePeriodicita.T:
                                case DB.DataWrapper.cPeriodici.ePeriodicita.Q:
                                case DB.DataWrapper.cPeriodici.ePeriodicita.S:
                                case DB.DataWrapper.cPeriodici.ePeriodicita.A:
                                    dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddMonths(MeseDaAggiungere));
                                    break;
                            }

                            pi.GiornoDelMese = dtd;
                        }

                        mov_periodici_entro_oggi.Sort();

                        using (var f = new fPromemoriaPeriodici() { Movimenti = mov_periodici_entro_oggi })
                            f.ShowDialog();
                    }
            }
            else if (a == eActions.ControllaPeriodici)
            {
                var CiSono = false;
                var c = new DB.DataWrapper.cPeriodici();
                var mov_periodici_entro_oggi = c.RicercaScadenzeEntroOggi();

                if (mov_periodici_entro_oggi != null)
                    if (mov_periodici_entro_oggi.Count > 0)
                        CiSono = true;

                if (CiSono)
                {
                    var QualcosaInserito = false;

                    if (cGB.MsgBox("Ci sono dei movimenti periodici da inserire, vuoi visualizzarli ora?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (var pi in mov_periodici_entro_oggi)
                            using (var inz = new fInserimento())
                            {
                                var dtd = DateTime.Now;
                                var MeseDaAggiungere_ = 0;

                                switch (pi.TipoGiornoMese)
                                {
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.G:
                                        if (pi.PartendoDalGiorno.Year < 1900)
                                            dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddDays(pi.NumeroGiorni));
                                        else
                                            dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.PartendoDalGiorno.Day).AddDays(pi.NumeroGiorni));
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.M:
                                        MeseDaAggiungere_ = 1;
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.B:
                                        MeseDaAggiungere_ = 2;
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.T:
                                        MeseDaAggiungere_ = 3;
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.Q:
                                        MeseDaAggiungere_ = 4;
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.S:
                                        MeseDaAggiungere_ = 6;
                                        break;
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.A:
                                        MeseDaAggiungere_ = 12;
                                        break;
                                }

                                switch (pi.TipoGiornoMese)
                                {
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.M:
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.B:
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.T:
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.Q:
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.S:
                                    case DB.DataWrapper.cPeriodici.ePeriodicita.A:
                                        dtd = cGB.DateToOnlyDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, pi.GiornoDelMese.Day).AddMonths(MeseDaAggiungere_));
                                        break;
                                }

                                inz.Tipo = pi.tipo;
                                inz.eDescrizione.Text = pi.descrizione;
                                inz.eMacroArea.Text = ((pi.MacroArea == null || pi.MacroArea == "") ? inz.GetMacroArea4Descrizione(pi.descrizione) : pi.MacroArea);
                                inz.eNome.Text = pi.nome;
                                inz.eSoldi.Value = pi.soldi;

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
            }
            else if (a == eActions.GraficoSpline)
            {
                AddNewTabGrafico("RationesCurare7.UI.Controlli.cGraficoSpline", "Grafico a linee", Properties.Resources.grafico32);
            }
            else if (a == eActions.Grafico)
            {
                AddNewTabGrafico("RationesCurare7.UI.Controlli.cGrafico", "Grafico", Properties.Resources.grafico32);
            }
            else if (a == eActions.Torta)
            {
                AddNewTabGrafico("RationesCurare7.UI.Controlli.cGraficoTorta", "Torta", Properties.Resources.PieChart);
            }
            else if (a == eActions.MovimentiPeriodici)
            {
                var c = new Controlli.cMovimentiPeriodici();
                AddNewTab(c, "Movimenti periodici", Properties.Resources.perdioci32);
            }
            else if (a == eActions.Casse)
            {
                var c = new Controlli.cCasse();
                AddNewTab(c, "Casse", Properties.Resources.ingranaggio32);
            }
            else if (a == eActions.Novita)
            {
                var c = new Controlli.cNovita();
                AddNewTab(c, "Novità", Properties.Resources.star32);
            }
            else if (a == eActions.Cerca)
            {
                var c = new Controlli.cRicerca();
                AddNewTab(c, "Ricerca", Properties.Resources.find32);
            }
            else if (a == eActions.About)
            {
                var c = new Controlli.cAbout();
                AddNewTab(c, "About", Properties.Resources.about32);
            }
            else if (a == eActions.CosaNePensi)
            {
                var c = new Controlli.cCosaNePensi();
                AddNewTab(c, "Cosa ne pensi?", Properties.Resources.mail32);
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

            pQuickInserimento.Visible = (tcSchede.TabPages.Count < 1);

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
                    case Keys.F1:
                        Action(eActions.Dare);
                        break;
                    case Keys.F2:
                        Action(eActions.Avere);
                        break;
                    case Keys.F3:
                        Action(eActions.Cerca);
                        break;
                    case Keys.F4:
                        Action(eActions.Portafogli);
                        break;
                    case Keys.F5:
                        Action(eActions.Saldo);
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
                    case Keys.F9:
                        Action(eActions.Cassaforte);
                        break;
                    case Keys.F10:
                        Action(eActions.Torta);
                        break;
                    case Keys.F11:
                        Action(eActions.Grafico);
                        break;
                    case Keys.F12:
                        Action(eActions.Salvadanaio);
                        break;
                }
            }
        }

        private void button_NodeClick(TreeNode s)
        {
            if (s.Name.Equals("nAbout"))
                Action(eActions.About);
            else if (s.Name.Equals("nAvere"))
                Action(eActions.Avere);
            else if (s.Name.Equals("nCalcolatrice"))
                Action(eActions.Calcolatrice);
            else if (s.Name.Equals("nCalendario"))
                Action(eActions.Calendario);
            else if (s.Name.Equals("nCassaforte"))
                Action(eActions.Cassaforte);
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
            else if (s.Name.Equals("nDare"))
                Action(eActions.Dare);
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
            else if (s.Name.Equals("nPortafoglio"))
                Action(eActions.Portafogli);
            else if (s.Name.Equals("nSaldo"))
                Action(eActions.Saldo);
            else if (s.Name.Equals("nSalvadanaio"))
                Action(eActions.Salvadanaio);
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