/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cParamEdit : UserControl
    {

        public string Nome => lNome.Text;

        public string Valore => eValore.Text;


        public cParamEdit(string nome) : this()
        {
            lNome.Text = nome;
        }

        public cParamEdit()
        {
            InitializeComponent();
        }


    }
}