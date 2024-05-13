-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAuctionPlayerDetails]
	-- Add the parameters for the stored procedure here
	@AuctionId INT,
	@ScreenType VARCHAR(500),
	@PlayerNo INT = null,
	@PlayerCategory VARCHAR(50) = null,
	@TournamentId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@ScreenType = 'admin')
	BEGIN
		IF NOT EXISTS (
            SELECT 1
            FROM AuctionPlayerMapping
            WHERE TournamentId = @TournamentId
            AND PlayerStatus IN ('notdisclosed', 'disclosed', 'unsold')
			AND (UnsoldCount IN (0, 1) OR UnsoldCount IS NULL)
        )
		BEGIN
			--SET @ErrorMessage = 'All players have been sold in this auction.'
			RETURN 1
		END
		
		IF EXISTS (
            SELECT 1
			FROM
			PlayerRegister pr WITH(NOLOCK) 
			JOIN AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
            WHERE TournamentId = @TournamentId AND (pr.PlayerNo = @PlayerNo)
            AND PlayerStatus IN ('notdisclosed', 'disclosed', 'unsold')
			AND pm.UnsoldCount IN (0, 1)
        )
		BEGIN
			SELECT 
				TOP 1 
				pr.PlayerRegisterId,
				pr.FirstName,
				pr.LastName,
				pr.Gender,
				pr.MobileNo,
				pr.AlternativePhoneNo,
				pr.City,
				pr.Email,
				pr.DOB,
				pr.Batsman,
				pr.Bowler,
				pr.WicketKeeper,
				pr.BattingAllRounder,
				pr.BowlingAllRounder,
				pr.PreviousTeamId,
				pr.LastPlayedYear,
				pr.ProfilePicture,
				pr.BasePrice,
				tr.TeamName,
				pm.PlayerStatus,
				pm.UnsoldCount,
				tr.TeamLogo,
				CAST(1 AS BIT) as IsVideoAvailable,
				pm.ShowSoldPopup,
				pr.PlayerNo,
				pr.PlayerCategory
			FROM 
				PlayerRegister pr WITH(NOLOCK)
			JOIN
				AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
			LEFT JOIN 
				TeamRegister tr ON tr.TeamId = pr.PreviousTeamId
			WHERE
				pm.TournamentId = @TournamentId
				AND
				pr.IsDeleted = 0
				AND
                (@PlayerNo IS NULL OR pr.PlayerNo = @PlayerNo)
                AND
                (@PlayerCategory IS NULL OR pr.PlayerCategory = @PlayerCategory)
		END
		ELSE
		BEGIN
			SELECT 
				TOP 1 
				pr.PlayerRegisterId,
				pr.FirstName,
				pr.LastName,
				pr.Gender,
				pr.MobileNo,
				pr.AlternativePhoneNo,
				pr.Email,
				pr.DOB,
				pr.City,
				pr.Batsman,
				pr.Bowler,
				pr.WicketKeeper,
				pr.BattingAllRounder,
				pr.BowlingAllRounder,
				pr.PreviousTeamId,
				pr.LastPlayedYear,
				pr.ProfilePicture,
				pr.BasePrice,
				tr.TeamName,
				pm.PlayerStatus,
				pm.UnsoldCount,
				tr.TeamLogo,
				CAST(1 AS BIT) as IsVideoAvailable,
				pm.ShowSoldPopup,
				pr.PlayerNo,
				pr.PlayerCategory
			FROM 
				PlayerRegister pr WITH(NOLOCK)
			JOIN
				AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
			LEFT JOIN 
				TeamRegister tr ON tr.TeamId = pr.PreviousTeamId
			WHERE
				pm.TournamentId = @TournamentId
				AND
				pm.PlayerStatus NOT IN ('sold', 'unsold')
				AND
				pr.IsDeleted = 0
				AND
                (@PlayerNo IS NULL OR pr.PlayerNo = @PlayerNo)
                AND
                (@PlayerCategory IS NULL OR pr.PlayerCategory = @PlayerCategory)
			ORDER BY 
				CASE 
				    WHEN pr.SortByType = 'Batsman' THEN 1
				    WHEN pr.SortByType = 'Bowl' THEN 2
				    WHEN pr.SortByType = 'WK' THEN 3
				    WHEN pr.SortByType = 'Allrounder' THEN 4
				    ELSE 5 
				END
				,pr.SortByIndex ASC
				,CASE WHEN @AuctionId = 1 THEN 1 ELSE 0 END DESC
				
		END
	END
	ELSE IF (@ScreenType = 'user')   
	BEGIN
		SELECT 
			TOP 1 
			pr.PlayerRegisterId,
			pr.FirstName,
			pr.LastName,
			pr.Gender,
			pr.MobileNo,
			pr.AlternativePhoneNo,
			pr.Email,
			pr.DOB,
			pr.City,
			pr.Batsman,
			pr.Bowler,
			pr.WicketKeeper,
			pr.BattingAllRounder,
			pr.BowlingAllRounder,
			pr.PreviousTeamId,
			pr.LastPlayedYear,
			pr.ProfilePicture,
			pr.BasePrice,
			tr.TeamName,
			bid.BidAmount,
			tr.TeamSize,
			CASE 
			    WHEN pr.PlayerCategory <> 'I'
			    THEN tr.TotalBalance - ISNULL((SELECT TOP 1 BidAmount FROM [dbo].[Bids] WHERE TeamId = tr.TeamId AND AuctionId = @AuctionId AND PlayerId = pr.PlayerRegisterId AND Sold = 0 AND IsDeleted = 0 ORDER BY CreatedOn DESC), 0)
			    ELSE tr.TotalBalance
			END AS RemainingBalance,
			tr.MaximumBid,
			pm.PlayerStatus,
			pm.UnsoldCount,
			tr.TeamLogo,
			CAST(1 AS BIT) as IsVideoAvailable,
			pm.ShowSoldPopup,
			tm.TeamName AS OldTeamName,
			pr.PlayerCategory,
			pr.PlayerNo
		FROM 
			PlayerRegister pr WITH(NOLOCK)
		JOIN
			AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
		LEFT JOIN
			Bids bid ON bid.PlayerId = pr.PlayerRegisterId AND bid.IsDeleted = 0
		LEFT JOIN 
			TeamRegister tr ON tr.TeamId = bid.TeamId
		LEFT JOIN
			TeamRegister tm ON tm.TeamId = pr.PreviousTeamId
		WHERE
			pm.TournamentId = @TournamentId
			AND
			(pm.PlayerStatus = 'disclosed' OR (pm.PlayerStatus = 'sold' AND pm.ShowSoldPopup = 'sold') OR (pm.PlayerStatus = 'unsold' AND pm.ShowSoldPopup = 'unsold'))
			AND
			pr.IsDeleted = 0
		ORDER BY
			bid.BidAmount DESC; 

	END
END


