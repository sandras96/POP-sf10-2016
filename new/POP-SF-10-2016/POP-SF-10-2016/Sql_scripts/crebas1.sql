CREATE TABLE TipNamestaja (
	Id int PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(80),
	Obrisan BIT
)
GO
CREATE TABLE Namestaj (
	Id INT PRIMARY KEY IDENTITY(1,1),
	tipNID INT,
	Naziv VARCHAR(100),
	Cena NUMERIC(9,2),
	Kolicina INT,
	Obrisan BIT,
	FOREIGN KEY (tipNID) REFERENCES TipNamestaja(Id)
)