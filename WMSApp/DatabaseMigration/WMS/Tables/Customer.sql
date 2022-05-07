CREATE TABLE [WMS].[Customer]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [ScreenCode] NVARCHAR(MAX) NULL, 
    [CustomerCategory] NVARCHAR(MAX) NULL, 
    [CustomerName] NVARCHAR(MAX) NULL,
    [CustomerCode] NVARCHAR(MAX) NULL,
    [Location] NVARCHAR(MAX) NULL

   
)
