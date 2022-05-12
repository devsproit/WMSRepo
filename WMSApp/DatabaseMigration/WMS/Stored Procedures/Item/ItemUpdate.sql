CREATE PROCEDURE [WMS].[ItemUpdate]
 @Id int,
  @CompanyName nvarchar(max) = NULL,
 @ItemName nvarchar(max) =null,
 @ItemCode nvarchar(max) = null
 
 AS 
 BEGIN
    
UPDATE [WMS].[Item]
   SET [CompanyName] = @CompanyName
      ,[ItemName] = @ItemName
      ,[ItemCode] = @ItemCode
     
 WHERE Id = @Id

 END