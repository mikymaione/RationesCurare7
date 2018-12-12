/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public class cMyUCRicerca : cMyUserControl
    {
        protected bool RicercaAbilitata = true;
        protected bool StampaAbilitata = true;
        protected BindingSource MyBindingSource;

        protected int SelectedID
        {
            get
            {
                var i = -1;

                try
                {
                    i = Convert.ToInt32(((System.Data.DataRowView)MyBindingSource.Current).Row.ItemArray[0]);
                }
                catch
                {
                    //cannot convert
                }

                return i;
            }
        }


    }
}