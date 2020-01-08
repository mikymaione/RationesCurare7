/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cUtenteListElement : UserControl
    {
        public delegate void ClickEventHandler(object sender);
        public event ClickEventHandler ClickEvent;
        public delegate void DoubleClickEventHandler();
        public event DoubleClickEventHandler DoubleClickEvent;
        public bool ImClicked = false;
        public int ID_ = -1;
        public string Psw = "";
        public string Email = "";
        public string TipoDB = "A";

        public string NomeUtente
        {
            get
            {
                return lNomeUtente.Text;
            }
            set
            {
                lNomeUtente.Text = value;
            }
        }

        public string PathDB
        {
            get
            {
                return lPathDB.Text;
            }
            set
            {
                lPathDB.Text = value;
                CaricaFoto();
            }
        }

        public cUtenteListElement()
        {
            InitializeComponent();
        }

        protected virtual void OnClickEvent()
        {
            ClickEventHandler handler = ClickEvent;

            if (handler != null)
                handler(this);
        }

        protected virtual void OnDoubleClickEvent()
        {
            DoubleClickEventHandler handler = DoubleClickEvent;

            if (handler != null)
                handler();
        }

        private void CaricaFoto()
        {
            if (System.IO.File.Exists(lPathDB.Text))
            {
                string l = System.IO.Path.ChangeExtension(lPathDB.Text, ".jpg");
                string p = System.IO.Path.ChangeExtension(lPathDB.Text, ".png");

                if (System.IO.File.Exists(l))
                {
                    cGB.LoadImage_Try(l, ref pUtente);
                }
                else if (System.IO.File.Exists(p))
                {
                    cGB.LoadImage_Try(p, ref pUtente);
                }
                else { 
                
                }
            }
            else
            {
                pUtente.Image = global::RationesCurare7.Properties.Resources.DBNotFound;
            }
        }

        public void DeselectMe()
        {
            ImClicked = false;
            this.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        public void Click_()
        {
            Click_(false);
        }

        public void Click_(bool ByCode)
        {
            ImClicked = true;
            this.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);

            if (!ByCode)
                OnClickEvent();
        }

        private void DoubleClick_()
        {
            OnDoubleClickEvent();
        }

        private void Leave_()
        {
            if (!ImClicked)
                DeselectMe();
        }

        private void Enter_()
        {
            if (!ImClicked)
                this.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
        }

        private void pUtente_Click(object sender, EventArgs e)
        {
            Click_();
        }

        private void lPathDB_Click(object sender, EventArgs e)
        {
            Click_();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Click_();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Click_();
        }

        private void lPathDB_MouseEnter(object sender, EventArgs e)
        {
            Enter_();
        }

        private void lPathDB_MouseLeave(object sender, EventArgs e)
        {
            Leave_();
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            Leave_();
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            Enter_();
        }

        private void pUtente_MouseEnter(object sender, EventArgs e)
        {
            Enter_();
        }

        private void pUtente_MouseLeave(object sender, EventArgs e)
        {
            Leave_();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            Leave_();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            Enter_();
        }

        private void lPathDB_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick_();
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick_();
        }

        private void pUtente_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick_();
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick_();
        }


    }
}