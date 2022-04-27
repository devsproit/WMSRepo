CREATE TABLE [WMS].[UserRoles]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [UserId] INT NULL, 
    [RoleId] INT NULL, 
    [IsActive] BIT NULL DEFAULT 1
)
