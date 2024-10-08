﻿/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Windows.Forms;
using RationesCurare7.WS;

namespace RationesCurare7.UI.Controlli
{
    public partial class cCosaNePensi : cMyUserControl
    {
        public cCosaNePensi()
        {
            InitializeComponent();

            AutoFocus = eOggetto;

            LatoDaDisegnare = new sLatoBordo
            {
                Setted = true
            };
        }

        private void bInvia_Click(object sender, EventArgs ea)
        {
            Invia();
        }

        private void Invia()
        {
            bool TuttoRiempito = false;

            try
            {
                if (eOggetto.Text.Length > 3)
                    if (eTesto.Text.Length > 3)
                        if (cbGiudizio.SelectedIndex > -1)
                            TuttoRiempito = true;
            }
            catch
            {
                TuttoRiempito = false;
            }

            if (TuttoRiempito)
            {
                var ok = false;
                var r = "";

                Enabled = false;

                try
                {
                    var t = cGB.DatiUtente.Nome + " [" + cGB.DatiUtente.Email + "] :" + Environment.NewLine + eTesto.Text;

                    using (var e = new EmailSending())
                        r = e.MandaMail(eOggetto.Text + " [" + cbGiudizio.Text + "]", t, "mikymaione@hotmail.it");

                    ok = r.Equals("OK", StringComparison.OrdinalIgnoreCase);
                }
                catch (Exception ex)
                {
                    cGB.MsgBox(ex.Message, MessageBoxIcon.Error);
                }

                Enabled = true;

                if (ok)
                {
                    cGB.MsgBox("Inviato !", MessageBoxIcon.Information);
                    ChiudiScheda();
                }
                else
                {
                    cGB.MsgBox(r, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                cGB.MsgBox("Si prega di riempire tutti i campi!", MessageBoxIcon.Exclamation);
            }
        }


    }
}