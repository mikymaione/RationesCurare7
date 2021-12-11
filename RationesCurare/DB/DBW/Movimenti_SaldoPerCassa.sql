select
	m.Tipo,
	sum(m.soldi) as Saldo
from Movimenti m
inner join Casse c on
	c.Nome = m.Tipo
where
	c.Nascondi = 0
group by
	m.Tipo
order by
	m.Tipo