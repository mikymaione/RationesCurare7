/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fPromemoria : fMyForm
    {
        public fPromemoria()
        {
            InitializeComponent();
            Carica();
        }

        private void Carica()
        {
            DateTime n = DateTime.Now;
            DateTime da = new DateTime(n.Year, n.Month, n.Day);
            DateTime a = new DateTime(n.Year, n.Month, n.Day, 23, 59, 59);

            lOggi.Text = n.ToShortDateString();
            lDomani.Text = n.AddDays(1).ToShortDateString();

            DB.DataWrapper.cCalendario c = new DB.DataWrapper.cCalendario();
            List<DB.DataWrapper.cCalendario> oggi = c.Ricerca(da, a);

            da = da.AddDays(1);
            a = a.AddDays(1);

            List<DB.DataWrapper.cCalendario> doma = c.Ricerca(da, a);

            if (oggi != null)
                if (oggi.Count > 0)
                    Riempi(oggi, ref lTodoOggi);

            if (doma != null)
                if (doma.Count > 0)
                    Riempi(doma, ref lTodoDomani);
        }

        private void Riempi(List<DB.DataWrapper.cCalendario> c, ref TextBox l)
        {
            string h = "";

            foreach (DB.DataWrapper.cCalendario ci in c)
                h += "● " + ci.Descrizione + Environment.NewLine + Environment.NewLine;

            l.Text = h;
        }


    }
}