/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RationesCurare7.UI.Controlli
{
    public partial class cCalendar : UserControl
    {

        public enum eGotoDate
        {
            MeseSuccessivo,
            MesePrecedente
        }

        private List<DB.DataWrapper.cCalendario> CalendarioDB = null;
        public DateTime CurDate = DateTime.Now;
        public DateTime SelectedDate = DateTime.Now;
        public List<string> SelectedID = new List<string>();

        public cCalendar()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                GotoDate_Now();
        }

        public void GotoDate(eGotoDate e)
        {
            if (e == eGotoDate.MeseSuccessivo)
                GotoDate(CurDate.AddMonths(1));
            else if (e == eGotoDate.MesePrecedente)
                GotoDate(CurDate.AddMonths(-1));
        }

        public void GotoDate_Choose()
        {
            using (var fds = new Forms.fDateSelector())
                if (fds.ShowDialog() == DialogResult.OK)
                    GotoDate(fds.ChoosedDate);
        }

        public void GotoDate_Now()
        {
            GotoDate(DateTime.Now);
        }

        public void GotoDate(DateTime t)
        {
            CurDate = t;
            LoadDays();
        }

        public void LoadDays()
        {
            var n = 0;
            var inizioMese = new DateTime(CurDate.Year, CurDate.Month, 1);
            var giorno = inizioMese.DayOfWeek;
            var m = CurDate.ToString("MMMM", System.Threading.Thread.CurrentThread.CurrentUICulture);
            m = System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.ToTitleCase(m);

            SelectedID = new List<string>();

            lMese.Text = m + " " + CurDate.Year;

            Carica(CurDate);

            switch (giorno)
            {
                case DayOfWeek.Monday: n = 1; break;
                case DayOfWeek.Tuesday: n = 2; break;
                case DayOfWeek.Wednesday: n = 3; break;
                case DayOfWeek.Thursday: n = 4; break;
                case DayOfWeek.Friday: n = 5; break;
                case DayOfWeek.Saturday: n = 6; break;
                case DayOfWeek.Sunday: n = 7; break;
            }

            var c = inizioMese.AddDays(n * -1);

            SetDays(fl0, ref c);
            SetDays(fl1, ref c);
            SetDays(fl2, ref c);
            SetDays(fl3, ref c);
            SetDays(fl4, ref c);
            SetDays(fl5, ref c);
        }

        private bool SameDay(DateTime a, DateTime b)
        {
            return (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day);
        }

        private List<DB.DataWrapper.cCalendario> GetInfoFromDB(DateTime d)
        {
            var it = new List<DB.DataWrapper.cCalendario>();

            for (var i = 0; i < (CalendarioDB?.Count ?? 0); i++)
                if (SameDay(CalendarioDB[i].Giorno, d))
                    it.Add(CalendarioDB[i]);

            return it;
        }

        private void SetDays(Control fp, ref DateTime c)
        {
            string h, db;
            var Holy = "";
            var IsHoliday = false;
            var i = fp.Controls.Count;

            for (var zi = 0; zi < fp.Controls.Count; zi++)
            {
                i--;

                if (fp.Controls[i] is cCalendarItem)
                {
                    c = c.AddDays(1);
                    IsHoliday = cGB.IsHoliday(c);

                    if (IsHoliday)
                        Holy = cGB.HolidayName(c);

                    ((cCalendarItem)fp.Controls[i]).ResetVar();
                    ((cCalendarItem)fp.Controls[i]).lGiorno.Text = Convert.ToString(c.Day);

                    var cal = GetInfoFromDB(c);

                    h = (IsHoliday ? "● " + Holy : "");

                    if (cal.Count > 0)
                    {
                        var db1 = "";
                        db = "";

                        for (var y = 0; y < cal.Count; y++)
                        {
                            db1 = "● " + cal[y].Descrizione;
                            db += db1 + Environment.NewLine;

                            if (db1 == h)
                                h = "";

                            ((cCalendarItem)fp.Controls[i]).ID_.Add(cal[y].ID);
                        }
                    }
                    else
                    {
                        db = "";
                    }

                    if (db != "")
                        if (h != "")
                            h += Environment.NewLine;

                    ((cCalendarItem)fp.Controls[i]).lTesto.Text = (h + db);
                    ((cCalendarItem)fp.Controls[i]).MyDate = c;

                    ((cCalendarItem)fp.Controls[i]).BackCalendarColor =
                        (c.Month == CurDate.Month ?
                            (IsHoliday ?
                                Color.FromArgb(244, 194, 194) :
                                Color.FromArgb(242, 242, 242)
                            ) :
                            (IsHoliday ?
                                Color.FromArgb(219, 169, 169) :
                                Color.LightGray
                            )
                        );

                    ((cCalendarItem)fp.Controls[i]).BackColor =
                        ((c.Year == DateTime.Now.Year) && (c.Month == DateTime.Now.Month) && (c.Day == DateTime.Now.Day) ?
                            Color.Lime :
                            Color.White
                        );

                    if (SelectedDate == ((cCalendarItem)fp.Controls[i]).MyDate)
                        ((cCalendarItem)fp.Controls[i]).OnClickEvent();
                }
            }
        }

        public void SizeAll()
        {
            this.Visible = false;

            var w = this.Width / 7;
            var h = (this.Height - pDays.Height - pMonth.Height) / 6;

            foreach (Panel p in this.Controls)
            {
                if (!p.Tag.Equals("0"))
                    p.Height = h;

                foreach (Control d in p.Controls)
                    if (d is cCalendarItem || d is Label)
                        d.Width = w;
            }

            this.Visible = true;
        }

        private void cCalendar_SizeChanged(object sender, EventArgs e)
        {
            SizeAll();
        }

        private void Carica(DateTime Data_)
        {
            var da = new DateTime(Data_.Year, Data_.Month, 1);
            var a = new DateTime(Data_.Year, Data_.Month, DateTime.DaysInMonth(Data_.Year, Data_.Month));

            da = da.AddDays(-15);
            a = a.AddDays(15);

            var c1 = new DB.DataWrapper.cCalendario();
            CalendarioDB = c1.Ricerca(da, a);
        }

        private void cCalendarItem_ClickEvent(cCalendarItem sender)
        {
            for (var x = 0; x < sender.ID_.Count; x++)
                if (!SelectedID.Contains(sender.ID_[x]))
                    SelectedID.Add(sender.ID_[x]);

            SelectedDate = sender.MyDate;

            //clear all
            foreach (Panel p in this.Controls)
                foreach (Control d in p.Controls)
                    if (d is cCalendarItem)
                        if (sender.Name != d.Name)
                            ((cCalendarItem)d).Selected = false;
        }


    }
}