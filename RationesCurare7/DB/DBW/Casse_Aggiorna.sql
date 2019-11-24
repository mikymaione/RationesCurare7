update Casse set
	nome = @nomenuovo,
	imgName = @imgName,
	Valuta = @Valuta,
	Nascondi = @Nascondi
where
	nome = @nome
	