CREATE TABLE [WMS].[Branch]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [ScreenCode] NVARCHAR(MAX) NULL, 
    [BranchCode] NVARCHAR(MAX) NULL, 
    [BranchName] NVARCHAR(MAX) NULL, 
    [CompanyName] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Location] NVARCHAR(MAX) NULL, 
    [ContactPersonBranch] NVARCHAR(MAX) NULL, 
    [ContactNumberBranch] NVARCHAR(MAX) NULL, 
    [EmailIdBranch] NVARCHAR(MAX) NULL, 
    [AssociatedEmployee] NVARCHAR(MAX) NULL, 
    [WarehouseId] INT NULL
)
