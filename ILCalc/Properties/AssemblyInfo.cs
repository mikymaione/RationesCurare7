/*
ILCalc - Arithmetical expressions compiler and evaluator.
Copyright (C) Shvedov A. V. © 2008-2015
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/

using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;

[assembly: AssemblyTitle("ILCalc")]
[assembly: AssemblyDescription("Arithmetical expressions compiler and evaluator.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Pelmen Software")]
[assembly: AssemblyProduct("ILCalc")]
[assembly: AssemblyCopyright("Shvedov A. V. © 2008-2018")]
[assembly: AssemblyTrademark("Pelmen Software")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: System.CLSCompliant(true)]

[assembly: AssemblyVersion("20.18.04.11")]

#if !CF

[assembly: AssemblyFileVersion("20.18.04.11")]
[assembly: AssemblyFlags(
  AssemblyNameFlags.EnableJITcompileOptimizer)]

[assembly:
  NeutralResourcesLanguage("")]

#else

[assembly: NeutralResourcesLanguage("en")]

#endif
