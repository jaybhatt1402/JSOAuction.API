-- EXEC [dbo].[GetAllPlayerDetailsWithTournamentID]  
CREATE PROCEDURE [dbo].[GetAllPlayerDetailsWithTournamentID] 
@TournamentId int = null
AS
BEGIN
	select PlayerRegisterId, FirstName, LastName, Gender, MobileNo, AlternativePhoneNo, T0.Email, DOB, Batsman, Bowler, WicketKeeper, BattingAllRounder, BowlingAllRounder, PreviousTeamId,
	LastPlayedYear, ProfilePicture, [Password], BasePrice, WinningBid, T0.UpdatedOn, T0.CreatedOn, T0.CreatedBy, T0.UpdatedBy, T0.IsDeleted, T0.IsActive, T0.City, T0.PlayerCategory, T0.PlayerNo , T0.BattingStyle , T0.BowlingStyle , team.TeamName
	from [dbo].PlayerRegister T0 WITH (NOLOCK)
	INNER JOIN [dbo].AuctionPlayerMapping T1 WITH (NOLOCK)
	ON T0.PlayerRegisterId = T1.PlayerId
	LEFT JOIN [dbo].TeamRegister AS team
	ON team.TeamId = T0.PreviousTeamId
	Where 
	(@TournamentId IS NULL OR @TournamentId = T1.TournamentId) AND
	T0.IsDeleted = 0
	ORDER BY T0.CreatedOn DESC
END