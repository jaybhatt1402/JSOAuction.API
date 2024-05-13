-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[GetGroupListByTournamentId]
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
    SELECT Id, GroupName, BasePrice FROM Groups WHERE TournamentId = @TournamentId AND IsActive = 1 AND IsDeleted = 0
END
