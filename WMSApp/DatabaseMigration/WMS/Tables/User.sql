CREATE TABLE [WMS].[User]
(
	[Id]  INT IDENTITY (100, 1) PRIMARY KEY, 
    [UserId] NVARCHAR(MAX) NULL, 
    [FirstName] NVARCHAR(MAX) NULL, 
    [LastName] NVARCHAR(MAX) NULL, 
    [IsActive] BIT NULL DEFAULT 1,     
)
