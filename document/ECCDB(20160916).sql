USE ECCDB
GO


---- �޸���dbo.p_Device_Init
USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[p_Device_Init]    Script Date: 09/17/2016 18:53:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		hxx
-- Create date: 2016.0801
-- Description:	��ʼ���豸���Ӱ����������
-- =============================================
ALTER PROCEDURE [dbo].[p_Device_Init]
    (
      @Errorcode INT OUTPUT ,
      @Errormsg VARCHAR(MAX) OUTPUT ,
      @RoomNum VARCHAR(10) ,
      @ClassCode NVARCHAR(50) ,
      @DeviceSN VARCHAR(30) ,
      @ModifiedID NVARCHAR(20) ,
      @ModifiedName NVARCHAR(20) ,
      @IPAddress NVARCHAR(50) ,
      @IPPort INT ,
      @DeviceTypeID TINYINT,
      @JPushID NVARCHAR(50),
      @IsCheckInt INT ,--�Ƿ����Ѿ���ʼ����0--����飺������״��򱣴������ݣ���������ݣ�1--��飺������״����ʼ��ʧ�ܣ�
      @IsCheckRoomNum INT--�Ƿ��鷿���������0--����飬1--��飩
    )
AS 
    BEGIN
	
        SET NOCOUNT ON;
   
        SET @Errorcode = 0;
        SET @Errormsg = '';
        UPDATE [dbo].[DeviceClassInfo] SET [JPushID]='' WHERE [JPushID]=@JPushID;
        IF EXISTS ( SELECT  id
                    FROM    [dbo].[DeviceClassInfo]
                    WHERE   [ClassCode] = @ClassCode ) 
            BEGIN
                IF ( @IsCheckInt = 1
                     AND EXISTS ( SELECT    id
                                  FROM      [dbo].[DeviceClassInfo]
                                  WHERE     [ClassCode] = @ClassCode
                                            AND [DeviceSN] IS NOT NULL
                                            AND [DeviceSN] <> '' )
                   ) 
                    BEGIN
                        SET @errorcode = 2;
                        SET @errormsg = '�ð༶�豸�����Ѿ���ʼ��';
                    END
                ELSE 
                    BEGIN
                        IF ( @IsCheckRoomNum = 1
                             AND EXISTS ( SELECT    id
                                          FROM      [dbo].[DeviceClassInfo]
                                          WHERE     [RoomNum] = @RoomNum )
                           ) 
                            BEGIN
                                DECLARE @className NVARCHAR(50);
                                DECLARE @deviceCode NVARCHAR(50);
                                SET @className='';
                                SET @deviceCode='';
                                SELECT  @className = [ClassName] ,
                                        @deviceCode = [DeviceCode]
                                FROM    [dbo].[DeviceClassInfo]
                                WHERE   [RoomNum] = @RoomNum
                                SET @errorcode = 3;
                                SET @errormsg = '��ʼ��ʧ�ܣ�������Ѿ���' + @className
                                    + '(�豸���:' + @deviceCode + ')ռ��';
                            END
                        ELSE 
                            BEGIN
                                IF EXISTS ( SELECT  id
                                            FROM    [dbo].[DeviceClassInfo]
                                            WHERE   [ClassCode] = @ClassCode
                                                    AND [DeviceSN] IS NOT NULL
                                                    AND [DeviceSN] <> '' ) 
                                    BEGIN --���³�ʼ��
                                        UPDATE  [dbo].[DeviceClassInfo]
                                        SET     [OldDeviceSN] = [DeviceSN] ,
                                                [DeviceSN] = @DeviceSN ,
                                                [RoomNum] = @RoomNum ,
                                                [IPAddress] = @IPAddress ,
                                                [JPushID]=@JPushID,
                                                [DeviceTypeID]=@DeviceTypeID,
                                                [IPPort] = @IPPort ,
                                                [ModifiedID] = @ModifiedID ,
                                                [ModifiedName] = @ModifiedName ,
                                                [ModifiedDate] = GETDATE()
                                        WHERE   [ClassCode] = @ClassCode;
                                        SET @errorcode = 0;
                                        SET @errormsg = '��ʼ���ɹ�';
                                    END
                                ELSE --�״γ�ʼ��
                                    BEGIN
                                        UPDATE  [dbo].[DeviceClassInfo]
                                        SET     [DeviceSN] = @DeviceSN ,
                                                [RoomNum] = @RoomNum ,
                                                [IPAddress] = @IPAddress ,
                                                 [JPushID]=@JPushID,
                                                [DeviceTypeID]=@DeviceTypeID,
                                                [IPPort] = @IPPort ,
                                                [ModifiedID] = @ModifiedID ,
                                                [ModifiedName] = @ModifiedName ,
                                                [ModifiedDate] = GETDATE()
                                        WHERE   [ClassCode] = @ClassCode;
                                        SET @errorcode = 0;
                                        SET @errormsg = '��ʼ���ɹ�';
                                    END 
                            END
                    END
            END
        ELSE 
            BEGIN
                SET @errorcode = 1;
                SET @errormsg = '��ʼ��ʧ�ܣ������쳣';
            END
	
        RETURN 0;
        SET NOCOUNT OFF;
    END




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
GO



