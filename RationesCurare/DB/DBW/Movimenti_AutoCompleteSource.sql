select distinct
	descrizione
from Movimenti
where
	length(descrizione) > 0
order by
	1