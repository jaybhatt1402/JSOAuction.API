
CREATE PROCEDURE [dbo].[GetAuctionById]
	@Id int
AS
BEGIN
    -- Insert statements for procedure here
	SELECT * FROM [dbo].[AuctionRegister] WHERE AuctionId = @Id AND IsDeleted = 0 AND IsActive = 1
END
