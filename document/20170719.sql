USE [ECCDB]
ALTER table dbo.DeviceTaskInfo add AreaModule nvarchar(max) null

UPDATE dbo.Dict SET Note='0,5' WHERE ItemName='DisplayModelID' AND ItemKey=1
UPDATE dbo.Dict SET Note='5' WHERE ItemName='DisplayModelID' AND ItemKey=3
UPDATE dbo.Dict SET Note='0,5' WHERE ItemName='DisplayModelID' AND ItemKey=4
UPDATE dbo.Dict SET Note='0,5' WHERE ItemName='DisplayModelID' AND ItemKey=5
UPDATE dbo.Dict SET ItemValue='考试模式' WHERE ItemName='DisplayModelID' AND ItemKey=8

UPDATE dbo.Dict SET Note='0,1,3,5' WHERE ItemName='DisplayModelID' AND ItemKey=9
UPDATE dbo.Dict SET Note='1,5' WHERE ItemName='DisplayModelID' AND ItemKey=10
UPDATE dbo.Dict SET Note='0,1,2,4' WHERE ItemName='DisplayModelID' AND ItemKey=11
UPDATE dbo.Dict SET Note='0,1,2,4,5' WHERE ItemName='DisplayModelID' AND ItemKey=12
INSERT INTO dbo.Dict VALUES('OperateTypeID',21,'进入设置界面',21,1,'')

INSERT INTO dbo.Dict VALUES('DisplayTemplateID',0,'班级活动',1,1,'ClsActive')
INSERT INTO dbo.Dict VALUES('DisplayTemplateID',1,'班级荣誉',2,1,'ClsHonor')
INSERT INTO dbo.Dict VALUES('DisplayTemplateID',2,'作业布置',3,1,'ClsHomeWk')
INSERT INTO dbo.Dict VALUES('DisplayTemplateID',3,'指标检查考勤',4,1,'ClsCheckItem')
INSERT INTO dbo.Dict VALUES('DisplayTemplateID',4,'学生出勤考勤',5,1,'ClsCheckStu')
INSERT INTO dbo.Dict VALUES('DisplayTemplateID',5,'通知公告',6,1,'ClsNotice')

CREATE table DeviceTaskNoticeInfo(
	NoticeID bigint  IDENTITY(1,1)  NOT NULL PRIMARY KEY ,
	TaskCode bigint NULL,
	NoticeTitle nvarchar(200) NULL,
	NoticeContent nvarchar(1000) NULL,
	NoticeCreateTime datetime NULL,
	NoticeTime datetime NULL
)


