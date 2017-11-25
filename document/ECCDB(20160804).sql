USE ECCDB
GO


--ѧ������
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='SubjectTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('SubjectTypeID',1,'���',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('SubjectTypeID',2,'�Ŀ�',2,1,'')
GO


--�༶����
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='ClassTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ClassTypeID',1,'ʵ���',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ClassTypeID',2,'�ص��',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ClassTypeID',3,'����',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ClassTypeID',4,'��ͨ��',4,1,'')
GO


--�豸����
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='DeviceTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceTypeID',1,'��ʦ',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceTypeID',2,'ѧ��',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceTypeID',3,'�ҳ�',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceTypeID',4,'���Ӱ���',4,1,'')
GO


--�豸״̬
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='DeviceStatusID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceStatusID',1,'����',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceStatusID',2,'ά��',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DeviceStatusID',3,'ɾ��',3,1,'')
GO


--��������
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='DisplayModelID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',1,'�༶ģʽ',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',2,'����ģʽ',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',3,'����ģʽ',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',4,'ȫ��ͼƬģʽ',4,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',5,'ȫ����Ƶģʽ',5,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',6,'������ѡģʽ',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('DisplayModelID',7,'���ÿ���ģʽ',7,1,'')
GO


--�����������
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='OperateTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',1,'�����ϴ�����ģʽ',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',2,'�����½�����ģʽ',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',3,'ɾ������',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',4,'�������',4,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',5,'�豸��ʼ��',5,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',6,'�豸���',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',7,'ͬ���豸ʱ��',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',8,'���ÿ��ػ�ʱ��',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',9,'���õ�����',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',10,'���°༶��Ϣ',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',11,'���¿γ̱�',11,1,'')
GO


--��Ϣ����
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='MessageTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageTypeID',1,'����֪ͨ',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageTypeID',2,'�γ̱�',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageTypeID',3,'�༶������',3,1,'')
GO


--��Ϣ��Դ
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='MessageSourceID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageSourceID',1,'���Ӱ���ƽ̨',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageSourceID',2,'����У԰ƽ̨',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('MessageSourceID',3,'�ֻ��ǻ�У԰',3,1,'')
GO


--�������ȼ�
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='TaskPriorityID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',1,'1��',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',2,'2��',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',3,'3��',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',4,'4��',4,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',5,'5��',5,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',6,'6��',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',7,'7��',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',8,'8��',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',9,'9��',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',10,'10��',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',11,'11��',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',12,'12��',12,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',13,'13��',13,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',14,'14��',14,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',15,'15��',15,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',16,'16��',16,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',17,'17��',17,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',18,'18��',18,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',19,'19��',19,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskPriorityID',20,'20��',20,1,'')
GO


--ͼƬ��ʾЧ������
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='ImageEffectID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',1,'Ĭ��Ч��',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',2,'Tablet',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',3,'ZoomIn',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',4,'ZoomOut',4,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',5,'Accordion',5,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',6,'CubeOut',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',7,'DepthPage',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',8,'ForegroundToBackground',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',9,'RotateDown',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',10,'RotateUp',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',11,'ScaleInOut',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',12,'Stack',12,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',13,'FlipHorizontal',13,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',14,'FlipVertical',14,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',15,'BackgroundToForeground',15,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',16,'ZoomOutSlide',16,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('ImageEffectID',17,'CubeIn',17,1,'')
GO


--��������
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='TaskTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskTypeID',1,'��ͨ����',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskTypeID',2,'��ʱ����',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskTypeID',3,'��������',3,1,'')
GO


--����״̬
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='TaskStatusID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskStatusID',1,'����',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('TaskStatusID',2,'ɾ��',2,1,'')
GO
