/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Windows.Forms;
using RationesCurare7.GB;
using RationesCurare7.UI.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cCalendario : cMyUserControl
    {

        public cCalendario()
        {
            InitializeComponent();
        }

        private void bMeseSuccessivo_Click(object sender, EventArgs e)
        {
            cCalendar1.GotoDate(cCalendar.eGotoDate.MeseSuccessivo);
        }

        private void bVaiAdOggi_Click(object sender, EventArgs e)
        {
            cCalendar1.GotoDate_Now();
        }

        private void bVaiA_Click(object sender, EventArgs e)
        {
            cCalendar1.GotoDate_Choose();
        }

        private void bMesePrecedente_Click(object sender, EventArgs e)
        {
            cCalendar1.GotoDate(cCalendar.eGotoDate.MesePrecedente);
        }

        private void bNuovo_Click(object sender, EventArgs e)
        {
            Dettaglio(true);
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            Dettaglio(false);
        }

        private string SelezionaElemento()
        {
            var s = "";
            cComboItem sel = null;

            if (cCalendar1.SelectedID.Count == 1)
            {
                var idj = cCalendar1.SelectedID[0];
                DB.DataWrapper.cCalendario cal = new DB.DataWrapper.cCalendario(idj);
                sel = new cComboItem(idj, cal.Descrizione);
            }
            else if (cCalendar1.SelectedID.Count > 1)
            {
                var idj = "";
                var ite = new cComboItem[cCalendar1.SelectedID.Count];

                for (var i = 0; i < cCalendar1.SelectedID.Count; i++)
                {
                    idj = cCalendar1.SelectedID[i];
                    DB.DataWrapper.cCalendario cal = new DB.DataWrapper.cCalendario(idj);
                    ite[i] = new cComboItem(idj, cal.Descrizione);
                }

                using (var ev = new fSceltaEvento())
                {
                    ev.Elementi = ite;

                    if (ev.ShowDialog() == DialogResult.OK)
                        sel = ev.Selezionato;
                }
            }

            if (sel != null)
                s = sel.ID;

            return s;
        }

        private bool ControllaDataSelezionata()
        {
            var b = false;

            if (cCalendar1.SelectedDate.Month == cCalendar1.CurDate.Month)
                if (cCalendar1.SelectedDate.Year == cCalendar1.CurDate.Year)
                    b = true;

            if (!b)
                cGB.MsgBox("Non puoi modificare le date che non fanno parte del mese di " + cCalendar1.CurDate.ToString("MMMM"), MessageBoxIcon.Exclamation);

            return b;
        }

        private void Dettaglio(bool nuovo)
        {
            if (ControllaDataSelezionata())
            {
                var k = "";
                var ModificaSerie = false;

                if (!nuovo)
                {
                    k = SelezionaElemento();
                    var cal = new DB.DataWrapper.cCalendario(k);

                    if (cal.IDGruppo != "")
                        using (var fst = new fSceltaTipoModificaCalendario())
                        {
                            fst.ShowDialog();
                            ModificaSerie = fst.Tipo == fSceltaTipoModificaCalendario.eTipo.Serie;
                        }
                }

                if (k != "" && !nuovo || nuovo)
                {
                    var NewType = true;

                    if (k.Length > 0)
                        if (k[0] == 'A')
                            NewType = false;

                    if (NewType)
                    {
                        using (var c = new fInserimentoCalendario2())
                        {
                            if (!nuovo)
                                c.ID_ = k;

                            c.DataSelezionata = cCalendar1.SelectedDate;

                            if (c.ShowDialog() == DialogResult.OK)
                                cCalendar1.LoadDays();
                        }
                    }
                    else
                    {
                        using (var c = new fInserimentoCalendario())
                        {
                            if (!nuovo)
                                c.ID_ = k;

                            c.ModificaSerie = ModificaSerie;
                            c.DataSelezionata = cCalendar1.SelectedDate;

                            if (c.ShowDialog() == DialogResult.OK)
                                cCalendar1.LoadDays();
                        }
                    }
                }
            }
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            if (ControllaDataSelezionata())
            {
                var i = SelezionaElemento();

                if (i != "")
                {
                    var cal = new DB.DataWrapper.cCalendario(i);

                    if (cal.ID != "")
                    {
                        var ModificaSerie = false;

                        if (cal.IDGruppo != "")
                            using (var fst = new fSceltaTipoModificaCalendario())
                            {
                                fst.ShowDialog();
                                ModificaSerie = fst.Tipo == fSceltaTipoModificaCalendario.eTipo.Serie;
                            }

                        var m = new DB.DataWrapper.cCalendario();

                        if (ModificaSerie)
                            m.EliminaSerie(cal.IDGruppo);
                        else
                            m.Elimina(cal.ID);

                        cCalendar1.LoadDays();
                    }
                }
            }
        }


    }
}