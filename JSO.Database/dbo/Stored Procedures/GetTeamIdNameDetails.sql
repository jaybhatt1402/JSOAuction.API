-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetTeamIdNameDetails]
(
    -- Add the parameters for the stored procedure here
	@AuctionId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT TeamId, TeamName FROM TeamRegister WHERE AuctionId = @AuctionId AND IsActive = 1 AND IsDeleted = 0
END
