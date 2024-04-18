insert into Movimenti (
	nome, 
	tipo, 
	descrizione,
	soldi,
	data,
	MacroArea,
	IDGiroconto
) values (
	@nome, 
	@tipo, 
	@descrizione,
	@soldi,
	@data,
	@MacroArea,
	@IDGiroconto
)