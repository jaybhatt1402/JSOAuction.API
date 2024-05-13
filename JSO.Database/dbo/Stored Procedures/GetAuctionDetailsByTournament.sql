-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetAuctionDetailsByTournament]
(
    -- Add the parameters for the stored procedure here
    @TournamentId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT * from AuctionRegister Where TournamentId = @TournamentId And IsActive = 1 And IsDeleted = 0
END
