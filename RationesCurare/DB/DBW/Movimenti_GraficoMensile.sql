SELECT  
	Format(m.data, 'yyyy/mm') as Mese,  
	sum(m.soldi) as Soldini_TOT	
FROM movimenti m  
group by  
	Format(m.data, 'yyyy/mm')