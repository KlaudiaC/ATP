create database ProjectTenis 
use ProjectTenis
---------------------------------------------------------------------------------------------------------------------------------------------
create table ZAWODNICY(
Id_Zawodnika int identity(1, 1) primary key,
Nazwisko varchar(30) not null,
Imie varchar(30) not null,
Kraj varchar(30) not null, 
Aktywny bit not null,
constraint zawodnicy_ogr_nazw unique (Nazwisko)
);
-- drop table ZAWODNICY
-- truncate table ZAWODNICY
-- delete from ZAWODNICY
select * from ZAWODNICY

go
create procedure dodajZaw
	@Nazwisko varchar(30),
	@Imie varchar(30),
	@Kraj varchar(30),
	@Aktywny bit
as
	if (select count(*) from (select * from Zawodnicy where Nazwisko = @Nazwisko) SZ)  = 0
	insert into ZAWODNICY(Nazwisko, Imie, Kraj, Aktywny) values (@Nazwisko, @Imie, @Kraj, @Aktywny)
go
-- drop procedure dodajZaw
exec dbo.dodajZaw 'Murray', 'Andy', 'Wielka Brytania', 1
exec dbo.dodajZaw 'Connors', 'Jimmy', 'USA', 0
exec dbo.dodajZaw 'Lu', 'Yen-Hsun', 'Tajwan', 1
exec dbo.dodajZaw 'Federer', 'Roger', 'Szwajcaria', 1
exec dbo.dodajZaw 'Nadal', 'Rafael', 'Hiszpania', 1
exec dbo.dodajZaw 'Gonzalez', 'Fernando', 'Chile', 0
exec dbo.dodajZaw 'Sampras', 'Pete', 'USA', 0
exec dbo.dodajZaw 'Djokovic', 'Novak', 'Serbia', 1
exec dbo.dodajZaw 'del Potro', 'Juan Martin', 'Argentyna', 1
exec dbo.dodajZaw 'Wawrinka', 'Stan', 'Szwajcaria', 1
go
create procedure zmienAktywny
	@Nazwisko varchar(30)
as
update ZAWODNICY
set Aktywny = Aktywny * (-1) + 1 where Nazwisko = @Nazwisko
go
-- drop procedure zmienAktywny
-- exec zmienAktywny 'Connors'

go
create procedure usunZawodnika
	@Nazwisko varchar(30)
as
begin
	declare @ID int
	set @ID = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Nazwisko)
	update TRENERZY
		set
			IdZawodnika = null
		where IdZawodnika = @ID

	delete from RANKING
	where Id_Zawodnika = @ID

	delete from KARIERY
	where Id_Zawodnika = @ID

	update MASTERS_WYNIKI
	set
		I_SET = null,
		II_SET = null,
		III_SET = null,
		IdZwyciezcy = null,
		IdFinalisty = null
	where
		IdZwyciezcy = @ID or IdFinalisty = @ID

	update WIELKIE_SZLEMY_WYNIKI
	set
		I_SET = null,
		II_SET = null,
		III_SET = null,
		IV_SET = null,
		V_SET = null,
		IdZwyciezcy = null,
		IdFinalisty = null
	where
		IdZwyciezcy = @ID or IdFinalisty = @ID

	update OLIMPIADY_WYNIKI
	set
		I_SET = null,
		II_SET = null,
		III_SET = null,
		IV_SET = null,
		V_SET = null,
		Zloto = null,
		Srebro = null,
		Braz = null
	where
		Zloto = @ID or Srebro = @ID or Braz = @ID

	delete from ZAWODNICY
	where Id_Zawodnika = @ID
end
go
-- drop procedure usunZawodnika
-- exec dbo.usunZawodnika 'Federer'
---------------------------------------------------------------------------------------------------------------------------------------------
create table RANKING(
Id_Zawodnika int primary key not null,
Punkty int not null default 0,
constraint ranking_ogr_obcy foreign key(Id_Zawodnika) references[ZAWODNICY](Id_Zawodnika)
);
-- drop table RANKING
select * from RANKING

go
create view rankingView
as
    select R.Id_Zawodnika, Z.Imie, Z.Nazwisko, R.Punkty from ZAWODNICY Z right join RANKING R on Z.Id_Zawodnika = R.Id_Zawodnika where Aktywny = 1;
go
-- drop view rankingView
select * from rankingView

go
create function czyIstnieje(@Nazwisko varchar(30))
	returns bit
