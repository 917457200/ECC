--***************************************
--新增了数据库日志表

USE [ECCDB]
GO

/****** Object:  Table [dbo].[operatelog]    Script Date: 09/27/2016 17:23:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[operatelog]') AND type in (N'U'))
DROP TABLE [dbo].[operatelog]
GO

USE [ECCDB]
GO

/****** Object:  Table [dbo].[operatelog]    Script Date: 09/27/2016 17:23:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[operatelog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[otype] [varchar](8) NULL,
	[module] [nvarchar](16) NULL,
	[functionName] [varchar](max) NULL,
	[pageurl] [varchar](max) NULL,
	[logcontent] [nvarchar](max) NULL,
	[cuser] [bigint] NULL,
	[ctime] [datetime] NULL,
 CONSTRAINT [PK_OPERATELOG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'otype'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'module'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'pageurl'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'logcontent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'cuser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog', @level2type=N'COLUMN',@level2name=N'ctime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户操作日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'operatelog'
GO

--***************************************

--增加了添加日志存储过程
USE [ECCDB]
GO
/****** Object:  StoredProcedure [dbo].[p_operatelog_Add]    Script Date: 09/27/2016 17:24:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[p_operatelog_Add]  
( @module nvarchar(500), @pageurl nvarchar(500), @otype varchar(8), @logcontent nvarchar(MAX), @cuser BIGINT,@functionName nvarchar(500)  
)  
 AS   
 set nocount ON;  
 BEGIN  
  INSERT INTO [operatelog]([module],[pageurl],[otype],[logcontent],[cuser],[ctime],[functionName])  
  VALUES(@module,@pageurl,@otype,@logcontent,@cuser,GETDATE(),@functionName)  
  RETURN 0;  
 END  
 set nocount OFF;  