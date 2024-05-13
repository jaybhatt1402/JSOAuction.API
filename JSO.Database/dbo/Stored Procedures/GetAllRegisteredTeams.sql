CREATE PROCEDURE [dbo].[GetAllRegisteredTeams] 
	@AuctionId int
AS
BEGIN
	SELECT * from [dbo].TeamRegister Where AuctionId = @AuctionId
END