as
begin
	return (select count(*) from ZAWODNICY where Nazwisko = @Nazwisko);
end
go
-- drop function czyIstnieje
-- select dbo.czyIstnieje('Federer')

go
create procedure dodaj
	@Nazwisko varchar(30),
	@Punkty int
as
	if (select count(*) from (select * from Ranking where Id_zawodnika = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Nazwisko)) SR) = 0 
	if (select dbo.czyIstnieje(@Nazwisko)) = 1
	if (select count(*) from ZAWODNICY where Nazwisko = @Nazwisko and Aktywny = 1) = 1
	insert into RANKING (Punkty, Id_Zawodnika) values (@Punkty, (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Nazwisko));
go
-- drop procedure dodaj
exec dodaj 'Federer', 16500
exec dodaj 'Djokovic', 10825
exec dodaj 'Nadal', 14250
exec dodaj 'Lu', 4620
exec dodaj 'Wawrinka', 6865
exec dodaj 'del Potro', 5230
exec dodaj 'Murray', 8550
---------------------------------------------------------------------------------------------------------------------------------------------
create table KARIERY( 
Id_Zawodnika int primary key not null,
Poczatek_kariery int not null default 0, 
Koniec_kariery int,
Tytuly int not null default 0,
Finaly int not null default 0, 
Zwyciestwa int not null default 0, 
Porazki int not null default 0, 
Zarobione_pieniadze int not null default 0,
constraint kariera_ogr_nazw unique (Id_Zawodnika),
constraint kariera_ogr_kar check (Koniec_kariery > Poczatek_kariery),
constraint kariera_ogr_obcy foreign key (Id_Zawodnika) references [ZAWODNICY](Id_Zawodnika),
constraint kariera_ogr_tytuly check (tytuly > -1),
constraint kariera_ogr_finaly check (finaly > -1),
constraint kariera_ogr_zwyciestwa check (zwyciestwa > -1),
constraint kariera_ogr_porazki check (porazki > -1),
constraint kariera_ogr_pieniadze check (zarobione_pieniadze > -1)
);
-- drop table KARIERY
select * from KARIERY

go
create view karieryView
as
	select K.Id_Zawodnika, Z.Imie, Z.Nazwisko, K.Poczatek_kariery, K.Koniec_kariery, K.Tytuly, K.Finaly, K.Zwyciestwa, K.Porazki, K.Zarobione_pieniadze 
	from ZAWODNICY Z right join KARIERY K on Z.Id_Zawodnika = K.Id_Zawodnika
go
-- drop view karieryView
select * from karieryView

go
create procedure dodajKar
	@Nazwisko varchar(30),
	@Poczatek int,
	@Koniec int,
	@Tytuly int,
	@Finaly int,
	@Zwyciestwa int,
	@Porazki int,
	@Pieniadze int
as
	if (select count(*) from (select * from KARIERY where Id_zawodnika = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Nazwisko)) SR) = 0
	if (select dbo.czyIstnieje(@Nazwisko)) = 1
	insert into KARIERY values ((select Id_Zawodnika from ZAWODNICY where Nazwisko = @Nazwisko), @Poczatek, @Koniec, @Tytuly, @Finaly, @Zwyciestwa, @Porazki, @Pieniadze);
go
-- drop procedure dodajKar

go
create trigger karieraPieniadze 
on KARIERY
after update
as
	if ((select Zarobione_pieniadze from KARIERY where Id_zawodnika in (select Id_zawodnika from deleted)) <= (select Zarobione_Pieniadze from deleted)
	and ((select Tytuly from KARIERY where Id_zawodnika in (select Id_zawodnika from deleted)) <= (select Tytuly from deleted)))
		update KARIERY
		set Zarobione_Pieniadze = (select Zarobione_Pieniadze from deleted),
		    Tytuly = (select Tytuly from deleted)
		where Id_zawodnika in (select Id_zawodnika from deleted)
go
-- drop trigger karieraPieniadze

