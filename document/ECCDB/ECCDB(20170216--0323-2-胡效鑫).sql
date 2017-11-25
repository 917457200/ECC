USE [ECCDB]
--alter table [DeviceClassInfo] add [OldClassName] NVARCHAR(50);
--alter table [DeviceClassInfo] add [AsyncResultID] INT;
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
     VALUES ('OperateTypeID',6,'�豸�����ļ����',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',7,'�豸���ݿ����',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',8,'���ÿ��ػ�ʱ��',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',9,'���õ�����',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',10,'���°༶��Ϣ',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',11,'���¿γ̱�',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',12,'ͬ���豸ʱ��',12,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',13,'�豸�������',13,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',14,'���������������',14,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',15,'���������������',15,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',16,'����������񿪹�',16,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',17,'���°󶨰༶��Ϣ',17,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',18,'�ն˽���',18,1,'')
GO

ALTER TABLE [dbo].[DeviceClassInfo] ADD TerminalScreenshot BIGINT DEFAULT 0; 

ALTER TABLE [dbo].[OperateLogInfo] ALTER COLUMN [module] VARCHAR(50);

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
     VALUES ('OperateTypeID',6,'�豸�����ļ����',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',7,'�豸���ݿ����',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',8,'���ÿ��ػ�ʱ��',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',9,'���õ�����',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',10,'���°༶��Ϣ',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',11,'���¿γ̱�',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',12,'ͬ���豸ʱ��',12,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',13,'�豸�������',13,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',14,'���������������',14,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',15,'���������������',15,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',16,'����������񿪹�',16,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',17,'���°󶨰༶��Ϣ',17,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',18,'�ն˽���',18,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',19,'�󶨹���',19,1,'')
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
     VALUES ('OperateTypeID',6,'�豸�����ļ����',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',7,'�豸���ݿ����',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',8,'���ÿ��ػ�ʱ��',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',9,'���õ�����',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',10,'���°༶��Ϣ',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',11,'���¿γ̱�',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',12,'ͬ���豸ʱ��',12,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',13,'�豸�������',13,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',14,'���������������',14,1,'')
     INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',15,'���������������',15,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',16,'����������񿪹�',16,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',17,'���°󶨰༶��Ϣ',17,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',18,'�ն˽���',18,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',19,'�󶨹���',19,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',20,'��ȡ������Ϣ',20,1,'')
GO



CREATE TABLE [dbo].[ExaminationInfo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ClassCode] [nvarchar](50) NULL,
	[ClassName] [nvarchar](50) NULL,
	[VisibleTime] [smalldatetime] NULL,
	[HideTime] [smalldatetime] NULL,
	[ExamName] [nvarchar](50) NULL,
	[ExamRoom] [nvarchar](50) NULL,
	[ExamSubject] [nvarchar](50) NULL,
	[ExamTime] [nvarchar](50) NULL,
	[StudentNumberRange] [nvarchar](50) NULL,
	[StudentNumber] [int] NULL,
	[Teachers] [nvarchar](50) NULL,
	[Notice] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsValid] [bit] NULL,
	[HandledDate] [smalldatetime] NULL,
	[HandledID] [nvarchar](20) NULL,
 CONSTRAINT [PK_Examination] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
VALUES ('OperateTypeID',20,'����������Ϣ',20,1,'')
GO    