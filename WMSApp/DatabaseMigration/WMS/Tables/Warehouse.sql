CREATE TABLE [WMS].[Warehouse]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [ScreenCode] NVARCHAR(MAX) NULL, 
    [WarehouseCode] NVARCHAR(MAX) NULL, 
    [WarehouseName] NVARCHAR(MAX) NULL, 
    [CompanyName] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Location] NVARCHAR(MAX) NULL, 
    [ContactPersonWarehouse] NVARCHAR(MAX) NULL, 
    [ContactNumberWarehouse] NVARCHAR(MAX) NULL, 
    [EmailIdBranch] NVARCHAR(MAX) NULL, 
    [BranchId] INT NULL
)