exec dodajKar 'Connors', 1972, 1996, 109, 163, 1254, 278, 8625201
exec dodajKar 'Federer', 1998, null, 100, 50, 1555, 333, 97303556
exec dodajKar 'Nadal', 2001, null, 67, 31, 767, 160, 75888125
exec dodajKar 'del Potro', 2005, null, 18, 7, 314, 128, 15369422
exec dodajKar 'Gonzalez', 1999, 2012, 11, 11, 370, 202, 8862276
exec dodajKar 'Murray', 2005, null, 35, 17, 552, 165, 42435316
exec dodajKar 'Wawrinka', 2002, null, 11, 9, 392, 233, 20947676
exec dodajKar 'Lu', 2001, null, 0, 1, 145, 198, 3988958
exec dodajKar 'Djokovic', 2003, null, 59, 26, 686, 146, 94050059
exec dodajKar 'Sampras', 1988, 2002, 64, 24, 762, 222, 43280489
---------------------------------------------------------------------------------------------------------------------------------------------
create table TRENERZY(
Id_Trenera int identity(1, 1) primary key,
Nazwisko varchar(30) not null,
Imie varchar(30) not null,
IdZawodnika int,
constraint trenerzy_ogr_nazw unique (Nazwisko),
constraint trenerzy_ogr_obcy foreign key (IdZawodnika) references [ZAWODNICY](Id_Zawodnika)
);
-- drop table TRENERZY
select * from TRENERZY

go
create view trenerzyView
as
	select T.Id_Trenera, T.Imie as 'Imie trenera', T.Nazwisko as 'Nazwisko trenera', Z.Nazwisko as 'Nazwisko zawodnika'
	from ZAWODNICY Z right join TRENERZY T on Z.Id_Zawodnika = T.IdZawodnika
go
-- drop view trenerzyView
select * from trenerzyView

go
create procedure dodajTren
	@Nazwisko varchar(30),
	@Imie varchar(30),
	@NazwiskoZaw varchar(30)
as
	if (select count(*) from (select * from TRENERZY where Nazwisko = @Nazwisko) SZ) = 0 
	if(select dbo.czyIstnieje(@NazwiskoZaw)) = 1
	insert into TRENERZY  (Nazwisko, Imie, IdZawodnika) values (@Nazwisko, @Imie, (select Id_Zawodnika from ZAWODNICY where Nazwisko = @NazwiskoZaw));
	else
	insert into TRENERZY values(@Nazwisko, @Imie, null);
go
-- drop procedure dodajTren

go
create procedure updTren
	@Nazwisko varchar(30),
	@Imie varchar(30),
	@NazwiskoZaw varchar(30),
	@ID varchar(30)
as
begin
	update TRENERZY
	set
		Nazwisko = @Nazwisko,
		Imie = @Imie,
		IdZawodnika = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @NazwiskoZaw)
	where 
		Id_Trenera = @ID
end
go
-- drop procedure updTren	
-- exec dbo.updTren 'Boris', 'Becker', 'Nadal', 1

exec dodajTren 'Becker', 'Boris', 'Djokovic'
exec dodajTren 'Luthi', 'Severin', 'Federer'
exec dodajTren 'Nadal', 'Toni', 'Nadal'
exec dodajTren 'Ljubicic', 'Ivan', 'Federer'
exec dodajTren 'Antonini', 'Roberto', null
exec dodajTren 'Norman', 'Magnus', 'Wawrinka'
exec dodajTren 'Vajda', 'Marian', null
exec dodajTren 'Mauresmo', 'Amelie', 'Murray'
---------------------------------------------------------------------------------------------------------------------------------------------
create table MASTERS_TURNIEJE(
Id int identity(1, 1) primary key,
NazwaTurnieju varchar(30) unique not null,
Nawierzchnia varchar(30) not null
);
-- drop table MASTERS_TURNIEJE
select * from MASTERS_TURNIEJE

go
create procedure dodajTurniejMasters
	@Nazwa varchar(30),
	@Naw varchar(30)
as
	if (select count(*) from (select * from MASTERS_TURNIEJE where NazwaTurnieju = @Nazwa) SZ) = 0 
	insert into MASTERS_TURNIEJE (NazwaTurnieju, Nawierzchnia) values (@Nazwa, @Naw);
go
-- drop procedure dodajTurniejMasters
exec dbo.dodajTurniejMasters 'Indian Wells', 'Twarda'
exec dbo.dodajTurniejMasters 'Miami', 'Twarda'
exec dbo.dodajTurniejMasters 'Monte Carlo', 'Glina'
exec dbo.dodajTurniejMasters 'Madrid', 'Glina'
exec dbo.dodajTurniejMasters 'Rome', 'Glina'
exec dbo.dodajTurniejMasters 'Montreal', 'Glina'
exec dbo.dodajTurniejMasters 'Cincinnati', 'Twarda'
exec dbo.dodajTurniejMasters 'Shanghai', 'Twarda'
exec dbo.dodajTurniejMasters 'Paris', 'Twarda'

