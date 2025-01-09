USE CarRentalComparerDb

DELETE FROM dbo.RentalTransactions
DELETE FROM dbo.CarDetails
DELETE FROM dbo.Employees
DELETE FROM dbo.Providers
DELETE FROM dbo.Users

DBCC CHECKIDENT ('RentalTransactions', RESEED, 0);
DBCC CHECKIDENT ('CarDetails', RESEED, 0);
DBCC CHECKIDENT ('Employees', RESEED, 0);
DBCC CHECKIDENT ('Providers', RESEED, 0);
DBCC CHECKIDENT ('Users', RESEED, 0);

INSERT INTO Users (Email, Name, Lastname, Birthday, DrivingLicenseDate, Longitude, Latitude)
VALUES
    ('admgrac@gmail.com', 'Adam', 'Grącikowski', '1993-11-28', '2018-11-28', 21.0122, 52.2297),
    ('01180781@pw.edu.pl', 'Adam', 'Grącikowski', '1995-11-28', '2019-11-28', 21.0181, 52.2295),
    ('marcin.gronicki@gmail.com', 'Marcin', 'Gronicki', '2000-11-28', '2020-11-28', 21.0059, 52.2380),
    ('01180776@pw.edu.pl', 'Antonina', 'Frąckowiak', '1983-11-28', '2002-11-28', 21.0145, 52.2252)
	
INSERT INTO Providers (Name)
VALUES
    ('CarRental.Provider.API'),
    ('External.Provider.API')

INSERT INTO Employees (Email, ProviderId, FirstName, LastName)
VALUES
    ('adam_employee@carrentalatmini.onmicrosoft.com', 1, 'Adam', 'Gracikowski'),
    ('marcin_employee@carrentalatmini.onmicrosoft.com', 1, 'Marcin', 'Gronicki'),
    ('tosia_employee@carrentalatmini.onmicrosoft.com', 1, 'Antonina', 'Frąckowiak')

