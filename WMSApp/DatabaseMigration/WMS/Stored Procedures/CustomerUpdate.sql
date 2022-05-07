CREATE PROCEDURE [WMS].[CustomerUpdate]
 @Id int,
  @ScreenCode nvarchar(max) = NULL,
 @CustomerCategory nvarchar(max) =null,
 @CustomerName nvarchar(max) = null,
  @CustomerCode nvarchar(max) =null,
   @Location nvarchar(max) =null
 
 AS 
 BEGIN
    
UPDATE [WMS].[Customer]
   SET [ScreenCode] = @ScreenCode
      ,[CustomerCategory] = @CustomerCategory
      ,[CustomerName] = @CustomerName
      ,[CustomerCode] = @CustomerCode
      ,[Location] = @Location
      
      
 WHERE Id = @Id

 END