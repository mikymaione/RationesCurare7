update utenti set
	nome = @nome,
	psw = @psw,
	path = @path,
	Email = @Email,	
	TipoDB = @TipoDB
where
	ID = @ID