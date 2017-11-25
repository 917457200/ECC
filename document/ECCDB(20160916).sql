USE ECCDB
GO


---- 修改了dbo.p_Device_Init
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
-- Description:	初始化设备电子班牌相关数据
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
      @IsCheckInt INT ,--是否检查已经初始化（0--不检查：如果非首次则保存老数据，填充新数据；1--检查：如果非首次则初始化失败）
      @IsCheckRoomNum INT--是否检查房间号重名（0--不检查，1--检查）
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
                        SET @errormsg = '该班级设备数据已经初始化';
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
                                SET @errormsg = '初始化失败：房间号已经被' + @className
                                    + '(设备编号:' + @deviceCode + ')占用';
                            END
                        ELSE 
                            BEGIN
                                IF EXISTS ( SELECT  id
                                            FROM    [dbo].[DeviceClassInfo]
                                            WHERE   [ClassCode] = @ClassCode
                                                    AND [DeviceSN] IS NOT NULL
                                                    AND [DeviceSN] <> '' ) 
                                    BEGIN --重新初始化
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
                                        SET @errormsg = '初始化成功';
                                    END
                                ELSE --首次初始化
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
                                        SET @errormsg = '初始化成功';
                                    END 
                            END
                    END
            END
        ELSE 
            BEGIN
                SET @errorcode = 1;
                SET @errormsg = '初始化失败：数据异常';
            END
	
        RETURN 0;
        SET NOCOUNT OFF;
    END




--任务操作类型
DELETE FROM [ECCDB].[dbo].[Dict] WHERE [ItemName]='OperateTypeID'
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',1,'更新上次任务模式',1,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',2,'创建新建任务模式',2,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',3,'删除任务',3,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',4,'软件升级',4,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',5,'设备初始化',5,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',6,'设备垃圾文件清空',6,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',7,'设备数据库清空',7,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',8,'设置开关机时间',8,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',9,'设置导航栏',9,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',10,'更新班级信息',10,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',11,'更新课程表',11,1,'')
INSERT INTO [ECCDB].[dbo].[Dict] ([ItemName],[ItemKey],[ItemValue],[ItemOrder],[IsValid],[Note])
     VALUES ('OperateTypeID',12,'同步设备时间',12,1,'')
GO



----增加了p_DeviceClassInfo_Update

USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceClassInfo_Update]    Script Date: 09/17/2016 18:47:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------
--项目名称：
--说明：修改一条记录
--作者：hxx
--时间：09/17/2016 14:37:48
----------------------------------------
create PROCEDURE [dbo].[p_DeviceClassInfo_Update]
    (
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @ID BIGINT ,
      @BanZhuRenPhotoPath NVARCHAR(500) ,--班主任照片
      @BanZhuRenQRPath NVARCHAR(500) ,--班主任二维码
      @RoomNum NVARCHAR(50) ,--房间号
      @ClassNickName NVARCHAR(50) ,--昵称
      @ClassSlogan NVARCHAR(500) ,--口号
      @ZuoYouMing NVARCHAR(500) ,--座右铭
      @Introduction NVARCHAR(4000) ,--班级介绍
      @Recommended NVARCHAR(4000) ,--推荐词
      @ClassLogoPath NVARCHAR(500) ,--班级照片
      @ClassQRPath NVARCHAR(500) ,--班级二维码
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
                SET @Errormsg = '成功';
             
            END
        ELSE 
            BEGIN
                SET @Errorcode = 1;
                SET @Errormsg = '失败';
            END
        RETURN 0;
        SET nocount OFF;
    END


