CREATE TABLE [dbo].[AuctionPlayerMapping] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [PlayerId]      INT              NULL,
    [AuctionId]     INT              NULL,
    [PlayerStatus]  VARCHAR (500)    NULL,
    [ShowSoldPopup] VARCHAR (500)    CONSTRAINT [DF_AuctionPlayerMapping_ShowSoldPopup] DEFAULT ((0)) NULL,
    [CreatedBy]     UNIQUEIDENTIFIER CONSTRAINT [DF_AuctionPlayerMapping_CreatedBy] DEFAULT ('E39F47A6-1C9B-4BB7-8AB1-67D6B8BB541B') NULL,
    [CreatedOn]     DATETIME         CONSTRAINT [DF_AuctionPlayerMapping_CreatedOn] DEFAULT (getdate()) NULL,
    [UpdatedBy]     UNIQUEIDENTIFIER CONSTRAINT [DF_AuctionPlayerMapping_UpdatedBy] DEFAULT ('E39F47A6-1C9B-4BB7-8AB1-67D6B8BB541B') NULL,
    [UpdatedOn]     DATETIME         CONSTRAINT [DF_AuctionPlayerMapping_UpdatedOn] DEFAULT (getdate()) NULL,
    [TeamId]        INT              NULL,
    [UnsoldCount]   INT              NULL,
    [TournamentId]  INT              NULL,
    CONSTRAINT [PK_AuctionPlayerMapping] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuctionPlayerMapping_AuctionRegister] FOREIGN KEY ([AuctionId]) REFERENCES [dbo].[AuctionRegister] ([AuctionId]),
    CONSTRAINT [FK_AuctionPlayerMapping_PlayerRegister] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[PlayerRegister] ([PlayerRegisterId]),
    CONSTRAINT [FK_AuctionPlayerMapping_TeamRegister] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[TeamRegister] ([TeamId])
);

