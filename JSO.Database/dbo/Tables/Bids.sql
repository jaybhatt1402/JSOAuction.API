CREATE TABLE [dbo].[Bids] (
    [BidId]     INT              IDENTITY (1, 1) NOT NULL,
    [PlayerId]  INT              NULL,
    [TeamId]    INT              NULL,
    [AuctionId] INT              NULL,
    [BidAmount] DECIMAL (10, 2)  NULL,
    [IsDeleted] BIT              NULL,
    [Sold]      BIT              NULL,
    [UpdatedOn] DATETIME         NULL,
    [CreatedOn] DATETIME         NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY CLUSTERED ([BidId] ASC),
    CONSTRAINT [FK_Bids_AuctionRegister] FOREIGN KEY ([AuctionId]) REFERENCES [dbo].[AuctionRegister] ([AuctionId]),
    CONSTRAINT [FK_Bids_PlayerRegister] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[PlayerRegister] ([PlayerRegisterId]),
    CONSTRAINT [FK_Bids_TeamRegister] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[TeamRegister] ([TeamId])
);

