SELECT  
    strftime('%Y', data) AS Anno,    
    SUM(SUM(m.soldi)) OVER (ORDER BY strftime('%Y', data)) AS Soldini_TOT
FROM movimenti m
GROUP BY
    Anno
ORDER BY
    Anno