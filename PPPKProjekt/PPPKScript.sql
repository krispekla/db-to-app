--Kris Peklaric 3IP1 PPPK

USE master
GO
IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'PPPKProjekt')
    BEGIN
        CREATE DATABASE PPPKProjekt
    END;

GO
USE PPPKProjekt
GO
CREATE TABLE Users
(
	Id int not null primary key identity,
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
	Mobile nvarchar(15),
	Created datetime default GETDATE(),
	Modified datetime default GETDATE(),
	Driving_License nvarchar(50)
)

CREATE TABLE VehicleType
(
	Id int not null identity primary key,
	Name nvarchar(50)
)
GO

CREATE TABLE Vehicle
(
	Id int not null primary key identity,
	VehicleTypeId int not null foreign key references VehicleType(Id),
	Plate nvarchar(50) not null,
	Brand nvarchar(50) not null,
	Production_Year date not null,
	Vehicle_Status bit,
	Milleage bigint not null,
	Created datetime default GETDATE(),
	Modified datetime default GETDATE(),
)
GO

CREATE TABLE Point
(
	Id int not null identity primary key,
	City nvarchar(50) not null,
	Street nvarchar(50) not null,
	House_number int not null,
)
GO

CREATE TABLE TravelOrder
(
	Id int not null primary key identity,
	OrderStatus int not null default 1,
	VehicleID int foreign key references Vehicle(Id),
	UserID int not null foreign key references Users(Id),
	Vehicle_km_start int not null,
	Vehicle_km_end int ,
	Distance_crossed int,
	Starting_point int foreign key references Point(Id),
	Finish_point int foreign key references Point(Id),
	Total_price money,
	Total_days int,
	Created datetime default GETDATE(),
	Modified datetime default GETDATE(),

)

GO
CREATE TABLE StatusType
(
	Id int not null primary key,
	Name nvarchar(50) not null
)

GO

CREATE TABLE Status
(
	Id int not null identity primary key,
	Code int not null references StatusType(Id),
	Created datetime default GETDATE(),
	Modified datetime default GETDATE(),
	PointId int foreign key references Point(Id),
	TravelOrderId int foreign key references TravelOrder(Id),
	Price money
)
GO


----------------------
USE PPPKProjekt
GO

CREATE PROC sp_insertPreparedData 
AS

--SET NOCOUNT ON;  


INSERT INTO  Users(Name, Surname, Mobile, Driving_License) VALUES ('Pero', 'Peric', '0991234567', '1222867'),
											  					  ('Ivan', 'Ivanovic', '09432134567', '7712834'),
																  ('Arsen', 'Dedic', '0923334567', '3426562')

INSERT INTO VehicleType(Name) VALUES('Auto'), ('Motor'), ('Zrakoplov'), ('Autobus')
INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage) VALUES(1, 'ZG3387HN', 'Tesla', '2010-01-01', 1, 32420),
																					(1, 'ZG227AA', 'Tesla', '2010-01-01', 1, 72000),
																					(1, 'ZG0000BB', 'Tesla', '2010-01-01', 1, 82000)

INSERT INTO StatusType(Id,Name) VALUES(1000 ,'Definiran nalog'),(2000, 'Pocetak puta'),(3000, 'Kraj puta'),
										(4000, 'Koordinata'),(5000, 'Gorivo'), (6000, 'Naplatna kuca'), (7000, 'Hotel'),
										(8000, 'Avionska karta')




INSERT INTO Point(City, Street, House_number) VALUES('Zagreb', 'Ilica', 12), ('Varaždin', 'Jalkovečka', 15),
													('Osijek', 'Osjecka', 2),('Zagreb', 'Zvnomirova', 5),('Split', 'Riva', 3), 
													('Opatija', 'Opatijska', 14)



INSERT INTO TravelOrder(VehicleID, UserID, Vehicle_km_start, Total_days, Starting_point, Finish_point) VALUES(1, 1, 32420, 3, 1, 5),
																											(2, 2, 72000, 2, 3, 1),
																											(3, 3, 82000, 1, 4, 2)


INSERT INTO Status(Code, TravelOrderId, PointId, Price) VALUES (1000, 1, 1, 2000),
																(1000, 2, 3, 3500),
																(1000, 2, 3, 1500)

GO
----------------------
GO

USE PPPKProjekt
GO

CREATE PROC sp_cleanDatabase 
AS

SET NOCOUNT ON;  

EXEC [sys].[sp_MSforeachtable] 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 

EXEC [sys].[sp_MSforeachtable] 'DELETE FROM ?' 

EXEC [sys].[sp_MSforeachtable] 'ALTER TABLE ? CHECK CONSTRAINT ALL' 

GO

