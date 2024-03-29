﻿/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using RationesCurare7.DB.DataWrapper;

namespace RationesCurare7.UI.Controlli
{
    public partial class cGraficoSpline : cBaseGrafico
    {

        public cGraficoSpline()
        {
            InitializeComponent();

            if (!DesignTime)
            {
                AcceptButton = bCerca;
                Inita();
            }
        }

        private void Inita()
        {
            Cerca();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            Cerca();
        }

        private Series CreaSerie()
        {
            var s = new Series();
            s.ChartType = SeriesChartType.Line;
            s["DrawingStyle"] = ColoreBarra;

            return s;
        }

        private DataPoint CreaDataPoint(string LaData, double yuo, Color positivo, Color negativo)
        {
            return new DataPoint
            {
                AxisLabel = LaData,
                YValues = DoubleToDataPointDouble(yuo),
                ToolTip = ToEuroWithDate(yuo, LaData),
                Color = yuo < 0 ? negativo : positivo
            };
        }

        private DataPoint CreaDataPoint(string LaData, double yuo)
        {
            return CreaDataPoint(LaData, yuo, Color.Green, Color.Red);
        }

        private void SettaTitolo()
        {
            Titolo = cTitolo;
            Titolo += Environment.NewLine + "Saldo: " + cGB.DoubleToMoneyStringValuta(Totale);
        }

        private void bStampa_Click(object sender, EventArgs e)
        {
            Stampa(grafico);
        }

        public void Cerca()
        {
            Enabled = false;

            try
            {
                var mov = new cMovimenti();

                Totale = mov.RicercaGraficoSaldo(false);
                grafico.Series.Clear();

                using (var data_reader = mov.RicercaGraficoSpline())
                {
                    var NumCorre = -1;
                    var serie_nuova = CreaSerie();
                    var DataPrec = default(DateTime);
                    var DataAct = default(DateTime);
                    var DateUsate = new List<string>();
                    var AnnoSoldi = new SortedDictionary<string, double>();

                    try
                    {
                        var ultimoSoldi = 0D;

                        if (data_reader.HasRows)
                            while (data_reader.Read())
                            {
                                NumCorre += 1;
                                var LaData = data_reader.GetString(0);
                                DateUsate.Add(LaData);

                                DataPrec = DataAct;
                                DataAct = StringYYYYMMToDate(LaData);
                                var soldi = cGB.ObjectToMoney(data_reader[1]);
                                var soldiDB = soldi;

                                foreach (var yyyy in AnnoSoldi)
                                    soldi += yyyy.Value;

                                AnnoSoldi[LaData] = soldiDB;

                                var data_point_attuale = CreaDataPoint(LaData, soldi);

                                //inserire un datapoint vuoto se non ho dati nel db per quella data
                                if (DataPrec != DateTime.MinValue)
                                    if (DateDiff(DataPrec, DataAct) != 1)
                                    {
                                        var differenza_date = DiffDate(DataPrec, DataAct);

                                        for (var u = 0; u < (differenza_date?.Length ?? 0); u++)
                                            if (differenza_date[u] != LaData)
                                            {
                                                var data_point_vuoto_no_dati_su_db = new DataPoint
                                                {
                                                    YValues = DoubleToDataPointDouble(ultimoSoldi),
                                                    ToolTip = ToEuroWithDate(ultimoSoldi, differenza_date[u]),
                                                    AxisLabel = differenza_date[u],
                                                    Color = Color.Green
                                                };

                                                DateUsate.Add(differenza_date[u]);
                                                serie_nuova.Points.Insert(NumCorre, data_point_vuoto_no_dati_su_db);

                                                NumCorre += 1;
                                            }
                                    }

                                serie_nuova.Points.Insert(NumCorre, data_point_attuale);
                                ultimoSoldi = soldi;
                            } //fine lettura DB                       

                        gbGrafico.Text = "Grafico dei movimenti - Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);
                        serie_nuova.Name = "Saldo = " + cGB.DoubleToMoneyStringValuta(Totale);
                        grafico.Series.Add(serie_nuova);
                    }
                    finally
                    {
                        data_reader.Close();
                    }
                } //using data_reader
            }
            finally
            {
                SettaTitolo();
                Enabled = true;

                foreach (var area in grafico.ChartAreas)
                    area.RecalculateAxesScale();
            }
        }

        private int DateDiff(DateTime s1, DateTime s2)
        {
            var span = s2 - s1;
            var zeroTime = new DateTime(1, 1, 1);
            var m = (zeroTime + span).Year - 1;

            return m;
        }

        private string[] DiffDate(DateTime s1, DateTime s2)
        {
            var m = DateDiff(s1, s2);
            var s = new string[m];

            for (var i = 0; i < m; i++)
                s[i] = s1.AddYears(i + 1).ToString("yyyy");

            return s;
        }

        private DateTime StringYYYYMMToDate(string s)
        {
            var m = 1;
            var y = Convert.ToInt32(s.Substring(0, 4));

            if (s.Length > 4)
                m = Convert.ToInt32(s.Substring(s.Length - 2, 2));

            return new DateTime(y, m, 1);
        }

        private double[] DoubleToDataPointDouble(double d)
        {
            return new double[1] { d };
        }

        private string ToEuroWithDate(double d, string AnnoMese)
        {
            return AnnoMese + " " + cGB.DoubleToMoneyStringValuta(d);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DisegnaBordo(eLatoBordo.HW, e.Graphics, panel1);
        }


    }
}