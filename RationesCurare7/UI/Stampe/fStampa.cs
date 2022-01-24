/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Data;
using RationesCurare7.UI.Forms;

namespace RationesCurare7.UI.Stampe
{
    public partial class fStampa : fMyForm
    {
        public DataTable DataTable_;

        public fStampa() : this(null) {}

        public fStampa(DataTable DataTable__)
        {
            DataTable_ = DataTable__;

            InitializeComponent();
        }

        private void fStampa_Load(object sender, EventArgs e)
        {
            movimentiBindingSource.DataSource = DataTable_;

            #if __MonoCS__
            #else
            ReportViewer1.RefreshReport();
            #endif
        }


    }
}