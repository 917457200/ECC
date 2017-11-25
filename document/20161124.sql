USE [ECCDB]
GO

/****** Object:  View [dbo].[UserClassInfoList]    Script Date: 11/24/2016 15:33:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[UserClassInfoList]
AS
SELECT     users.ID, users.UserCode, users.UserType, users.ClassCode, users.ClassFullCode, users.ClassName, users.RoleType, users.IsValid, users.Note, users.HandledID, 
                      users.HandledName, users.HandledDate, device.SubjectTypeID, device.SubjectTypeIDText, device.ClassTypeID, device.ClassTypeIDText, device.JPushID, 
                      device.Version
FROM         dbo.UserClassInfo AS users INNER JOIN
                      dbo.DeviceClassInfo AS device ON users.ClassCode = device.ClassCode
WHERE     (users.IsValid = 1) AND (device.IsValid = 1)

GO


