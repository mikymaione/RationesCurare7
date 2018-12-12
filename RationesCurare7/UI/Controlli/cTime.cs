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
    public partial class cTime : UserControl
    {
        public cGB.Time Value_
        {
            get
            {
                return cGB.StringToTime(maskedTextBox1.Text);
            }
            set
            {
                maskedTextBox1.Text = cGB.TimeToString(value);
            }
        }

        public cTime()
        {
            InitializeComponent();

            if (!cGB.String_For_Time_IsValid(maskedTextBox1.Text))
                maskedTextBox1.Text = cGB.TimeToString(DateTime.Now);
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            cGB.Time v = Value_;
            cGB.Time t = v;

            if (v.Ora > 23)
                t.Ora = 0;

            if (v.Minuto > 59)
                t.Minuto = 0;

            if (v.Ora < 0)
                t.Ora = 0;

            if (v.Minuto < 0)
                t.Minuto = 0;

            Value_ = t;
        }


    }
}