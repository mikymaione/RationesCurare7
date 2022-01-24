/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.IO;
using System.Windows.Forms;
using RationesCurare7.DB;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.UI.Controlli;

namespace RationesCurare7.UI.Forms
{
#if DEBUG
    public partial class fListaUtenti : Form
#else
        public partial class fListaUtenti : fMyForm
#endif
    {
        private enum eDirezione
        {
            Su, Giu
        }

        private cUtenteListElement[] eUtenti;
        public int IDUtente;

        private int IDUtenteSelezionato
        {
            get
            {
                var x = -1;

                for (var i = 0; i < (eUtenti?.Length ?? 0); i++)
                    if (eUtenti[i].ImClicked)
                    {
                        x = i;
                        break;
                    }

                return x;
            }
        }

        public fListaUtenti()
        {
            InitializeComponent();
            Carica();
        }

        private void Carica()
        {
            var cisono = false;
            var us = new cUtenti();
            var u = us.ListaUtenti();

            if ((u?.Count ?? 0) > 0)
            {
                eUtenti = new cUtenteListElement[u.Count];

                for (var i = 0; i < eUtenti.Length; i++)
                {
                    eUtenti[i] = new cUtenteListElement
                    {
                        ID_ = u[i].ID,
                        NomeUtente = u[i].Nome,
                        Psw = u[i].Psw,
                        Email = u[i].Email,
                        PathDB = u[i].Path,
                        TipoDB = u[i].TipoDB,
                        Dock = DockStyle.Top
                    };

                    eUtenti[i].ClickEvent += fListaUtenti_ClickEvent;
                    eUtenti[i].DoubleClickEvent += fListaUtenti_DoubleClickEvent;
                }

                Array.Reverse(eUtenti);

                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.AddRange(eUtenti);

                eUtenti[eUtenti.Length - 1].Click_();

                cisono = true;
            }

            bOk.Enabled = cisono;
            bModifica.Enabled = cisono;
            bNascondi.Enabled = cisono;
        }

        private void fListaUtenti_DoubleClickEvent()
        {
            Accedi();
        }

        private void fListaUtenti_ClickEvent(object sender)
        {
            for (var i = 0; i < (eUtenti?.Length ?? 0); i++)
                eUtenti[i].DeselectMe();

            ((cUtenteListElement)sender).Click_(true);
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Accedi();
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            Modifica(true);
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            Modifica(false);
        }

        private int GetSelectedUserID()
        {
            var i = GetSelectedUserIndex();

            if (eUtenti[i].ImClicked)
                return eUtenti[i].ID_;

            return -1;
        }

        private int GetSelectedUserIndex()
        {
            for (var i = 0; i < (eUtenti?.Length ?? 0); i++)
                if (eUtenti[i].ImClicked)
                    return i;

            return -1;
        }

        private void Modifica(bool nuovo)
        {
            var continua = true;

            if (!nuovo)
                continua = ChiediPsw(GetSelectedUserIndex());

            if (continua)
                using (var f = new fDettaglioUtente())
                {
                    if (!nuovo)
                        f.ID_ = GetSelectedUserID();

                    if (f.ShowDialog() == DialogResult.OK)
                        Carica();
                }
        }

        private void bNascondi_Click(object sender, EventArgs e)
        {
            Elimina();
        }

        private void Elimina()
        {
            if (cGB.MsgBox("Nascondere l'utente?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (ChiediPsw(GetSelectedUserIndex()))
                {
                    var u = new cUtenti();
                    u.Elimina(GetSelectedUserID());

                    Carica();
                }
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            Cerca();
        }

        private void AggiungiQuestoDBAllaLista(string FileName, string psw)
        {
            if (File.Exists(FileName))
            {
                var Presente = false;
                var u = new cUtenti();
                u.CaricaByPath(FileName);

                if (u.ID > -1)
                    for (var i = 0; i < (eUtenti?.Length ?? 0); i++)
                        if (eUtenti[i].ID_ == u.ID)
                        {
                            Presente = true;
                            cGB.MsgBox("Questo utente è gia presente!", MessageBoxIcon.Exclamation);
                            break;
                        }

                if (!Presente)
                {
                    u.Nome = Path.GetFileNameWithoutExtension(FileName);
                    u.Path = FileName;
                    u.Email = u.Nome;
                    u.SetTipoDBByExtension(Path.GetExtension(FileName));

                    cGB.sDB = new cDB(false, cDB.DataBase.SQLite, FileName);

                    using (var fd = new fDettaglioUtente(u))
                        if (fd.ShowDialog() == DialogResult.OK)
                            Carica();
                }
            }
        }

        private void Cerca()
        {
            using (var op = new OpenFileDialog
            {
                Title = "Selezionare un database da aggiungere",
                Multiselect = false,
                Filter = "DataBase di RationesCurare|*.rqd*"
            })
                if (op.ShowDialog() == DialogResult.OK)
                    AggiungiQuestoDBAllaLista(op.FileName, "");
        }

        private void Accedi()
        {
            for (var i = 0; i < (eUtenti?.Length ?? 0); i++)
                if (eUtenti[i].ImClicked)
                {
                    if (File.Exists(eUtenti[i].PathDB))
                    {
                        if (ChiediPsw(i))
                        {
                            IDUtente = eUtenti[i].ID_;
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        cGB.MsgBox("Questo database è offline!", MessageBoxIcon.Exclamation);
                    }

                    break;
                }
        }

        private bool ChiediPsw(int i)
        {
            var ok = false;

            using (var p = new fPsw())
            {
                p.Email = eUtenti[i].Email;
                p.PswC = eUtenti[i].Psw;
                ok = p.ShowDialog() == DialogResult.OK;
            }

            return ok;
        }

        private void fListaUtenti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Modifica(true);
            else if (e.KeyCode == Keys.F7)
                NuovoDaWeb();
            else if (e.KeyCode == Keys.F2)
                Modifica(false);
            else if (e.KeyCode == Keys.F3)
                Cerca();
            else if (e.KeyCode == Keys.F4)
                Elimina();
        }

        private void fListaUtenti_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)//giù            
                SelectNextUser(eDirezione.Giu);
            else if (e.KeyCode == Keys.Up)//su            
                SelectNextUser(eDirezione.Su);
        }

        private void SelectNextUser(eDirezione d)
        {
            var j = -1;

            if (d == eDirezione.Su)
            {
                if (IDUtenteSelezionato > -1)
                    if (IDUtenteSelezionato < eUtenti.Length - 1)
                        j = IDUtenteSelezionato + 1;
            }
            else
            {
                if (IDUtenteSelezionato > -1)
                    if (IDUtenteSelezionato > 0)
                        j = IDUtenteSelezionato - 1;
            }

            if (j > -1)
            {
                flowLayoutPanel1.ScrollControlIntoView(eUtenti[j]);
                eUtenti[j].Click_();
            }
        }

        private void flowLayoutPanel1_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheel_(e.Delta);
        }

        void fListaUtenti_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheel_(e.Delta);
        }

        private void MouseWheel_(int d)
        {
            if (d < 0)
                SelectNextUser(eDirezione.Giu);
            else
                SelectNextUser(eDirezione.Su);
        }

        private void bNuovoDaWeb_Click(object sender, EventArgs e)
        {
            NuovoDaWeb();
        }

        private void NuovoDaWeb()
        {
            using (var fcw = new fCredenzialiWeb())
                if (fcw.ShowDialog() == DialogResult.OK)
                    AggiungiQuestoDBAllaLista(fcw.FileSelezionato, fcw.Psw);
        }


    }
}