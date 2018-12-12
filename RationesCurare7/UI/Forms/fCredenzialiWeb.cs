/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2018 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Windows.Forms;

namespace RationesCurare7.UI.Forms
{
#if DEBUG
    public partial class fCredenzialiWeb : Form
#else
        public partial class fCredenzialiWeb : fMyForm
#endif    
    {

        private string FileSelezionato_ = "";
        public string FileSelezionato
        {
            get
            {
                return FileSelezionato_;
            }
        }

        private string Psw_ = "";
        public string Psw
        {
            get
            {
                return Psw_;
            }
        }


        public fCredenzialiWeb()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            using (var ema = new maionemikyWS.EmailSending())
            {
                var r = ema.ControllaCredenzialiRC_simple(eEmail.Text, ePassword.Text);

                switch (r)
                {
                    case maionemikyWS.CredenzialiRisultato.TuttoOK:
                        using (var fd = new FolderBrowserDialog()
                        {
                            Description = "Cartella dove creare il DB dell'utente",
                            ShowNewFolderButton = true
                        })
                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                Psw_ = ePassword.Text;
                                FileSelezionato_ = System.IO.Path.Combine(fd.SelectedPath, eEmail.Text + ".rqd8");

                                var ok = cGB.ScaricaUltimoDBDalWeb(ema, "19000101000000", FileSelezionato, eEmail.Text, ePassword.Text, true);

                                if (ok)
                                    this.DialogResult = DialogResult.OK;
                            }
                        break;

                    case maionemikyWS.CredenzialiRisultato.Presente_PasswordErrata:
                        cGB.MsgBox("Password errata!", MessageBoxIcon.Exclamation);
                        break;

                    case maionemikyWS.CredenzialiRisultato.Assente:
                        cGB.MsgBox("Questo utente non esiste!", MessageBoxIcon.Exclamation);
                        break;

                    case maionemikyWS.CredenzialiRisultato.Errore:
                        cGB.MsgBox("Errore, riprovare.", MessageBoxIcon.Error);
                        break;

                    case maionemikyWS.CredenzialiRisultato.ProgrammaNonAutorizzato:
                        cGB.MsgBox("Programma non autorizzato!", MessageBoxIcon.Error);
                        break;
                }
            }
        }


    }
}