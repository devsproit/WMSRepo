CREATE PROCEDURE [WMS].[ItemInsert]
 @CompanyName nvarchar(max) = NULL,
 @ItemName nvarchar(max) =null,
 @ItemCode nvarchar(max) = null
 
 AS 
 BEGIN
	INSERT INTO [WMS].[Item]
           ([CompanyName]
           ,[ItemName]
           ,[ItemCode])
           
     VALUES
           (@CompanyName,@ItemName,@ItemCode)

 END