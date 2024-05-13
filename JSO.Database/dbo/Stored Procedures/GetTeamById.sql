
CREATE PROCEDURE [dbo].[GetTeamById]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	select * from TeamRegister where TeamId = @Id and IsActive = 1 and IsDeleted = 0
END
