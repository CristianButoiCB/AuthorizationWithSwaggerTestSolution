USE [master]
GO
/****** Object:  Database [GoodsTest]    Script Date: 1/31/2024 12:18:18 PM ******/
CREATE DATABASE [GoodsTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GoodsTest', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL11.CRISTILAPTOP1112\MSSQL\DATA\GoodsTest.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GoodsTest_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL11.CRISTILAPTOP1112\MSSQL\DATA\GoodsTest_log.ldf' , SIZE = 4096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GoodsTest] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GoodsTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GoodsTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GoodsTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GoodsTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GoodsTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GoodsTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [GoodsTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GoodsTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GoodsTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GoodsTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GoodsTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GoodsTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GoodsTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GoodsTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GoodsTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GoodsTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GoodsTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GoodsTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GoodsTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GoodsTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GoodsTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GoodsTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GoodsTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GoodsTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GoodsTest] SET  MULTI_USER 
GO
ALTER DATABASE [GoodsTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GoodsTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GoodsTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GoodsTest] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GoodsTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GoodsTest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GoodsTest] SET QUERY_STORE = ON
GO
ALTER DATABASE [GoodsTest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GoodsTest]
GO
/****** Object:  User [sn]    Script Date: 1/31/2024 12:18:18 PM ******/
CREATE USER [sn] FOR LOGIN [sn] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [sn]
GO
ALTER ROLE [db_datareader] ADD MEMBER [sn]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [sn]
GO
/****** Object:  Table [dbo].[BPType]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BPType](
	[TypeCode] [nvarchar](1) NOT NULL,
	[TypeName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_BPType] PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessPartners]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPartners](
	[BPCode] [nvarchar](128) NOT NULL,
	[BPName] [nvarchar](254) NOT NULL,
	[BPType] [nvarchar](1) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_BusinessPartners] PRIMARY KEY CLUSTERED 
(
	[BPCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemCode] [nvarchar](128) NOT NULL,
	[ItemName] [nvarchar](254) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrders]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BPCode] [nvarchar](128) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrdersLines]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrdersLines](
	[LineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NOT NULL,
	[ItemCode] [nvarchar](128) NOT NULL,
	[Quantity] [decimal](38, 18) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_PurchaseOrdersLines] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrders]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BPCode] [nvarchar](128) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_SaleOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrdersLines]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrdersLines](
	[LineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NOT NULL,
	[ItemCode] [nvarchar](128) NOT NULL,
	[Quantity] [decimal](38, 18) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_SaleOrdersLines] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrdersLinesComments]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrdersLinesComments](
	[CommentLineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NOT NULL,
	[LineID] [int] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SaleOrdersLinesComments] PRIMARY KEY CLUSTERED 
(
	[CommentLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/31/2024 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](1024) NOT NULL,
	[UserName] [nvarchar](254) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ClusteredIndex-20240125-102856]    Script Date: 1/31/2024 12:18:18 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20240125-102856] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
INSERT [dbo].[BPType] ([TypeCode], [TypeName]) VALUES (N'C', N'Customer')
GO
INSERT [dbo].[BPType] ([TypeCode], [TypeName]) VALUES (N'V', N'Vendor')
GO
INSERT [dbo].[BusinessPartners] ([BPCode], [BPName], [BPType], [Active]) VALUES (N'C0001', N'Customer 1', N'C', 1)
GO
INSERT [dbo].[BusinessPartners] ([BPCode], [BPName], [BPType], [Active]) VALUES (N'C0002', N'Customer 2', N'C', 0)
GO
INSERT [dbo].[BusinessPartners] ([BPCode], [BPName], [BPType], [Active]) VALUES (N'V0001', N'Vendor 1', N'V', 1)
GO
INSERT [dbo].[BusinessPartners] ([BPCode], [BPName], [BPType], [Active]) VALUES (N'V0002', N'Vendor 2', N'V', 0)
GO
INSERT [dbo].[Items] ([ItemCode], [ItemName], [Active]) VALUES (N'Itm1', N'Item 1', 1)
GO
INSERT [dbo].[Items] ([ItemCode], [ItemName], [Active]) VALUES (N'Itm2', N'Item 2', 1)
GO
INSERT [dbo].[Items] ([ItemCode], [ItemName], [Active]) VALUES (N'Itm3', N'Item 3', 0)
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrders] ON 
GO
INSERT [dbo].[PurchaseOrders] ([ID], [BPCode], [CreateDate], [LastUpdateDate], [CreatedBy], [LastUpdatedBy]) VALUES (5, N'C0001', CAST(N'2024-01-30T18:06:24.990' AS DateTime), CAST(N'2024-01-31T10:31:10.307' AS DateTime), 2, 2)
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrdersLines] ON 
GO
INSERT [dbo].[PurchaseOrdersLines] ([LineID], [DocID], [ItemCode], [Quantity], [CreateDate], [LastUpdateDate], [CreatedBy], [LastUpdatedBy]) VALUES (7, 5, N'Itm2', CAST(30.000000000000000000 AS Decimal(38, 18)), CAST(N'2024-01-30T18:06:24.990' AS DateTime), CAST(N'2024-01-31T10:31:10.307' AS DateTime), 2, 2)
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrdersLines] OFF
GO
SET IDENTITY_INSERT [dbo].[SaleOrders] ON 
GO
INSERT [dbo].[SaleOrders] ([ID], [BPCode], [CreateDate], [LastUpdateDate], [CreatedBy], [LastUpdatedBy]) VALUES (7, N'C0001', CAST(N'2024-01-31T12:09:06.737' AS DateTime), NULL, 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[SaleOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[SaleOrdersLines] ON 
GO
INSERT [dbo].[SaleOrdersLines] ([LineID], [DocID], [ItemCode], [Quantity], [CreateDate], [LastUpdateDate], [CreatedBy], [LastUpdatedBy]) VALUES (2, 7, N'Itm2', CAST(390.000000000000000000 AS Decimal(38, 18)), CAST(N'2024-01-31T12:09:06.737' AS DateTime), NULL, 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[SaleOrdersLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([ID], [FullName], [UserName], [Password], [Active]) VALUES (2, N'U1_FULL', N'U1', N'P1', 1)
GO
INSERT [dbo].[Users] ([ID], [FullName], [UserName], [Password], [Active]) VALUES (3, N'U2_FULL', N'U2', N'P2', 0)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [PK_Users]    Script Date: 1/31/2024 12:18:19 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusinessPartners] ADD  CONSTRAINT [DF_BusinessPartners_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessPartners]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPartners_BPType] FOREIGN KEY([BPType])
REFERENCES [dbo].[BPType] ([TypeCode])
GO
ALTER TABLE [dbo].[BusinessPartners] CHECK CONSTRAINT [FK_BusinessPartners_BPType]
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrders_BusinessPartners] FOREIGN KEY([BPCode])
REFERENCES [dbo].[BusinessPartners] ([BPCode])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_BusinessPartners]
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrders_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_Users]
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrders_Users1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_Users1]
GO
ALTER TABLE [dbo].[PurchaseOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrdersLines_Items] FOREIGN KEY([ItemCode])
REFERENCES [dbo].[Items] ([ItemCode])
GO
ALTER TABLE [dbo].[PurchaseOrdersLines] CHECK CONSTRAINT [FK_PurchaseOrdersLines_Items]
GO
ALTER TABLE [dbo].[PurchaseOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrdersLines_PurchaseOrdersLines] FOREIGN KEY([DocID])
REFERENCES [dbo].[PurchaseOrders] ([ID])
GO
ALTER TABLE [dbo].[PurchaseOrdersLines] CHECK CONSTRAINT [FK_PurchaseOrdersLines_PurchaseOrdersLines]
GO
ALTER TABLE [dbo].[PurchaseOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrdersLines_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PurchaseOrdersLines] CHECK CONSTRAINT [FK_PurchaseOrdersLines_Users]
GO
ALTER TABLE [dbo].[PurchaseOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrdersLines_Users1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PurchaseOrdersLines] CHECK CONSTRAINT [FK_PurchaseOrdersLines_Users1]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_BusinessPartners] FOREIGN KEY([BPCode])
REFERENCES [dbo].[BusinessPartners] ([BPCode])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_BusinessPartners]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_Users]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_Users1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_Users1]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_Items] FOREIGN KEY([ItemCode])
REFERENCES [dbo].[Items] ([ItemCode])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_Items]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_SaleOrders] FOREIGN KEY([DocID])
REFERENCES [dbo].[SaleOrders] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_SaleOrders]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_SaleOrdersLines] FOREIGN KEY([LineID])
REFERENCES [dbo].[SaleOrdersLines] ([LineID])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_SaleOrdersLines]
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLines] FOREIGN KEY([LineID])
REFERENCES [dbo].[SaleOrdersLines] ([LineID])
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments] CHECK CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLines]
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLinesComments] FOREIGN KEY([DocID])
REFERENCES [dbo].[SaleOrders] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments] CHECK CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLinesComments]
GO
USE [master]
GO
ALTER DATABASE [GoodsTest] SET  READ_WRITE 
GO
