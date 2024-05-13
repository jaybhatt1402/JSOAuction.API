CREATE TABLE [dbo].[RefreshToken] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [UserId]          UNIQUEIDENTIFIER NOT NULL,
    [Token]           NVARCHAR (500)   NOT NULL,
    [Expires]         DATETIME2 (7)    NOT NULL,
    [Created]         DATETIME2 (7)    NOT NULL,
    [CreatedByIp]     NVARCHAR (50)    NULL,
    [Revoked]         DATETIME2 (7)    NULL,
    [RevokedByIp]     NVARCHAR (50)    NULL,
    [ReplacedByToken] NVARCHAR (500)   NULL,
    [ReasonRevoked]   NVARCHAR (500)   NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);

