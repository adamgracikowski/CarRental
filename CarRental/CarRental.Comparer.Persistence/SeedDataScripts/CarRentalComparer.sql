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
    ('jan.kowalski@example.com', 'Jan', 'Kowalski', '1993-11-28', '2018-11-28', 21.0122, 52.2297),
    ('anna.nowak@example.com', 'Anna', 'Nowak', '1995-11-28', '2020-11-28', 21.0181, 52.2295),
    ('piotr.zielinski@example.com', 'Piotr', 'Zielinski', '1988-11-28', '2006-11-28', 21.0059, 52.2380),
    ('ewa.kowalczyk@example.com', 'Ewa', 'Kowalczyk', '1983-11-28', '1998-11-28', 21.0145, 52.2252),
    ('tomasz.szymanski@example.com', 'Tomasz', 'Szymanski', '1991-11-28', '2013-11-28', 21.0070, 52.2396);
	
INSERT INTO Providers (Name)
VALUES
    ('CarRental.Provider.API'),
    ('External.Provider.API')

INSERT INTO Employees (Email, ProviderId, FirstName, LastName)
VALUES
    ('adam@example.com', 1, 'Ad', 'Am'),
    ('marcin.gronicki@gmail.com', 1, 'Mar', 'Cin'),
    ('tosia@example.com', 1, 'To', 'Sia')

INSERT INTO CarDetails (OuterId, Make, Model, Segment, FuelType, TransmissionType, YearOfProduction, NumberOfDoors, NumberOfSeats)
VALUES
    ('1', 'Toyota', 'Corolla', 'Economy', 1, 1, 2020, 4, 5),
    ('2', 'Ford', 'Focus', 'Economy', 1, 1, 2019, 4, 5),
    ('3', 'Mazda', 'Mazda2', 'Economy', 1, 2, 2021, 4, 5),
    ('4', 'Honda', 'Civic', 'Compact', 2, 1, 2022, 4, 5),
    ('5', 'Volkswagen', 'Golf', 'Compact', 2, 1, 2023, 4, 5),
	('20', 'Honda', 'Odyssey', 'Minivan', 1, 1, 2020, 4, 5),
    ('7', 'Nissan', 'Leaf', 'Compact', 1, 1, 2019, 4, 5),
    ('8', 'Honda', 'Accord', 'Midsize', 1, 2, 2021, 4, 5),
    ('9', 'Volkswagen', 'Passat', 'MidSize', 2, 1, 2022, 4, 5),
    ('6', 'Audi', 'A3', 'Compact', 2, 1, 2023, 4, 5)

INSERT INTO RentalTransactions (UserId, ProviderId, RentalOuterId, RentalPricePerDay, InsurancePricePerDay, RentedAt, ReturnedAt, CarDetailsId, Status)
VALUES
    (1, 1, '1', 50.00, 30.00, '2024-11-01 09:00:00', NULL, 1, 1),
    (2, 1, '2', 60.00, 30.00, '2024-11-01 10:30:00', NULL, 2, 2),
    (1, 1, '3', 55.00, 50.00, '2024-10-15 12:00:00', NULL, 3, 2),
    (3, 1, '4', 65.00, 30.00, '2024-10-15 14:00:00', NULL, 4, 1),
    (4, 1, '5', 70.00, 30.00, '2024-10-15 15:00:00', NULL, 5, 3),
    (2, 1, '6', 75.00, 40.00, '2024-10-15 16:00:00', '2024-10-15 16:00:00', 6, 4),
    (5, 1, '7', 80.00, 50.00, '2024-10-01 17:00:00', '2024-10-15 16:00:00', 7, 4),
    (1, 1, '8', 85.00, 50.00, '2024-10-01 18:00:00', '2024-10-15 16:00:00', 8, 4),
    (3, 1, '9', 90.00, 50.00, '2024-10-01 19:00:00', '2024-10-15 16:00:00', 9, 4),
    (5, 1, '10', 95.00, 55.00, '2024-10-01 20:00:00', NULL, 10, 2);

