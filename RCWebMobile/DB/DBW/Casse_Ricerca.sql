select
	*
from Casse
where
	Nascondi <> 1 and
	Nome not in ('Cassaforte', 'Saldo', 'Avere', 'Dare', 'Salvadanaio', 'Portafogli')
order by
	Nome