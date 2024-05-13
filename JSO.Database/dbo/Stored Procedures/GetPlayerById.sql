
CREATE PROCEDURE [dbo].[GetPlayerById]
	@Id int
AS
BEGIN
    -- Insert statements for procedure here
	SELECT * from [dbo].PlayerRegister where PlayerRegisterId = @Id and IsDeleted = 0 and IsActive = 1
END