go
create function czyIstniejeTurniejMasters(@Turniej varchar(30))
	returns bit
as
begin
	return (select count(*) from MASTERS_TURNIEJE where NazwaTurnieju = @Turniej);
end
go
-- drop function czyIstniejeTurniejMasters
-------------------------------
create table MASTERS_WYNIKI(
IdTurnieju int primary key not null,
IdZwyciezcy int foreign key references[ZAWODNICY](Id_Zawodnika),
IdFinalisty int foreign key references[ZAWODNICY](Id_Zawodnika),
I_SET varchar(8),
II_SET varchar(8),
III_SET varchar(8),
constraint masters_wyniki_dupl check (IdZwyciezcy != IdFinalisty),
constraint masters_wyniki_for foreign key (IdTurnieju) references [MASTERS_TURNIEJE](Id)
);
-- drop table MASTERS_WYNIKI
select * from MASTERS_WYNIKI

go
create view mastersView
as
	select MT.Id, MT.NazwaTurnieju, MT.Nawierzchnia, Z1.Nazwisko as 'Nazwisko zwyciezcy', Z2.Nazwisko as 'Nazwisko finalisty',
		   MW.I_SET, MW.II_SET, MW.III_SET 
	from MASTERS_WYNIKI MW join ZAWODNICY Z1 on MW.IdZwyciezcy = Z1.Id_Zawodnika join ZAWODNICY Z2 on MW.IdFinalisty = Z2.Id_Zawodnika 
	right join MASTERS_TURNIEJE MT on MW.IdTurnieju = MT.Id
go
-- drop view mastersView
select * from mastersView

go
create procedure dodajWynikMasters
	@Turniej varchar(30),
	@Zwyciezca varchar(30),
	@Finalista varchar(30),
	@1set varchar(8),
	@2set varchar(8),
	@3set varchar(8)
as
	if (select dbo.czyIstniejeTurniejMasters(@Turniej)) = 1
	if (@Zwyciezca != @Finalista)
	if (select count(*) from (select * from MASTERS_WYNIKI where IdTurnieju = (select Id from MASTERS_TURNIEJE where NazwaTurnieju = @Turniej)) SR) = 0 
		if (select dbo.czyIstnieje(@Zwyciezca)) = 1
			if (select dbo.czyIstnieje(@Finalista)) = 1
				insert into MASTERS_WYNIKI values((select Id from MASTERS_TURNIEJE where NazwaTurnieju = @Turniej),
												  (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zwyciezca),
												  (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Finalista),
												  @1set, @2set, @3set);
go
-- drop procedure dodajWynikMasters
-- exec dbo.updMasters 8, 'Shang', 'Tw', 'Federer', 'Murray', '1:2', '2:3', '3:3'

go
create procedure updMasters
	@ID int,
	@NazwaTurnieju varchar(30),
	@Nawierzchnia varchar(30),
	@Zwyciezca varchar(30),
	@Finalista varchar(30),
	@I_SET varchar(8),
	@II_SET varchar(8),
	@III_SET varchar(8)
as
begin
	update MASTERS_TURNIEJE
	set
		NazwaTurnieju = @NazwaTurnieju,
		Nawierzchnia = @Nawierzchnia
	where Id = @ID

	update MASTERS_WYNIKI
	set
		IdZwyciezcy = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zwyciezca),
		IdFinalisty = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Finalista),
		I_SET = @I_SET,
		II_SET = @II_SET,
		III_SET = @III_SET
	where IdTurnieju = @ID
end
go
-- drop procedure updMasters

go
create trigger karieraMasters
on MASTERS_WYNIKI
after insert
as
begin
	update KARIERY
	set Zwyciestwa = Zwyciestwa + 1,
	Tytuly = Tytuly + 1
	where Id_Zawodnika = (select IdZwyciezcy from inserted)
	update KARIERY
	set Porazki = Porazki + 1,
	Finaly = Finaly + 1
	where Id_Zawodnika = (select IdFinalisty from inserted)
end
go
-- drop trigger karieraMasters

