CREATE PROCEDURE [WMS].[BranchInsert]
 @ScreenCode nvarchar(max) = NULL,
 @BranchCode nvarchar(max) =null,
 @BranchName nvarchar(max) = null, 
 @CompanyName nvarchar(max) = null, 
 @Address nvarchar(max) =null,
 @Location nvarchar(max) =null,
 @ContactPersonBranch nvarchar(max) =null,
 @ContactNumberBranch nvarchar(max) =null,
 @EmailIdBranch nvarchar(max)=null,
 @AssociatedEmployee nvarchar(max)=null,
 @WarehouseName nvarchar(max) = null

 AS 
 BEGIN
	INSERT INTO [WMS].[Branch]
           ([ScreenCode]
           ,[BranchCode]
           ,[BranchName]
           ,[CompanyName]
           ,[Address]
           ,[Location]
           ,[ContactPersonBranch]
           ,[ContactNumberBranch]
           ,[EmailIdBranch]
           ,[AssociatedEmployee]
           ,[WarehouseId])
     VALUES
           (@ScreenCode,@BranchCode,@BranchName,@CompanyName,@Address,@Location,@ContactPersonBranch,@ContactNumberBranch,@EmailIdBranch,@AssociatedEmployee,@WarehouseName)

 END