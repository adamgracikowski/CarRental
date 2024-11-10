use CarRentalProviderDb

DELETE FROM dbo.Cars
DELETE FROM dbo.Customers
DELETE FROM dbo.Insurances
DELETE FROM dbo.Makes
DELETE FROM dbo.Models
DELETE FROM dbo.Offers
DELETE FROM dbo.RentalReturns
DELETE FROM dbo.Rentals
DELETE FROM dbo.Segments

DBCC CHECKIDENT ('Cars', RESEED, 0);
DBCC CHECKIDENT ('Customers', RESEED, 0);
DBCC CHECKIDENT ('Insurances', RESEED, 0);
DBCC CHECKIDENT ('Makes', RESEED, 0);
DBCC CHECKIDENT ('Models', RESEED, 0);
DBCC CHECKIDENT ('Offers', RESEED, 0);
DBCC CHECKIDENT ('RentalReturns', RESEED, 0);
DBCC CHECKIDENT ('Rentals', RESEED, 0);
DBCC CHECKIDENT ('Segments', RESEED, 0);

INSERT INTO Customers (EmailAddress, FirstName, LastName)
VALUES 
    ('jan.kowalski@example.com', 'Jan', 'Kowalski'),
    ('anna.nowak@example.com', 'Anna', 'Nowak'),
    ('piotr.zielinski@example.com', 'Piotr', 'Zielinski'),
    ('ewa.kowalczyk@example.com', 'Ewa', 'Kowalczyk'),
    ('tomasz.szymanski@example.com', 'Tomasz', 'Szymanski');

INSERT INTO Insurances (Name, Description, PricePerDay)
VALUES 
    ('Tier 1', 'Base OC incsurance price', 30.00),
    ('Tier 2', 'Base OC incsurance price', 50.00),
    ('Tier 3', 'Base OC incsurance price', 70.00)

INSERT INTO Segments (Name, Description, PricePerDay, InsuranceId)
VALUES
    ('Economy', 'Basic segment with affordable options, ideal for budget-conscious customers.', 35.00, 1),
    ('Compact', 'Compact cars offering more space and comfort, suitable for city driving.', 45.00, 1),
    ('Midsize', 'Midsize vehicles with balanced features and pricing.', 55.00, 1),
    ('Fullsize', 'Spacious fullsize cars for families and longer trips.', 70.00, 2),
    ('Luxury', 'Premium segment for luxury experience and high-end features.', 130.00, 3),
    ('SUV', 'Sport Utility Vehicles, ideal for outdoor and family trips.', 80.00, 2),
    ('Minivan', 'Spacious minivans for large groups and families.', 60.00, 2)

INSERT INTO Makes (Name)
VALUES 
    ('Toyota'),
    ('Ford'),
    ('BMW'),
    ('Mercedes'),
    ('Audi'),
    ('Honda'),
    ('Volkswagen'),
    ('Mazda'),
    ('Hyundai'),
    ('Nissan');

INSERT INTO Models (Name, MakeId, SegmentId, NumberOfDoors, NumberOfSeats, EngineType, WheelDriveType)
VALUES
    ('Corolla', 1, 1, 4, 5, 0, 0),      -- Toyota, Economy
    ('Focus', 2, 1, 4, 5, 0, 0),        -- Ford, Economy
    ('Mazda2', 8, 1, 4, 5, 0, 0),       -- Mazda, Economy
    ('Civic', 6, 2, 4, 5, 0, 1),        -- Honda, Compact
    ('Golf', 7, 2, 4, 5, 0, 0),         -- Volkswagen, Compact
    ('A3', 5, 2, 4, 5, 0, 1),           -- Audi, Compact
	('Leaf', 10, 1, 4, 5, 1, 1),        -- Nissan, Compact
    ('Accord', 6, 3, 4, 5, 0, 0),       -- Honda, Midsize
    ('Passat', 7, 3, 4, 5, 0, 2),       -- Volkswagen, Midsize
    ('Camry', 1, 3, 4, 5, 0, 2),        -- Toyota, Midsize
    ('E-Class', 4, 4, 4, 5, 0, 0),      -- Mercedes, Fullsize
    ('5 Series', 3, 4, 4, 5, 0, 1),     -- BMW, Fullsize
    ('A6', 5, 4, 4, 5, 0, 1),           -- Audi, Fullsize
    ('S-Class', 4, 5, 4, 5, 0, 4),      -- Mercedes, Luxury
    ('7 Series', 3, 5, 4, 5, 0, 3),     -- BMW, Luxury
    ('A8', 5, 5, 4, 5, 0, 3),           -- Audi, Luxury
    ('CR-V', 6, 6, 5, 7, 0, 2),         -- Honda, SUV
    ('Tiguan', 7, 6, 5, 7, 0, 2),       -- Volkswagen, SUV
    ('RAV4', 1, 6, 5, 7, 0, 2),         -- Toyota, SUV
    ('Odyssey', 6, 7, 5, 8, 0, 1),      -- Honda, Minivan
    ('Sienna', 1, 7, 5, 8, 0, 3)        -- Toyota, Minivan

