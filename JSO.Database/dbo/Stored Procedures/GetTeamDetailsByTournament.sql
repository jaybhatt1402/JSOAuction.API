-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetTeamDetailsByTournament]
(
    -- Add the parameters for the stored procedure here
    --@AuctionId int,
    @TournamentId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    --SELECT TeamId,TeamName,[Owner] as OwnerName,CoachName,MobileNumber,Email,FoundedYear,TeamLogo,TournamentId from TeamRegister Where AuctionId = @AuctionId And TournamentId = @TournamentId And IsActive = 1 And IsDeleted = 0
	SELECT TeamId
		,TeamName
		,[Owner] as OwnerName
		,CoachName
		,MobileNumber
		,Email
		,FoundedYear
		,TeamLogo
		,TournamentId 
	from 
		TeamRegister 
	Where 
		TournamentId = @TournamentId 
		And 
		IsActive = 1 
		And 
		IsDeleted = 0
END
