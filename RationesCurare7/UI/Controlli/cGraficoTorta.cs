/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cGraficoTorta : cBaseGrafico
    {

        public cGraficoTorta()
        {
            InitializeComponent();

            if (!DesignTime)
            {
                AcceptButton = bCerca;
                Inita();
            }
        }

        private void SettaTitolo()
        {
            Titolo = cTitolo;
            Titolo += " [" + eDa.Value.ToShortDateString() + " - " + eA.Value.ToShortDateString() + "]";
            Titolo += Environment.NewLine + "Saldo: " + cGB.DoubleToMoneyStringValuta(Totale);
        }

        private void Inita()
        {
            var m = new DB.DataWrapper.cMovimenti();
            eDa.Value = new DateTime(2005, 1, 1, 0, 0, 0);
            eA.Value = cGB.DateTo235959(DateTime.Now.AddYears(1));
            cbImporti.SelectedIndex = 2;

            Cerca();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            Cerca();
        }

        public void Cerca()
        {
            this.Enabled = false;

            try
            {
                var mov = new DB.DataWrapper.cMovimenti()
                {
                    DataDa = cGB.DateTo00000(eDa.Value),
                    DataA = cGB.DateTo235959(eA.Value)
                };

                var positivita = 0;
                var NumCorre = -1;
                var area = "Altro";

                grafico.Series.Clear();

                if (cbImporti.SelectedIndex == 0)
                    positivita = -1;
                else if (cbImporti.SelectedIndex == 1)
                    positivita = 1;

                Totale = mov.RicercaGraficoTortaSaldo(positivita);

                using (var data_reader = mov.RicercaTorta(positivita))
                {
                    var serie_nuova = new System.Windows.Forms.DataVisualization.Charting.Series();
                    serie_nuova.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    serie_nuova.SmartLabelStyle.Enabled = true;
                    serie_nuova["DrawingStyle"] = ColoreBarra;

                    try
                    {
                        if (data_reader.HasRows)
                            while (data_reader.Read())
                            {
                                var soldi = cGB.ObjectToMoney(data_reader[1]);

                                try
                                {
                                    area = data_reader.GetString(0);

                                    if (area == null || area.Equals(""))
                                        area = "Altro";
                                }
                                catch
                                {
                                    area = "Altro";
                                }

                                if (soldi != 0)
                                {
                                    NumCorre += 1;

                                    var datapoint_attuale = new System.Windows.Forms.DataVisualization.Charting.DataPoint()
                                    {
                                        LegendText = ToEuroWithString(soldi, area),
                                        AxisLabel = area,
                                        YValues = DoubleToDataPointDouble(soldi),
                                        ToolTip = soldi.ToString("c")
                                    };

                                    serie_nuova.Points.Insert(NumCorre, datapoint_attuale);
                                }
                            }

                        this.gbGrafico.Text = "Grafico a torta - Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);
                        serie_nuova.Name = "Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);

                        grafico.Series.Add(serie_nuova);
                    }
                    finally
                    {
                        data_reader.Close();
                    }
                }
            }
            finally
            {
                SettaTitolo();
                this.Enabled = true;

                foreach (var a in grafico.ChartAreas)
                    a.RecalculateAxesScale();
            }
        }
   
        private double[] DoubleToDataPointDouble(double d)
        {
            return new double[] { d };
        }

        private string ToEuroWithString(double d, string s)
        {
            return s + " " + cGB.DoubleToMoneyStringValuta(d);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DisegnaBordo(eLatoBordo.HW, e.Graphics, panel1);
        }

        private void bStampa_Click(object sender, EventArgs e)
        {
            Stampa(grafico);
        }

        private void bPrecedente_Click(object sender, EventArgs e)
        {
            SpostatiInDirezione(-1, 0, ref eDa, ref eA);
            Cerca();
        }

        private void bSuccessivo_Click(object sender, EventArgs e)
        {
            SpostatiInDirezione(1, 0, ref eDa, ref eA);
            Cerca();
        }


    }
}