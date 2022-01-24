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
    public partial class cDateControl : UserControl
    {

        public DateTime Value_
        {
            get
            {
                DateTime v = dateTimePicker1.Value;
                cGB.Time o = cTime1.Value_;

                return new DateTime(v.Year, v.Month, v.Day, o.Ora, o.Minuto, 0);
            }
            set
            {
                try
                {
                    dateTimePicker1.Value = value;
                }
                catch
                {
                    //cannot set
                }

                cTime1.Value_ = new cGB.Time
                {
                    Ora = value.Hour,
                    Minuto = value.Minute
                };
            }
        }

        public DateTime MinDate
        {
            get
            {
                return dateTimePicker1.MinDate;
            }
            set
            {
                dateTimePicker1.MinDate = value;
            }
        }

        public bool Checked
        {
            get
            {
                return dateTimePicker1.Checked;
            }
            set
            {
                dateTimePicker1.Checked = value;
            }
        }

        public bool ShowCheckBox
        {
            get
            {
                return dateTimePicker1.ShowCheckBox;
            }
            set
            {
                dateTimePicker1.ShowCheckBox = value;
            }
        }

        public cDateControl()
        {
            InitializeComponent();
        }


    }
}