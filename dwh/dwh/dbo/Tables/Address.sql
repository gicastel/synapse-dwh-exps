CREATE TABLE [dbo].[address]
(
    [AddressID]     INT              NOT NULL,
    [AddressLine1]  NVARCHAR (60)    NOT NULL,
    [AddressLine2]  NVARCHAR (60)    NULL,
    [City]          NVARCHAR (30)    NOT NULL,
    [StateProvince] nvarchar(50)     NOT NULL,
    [CountryRegion] nvarchar(50)     NOT NULL,
    [PostalCode]    NVARCHAR (15)    NOT NULL
)
WITH
(
    DISTRIBUTION = HASH ([AddressID]),
    CLUSTERED COLUMNSTORE INDEX
)
GO