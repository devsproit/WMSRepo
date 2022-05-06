CREATE PROCEDURE [WMS].[LocationUpdate]
 @Id int,
  @ScreenCode nvarchar(max) = NULL,
 @LocationCode nvarchar(max) =null,
 @LocationName nvarchar(max) = null 
 
 AS 
 BEGIN
    
UPDATE [WMS].[Location]
   SET [ScreenCode] = @ScreenCode
      ,[LocationCode] = @LocationCode
      ,[LocationName] = @LocationName
      
      
 WHERE Id = @Id

 END