/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cMathEdit : UserControl
    {
        public bool Modificato = false;
        private double Saldo = 0;

        public double setSaldo
        {
            set
            {
                Saldo = Math.Round(value, 2);
            }
        }

        public cMathEdit()
        {
            InitializeComponent();
        }

        public bool Selected
        {
            get
            {
                return Focused || eMathTextBox.Focused || bOpenCalc.Focused;
            }
        }

        public bool ShowCalcButton
        {
            get
            {
                return this.bOpenCalc.Visible;
            }
            set
            {
                this.bOpenCalc.Visible = value;

                if (value)
                {
                    this.eMathTextBox.Width = this.Width - 24;
                }
                else
                {
                    this.eMathTextBox.Width = this.Width;
                }
            }
        }

        public string Testo
        {
            get
            {
                return eMathTextBox.Text;
            }
            set
            {
                eMathTextBox.Text = value;
            }
        }

        public double Value
        {
            get
            {
                return cGB.DoubleToMoney(eMathTextBox.Text);
            }
            set
            {
                eMathTextBox.Text = cGB.DoubleToMoneyString(value);
                eMathTextBox.SelectionStart = eMathTextBox.Text.Length;
            }
        }

        private void bOpenCalc_Click(object sender, EventArgs e)
        {
            MostraCalcolatrice();
        }

        private void MostraCalcolatrice()
        {
            using (var calc = new fCalc())
            {
                var p = this.PointToScreen(new Point());
                p.Y += 30;

                calc.Location = p;
                calc.Value = Value;
                calc.setSaldo = Saldo;
                calc.ShowDialog();

                Value = calc.Value;
            }
        }

        private void eMathTextBox_Leave(object sender, EventArgs e)
        {
            DoCalc();
        }

        private void eMathTextBox_TextChanged(object sender, EventArgs e)
        {
            Modificato = true;
        }

        private void eMathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DoCalc();
                    break;

                case Keys.F1:
                    eMathTextBox.Text += " - " + Saldo;
                    DoCalc();
                    break;

                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.OemMinus:
                case Keys.Oemplus:
                case Keys.Oemcomma:
                case Keys.OemPeriod:
                case Keys.Subtract:
                case Keys.Add:
                case Keys.Multiply:
                case Keys.Divide:
                case Keys.Left:
                case Keys.Right:
                case Keys.Decimal:
                case Keys.Delete:
                case Keys.Back:
                    e.SuppressKeyPress = false;
                    break;

                default:
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        public void Clear()
        {
            this.eMathTextBox.Text = "";
        }

        public void DoCalc()
        {
            var z = eMathTextBox.Text;
            z = z.Replace(",", ".");

            try
            {
                var calc = new ILCalc.CalcContext<double>();
                Value = calc.Evaluate(z);
            }
            catch
            {
                //error
            }

            Modificato = false;
        }


    }
}