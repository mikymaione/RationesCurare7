/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public class cMyUserControl : UserControl
    {
        protected Button AcceptButton;
        protected Control AutoFocus;
        protected sLatoBordo LatoDaDisegnare = new sLatoBordo();

        protected enum eLatoBordo
        {
            H, W, HW, Null
        }

        protected struct sLatoBordo
        {
            public bool Setted;
            public int W, H;
            public eLatoBordo LatoDaDisegnare;
        }

        public bool DesignTime
        {
            get
            {
                return (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
            }
        }

        public cMyUserControl()
        {
            this.Load += new EventHandler(cMyUserControl_Load);
            this.Paint += new PaintEventHandler(cMyUserControl_Paint);
        }

        public void ChiudiScheda()
        {
            try
            {
                cGB.RationesCurareMainForm.tcSchede.Controls.Remove(cGB.RationesCurareMainForm.tcSchede.TabPages[cGB.RationesCurareMainForm.tcSchede.SelectedIndex]);
            }
            catch
            {
                //error
            }
        }

        private void cMyUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                this.ParentForm.AcceptButton = AcceptButton;
            }
            catch
            {
                //not loaded
            }

            try
            {
                AutoFocus.Focus();
            }
            catch
            {
                //not loaded                
            }
        }

        protected void DisegnaBordo(eLatoBordo b, System.Drawing.Graphics g)
        {
            DisegnaBordo(b, g, this.Width, 40);
        }

        protected void DisegnaBordo(eLatoBordo b, System.Drawing.Graphics g, Control sender)
        {
            var h = sender.Height - 1;
            var w = sender.Width;

            DisegnaBordo(b, g, w, h);
        }

        private void DisegnaBordo(eLatoBordo b, System.Drawing.Graphics g, int w, int h)
        {
            var H = false;
            var W = false;

            if (b == eLatoBordo.H)
                H = true;

            if (b == eLatoBordo.W)
                W = true;

            if (b == eLatoBordo.HW)
            {
                H = true;
                W = true;
            }

            if (H)
                g.DrawLine(cGB.myPenLeft, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, h));

            if (W)
                g.DrawLine(cGB.myPenBottom, new System.Drawing.Point(0, h), new System.Drawing.Point(w, h));
        }

        private void cMyUserControl_Paint(object sender, PaintEventArgs e)
        {
            if (LatoDaDisegnare.Setted)
                if (LatoDaDisegnare.W > 0)
                {
                    DisegnaBordo(LatoDaDisegnare.LatoDaDisegnare, e.Graphics, LatoDaDisegnare.W, LatoDaDisegnare.H);
                }
                else
                {
                    DisegnaBordo(LatoDaDisegnare.LatoDaDisegnare, e.Graphics);
                }
        }

        protected bool MsgElimina(string addText = "")
        {
            return (cGB.MsgBox("Sicuro di voler eliminare " + addText + "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }


    }
}