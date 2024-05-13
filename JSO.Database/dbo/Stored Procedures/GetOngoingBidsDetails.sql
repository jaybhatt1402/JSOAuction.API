
CREATE PROCEDURE [dbo].[GetOngoingBidsDetails]
	-- Add the parameters for the stored procedure here
	@AuctionId int,
	@PlayerId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		bid.BidId AS BidId,
		bid.BidAmount AS BidAmount,
		team.TeamName AS TeamName,
		team.TeamId AS TeamId
	FROM 
		[dbo].[Bids] bid WITH(NOLOCK)
	JOIN
		TeamRegister team ON team.TeamId = bid.TeamId
	WHERE 
		bid.Sold = 0
		AND 
		bid.PlayerId = @PlayerId
		AND
		bid.IsDeleted = 0
		AND
		NOT EXISTS (
			SELECT 1
			FROM [dbo].[Bids] soldBids
			WHERE soldBids.PlayerId = @PlayerId
			  AND soldBids.Sold = 1 
		)
		AND 
		bid.AuctionId = @AuctionId
END
