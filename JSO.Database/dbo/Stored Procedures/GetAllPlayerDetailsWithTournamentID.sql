-- EXEC [dbo].[GetAllPlayerDetailsWithTournamentID]  
CREATE PROCEDURE [dbo].[GetAllPlayerDetailsWithTournamentID] 
@TournamentId int = null
AS
BEGIN
	select PlayerRegisterId, FirstName, LastName, Gender, MobileNo, AlternativePhoneNo, Email, DOB, Batsman, Bowler, WicketKeeper, BattingAllRounder, BowlingAllRounder, PreviousTeamId,
	LastPlayedYear, ProfilePicture, [Password], BasePrice, WinningBid, T0.UpdatedOn, T0.CreatedOn, T0.CreatedBy, T0.UpdatedBy, IsDeleted, IsActive, T0.City, T0.PlayerCategory, T0.PlayerNo
	from [dbo].PlayerRegister T0 WITH (NOLOCK)
	INNER JOIN [dbo].AuctionPlayerMapping T1 WITH (NOLOCK)
	ON T0.PlayerRegisterId = T1.PlayerId
	Where 
	(@TournamentId IS NULL OR @TournamentId = T1.TournamentId) AND
	IsDeleted = 0
	ORDER BY T0.CreatedOn DESC
END