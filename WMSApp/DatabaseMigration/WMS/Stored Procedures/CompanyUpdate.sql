CREATE PROCEDURE [WMS].[CompanyUpdate]
 @Id int,
 @ScreenCode nvarchar(max) = NULL,
 @CompanyCode nvarchar(max) =null,
 @CompanyName nvarchar(max) = null, 
 @Address nvarchar(max) =null,
 @Location nvarchar(max) =null,
 @ContactPersonHO nvarchar(max) =null,
 @ContactNumberHO nvarchar(max) =null,
 @EmailIdHO nvarchar(max)=null,
 @SpaceSizeFormat nvarchar(max)=null,
 @Items nvarchar(max) = null,
 @SubItem nvarchar(max) = null
 AS 
 BEGIN
    
UPDATE [WMS].[Company]
   SET [ScreenCode] = @ScreenCode
      ,[CompanyCode] = @CompanyCode
      ,[CompanyName] = @CompanyName
      ,[Address] = @Address
      ,[Location] = @Location
      ,[ContactPersonHO] = @ContactPersonHO
      ,[ContactNumberHO] = @ContactNumberHO
      ,[EmailIdHO] = @EmailIdHO
      ,[SpaceSizeFormat] = @SpaceSizeFormat
      ,[Items] = @Items
      ,[SubItem] = @SubItem
 WHERE Id = @Id

 END