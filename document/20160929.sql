USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_Add]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DeviceTaskInfo_Add]
GO
/****** Object:  View [dbo].[UserClass_View]    Script Date: 09/29/2016 11:50:38 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[UserClass_View]'))
DROP VIEW [dbo].[UserClass_View]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_UpdateTaskResultID]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_UpdateTaskResultID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DeviceTaskInfo_UpdateTaskResultID]
GO
/****** Object:  StoredProcedure [dbo].[p_operatelog_Add]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_operatelog_Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_operatelog_Add]
GO
/****** Object:  StoredProcedure [dbo].[GetSerialNo]    Script Date: 09/29/2016 11:50:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSerialNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSerialNo]
GO
/****** Object:  StoredProcedure [dbo].[p_DataFieldInfo_Add]    Script Date: 09/29/2016 11:50:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DataFieldInfo_Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DataFieldInfo_Add]
GO
/****** Object:  StoredProcedure [dbo].[p_Device_Init]    Script Date: 09/29/2016 11:50:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_Device_Init]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_Device_Init]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceClassInfo_Update]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceClassInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DeviceClassInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[pPagingLarge]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pPagingLarge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pPagingLarge]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_Delete]    Script Date: 09/29/2016 11:50:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_DeviceTaskInfo_Delete]
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_Delete]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：删除记录，支持多条删除
--作者：hxx
--时间：2016-08-25 18:03:54
----------------------------------------
CREATE PROCEDURE [dbo].[p_DeviceTaskInfo_Delete]
(
	@IDs varchar(max)
)
 AS
	declare @sql varchar(max)
	set nocount ON;
	set @sql =
	''UPDATE DeviceTaskInfo SET [TaskStatusID]=2 WHERE ID in(''+@IDs+'')'';
	Exec(@sql)
	RETURN 0;
	set nocount OFF;
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pPagingLarge]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pPagingLarge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pPagingLarge] (      
    @tablenames  varchar(MAX),   --表名，可以是多个表，用别名    
    @primarykey  varchar(100),   --主键，可以为空，但@order为空时该值不能为空    
    @fields   varchar(3000) = ''*'',  --要取出的字段，可以是多个表的字段，可以为空，为空表示select *      
    @pagesize  int,     --每页记录数    
    @currentpage int,     --当前页，表示第页    
    @recnums  int output,    --记录个数    
    @pagenums  int output,    -- 页数    
    @filter   varchar(2000) = '''',  --条件，可以为空，不用填where      
    @group   varchar(200) = '''',  --分组依据，可以为空，不用填group by      
    @order   varchar(200) = ''''  --排序，可以为空，为空默认按主键升序排列，不用填order by      
   ) AS    
       
begin      
 SET NOCOUNT ON    
     
 DECLARE @sql NVARCHAR(max)    
 DECLARE @sqltemp NVARCHAR(max)    
 DECLARE @group2 varchar(200)    
    
 IF @fields IS NULL OR LTRIM(RTRIM(@fields)) = ''''    
 BEGIN    
  SET @fields = ''*''    
 END    
     
 -- 判断条件    
 SET @filter = LTRIM(RTRIM(ISNULL(@filter,'''')))    
 IF @filter <> ''''    
 BEGIN    
  IF UPPER(SUBSTRING(@filter,1,5)) <> ''WHERE''    
  BEGIN    
   SET @filter = ''WHERE '' + @filter    
  END    
 END    
     
 -- 判断排序    
 SET @order = LTRIM(RTRIM(ISNULL(@order,'''')))    
 IF @order <> ''''    
 BEGIN    
  IF UPPER(SUBSTRING(@order,1,5)) <> ''ORDER''    
  BEGIN    
   SET @order = ''ORDER BY '' + @order     
  END    
 END    
 ELSE    
 BEGIN    
  SET @order = ''ORDER BY '' + @primarykey    
 END    
     
 -- 判断分组    
 set @group2=@group    
 SET @group = LTRIM(RTRIM(ISNULL(@group2,'''')))    
 IF @group <> ''''    
 BEGIN    
  IF UPPER(SUBSTRING(@group,1,5)) <> ''GROUP''    
  BEGIN    
   SET @group = ''GROUP BY '' + @group     
  END    
 END    
    
/* 校验页码与页大小*/    
IF @currentpage IS NULL OR @currentpage < 1    
BEGIN    
 SET @currentpage = 1    
END    
    
