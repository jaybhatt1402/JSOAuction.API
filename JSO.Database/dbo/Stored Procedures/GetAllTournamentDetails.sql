CREATE PROCEDURE [dbo].[GetAllTournamentDetails] 
AS
BEGIN
	SELECT * from [dbo].TournamentRegister Where IsDeleted = 0 and IsActive = 1 Order By CreatedOn Desc
END
