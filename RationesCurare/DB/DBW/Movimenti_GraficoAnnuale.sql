SELECT  
	strftime('%Y', data) AS Mese,  	
	SUM(soldi) AS Soldini_TOT	
FROM movimenti
WHERE
	(MacroArea like :MacroArea) and
	data BETWEEN @dataDa AND @dataA
GROUP BY
	Mese