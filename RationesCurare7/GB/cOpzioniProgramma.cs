/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare7
{
    [Serializable]
    public class cOpzioniProgramma
    {
        public bool FileSavedCorrectly = false;
        public bool FileReadedCorrectly = false;
        public bool SincronizzaDB = false;

        private static string FileOpzioni
        {
            get
            {
                var p = cGB.PathDBUtenti_Cartella;
                p = System.IO.Path.Combine(p, "Opzioni.db");

                return p;
            }
        }

        public void Salva()
        {
            FileSavedCorrectly = false;

            if (!System.IO.Directory.Exists(cGB.PathDBUtenti_Cartella))
                try
                {
                    System.IO.Directory.CreateDirectory(cGB.PathDBUtenti_Cartella);
                }
                catch
                {
                    //error                    
                }

            if (System.IO.Directory.Exists(cGB.PathDBUtenti_Cartella))
                try
                {
                    using (var fs = new System.IO.FileStream(FileOpzioni, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                    {
                        var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bf.Serialize(fs, this);
                    }

                    FileSavedCorrectly = true;
                }
                catch
                {
                    //error
                }
        }

        public static cOpzioniProgramma Carica()
        {
            if (System.IO.File.Exists(FileOpzioni))
                try
                {
                    using (var fs = new System.IO.FileStream(FileOpzioni, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        var t = (cOpzioniProgramma)bf.Deserialize(fs);

                        t.FileReadedCorrectly = true;

                        return t;
                    }
                }
                catch
                {
                    //error
                }

            return new cOpzioniProgramma();
        }


    }
}