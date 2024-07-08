CREATE TABLE [dbo].[SignUp] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       VARCHAR (50)   NULL,
    [LastName]        VARCHAR (50)   NULL,
    [EmailId]         NVARCHAR (50)  NULL,
    [Mobile]          NVARCHAR (50)  NULL,
    [NewPassword]     NVARCHAR (MAX) NULL,
    [ConfirmPassword] NVARCHAR (MAX) NULL,
    [IsDeleted]       BIT            NULL
);