exec dbo.dodajWynikMasters 'Indian Wells', 'Djokovic', 'Federer','6:3', '6:7(5)', '6:2'
exec dbo.dodajWynikMasters 'Miami', 'Djokovic', 'Murray', '7:6(3)', '4:6', '6:0'
exec dbo.dodajWynikMasters 'Monte Carlo', 'Djokovic', 'Wawrinka', '7:5', '4:6', '6:3'
exec dbo.dodajWynikMasters 'Madrid', 'Murray', 'Nadal', '6:3', '6:2', null
exec dbo.dodajWynikMasters 'Rome', 'Djokovic', 'Federer', '6:4', '6:3', null
exec dbo.dodajWynikMasters 'Montreal', 'Murray', 'Djokovic', '6:4', '4:6', '6:3'
exec dbo.dodajWynikMasters 'Cincinnati', 'Federer', 'Djokovic', '7:6(1)', '6:3', null
---------------------------------------------------------------------------------------------------
create table WIELKIE_SZLEMY(
Id int identity(1, 1) primary key,
NazwaTurnieju varchar(30) unique not null,
Nawierzchnia varchar(30) not null
constraint szlemy_ogr_turn unique (NazwaTurnieju)
);
-- drop table WIELKIE_SZLEMY
select * from WIELKIE_SZLEMY

go
create procedure dodajWielkiSzlem
	@Nazwa varchar(30),
	@Naw varchar(30)
as
	if (select count(*) from (select * from WIELKIE_SZLEMY where NazwaTurnieju = @Nazwa) SZ) = 0 
	insert into WIELKIE_SZLEMY (NazwaTurnieju, Nawierzchnia) values(@Nazwa, @Naw);
go
-- drop procedure dodajWielkiSzlem
exec dbo.dodajWielkiSzlem 'Australian Open', 'Twarda'
exec dbo.dodajWielkiSzlem 'Roland Garros', 'Glina'
exec dbo.dodajWielkiSzlem 'Wimbledon', 'Trawa'
exec dbo.dodajWielkiSzlem 'US Open', 'Twarda'

go
create function czyIstniejeWielkiSzlem(@Turniej varchar(30))
	returns bit
as
begin
	return (select count(*) from WIELKIE_SZLEMY where NazwaTurnieju = @Turniej)
end
go
-- drop function czyIstniejeWielkiSzlem
-------------------------------
create table WIELKIE_SZLEMY_WYNIKI(
IdTurnieju int primary key not null,
IdZwyciezcy int foreign key references [ZAWODNICY](Id_Zawodnika),
IdFinalisty int foreign key references [ZAWODNICY](Id_Zawodnika),
I_SET varchar(8),
II_SET varchar(8),
III_SET varchar(8),
IV_SET varchar(8),
V_SET varchar(8),
constraint szlemy_wyniki_dupl check (IdZwyciezcy != IdFinalisty),
constraint szlemy_wyniki_for foreign key(IdTurnieju) references [WIELKIE_SZLEMY](Id),
);
-- drop table WIELKIE_SZLEMY_WYNIKI
select * from WIELKIE_SZLEMY_WYNIKI

go
create view szlemyView
as
	select WS.Id, WS.NazwaTurnieju, WS.Nawierzchnia, Z1.Nazwisko as 'Nazwisko zwyciezcy', 
		   Z2.Nazwisko as 'Nazwisko finalisty', WSW.I_SET, WSW.II_SET, WSW.III_SET, WSW.IV_SET, WSW.V_SET 
	from WIELKIE_SZLEMY_WYNIKI WSW join ZAWODNICY Z1 on WSW.IdZwyciezcy = Z1.Id_Zawodnika join ZAWODNICY Z2 on WSW.IdFinalisty = Z2.Id_Zawodnika 
	right join WIELKIE_SZLEMY WS on WSW.IdTurnieju = WS.Id
go
-- drop view szlemyView
select * from szlemyView

go
create procedure dodajWynikSzlem
	@Turniej varchar(30),
	@Zwyciezca varchar(30),
	@Finalista varchar(30),
	@1set varchar(8),
	@2set varchar(8),
	@3set varchar(8),
	@4set varchar(8),
	@5set varchar(8)
as
	if(select dbo.czyIstniejeWielkiSzlem(@Turniej)) = 1
	if(select count(*) from (select * from WIELKIE_SZLEMY_WYNIKI where IdTurnieju = (select Id from WIELKIE_SZLEMY where NazwaTurnieju = @Turniej)) SR ) = 0
		if (select dbo.czyIstnieje(@Zwyciezca)) = 1
			if (select dbo.czyIstnieje(@Finalista)) = 1
				insert into WIELKIE_SZLEMY_WYNIKI values((select Id from WIELKIE_SZLEMY where NazwaTurnieju = @Turniej),
														 (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zwyciezca),
														 (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Finalista),
														 @1set, @2set, @3set, @4set, @5set);
