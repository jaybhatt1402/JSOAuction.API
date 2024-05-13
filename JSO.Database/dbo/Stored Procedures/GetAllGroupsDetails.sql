
CREATE PROCEDURE [dbo].[GetAllGroupsDetails] 
AS
BEGIN
	SELECT * FROM [dbo].[Groups] Where IsActive = 1 AND IsDeleted = 0
END
