select distinct
	MacroArea,
	descrizione
from Movimenti
where
	length(MacroArea) > 0
order by
	1, 2