go
-- drop procedure dodajWynikSzlem

go
create procedure updSzlemy
	@ID int,
	@NazwaTurnieju varchar(30),
	@Nawierzchnia varchar(30),
	@Zwyciezca varchar(30),
	@Finalista varchar(30),
	@I_SET varchar(8),
	@II_SET varchar(8),
	@III_SET varchar(8),
	@IV_SET varchar(8),
	@V_SET varchar(8)
as
begin
	update WIELKIE_SZLEMY
	set
		NazwaTurnieju = @NazwaTurnieju,
		Nawierzchnia = @Nawierzchnia
	where Id = @ID

	update WIELKIE_SZLEMY_WYNIKI
	set
		IdZwyciezcy = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zwyciezca),
		IdFinalisty = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Finalista),
		I_SET = @I_SET,
		II_SET = @II_SET,
		III_SET = @III_SET,
		IV_SET = @IV_SET,
		V_SET = @V_SET
	where IdTurnieju = @ID
end
go
-- drop procedure updSzlemy

create trigger karieraSzlemy
on WIELKIE_SZLEMY_WYNIKI
after insert
as
begin
	update KARIERY
	set Zwyciestwa = Zwyciestwa + 1,
	Tytuly = Tytuly + 1
	where Id_Zawodnika = (select IdZwyciezcy from inserted)
	update KARIERY
	set Porazki = Porazki + 1,
	Finaly = Finaly + 1
	where Id_Zawodnika = (select IdFinalisty from inserted)
end
go
-- drop trigger karieraSzlemy

exec dbo.dodajWynikSzlem 'Australian Open', 'Federer', 'Murray','7:6(5)', '6:7(4)', '6:3', '6:0', null
exec dbo.dodajWynikSzlem 'Roland Garros', 'Wawrinka', 'Djokovic', '6:4', '6:3', '6:4', null, null
exec dbo.dodajWynikSzlem 'Wimbledon', 'Federer', 'Djokovic', '7:6(1)', '6:7(10)', '6:4', '6:3', null
exec dbo.dodajWynikSzlem 'US Open', 'Djokovic', 'Federer', '4:6', '7:5', '4:6', '6:4', '7:5'
----------------------------------------------------------------------------------------------------
create table OLIMPIADY(
Rok int primary key not null,
Miejsce varchar(30) not null,
Nawierzchnia varchar(30) not null,
constraint Olimp_ogr_rok unique(Rok),
);
-- drop table OLIMPIADY
select * from OLIMPIADY

go
create procedure dodajOlimpiade
	@Rok int,
	@Miasto varchar(30),
	@Naw varchar(30)
as
	if (select count(*) from (select * from OLIMPIADY where Rok = @Rok) SZ) = 0 
	insert into OLIMPIADY (Rok, Miejsce, Nawierzchnia) values (@Rok, @Miasto, @Naw);
go
-- drop procedure dodajOlimpiade
exec dbo.dodajOlimpiade 2008, 'Pekin', 'Twarda'
exec dbo.dodajOlimpiade 2012, 'Londyn', 'Trawa'
exec dbo.dodajOlimpiade 2016, 'Rio de Janerio', 'Twarda'

go
create function czyIstniejeOlimpiada(@Kiedy int)
	returns bit
as
begin
	return (select count(*) from OLIMPIADY where Rok = @Kiedy);
end
go
-- drop function czyIstniejeOlimpiada
-------------------------------
create table OLIMPIADY_WYNIKI(
RokTurnieju int primary key not null,
Zloto int foreign key references[ZAWODNICY](Id_Zawodnika),
Srebro int foreign key references[ZAWODNICY](Id_Zawodnika),
Braz int foreign key references[ZAWODNICY](Id_Zawodnika),
I_SET varchar(8),
II_SET varchar(8),
III_SET varchar(8),
IV_SET varchar(8),
V_SET varchar(8),
constraint olimpiady_wyniki_dupl check(Zloto != Srebro and Zloto != Braz and Srebro != Braz),
constraint olimpiady_wyniki_for foreign key(RokTurnieju) references[OLIMPIADY](Rok),
);
-- drop table OLIMPIADY_WYNIKI
select * from OLIMPIADY_WYNIKI

