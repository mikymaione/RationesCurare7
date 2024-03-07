SELECT  	
	sum(m.soldi) as Soldini_TOT 
FROM movimenti m
WHERE
	m.Data BETWEEN @da and @a