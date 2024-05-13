
CREATE PROCEDURE [dbo].[GetAllTournamentDetails] 
AS
BEGIN
	SELECT * from [dbo].TournamentRegister Where IsActive = 1 Order By CreatedOn Desc
END
