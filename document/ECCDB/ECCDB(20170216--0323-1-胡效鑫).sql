USE [ECCDB];
--alter table [DeviceClassInfo] add [OldClassName] NVARCHAR(50);
--alter table [DeviceClassInfo] add [AsyncResultID] INT;

if exists (select * from sysobjects where id = object_id(N'[spRepeatDeviceClassInfo]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spRepeatDeviceClassInfo
USE [ECCDB]
GO
CREATE PROCEDURE [dbo].[spRepeatDeviceClassInfo]
    (
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @JPushID NVARCHAR(50) ,
      @ClassCode NVARCHAR(50) ,
      @OldClassName NVARCHAR(50) ,
      @ClassName NVARCHAR(50) ,
      @ClassFullCode NVARCHAR(50) ,
      @BanZhuRenPhotoPath NVARCHAR(500) ,--°àÖ÷ÈÎÕÕÆ¬
      @BanZhuRenQRPath NVARCHAR(500) ,--°àÖ÷ÈÎ¶þÎ¬Âë
      @ClassNickName NVARCHAR(50) ,--êÇ³Æ
      @ClassSlogan NVARCHAR(500) ,--¿ÚºÅ
      @ZuoYouMing NVARCHAR(500) ,--×ùÓÒÃú
      @Introduction NVARCHAR(4000) ,--°à¼¶½éÉÜ
      @Recommended NVARCHAR(4000) ,--ÍÆ¼ö´Ê
      @ClassLogoPath NVARCHAR(500) ,--°à¼¶ÕÕÆ¬
      @ClassQRPath NVARCHAR(500) , --°à¼¶¶þÎ¬Âë
      @SubjectTypeID TINYINT ,
      @SubjectTypeIDText NVARCHAR(50) ,
      @ClassTypeID TINYINT ,
      @ClassTypeIDText NVARCHAR(50) ,
      @SemesterName NVARCHAR(50) ,
      @BanZhuRenCode NVARCHAR(20) ,
      @BanZhuRenName NVARCHAR(50) ,
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
                        [OldClassName]=@OldClassName
                WHERE   [JPushID] = @JPushID 
           
                SET @Errorcode = 0;
                SET @Errormsg = '³É¹¦';
             
            END
        ELSE 
            BEGIN
                SET @Errorcode = 1;
                SET @Errormsg = 'Ê§°Ü';
            END
        RETURN 0;
        SET nocount OFF;
    END
 go
 
if exists (select * from sysobjects where id = object_id(N'[spUploadTerminalScreenshot]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spUploadTerminalScreenshot
USE [ECCDB]
GO
CREATE PROCEDURE [dbo].[spUploadTerminalScreenshot]
    (
      @ID BIGINT OUTPUT ,
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @ClassCode NVARCHAR(50),
      @FieldCode NVARCHAR(50) ,
      @FieldTypeID INT ,
      @FieldName1 NVARCHAR(1000) ,
      @FieldContent NVARCHAR(MAX) ,
      @CreatedID NVARCHAR(20)
    )
AS 
    SET nocount ON;
	
    SET @ID = 0;
    SET @Errorcode = 0;
    SET @Errormsg = '';
    BEGIN TRAN
    INSERT  INTO [DataFieldInfo]
            ( [FieldCode] ,
              [FieldTypeID] ,
              [FieldSerialID],
              [FieldName1] ,
              [FieldContent] ,
              [CreatedID] ,
              [CreatedDate]        
                
            )
    VALUES  ( @FieldCode ,
              @FieldTypeID ,
              0,
              @FieldName1 ,
              @FieldContent ,
              @CreatedID ,
              GETDATE()
            )
    SET @ID = SCOPE_IDENTITY();
    UPDATE [dbo].[DeviceClassInfo] SET TerminalScreenshot=@ID WHERE [IsValid]=1 AND [ClassCode]=@ClassCode;
    IF @@ERROR=0
    BEGIN
    SET @Errorcode = 0;
    SET @Errormsg = '³É¹¦';
   COMMIT;
   END;
   ELSE
   BEGIN
    SET @Errorcode = 1;
    SET @Errormsg = 'Ê§°Ü';
    END
    RETURN 0;
    SET nocount OFF;
