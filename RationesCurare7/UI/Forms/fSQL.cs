/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare7.UI.Forms
{
    public partial class fSQL : fMyForm
    {

        public fSQL(string query) : this()
        {
            eSQL.Text = query;
        }

        public fSQL()
        {
            InitializeComponent();
        }


        private void bEsegui_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource1;

            bindingSource1.DataSource = DB.cDB.EseguiSQLDataTable(eSQL.Text, GetParamsForSQL());
        }

        private System.Data.Common.DbParameter[] GetParamsForSQL()
        {
            using (var fSP = new fSQLParams(eSQL.Text))
                if (fSP.Valori.Count == 0 || fSP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    return DB.cDB.NewPars(fSP.Valori);

            return null;
        }

    }
}