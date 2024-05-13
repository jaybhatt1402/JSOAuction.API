-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UndoAuctionBid]
	-- Add the parameters for the stored procedure here
	@AuctionId INT, 
	@BidId INT,
	@Success INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @Success = 0
    -- Insert statements for procedure here
	UPDATE Bids SET IsDeleted = 1 where AuctionId = @AuctionId AND BidId = @BidId

    -- Check if the record exists and meets the update criteria
    IF EXISTS (SELECT 1 FROM Bids WHERE AuctionId = @AuctionId AND BidId = @BidId)
    BEGIN
        -- Update the record
        UPDATE Bids SET IsDeleted = 1 WHERE AuctionId = @AuctionId AND BidId = @BidId;

        -- Check if the record was updated
        IF EXISTS (SELECT 1 FROM Bids WHERE AuctionId = @AuctionId AND BidId = @BidId AND IsDeleted = 1)
        BEGIN
			Insert Into UndoBids(BidId, AuctionId) Values (@BidId, @AuctionId)
            SET @Success = 1; -- Record was successfully updated
        END
        ELSE
        BEGIN
            SET @Success = 0; -- Record was not updated
        END
    END
    ELSE
    BEGIN
        SET @Success = 0; -- Record does not exist
    END

END
