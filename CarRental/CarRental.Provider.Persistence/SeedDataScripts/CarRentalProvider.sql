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
    ('admgrac@gmail.com', 'Adam', 'Grącikowski'),
    ('01180781@pw.edu.pl', 'Adam', 'Grącikowski'),
    ('marcin.gronicki@gmail.com', 'Marcin', 'Gronicki'),
    ('01180776@pw.edu.pl', 'Antonina', 'Frąckowiak')

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
    ('Corolla', 1, 1, 4, 5, 0, 0),
    ('Focus', 2, 1, 4, 5, 0, 0),
    ('Mazda2', 8, 1, 4, 5, 0, 0),
    ('Civic', 6, 2, 4, 5, 0, 1), 
    ('Golf', 7, 2, 4, 5, 0, 0),    
    ('A3', 5, 2, 4, 5, 0, 1),
	('Leaf', 10, 1, 4, 5, 1, 1),     
    ('Accord', 6, 3, 4, 5, 0, 0),      
    ('Passat', 7, 3, 4, 5, 0, 2),
    ('Camry', 1, 3, 4, 5, 0, 2),
    ('E-Class', 4, 4, 4, 5, 0, 0),
    ('5 Series', 3, 4, 4, 5, 0, 1),
    ('A6', 5, 4, 4, 5, 0, 1),
    ('S-Class', 4, 5, 4, 5, 0, 4),
    ('7 Series', 3, 5, 4, 5, 0, 3),
    ('A8', 5, 5, 4, 5, 0, 3),
    ('CR-V', 6, 6, 5, 7, 0, 2),
    ('Tiguan', 7, 6, 5, 7, 0, 2),
    ('RAV4', 1, 6, 5, 7, 0, 2),
    ('Odyssey', 6, 7, 5, 8, 0, 1), 
    ('Sienna', 1, 7, 5, 8, 0, 3),
	('Q5', 5, 6, 5, 5, 0, 0)


INSERT INTO Cars (ModelId, ProductionYear, FuelType, TransmissionType, Longitude, Latitude, Status)
VALUES
    (1, 2018, 0, 0, 21.012229, 52.229676, 0),
    (2, 2017, 0, 1, 21.015059, 52.237049, 0),
    (3, 2019, 1, 2, 21.036473, 52.245781, 0),
    (4, 2020, 0, 0, 21.045208, 52.232378, 0),
    (5, 2019, 2, 2, 21.012229, 52.229676, 0),
    (6, 2021, 1, 0, 21.015059, 52.237049, 0),
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
    (21, 2020, 2, 1, 21.012229, 52.229676, 0),
    (1, 2020, 0, 0, 21.001229, 52.221676, 0),
    (1, 2021, 1, 1, 21.011229, 52.231676, 1),
    (1, 2019, 2, 0, 21.021229, 52.241676, 1),
    (1, 2022, 3, 1, 21.031229, 52.251676, 0),
    (1, 2018, 0, 1, 21.041229, 52.261676, 1),
    (1, 2020, 1, 0, 21.051229, 52.271676, 0),
    (1, 2017, 2, 1, 21.061229, 52.281676, 0),
    (1, 2023, 0, 0, 21.071229, 52.291676, 0),
    (1, 2021, 3, 1, 21.081229, 52.301676, 0),
    (1, 2022, 0, 0, 21.091229, 52.311676, 1),
	(22, 2020, 0, 0, 21.001229, 52.221676, 0),
    (22, 2021, 1, 1, 21.011229, 52.231676, 1),
    (22, 2019, 2, 0, 21.021229, 52.241676, 1),
    (22, 2022, 3, 1, 21.031229, 52.251676, 0),
    (22, 2018, 0, 1, 21.041229, 52.261676, 1),
    (22, 2020, 1, 0, 21.051229, 52.271676, 0),
    (22, 2017, 2, 1, 21.061229, 52.281676, 0),
    (22, 2023, 0, 0, 21.071229, 52.291676, 0),
    (22, 2021, 3, 1, 21.081229, 52.301676, 0),
    (22, 2022, 0, 0, 21.091229, 52.311676, 1)




