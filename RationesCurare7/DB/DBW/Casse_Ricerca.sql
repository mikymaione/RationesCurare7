select	
	c.*,
	v.Descrizione as ValutaD
from Casse c
left outer join Valute v on c.Valuta = v.Valuta
where
	((@MostraTutte = 1) or (Nascondi = 0))
order by
	Nome