CREATE PROCEDURE [WMS].[BranchUpdate]
 @Id int,
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
    
UPDATE [WMS].[Branch]
   SET [ScreenCode] = @ScreenCode
      ,[BranchCode] = @BranchCode
      ,[BranchName] = @BranchName
      ,[CompanyName] = @CompanyName
      ,[Address] = @Address
      ,[Location] = @Location
      ,[ContactPersonBranch] = @ContactPersonBranch
      ,[ContactNumberBranch] = @ContactNumberBranch
      ,[EmailIdBranch] = @EmailIdBranch
      ,[AssociatedEmployee] = @AssociatedEmployee
      ,[WarehouseId] = @WarehouseName
      
 WHERE Id = @Id

 END