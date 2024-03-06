SELECT  
	PRINTF("%,.2f %s", sum(m.soldi), m.MacroArea) as Titolo,
	SUM(m.soldi) AS Soldini_TOT 
FROM movimenti m
WHERE
	m.Data BETWEEN @da and @a
GROUP BY  
	m.MacroArea
HAVING	
	Soldini_TOT <> 0
	AND Titolo <> ''
ORDER BY
	SUM(m.soldi) DESC