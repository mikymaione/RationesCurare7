/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
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
                    cGB.OpzioniProgramma = cOpzioniProgramma.Carica();

                    DB.cDB.ApriConnessione(DB.cDB.DataBase.SQLite, cGB.PathDBUtenti, true);
                }
                catch (Exception ex)
                {
                    cGB.MsgBox("Errore apertura DB: " + ex.Message);
                }

                var CaricaQuestoIDUtente = -1;
                if (cGB.Parametri != null && !cGB.Parametri.Equals("") && System.IO.File.Exists(cGB.Parametri)) //se fai doppio click su un file
                {
                    var u = new DB.DataWrapper.cUtente();
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
                    var u = new DB.DataWrapper.cUtente(CaricaQuestoIDUtente);

                    if (u.ID > -1)
                        using (var psw = new UI.Forms.fPsw(u.Email, u.psw))
                            ok = (psw.ShowDialog() == DialogResult.OK);

                    if (ok)
                        cGB.UtenteConnesso = new cGB.sUtente()
                        {
                            PathDB = u.path,
                            UserName = u.nome,
                            ID = u.ID,
                            Psw = u.psw,
                            Email = u.Email,
                            TipoDB = u.TipoDB
                        };
                }
                else
                {
                    try
                    {
                        using (var fus = new UI.Forms.fListaUtenti())
                            ok = (fus.ShowDialog() == DialogResult.OK);
                    }
                    catch (Exception ex)
                    {
                        cGB.MsgBox("Errore lista utenti: " + ex.Message);
                    }
                }

                if (ok)
                {
                    cGB.ControllaDBSulServer();

                    DB.cDB.ApriConnessione(cGB.UtenteConnesso.TipoDB.Equals("S") ? DB.cDB.DataBase.SQLite : DB.cDB.DataBase.Access, true);

                    using (cGB.RationesCurareMainForm = new UI.Forms.fMain())
                    {
                        Application.Run(cGB.RationesCurareMainForm);

                        cGB.UtenteConnesso.ID = -1;
                        if (cGB.RestartMe)
                            goto Inizio;

                        //if (!cGB.IAmInDebug)
                        if (cGB.OpzioniProgramma.SincronizzaDB)
                        {
                            var mc = new GB.MikyCloud(cGB.UtenteConnesso);
                            mc.MandaDBSulSito(DB.cDB.UltimaModifica);
                        }

                        DB.cDB.ApriConnessione(DB.cDB.DataBase.SQLite, cGB.PathDBUtenti, true);

                        var ca = new DB.DataWrapper.cAggiornamenti();
                        ca.AggiornaDataDB(DB.cDB.UltimaModifica);

                        DB.cDB.ChiudiConnessione();
                    }
                }
            }
        }

    }
}