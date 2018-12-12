/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2017 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Drawing;

namespace RationesCurare7.UI.Controlli
{
    public class cBaseGrafico : cMyUserControl
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart il_grafico;

        protected const string ColoreBarra = "LightToDark";
        protected const string cTitolo = "Grafico dei movimenti";
        protected string Titolo = cTitolo;
        protected double Totale = 0;
        protected DateTime PeriodoCorrente = DateTime.MinValue;

        protected void SpostatiInDirezione(int d, int Periodicita, ref System.Windows.Forms.DateTimePicker eDa, ref System.Windows.Forms.DateTimePicker eA)
        {
            if (PeriodoCorrente == DateTime.MinValue)
                PeriodoCorrente = DateTime.Now;

            if (Periodicita == 0) //mensile
            {
                PeriodoCorrente = PeriodoCorrente.AddMonths(d);
                var inizio = new DateTime(PeriodoCorrente.Year, PeriodoCorrente.Month, 1); ;

                eDa.Value = inizio;
                eA.Value = inizio.AddMonths(1).AddSeconds(-1);
            }
            else if (Periodicita == 1) //annuale
            {
                PeriodoCorrente = PeriodoCorrente.AddYears(d);
                var inizio = new DateTime(PeriodoCorrente.Year, 1, 1);

                eDa.Value = inizio;
                eA.Value = inizio.AddYears(1).AddSeconds(-1);
            }
        }

        protected void Stampa(System.Windows.Forms.DataVisualization.Charting.Chart grafico_)
        {
            il_grafico = grafico_;

            using (var pd = new System.Drawing.Printing.PrintDocument())
            {
                var m = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                il_grafico.Printing.PrintDocument = pd;
                il_grafico.Printing.PrintDocument.PrintPage += pd_PrintPage;

                il_grafico.Printing.PrintDocument.DefaultPageSettings.Margins = m;
                il_grafico.Printing.PrintDocument.DefaultPageSettings.Landscape = true;

                il_grafico.Printing.PrintPreview();
            }
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            using (var fontTitle = new Font("Times New Roman", 16))
            using (var format = new StringFormat())
            {
                var titlePosition = new Rectangle(ev.MarginBounds.X, ev.MarginBounds.Y, ev.MarginBounds.Width, ev.MarginBounds.Height);
                var titleSize = ev.Graphics.MeasureString(Titolo, fontTitle);
                titlePosition.Height = Convert.ToInt32(titleSize.Height);

                format.Alignment = StringAlignment.Center;
                ev.Graphics.DrawString(Titolo, fontTitle, Brushes.Black, titlePosition, format);

                var chartPosition = new Rectangle(ev.MarginBounds.X, titlePosition.Bottom, ev.MarginBounds.Width, 600);

                il_grafico.Printing.PrintPaint(ev.Graphics, chartPosition);
            }
        }


    }
}