IF @pagesize IS NULL OR @pagesize < 1    
BEGIN    
 SET @pagesize = 10    
END /*校验页码与页大小结束*/    
    
    
-- 若是第一页，则计算总记录个数    
--IF @currentpage = 1    
--BEGIN    
 if(@group <> '''')    
 begin    
  set @group2 = replace(replace(UPPER(@group2),'' ASC'',''''),'' DESC'','''')    
  --set @sqltemp = ''select @counts=COUNT(1)from(Select ''+@group2+'' FROM   '' + @tablenames + '' '' + @filter+ '' '' + @group +'')as XX'';    
  set @sqltemp = ''WITH XX(a)AS(Select 1 FROM  '' + @tablenames + '' '' + @filter+ '' '' + @group +'')select @counts=COUNT(1)FROM XX;'';    
 end     
 else    
 begin    
  set @sqltemp = ''Select @counts=count(1) FROM   '' + @tablenames + '' '' + @filter+ '' '' + @group    
 end    
 --取得查询结果总数量-----    
 exec sp_executesql @sqltemp,N''@counts int out'',@recnums OUT    
     
 --取得分页总数    
 if (@recnums %@pagesize>0)    
  set @pagenums = (@recnums / @pagesize) + 1    
 else    
  set @pagenums = (@recnums / @pagesize)    
--END    
SELECT @sql = ''with RowNumberTableSource'' + char(13) + char(10) + ''AS'' + char(13) + char(10) +    
''(select '' + @fields + '',row_number() over( '' + @order + '') as RowNumber    
from '' + @tablenames + '' '' + @filter +'' ''+@group+ '' )'' + CHAR(13) + CHAR(10) +    
''select * from RowNumberTableSource where RowNumber between '' + CAST(((@currentpage - 1)*@pagesize+1) AS VARCHAR)    
+ '' and '' +    
CAST((@currentpage*@pagesize) AS VARCHAR)    
    
--PRINT @sql    
    
--返回查询结果-----    
exec sp_executesql @sql    
    
end ' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceClassInfo_Update]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceClassInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：修改一条记录
--作者：hxx
--时间：09/17/2016 14:37:48
----------------------------------------
CREATE PROCEDURE [dbo].[p_DeviceClassInfo_Update]
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
        SET @Errormsg = '''';
     
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
                SET @Errormsg = ''成功'';
             
            END
        ELSE 
            BEGIN
                SET @Errorcode = 1;
                SET @Errormsg = ''失败'';
            END
        RETURN 0;
        SET nocount OFF;
    END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_Device_Init]    Script Date: 09/29/2016 11:50:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_Device_Init]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		hxx
-- Create date: 2016.0801
-- Description:	初始化设备电子班牌相关数据
-- =============================================
CREATE PROCEDURE [dbo].[p_Device_Init]
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
        SET @Errormsg = '''';
        UPDATE [dbo].[DeviceClassInfo] SET [JPushID]='''' WHERE [JPushID]=@JPushID;
        IF EXISTS ( SELECT  id
                    FROM    [dbo].[DeviceClassInfo]
                    WHERE   [ClassCode] = @ClassCode ) 
            BEGIN
                IF ( @IsCheckInt = 1
                     AND EXISTS ( SELECT    id
                                  FROM      [dbo].[DeviceClassInfo]
                                  WHERE     [ClassCode] = @ClassCode
                                            AND [DeviceSN] IS NOT NULL
                                            AND [DeviceSN] <> '''' )
                   ) 
                    BEGIN
                        SET @errorcode = 2;
                        SET @errormsg = ''该班级设备数据已经初始化'';
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
                                SET @className='''';
                                SET @deviceCode='''';
                                SELECT  @className = [ClassName] ,
                                        @deviceCode = [DeviceCode]
                                FROM    [dbo].[DeviceClassInfo]
                                WHERE   [RoomNum] = @RoomNum
                                SET @errorcode = 3;
                                SET @errormsg = ''初始化失败：房间号已经被'' + @className
                                    + ''(设备编号:'' + @deviceCode + '')占用'';
                            END
                        ELSE 
                            BEGIN
                                IF EXISTS ( SELECT  id
                                            FROM    [dbo].[DeviceClassInfo]
                                            WHERE   [ClassCode] = @ClassCode
                                                    AND [DeviceSN] IS NOT NULL
                                                    AND [DeviceSN] <> '''' ) 
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
                                        SET @errormsg = ''初始化成功'';
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
                                        SET @errormsg = ''初始化成功'';
                                    END 
                            END
                    END
            END
        ELSE 
            BEGIN
                SET @errorcode = 1;
                SET @errormsg = ''初始化失败：数据异常'';
            END
	
        RETURN 0;
        SET NOCOUNT OFF;
    END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_DataFieldInfo_Add]    Script Date: 09/29/2016 11:50:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DataFieldInfo_Add]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：增加一条记录
