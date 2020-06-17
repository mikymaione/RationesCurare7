/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

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
                    cGB.sPC = new DB.cDB(false, DB.cDB.DataBase.SQLite, cGB.PathDBUtenti);
                }
                catch (Exception ex)
                {
                    cGB.MsgBox("Errore apertura DB: " + ex.Message);
                }

                var CaricaQuestoIDUtente = -1;
                if (cGB.Parametri != null && !cGB.Parametri.Equals("") && System.IO.File.Exists(cGB.Parametri)) //se fai doppio click su un file
                {
                    var u = new DB.DataWrapper.cUtenti();
                    u.CaricaByPath(cGB.Parametri);

                    CaricaQuestoIDUtente = u.ID;

                    if (u.ID < 0)
                        using (var du = new UI.Forms.fDettaglioUtente())
                        {
                            du.DBPath = cGB.Parametri;
                            du.ShowDialog();
                        }
                }

                var ok = false;
                if (CaricaQuestoIDUtente > -1)
                {
                    var u = new DB.DataWrapper.cUtenti(CaricaQuestoIDUtente);

                    if (u.ID > -1)
                    {
                        var ute = new DB.DataWrapper.cDBInfo(u.Email);

                        using (var psw = new UI.Forms.fPsw(ute.Email, ute.Psw))
                            ok = (psw.ShowDialog() == DialogResult.OK);
                    }
                }
                else
                {
                    try
                    {
                        using (var fus = new UI.Forms.fListaUtenti())
                        {
                            ok = (fus.ShowDialog() == DialogResult.OK);

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
                    cGB.DatiDBFisico = new DB.DataWrapper.cUtenti(CaricaQuestoIDUtente);

                    cGB.sDB = new DB.cDB(true, DB.cDB.DataBase.SQLite);

                    cGB.DatiUtente = new DB.DataWrapper.cDBInfo(cGB.DatiDBFisico.Email);

                    cGB.ControllaDBSulServer();

                    if (cGB.sDB.Connessione.State == System.Data.ConnectionState.Closed)
                        cGB.sDB.Connessione.Open();

                    cGB.initCulture();
                    Application.CurrentCulture = cGB.valutaCorrente;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InstalledUICulture;
                    //System.Threading.Thread.CurrentThread.CurrentCulture = cGB.valutaCorrente;
                    //System.Threading.Thread.CurrentThread.CurrentUICulture = cGB.valutaCorrente;

                    using (cGB.RationesCurareMainForm = new UI.Forms.fMain())
                    {
                        Application.Run(cGB.RationesCurareMainForm);

                        if (cGB.RestartMe)
                            goto Inizio;

                        //if (!cGB.IAmInDebug)
                        if (cGB.DatiUtente.SincronizzaDB)
                        {
                            var mc = new GB.MikyCloud(cGB.DatiDBFisico.Path, cGB.DatiUtente.Email, cGB.DatiUtente.Psw);
                            mc.MandaDBSulSito(cGB.sDB.UltimaModifica);
                        }
                    }
                }
            }
        }

    }
}