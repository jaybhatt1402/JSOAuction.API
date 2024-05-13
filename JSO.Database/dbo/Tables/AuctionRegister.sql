CREATE TABLE [dbo].[AuctionRegister] (
    [AuctionId]    INT              IDENTITY (1, 1) NOT NULL,
    [Year]         INT              NULL,
    [Location]     NVARCHAR (MAX)   NULL,
    [StartDate]    DATETIME         NULL,
    [EndDate]      DATETIME         NULL,
    [AuctionName]  VARCHAR (50)     NULL,
    [IsDeleted]    BIT              NULL,
    [IsActive]     BIT              NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    [CreatedOn]    DATETIME         NULL,
    [UpdatedBy]    UNIQUEIDENTIFIER NULL,
    [UpdatedOn]    DATETIME         NULL,
    [StartBid]     DECIMAL (10)     NULL,
    [NextBid]      DECIMAL (10)     NULL,
    [TournamentId] INT              NULL,
    CONSTRAINT [PK_AuctionRegister_New] PRIMARY KEY CLUSTERED ([AuctionId] ASC)
);

