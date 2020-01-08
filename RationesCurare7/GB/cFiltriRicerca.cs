/*
RationesCurare7 - Gestione piccola contabilità
Copyright (C) 2008 [MAIONE MIKY]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System;

namespace RationesCurare7.GB
{
    public class cFiltriRicerca
    {
        public DateTime DataDa, DataA;
        public string Descrizione, Cassa, MacroAree;
        public double SoldiDa, SoldiA;
        public bool bData, bSoldi, bCassa, bDescrizione, bMacroAree;

        public cFiltriRicerca()
        {
            Descrizione = "%%";
            MacroAree = "%%";
            Cassa = "Saldo";
            bData = false;
            bSoldi = false;
            bCassa = false;
            bDescrizione = false;
            bMacroAree = false;
        }


    }
}