GO
/****** Object:  StoredProcedure [dbo].[spDeviceTaskInfoAdd]    Script Date: 07/13/2017 13:48:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------
--项目名称：
--说明：增加一条记录
--作者：hxx
--时间：2016-08-10 11:59:06
----------------------------------------
ALTER PROCEDURE [dbo].[spDeviceTaskInfoAdd]
    (
      @ID BIGINT OUTPUT ,
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @Code NVARCHAR(50) OUTPUT ,
      @DisplayModelID TINYINT ,
      @OperateTypeID TINYINT ,
      @MessageTitle NVARCHAR(500) ,
      @MessageTypeID TINYINT ,
      @MessageSourceID TINYINT ,
      @MessageContent NVARCHAR(MAX) ,
      @TargetRange NVARCHAR(MAX) ,
      @RargetAlias NVARCHAR(MAX) ,
      @TaskBeginTime SMALLDATETIME ,
      @TaskEndTime SMALLDATETIME ,
      @TaskPriorityID TINYINT ,
      @ImageSpanSecond TINYINT ,
      @ImageEffectID TINYINT ,
      @TaskTypeID TINYINT ,
      @TaskStatusID TINYINT ,
      @rootCode NVARCHAR(50) ,
      @Note NVARCHAR(4000) ,
      @CreatedID NVARCHAR(20) ,
      @CreatedName NVARCHAR(50),
      @Tag NVARCHAR(MAX) ,
       @Tag_and NVARCHAR(MAX) ,
        @Alias NVARCHAR(MAX) ,
        @Registration_ID NVARCHAR(MAX),
        @MessageContentAlias nvarchar(max),
        @AreaModule nvarchar(max)
    )
AS 
    BEGIN
        SET nocount ON;

        SET @ID = 0;
        SET @Errorcode = 0;
        SET @Errormsg = '';
        SET @Code = '';
        BEGIN TRAN  
        BEGIN TRY
            DECLARE @SN NVARCHAR(50);   
            DECLARE @scode2 VARCHAR(2);
            SET @scode2 = RIGHT('00' + CONVERT(VARCHAR(2), @MessageTypeID),
                                  2);
            EXEC [dbo].[spGetSerialNo] @Code1 = @rootCode, @Code2 = @scode2,
                @result = @SN OUTPUT;
	
            INSERT  INTO [DeviceTaskInfo]
                    ( [Code] ,
                      [DisplayModelID] ,
                      [OperateTypeID] ,
                      [MessageTitle] ,
                      [MessageTypeID] ,
                      [MessageSourceID] ,
                      [MessageContent] ,
                      [TargetRange] ,
                      [RargetAlias] ,
                      [TaskBeginTime] ,
                      [TaskEndTime] ,
                      [TaskPriorityID] ,
                      [ImageSpanSecond] ,
                      [ImageEffectID] ,
                      [TaskTypeID] ,
                      [TaskStatusID] ,
                      [TaskResultID] ,
                      [Note] ,
                      [CreatedID] ,
                      [CreatedName] ,
                      [CreatedDate] ,
                      [ModifiedID] ,
                      [ModifiedName] ,
                      [ModifiedDate],
                      [Tag],
                      [tag_and],
                      [Alias],
                      [Registration_ID],
                      [MessageContentAlias],
                      [AreaModule]
                    )
            VALUES  ( @SN ,
                      @DisplayModelID ,
                      @OperateTypeID ,
                      @MessageTitle ,
                      @MessageTypeID ,
                      @MessageSourceID ,
                      @MessageContent ,
                      @TargetRange ,
                      @RargetAlias ,
                      @TaskBeginTime ,
                      @TaskEndTime ,
                      @TaskPriorityID ,
                      @ImageSpanSecond ,
                      @ImageEffectID ,
                      @TaskTypeID ,
                      @TaskStatusID ,
                      0 ,
                      @Note ,
                      @CreatedID ,
                      @CreatedName ,
                      GETDATE() ,
                      0 ,
                      '' ,
                      NULL,
                      @Tag,
                      @Tag_and,
                      @Alias,
                      @Registration_ID,
                      @MessageContentAlias,
                      @AreaModule 
                    )
            SET @ID = SCOPE_IDENTITY();
            SET @Code =@SN;
            SET @Errorcode = 0;
            SET @Errormsg = '成功';
            COMMIT TRAN   
 
        END TRY   
 
        BEGIN CATCH   
 
            ROLLBACK TRAN   
            SET @ID = 0;
            SET @Code = '';
            SET @Errorcode = 1;
            SET @Errormsg = '失败';
 
        END CATCH 
  
        RETURN 0;
        SET nocount OFF;
    END
GO
/****** Object:  StoredProcedure [dbo].[spDeviceTaskNoticeInfoAdd]    Script Date: 07/17/2017 15:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------
--项目名称：
--说明：增加一条记录
--作者：hxx
--时间：2016-08-14 18:03:54
----------------------------------------
CREATE PROCEDURE [dbo].[spDeviceTaskNoticeInfoAdd]
    (
      @NoticeID BIGINT OUTPUT ,
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
      @TaskCode nvarchar(50) ,
      @NoticeTitle NVARCHAR(200) ,
      @NoticeContent NVARCHAR(MAX),
      @NoticeTime datetime
    )
AS 
    SET nocount ON;
	
    SET @NoticeID = 0;
    SET @Errorcode = 0;
    SET @Errormsg = '';


    INSERT  INTO [DeviceTaskNoticeInfo]
            ( [TaskCode],
              [NoticeTitle] ,
              [NoticeContent],
              [NoticeCreateTime] ,
              [NoticeTime]  
            )
    VALUES  ( @TaskCode ,
              @NoticeTitle,
              @NoticeContent,
               GETDATE(),
              @NoticeTime 
            )
    SET @NoticeID = SCOPE_IDENTITY();
    SET @Errorcode = 0;
    SET @Errormsg = '成功';
  

    RETURN 0;
    SET nocount OFF;
