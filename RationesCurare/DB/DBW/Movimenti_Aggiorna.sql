update Movimenti set
	nome = @nome,
	tipo = @tipo,
	descrizione = @descrizione,
	soldi = @soldi,
	data = @data,
	MacroArea = @MacroArea,
	IDGiroconto = @IDGiroconto
where
	ID = @ID