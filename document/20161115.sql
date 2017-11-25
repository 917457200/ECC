--ÐÞ¸ÄÁËDeviceClassInfo±í
alter table [DeviceClassInfo] add TastDate SMALLDATETIME;
alter table [DeviceClassInfo] add EmptyDate SMALLDATETIME;
alter table [DeviceClassInfo] add SwitchDate NVARCHAR(50);
alter table [DeviceClassInfo] add NavigationBarVisible BIT DEFAULT 0;
alter table [DeviceClassInfo] add UpdateScheduleDate SMALLDATETIME;
alter table [DeviceClassInfo] add HeartBeatCheckDate SMALLDATETIME;

UPDATE [dbo].[DeviceClassInfo] SET [NavigationBarVisible]=0;