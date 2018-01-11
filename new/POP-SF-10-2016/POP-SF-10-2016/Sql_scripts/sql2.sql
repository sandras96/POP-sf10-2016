INSERT INTO TipNamestaja (Naziv, Obrisan) VALUES ('Krevet', 0);
INSERT INTO TipNamestaja (Naziv, Obrisan) VALUES ('Sto', 0);
INSERT INTO TipNamestaja (Naziv, Obrisan) VALUES ('Kauc', 0);

INSERT INTO Namestaj (tipNID, Naziv, Cena, Kolicina, Obrisan) VALUES (1, 'Francuski krevet',20000, 22, 0);
INSERT INTO Namestaj (tipNID, Naziv, Cena, Kolicina, Obrisan) VALUES (2, 'Trpezarijski sto',9500, 15, 0);
INSERT INTO Namestaj (tipNID, Naziv, Cena, Kolicina, Obrisan) VALUES (2, 'Bastenski sto', 750, 9, 0);
INSERT INTO Namestaj (tipNID, Naziv, Cena, Kolicina, Obrisan) VALUES (3, 'Kauc na rasklapanje', 12000, 2, 1);

Insert into Korisnik(ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan) values('admin','admin','admin','admin',1,0);
Insert into Korisnik(ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan) values('prodavac','prodavac','prodavac','prodavac',0,0);


insert into DodatnaUsluga(Naziv,Cena,Obrisan) values ('Prevoz',1400,0)
insert into DodatnaUsluga(Naziv,Cena,Obrisan) values ('Montaza',2500,0)

insert into ProdajaNamestaja (DodatnaUslugaID,DatumProdaje, BrojRacuna, Kupac, UkupnaCena, Obrisan ) values (1,'2017-10-10','565','Sandra Stojanovic', 5620, 0)
insert into ProdajaNamestaja values (2,'2017-12-12','963','Nela Milojevic', 3620, 0)


insert into Akcija values ('2017-05-24', '2017-09-15', 50, 0)
insert into Akcija values ('2018-01-21', '2017-09-15', 70, 0)

insert into Salon values ('Chuck Bass', 'Strahinjica Bana', '031/513-064','chuckbass@gmail.com','www.chuckbass.com', 123, 123456789, '40-11-54651-1656-12' )

insert into NamestajNaAkciji values (1,1);
insert into NamestajNaAkciji values (1,2);

