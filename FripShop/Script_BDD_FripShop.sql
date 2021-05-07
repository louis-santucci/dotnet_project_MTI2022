CREATE DATABASE [Fripshop]
 CONTAINMENT = NONE
GO
ALTER DATABASE [Fripshop] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [Fripshop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Fripshop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Fripshop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Fripshop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Fripshop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Fripshop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Fripshop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Fripshop] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)
GO
ALTER DATABASE [Fripshop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Fripshop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Fripshop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Fripshop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Fripshop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Fripshop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Fripshop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Fripshop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Fripshop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Fripshop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Fripshop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Fripshop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Fripshop] SET  READ_WRITE 
GO
ALTER DATABASE [Fripshop] SET RECOVERY FULL 
GO
ALTER DATABASE [Fripshop] SET  MULTI_USER 
GO
ALTER DATABASE [Fripshop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Fripshop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Fripshop] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Fripshop]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = On;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = Primary;
GO
USE [Fripshop]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [Fripshop] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO

CREATE TABLE [dbo].[User] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [userName] NVARCHAR (50)  NOT NULL,
    [email]    NVARCHAR (50)  NOT NULL,
    [password] NVARCHAR (40)  NOT NULL,
    [name]     NVARCHAR (50)  NOT NULL,
    [address]  NVARCHAR (100) NOT NULL,
    [gender]   NVARCHAR (10)  NOT NULL,
    [note]     FLOAT (53)     NOT NULL,
    [nbNotes]  BIGINT         CONSTRAINT [DF_User_nbNotes] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_User] UNIQUE NONCLUSTERED ([id] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_1]
    ON [dbo].[User]([userName] ASC);


	CREATE TABLE [dbo].[Article] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [imageSource] NVARCHAR (100) NULL,
    [sellerId]    BIGINT         NOT NULL,
    [state]       NVARCHAR (20)  NOT NULL,
    [name]        NVARCHAR (50)  NOT NULL,
    [price]       FLOAT (53)     NOT NULL,
    [description] NVARCHAR (200) NULL,
    [category]    NVARCHAR (30)  NULL,
    [sex]         NVARCHAR (10)  NULL,
    [brand]       NVARCHAR (20)  NULL,
    [condition]   INT            NOT NULL,
    [createdAt]   DATETIME       NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Article_User] FOREIGN KEY ([sellerId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
);





CREATE TABLE [dbo].[Cart] (
    [id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [buyerId]   BIGINT NOT NULL,
    [articleId] BIGINT NOT NULL,
    [quantity]  INT    NOT NULL,
    CONSTRAINT [PK_Cart_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Cart_Article] FOREIGN KEY ([articleId]) REFERENCES [dbo].[Article] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cart_User] FOREIGN KEY ([buyerId]) REFERENCES [dbo].[User] ([id])
);





CREATE TABLE [dbo].[Transaction] (
    [id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [articleId]        BIGINT        NOT NULL,
    [buyerId]          BIGINT        NOT NULL,
    [transactionState] NVARCHAR (20) NOT NULL,
    [lastUpdateAt]     DATETIME      NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Transaction_Article] FOREIGN KEY ([articleId]) REFERENCES [dbo].[Article] ([id]),
    CONSTRAINT [FK_Transaction_User] FOREIGN KEY ([buyerId]) REFERENCES [dbo].[User] ([id])
);


USE [FripShop]
GO

INSERT INTO [dbo].[User]
           ([userName]
           ,[email]
           ,[password]
           ,[name]
           ,[address]
           ,[gender]
           ,[note]
           ,[nbNotes])
     VALUES
           ('test_user'
           ,'test@test.com'
           ,'098f6bcd4621d373cade4e832627b4f6'
           ,'Test'
           ,'23 Old Street TESTCITY'
           ,'man'
           ,'0'
           ,'0')


INSERT INTO [dbo].[Article]
           ([imageSource]
           ,[sellerId]
           ,[state]
           ,[name]
           ,[price]
           ,[description]
           ,[category]
           ,[sex]
           ,[brand]
           ,[condition]
           ,[createdAt])
     VALUES
           ('.\ArticleImages\hautbleu.jpg'
		   ,'1'
           ,'free'
           ,'Joli haut bleu'
           ,'29.99'
           ,'Joli haut bleu'
           ,'top'
           ,'man'
           ,'nike'
           ,'6'
           ,'2021-05-07T16:00:00'),

		   ('.\ArticleImages\arcenciel.jpg'
		   ,'1'
           ,'free'
           ,'Pantalon multicolore T36'
           ,'5'
           ,'Joli pantalon multicolore'
           ,'pants'
           ,'woman'
           ,NULL
           ,'7'
           ,'2021-05-07T16:00:00')
GO