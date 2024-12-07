select distinct
	descrizione
from Movimenti
where
	length(descrizione) > 0
	and descrizione like @descrizione
order by
	1