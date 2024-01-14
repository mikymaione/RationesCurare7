select
	Nome,
	CASE Nascondi
		WHEN 0  THEN 'Sì' ELSE 'No' 
	END NascondiS
from Casse
order by
	Nome