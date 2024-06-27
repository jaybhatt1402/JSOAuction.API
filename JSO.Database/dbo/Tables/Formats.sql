CREATE TABLE [dbo].[Formats] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [FormatName] VARCHAR (500)    NULL,
    [IsActive]   BIT              NULL,
    [IsDeleted]  BIT              NULL,
    [CreatedOn]  DATETIME         NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NULL,
    [UpdatedOn]  DATETIME         NULL,
    [UpdatedBy]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY CLUSTERED ([Id] ASC)
);

