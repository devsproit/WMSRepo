CREATE PROCEDURE [WMS].[CustomerInsert]
 @ScreenCode nvarchar(max) = NULL,
 @CustomerCategory nvarchar(max) =null,
 @CustomerName nvarchar(max) = null,
 @CustomerCode nvarchar(max) =null,
 @Location nvarchar(max) =null
 
 AS 
 BEGIN
	INSERT INTO [WMS].[Customer]
           ([ScreenCode]
           ,[CustomerCategory]
           ,[CustomerName]
           ,[CustomerCode]
           ,[Location])
           
     VALUES
           (@ScreenCode,@CustomerCategory,@CustomerName,@CustomerCode,@Location)

 END