go
create view olimpiadyView
as
	select O.Rok, O.Miejsce, O.Nawierzchnia, Z1.Nazwisko as 'Zloty medal', Z2.Nazwisko as 'Srebrny medal',
	Z3.Nazwisko as 'Brazowy medal', OW.I_SET, OW.II_SET, OW.III_SET, OW.IV_SET, OW.V_SET 
	from OLIMPIADY_WYNIKI OW join ZAWODNICY Z1 on OW.Zloto = Z1.Id_Zawodnika join ZAWODNICY Z2 on OW.Srebro = Z2.Id_Zawodnika
	join ZAWODNICY Z3 on OW.Braz = Z3.Id_Zawodnika 
	right join OLIMPIADY O on O.Rok = OW.RokTurnieju
go
-- drop view olimpiadyView
select * from olimpiadyView

go
create procedure dodajWynikOlimpiada
	@KtoryRok int,
	@Zloto varchar(30),
	@Srebro varchar(30),
	@Braz varchar(30),
	@1set varchar(8),
	@2set varchar(8),
	@3set varchar(8),
	@4set varchar(8),
	@5set varchar(8)
as
	if(select dbo.czyIstniejeOlimpiada(@KtoryRok)) = 1
	if(select count(*) from (select * from OLIMPIADY_WYNIKI where RokTurnieju = (select Rok from OLIMPIADY where Rok = @KtoryRok)) SR) = 0
		if (select dbo.czyIstnieje(@Zloto)) = 1
			if (select dbo.czyIstnieje(@Srebro)) = 1
				if (select dbo.czyIstnieje(@Braz)) = 1
					insert into OLIMPIADY_WYNIKI values((select Rok from OLIMPIADY where Rok = @KtoryRok),
												(select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zloto),
												(select Id_Zawodnika from ZAWODNICY where Nazwisko = @Srebro),
												(select Id_Zawodnika from ZAWODNICY where Nazwisko = @Braz),
												@1set, @2set, @3set, @4set, @5set);
go
-- drop procedure dodajWynikOlimpiada

go
create procedure updOlimp
	@Rok int,
	@Miejsce varchar(30),
	@Nawierzchnia varchar(30),
	@Zloto varchar(30),
	@Srebro varchar(30),
	@Braz varchar(30),
	@I_SET varchar(8),
	@II_SET varchar(8),
	@III_SET varchar(8),
	@IV_SET varchar(8),
	@V_SET varchar(8)
as
begin
	update OLIMPIADY
	set
		Miejsce = @Miejsce,
		Nawierzchnia = @Nawierzchnia
	where Rok = @Rok

	update OLIMPIADY_WYNIKI
	set
		Zloto = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Zloto),
		Srebro = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Srebro),		
		Braz = (select Id_Zawodnika from ZAWODNICY where Nazwisko = @Braz),
		I_SET = @I_SET,
		II_SET = @II_SET,
		III_SET = @III_SET,
		IV_SET = @IV_SET,
		V_SET = @V_SET
	where RokTurnieju = @Rok
end
go
-- drop procedure updOlimp

go
create trigger karieraOlimpiady
on OLIMPIADY_WYNIKI
after insert
as
begin
	update KARIERY
	set Zwyciestwa = Zwyciestwa + 1,
	Tytuly = Tytuly + 1
	where Id_Zawodnika = (select Zloto from inserted)
	update KARIERY
	set Porazki = Porazki + 1,
	Finaly = Finaly + 1
	where Id_Zawodnika = (select Srebro from inserted)
end
go
-- drop trigger karieraOlimpiady

exec dbo.dodajWynikOlimpiada 2008, 'Nadal', 'Gonzalez', 'Djokovic', '6:3', '7:6(5)', '6:3', null, null
exec dbo.dodajWynikOlimpiada 2012, 'Federer', 'Murray', 'del Potro', '6:2', '6:1', '4:6', '7:5', null
-- exec dbo.dodajWynikOlimpiada 2016, 'Nadal', 'Gonzalez', 'Sampras', '6:3', '7:6(5)', '6:3', null, null
---------------------------------------------------------------------------------------------------
create table REKORDY(
IdRekordu int identity(1,1) primary key,
Rodzaj varchar(30) unique not null, 
Wynik decimal(10,2), 
Id_Zawodnika int foreign key references[ZAWODNICY](Id_Zawodnika)
);
-- drop table REKORDY
select * from REKORDY

