CREATE TABLE Valute (
	Valuta VARCHAR(3) PRIMARY KEY COLLATE NOCASE NOT NULL,
	Descrizione VARCHAR(800) COLLATE NOCASE NOT NULL
);

-- User
CREATE TABLE Calendario (
	ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	Giorno VARCHAR(19) COLLATE NOCASE NOT NULL,
	Descrizione VARCHAR(255) COLLATE NOCASE NOT NULL,
	IDGruppo VARCHAR(36) COLLATE NOCASE,
	Inserimento VARCHAR(19) COLLATE NOCASE NOT NULL
);

CREATE TABLE Casse (
	Nome VARCHAR(20) PRIMARY KEY COLLATE NOCASE NOT NULL,
	ImgName BLOB,
	Nascondi boolean	
);

CREATE TABLE MovimentiTempo (
	ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	Nome VARCHAR(255) COLLATE NOCASE NOT NULL,
	Tipo VARCHAR(20) COLLATE NOCASE NOT NULL,
	Descrizione VARCHAR(255) COLLATE NOCASE NOT NULL,
	MacroArea VARCHAR(250) COLLATE NOCASE NOT NULL,
	Soldi DOUBLE NOT NULL,
	NumeroGiorni INTEGER,
	GiornoDelMese VARCHAR(19) COLLATE NOCASE,
	TipoGiorniMese VARCHAR(1) COLLATE NOCASE,
	PartendoDalGiorno VARCHAR(19) COLLATE NOCASE,
	Scadenza VARCHAR(19) COLLATE NOCASE,
	
	FOREIGN KEY (Tipo) REFERENCES Casse(Nome)
);

CREATE TABLE Movimenti (
	ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	Nome VARCHAR(255) COLLATE NOCASE NOT NULL,
	Data VARCHAR(19) COLLATE NOCASE NOT NULL,
	Tipo VARCHAR(20) COLLATE NOCASE NOT NULL,
	Descrizione TEXT COLLATE NOCASE NOT NULL,
	MacroArea VARCHAR(250) COLLATE NOCASE NOT NULL,
	Soldi DOUBLE NOT NULL,
	IDGiroconto INTEGER,
	IDMovimentoTempo INTEGER,
	
	FOREIGN KEY (Tipo) REFERENCES Casse(Nome),
	FOREIGN KEY (IDGiroconto) REFERENCES Movimenti(ID),
	FOREIGN KEY (IDMovimentoTempo) REFERENCES MovimentiTempo(ID)
);

CREATE TABLE DBInfo (
	Email VARCHAR(80) PRIMARY KEY COLLATE NOCASE NOT NULL,
	Nome VARCHAR(255) COLLATE NOCASE NOT NULL,
	Psw VARCHAR(80) COLLATE NOCASE NOT NULL,
	
	SincronizzaDB boolean,
	
	UltimaModifica VARCHAR(19) COLLATE NOCASE NOT NULL,	
	UltimoAggiornamentoDB VARCHAR(19) COLLATE NOCASE,
	
	Valuta	VARCHAR(3) NOT NULL COLLATE NOCASE,	
	FOREIGN KEY(Valuta) REFERENCES Valute(Valuta)
);
-- User

-- RC
CREATE TABLE Utenti (
	ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,	
	
	Email VARCHAR(80) COLLATE NOCASE NOT NULL,
	Nome VARCHAR(255) COLLATE NOCASE NOT NULL,	
	Psw VARCHAR(80) COLLATE NOCASE NOT NULL,
	
	Path TEXT COLLATE NOCASE NOT NULL,
	TipoDB VARCHAR(1) COLLATE NOCASE NOT NULL
);
-- RC