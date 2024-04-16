USE MeetManager
GO

CREATE OR ALTER PROC spGetMeets_SC
AS
BEGIN
	SELECT
		Meet.Id,
		Meet.[Name] + ' - ' + Venue.City + ', ' + Province.Abbr AS MeetName
	FROM 
		Meet
			INNER JOIN Venue ON Meet.VenueId = Venue.Id
			INNER JOIN Province ON Venue.ProvinceId = Province.Id
	ORDER BY
		Meet.[Name]
END
GO

CREATE OR ALTER PROC spGetVenues_SC
AS
BEGIN
	SELECT
		Venue.Id,
		Venue.[Name] + ' - ' + City + ', ' + Province.Abbr AS VenueName
	FROM 
		Venue
			INNER JOIN Province ON Venue.ProvinceId = Province.Id
	ORDER BY
		VenueName
END
GO

CREATE OR ALTER PROC spGetMeet_SC
	@MeetId INT
AS
BEGIN
	SELECT
		Id,
		[Name],
		StartDate,
		EndDate,
		RegistrationDeadline,
		EntryFee,
		FeePerEvent,
		Information,
		VenueId,
		MeetType,
		RecordVersion
	FROM
		Meet
	WHERE
		Id = @MeetId
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.types WHERE name = 'MeetEventsTableType' AND is_table_type =1)
BEGIN
CREATE TYPE MeetEventsTableType AS TABLE 
	(Id INT, MeetId INT, EventId INT, StartTime DATETIME2)
END
GO

CREATE OR ALTER PROC spUpdateMeet_SC
	@RecordVersion			ROWVERSION OUTPUT,
	@MeetId					INT,
	@MeetName				NVARCHAR(100),
	@StartDate				DATETIME2(7),
	@EndDate				DATETIME2(7),
	@RegistrationDeadline	DATETIME2(7),
	@EntryFee				MONEY,
	@FeePerEvent			MONEY,
	@Information			NTEXT,
	@VenueId				INT,
	@MeetType				INT,
	@MeetEvents				MeetEventsTableType READONLY
AS
BEGIN
	BEGIN TRY
		IF @RecordVersion <> (SELECT RecordVersion FROM Meet WHERE Id = @MeetId)
			THROW 51002, 'The record has been updated since you last retrieve it', 1;

		BEGIN TRAN	
			UPDATE Meet SET
				[Name] = @MeetName,
				StartDate = @StartDate,
				EndDate = @EndDate,
				RegistrationDeadline = @RegistrationDeadline,
				EntryFee = @EntryFee,
				FeePerEvent = @FeePerEvent,
				Information = @Information,
				VenueId = @VenueId,
				MeetType = @MeetType
			WHERE
				Id = @MeetID

			-- update the start time for existing meet events
			UPDATE MeetEvents SET StartTime = me.StartTime
			FROM MeetEvents m INNER JOIN @MeetEvents me ON m.EventId = me.Id

			-- ADD NEW MEET EVENTS

			INSERT INTO MeetEvents
				SELECT @MeetId, EventId, StartTime FROM @MeetEvents WHERE Id = 0;

			SET @RecordVersion = (SELECT RecordVersion FROM Meet WHERE Id = @MeetId)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
			IF @@TRANCOUNT >0
			ROLLBACK TRAN
		;THROW
	END CATCH
END
GO

--CREATE OR ALTER PROC spUpdateMeet_SC
--	@MeetId INT,
--	@MeetName NVARCHAR(100),
--	@StartDate DATETIME2(7),
--	@EndDate DATETIME2(7),
--	@RegistrationDeadline DATETIME2(7),
--	@EntryFee MONEY,
--	@FeePerEvent MONEY,
--	@Information NTEXT,
--	@VenueId INT
--AS
--BEGIN
--	BEGIN TRY
--		DECLARE @NumMeetsPast30Days INT = 
--			(SELECT 
--				COUNT(*) 
--			FROM 
--				Meet 
--			WHERE 
--				VenueId = @VenueId 
--				AND EndDate < @StartDate
--				AND @StartDate < DATEADD(DAY,30,EndDate)
--				AND Id <> @MeetId)

--		DECLARE @NumMeetsAtVenueYear INT = 
--			(SELECT
--				COUNT(*)
--			FROM
--				Meet
--			WHERE
--				VenueId IN (
--					SELECT 
--						Id
--					FROM
--						Venue
--					WHERE
--						ProvinceId = (
--							SELECT	
--								ProvinceId
--							FROM
--								Venue
--							WHERE
--								Id = @VenueId)
--				)
--				AND DATEPART(year, StartDate) = (SELECT DATEPART(year, @StartDate))
--				AND Id <> @MeetId)

