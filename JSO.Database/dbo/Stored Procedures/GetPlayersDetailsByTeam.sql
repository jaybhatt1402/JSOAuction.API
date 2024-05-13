-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetPlayersDetailsByTeam]
(
	@AuctionId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    SELECT 
	CONCAT(pr.FirstName, ' ', pr.LastName) AS FullName,
	pr.MobileNo,
	pr.Batsman,
	pr.BattingAllRounder,
	pr.Bowler,
	pr.BowlingAllRounder,
	bid.BidAmount,
	tr.TeamName,
	tr.TeamLogo,
	tr.TeamId,
	pm.PlayerId,
	pr.WicketKeeper,
	pr.PlayerCategory,
	pr.PlayerNo
	FROM 
		AuctionPlayerMapping pm WITH(NOLOCK)
	LEFT JOIN
		PlayerRegister pr ON pr.PlayerRegisterId = pm.PlayerId
	LEFT JOIN 
		TeamRegister tr ON tr.TeamId = pm.TeamId
	LEFT JOIN 
		Bids bid ON bid.PlayerId = pm.PlayerId
				 AND bid.TeamId = tr.TeamId
				 AND bid.Sold = 1
				 AND bid.IsDeleted = 0
	WHERE
		pm.PlayerStatus = 'sold'
		AND
		pm.AuctionId = @AuctionId
		AND
		pm.TeamId IS NOT NULL
	GROUP BY
	    tr.TeamName,
		CONCAT(pr.FirstName, ' ', pr.LastName),
	    pr.MobileNo,
	    pr.Batsman,
	    pr.BattingAllRounder,
	    pr.Bowler,
	    pr.BowlingAllRounder,
	    bid.BidAmount,
	    tr.TeamLogo,
		tr.TeamId,
		pm.PlayerId,
		pr.WicketKeeper,
		pr.PlayerCategory,
		pr.PlayerNo
	
	END
