select	
	c.*,
	v.Descrizione as ValutaD
from Casse c
left outer join Valute v on c.Valuta = v.Valuta
order by
	Nome