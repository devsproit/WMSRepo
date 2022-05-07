CREATE PROCEDURE [WMS].[CompanyDeleteById]
	 @Id INT
AS
BEGIN
	UPDATE [WMS].[Company]
   SET [IsDeleted] = 1
      WHERE Id = @Id
 END