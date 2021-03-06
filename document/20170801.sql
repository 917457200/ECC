USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[spRepeatDeviceClassInfo]    Script Date: 08/01/2017 15:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spRepeatUserClassInfo]
(
@UserCode NVARCHAR (20),
@UserType tinyint,
@ClassCode NVARCHAR (50),
@ClassFullCode NVARCHAR (50),
@ClassName NVARCHAR (50),
@RoleType tinyint, 
@IsValid bit, 
@Note NVARCHAR (500), 
@HandledID NVARCHAR (20),
@HandledName NVARCHAR (50), 
@HandledDate smalldatetime 
)
AS
BEGIN
SET NOCOUNT ON;
IF not EXISTS (SELECT
	id
FROM [dbo].[UserClassInfo]
WHERE [UserCode] = @UserCode AND ClassCode = @ClassCode AND UserType=@UserType and RoleType=@RoleType)
BEGIN
INSERT INTO [dbo].[UserClassInfo]
	VALUES (@UserCode, @UserType, @ClassCode, @ClassFullCode, @ClassName, @RoleType, @IsValid, @Note, @HandledID, @HandledName, @HandledDate)
END
ELSE
RETURN 0;
SET NOCOUNT OFF;
END