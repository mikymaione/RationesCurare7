--20170211;
alter table utenti add UltimaModifica datetime;

alter table movimenti add MacroArea VARCHAR(250);
alter table movimenti alter column soldi CURRENCY;

alter table MovimentiTempo alter column soldi CURRENCY;
alter table MovimentiTempo add MacroArea VARCHAR(250);
alter table MovimentiTempo add Scadenza datetime;

alter table Calendario add IDGruppo VARCHAR (36);
alter table Calendario add Inserimento datetime;

update MovimentiTempo set descrizione = '' where descrizione is null;
update MovimentiTempo set MacroArea = '' where MacroArea is null;
update movimenti set MacroArea = '' where MacroArea is null;
update movimenti set descrizione = '' where descrizione is null;