CREATE PROCEDURE [WMS].[SubItemUpdate]
 @Id int,
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
    
UPDATE [WMS].[SubItem]
   SET [SubItemCode] = @SubItemCode
      ,[SubItemName] = @SubItemName
      ,[ItemName] = @ItemName
      ,[MaterialDescription] = @MaterialDescription
      ,[SubItemSize] = @SubItemSize
      ,[FOC] = @FOC
      ,[SubItemCategory] = @SubItemCategory
      ,[SubItemSR] = @SubItemSR
      
      
 WHERE Id = @Id

 END