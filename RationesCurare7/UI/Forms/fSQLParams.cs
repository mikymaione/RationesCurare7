/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;

namespace RationesCurare7.UI.Forms
{
    public partial class fSQLParams : fMyForm
    {

        public Dictionary<string, object> Valori
        {
            get
            {
                var D = new Dictionary<string, object>();

                foreach (Controlli.cParamEdit c in flowLayoutPanel1.Controls)
                    D.Add(c.Nome, TryParse(c.Valore));

                return D;
            }
        }

        public fSQLParams(string sql) : this()
        {
            var paras = cGB.sDB.ParseParameters(sql);

            if (paras != null)
                foreach (var p in paras)
                    CreaComponente(p);
        }

        public fSQLParams()
        {
            InitializeComponent();
        }


        private void CreaComponente(string p)
        {
            flowLayoutPanel1.Controls.Add(new Controlli.cParamEdit(p));
        }

        private object TryParse(string p)
        {
            try
            {
                return Convert.ToDateTime(p);
            }
            catch
            {
                //no date
            }

            try
            {
                return Convert.ToDouble(p);
            }
            catch
            {
                //no number
            }

            return p;
        }


    }
}