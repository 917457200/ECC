--添加了数据词典
USE [ECCDB]
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',13,'设备心跳检查',13,1,'')

--修改了DeviceClassInfo表
alter table [DeviceClassInfo] add [Version] NVARCHAR(50)