CREATE PROCEDURE [dbo].[ManageTournament]
(
    @TournamentId INT = null,
    @TournamentName NVARCHAR(50)  = null,
    @Description NVARCHAR(MAX) = null,
    @OrganizerName VARCHAR(50) = null,
    @OrganizerContact NVARCHAR(50) = null,
    @OrganizerEmail NVARCHAR(MAX) = null,
    @StartDate DATETIME = null,
    @EndDate DATETIME = null,
    @DueDate DATETIME = null,
    @DueTime DATETIME = null,
    @GroundAddress NVARCHAR(MAX) = null,
    @City VARCHAR(100) = null,
    @State VARCHAR(100) = null,
    @Country VARCHAR(100) = null,
    @ZipCode NVARCHAR(100) = null,
    @UploadBanner NVARCHAR(MAX) = null,
    @UploadLogo NVARCHAR(MAX) = null,
    @Open BIT = null,
    @Corporate BIT = null,
    @Community BIT = null,
    @School BIT = null,
    @BoxCricket BIT = null,
    @Series BIT = null,
    @Other BIT = null,
    @BallType VARCHAR(50) = null,
    @Overs INT = null,
    @Format NVARCHAR(MAX) = null,
    @MaxTeams INT = null,
    @Gender VARCHAR(50) = null,
    @MinPlayer INT = null,
    @MaxPlayer INT = null,
    @PaymentTerms NVARCHAR(50) = null,
    @Amount DECIMAL(10, 2) = null,
    @IsActive BIT = null,
    @IsDeleted BIT = null,
	@Message VARCHAR(MAX) OUTPUT,
	@Success INT OUTPUT
)
AS
BEGIN
	SET @Success = 0;

    IF @TournamentId IS NULL
    BEGIN
        INSERT INTO TournamentRegister (
            TournamentName, [Description], OrganizerName, OrganizerContact, OrganizerEmail, 
            StartDate, EndDate, DueDate, DueTime, GroundAddress, City, [State], Country, ZipCode, 
            UploadBanner, UploadLogo, [Open], Corporate, Community, School, BoxCricket, Series, Other, 
            BallType, Overs, [Format], MaxTeams, Gender, MinPlayer, MaxPlayer, PaymentTerms, Amount, 
            CreatedOn, CreatedBy, UpdatedOn, UpdatedBy, IsActive, IsDeleted
        )
        VALUES (
            @TournamentName, @Description, @OrganizerName, @OrganizerContact, @OrganizerEmail, 
            @StartDate, @EndDate, @DueDate, @DueTime, @GroundAddress, @City, @State, @Country, @ZipCode, 
            @UploadBanner, @UploadLogo, @Open, @Corporate, @Community, @School, @BoxCricket, @Series, @Other, 
            @BallType, @Overs, @Format, @MaxTeams, @Gender, @MinPlayer, @MaxPlayer, @PaymentTerms, @Amount, 
            GETUTCDATE(), null, null, null, @IsActive, @IsDeleted
        );
		SET @Message = 'Inserted Successfully';
		SET @Success = 1;
    END
    ELSE
    BEGIN
		IF EXISTS(SELECT 1 FROM TournamentRegister WHERE TournamentId = @TournamentId) 
		BEGIN
			UPDATE TournamentRegister
			SET
			    TournamentName = @TournamentName,
			    [Description] = @Description,
			    OrganizerName = @OrganizerName,
			    OrganizerContact = @OrganizerContact,
			    OrganizerEmail = @OrganizerEmail,
			    StartDate = @StartDate,
			    EndDate = @EndDate,
			    DueDate = @DueDate,
			    DueTime = @DueTime,
			    GroundAddress = @GroundAddress,
			    City = @City,
			    [State] = @State,
			    Country = @Country,
			    ZipCode = @ZipCode,
			    UploadBanner = @UploadBanner,
			    UploadLogo = @UploadLogo,
			    [Open] = @Open,
			    Corporate = @Corporate,
			    Community = @Community,
			    School = @School,
			    BoxCricket = @BoxCricket,
			    Series = @Series,
			    Other = @Other,
			    BallType = @BallType,
			    Overs = @Overs,
			    [Format] = @Format,
			    MaxTeams = @MaxTeams,
			    Gender = @Gender,
			    MinPlayer = @MinPlayer,
			    MaxPlayer = @MaxPlayer,
			    PaymentTerms = @PaymentTerms,
			    Amount = @Amount,
			    CreatedOn = GETUTCDATE(),
			    IsActive = @IsActive,
			    IsDeleted = @IsDeleted
			WHERE TournamentId = @TournamentId;

			SET @Message = 'Updated Succesfully';
			SET @Success = 1;
		END
		ELSE
		BEGIN
			SET @Message = 'No record found to update';
			SET @Success = 0;
		END
    END
END