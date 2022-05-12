CREATE TABLE [WMS].[SubItem]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [SubItemCode] NVARCHAR(MAX) NULL, 
    [SubItemName] NVARCHAR(MAX) NULL, 
    [ItemName] NVARCHAR(MAX) NULL,
    [MaterialDescription] NVARCHAR(MAX) NULL,
     [SubItemSize] NVARCHAR(MAX) NULL,
     [FOC] NVARCHAR(MAX) NULL,
     [SubItemCategory] NVARCHAR(MAX) NULL,
     [SubItemSR] NVARCHAR(MAX) NULL
)
