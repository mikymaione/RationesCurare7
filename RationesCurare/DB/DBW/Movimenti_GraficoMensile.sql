SELECT
	strftime('%Y/%m', data) AS Mese,  
	SUM(soldi) AS Soldini_TOT	
FROM movimenti
WHERE
	(MacroArea like @MacroArea)
	AND (data BETWEEN @dataDa AND @dataA)
GROUP BY
	1
ORDER BY
	1