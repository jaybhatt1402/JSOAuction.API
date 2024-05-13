
CREATE PROCEDURE [dbo].[GetAuctionTeamsList]
    -- Add the parameters for the stored procedure here
    @AuctionId int,
    @PlayerId int,
	@TournamentId int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    DECLARE @RemainingAmount decimal(10, 2)
    DECLARE @TeamName varchar(50)
	DECLARE @BasePrice decimal(10, 2) = 300000

    CREATE TABLE #tblAuctionTeam (
        Id int,
        TeamName varchar(50),
        CaptainName varchar(50),
        CoachName varchar(50),
        TotalBalance decimal(10, 2),
        IsActive bit,
		TeamLogo nvarchar(max),
		MaximumBid decimal(10,2),
        RemainingAmount decimal(10, 2)
    )

    INSERT INTO #tblAuctionTeam
    SELECT
        team.TeamId,
        team.TeamName,
        team.CaptainName,
        team.CoachName,
        team.TotalBalance,
        team.IsActive,
		team.TeamLogo,
		team.TotalBalance - (((15 - 1) - team.TeamSize) * @BasePrice) AS MaximumBid,
        --team.TotalBalance - ISNULL((SELECT TOP 1 BidAmount FROM [dbo].[Bids] WHERE TeamId = team.TeamId AND AuctionId = @AuctionId AND PlayerId = @PlayerId AND Sold = 0 AND IsDeleted = 0 ORDER BY CreatedOn DESC), 0) as RemainingAmount
		team.TotalBalance - ISNULL((SELECT TOP 1 
                                    CASE WHEN pr.PlayerCategory != 'I' THEN BidAmount ELSE 0 END 
                                FROM [dbo].[Bids] 
                                WHERE TeamId = team.TeamId 
                                    AND AuctionId = @AuctionId 
                                    AND PlayerId = @PlayerId 
                                    AND Sold = 0 
                                    AND IsDeleted = 0 
                                ORDER BY CreatedOn DESC), 0) as RemainingAmount
    FROM
        [dbo].[TeamRegister] team WITH(NOLOCK)
    LEFT JOIN
        Bids bids ON team.TeamId = bids.TeamId AND bids.AuctionId = @AuctionId AND bids.Sold = 1 AND bids.IsDeleted = 0
	LEFT JOIN
		PlayerRegister pr ON pr.PlayerRegisterId = @PlayerId
	Where
		team.IsActive = 1
		AND
		team.IsDeleted = 0
		AND
		team.TournamentId = @TournamentId
    GROUP BY
        team.TeamId,
        team.TeamName,
        team.CaptainName,
        team.CoachName,
        team.TotalBalance,
        team.IsActive,
		team.TeamLogo,
		team.MaximumBid,
		pr.PlayerCategory,
		team.TeamSize;

    -- Insert statements for procedure here
	DECLARE @BidAmount int = 50000;

    SELECT
    team.Id as TeamId,
    team.TeamName,
    team.RemainingAmount,
    team.TeamLogo,
    team.MaximumBid,
    --CAST(CASE WHEN (team.RemainingAmount > ISNULL((SELECT TOP 1 BidAmount FROM [dbo].[Bids] WHERE TeamId = team.Id AND AuctionId = @AuctionId AND PlayerId = @PlayerId AND Sold = 0 AND IsDeleted = 0 ORDER BY CreatedOn DESC), 0)) THEN 1 ELSE 0 END AS BIT) AS IsBiddable
    --CAST(CASE WHEN (team.RemainingAmount > team.MaximumBid) THEN 1 ELSE 0 END AS BIT) AS IsBiddable	
	--CAST(
 --       CASE 
 --           WHEN team.MaximumBid >
	--		ISNULL((
 --               SELECT TOP 1 BidAmount 
 --               FROM [dbo].[Bids] 
 --               WHERE TeamId = team.Id 
 --                   AND AuctionId = @AuctionId 
 --                   AND PlayerId = @PlayerId 
 --                   AND Sold = 0 
 --                   AND IsDeleted = 0 
 --               ORDER BY CreatedOn DESC
 --           ), 0)
 --           THEN 1 
 --           ELSE 0 
 --       END 
 --       AS BIT
 --   ) AS IsBiddable
	CAST(1 AS BIT) AS IsBiddable
	FROM
    #tblAuctionTeam team
    WHERE
		team.IsActive = 1
		AND
		NOT EXISTS (
			SELECT 1
			FROM [dbo].[Bids] soldBids
			WHERE soldBids.PlayerId = @PlayerId
			  AND soldBids.Sold = 1
		)
    GROUP BY
		team.Id,
        team.TeamName,
        team.RemainingAmount,
		team.TeamLogo,
		team.MaximumBid

    DROP TABLE #tblAuctionTeam

END
