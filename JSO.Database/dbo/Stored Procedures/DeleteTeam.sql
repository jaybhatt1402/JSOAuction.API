
CREATE PROCEDURE [dbo].[DeleteTeam]
(
    @TeamId INT,
	@Success INT OUTPUT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	SET @Success = 0
	IF EXISTS (SELECT 1 FROM TeamRegister WHERE TeamId = @TeamId AND IsDeleted = 0) 
	BEGIN 
		UPDATE TeamRegister SET IsDeleted = 1 WHERE TeamId = @TeamId
		SET @Success = 1; -- Record was successfully updated
	END
	ELSE 
	BEGIN
		SET @Success = 0; -- Record was not updated
	END
END
