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


CREATE TABLE TravelOrder
(
	Id int not null primary key identity,
	OrderStatus int default 1,
	VehicleID int foreign key references Vehicle(Id),
	UserID int foreign key references Users(Id),
	Vehicle_km_start bigint ,
	Vehicle_km_end bigint ,
	Distance_crossed int,
	Starting_city nvarchar(50),
	Finish_city nvarchar(50),
	Total_price money,
	Total_days int,
	Created datetime default GETDATE(),
	Modified datetime default GETDATE(),
	StartingDate datetime
)

GO

CREATE TABLE Route
(
	Id int not null primary key identity,
	TravelOrderID int foreign key references TravelOrder(Id),
	DateStart datetime,
	DateEnd datetime,
    StartCoordinate nvarchar(50),
	EndCoordinate nvarchar(50),
	DistanceCrossed int,
	AverageSpeed int,
	FuelConsumption int
)

CREATE TABLE Service
(
	Id int not null primary key identity,
	VehicleID int not null foreign key references Vehicle(Id),
	Date datetime not null
)

CREATE TABLE ServiceItem
(
	Id int not null primary key identity,
	ServiceID int not null foreign key references Service(Id),
	ServiceName nvarchar(50) not null,
	Price money not null
)

CREATE TABLE Bills
(
	Id int not null primary key identity,
	TravelOrder int not null foreign key references TravelOrder(Id),
	Price money not null
)

CREATE TABLE VehicleFuel
(
	Id int not null primary key identity,
	UserID int not null foreign key references Users(Id),
	Date datetime not null ,
	Litres int not null,
	FuelPrice money not null
)



----------------------
USE PPPKProjekt
GO

CREATE PROC sp_insertPreparedData 
AS

--SET NOCOUNT ON;  

DECLARE @usId1 int
DECLARE @usId2 int
DECLARE @usId3 int

DECLARE @toId1 int
DECLARE @toId2 int
DECLARE @toId3 int

DECLARE @vehId1 int
DECLARE @vehId2 int
DECLARE @vehId3 int

DECLARE @vehType int

DECLARE @serId1 int
DECLARE @serId2 int
DECLARE @serId3 int

INSERT INTO  Users(Name, Surname, Mobile, Driving_License) VALUES ('Pero', 'Peric', '0991234567', '1222867')
SET @usId1 = SCOPE_IDENTITY()
INSERT INTO  Users(Name, Surname, Mobile, Driving_License) VALUES ('Ivan', 'Ivanovic', '09432134567', '7712834')
SET @usId2 = SCOPE_IDENTITY()
INSERT INTO  Users(Name, Surname, Mobile, Driving_License) VALUES ('Arsen', 'Dedic', '0923334567', '3426562')
SET @usId3 = SCOPE_IDENTITY()


INSERT INTO VehicleType(Name) VALUES('Auto')
SET @vehType = SCOPE_IDENTITY()
INSERT INTO VehicleType(Name) VALUES('Zrakoplov')
INSERT INTO VehicleType(Name) VALUES('Autobus')

INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage) VALUES(@vehType, 'ZG3387HN', 'Tesla', '2011-01-11', 1, 32420)
SET  @vehId1 = SCOPE_IDENTITY()
INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage) VALUES (@vehType, 'ZG227AA', 'Ford', '2012-05-03', 1, 72000)
SET  @vehId2 = SCOPE_IDENTITY()
INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage) VALUES(@vehType, 'ZG0000BB', 'Bmw', '2013-03-08', 1, 82000)
SET  @vehId3 = SCOPE_IDENTITY()

INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(1, @vehId1, @usId1, 32420, 3, 'Zagreb', 'Osijek', '2013-05-11')
SET  @toId1 = SCOPE_IDENTITY()
INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(1, @vehId2, @usId2, 72000, 2, 'Zagreb', 'Graz', '2010-08-10')
SET  @toId2 = SCOPE_IDENTITY()
INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(2, @vehId3, @usId3, 82000, 1, 'Velika Gorica', 'Ljubljana', '2004-11-01')
SET  @toId3 = SCOPE_IDENTITY()
INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(2, @vehId3, @usId3, 82000, 1, 'Split', 'Požega', '2010-08-12')
INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(3, @vehId3, @usId3, 82000, 1, 'Dubrovnik', 'Osijek', '2010-04-11')
INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Total_days, Starting_city, Finish_city, StartingDate) VALUES(3, @vehId3, @usId3, 82000, 1,'Pula','Varaždin', '2010-02-02')


INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption) VALUES(@toId1, '2010-01-07', '2010-01-12', '45.747,16.056', '46.123,15.3697', 120, 80, 8)
INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption) VALUES(@toId1, '2010-01-07', '2010-01-12', '45.9696,16.594567', '46.123,15.3697', 312, 80, 8)
INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption) VALUES(@toId2, '2010-03-07', '2010-04-08', '45.7467,16.0756', '46.123,15.9679', 280, 120, 6)
INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption) VALUES(@toId3, '2010-02-07', '2010-06-09', '45.3644,16.76', '46.089,15.2468', 620, 150, 5)
INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption) VALUES(@toId3, '2010-01-03', '2010-08-11', '45.053,16.950', '46.987,15.789', 400, 75, 10)


INSERT INTO Service(VehicleID, Date) VALUES(@vehId1, '2010-03-08')
SET  @serId1= SCOPE_IDENTITY()
INSERT INTO Service(VehicleID, Date) VALUES(@vehId1, '2012-06-04')
SET  @serId2 = SCOPE_IDENTITY()
INSERT INTO Service(VehicleID, Date) VALUES(@vehId2, '2011-07-05')
SET  @serId3= SCOPE_IDENTITY()
INSERT INTO Service(VehicleID, Date) VALUES(@vehId3, '2016-08-02')

----------------------


INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId1, 'Izmjena ulja', 330)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId1, 'Promjena diskova', 2050)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId1, 'Promjena tekučine', 850)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId2, 'Promjena pumpe za gorivo',3450)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId2, 'Promjena ulja', 350)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId3, 'Promjena motora', 10850)
INSERT INTO ServiceItem(ServiceID, ServiceName, Price) VALUES(@serId3, 'Promjena ulja', 350)

INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice) VALUES(@usId1, '2010-08-11', 24, 10)
INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice) VALUES(@usId1, '2013-08-11', 34, 9)
INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice) VALUES(@usId2, '2013-08-11', 14, 11)
INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice) VALUES(@usId2, '2012-08-11', 22, 9)
INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice) VALUES(@usId3, '2015-08-11', 21, 8)

INSERT INTO Bills(TravelOrder, Price) VALUES(@toId1, 440),
							(@toId1, 650),
							(@toId2, 850),
							(@toId3, 430)

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

-----------------------------------
--Stored procedures
------------------------------
create proc sp_getAllVehicles
as
begin
	SELECT * FROM Vehicle
end
go

go

create proc sp_insertVehicle
	@vehicleType int,
	@plate nvarchar(50),
	@brand nvarchar(50),
	@year date,
	@isAvailable bit,
	@milleage bigint
as
begin
	  INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage) VALUES(@vehicleType, @plate, @brand, @year, @isAvailable, @milleage)
end
go

create proc sp_updateVehicle
	@id int,
	@vehicleType int,
	@plate nvarchar(50),
	@brand nvarchar(50),
	@year date,
	@isAvailable bit,
	@milleage bigint
as
begin
	  UPDATE Vehicle
	  SET VehicleTypeId = @vehicleType, Plate = @plate, Brand = @brand, Production_Year = @year, Vehicle_Status = @isAvailable, Milleage = @milleage
	  WHERE Id = @id
end
go

create proc sp_deleteVehicle
	@id int
as 
begin
	delete from Vehicle where Id = @id
end
go

create proc insertRoute
	@travelOrderID int,
	@dateStart datetime,
	@dateEnd datetime,
	@startCoordinate nvarchar(50),
	@endCoordinate nvarchar(50),
	@distance int,
	@speed int,
	@fuel int
AS
	BEGIN 
		INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption)
			VALUES(@travelOrderID, @dateStart,@dateEnd, @startCoordinate,@endCoordinate,@distance, @speed, @fuel)
	END 
GO

create proc updateRoute
	@id int,
	@dateStart datetime,
	@dateEnd datetime,
	@startCoordinate nvarchar(50),
	@endCoordinate nvarchar(50),
	@distance int,
	@speed int,
	@fuel int
AS
	BEGIN 
		UPDATE Route SET  DateStart = @dateStart, DateEnd = @dateEnd, StartCoordinate = @startCoordinate, EndCoordinate = @endCoordinate, DistanceCrossed = @distance, AverageSpeed = @speed, FuelConsumption = @fuel
			WHERE Id = @id
	END 
GO

create proc deleteRoute
	@id int
as 
begin
	delete from Route where Id = @id
end
go
