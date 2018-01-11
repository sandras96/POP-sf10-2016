CREATE TABLE TipNamestaja (
	Id int PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(80),
	Obrisan BIT
)

CREATE TABLE Namestaj (
	Id INT PRIMARY KEY IDENTITY(1,1),
	tipNID INT,
	Naziv VARCHAR(100),
	Cena INT,
	Kolicina INT,
	Obrisan BIT,
	FOREIGN KEY (tipNID) REFERENCES TipNamestaja(Id)
)
create table DodatnaUsluga(
	Id int primary key IDENTITY (1, 1),
	Naziv varchar(80),
	Obrisan bit ,
	Cena int

)
create table Korisnik(
	Id int primary key IDENTITY (1, 1),
	Ime varchar(20),
	Prezime varchar(40),
	KorisnickoIme varchar(50),
	Lozinka varchar(50),
	Obrisan bit,
	TipKorisnika bit,
)

create table ProdajaNamestaja(
	Id int primary key IDENTITY(1,1),
	dodatnaUslugaID int,
	DatumProdaje datetime,
	BrojRacuna varchar(256),
	Kupac varchar(50),
	UkupnaCena int,
	Obrisan bit,
	FOREIGN KEY (dodatnaUslugaID) references DodatnaUsluga(Id)
)

create table Akcija(
	Id int primary key IDENTITY(1,1),
	Pocetak datetime,
	Kraj datetime,
	Popust int,
	Obrisan bit
)

create table Salon(
	Id int primary key IDENTITY(1,1),
	Naziv varchar(50),
	Adresa varchar(100),
	Telefon varchar(15),
	Email varchar(30),
	WebSite varchar(20),
	PIB int,
	MaticniBroj int,
	BrojZiroRacuna varchar (20)

)

create table NamestajNaAkciji(
	Id int primary key IDENTITY(1,1),
	AkcijaID int,
	NamestajID int,
	FOREIGN KEY (AkcijaID) references Akcija(Id),
	FOREIGN KEY (NamestajID) references Namestaj(Id),
)
