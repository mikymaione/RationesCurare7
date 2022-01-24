/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using RationesCurare7.DB;
using RationesCurare7.DB.DataWrapper;
using RationesCurare7.GB;
using RationesCurare7.UI.Forms;

namespace RationesCurare7
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (cGB.ControllaGiaInEsecuzione_SeContinuare())
            {
            Inizio:
                cGB.DoTheAutoUpdate();

                try
                {
                    cGB.RestartMe = false;
                    cGB.sPC = new cDB(false, cDB.DataBase.SQLite, cGB.PathDBUtenti);
                }
                catch (Exception ex)
                {
                    cGB.MsgBox("Errore apertura DB: " + ex.Message);
                }

                var CaricaQuestoIDUtente = -1;
                if (cGB.Parametri != null && !cGB.Parametri.Equals("") && File.Exists(cGB.Parametri)) //se fai doppio click su un file
                {
                    var u = new cUtenti();
                    u.CaricaByPath(cGB.Parametri);

                    CaricaQuestoIDUtente = u.ID;

                    if (u.ID < 0)
                        using (var du = new fDettaglioUtente())
                        {
                            du.DBPath = cGB.Parametri;
                            du.ShowDialog();
                        }
                }

                var ok = false;
                if (CaricaQuestoIDUtente > -1)
                {
                    var u = new cUtenti(CaricaQuestoIDUtente);

                    if (u.ID > -1)
                    {
                        var ute = new cDBInfo(u.Email);

                        using (var psw = new fPsw(ute.Email, ute.Psw))
                            ok = psw.ShowDialog() == DialogResult.OK;
                    }
                }
                else
                {
                    try
                    {
                        using (var fus = new fListaUtenti())
                        {
                            ok = fus.ShowDialog() == DialogResult.OK;

                            if (ok)
                                CaricaQuestoIDUtente = fus.IDUtente;
                        }
                    }
                    catch (Exception ex)
                    {
                        cGB.MsgBox("Errore lista utenti: " + ex.Message);
                    }
                }

                if (ok)
                {
                    cGB.DatiDBFisico = new cUtenti(CaricaQuestoIDUtente);

                    cGB.sDB = new cDB(true, cDB.DataBase.SQLite);

                    cGB.DatiUtente = new cDBInfo(cGB.DatiDBFisico.Email);

                    cGB.ControllaDBSulServer();

                    if (cGB.sDB.Connessione.State == ConnectionState.Closed)
                        cGB.sDB.Connessione.Open();

                    cGB.initCulture();
                    Application.CurrentCulture = cGB.valutaCorrente;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
                    //System.Threading.Thread.CurrentThread.CurrentCulture = cGB.valutaCorrente;
                    //System.Threading.Thread.CurrentThread.CurrentUICulture = cGB.valutaCorrente;

                    using (cGB.RationesCurareMainForm = new fMain())
                    {
                        Application.Run(cGB.RationesCurareMainForm);

                        if (cGB.RestartMe)
                            goto Inizio;

                        //if (!cGB.IAmInDebug)
                        if (cGB.DatiUtente.SincronizzaDB)
                        {
                            cGB.sDB.SQLiteVacuum();

                            var mc = new MikyCloud(cGB.DatiDBFisico.Path, cGB.DatiUtente.Email, cGB.DatiUtente.Psw);
                            mc.MandaDBSulSito(cGB.sDB.UltimaModifica);
                        }
                    }
                }
            }
        }

    }
}