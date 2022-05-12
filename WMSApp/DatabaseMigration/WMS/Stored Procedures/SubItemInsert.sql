CREATE PROCEDURE [WMS].[SubItemInsert]
 @SubItemCode nvarchar(max) = NULL,
 @SubItemName nvarchar(max) =null,
 @ItemName nvarchar(max) = null,
 @MaterialDescription nvarchar(max) =null,
 @SubItemSize nvarchar(max) =null,
 @FOC nvarchar(max) =null,
 @SubItemCategory nvarchar(max) =null,
 @SubItemSR nvarchar(max) =null
 
 AS 
 BEGIN
	INSERT INTO [WMS].[SubItem]
           ([SubItemCode]
           ,[SubItemName]
           ,[ItemName]
           ,[MaterialDescription]
           ,[SubItemSize]
           ,[FOC]
           ,[SubItemCategory]
           ,[SubItemSR])
           
     VALUES
           (@SubItemCode,@SubItemName,@ItemName,@MaterialDescription,@SubItemSize,@FOC,@SubItemCategory,@SubItemSR)

 END
