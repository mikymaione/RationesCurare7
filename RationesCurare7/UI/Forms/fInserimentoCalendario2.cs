/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
    public partial class fInserimentoCalendario2 : fMyForm
    {
        public string ID_ = "";
        private DateTime DataSelezionata_;
        private DateTime precDataTermine;

        public DateTime DataSelezionata
        {
            get
            {
                return DataSelezionata_;
            }
            set
            {
                DataSelezionata_ = value;

                if (cGB.DateSonoUgualiPer_YYYYMMDD(eTerminaData.Value, DateTime.Now))
                {
                    precDataTermine = DataSelezionata_.AddMonths(1);
                    eTerminaData.Value = precDataTermine;
                }

                eTerminaData.MinDate = value.AddDays(1);
                eData.Text = value.ToShortDateString();
            }
        }

        public fInserimentoCalendario2()
        {
            InitializeComponent();

            cbRipetiOgni.ValueMember = "ID";
            cbRipetiOgni.DisplayMember = "Valore";
            cbRipetiOgni.Items.AddRange(
                new GB.cComboItem[] {
                    new GB.cComboItem("X","Mai"), //0
                    new GB.cComboItem("D","Ogni giorno"), //1
                    new GB.cComboItem("W","Ogni settimana"), //2
                    new GB.cComboItem("M","Ogni mese"), //3
                    new GB.cComboItem("Y","Ogni anno") //4
                }
            );

            cbTermina.ValueMember = "ID";
            cbTermina.DisplayMember = "Valore";
            cbTermina.Items.AddRange(
                new GB.cComboItem[] {
                    new GB.cComboItem("X","Mai"), //0                   
                    new GB.cComboItem("D","Data"), //1
                    new GB.cComboItem("N","Numero occorrenze") //2
                }
            );
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            Salva();
        }

        private void Salva()
        {
            string g = "";
            int Num = 1;
            int ripe = (int)eRipetiOgniN.Value;
            DateTime gg = DataSelezionata;

            if (cbTermina.SelectedIndex == 2)
                Num = (int)eNumeroOccorrenze.Value;

            if (ripe < 1)
                ripe = 1;

            if (Num == 1)
                if (cbRipetiOgni.SelectedIndex == 1)
                {
                    Num = 36500;
                    DateTime m = DataSelezionata;

                    if (cbTermina.SelectedIndex == 1)
                        for (int i = 0; i < Num; i++)
                        {
                            m = m.AddDays(ripe);

                            if (m > eTerminaData.Value)
                            {
                                Num = i;
                                break;
                            }
                        }
                }
                else if (cbRipetiOgni.SelectedIndex == 2)
                {
                    Num = 5200;
                    DateTime m = DataSelezionata;

                    if (cbTermina.SelectedIndex == 1)
                        for (int i = 0; i < Num; i++)
                        {
                            m = m.AddDays(7 * ripe);

                            if (m > eTerminaData.Value)
                            {
                                Num = i;
                                break;
                            }
                        }
                }
                else if (cbRipetiOgni.SelectedIndex == 3)
                {
                    Num = 1200;
                    DateTime m = DataSelezionata;

                    if (cbTermina.SelectedIndex == 1)
                        for (int i = 0; i < Num; i++)
                        {
                            m = m.AddMonths(ripe);

                            if (m > eTerminaData.Value)
                            {
                                Num = i;
                                break;
                            }
                        }
                }
                else if (cbRipetiOgni.SelectedIndex == 4)
                {
                    Num = 100;
                    DateTime m = DataSelezionata;

                    if (cbTermina.SelectedIndex == 1)
                        for (int i = 0; i < Num; i++)
                        {
                            m = m.AddYears(ripe);

                            if (m > eTerminaData.Value)
                            {
                                Num = i;
                                break;
                            }
                        }
                }

            DB.DataWrapper.cCalendario[] e = new DB.DataWrapper.cCalendario[Num];

            if (Num > 1)
                g = Guid.NewGuid().ToString();

            for (int i = 0; i < Num; i++)
            {
                e[i] = new DB.DataWrapper.cCalendario()
                {
                    ID = ID_,
                    Descrizione = eDescrizione.Text,
                    Giorno = gg,
                    IDGruppo = g
                };

                if (cbRipetiOgni.SelectedIndex == 1)
                    gg = gg.AddDays(ripe);
                else if (cbRipetiOgni.SelectedIndex == 2)
                    gg = gg.AddDays(7 * ripe);
                else if (cbRipetiOgni.SelectedIndex == 3)
                    gg = gg.AddMonths(ripe);
                else if (cbRipetiOgni.SelectedIndex == 4)
                    gg = gg.AddYears(ripe);
            }

            DB.DataWrapper.cCalendario mm = new DB.DataWrapper.cCalendario();

            if (mm.Inserisci(e) <= 0)
                MsgErroreSalvataggio();
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cbRipetiOgni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRipetiOgni.SelectedIndex > 0)
            {
                eRipetiOgniN.Enabled = true;
                eRipetiOgniN.Value = 1;
                cbTermina.Enabled = true;
            }
            else
            {
                eRipetiOgniN.Enabled = false;
                eRipetiOgniN.Text = null;
                cbTermina.Enabled = false;
            }
        }

        private void cbTermina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGB.DateSonoUgualiPer_YYYYMMDD(eTerminaData.Value, DataSelezionata_.AddMonths(1)))
            {
                eTerminaData.Value = precDataTermine;
            }
            else
            {
                precDataTermine = eTerminaData.Value;
            }

            if (cbTermina.SelectedIndex == 0)
            {
                eTerminaData.Enabled = false;
                eTerminaData.Value = DataSelezionata_.AddMonths(1);
                eNumeroOccorrenze.Enabled = false;
            }
            else if (cbTermina.SelectedIndex == 1)
            {
                eTerminaData.Enabled = true;
                eNumeroOccorrenze.Enabled = false;
            }
            else if (cbTermina.SelectedIndex == 2)
            {
                eTerminaData.Enabled = false;
                eNumeroOccorrenze.Enabled = true;
            }
        }

        private void fInserimentoCalendario2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Salva();
        }


    }
}