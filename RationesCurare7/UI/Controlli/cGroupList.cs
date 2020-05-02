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

namespace RationesCurare7.UI.Controlli
{
    public partial class cGroupList : UserControl
    {
        public bool Modificabile = true;
        private object[] Lista_ = null;

        public ListBox.ObjectCollection Items
        {
            get
            {
                return Lista.Items;
            }
        }

        public int Count
        {
            get
            {
                return Lista.Items.Count;
            }
        }

        public string Text
        {
            get
            {
                return G.Text;
            }
            set
            {
                G.Text = value;
            }
        }

        public bool Selected
        {
            get
            {
                return (Lista.BackColor == Color.Red);
            }
            set
            {
                if (Modificabile)
                    if (value)
                    {
                        Lista.BackColor = Color.Red;
                    }
                    else
                    {
                        Lista.BackColor = Color.White;
                    }
            }
        }

        public cGroupList()
        {
            InitializeComponent();
        }

        private void G_Click(object sender, System.EventArgs e)
        {
            if (Modificabile)
            {
                eNome.Text = Text;
                eNome.Visible = true;
                bOK.Visible = true;
                Selected = true;
            }
        }

        private void eNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (Modificabile)
                if (e.KeyCode == Keys.Enter)
                {
                    Text = eNome.Text;
                    eNome.Visible = false;
                    bOK.Visible = false;
                    Selected = false;
                }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Text = eNome.Text;
            eNome.Visible = false;
            bOK.Visible = false;
            Selected = false;
        }

        private void bDelFiltro_Click(object sender, EventArgs e)
        {
            eFiltro.Text = "";
            Lista.Items.Clear();

            try
            {
                Lista.Items.AddRange(Lista_);
            }
            catch
            {
                //lista is empty             
            }
        }

        private bool CompareLetters(string parola, string lettere)
        {
            int x = 0;

            for (int i = 0; i < lettere.Length; i++)
                if (parola.Length - 1 >= i)
                    if (parola[i].Equals(lettere[i]))
                        x++;

            return (x >= lettere.Length);
        }

        private void Filtra()
        {
            if (Lista.Items.Count > 0)
            {
                int x = -1;
                string[] h = new string[Lista.Items.Count];

                for (int i = 0; i < Lista.Items.Count; i++)
                    if (CompareLetters(Lista.Items[i].ToString(), eFiltro.Text))
                        h[x += 1] = Lista.Items[i].ToString();

                if (x > -1)
                {
                    string[] h2 = new string[x + 1];
                    Array.Copy(h, h2, x + 1);

                    Lista.Items.Clear();
                    Lista.Items.AddRange(h2);
                }
            }
        }

        public void AddRange(List<string> l)
        {
            if (l != null)
                if (l.Count > 0)
                {
                    int x = -1;
                    object[] o = new object[l.Count];

                    foreach (string s in l)
                        o[x += 1] = s;

                    if (x > -1)
                        AddRange(o);
                }
        }

        public void AddRange(object[] o)
        {
            Lista.Items.AddRange(o);
            Lista_ = o;
        }

        public void ClearSelected()
        {
            Lista.ClearSelected();
        }

        public void ClearItems()
        {
            Lista_ = null;
            Lista.Items.Clear();
        }

        private void eFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            Filtra();
        }


    }
}