INSERT INTO Cars (ModelId, ProductionYear, FuelType, TransmissionType, Longitude, Latitude, Status)
VALUES
    (1, 2018, 0, 0, 21.012229, 52.229676, 1),
    (2, 2017, 0, 1, 21.015059, 52.237049, 1),
    (3, 2019, 1, 2, 21.036473, 52.245781, 1),
    (4, 2020, 0, 0, 21.045208, 52.232378, 1),
    (5, 2019, 2, 2, 21.012229, 52.229676, 1),
    (6, 2021, 1, 0, 21.015059, 52.237049, 1),
    (7, 2022, 0, 1, 21.036473, 52.245781, 0),
    (8, 2021, 0, 1, 21.045208, 52.232378, 0),
    (9, 2020, 3, 2, 21.012229, 52.229676, 0),
    (10, 2019, 1, 1, 21.015059, 52.237049, 0),
    (11, 2022, 0, 1, 21.036473, 52.245781, 0),
    (12, 2021, 2, 1, 21.045208, 52.232378, 0),
    (13, 2020, 4, 0, 21.012229, 52.229676, 0),
    (14, 2015, 1, 1, 21.015059, 52.237049, 0),
    (15, 2021, 0, 1, 21.036473, 52.245781, 0),
    (16, 2022, 0, 0, 21.045208, 52.232378, 0),
    (17, 2020, 1, 0, 21.012229, 52.229676, 0),
    (18, 2019, 2, 1, 21.015059, 52.237049, 0),
    (19, 2021, 0, 0, 21.036473, 52.245781, 0),
    (20, 2022, 0, 1, 21.045208, 52.232378, 0),
    (21, 2015, 2, 1, 21.012229, 52.229676, 0),
    (1, 2018, 0, 0, 21.012229, 52.229676, 0),
    (2, 2017, 0, 1, 21.015059, 52.237049, 0),
    (3, 2019, 1, 0, 21.036473, 52.245781, 0),
    (4, 2020, 2, 0, 21.045208, 52.232378, 0),
    (5, 2019, 0, 2, 21.012229, 52.229676, 0),
    (6, 2021, 1, 2, 21.015059, 52.237049, 0),
    (7, 2022, 0, 1, 21.036473, 52.245781, 0),
    (8, 2021, 3, 1, 21.045208, 52.232378, 0),
    (9, 2020, 2, 2, 21.012229, 52.229676, 0),
    (10, 2019, 1, 0, 21.015059, 52.237049, 0),
    (11, 2022, 0, 1, 21.036473, 52.245781, 0),
    (12, 2021, 2, 0, 21.045208, 52.232378, 0),
    (13, 2015, 1, 0, 21.012229, 52.229676, 0),
    (14, 2019, 0, 1, 21.015059, 52.237049, 0),
    (15, 2021, 0, 1, 21.036473, 52.245781, 0),
    (16, 2022, 2, 0, 21.045208, 52.232378, 0),
    (17, 2020, 1, 1, 21.012229, 52.229676, 0),
    (18, 2019, 2, 1, 21.015059, 52.237049, 0),
    (19, 2021, 0, 0, 21.036473, 52.245781, 0),
    (20, 2022, 1, 1, 21.045208, 52.232378, 0),
    (21, 2020, 2, 1, 21.012229, 52.229676, 0)

INSERT INTO Offers (CarId, GeneratedAt, ExpiresAt, RentalPricePerDay, InsurancePricePerDay, RentalId, [Key])
VALUES
	(1, '2024-11-01 09:00:00', '2024-11-01 09:00:30', 50.00, 30.00 , NULL, 'a'),
    (2, '2024-11-01 10:00:00', '2024-11-01 10:30:00', 60.00, 30.00, NULL, 'a'),
    (3, '2024-10-15 12:00:00','2024-10-15 12:30:00', 55.00, 50.00, NULL, 'a'),
    (4, '2024-10-15 14:00:00', '2024-10-15 14:30:00', 65.00, 30.00,NULL, 'a'),
    (5, '2024-10-15 15:00:00', '2024-10-15 15:30:00', 70.00, 30.00, NULL, 'a'),
    (20, '2024-10-15 16:00:00', '2024-10-15 16:30:00', 70.00, 30.00, NULL, 'a'),
    (7, '2024-10-01 17:00:00', '2024-10-15 17:30:00', 80.00, 50.00, NULL, 'a'),
    (8, '2024-10-01 18:00:00', '2024-10-01 18:30:00',85.00, 50.00, NULL, 'a'),
    (9, '2024-10-01 19:00:00', '2024-10-01 19:30:00', 90.00, 50.00, NULL, 'a'),
    (6, '2024-10-01 20:00:00', '2024-10-01 20:30:00', 95.00, 55.00, NULL, 'a'),
    (21, '2024-10-01 20:00:00', '2024-10-01 20:30:00', 95.00, 55.00, NULL, 'a');

INSERT INTO Rentals (OfferId, CustomerId, RentalReturnId, Status)
VALUES
    (1, 1, NULL, 1),
    (2, 2, NULL, 2),
	(3, 1, NULL, 3),
	(4, 3, NULL, 1),
    (5, 4, NULL, 3),
	(6, 2, NULL, 4),
	(7, 5, NULL, 4),
    (8, 1, NULL, 4),
	(9, 3, NULL, 4),
	(10, 5, NULL, 2),
    (11, 5, NULL, 0);

UPDATE Offers
SET RentalId = Offers.Id;

INSERT INTO RentalReturns (RentalId, ReturnedAt, Description, Image, Latitude, Longitude)
VALUES
    (6, '2024-10-15 16:00:00', 'Car returned in good condition', 'image1.jpg', 52.2297, 21.0122),
    (7, '2024-10-15 16:00:00', 'Minor scratch on the bumper', 'image2.jpg', 52.4064, 16.9252),
    (8, '2024-10-15 16:00:00', 'Car returned with fuel tank full', 'image3.jpg', 52.4064, 16.9252),
    (9, '2024-10-15 16:00:00', 'Returned early', 'image4.jpg', 52.2297, 21.0122)

UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 5 
WHERE Rentals.Id BETWEEN 6 AND 9