INSERT INTO Offers (CarId, GeneratedAt, ExpiresAt, RentalPricePerDay, InsurancePricePerDay, RentalId, [Key], GeneratedBy)
VALUES
	(43, '2025-01-01 09:00:00', '2025-01-01 09:30:00', 50.00, 30.00 , NULL, 'a', 'CarRental.Comparer.API.Audience'), --
    (44, '2024-11-01 10:00:00', '2024-11-01 10:30:00', 60.00, 30.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (45, '2024-10-15 12:00:00','2024-10-15 12:30:00', 55.00, 50.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (46, '2024-10-15 14:00:00', '2024-10-15 14:30:00', 65.00, 30.00,NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (47, '2025-01-01 15:00:00', '2025-01-01 15:30:00', 70.00, 30.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (48, '2024-10-15 16:00:00', '2024-10-15 16:30:00', 75.00, 40.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (49, '2024-10-01 17:00:00', '2024-10-15 17:30:00', 80.00, 50.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (50, '2024-10-01 18:00:00', '2024-10-01 18:30:00', 85.00, 50.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (51, '2024-10-01 19:00:00', '2024-10-01 19:30:00', 90.00, 50.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (52, '2024-10-01 20:00:00', '2024-10-01 20:30:00', 95.00, 55.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (53, '2024-09-20 10:00:00', '2024-09-20 10:30:00', 50.00, 25.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
	(54, '2024-09-18 14:00:00', '2024-09-18 14:30:00', 55.00, 30.00 , NULL, 'a', 'CarRental.Comparer.API.Audience'), --
    (55, '2024-09-16 08:00:00', '2024-09-16 08:30:00', 60.00, 35.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (56, '2024-09-14 12:00:00','2024-09-14 12:30:00', 65.00, 40.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (43, '2024-09-10 15:00:00', '2024-09-10 15:30:00', 70.00, 45.00,NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (44, '2024-09-08 09:00:00', '2024-09-08 09:30:00', 75.00, 50.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (45, '2024-12-06 11:00:00', '2024-12-06 11:30:00', 80.00, 55.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (46, '2024-11-04 16:00:00', '2024-11-04 16:30:00', 85.00, 60.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (47, '2024-11-02 18:00:00', '2024-11-02 18:30:00',90.00, 65.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (48, '2024-11-30 09:00:00', '2024-11-30 09:30:00', 95.00, 70.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (49, '2023-10-28 13:00:00', '2023-10-28 13:30:00', 100.00, 75.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (50, '2023-09-26 10:00:00', '2023-09-26 10:30:00', 50.00, 30.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
	(51, '2023-08-24 15:00:00', '2023-08-24 15:30:00', 55.00, 35.00 , NULL, 'a', 'CarRental.Comparer.API.Audience'), --
    (52, '2023-08-22 12:00:00', '2023-08-22 12:30:00', 60.00, 40.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (53, '2023-08-20 18:00:00','2023-08-20 18:30:00', 65.00, 45.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (54, '2023-08-18 09:00:00', '2023-08-18 09:30:00', 70.00, 50.00,NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (55, '2023-08-16 14:00:00', '2023-08-16 14:30:00', 75.00, 55.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (43, '2023-08-14 16:00:00', '2023-08-14 16:30:00', 80.00, 60.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (44, '2023-08-12 19:00:00', '2023-08-12 19:30:00', 85.00, 65.00, NULL, 'a', 'CarRental.Comparer.API.Audience'),
    (45, '2023-08-10 20:00:00', '2023-08-10 20:30:00',90.00, 70.00, NULL, 'a', 'CarRental.Comparer.API.Audience');
  
INSERT INTO Rentals (OfferId, CustomerId, RentalReturnId, Status)
VALUES
    (1, 1, NULL, 3),
    (2, 2, NULL, 2),
	(3, 1, NULL, 2),
	(4, 3, NULL, 1),
    (5, 4, NULL, 2),
	(6, 2, NULL, 4),
	(7, 1, NULL, 4),
    (8, 1, NULL, 4),
	(9, 3, NULL, 4),
	(10, 4, NULL, 2),
    (11, 2, NULL, 4),
	(12, 3, NULL, 4),
    (13, 4, NULL, 4),
	(14, 2, NULL, 4),
	(15, 1, NULL, 1),
    (16, 2, NULL, 4),
	(17, 3, NULL, 4),
	(18, 4, NULL, 4),
    (19, 1, NULL, 1),
	(20, 1, NULL, 4),
	(21, 2, NULL, 4),
    (22, 3, NULL, 4),
	(23, 4, NULL, 4),
    (24, 2, NULL, 4),
	(25, 1, NULL, 1),
	(26, 2, NULL, 4),
    (27, 3, NULL, 4),
	(28, 4, NULL, 4),
	(29, 3, NULL, 4),
    (30, 1, NULL, 1);


UPDATE Offers
SET RentalId = Offers.Id;

INSERT INTO RentalReturns (RentalId, ReturnedAt, Description, Image, Latitude, Longitude)
VALUES
    (6, '2024-12-15 16:00:00', 'Car returned in good condition', '', 52.2297, 21.0122),
    (7, '2024-10-15 16:00:00', 'Minor scratch on the bumper', '', 52.4064, 16.9252),
    (8, '2024-10-15 16:00:00', 'Car returned with fuel tank full', '', 52.4064, 16.9252),
    (9, '2024-10-15 16:00:00', 'Returned early', '', 52.2297, 21.0122),
	(11, '2024-09-22 10:00:00', 'Car returned in good condition', '', 52.2297, 21.0122),
    (12, '2024-09-20 14:00:00', 'Minor scratch on the bumper', '', 52.4064, 16.9252),
    (13, '2024-09-18 08:00:00', 'Car returned with fuel tank full', '', 52.4064, 16.9252),
    (14, '2024-09-16 12:00:00', 'Returned early', '', 52.2297, 21.0122),
	(16, '2024-09-10 09:00:00', 'Car returned in good condition', '', 52.2297, 21.0122),
    (17, '2024-12-08 11:00:00', 'Minor scratch on the bumper', '', 52.4064, 16.9252),
    (18, '2024-12-06 16:00:00', 'Car returned with fuel tank full', '', 52.4064, 16.9252),
    (20, '2024-12-02 09:00:00', 'Returned early', '', 52.2297, 21.0122),
	(21, '2023-11-30 13:00:00', 'Car returned in good condition', '', 52.2297, 21.0122),
    (22, '2023-09-28 10:00:00', 'Minor scratch on the bumper', '', 52.4064, 16.9252),
    (23, '2023-08-26 15:00:00', 'Car returned with fuel tank full', '', 52.4064, 16.9252),
    (24, '2023-08-24 12:00:00', 'Returned early', '', 52.2297, 21.0122),
	(26, '2023-08-20 09:00:00', 'Car returned in good condition', '', 52.2297, 21.0122),
    (27, '2023-09-18 14:00:00', 'Minor scratch on the bumper', '', 52.4064, 16.9252),
    (28, '2023-08-16 16:00:00', 'Car returned with fuel tank full', '', 52.4064, 16.9252),
    (29, '2023-09-14 19:00:00', 'Returned early', '', 52.2297, 21.0122);
	  
UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 5 
WHERE Rentals.Id BETWEEN 6 AND 9

UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 6
WHERE Rentals.Id BETWEEN 11 AND 14

UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 7
WHERE Rentals.Id BETWEEN 16 AND 19

UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 8
WHERE Rentals.Id BETWEEN 21 AND 24

UPDATE Rentals
SET Rentals.RentalReturnId = Rentals.Id - 9
WHERE Rentals.Id BETWEEN 26 AND 29