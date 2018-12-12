SELECT 
	m.*,
	case 
		when m.TipoGiorniMese = 'M' then Datepart('d', m.GiornoDelMese)
		else null
	end as GiornoDelMese_H,
	case
		when m.TipoGiorniMese = 'G' then 'giorni'
		else 'mese'
	end as Periodo_H
FROM MovimentiTempo m
order by
	m.tipo, m.GiornoDelMese