--作者：hxx
--时间：2016-08-14 18:03:54
----------------------------------------
CREATE PROCEDURE [dbo].[p_DataFieldInfo_Add]
    (
      @ID BIGINT OUTPUT ,
      @Errorcode INT OUTPUT ,
      @Errormsg NVARCHAR(256) OUTPUT ,
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
    SET @Errormsg = '''';


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
    SET @Errorcode = 0;
    SET @Errormsg = ''成功'';
  

    RETURN 0;
    SET nocount OFF;
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSerialNo]    Script Date: 09/29/2016 11:50:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSerialNo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetSerialNo]   
(   
    @Code1 varchar(50),
    @Code2 VARCHAR(50),
    @result VARCHAR(180)  OUTPUT 
)   
 
  as 
 
--exec GetSerialNo   
 
begin 
 
   Declare @sValue  varchar(16),   
 
           @dToday   datetime,           
 
           @sQZ1  varchar(50),  --这个代表前缀 
           @sQZ2  varchar(50)  --这个代表前缀 
 
   Begin Tran     
 
   Begin Try   
 
 

     -- 锁定该条记录，好多人用lock去锁，起始这里只要执行一句update就可以了 
    --在同一个事物中，执行了update语句之后就会启动锁 
     Update SerialNo set [SNValue]=[SNValue] where Code1=@Code1 AND Code2=@Code2  
 
     Select @sValue = [SNValue],@sQZ1=QZ1,@sQZ2=QZ2 From SerialNo where Code1=@Code1 AND Code2=@Code2  
 
     -- 因子表中没有记录，插入初始值   
 
     If @sValue is null   
 
     Begin 

       Select @sValue = convert(bigint, ''20''+convert(varchar(6), getdate(), 12)+@Code2 + ''000001'')   

       Update SerialNo set [SNValue]=@sValue where Code1=@Code1 AND Code2=@Code2  
 
     end else   
 
     Begin               --因子表中没有记录   
 
       Select @dToday = substring(@sValue,1,8)   
  
 
       --如果日期相等，则加1   
 
       If @dToday = (''20''+convert(varchar(6), getdate(), 12))   
 
         Select @sValue = convert(varchar(16), (convert(bigint, @sValue) + 1))   
 
       else              --如果日期不相等，则先赋值日期，流水号从1开始   
 
         Select @sValue = convert(bigint, ''20''+convert(varchar(6), getdate(), 12)+@Code2 +''000001'')   
 
           
 
       Update SerialNo set [SNValue]=@sValue where Code1=@Code1 AND Code2=@Code2  
 
     End 
 
     Select @result = @sQZ1+@sValue;
 
     Commit Tran   
 
   End Try   
 
   Begin Catch   
 
     Rollback Tran   
 
     Select result = ''Error'' 
 
   End Catch   
 
end 
 

' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_operatelog_Add]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_operatelog_Add]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[p_operatelog_Add]  
( @module nvarchar(500), @pageurl nvarchar(500), @otype varchar(8), @logcontent nvarchar(MAX), @cuser BIGINT,@functionName nvarchar(500)  
)  
 AS   
 set nocount ON;  
 BEGIN  
  INSERT INTO [operatelog]([module],[pageurl],[otype],[logcontent],[cuser],[ctime],[functionName])  
  VALUES(@module,@pageurl,@otype,@logcontent,@cuser,GETDATE(),@functionName)  
  RETURN 0;  
 END  
 set nocount OFF;  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_UpdateTaskResultID]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_UpdateTaskResultID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：修改一条记录
--作者：hxx
--时间：2016-08-10 11:59:06
----------------------------------------
CREATE PROCEDURE [dbo].[p_DeviceTaskInfo_UpdateTaskResultID]
(
	@errorcode INT OUTPUT,
	@errormsg NVARCHAR(256) OUTPUT,
	@Code NVARCHAR(50),
	@TaskResultID tinyint
)
 AS
	set nocount ON;
	SET @errorcode=0;SET @errormsg='''';
	IF EXISTS(Select [ID] from [dbo].[DeviceTaskInfo] Where Code=@Code)
	BEGIN 
		UPDATE [dbo].[DeviceTaskInfo] SET [TaskResultID]=@TaskResultID WHERE Code=@Code;
		SET @errorcode=0;
		SET @errormsg=''成功'';
	END
	ELSE
	BEGIN
		SET @errorcode=1;
		SET @errormsg=''失败：数据异常'';
	END
	RETURN 0;
	set nocount OFF;
' 
END
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：修改一条记录
--作者：hxx
--时间：2016-08-10 11:59:06
----------------------------------------
CREATE PROCEDURE [dbo].[p_DeviceTaskInfo_UpdateOperateTypeID]
(
	@errorcode INT OUTPUT,
	@errormsg NVARCHAR(256) OUTPUT,
	@Code NVARCHAR(50),
	@OperateTypeID TINYINT,
	@ModifiedID NVARCHAR(20),
	@ModifiedName NVARCHAR(50)
)
 AS
	set nocount ON;
	SET @errorcode=0;SET @errormsg='''';
	IF EXISTS(Select [ID] from [dbo].[DeviceTaskInfo] Where Code=@Code)
	BEGIN 
		UPDATE [dbo].[DeviceTaskInfo] SET [OperateTypeID]=@OperateTypeID,[ModifiedID]=@ModifiedID,[ModifiedName]=@ModifiedName,[ModifiedDate]=GETDATE() WHERE Code=@Code;
		SET @errorcode=0;
		SET @errormsg=''成功'';
	END
	ELSE
	BEGIN
		SET @errorcode=1;
		SET @errormsg=''失败：数据异常'';
	END
	RETURN 0;
	set nocount OFF;
' 
END
GO
/****** Object:  View [dbo].[UserClass_View]    Script Date: 09/29/2016 11:50:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[UserClass_View]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[UserClass_View]
AS
SELECT     users.ID, users.UserCode, users.UserType, users.ClassCode, users.ClassFullCode, users.ClassName, users.RoleType, users.IsValid, users.Note, users.HandledID, 
                      users.HandledName, users.HandledDate, device.SubjectTypeID, device.SubjectTypeIDText, device.ClassTypeID, device.ClassTypeIDText, device.JPushID
FROM         dbo.UserClassInfo AS users INNER JOIN
                      dbo.DeviceClassInfo AS device ON users.ClassCode = device.ClassCode
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'UserClass_View', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 231
               Right = 193
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "device"
            Begin Extent = 
               Top = 6
               Left = 231
               Bottom = 266
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserClass_View'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'UserClass_View', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserClass_View'
GO
/****** Object:  StoredProcedure [dbo].[p_DeviceTaskInfo_Add]    Script Date: 09/29/2016 11:50:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_DeviceTaskInfo_Add]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------
--项目名称：
--说明：增加一条记录
--作者：hxx
--时间：2016-08-10 11:59:06
----------------------------------------
CREATE PROCEDURE [dbo].[p_DeviceTaskInfo_Add]
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
        @MessageContentAlias nvarchar(max)
    )
AS 
    BEGIN
        SET nocount ON;

        SET @ID = 0;
        SET @Errorcode = 0;
        SET @Errormsg = '''';
        SET @Code = '''';
        BEGIN TRAN  
        BEGIN TRY
            DECLARE @SN NVARCHAR(50);   
            DECLARE @scode2 VARCHAR(2);
            SET @scode2 = RIGHT(''00'' + CONVERT(VARCHAR(2), @MessageTypeID),
                                  2);
            EXEC [dbo].[GetSerialNo] @Code1 = @rootCode, @Code2 = @scode2,
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
                      [MessageContentAlias]
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
                      '''' ,
                      NULL,
                      @Tag,
                      @Tag_and,
                      @Alias,
                      @Registration_ID,
                      @MessageContentAlias
                    )
            SET @ID = SCOPE_IDENTITY();
            SET @Code =@SN;
            SET @Errorcode = 0;
            SET @Errormsg = ''成功'';
            COMMIT TRAN   
 
        END TRY   
 
        BEGIN CATCH   
 
            ROLLBACK TRAN   
            SET @ID = 0;
            SET @Code = '''';
            SET @Errorcode = 1;
            SET @Errormsg = ''失败'';
 
        END CATCH 
  
        RETURN 0;
        SET nocount OFF;
    END
' 
END
GO