INSERT INTO CarDetails (OuterId, Make, Model, Segment, FuelType, TransmissionType, YearOfProduction, NumberOfDoors, NumberOfSeats)
VALUES
    ('1', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2020, 4, 5),
    ('2', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Manual', 2019, 4, 5),
    ('3', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Manual', 2021, 4, 5),
    ('4', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2020, 4, 5),
    ('5', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2020, 4, 5),
    ('6', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2020, 4, 5),
    ('7', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Manual', 2019, 4, 5),
    ('8', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Automatic', 2020, 4, 5),
    ('9', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Manual', 2018, 4, 5),
    ('10', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Automatic', 2019, 4, 5),
    ('11', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Automatic', 2020, 4, 5),
    ('12', 'Toyota', 'Corolla', 'Economy', 'Hybrid', 'Automatic', 2018, 4, 5),
    ('13', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2017, 4, 5),
    ('14', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2021, 4, 5),
    ('15', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Automatic', 2019, 4, 5),
    ('16', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Automatic', 2022, 4, 5),
    ('17', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2020, 4, 5),
    ('18', 'Toyota', 'Corolla', 'Economy', 'Hybrid', 'Automatic', 2021, 4, 5),
    ('19', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2018, 4, 5),
	('20', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Automatic', 2021, 4, 5),
    ('21', 'Toyota', 'Corolla', 'Economy', 'Hybrid', 'Automatic', 2019, 4, 5),
    ('22', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2020, 4, 5),
    ('23', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2016, 4, 5),
    ('24', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Automatic', 2018, 4, 5),
    ('25', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Automatic', 2021, 4, 7),
    ('26', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Automatic', 2021, 4, 5),
    ('27', 'Toyota', 'Corolla', 'Economy', 'Electric', 'Automatic', 2021, 4, 5),
    ('28', 'Toyota', 'Corolla', 'Economy', 'Hybrid', 'Automatic', 2020, 4, 5),
    ('29', 'Toyota', 'Corolla', 'Economy', 'Diesel', 'Automatic', 2019, 4, 7),
    ('30', 'Toyota', 'Corolla', 'Economy', 'Gasoline', 'Manual', 2017, 4, 5);

INSERT INTO RentalTransactions (UserId, ProviderId, RentalOuterId, RentalPricePerDay, InsurancePricePerDay, RentedAt, ReturnedAt, CarDetailsId, Status)
VALUES
    (1, 1, '1', 50.00, 30.00, '2025-01-01 09:00:00', NULL, 1, 3),
    (2, 1, '2', 60.00, 30.00, '2024-11-01 10:00:00', NULL, 2, 2),
    (1, 1, '3', 55.00, 50.00, '2024-10-15 12:00:00', NULL, 3, 2),
    (3, 1, '4', 65.00, 30.00, '2024-10-15 14:00:00', NULL, 4, 1),
    (4, 1, '5', 70.00, 30.00, '2025-01-01 15:00:00', NULL, 5, 2),
    (2, 1, '6', 75.00, 40.00, '2024-10-15 16:00:00', '2024-12-15 16:00:00', 6, 4),
    (1, 1, '7', 80.00, 50.00, '2024-10-01 17:00:00', '2024-10-15 16:00:00', 7, 4),
    (1, 1, '8', 85.00, 50.00, '2024-10-01 18:00:00', '2024-10-15 16:00:00', 8, 4),
    (3, 1, '9', 90.00, 50.00, '2024-10-01 19:00:00', '2024-10-15 16:00:00', 9, 4),
    (4, 1, '10', 95.00, 55.00, '2024-10-01 20:00:00', NULL, 10, 2),
    (2, 1, '11', 50.00, 25.00, '2024-09-20 10:00:00', '2024-09-22 10:00:00', 11, 4),
    (3, 1, '12', 55.00, 30.00, '2024-09-18 14:00:00', '2024-09-20 14:00:00', 12, 4),
    (4, 1, '13', 60.00, 35.00, '2024-09-16 08:00:00', '2024-09-18 08:00:00', 13, 4),
    (2, 1, '14', 65.00, 40.00, '2024-09-14 12:00:00', '2024-09-16 12:00:00', 14, 4),
    (1, 1, '15', 70.00, 45.00, '2024-09-10 15:00:00', NULL, 15, 1),
    (2, 1, '16', 75.00, 50.00, '2024-09-08 09:00:00', '2024-09-10 09:00:00', 16, 4),
    (3, 1, '17', 80.00, 55.00, '2024-12-06 11:00:00', '2024-12-08 11:00:00', 17, 4),
    (4, 1, '18', 85.00, 60.00, '2024-11-04 16:00:00', '2024-12-06 16:00:00', 18, 4),
    (1, 1, '19', 90.00, 65.00, '2024-11-02 18:00:00', NULL, 19, 1),
    (1, 1, '20', 95.00, 70.00, '2024-11-30 09:00:00', '2024-12-02 09:00:00', 20, 4),
    (2, 1, '21', 100.00, 75.00, '2023-10-28 13:00:00', '2023-11-30 13:00:00', 21, 4),
    (3, 1, '22', 50.00, 30.00, '2023-09-26 10:00:00', '2023-09-28 10:00:00', 22, 4),
    (4, 1, '23', 55.00, 35.00, '2023-08-24 15:00:00', '2023-08-26 15:00:00', 23, 4),
    (2, 1, '24', 60.00, 40.00, '2023-08-22 12:00:00', '2023-08-24 12:00:00', 24, 4),
    (1, 1, '25', 65.00, 45.00, '2023-08-20 18:00:00', NULL, 25, 1),
    (2, 1, '26', 70.00, 50.00, '2023-08-18 09:00:00', '2023-08-20 09:00:00', 26, 4),
    (3, 1, '27', 75.00, 55.00, '2023-08-16 14:00:00', '2023-09-18 14:00:00', 27, 4),
    (4, 1, '28', 80.00, 60.00, '2023-08-14 16:00:00', '2023-08-16 16:00:00', 28, 4),
    (3, 1, '29', 85.00, 65.00, '2023-08-12 19:00:00', '2023-09-14 19:00:00', 29, 4),
    (1, 1, '30', 90.00, 70.00, '2023-08-10 20:00:00', NULL, 30, 1);


