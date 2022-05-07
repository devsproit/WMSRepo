CREATE PROCEDURE [WMS].[CompanyInsert]
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
	INSERT INTO [WMS].[Company]
           ([ScreenCode]
           ,[CompanyCode]
           ,[CompanyName]
           ,[Address]
           ,[Location]
           ,[ContactPersonHO]
           ,[ContactNumberHO]
           ,[EmailIdHO]
           ,[SpaceSizeFormat]
           ,[Items]
           ,[SubItem])
     VALUES
           (@ScreenCode,@CompanyCode,@CompanyName,@Address,@Location,@ContactPersonHO,@ContactNumberHO,@EmailIdHO,@SpaceSizeFormat,@Items,@SubItem)

 END