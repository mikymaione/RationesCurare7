/*
RationesCurare - Gestione piccola contabilità
Copyright (C) 2024 [MAIONE MIKΨ]
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version. 
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/. 
*/
using RationesCurare7.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.SessionState;

namespace RationesCurare
{
    public class GestioneValute
    {

        private static readonly Dictionary<RegionInfo, CultureInfo> RegionInfos =
            CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .ToDictionary(
                    culture => new RegionInfo(culture.Name),
                    culture => culture
                );

        public static readonly List<LanguageCodeDescription> Currencies =
            RegionInfos
                .GroupBy(pair => pair.Key.ISOCurrencySymbol)
                .Select(g => g.First())
                .Select(pair => new LanguageCodeDescription
                {
                    code = pair.Key.ISOCurrencySymbol,
                    description = pair.Key.CurrencyEnglishName,
                })
                .OrderBy(e => e.description)
                .ToList();

        public static readonly List<LanguageCodeDescription> LanguageCurrencies =
            RegionInfos
                .Select(pair => new LanguageCodeDescription
                {
                    code = pair.Value.Name,
                    description = $"{pair.Value.EnglishName} - {pair.Key.CurrencyEnglishName}",
                })
                .OrderBy(e => e.description)
                .ToList();

        public static List<RegionInfo> GetRegionInfoByLingua(string language) =>
            RegionInfos
                .Where(pair => pair.Value.Name.Equals(language, StringComparison.InvariantCultureIgnoreCase))
                .Select(pair => pair.Key)
                .ToList();

        public static List<CultureInfo> GetCultureByValuta(string valuta) =>
            RegionInfos
                .Where(pair => pair.Key.ISOCurrencySymbol.Equals(valuta, StringComparison.InvariantCultureIgnoreCase))
                .Select(pair => pair.Value)
                .ToList();

        public static List<LanguageCodeDescription> GetLanguageCurrenciesByValuta(string valuta) =>
            GetCultureByValuta(valuta)
                .Select(c => new LanguageCodeDescription
                {
                    code = c.Name,
                    description = c.EnglishName,
                })
                .OrderBy(e => e.description)
                .ToList();

        public static List<CultureInfo> GetCultureByLanguage(string language) =>
            CultureInfo
               .GetCultures(CultureTypes.SpecificCultures)
               .Where(c => c.Name.Equals(language, StringComparison.InvariantCultureIgnoreCase))
               .ToList();

        public static CultureInfo LeggiValutaInDbInfo(HttpSessionState session, string email)
        {
            var maybeValuta = "";
            var maybeLanguage = "";

            var p = new System.Data.SQLite.SQLiteParameter[] {
                cDB.NewPar("Email", email)
            };

            using (var db = new cDB(GB.Instance.getCurrentSession(session).PathDB))
            using (var dr = db.EseguiSQLDataReader(cDB.Queries.DBInfo_Dettaglio, p))
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        maybeValuta = Convert.ToString(dr["Valuta"]);
                        maybeLanguage = Convert.ToString(dr["Lingua"]);
                    }

            var valuta = string.IsNullOrEmpty(maybeValuta) ? "EUR" : maybeValuta;
            var language = string.IsNullOrEmpty(maybeLanguage) ? "it-IT" : maybeLanguage;

            var culturesByValuta = GetCultureByValuta(valuta);
            var culturesByLanguage = GetCultureByLanguage(language);

            if (culturesByLanguage.Count > 0)
                return culturesByLanguage[0];
            else if (culturesByValuta.Count > 0)
                return culturesByValuta[0];
            else
                return CultureInfo.CurrentCulture;
        }

    }
}