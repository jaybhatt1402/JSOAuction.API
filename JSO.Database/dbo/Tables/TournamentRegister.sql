CREATE TABLE [dbo].[TournamentRegister] (
    [TournamentId]      INT              IDENTITY (1, 1) NOT NULL,
    [TournamentName]    NVARCHAR (50)    NULL,
    [Description]       NVARCHAR (MAX)   NULL,
    [OrganizerName]     VARCHAR (50)     NULL,
    [OrganizerContact]  NVARCHAR (50)    NULL,
    [OrganizerEmail]    NVARCHAR (MAX)   NULL,
    [StartDate]         DATETIME         NULL,
    [EndDate]           DATETIME         NULL,
    [DueDate]           DATETIME         NULL,
    [DueTime]           DATETIME         NULL,
    [GroundAddress]     NVARCHAR (MAX)   NULL,
    [City]              VARCHAR (100)    NULL,
    [State]             VARCHAR (100)    NULL,
    [Country]           VARCHAR (100)    NULL,
    [ZipCode]           NVARCHAR (100)   NULL,
    [UploadBanner]      NVARCHAR (MAX)   NULL,
    [UploadLogo]        NVARCHAR (MAX)   NULL,
    [Open]              BIT              NULL,
    [Corporate]         BIT              NULL,
    [Community]         BIT              NULL,
    [School]            BIT              NULL,
    [BoxCricket]        BIT              NULL,
    [Series]            BIT              NULL,
    [Other]             BIT              NULL,
    [BallType]          VARCHAR (50)     NULL,
    [Overs]             INT              NULL,
    [Format]            NVARCHAR (MAX)   NULL,
    [MaxTeams]          INT              NULL,
    [Gender]            VARCHAR (50)     NULL,
    [MinPlayer]         INT              NULL,
    [MaxPlayer]         INT              NULL,
    [PaymentTerms]      NVARCHAR (50)    NULL,
    [Amount]            DECIMAL (10, 2)  NULL,
    [CreatedOn]         DATETIME         NULL,
    [CreatedBy]         UNIQUEIDENTIFIER NULL,
    [UpdatedOn]         DATETIME         NULL,
    [UpdatedBy]         UNIQUEIDENTIFIER NULL,
    [IsActive]          BIT              NULL,
    [IsDeleted]         BIT              NULL,
    [TournamentGuid]    UNIQUEIDENTIFIER NULL,
    [BidAmount]         DECIMAL (10, 2)  NULL,
    [PlayerOrderBy]     VARCHAR (50)     NULL,
    [TotalBalance]      DECIMAL (10, 2)  NULL,
    [AuctionStartDate]  DATETIME         NULL,
    [AuctionStartTime]  DATETIME         NULL,
    [IsStart]           BIT              NULL,
    [CurrentTeamLength] INT              NULL,
    [FormatId]          INT              NULL,
    CONSTRAINT [PK_TournamentRegister] PRIMARY KEY CLUSTERED ([TournamentId] ASC),
    CONSTRAINT [FK_TournamentRegister_Formats] FOREIGN KEY ([FormatId]) REFERENCES [dbo].[Formats] ([Id])
);





















