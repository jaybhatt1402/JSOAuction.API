CREATE TABLE [dbo].[UndoBids] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [BidId]     INT              NOT NULL,
    [AuctionId] INT              NULL,
    [UpdatedOn] DATETIME         NULL,
    [CreatedOn] DATETIME         NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_UndoBids] PRIMARY KEY CLUSTERED ([Id] ASC)
);

