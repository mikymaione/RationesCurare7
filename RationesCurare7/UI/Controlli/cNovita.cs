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
    public partial class cNovita : cMyUserControl
    {
        public cNovita()
        {
            InitializeComponent();
            CaricaHtm();
        }

        private void CaricaHtm()
        {
            var z = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);            
            z = System.IO.Path.Combine(z, "Novita.htm");

            webBrowser1.Navigate(z);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            webBrowser1.Width = this.Width - 1;
            webBrowser1.Height = this.Height - 1;
            webBrowser1.Top = 1;
            webBrowser1.Left = 1;

            DisegnaBordo(eLatoBordo.H, e.Graphics);
        }


    }
}