----������p_DeviceClassInfo_Update

USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceClassInfo_Update]    Script Date: 09/17/2016 18:47:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------
--��Ŀ���ƣ�
--˵�����޸�һ����¼
--���ߣ�hxx
--ʱ�䣺09/17/2016 14:37:48
----------------------------------------
create PROCEDURE [dbo].[p_DeviceClassInfo_Update]
    (
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @ID BIGINT ,
      @BanZhuRenPhotoPath NVARCHAR(500) ,--��������Ƭ
      @BanZhuRenQRPath NVARCHAR(500) ,--�����ζ�ά��
      @RoomNum NVARCHAR(50) ,--�����
      @ClassNickName NVARCHAR(50) ,--�ǳ�
      @ClassSlogan NVARCHAR(500) ,--�ں�
      @ZuoYouMing NVARCHAR(500) ,--������
      @Introduction NVARCHAR(4000) ,--�༶����
      @Recommended NVARCHAR(4000) ,--�Ƽ���
      @ClassLogoPath NVARCHAR(500) ,--�༶��Ƭ
      @ClassQRPath NVARCHAR(500) ,--�༶��ά��
      @ModifiedID NVARCHAR(20) ,
      @ModifiedName NVARCHAR(50)
    )
AS 
    BEGIN
        SET nocount ON;
        SET @Errorcode = 0;
        SET @Errormsg = '';
     
        IF EXISTS ( SELECT  id
                    FROM    [dbo].[DeviceClassInfo]
                    WHERE   id = @ID ) 
            BEGIN
                UPDATE  [dbo].[DeviceClassInfo]
                SET     [BanZhuRenPhotoPath] = @BanZhuRenPhotoPath ,
                        [BanZhuRenQRPath] = @BanZhuRenQRPath ,
                        [RoomNum] = @RoomNum ,
                        [ClassNickName] = @ClassNickName ,
                        [ClassSlogan] = @ClassSlogan ,
                        [ZuoYouMing] = @ZuoYouMing ,
                        [Introduction] = @Introduction ,
                        [Recommended] = @Recommended ,
                        [ClassLogoPath] = @ClassLogoPath ,
                        [ClassQRPath] = @ClassQRPath ,
                        [ModifiedID] = @ModifiedID ,
                        [ModifiedName] = @ModifiedName ,
                        [ModifiedDate] = GETDATE()
                WHERE   id = @id
     
           
                SET @Errorcode = 0;
                SET @Errormsg = '�ɹ�';
             
            END
        ELSE 
            BEGIN
                SET @Errorcode = 1;
                SET @Errormsg = 'ʧ��';
            END
        RETURN 0;
        SET nocount OFF;
    END


