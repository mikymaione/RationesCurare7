SELECT
	strftime('%Y/%m', data) AS Mese,  
	SUM(soldi) AS Soldini_TOT	
FROM movimenti
WHERE
	data BETWEEN @dataDa AND @dataA
GROUP BY
	Mese