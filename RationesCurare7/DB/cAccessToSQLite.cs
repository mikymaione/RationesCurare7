/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2015 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace RationesCurare7.DB
{
    public class cAccessToSQLite
    {

        public bool Converti(String Da, String A)
        {
            var SA = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Da + ";";
            var SS = "Version=3;Data Source=" + A + ";";

            using (System.Data.OleDb.OleDbConnection connA = new System.Data.OleDb.OleDbConnection(SA))
            using (var connS = 
            #if __MonoCS__
                new Mono.Data.Sqlite.SqliteConnection(SS)
            #else 
                new System.Data.SQLite.SQLiteConnection(SS)
            #endif
            )
            {                
                connA.Close();
                connS.Close();
            }                            

            return true;
        }

    }
}