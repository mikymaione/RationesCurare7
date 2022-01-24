/*
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
    public partial class cGrafico : cBaseGrafico
    {

        private DateTime MaxDate, MinDate;

        private enum eTipoData
        {
            Mese, Anno
        }

        public cGrafico()
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
            var m = new cMovimenti();
            m.IntervalloDate(out MinDate, out MaxDate);

            eDescrizione.AutoCompleteCustomSource = m.TutteLeDescrizioni();
            eMacroArea.AutoCompleteCustomSource = m.TutteLeMacroAree();
            eDa.Value = cGB.DateTo00000(MinDate);
            eA.Value = cGB.DateTo235959(MaxDate.AddMonths(3));
            cbPeriodicita.SelectedIndex = 0;

            Cerca();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            Cerca();
        }

        private Series CreaSerie()
        {
            var s = new Series
            {
                ChartType = SeriesChartType.Column,
                ["DrawingStyle"] = ColoreBarra
            };

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
            Titolo = cTitolo + " " + (cbPeriodicita.SelectedIndex == 1 ? "annuali" : "mensili");
            Titolo += " [" + eDa.Value.ToShortDateString() + " - " + eA.Value.ToShortDateString() + "]";
            Titolo += Environment.NewLine + "Saldo: " + cGB.DoubleToMoneyStringValuta(Totale);
        }

        private void Cerca()
        {
            Enabled = false;
            var ancheFuturi = cbPrevisti.Checked;

            try
            {
                var DataDa = cGB.DateTo00000(eDa.Value);
                var DataA = cGB.DateTo235959(eA.Value);

                var mov = new cMovimenti
                {
                    DataDa = DataDa,
                    DataA = DataA,
                    descrizione = cGB.QQ(eDescrizione.Text),
                    MacroArea = cGB.QQ(eMacroArea.Text)
                };

                Totale = mov.RicercaGraficoSaldo();
                grafico.Series.Clear();

                using (var data_reader = mov.RicercaGrafico(cbPeriodicita.SelectedIndex == 1))
                {
                    var NumCorre = -1;
                    var serie_nuova = CreaSerie();
                    var DataPrec = default(DateTime);
                    var DataAct = default(DateTime);
                    var DateUsate = new List<string>();
                    var tipo_intervallo = cbPeriodicita.SelectedIndex == 1 ? eTipoData.Anno : eTipoData.Mese;

                    try
                    {
                        if (data_reader.HasRows)
                            while (data_reader.Read())
                            {
                                NumCorre += 1;
                                var LaData = data_reader.GetString(0);
                                DateUsate.Add(LaData);

                                DataPrec = DataAct;
                                DataAct = StringYYYYMMToDate(LaData);
                                var soldi = cGB.ObjectToMoney(data_reader[1]);
                                var data_point_attuale = CreaDataPoint(LaData, soldi);

                                //inserire un datapoint vuoto se non ho dati nel db per quella data
                                if (DataPrec != DateTime.MinValue)
                                    if (DateDiff(tipo_intervallo, DataPrec, DataAct) != 1)
                                    {
                                        var differenza_date = DiffDate(tipo_intervallo, DataPrec, DataAct);

                                        for (var u = 0; u < (differenza_date?.Length ?? 0); u++)
                                            if (differenza_date[u] != LaData)
                                            {
                                                var data_point_vuoto_no_dati_su_db = new DataPoint
                                                {
                                                    YValues = DoubleToDataPointDouble(0D),
                                                    ToolTip = ToEuroWithDate(0D, differenza_date[u]),
                                                    AxisLabel = differenza_date[u],
                                                    Color = Color.Green
                                                };

                                                DateUsate.Add(differenza_date[u]);
                                                serie_nuova.Points.Insert(NumCorre, data_point_vuoto_no_dati_su_db);

                                                NumCorre += 1;
                                            }
                                    }

                                serie_nuova.Points.Insert(NumCorre, data_point_attuale);
                            } //fine lettura DB

                        if (ancheFuturi)
                        {
                            var val = 0d;
                            bool daAggiungere;
                            string LaDataPeriodica;
                            var per = new cPeriodici();
                            var movimenti = per.RicercaScadenzeCalcolate(DataDa, DataA);

                            for (var i = 0; i < movimenti.Count; i++)
                            {
                                daAggiungere = false;
                                LaDataPeriodica = movimenti[i].GiornoDelMese.ToString(tipo_intervallo == eTipoData.Mese ? "yyyy/MM" : "yyyy");
                                val += movimenti[i].soldi_d;

                                if (i < movimenti.Count - 1)
                                {
                                    if (LaDataPeriodica != movimenti[i + 1].GiornoDelMese.ToString(tipo_intervallo == eTipoData.Mese ? "yyyy/MM" : "yyyy"))
                                        daAggiungere = true;
                                }
                                else
                                {
                                    daAggiungere = true;
                                }

                                if (daAggiungere)
                                {
                                    if (DateUsate.Contains(LaDataPeriodica))
                                    {
                                        for (var v = 0; v < serie_nuova.Points.Count; v++)
                                            if (serie_nuova.Points[v].AxisLabel.Equals(LaDataPeriodica, StringComparison.OrdinalIgnoreCase))
                                            {
                                                serie_nuova.Points[v] = CreaDataPoint(LaDataPeriodica, serie_nuova.Points[v].YValues[0] + val, Color.Blue, Color.Brown);
                                                break;
                                            }
                                    }
                                    else
                                    {
                                        NumCorre++;
                                        var e = CreaDataPoint(LaDataPeriodica, val, Color.Blue, Color.Brown);
                                        serie_nuova.Points.Insert(NumCorre, e);
                                    }

                                    val = 0;
                                }
                            }

                            Totale = 0;
                            foreach (var t in serie_nuova.Points)
                                Totale += t.YValues[0];
                        } //fine movimenti futuri

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

        private int DateDiff(eTipoData intervallo, DateTime s1, DateTime s2)
        {
            var span = s2 - s1;
            var zeroTime = new DateTime(1, 1, 1);
            var m = intervallo == eTipoData.Mese ? (zeroTime + span).Month - 1 : (zeroTime + span).Year - 1;

            return m;
        }

        private string[] DiffDate(eTipoData intervallo, DateTime s1, DateTime s2)
        {
            var m = DateDiff(intervallo, s1, s2);
            var s = new string[m];

            for (var i = 0; i < m; i++)
                switch (intervallo)
                {
                    case eTipoData.Mese:
                        s[i] = s1.AddMonths(i + 1).ToString("yyyy/MM");
                        break;
                    case eTipoData.Anno:
                        s[i] = s1.AddYears(i + 1).ToString("yyyy");
                        break;
                }

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

        private static double[] DoubleToDataPointDouble(double d)
        {
            return new[] { d };
        }

        private static string ToEuroWithDate(double d, string AnnoMese)
        {
            return AnnoMese + " " + cGB.DoubleToMoneyStringValuta(d);
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
            SpostatiInDirezione(-1, cbPeriodicita.SelectedIndex, ref eDa, ref eA);
            Cerca();
        }

        private void bSuccessivo_Click(object sender, EventArgs e)
        {
            SpostatiInDirezione(1, cbPeriodicita.SelectedIndex, ref eDa, ref eA);
            Cerca();
        }


    }
}