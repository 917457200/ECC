
--修改了DeviceClassInfo表
alter table [DeviceClassInfo] add [IsValid] bit;
alter table [DeviceClassInfo] alter column [DeviceSN] NVARCHAR(50) null;
UPDATE [dbo].[DeviceClassInfo] SET [IsValid]=1;

--修改了UserClassInfoList视图
SELECT     users.ID, users.UserCode, users.UserType, users.ClassCode, users.ClassFullCode, users.ClassName, users.RoleType, users.IsValid, users.Note, users.HandledID, 
                      users.HandledName, users.HandledDate, device.SubjectTypeID, device.SubjectTypeIDText, device.ClassTypeID, device.ClassTypeIDText, device.JPushID
FROM         dbo.UserClassInfo AS users INNER JOIN
                      dbo.DeviceClassInfo AS device ON users.ClassCode = device.ClassCode
WHERE     (device.IsValid = 1) AND (users.IsValid = 1)

