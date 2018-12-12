/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
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
    public partial class cCalendarItem : UserControl
    {
        public List<string> ID_ = new List<string>();
        public DateTime MyDate = DateTime.Now;
        protected string Title = "";
        protected string Description = "";

        public delegate void ClickEventHandler(cCalendarItem sender);
        public event ClickEventHandler ClickEvent;

        private Color PrecedentColor = Color.Empty;
        private bool Selected_ = false;

        public bool Selected
        {
            get
            {
                return Selected_;
            }
            set
            {
                Selected_ = value;

                if (value)
                {
                    if (PrecedentColor == Color.Empty)
                        PrecedentColor = BackColor;

                    BackColor = Color.Red;
                }
                else
                {
                    if (PrecedentColor != Color.Empty)
                        BackColor = PrecedentColor;
                }
            }
        }

        public Color BackCalendarColor
        {
            get
            {
                return lTesto.BackColor;
            }
            set
            {
                lTesto.BackColor = value;
                lGiorno.BackColor = value;
            }
        }

        public cCalendarItem()
        {
            InitializeComponent();
        }

        public virtual void OnClickEvent()
        {
            Selected = true;

            ClickEventHandler handler = ClickEvent;

            if (handler != null)
                handler(this);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            OnClickEvent();
        }

        public void ResetVar()
        {
            ID_ = new List<string>();
            Selected_ = false;
        }


    }
}