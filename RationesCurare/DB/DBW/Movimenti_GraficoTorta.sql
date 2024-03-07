SELECT
	m.MacroArea,
	SUM(m.soldi) AS Soldini_TOT 
FROM movimenti m
WHERE
	m.Data BETWEEN @da and @a
GROUP BY  
	m.MacroArea
HAVING	
	Soldini_TOT <> 0
	AND m.MacroArea <> ''