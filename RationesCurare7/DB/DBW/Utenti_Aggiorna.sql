update utenti set
	nome = @Nome,	
	Psw = @Psw,
	path = @Path,
	Email = @Email,	
	TipoDB = @TipoDB
where
	ID = @ID