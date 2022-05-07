CREATE TABLE [WMS].[Company]
(
	[Id] INT IDENTITY(1000,1) PRIMARY KEY, 
    [ScreenCode] NVARCHAR(MAX) NULL, 
    [CompanyCode] NVARCHAR(MAX) NULL, 
    [CompanyName] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Location] NVARCHAR(MAX) NULL, 
    [ContactPersonHO] NVARCHAR(MAX) NULL, 
    [ContactNumberHO] NVARCHAR(MAX) NULL, 
    [EmailIdHO] NVARCHAR(MAX) NULL, 
    [SpaceSizeFormat] NVARCHAR(MAX) NULL, 
    [Items] NVARCHAR(MAX) NULL, 
    [SubItem] NVARCHAR(MAX) NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0
)