--		IF(@NumMeetsPast30Days > 0)
--			THROW 50001, 'Meet cannot be updated.  At least 1 meet at that venue has an end date less than 30 days from the start date of this meet.', 1
--		ELSE IF(@NumMeetsAtVenueYear >= 3)
--			THROW 50001, 'Meet cannot be updated.  The venue''s province already has 3 meets in the year.', 1
--		ELSE
--			UPDATE Meet SET
--				[Name] = @MeetName,
--				StartDate = @StartDate,
--				EndDate = @EndDate,
--				RegistrationDeadline = @RegistrationDeadline,
--				EntryFee = @EntryFee,
--				FeePerEvent = @FeePerEvent,
--				Information = @Information,
--				VenueId = @VenueId
--			WHERE
--				Id = @MeetID
--	END TRY
--	BEGIN CATCH
--		;THROW
--	END CATCH
--END
--GO

CREATE OR ALTER PROC spInsertMeet_SC
	@MeetId					INT OUTPUT,
	@RecordVersion			ROWVERSION OUTPUT,
	@MeetName				NVARCHAR(100),
	@StartDate				DATETIME2(7),
	@EndDate				DATETIME2(7),
	@RegistrationDeadline	DATETIME2(7),
	@EntryFee				MONEY,
	@FeePerEvent			MONEY,
	@Information			NTEXT,
	@VenueId				INT,
	@MeetType				INT,
	@MeetEvents				MeetEventsTableType READONLY
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO Meet (
				[Name],
				StartDate,
				EndDate,
				RegistrationDeadline,
				EntryFee,
				FeePerEvent,
				Information,
				VenueId,
				MeetType)
			VALUES (
				@MeetName,
				@StartDate,
				@EndDate,
				@RegistrationDeadline,
				@EntryFee,
				@FeePerEvent,
				@Information,
				@VenueId,
				@MeetType)

			SET @MeetId = SCOPE_IDENTITY()

			INSERT INTO MeetEvents
				SELECT @MeetId, EventId, StartTime FROM @MeetEvents
		
			SET @RecordVersion = (SELECT RecordVersion FROM Meet WHERE Id = @MeetId)
		
		COMMIT TRAN

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT >0
			ROLLBACK TRAN
		;THROW
	END CATCH
END
GO

--CREATE OR ALTER PROC spInsertMeet_SC
--	@MeetName NVARCHAR(100),
--	@StartDate DATETIME2(7),
--	@EndDate DATETIME2(7),
--	@RegistrationDeadline DATETIME2(7),
--	@EntryFee MONEY,
--	@FeePerEvent MONEY,
--	@Information NTEXT,
--	@VenueId INT
--AS
--BEGIN
--	BEGIN TRY
--		DECLARE @NumMeetsPast30Days INT = 
--			(SELECT 
--				COUNT(*) 
--			FROM 
--				Meet 
--			WHERE 
--				VenueId = @VenueId 
--				AND EndDate < @StartDate
--				AND @StartDate < DATEADD(DAY,30,EndDate))

--		DECLARE @NumMeetsAtVenueYear INT = 
--			(SELECT
--				COUNT(*)
--			FROM
--				Meet
--			WHERE
--				VenueId IN (
--					SELECT 
--						Id
--					FROM
--						Venue
--					WHERE
--						ProvinceId = (
--							SELECT	
--								ProvinceId
--							FROM
--								Venue
--							WHERE
--								Id = @VenueId)
--				)
--				AND DATEPART(year, StartDate) = (SELECT DATEPART(year, @StartDate)))

--		IF(@NumMeetsPast30Days > 0)
--			THROW 50001, 'Meet cannot be added.  At least 1 meet at that venue has an end date less than 30 days from the start date of this meet.', 1
--		ELSE IF(@NumMeetsAtVenueYear >= 3)
--			THROW 50001, 'Meet cannot be added.  The venue''s province already has 3 meets in the year.', 1
--		ELSE
--			INSERT INTO Meet VALUES (
--				@MeetName,
--				@StartDate,
--				@EndDate,
--				@RegistrationDeadline,
--				@EntryFee,
--				@FeePerEvent,
--				@Information,
--				@VenueId)
--	END TRY
--	BEGIN CATCH
--		;THROW
--	END CATCH
--END
--GO

CREATE OR ALTER PROC spDeleteMeet_SC
	@MeetId INT
AS
BEGIN
	DELETE FROM Meet WHERE Id = @MeetID
END
GO

CREATE OR ALTER PROC spGetMeetEvents_SC
	@MeetId INT
AS
BEGIN
	SELECT 
		Id,
		MeetId,
		EventId,
		StartTime
	FROM
		MeetEvents
	WHERE
		MeetEvents.MeetId = @MeetId
END
GO


CREATE OR ALTER PROC spGetEvents_SC

AS

BEGIN

	SELECT 

		[Event].Id,

		[Event].[Name] AS EventName,

		Discipline.[Name] AS Discipline,

		Division.[Name] AS Division,

		Gender.[Name] AS Gender

	FROM

		[Event]

			INNER JOIN Discipline ON [Event].DisciplineId = Discipline.Id

			INNER JOIN Division ON [Event].DivisionId = Division.Id

			INNER JOIN Gender ON [Event].GenderId = Gender.Id

	ORDER BY

		Discipline,

		EventName,

		Division,

		Gender

END

GO