CREATE TABLE [dbo].[Matches] (
    [MatchId]   INT              IDENTITY (1, 1) NOT NULL,
    [TeamA]     NVARCHAR (500)   NULL,
    [TeamB]     NVARCHAR (500)   NULL,
    [NoOfOvers] INT              NULL,
    [City]      VARCHAR (500)    NULL,
    [Time]      TIME (7)         NULL,
    [BowlType]  VARCHAR (50)     NULL,
    [Sponsers]  NVARCHAR (MAX)   NULL,
    [IsActive]  BIT              NULL,
    [IsDeleted] BIT              NULL,
    [CreatedOn] DATETIME         NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedOn] DATETIME         NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL,
    [Ground]    VARCHAR (50)     NULL,
    [Date]      DATETIME         NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED ([MatchId] ASC)
);

