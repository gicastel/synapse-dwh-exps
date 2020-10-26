CREATE PROC [dbo].[CountAddressRows]
AS
BEGIN
	SELECT
		NULLIF(count(*), 450)
	FROM dbo.Address
END
