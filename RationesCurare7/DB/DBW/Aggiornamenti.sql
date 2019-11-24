--20170211;
alter table utenti add UltimaModifica datetime;
alter table utenti add UltimoAggiornamentoDB datetime;

--20191124;
alter table Casse add Nascondi boolean;
update Casse set Nascondi = false;