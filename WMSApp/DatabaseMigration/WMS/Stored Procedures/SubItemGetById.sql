﻿
CREATE PROCEDURE [WMS].[SubItemGetById]
@Id INT

AS
BEGIN
	SELECT * FROM WMS.SubItem
	 WHERE Id = @Id
END
GO