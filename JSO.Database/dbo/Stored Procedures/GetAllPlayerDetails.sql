-- EXEC [dbo].[GetAllPlayerDetails]  
CREATE PROCEDURE [dbo].[GetAllPlayerDetails] 
@AuctionId int = null
AS
BEGIN
	select PlayerRegisterId, FirstName, LastName, Gender, MobileNo, AlternativePhoneNo, Email, DOB, Batsman, Bowler, WicketKeeper, BattingAllRounder, BowlingAllRounder, PreviousTeamId,
	LastPlayedYear, ProfilePicture, [Password], BasePrice, WinningBid, T0.UpdatedOn, T0.CreatedOn, T0.CreatedBy, T0.UpdatedBy, IsDeleted, IsActive, T0.City, T0.PlayerCategory, T0.PlayerNo
	from [dbo].PlayerRegister T0 WITH (NOLOCK)
	INNER JOIN [dbo].AuctionPlayerMapping T1 WITH (NOLOCK)
	ON T0.PlayerRegisterId = T1.PlayerId
	Where 
	(@AuctionId IS NULL OR @AuctionId = T1.AuctionId) AND
	IsDeleted = 0
	ORDER BY CASE T0.PlayerCategory WHEN 'I' THEN 1
					WHEN 'A' THEN 2
					WHEN 'B' THEN 3
					ELSE 4 END;  
END
