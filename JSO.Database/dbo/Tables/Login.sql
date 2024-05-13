CREATE TABLE [dbo].[Login] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [RegisterId]  UNIQUEIDENTIFIER NULL,
    [Username]    VARCHAR (50)     NULL,
    [Password]    VARCHAR (50)     NULL,
    [NewPassword] VARCHAR (50)     NULL,
    [UserType]    VARCHAR (50)     NULL,
    [LoginDate]   DATETIME         NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([Id] ASC)
);

