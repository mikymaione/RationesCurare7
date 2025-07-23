SELECT  	
	SUM(soldi) as Soldini_TOT 
FROM movimenti
WHERE
	(MacroArea like :MacroArea) and
	data BETWEEN @dataDa AND @dataA