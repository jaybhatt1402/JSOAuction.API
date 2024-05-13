CREATE TABLE [dbo].[Groups] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [GroupName]    VARCHAR (50)     NULL,
    [BasePrice]    DECIMAL (10, 2)  NULL,
    [IsActive]     BIT              NULL,
    [IsDeleted]    BIT              NULL,
    [CreatedOn]    DATETIME         NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    [UpdatedOn]    DATETIME         NULL,
    [UpdatedBy]    UNIQUEIDENTIFIER NULL,
    [TournamentId] INT              NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_TournamentRegister] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[TournamentRegister] ([TournamentId])
);

