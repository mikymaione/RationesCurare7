/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fDateSelector : fMyForm
    {
        public DateTime ChoosedDate = DateTime.Now;

        public fDateSelector()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ChoosedDate = dateTimePicker1.Value;
            monthCalendar1.SelectionEnd = ChoosedDate;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ChoosedDate = monthCalendar1.SelectionEnd;
            dateTimePicker1.Value = ChoosedDate;
        }


    }
}