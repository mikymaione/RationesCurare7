SELECT  
	m.MacroArea,
	sum(m.soldi) as Soldini_TOT 
FROM movimenti m  
group by  
	m.MacroArea
order by
	sum(m.soldi)	