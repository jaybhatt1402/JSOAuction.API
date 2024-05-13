
CREATE PROCEDURE [dbo].[BidSave]
    @TeamId int,
    @PlayerId int,
    @AuctionId int,
    @BidAmount DECIMAL(10,2),
    --@ModifiedBy NVARCHAR(256),
    @ErrorMessage NVARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @InsertResult INT;
	DECLARE @LastBidAmount DECIMAL(10,2);
	DECLARE @MaxBidAmount DECIMAL(10,2);
	DECLARE @NewBidAmount DECIMAL(10,2);

	-- Retrieve the last bid amount for the auction
    SELECT TOP 1 @LastBidAmount = BidAmount
    FROM [dbo].Bids
    WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId AND IsDeleted = 0
    ORDER BY BidId DESC;

	IF @LastBidAmount IS NULL
    BEGIN
        SELECT @LastBidAmount = BasePrice FROM PlayerRegister WITH(NOLOCK) WHERE PlayerRegisterId = @PlayerId
		SET @NewBidAmount = @LastBidAmount;
    END
	ELSE 
	BEGIN
	  SET @NewBidAmount = @LastBidAmount + @BidAmount;
	END

	SELECT @MaxBidAmount = MaximumBid
    FROM [dbo].TeamRegister
    WHERE TeamId = @TeamId;

	IF @NewBidAmount > @MaxBidAmount
    BEGIN
        SET @ErrorMessage = 'Error: New bid amount exceeds maximum bid amount for the team.';
        RETURN; -- Exit the stored procedure
    END;

    INSERT INTO [dbo].Bids
                    (PlayerId,
                    TeamId,
                    AuctionId,
                    BidAmount,
                    CreatedOn,
					IsDeleted,
					Sold)
    VALUES
                (
					@PlayerId,
                    @TeamId,
                    @AuctionId,
                    @NewBidAmount,
                    GETDATE(),
					0,
					0
                );
	IF EXISTS (
	    SELECT 1
	    FROM [dbo].Bids
	    WHERE PlayerId = @PlayerId
	        AND TeamId = @TeamId
	        AND AuctionId = @AuctionId
	)
	BEGIN
	    -- Record inserted successfully
	    SET @ErrorMessage = NULL;
	END
	ELSE
	BEGIN
	    -- Record not inserted successfully
	    SET @ErrorMessage = 'Error: Unable to save bid.';
	END
END
