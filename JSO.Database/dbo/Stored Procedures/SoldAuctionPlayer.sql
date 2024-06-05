-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SoldAuctionPlayer] 
	-- Add the parameters for the stored procedure here
	@AuctionId int,
	@PlayerId int,
	@TeamId int,
	@BidId int,
	@Status varchar(100),
	@Success int OUTPUT,
	@TournamentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @Success = 0;

	BEGIN TRY
    -- Check if a transaction is already active
    IF @@TRANCOUNT = 0
    BEGIN
        -- Start a new transaction only if there's no active transaction
        BEGIN TRANSACTION;
    END

    -- Your existing code block
	IF @Status = 'sold'
	BEGIN
		IF EXISTS (SELECT 1 FROM AuctionPlayerMapping WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId AND PlayerStatus != 'sold')
		BEGIN
			UPDATE AuctionPlayerMapping 
			SET PlayerStatus = 'sold', TeamId = @TeamId, ShowSoldPopup = 'sold'
			WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId;

			IF @@ROWCOUNT > 0
			BEGIN
				SET @Success = 1;
			END
		END
		ELSE 
		BEGIN
			IF @@TRANCOUNT = 1
			BEGIN
				ROLLBACK;
			END
			SET @Success = 0;
		END
	END
	ELSE IF @Status = 'unsold'
    BEGIN
        IF EXISTS (SELECT 1 FROM AuctionPlayerMapping WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId AND PlayerStatus != 'unsold')
        BEGIN
            UPDATE AuctionPlayerMapping 
            SET PlayerStatus = 'unsold', ShowSoldPopup = 'unsold',
				UnsoldCount = ISNULL(UnsoldCount, 0) + 1
            WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId;

            IF @@ROWCOUNT > 0
            BEGIN
                SET @Success = 1;
            END

			IF @@TRANCOUNT = 1
            BEGIN
                COMMIT;
            END
        END
        ELSE
        BEGIN
            IF @@TRANCOUNT = 1
            BEGIN
                ROLLBACK;
            END
            SET @Success = 0;
        END

		IF EXISTS (
		    SELECT 1 
		    FROM AuctionPlayerMapping apm
		    JOIN PlayerRegister pr ON apm.PlayerId = pr.PlayerRegisterId
		    WHERE apm.AuctionId = @AuctionId 
		        AND apm.PlayerId = @PlayerId 
		        AND pr.PlayerCategory = 'I'
		)
		BEGIN
		    UPDATE PlayerRegister 
		    SET PlayerCategory = 'A', BasePrice = '700000'
		    WHERE PlayerRegisterId = @PlayerId;
		END
    END

    IF EXISTS (SELECT 1 FROM Bids WHERE PlayerId = @PlayerId AND BidId = @BidId AND TeamId = @TeamId AND AuctionId = @AuctionId AND IsDeleted = 0 AND Sold != 1)
    BEGIN
        --UPDATE Bids 
        --SET Sold = 1 
        --WHERE PlayerId = @PlayerId AND TeamId = @TeamId AND AuctionId = @AuctionId;

		UPDATE Bids 
		SET Sold = 1 
		WHERE BidId = (
					SELECT TOP 1 BidId from bids WHERE TeamId = @TeamId 
				      AND AuctionId = @AuctionId AND PlayerId = @PlayerId AND BidId = @BidId
					  Order By CreatedOn desc
				);
				

		IF @@ROWCOUNT > 0
		BEGIN
			SET @Success = 1;
		END

        --IF @@ROWCOUNT > 0 AND @Success = 1
        --BEGIN
        --     Commit the transaction only if it was started within this block
        --    IF @@TRANCOUNT = 1
        --    BEGIN
        --        COMMIT;
        --    END
        --END
        --ELSE
        --BEGIN
            -- Rollback the transaction only if it was started within this block
            --IF @@TRANCOUNT = 1
            --BEGIN
            --    ROLLBACK;
            --END
            --SET @Success = 0;
        --END
    END
	ELSE 
	BEGIN
		IF @@TRANCOUNT = 1
		BEGIN
			ROLLBACK;
		END
		SET @Success = 0;
	END

	IF EXISTS (SELECT 1 FROM TeamRegister WHERE TeamId = @TeamId AND TournamentId = @TournamentId AND IsDeleted = 0 AND IsActive = 1)
    BEGIN
        UPDATE TeamRegister 
        SET TeamSize = ISNULL(TeamSize, 0) + 1,
			--TotalBalance = TotalBalance - (
			--	SELECT ISNULL(BidAmount, 0) 
			--	FROM bids bid
			--	WHERE PlayerId = @PlayerId AND bid.Sold = 1
			--	AND bid.TeamId = @TeamId AND bid.AuctionId = @AuctionId
			--)
			TotalBalance = TotalBalance - (
			    SELECT ISNULL(
			        CASE WHEN pr.PlayerCategory != 'I' THEN BidAmount ELSE 0 END,
			        0
			    ) 
			    FROM bids bid
			    JOIN PlayerRegister pr ON pr.PlayerRegisterId = @PlayerId
			    WHERE PlayerId = @PlayerId 
			        AND bid.Sold = 1
			        AND bid.TeamId = @TeamId 
			        AND bid.AuctionId = @AuctionId
			)
        WHERE TeamId = @TeamId AND TournamentId = @TournamentId

        IF @@ROWCOUNT > 0 AND @Success = 1
        BEGIN
            -- Commit the transaction only if it was started within this block
            IF @@TRANCOUNT = 1
            BEGIN
                COMMIT;
            END
        END
        ELSE
        BEGIN
            -- Rollback the transaction only if it was started within this block
            IF @@TRANCOUNT = 1
            BEGIN
                ROLLBACK;
            END
            SET @Success = 0;
        END
    END
	ELSE 
	BEGIN
		IF @@TRANCOUNT = 1
		BEGIN
			ROLLBACK;
		END
		SET @Success = 0;
	END

	END TRY
	BEGIN CATCH
	    -- Rollback the transaction in case of any error
	    IF @@TRANCOUNT > 0
	    BEGIN
	        ROLLBACK;
	    END
	    SET @Success = 0;
	END CATCH;
END

