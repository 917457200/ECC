--��������ݴʵ�
USE [ECCDB]
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',13,'�豸�������',13,1,'')

--�޸���DeviceClassInfo��
alter table [DeviceClassInfo] add [Version] NVARCHAR(50)