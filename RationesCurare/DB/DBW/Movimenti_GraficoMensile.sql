SELECT
	strftime('%Y/%m', data) as Mese,  
	sum(soldi) as Soldini_TOT	
FROM movimenti
group by
	Mese