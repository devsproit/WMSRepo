CREATE PROCEDURE [WMS].[LocationInsert]
 @ScreenCode nvarchar(max) = NULL,
 @LocationCode nvarchar(max) =null,
 @LocationName nvarchar(max) = null
 
 AS 
 BEGIN
	INSERT INTO [WMS].[Location]
           ([ScreenCode]
           ,[LocationCode]
           ,[LocationName])
           
     VALUES
           (@ScreenCode,@LocationCode,@LocationName)

 END