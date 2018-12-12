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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cMacroAree : cMyUserControl
    {
        private int TotCount = 0;

        public cMacroAree()
        {
            InitializeComponent();

            gNoCat.Text = "Non categorizzati";
            gNoCat.Modificabile = false;

            Carica();
        }

        private void Carica()
        {
            string ulMA = "";
            DB.DataWrapper.cMovimenti mov = new DB.DataWrapper.cMovimenti();
            List<DB.DataWrapper.cMovimenti.sMacroArea_e_Descrizione> z = mov.MacroAree_e_Descrizioni();
            List<string> desc = mov.TutteLeDescrizioni_Array(false);

            gNoCat.ClearItems();
            pArea.Controls.Clear();

            if (desc != null)
                if (desc.Count > 0)
                    gNoCat.AddRange(desc);

            if (z != null)
                if (z.Count > 0)
                {
                    int g = -1;
                    int e = -1;
                    string[] elems = new string[z.Count];
                    cGroupList[] G = new cGroupList[z.Count];

                    for (int i = 0; i < z.Count; i++)
                    {
                        if ((!ulMA.Equals(z[i].MacroArea, StringComparison.OrdinalIgnoreCase)) || (i == z.Count - 1))
                        {
                            if (g > -1)
                                if (e > -1)
                                {
                                    string[] elems2 = new string[e + 1];
                                    Array.Copy(elems, elems2, e + 1);

                                    G[g].AddRange(elems2);

                                    e = -1;
                                }

                            if (!ulMA.Equals(z[i].MacroArea, StringComparison.OrdinalIgnoreCase))
                                G[g += 1] = NewG(z[i].MacroArea);
                        }

                        elems[e += 1] = z[i].Descrizione;
                        ulMA = z[i].MacroArea;
                    }

                    if (g > -1)
                    {
                        cGroupList[] G2 = new cGroupList[g + 1];
                        Array.Copy(G, G2, g + 1);
                        pArea.Controls.AddRange(G2);
                    }
                }
        }

        private cGroupList NewG(string Testo)
        {
            cGroupList g = new cGroupList()
            {
                Text = Testo,
                Width = 200,
                Height = 200
            };

            return g;
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            TotCount++;
            pArea.Controls.Add(NewG("MacroArea " + TotCount));
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            int Sele = 0;

            foreach (cGroupList i in pArea.Controls)
                if (i.Selected)
                    Sele++;

            if (Sele > 0)
            {
                if (MsgElimina("le " + Sele + " macro aree selezionate"))
                {
                Restart:
                    foreach (cGroupList i in pArea.Controls)
                        if (i.Selected)
                        {
                            if (i.Count > 0)
                            {
                                string[] s = new string[i.Count];

                                for (int x = 0; x < i.Count; x++)
                                    s[x] = i.Items[x].ToString();

                                gNoCat.AddRange(s);
                            }

                            pArea.Controls.Remove(i);

                            goto Restart;
                        }
                }
            }
            else
            {
                cGB.MsgBox("Seleziona una macro area cliccando sul nome", MessageBoxIcon.Exclamation);
            }
        }

        private void bSalva_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Application.DoEvents();

            List<DB.DataWrapper.cMovimenti.sMacroArea_e_Descrizione> m = new List<DB.DataWrapper.cMovimenti.sMacroArea_e_Descrizione>();

            foreach (cGroupList i in pArea.Controls)
                for (int x = 0; x < i.Count; x++)
                    m.Add(new DB.DataWrapper.cMovimenti.sMacroArea_e_Descrizione()
                    {
                        MacroArea = i.Text,
                        Descrizione = i.Items[x].ToString()
                    });

            if (m.Count > -1)
            {
                DB.DataWrapper.cMovimenti mov = new DB.DataWrapper.cMovimenti();
                int j = mov.AggiornaMacroAree(m);

                cGB.MsgBox("Aggiornati " + j + " elementi!");
            }

            this.Enabled = true;
        }


    }
}