go
create view rekordyView
as
	select R.Rodzaj, R.Wynik, Z.Imie, Z.Nazwisko from REKORDY R join ZAWODNICY Z on R.Id_Zawodnika = Z.Id_Zawodnika
go
-- drop view rekordyView
select * from rekordyView

go
create procedure dodajRekord
	@Nazwa varchar(30)
as
	insert into REKORDY(Rodzaj) values (@Nazwa)
go
-- drop procedure dodajRekord

go
create procedure zainicjujRekordy
as
	exec dbo.dodajRekord 'Wygrane turnieje'
	exec dbo.dodajRekord 'Finaly'
	exec dbo.dodajRekord 'Procent skutecznosci'
	exec dbo.dodajRekord 'Zarobione pieniadze'
go
-- drop procedure zainicjujRekordy
-- truncate table Rekordy
exec dbo.zainicjujRekordy

go
create view Skutecznosc as
select Zawodnicy.Id_zawodnika, ZAWODNICY.Imie, ZAWODNICY.Nazwisko, cast((cast(KARIERY.Zwyciestwa as decimal(10,2)) * 100)/
                                   (cast(KARIERY.Zwyciestwa as decimal(10,2)) + cast(KARIERY.Porazki as decimal(10,2)) + 0.00001) as decimal(10,2)) as Skutecznosc
from Zawodnicy join KARIERY on Zawodnicy.Id_zawodnika = KARIERY.Id_Zawodnika
go
-- drop view Skutecznosc
select * from Skutecznosc

go
create procedure aktualizujRekordy
as
	update REKORDY
	set Wynik = (select max(Zwyciestwa) from KARIERY),
	Id_Zawodnika = (select Id_Zawodnika from ZAWODNICY where Id_Zawodnika =
	               (select Id_Zawodnika from KARIERY where Zwyciestwa = (select max(Zwyciestwa) from KARIERY)))
	where IdRekordu = 1

	update REKORDY set
	Wynik = (select max(Finaly) from KARIERY),
	Id_Zawodnika = (select Id_Zawodnika from ZAWODNICY where Id_Zawodnika = 
	(select Id_Zawodnika from KARIERY where Finaly = (select max(Finaly) from KARIERY)))
	where idRekordu = 2
	
	update REKORDY
	set Wynik = (select max(Skutecznosc) from Skutecznosc),
	Id_Zawodnika = (select Id_Zawodnika from Skutecznosc where Skutecznosc = (select max(Skutecznosc) from Skutecznosc)) 
	where idRekordu = 3

	update REKORDY
	Set Wynik = (select max(Zarobione_pieniadze) from KARIERY),
	Id_Zawodnika = (select Id_Zawodnika from ZAWODNICY where Id_Zawodnika = 
	(select Id_Zawodnika from KARIERY where Zarobione_pieniadze = (select max(Zarobione_pieniadze) from KARIERY)))
	where idRekordu = 4
go
-- drop procedure aktualizujRekordy

create trigger trigRekordy
on KARIERY
after update, delete, insert
as
    exec dbo.aktualizujRekordy
go
-- drop trigger trigRekordy
---------------------------------------------------------------------------------------------------
go
create view wszystkoView
as
	select Z.Imie, Z.Nazwisko, Z.Kraj, Z.Aktywny, K.Poczatek_kariery, K.Koniec_kariery, K.Tytuly, K.Finaly, K.Zwyciestwa, K.Porazki, K.Zarobione_pieniadze
	from ZAWODNICY Z left join KARIERY K on Z.Id_Zawodnika = K.Id_Zawodnika
go
-- drop view wszystkoView
select * from wszystkoView

go
create view aktywni as
select * from wszystkoView where Aktywny = 1
go
-- drop view aktywni
select * from aktywni

go
create view nieaktywni as
select * from wszystkoView where Aktywny = 0
go
-- drop view nieaktywni
select * from nieaktywni

go
create function okresKarieryStart (@Start int)
	returns table
as
	return (select * from wszystkoView where Poczatek_kariery >= @Start)
go
-- drop function okresKarieryStart
-- select * from okresKarieryStart(2000)

go
create function okresKarieryStartKoniec (@Start int, @Koniec int)
	returns table
as
	return (select * from wszystkoView where Poczatek_kariery >= @Start and Koniec_kariery <= @Koniec)
go
-- drop function okresKarieryStartKoniec
-- select * from okresKarieryStartKoniec (1880, 2005)