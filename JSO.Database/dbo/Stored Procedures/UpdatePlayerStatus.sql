-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePlayerStatus]
	-- Add the parameters for the stored procedure here
	@AuctionId INT,
	@PlayerId INT,
	@Success INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @Success = 0;
    -- Insert statements for procedure here
	update AuctionPlayerMapping SET ShowSoldPopup = null WHERE (PlayerStatus = 'sold' OR PlayerStatus = 'unsold')

	IF EXISTS (SELECT 1 FROM AuctionPlayerMapping WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId AND (PlayerStatus = 'notdisclosed' OR PlayerStatus = 'unsold')) 
	BEGIN 
		UPDATE AuctionPlayerMapping SET PlayerStatus = 'disclosed' WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId
		IF EXISTS (SELECT 1 FROM AuctionPlayerMapping WHERE AuctionId = @AuctionId AND PlayerId = @PlayerId AND PlayerStatus = 'disclosed')
		BEGIN
			SET @Success = 1;
		END
	END
END
