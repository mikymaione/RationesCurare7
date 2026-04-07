SELECT
	Mese,
	Entrate,
	Uscite,
	Entrate + Uscite AS Saldo,
	100.0 * (Entrate + Uscite) / Entrate AS PCT
FROM (

	SELECT
		Mese,
		SUM(Entrate) AS Entrate,
		SUM(Uscite) AS Uscite
	FROM (
		SELECT
			STRFTIME('%Y/%m', data) AS Mese,  
			SUM(soldi) AS Entrate,
			0 AS Uscite
		FROM movimenti
		WHERE
			soldi > 0
			AND MacroArea like @MacroArea
			AND data BETWEEN @dataDa AND @dataA
		GROUP BY
			Mese

		UNION ALL

		SELECT
			STRFTIME('%Y/%m', data) AS Mese,
			0 AS Entrate,
			SUM(soldi) AS Uscite
		FROM movimenti
		WHERE
			soldi < 0
			AND MacroArea like @MacroArea
			AND data BETWEEN @dataDa AND @dataA
		GROUP BY
			Mese

	)
	GROUP BY
		Mese

)