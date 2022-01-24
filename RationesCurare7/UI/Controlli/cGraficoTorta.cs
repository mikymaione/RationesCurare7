/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using RationesCurare7.DB.DataWrapper;

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
            eDa.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            eA.Value = cGB.DateTo235959(DateTime.Now.AddYears(1));
            cbImporti.SelectedIndex = 2;

            Cerca();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            Cerca();
        }

        private void Cerca()
        {
            Enabled = false;

            try
            {
                var mov = new cMovimenti
                {
                    DataDa = cGB.DateTo00000(eDa.Value),
                    DataA = cGB.DateTo235959(eA.Value)
                };

                var positivita = 0;
                var numCorre = -1;
                var area = "Altro";

                switch (cbImporti.SelectedIndex)
                {
                    case 0:
                        positivita = -1;
                        break;
                    case 1:
                        positivita = 1;
                        break;
                }

                Totale = mov.RicercaGraficoTortaSaldo(positivita);

                grafico.Series.Clear();

                using (var dataReader = mov.RicercaTorta(positivita))
                {
                    var serieNuova = new Series
                    {
                        ChartType = SeriesChartType.Pie,
                        SmartLabelStyle = { Enabled = true },
                        ["DrawingStyle"] = ColoreBarra
                    };

                    try
                    {
                        if (dataReader.HasRows)
                            while (dataReader.Read())
                            {
                                var soldi = cGB.ObjectToMoney(dataReader[1]);

                                try
                                {
                                    area = dataReader.GetString(0);

                                    if (area == null || area.Equals(""))
                                        area = "Altro";
                                }
                                catch
                                {
                                    area = "Altro";
                                }

                                if (soldi != 0)
                                {
                                    numCorre += 1;

                                    var datapointAttuale = new DataPoint
                                    {
                                        LegendText = ToEuroWithString(soldi, area),
                                        AxisLabel = area,
                                        YValues = DoubleToDataPointDouble(soldi),
                                        ToolTip = soldi.ToString("c")
                                    };

                                    serieNuova.Points.Insert(numCorre, datapointAttuale);
                                }
                            }

                        gbGrafico.Text = "Grafico a torta - Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);
                        serieNuova.Name = "Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);

                        grafico.Series.Add(serieNuova);
                    }
                    finally
                    {
                        dataReader.Close();
                    }
                }
            }
            finally
            {
                SettaTitolo();
                Enabled = true;

                foreach (var a in grafico.ChartAreas)
                    a.RecalculateAxesScale();
            }
        }

        private static double[] DoubleToDataPointDouble(double d)
        {
            return new[] { d };
        }

        private static string ToEuroWithString(double d, string s)
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