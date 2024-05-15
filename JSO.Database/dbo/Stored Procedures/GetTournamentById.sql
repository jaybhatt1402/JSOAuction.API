-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetTournamentById]
	@Id int

AS
BEGIN
	select * from TournamentRegister where TournamentId = @Id and IsActive = 1 and IsDeleted = 0
END