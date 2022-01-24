/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.Collections.Generic;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.UI.Controlli;

namespace RationesCurare7.UI.Forms
{
    public partial class fPromemoriaPeriodici : fMyForm
    {

        private List<cPeriodici> mov_periodici;

        public List<cPeriodici> Movimenti
        {
            set
            {
                mov_periodici = value;
                Carica();
            }
        }

        public fPromemoriaPeriodici()
        {
            InitializeComponent();
        }

        private void Carica()
        {
            if (mov_periodici != null)
                if (mov_periodici.Count > 0)
                {
                    int i = -1;
                    var ctrls = new cMovimentoInfo[mov_periodici.Count];

                    foreach (var pi in mov_periodici)
                    {
                        var inz = new cMovimentoInfo();
                        inz.eData.Text += pi.GiornoDelMese.ToShortDateString();
                        inz.eCassa.Text += pi.tipo;
                        inz.eDescrizione.Text += pi.descrizione;
                        inz.eMacroArea.Text += pi.MacroArea;
                        inz.eNome.Text += pi.nome;
                        inz.eSoldi.Text += cGB.DoubleToMoneyStringValuta(pi.soldi);

                        ctrls[i += 1] = inz;
                    }

                    flowLayoutPanel1.Controls.AddRange(ctrls);
                }
        }


    }
}