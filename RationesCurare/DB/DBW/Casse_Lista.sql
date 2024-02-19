select
	Nome,
	CASE Nascondi
		WHEN 0  THEN 'No' ELSE 'Sì'
	END NascondiS
from Casse
order by
	Nome