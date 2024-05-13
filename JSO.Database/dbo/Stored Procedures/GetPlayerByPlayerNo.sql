-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetPlayerByPlayerNo]
(
    -- Add the parameters for the stored procedure here

	@AuctionId INT,
    @PlayerNo INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    IF NOT EXISTS (
            SELECT 1
            FROM AuctionPlayerMapping
            WHERE AuctionId = @AuctionId
            AND PlayerStatus IN ('notdisclosed', 'disclosed')
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
				pm.ShowSoldPopup 
			FROM 
				PlayerRegister pr WITH(NOLOCK)
			JOIN
				AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
			LEFT JOIN 
				TeamRegister tr ON tr.TeamId = pr.PreviousTeamId
			WHERE
				pm.AuctionId = @AuctionId
				AND
				pr.PlayerNo = @PlayerNo
				AND
				pm.PlayerStatus = 'unsold'
				AND
				pm.UnsoldCount = 1
				AND
				pr.IsDeleted = 0
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
				pm.ShowSoldPopup 
			FROM 
				PlayerRegister pr WITH(NOLOCK)
			JOIN
				AuctionPlayerMapping pm ON pm.PlayerId = pr.PlayerRegisterId
			LEFT JOIN 
				TeamRegister tr ON tr.TeamId = pr.PreviousTeamId
			WHERE
				pm.AuctionId = @AuctionId
				AND
				pr.PlayerNo = @PlayerNo
				AND
				pm.PlayerStatus NOT IN ('sold', 'unsold')
				AND
				pr.IsDeleted = 0
			ORDER BY 
				CASE 
				    WHEN pr.SortByType = 'Batsman' THEN 1
				    WHEN pr.SortByType = 'Bowl' THEN 2
					WHEN pr.SortByType = 'WK' THEN 3
					WHEN pr.SortByType = 'Allrounder' THEN 4
					ELSE 5 
				END
				,pr.SortByIndex ASC
				
		END
END
