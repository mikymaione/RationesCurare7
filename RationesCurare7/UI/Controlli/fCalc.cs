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
    public partial class fCalc : Form
    {

        private double Saldo = 0;

        public double setSaldo
        {
            set
            {
                Saldo = Math.Round(value, 2);
            }
        }

        public double Value
        {
            get
            {
                return textBox1.Value;
            }
            set
            {
                textBox1.Value = value;
            }
        }

        public fCalc()
        {
            InitializeComponent();
            this.bEurom.Text = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            textBox1.Testo += " + ";
        }

        private void bSott_Click(object sender, EventArgs e)
        {
            textBox1.Testo += " - ";
        }

        private void bEurom_Click(object sender, EventArgs e)
        {
            textBox1.Testo += " - " + Saldo;
            DoCalc();
        }

        private void bDiv_Click(object sender, EventArgs e)
        {
            textBox1.Testo += " / ";
        }

        private void bMul_Click(object sender, EventArgs e)
        {
            textBox1.Testo += " * ";
        }

        private void bEq_Click(object sender, EventArgs e)
        {
            DoCalc();
        }

        private void DoCalc()
        {
            var j = textBox1.Testo;

            textBox1.DoCalc();

            listBox1.Items.Add(j + " = " + textBox1.Value);

            textBox1.Focus();
        }

        private void fCalc_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.Enter:
                    DoCalc();
                    break;

                case Keys.F1:
                    textBox1.Testo += " - " + Saldo;
                    DoCalc();
                    break;
            }
        }


    }
}