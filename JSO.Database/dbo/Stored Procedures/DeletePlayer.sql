CREATE PROCEDURE [dbo].[DeletePlayer] 
	-- Add the parameters for the stored procedure here
	@PlayerRegisterId INT,
	@Success INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @Success = 0
	IF EXISTS (SELECT 1 FROM PlayerRegister WHERE PlayerRegisterId = @PlayerRegisterId)
    BEGIN
		UPDATE PlayerRegister SET IsDeleted = 1 WHERE PlayerRegisterId = @PlayerRegisterId
		SET @Success = 1;
	END
	ELSE
	BEGIN
		SET @Success = 0;
	END
END