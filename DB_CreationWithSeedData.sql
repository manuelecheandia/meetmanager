USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'MeetManager')
BEGIN
    DROP DATABASE MeetManager;  
END;
CREATE DATABASE MeetManager;
GO

USE MeetManager
GO

---- BEGIN TABLE CREATION ----

CREATE TABLE Discipline (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	UnitOfMeasurement INT NOT NULL
)

CREATE TABLE Division (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	MinAge INT NULL,
	MaxAge INT NULL
)

CREATE TABLE Gender (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Event] (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	DisciplineId INT NOT NULL,
	DivisionId INT NOT NULL,
	GenderId INT NOT NULL,
	CONSTRAINT FK_Discipline_Event FOREIGN KEY (DisciplineId) REFERENCES Discipline(Id),
	CONSTRAINT FK_Division_Event FOREIGN KEY (DivisionId) REFERENCES Division(Id),
	CONSTRAINT FK_Gender_Event FOREIGN KEY (GenderId) REFERENCES Gender(Id),
)

CREATE UNIQUE INDEX IX_Event_Unique
ON [Event] ([Name], DisciplineId, DivisionId, GenderId);

CREATE TABLE Province (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	Abbr CHAR(2) NOT NULL
)

CREATE TABLE AthleteStatus (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Athlete (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	MiddleInitial CHAR(1) NULL,
	DOB DATETIME2 NOT NULL,
	GenderId INT NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Phone NVARCHAR(12) NULL,
	[Address] NVARCHAR(255) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	ProvinceId INT NOT NULL,
	PostalCode NVARCHAR(10),
	AthleteStatusId INT NOT NULL,
	CONSTRAINT FK_Gender_Athlete FOREIGN KEY (GenderId) REFERENCES Gender(Id),
	CONSTRAINT FK_AthleteStatus_Athlete FOREIGN KEY (AthleteStatusId) REFERENCES AthleteStatus(Id),
	CONSTRAINT FK_Province_Athlete FOREIGN KEY (ProvinceId) REFERENCES Province(Id),
)

CREATE TABLE Venue (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(255) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	ProvinceId INT NOT NULL,
	CONSTRAINT FK_Province_Venue FOREIGN KEY (ProvinceId) REFERENCES Province(Id),
)

CREATE TABLE Meet (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	StartDate DATETIME2 NOT NULL,
	EndDate DATETIME2 NOT NULL,
	RegistrationDeadline DATETIME2 NOT NULL,
	EntryFee MONEY NOT NULL,
	FeePerEvent MONEY NOT NULL,
	Information NTEXT NULL,
	VenueId INT NOT NULL,
	MeetType INT NOT NULL,
	RecordVersion ROWVERSION,
	CONSTRAINT FK_Venue_Meet FOREIGN KEY (VenueId) REFERENCES Venue(Id),
)


CREATE TABLE MeetEvents (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	MeetId INT NOT NULL,
	EventId INT NOT NULL,
	StartTime DATETIME2 NULL,
	CONSTRAINT FK_Meet_Event FOREIGN KEY (MeetId) REFERENCES Meet(Id),
	CONSTRAINT FK_Event_Meet FOREIGN KEY (EventId) REFERENCES [Event](Id)
)

CREATE TABLE Competition (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AthleteId INT NOT NULL,
	MeetEventsId INT NOT NULL,
	DateRegistered DATETIME2 NOT NULL,
	CONSTRAINT FK_Athlete_Competition FOREIGN KEY (AthleteId) REFERENCES Athlete(Id),
	CONSTRAINT FK_MeetEvents_Competition FOREIGN KEY (MeetEventsId) REFERENCES MeetEvents(Id),
)

CREATE TABLE Results (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CompetitionId INT NOT NULL,
	Result DECIMAL(8,3) NULL,
	WindSpeed DECIMAL(4,2) NULL,
	DNF BIT NULL,
	CONSTRAINT FK_Competition_Results FOREIGN KEY (CompetitionId) REFERENCES Competition(Id)
)
	
GO

---- END TABLE CREATION ----

---- BEGIN SEED DATA ----

/**** PROVINCE ****/
SET IDENTITY_INSERT [dbo].Province ON 
GO

INSERT INTO Province (Id, [Name], Abbr)
VALUES 
	(1, 'New Brunswick', 'NB'),
	(2, 'Newfoundland and Labrador', 'NF'),
	(3, 'Nova Scotia', 'NS'),
	(4, 'Prince Edward Island', 'PE'),
	(5, 'Quebec', 'QC'),
	(6, 'Ontario', 'ON'),
	(7, 'Manitoba', 'MB'),
	(8, 'Saskatchewan', 'SK'),
	(9, 'Alberta', 'AB'),
	(10, 'British Columbia', 'BC'),
	(11, 'Yukon', 'YT'),
	(12, 'Northwest Territories', 'NT'),
	(13, 'Nunuvut', 'NU')

SET IDENTITY_INSERT [dbo].Province OFF 
GO

/**** DISCIPLINE ****/
SET IDENTITY_INSERT [dbo].Discipline ON 
GO

INSERT INTO Discipline (Id, [Name], UnitOfMeasurement)
VALUES 
	(1, 'Track - Running', 1),
	(2, 'Field - Jumping', 2),
	(3, 'Field - Throwing', 2)

SET IDENTITY_INSERT [dbo].Discipline OFF 
GO

/**** DIVISION ****/
SET IDENTITY_INSERT [dbo].Division ON 
GO

INSERT INTO Division (Id, [Name], MinAge, MaxAge)
VALUES 
	(1, 'Masters', 35, null),
	(2, 'Sub Masters', 30, 34),
	(3, 'Senior', 20, 29),
	(4, 'U20', 18, 19),
	(5, 'U18', 16, 17),
	(6, 'U16', 14, 15),
	(7, 'U14', 12, 13),
	(8, 'U12', 10, 11),
	(9, 'Open', null, null)

SET IDENTITY_INSERT [dbo].Division OFF 
GO

/**** GENDER ****/
SET IDENTITY_INSERT [dbo].Gender ON 
GO

INSERT INTO Gender (Id, [Name])
VALUES 
	(1, 'Male'),
	(2, 'Female'),
	(3, 'Other')

SET IDENTITY_INSERT [dbo].Gender OFF 
GO

/**** EVENT ****/
SET IDENTITY_INSERT [dbo].[Event] ON 
GO

INSERT INTO [Event] (Id, [Name], DisciplineId, DivisionId, GenderId)
VALUES 
	(1, '60m', 1, 1, 1),
	(2, '60m', 1, 2, 1),
	(3, '60m', 1, 3, 1),
	(4, '60m', 1, 4, 1),
	(5, '60m', 1, 5, 1),
	(6, '60m', 1, 6, 1),
	(7, '60m', 1, 7, 1),
	(8, '60m', 1, 8, 1),
	(9, '60m', 1, 9, 1),
	(10, '60m', 1, 1, 2),
	(11, '60m', 1, 2, 2),
	(12, '60m', 1, 3, 2),
	(13, '60m', 1, 4, 2),
	(14, '60m', 1, 5, 2),
	(15, '60m', 1, 6, 2),
	(16, '60m', 1, 7, 2),
	(17, '60m', 1, 8, 2),
	(18, '60m', 1, 9, 2),
	(19, '100m', 1, 1, 1),
	(20, '100m', 1, 2, 1),
	(21, '100m', 1, 3, 1),
	(22, '100m', 1, 4, 1),
	(23, '100m', 1, 5, 1),
	(24, '100m', 1, 6, 1),
	(25, '100m', 1, 7, 1),
	(26, '100m', 1, 8, 1),
	(27, '100m', 1, 9, 1),
	(28, '100m', 1, 1, 2),
	(29, '100m', 1, 2, 2),
	(30, '100m', 1, 3, 2),
	(31, '100m', 1, 4, 2),
	(32, '100m', 1, 5, 2),
	(33, '100m', 1, 6, 2),
	(34, '100m', 1, 7, 2),
	(35, '100m', 1, 8, 2),
	(36, '100m', 1, 9, 2),
	(37, '200m', 1, 1, 1),
	(38, '200m', 1, 2, 1),
	(39, '200m', 1, 3, 1),
	(40, '200m', 1, 4, 1),
	(41, '200m', 1, 5, 1),
	(42, '200m', 1, 6, 1),
	(43, '200m', 1, 7, 1),
	(44, '200m', 1, 8, 1),
	(45, '200m', 1, 9, 1),
	(46, '200m', 1, 1, 2),
	(47, '200m', 1, 2, 2),
	(48, '200m', 1, 3, 2),
	(49, '200m', 1, 4, 2),
	(50, '200m', 1, 5, 2),
	(51, '200m', 1, 6, 2),
	(52, '200m', 1, 7, 2),
	(53, '200m', 1, 8, 2),
	(54, '200m', 1, 9, 2),
	(55, '400m', 1, 1, 1),
	(56, '400m', 1, 2, 1),
	(57, '400m', 1, 3, 1),
	(58, '400m', 1, 4, 1),
	(59, '400m', 1, 5, 1),
	(60, '400m', 1, 6, 1),
	(61, '400m', 1, 7, 1),
	(62, '400m', 1, 8, 1),
	(63, '400m', 1, 9, 1),
	(64, '400m', 1, 1, 2),
	(65, '400m', 1, 2, 2),
	(66, '400m', 1, 3, 2),
	(67, '400m', 1, 4, 2),
	(68, '400m', 1, 5, 2),
	(69, '400m', 1, 6, 2),
	(70, '400m', 1, 7, 2),
	(71, '400m', 1, 8, 2),
	(72, '400m', 1, 9, 2),
	(73, '800m', 1, 1, 1),
	(74, '800m', 1, 2, 1),
	(75, '800m', 1, 3, 1),
	(76, '800m', 1, 4, 1),
	(77, '800m', 1, 5, 1),
	(78, '800m', 1, 6, 1),
	(79, '800m', 1, 7, 1),
	(80, '800m', 1, 8, 1),
	(81, '800m', 1, 9, 1),
	(82, '800m', 1, 1, 2),
	(83, '800m', 1, 2, 2),
	(84, '800m', 1, 3, 2),
	(85, '800m', 1, 4, 2),
	(86, '800m', 1, 5, 2),
	(87, '800m', 1, 6, 2),
	(88, '800m', 1, 7, 2),
	(89, '800m', 1, 8, 2),
	(90, '800m', 1, 9, 2),
	(91, '1500m', 1, 1, 1),
	(92, '1500m', 1, 2, 1),
	(93, '1500m', 1, 3, 1),
	(94, '1500m', 1, 4, 1),
	(95, '1500m', 1, 5, 1),
	(96, '1500m', 1, 6, 1),
	(97, '1500m', 1, 7, 1),
	(98, '1500m', 1, 8, 1),
	(99, '1500m', 1, 9, 1),
	(100, '1500m', 1, 1, 2),
	(101, '1500m', 1, 2, 2),
	(102, '1500m', 1, 3, 2),
	(103, '1500m', 1, 4, 2),
	(104, '1500m', 1, 5, 2),
	(105, '1500m', 1, 6, 2),
	(106, '1500m', 1, 7, 2),
	(107, '1500m', 1, 8, 2),
	(108, '1500m', 1, 9, 2),
	(109, '3000m', 1, 1, 1),
	(110, '3000m', 1, 2, 1),
	(111, '3000m', 1, 3, 1),
	(112, '3000m', 1, 4, 1),
	(113, '3000m', 1, 5, 1),
	(114, '3000m', 1, 6, 1),
	(115, '3000m', 1, 7, 1),
	(116, '3000m', 1, 8, 1),
	(117, '3000m', 1, 9, 1),
	(118, '3000m', 1, 1, 2),
	(119, '3000m', 1, 2, 2),
	(120, '3000m', 1, 3, 2),
	(121, '3000m', 1, 4, 2),
	(122, '3000m', 1, 5, 2),
	(123, '3000m', 1, 6, 2),
	(124, '3000m', 1, 7, 2),
	(125, '3000m', 1, 8, 2),
	(126, '3000m', 1, 9, 2),
	(127, '5000m', 1, 1, 1),
	(128, '5000m', 1, 2, 1),
	(129, '5000m', 1, 3, 1),
	(130, '5000m', 1, 4, 1),
	(131, '5000m', 1, 5, 1),
	(132, '5000m', 1, 6, 1),
	(133, '5000m', 1, 7, 1),
	(134, '5000m', 1, 8, 1),
	(135, '5000m', 1, 9, 1),
	(136, '5000m', 1, 1, 2),
	(137, '5000m', 1, 2, 2),
	(138, '5000m', 1, 3, 2),
	(139, '5000m', 1, 4, 2),
	(140, '5000m', 1, 5, 2),
	(141, '5000m', 1, 6, 2),
	(142, '5000m', 1, 7, 2),
	(143, '5000m', 1, 8, 2),
	(144, '5000m', 1, 9, 2),
	(145, '10000m', 1, 1, 1),
	(146, '10000m', 1, 2, 1),
	(147, '10000m', 1, 3, 1),
	(148, '10000m', 1, 4, 1),
	(149, '10000m', 1, 5, 1),
	(150, '10000m', 1, 6, 1),
	(151, '10000m', 1, 7, 1),
	(152, '10000m', 1, 8, 1),
	(153, '10000m', 1, 9, 1),
	(154, '10000m', 1, 1, 2),
	(155, '10000m', 1, 2, 2),
	(156, '10000m', 1, 3, 2),
	(157, '10000m', 1, 4, 2),
	(158, '10000m', 1, 5, 2),
	(159, '10000m', 1, 6, 2),
	(160, '10000m', 1, 7, 2),
	(161, '10000m', 1, 8, 2),
	(162, '10000m', 1, 9, 2),
	(163, 'Long jump', 2, 1, 1),
	(164, 'Long jump', 2, 2, 1),
	(165, 'Long jump', 2, 3, 1),
	(166, 'Long jump', 2, 4, 1),
	(167, 'Long jump', 2, 5, 1),
	(168, 'Long jump', 2, 6, 1),
	(169, 'Long jump', 2, 7, 1),
	(170, 'Long jump', 2, 8, 1),
	(171, 'Long jump', 2, 9, 1),
	(172, 'Long jump', 2, 1, 2),
	(173, 'Long jump', 2, 2, 2),
	(174, 'Long jump', 2, 3, 2),
	(175, 'Long jump', 2, 4, 2),
	(176, 'Long jump', 2, 5, 2),
	(177, 'Long jump', 2, 6, 2),
	(178, 'Long jump', 2, 7, 2),
	(179, 'Long jump', 2, 8, 2),
	(180, 'Long jump', 2, 9, 2),
	(181, 'Triple jump', 2, 1, 1),
	(182, 'Triple jump', 2, 2, 1),
	(183, 'Triple jump', 2, 3, 1),
	(184, 'Triple jump', 2, 4, 1),
	(185, 'Triple jump', 2, 5, 1),
	(186, 'Triple jump', 2, 6, 1),
	(187, 'Triple jump', 2, 7, 1),
	(188, 'Triple jump', 2, 8, 1),
	(189, 'Triple jump', 2, 9, 1),
	(190, 'Triple jump', 2, 1, 2),
	(191, 'Triple jump', 2, 2, 2),
	(192, 'Triple jump', 2, 3, 2),
	(193, 'Triple jump', 2, 4, 2),
	(194, 'Triple jump', 2, 5, 2),
	(195, 'Triple jump', 2, 6, 2),
	(196, 'Triple jump', 2, 7, 2),
	(197, 'Triple jump', 2, 8, 2),
	(198, 'Triple jump', 2, 9, 2),
	(199, 'High jump', 2, 1, 1),
	(200, 'High jump', 2, 2, 1),
	(201, 'High jump', 2, 3, 1),
	(202, 'High jump', 2, 4, 1),
	(203, 'High jump', 2, 5, 1),
	(204, 'High jump', 2, 6, 1),
	(205, 'High jump', 2, 7, 1),
	(206, 'High jump', 2, 8, 1),
	(207, 'High jump', 2, 9, 1),
	(208, 'High jump', 2, 1, 2),
	(209, 'High jump', 2, 2, 2),
	(210, 'High jump', 2, 3, 2),
	(211, 'High jump', 2, 4, 2),
	(212, 'High jump', 2, 5, 2),
	(213, 'High jump', 2, 6, 2),
	(214, 'High jump', 2, 7, 2),
	(215, 'High jump', 2, 8, 2),
	(216, 'High jump', 2, 9, 2),
	(217, 'Pole vault', 2, 1, 1),
	(218, 'Pole vault', 2, 2, 1),
	(219, 'Pole vault', 2, 3, 1),
	(220, 'Pole vault', 2, 4, 1),
	(221, 'Pole vault', 2, 5, 1),
	(222, 'Pole vault', 2, 6, 1),
	(223, 'Pole vault', 2, 7, 1),
	(224, 'Pole vault', 2, 8, 1),
	(225, 'Pole vault', 2, 9, 1),
	(226, 'Pole vault', 2, 1, 2),
	(227, 'Pole vault', 2, 2, 2),
	(228, 'Pole vault', 2, 3, 2),
	(229, 'Pole vault', 2, 4, 2),
	(230, 'Pole vault', 2, 5, 2),
	(231, 'Pole vault', 2, 6, 2),
	(232, 'Pole vault', 2, 7, 2),
	(233, 'Pole vault', 2, 8, 2),
	(234, 'Pole vault', 2, 9, 2),
	(235, 'Shot put', 3, 1, 1),
	(236, 'Shot put', 3, 2, 1),
	(237, 'Shot put', 3, 3, 1),
	(238, 'Shot put', 3, 4, 1),
	(239, 'Shot put', 3, 5, 1),
	(240, 'Shot put', 3, 6, 1),
	(241, 'Shot put', 3, 7, 1),
	(242, 'Shot put', 3, 8, 1),
	(243, 'Shot put', 3, 9, 1),
	(244, 'Shot put', 3, 1, 2),
	(245, 'Shot put', 3, 2, 2),
	(246, 'Shot put', 3, 3, 2),
	(247, 'Shot put', 3, 4, 2),
	(248, 'Shot put', 3, 5, 2),
	(249, 'Shot put', 3, 6, 2),
	(250, 'Shot put', 3, 7, 2),
	(251, 'Shot put', 3, 8, 2),
	(252, 'Shot put', 3, 9, 2),
	(253, 'Discus throw', 3, 1, 1),
	(254, 'Discus throw', 3, 2, 1),
	(255, 'Discus throw', 3, 3, 1),
	(256, 'Discus throw', 3, 4, 1),
	(257, 'Discus throw', 3, 5, 1),
	(258, 'Discus throw', 3, 6, 1),
	(259, 'Discus throw', 3, 7, 1),
	(260, 'Discus throw', 3, 8, 1),
	(261, 'Discus throw', 3, 9, 1),
	(262, 'Discus throw', 3, 1, 2),
	(263, 'Discus throw', 3, 2, 2),
	(264, 'Discus throw', 3, 3, 2),
	(265, 'Discus throw', 3, 4, 2),
	(266, 'Discus throw', 3, 5, 2),
	(267, 'Discus throw', 3, 6, 2),
	(268, 'Discus throw', 3, 7, 2),
	(269, 'Discus throw', 3, 8, 2),
	(270, 'Discus throw', 3, 9, 2),
	(271, 'Javelin throw', 3, 1, 1),
	(272, 'Javelin throw', 3, 2, 1),
	(273, 'Javelin throw', 3, 3, 1),
	(274, 'Javelin throw', 3, 4, 1),
	(275, 'Javelin throw', 3, 5, 1),
	(276, 'Javelin throw', 3, 6, 1),
	(277, 'Javelin throw', 3, 7, 1),
	(278, 'Javelin throw', 3, 8, 1),
	(279, 'Javelin throw', 3, 9, 1),
	(280, 'Javelin throw', 3, 1, 2),
	(281, 'Javelin throw', 3, 2, 2),
	(282, 'Javelin throw', 3, 3, 2),
	(283, 'Javelin throw', 3, 4, 2),
	(284, 'Javelin throw', 3, 5, 2),
	(285, 'Javelin throw', 3, 6, 2),
	(286, 'Javelin throw', 3, 7, 2),
	(287, 'Javelin throw', 3, 8, 2),
	(288, 'Javelin throw', 3, 9, 2)


SET IDENTITY_INSERT [dbo].[Event] OFF 
GO

/**** VENUE ****/
SET IDENTITY_INSERT [dbo].Venue ON 
GO

INSERT INTO Venue(Id, [Name], [Address], City, ProvinceId)
VALUES
	(1, 'Croix Bleue Medavie Stadium', '30 Antonine-Maillet Ave.', 'Moncton', 1),
	(2, 'Canada Games Stadium, UNBSJ', '100 Tucker Park Rd.', 'Saint John', 1),
	(3, 'Canada Games Centre', '26 Thomas Raddall Dr', 'Halifax', 3),
	(4, 'Cape Breton Health & Recreation Complex', 'Grand Lake Rd.', 'Sydney', 3),
	(5, 'Irving Oil Field House', '2141 Thurston Drive, Suite 105', 'Saint John', 1),
	(6, 'Percival Molson Memorial Stadium', '475 Pine Ave W', 'Montreal', 5)

SET IDENTITY_INSERT [dbo].Venue OFF 
GO

/**** MEET ****/
SET IDENTITY_INSERT [dbo].Meet ON 
GO

INSERT INTO Meet(Id, [Name], StartDate, EndDate, RegistrationDeadline, EntryFee, FeePerEvent, Information, VenueId, MeetType)
VALUES
	(1, 'Hub City Classic', '2024-06-18 13:30:00', '2024-06-18 21:00:00', '2024-06-15 23:59:59', 25, 10, null, 1, 2),
	(2, 'Canada Games Trials', '2024-07-09 10:00:00', '2024-07-10 17:00:00', '2024-07-06 23:59:59', 25, 10, null, 2, 1),
	(3, 'Saint John Reds Twilight # 1', '2024-05-09 18:00:00', '2024-05-09 21:00:00', '2024-05-07 23:59:59', 25, 10, null, 2, 3)

SET IDENTITY_INSERT [dbo].Meet OFF 
GO

/**** MEET EVENTS ****/
SET IDENTITY_INSERT [dbo].MeetEvents ON 
GO

INSERT INTO MeetEvents(Id, MeetId, EventId, StartTime)
VALUES
	(1, 1, 1, null),
	(2, 1, 6, null),
	(3, 1, 8, null),
	(4, 1, 33, null),
	(5, 1, 40, null),
	(6, 2, 58, null),
	(7, 1, 2, null),
	(8, 1, 78, null),
	(9, 1, 234, null)
GO

SET IDENTITY_INSERT [dbo].MeetEvents OFF 
GO

/**** ATHLETESTATUS ****/
SET IDENTITY_INSERT [dbo].AthleteStatus ON 
GO

INSERT INTO AthleteStatus (Id, [Name])
VALUES 
	(1, 'Active'),
	(2, 'Inactive'),
	(3, 'Suspended')

SET IDENTITY_INSERT [dbo].AthleteStatus OFF 
GO

/**** ATHLETE ****/
SET IDENTITY_INSERT [dbo].Athlete ON 
GO

INSERT INTO Athlete (Id, FirstName, LastName, MiddleInitial, DOB, GenderId, Email, Phone,  [Address], City, ProvinceId, PostalCode, AthleteStatusId)
VALUES 
	(1, 'Abbie', 'Carter', null, '2008-10-31', 2, 'abbie@carter.com', null, '1234 Mountain Rd.', 'Moncton', 1, 'E1E 1E1', 1),
	(2, 'Stephen', 'Carter', 'M', '1972-11-22', 1, 'stephen@carter.com', null, '1234 Mountain Rd.', 'Moncton', 1, 'E1E 1E1', 1)

SET IDENTITY_INSERT [dbo].Athlete OFF 
GO

-- END SEED DATA --