USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[spRepeatDeviceClassInfo]    Script Date: 08/03/2017 18:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spRepeatDeviceClassInfo]
    (
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @JPushID NVARCHAR(50) ,
      @ClassCode NVARCHAR(50) ,
      @OldClassName NVARCHAR(50) ,
      @ClassName NVARCHAR(50) ,
      @ClassFullCode NVARCHAR(50) ,
      @BanZhuRenPhotoPath NVARCHAR(500) ,--班主任照片
      @BanZhuRenQRPath NVARCHAR(500) ,--班主任二维码
      @ClassNickName NVARCHAR(50) ,--昵称
      @ClassSlogan NVARCHAR(500) ,--口号
      @ZuoYouMing NVARCHAR(500) ,--座右铭
      @Introduction NVARCHAR(4000) ,--班级介绍
      @Recommended NVARCHAR(4000) ,--推荐词
      @ClassLogoPath NVARCHAR(500) ,--班级照片
      @ClassQRPath NVARCHAR(500) , --班级二维码
      @SubjectTypeID TINYINT ,
      @SubjectTypeIDText NVARCHAR(50) ,
      @ClassTypeID TINYINT ,
      @ClassTypeIDText NVARCHAR(50) ,
      @SemesterName NVARCHAR(50) ,
      @BanZhuRenCode NVARCHAR(20) ,
      @BanZhuRenName NVARCHAR(50) ,
      @ModifiedID NVARCHAR(20) ,
      @ModifiedName NVARCHAR(50),
      @RoomNum NVARCHAR(50)
    )
AS 
    BEGIN
        SET nocount ON;
        SET @Errorcode = 0;
        SET @Errormsg = '';
        IF EXISTS ( SELECT  id
                    FROM    [dbo].[DeviceClassInfo]
                    WHERE   [JPushID] = @JPushID ) 
            BEGIN
                UPDATE  [dbo].[DeviceClassInfo]
                SET     ClassCode = @ClassCode ,
                        [ClassFullCode] = @ClassFullCode ,
                        [ClassName] = @ClassName ,
                        [ClassNickName] = @ClassNickName ,
                        [ClassLogoPath] = @ClassLogoPath ,
                        [ClassQRPath] = @ClassQRPath ,
                        [ClassSlogan] = @ClassSlogan ,
                        [SemesterName] = @SemesterName ,
                        [BanZhuRenCode] = @BanZhuRenCode ,
                        [BanZhuRenName] = @BanZhuRenName ,
                        [BanZhuRenPhotoPath] = @BanZhuRenPhotoPath ,
                        [BanZhuRenQRPath] = @BanZhuRenQRPath ,
                        [ZuoYouMing] = @ZuoYouMing ,
                        [Introduction] = @Introduction ,
                        [Recommended] = @Recommended ,
                        [SubjectTypeID] = @SubjectTypeID ,
                        [SubjectTypeIDText] = @SubjectTypeIDText ,
                        [ClassTypeID] = @ClassTypeID ,
                        [ClassTypeIDText] = @ClassTypeIDText,
                        [OldClassName]=@OldClassName,
                        RoomNum=@RoomNum
                WHERE   [JPushID] = @JPushID 
                
                UPDATE  [dbo].[DeviceClassInfo]
                SET     ClassCode = SUBSTRING(@ClassCode,1,10),
                        [ClassFullCode] = '',
                        [ClassName] = '' ,
                        [ClassNickName] ='' ,
                        [ClassLogoPath] = '',
                        [ClassQRPath] = '' ,
                        [ClassSlogan] = '',
                        [SemesterName] = '',
                        [BanZhuRenCode] ='',
                        [BanZhuRenName] = '',
                        [BanZhuRenPhotoPath] = '' ,
                        [BanZhuRenQRPath] = '' ,
                        [ZuoYouMing] = '',
                        [Introduction] = '' ,
                        [Recommended] = '' ,
                        [SubjectTypeID] = '' ,
                        [SubjectTypeIDText] = '' ,
                        [ClassTypeID] ='',
                        [ClassTypeIDText] = '',
                        [OldClassName]=''
                WHERE   [JPushID] != @JPushID  and ClassCode=@ClassCode
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
