﻿/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2024 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using System.Threading;
using System.Web.UI;

namespace RationesCurare
{
    public class CulturePage : Page
    {
        protected override void InitializeCulture()
        {
            var cs = GB.Instance.getCurrentSession(Session);
            
            if (cs != null)
            {                
                var culture = cs.Culture;

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            
            base.InitializeCulture();
        }

    }
}