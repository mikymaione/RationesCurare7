SELECT  	
	SUM(soldi) as Soldini_TOT 
FROM movimenti
WHERE
	data BETWEEN @dataDa AND @dataA