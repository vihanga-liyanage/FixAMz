USE [master]
GO
/****** Object:  Database [FixAMz]    Script Date: 1/10/2016 3:53:48 PM ******/
CREATE DATABASE [FixAMz]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FixAMz', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\FixAMz.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FixAMz_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\FixAMz_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FixAMz] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FixAMz].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FixAMz] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FixAMz] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FixAMz] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FixAMz] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FixAMz] SET ARITHABORT OFF 
GO
ALTER DATABASE [FixAMz] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FixAMz] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [FixAMz] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FixAMz] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FixAMz] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FixAMz] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FixAMz] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FixAMz] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FixAMz] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FixAMz] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FixAMz] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FixAMz] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FixAMz] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FixAMz] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FixAMz] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FixAMz] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FixAMz] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FixAMz] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FixAMz] SET RECOVERY FULL 
GO
ALTER DATABASE [FixAMz] SET  MULTI_USER 
GO
ALTER DATABASE [FixAMz] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FixAMz] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FixAMz] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FixAMz] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FixAMz', N'ON'
GO
USE [FixAMz]
GO
/****** Object:  Table [dbo].[Asset]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Asset](
	[assetID] [char](20) NOT NULL,
	[costID] [char](15) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[value] [float] NOT NULL,
	[salvageValue] [float] NOT NULL,
	[updatedValue] [float] NOT NULL,
	[category] [char](15) NOT NULL,
	[subcategory] [char](15) NOT NULL,
	[owner] [char](15) NOT NULL,
	[status] [varchar](8) NOT NULL,
	[location] [char](15) NOT NULL,
	[approvedDate] [datetime] NULL,
	[recommend] [char](15) NOT NULL,
	[approve] [char](15) NULL,
 CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED 
(
	[assetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[catID] [char](15) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[catID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CostCenter](
	[costID] [char](15) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[recommendPerson] [char](15) NOT NULL,
	[approvePerson] [char](15) NOT NULL,
 CONSTRAINT [PK_Cost_Center] PRIMARY KEY CLUSTERED 
(
	[costID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CostLocation]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CostLocation](
	[locID] [char](15) NOT NULL,
	[costID] [char](15) NOT NULL,
 CONSTRAINT [PK_Location_CostID] PRIMARY KEY CLUSTERED 
(
	[locID] ASC,
	[costID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DisposeAsset]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DisposeAsset](
	[dispID] [char](15) NOT NULL,
	[assetID] [char](20) NOT NULL,
	[date] [date] NULL,
	[description] [varchar](100) NOT NULL,
	[recommend] [char](15) NOT NULL,
	[approve] [char](15) NOT NULL,
 CONSTRAINT [PK_Dispose_Asset] PRIMARY KEY CLUSTERED 
(
	[dispID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[empID] [char](15) NOT NULL,
	[costID] [char](15) NOT NULL,
	[firstName] [varchar](15) NOT NULL,
	[lastName] [varchar](15) NOT NULL,
	[contactNo] [varchar](10) NOT NULL,
	[email] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[empID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Location]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[locID] [char](15) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[address] [varchar](50) NOT NULL,
	[contactNo] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[locID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[notID] [char](15) NOT NULL,
	[type] [varchar](20) NOT NULL,
	[action] [varchar](20) NOT NULL,
	[assetID] [char](20) NULL,
	[notContent] [varchar](100) NOT NULL,
	[sendUser] [char](15) NOT NULL,
	[receiveUser] [char](15) NOT NULL,
	[date] [datetime] NULL,
	[status] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[notID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubCategory](
	[scatID] [char](15) NOT NULL,
	[catID] [char](15) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[depreciationRate] [int] NOT NULL,
	[lifetime] [int] NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[scatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemUser](
	[empID] [char](15) NOT NULL,
	[costID] [char](15) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[type] [varchar](30) NOT NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[empID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransferAsset]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransferAsset](
	[transID] [char](15) NOT NULL,
	[assetID] [char](20) NOT NULL,
	[costID] [char](20) NOT NULL,
	[type] [varchar](20) NOT NULL,
	[status] [varchar](20) NOT NULL,
	[date] [datetime] NULL,
	[location] [char](15) NOT NULL,
	[owner] [char](15) NOT NULL,
	[recommend] [char](15) NOT NULL,
	[approve] [char](15) NULL,
 CONSTRAINT [PK_Transfer_Asset] PRIMARY KEY CLUSTERED 
(
	[transID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UpgradeAsset]    Script Date: 1/10/2016 3:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpgradeAsset](
	[upID] [char](15) NOT NULL,
	[assetID] [char](20) NOT NULL,
	[value] [float] NOT NULL,
	[updatedValue] [float] NOT NULL,
	[date] [datetime] NULL,
	[description] [varchar](100) NOT NULL,
	[recommend] [char](15) NOT NULL,
	[approve] [char](15) NULL,
	[status] [varchar](20) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000001', N'CC002          ', N'Executive Table', 27600, 8300, 27600, N'C00001         ', N'SC00001        ', N'E00004         ', N'1', N'L00002         ', CAST(0x0000A58900B4C300 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000002', N'CC002          ', N'HP pavilion g4 laptop', 73000, 15000, 73000, N'C00002         ', N'SC00002        ', N'E00006         ', N'1', N'L00002         ', CAST(0x0000A58900B4C300 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000003', N'CC002          ', N'Executive chair', 14000, 6000, 13440, N'C00001         ', N'SC00004        ', N'E00006         ', N'1', N'L00002         ', CAST(0x0000A41C00BABEA4 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000004', N'CC002          ', N'DELL Pc', 56500, 13000, 46060, N'C00002         ', N'SC00003        ', N'E00005         ', N'1', N'L00003         ', CAST(0x0000A2AF00BABEA4 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000006', N'CC002          ', N'Arpico Cushion chair', 7500, 2500, 7206.58, N'C00001         ', N'SC00004        ', N'E00025         ', N'1', N'L00002         ', CAST(0x0000A45700B4C300 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000007', N'CC002          ', N'damro chair model 21', 25000, 7000, 25000, N'C00001         ', N'SC00004        ', N'E00024         ', N'1', N'L00003         ', CAST(0x0000A58900BABEA4 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000008', N'CC002          ', N'damro sofa modelgx3', 25000, 10000, 25000, N'C00001         ', N'SC00008        ', N'E00026         ', N'1', N'L00003         ', CAST(0x0000A58900BABEA4 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000009', N'CC002          ', N'Arpico rack mod34', 6999, 3000, 2204.58, N'C00001         ', N'SC00009        ', N'E00025         ', N'1', N'L00007         ', CAST(0x0000A14300B4C300 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000010', N'CC002          ', N'DELL INSPIRE n534', 72500, 23000, 60620, N'C00002         ', N'SC00003        ', N'E00026         ', N'1', N'L00003         ', CAST(0x0000A2AF00B4C300 AS DateTime), N'E00005         ', N'E00004         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC003/A0000001', N'CC003          ', N'HP pro desk 600g1', 35600, 12000, 35600, N'C00002         ', N'SC00003        ', N'E00019         ', N'1', N'L00004         ', CAST(0x0000A58900B4C300 AS DateTime), N'E00007         ', N'E00008         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC003/A0000002', N'CC003          ', N'Espon pro cinema 1985', 132890, 12300, 132890, N'C00002         ', N'SC00010        ', N'E00015         ', N'1', N'L00005         ', CAST(0x0000A58900BABEA4 AS DateTime), N'E00007         ', N'E00008         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC003/A0000004', N'CC003          ', N'Qnap ts 219pII', 350000, 120000, 350000, N'C00002         ', N'SC00012        ', N'E00015         ', N'1', N'L00004         ', CAST(0x0000A58900B4C300 AS DateTime), N'E00007         ', N'E00008         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC003/A0000005', N'CC003          ', N'Pansonic gx95', 7800, 2000, 5480, N'C00002         ', N'SC00013        ', N'E00008         ', N'1', N'L00004         ', CAST(0x0000A2AF00BABEA4 AS DateTime), N'E00007         ', N'E00008         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC003/A0000006', N'CC003          ', N'HP p102 wireless scanner', 14700, 12500, 14700, N'C00002         ', N'SC00013        ', N'E00019         ', N'1', N'L00004         ', CAST(0x0000A58900B4C300 AS DateTime), N'E00007         ', N'E00008         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC004/A0000001', N'CC004          ', N'Toyota Dolphin lh113', 7245000, 230000, 4437078, N'C00003         ', N'SC00014        ', N'E00016         ', N'1', N'L00006         ', CAST(0x00009FD400BABEA4 AS DateTime), N'E00011         ', N'E00012         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC004/A0000002', N'CC004          ', N'Ashok leyland', 8905000, 2387508, 8905000, N'C00003         ', N'SC00015        ', N'E00012         ', N'1', N'L00006         ', CAST(0x0000A58900BABEA4 AS DateTime), N'E00011         ', N'E00012         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC004/A0000003', N'CC004          ', N'damro fan', 5500, 800, 4795, N'C00004         ', N'SC00018        ', N'E00016         ', N'1', N'L00006         ', CAST(0x0000A14200B4C300 AS DateTime), N'E00011         ', NULL)
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00001         ', N'Furniture')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00002         ', N' Computer Accessories')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00003         ', N'Vehicle')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00004         ', N'Electrical Equipment')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00005         ', N'Land and Building')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00006         ', N'Machinery')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC001          ', N'IT Department', N'E00002         ', N'E00003         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC002          ', N'Finance', N'E00005         ', N'E00004         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC003          ', N'HR', N'E00007         ', N'E00008         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC004          ', N'Legal', N'E00011         ', N'E00012         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC005          ', N'Marketing', N'E00009         ', N'E00010         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC006          ', N'Manufacture', N'E00013         ', N'E00014         ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00001         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00002         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00003         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00003         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00004         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00004         ', N'CC003          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00005         ', N'CC003          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00006         ', N'CC004          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00007         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00007         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00008         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00008         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00009         ', N'CC006          ')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00001         ', N'CC001          ', N'Vihanga', N'Liyanage', N'0758598063', N'vihangaliyanage007@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00002         ', N'CC001          ', N'Thisara', N'Salgado', N'0712233445', N'mtysalgado@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00003         ', N'CC001          ', N'Nipuna', N'Jayaweera', N'0712233445', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00004         ', N'CC002          ', N'Dineth', N'Madusara', N'0722323235', N'dineth454@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00005         ', N'CC002          ', N'Charee', N'Paranamana', N'0712233445', N'chareep@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00006         ', N'CC002          ', N'Sandra', N'Perera', N'0717504859', N'sandraperera1993@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00007         ', N'CC003          ', N'Hiranthi', N'Thennakoon', N'0710535513', N'hiranthithennakoon@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00008         ', N'CC003          ', N'Sulakshi', N'Chandrasiri', N'0727896781', N'sulakshichandrasiri@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00009         ', N'CC005          ', N'Hashini', N'Gunathilaka', N'0727896781', N'hashini17s1@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00010         ', N'CC005          ', N'Dulaji', N'Hidelarachchi', N'0715677894', N'dulajidinupama@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00011         ', N'CC004          ', N'Pasindu', N'Deeyagahage', N'0776256962', N'mthisarays@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00012         ', N'CC004          ', N'Kalinga', N'yapa', N'0710535513', N'sandraperera1993@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00013         ', N'CC006          ', N'Thusitha', N'Thiushan', N'0714089950', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00014         ', N'CC006          ', N'Kasun', N'Mathugama', N'0714089950', N'mthisarays@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00015         ', N'CC003          ', N'Darshana', N'Thennakoon', N'0716948153', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00016         ', N'CC004          ', N'Anuradhi', N'wickrama', N'0727896781', N'mthisarays@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00017         ', N'CC005          ', N'Nuren', N'Wijay', N'0715677894', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00018         ', N'CC006          ', N'Baagya', N'Tharushi', N'0710535513', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00019         ', N'CC003          ', N'Nadeesha', N'Gangoda', N'0710535513', N'sandraperera1993@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00020         ', N'CC001          ', N'Chulani', N'Weerathunga', N'0727896781', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00021         ', N'CC001          ', N'paramie', N'jayasinghe', N'0714089950', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00022         ', N'CC001          ', N'Nuwanthika', N'deshapriya', N'0710535513', N'sandraperera1993@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00023         ', N'CC001          ', N'Piumi', N'Radeeshani', N'0716948153', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00024         ', N'CC002          ', N'Hansika', N'Thilakarathna', N'0715677894', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00025         ', N'CC002          ', N'Geethika', N'Senarathna', N'0727896781', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00026         ', N'CC002          ', N'Maduwanthika', N'Somachandra', N'0710535513', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00001         ', N'Thelawala', N'No. 23, Thelawala Road.', N'0718899887')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00002         ', N'Kegalle', N'No. 32, Dewalagama', N'0717504859')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00003         ', N'Head Office', N'No. 54, Rathmalane', N'0717504859')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00004         ', N'NWSDB Panadura', N'No37/1, Galle rd, Panadura', N'0382246783')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00005         ', N'NWSDB Matara', N'No5, Matara', N'0418908901')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00006         ', N'NWSDB Galle', N'No37/1, Galle rd, Galle', N'0918908901')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00007         ', N'NWSDB Colombo 7', N'No 120, Col 7', N'0112890890')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00008         ', N'NWSDB Kegalle', N'No5, kegalle', N'0382246783')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00009         ', N'NWSDB Anuradhapura', N'No37/1, Galle rd, Anuradhapura', N'0318908901')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00008         ', N'AddNew', N'Approve', N'NWSDB/CC002/A0000002', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58900B82D5F AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00009         ', N'Delete', N'Recommend', N'NWSDB/CC002/A0000002', N'ksdnfjs sjfhasjhfj jfqwehjrkwekrjkjwkjrkwqefas jehfjsf', N'E00004         ', N'E00005         ', CAST(0x0000A58900B9609F AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00012         ', N'AddNew', N'Cancel', N'NWSDB/CC002/A0000003', N' ', N'E00005         ', N'E00006         ', CAST(0x0000A58900BA5F87 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00013         ', N'Transfer', N'Recommend', N'NWSDB/CC002/A0000001', N'0', N'E00004         ', N'E00005         ', CAST(0x0000A58900BB4409 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00014         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000005', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CC3DCA AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00015         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000006', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CC800D AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00016         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000007', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CCBDBC AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00017         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000008', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CD0006 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00018         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000009', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CE73BE AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00019         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000010', N' ', N'E00006         ', N'E00005         ', CAST(0x0000A58900CEB6C0 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00020         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000001', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D101F8 AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00021         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000002', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D14539 AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00022         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000003', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D181F1 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00023         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000004', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D1B883 AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00024         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000005', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D2073D AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00025         ', N'AddNew', N'Recommend', N'NWSDB/CC003/A0000006', N' ', N'E00008         ', N'E00007         ', CAST(0x0000A58900D239AF AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00026         ', N'AddNew', N'Recommend', N'NWSDB/CC004/A0000001', N' ', N'E00011         ', N'E00011         ', CAST(0x0000A58900D2B36C AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00027         ', N'AddNew', N'Recommend', N'NWSDB/CC004/A0000002', N' ', N'E00011         ', N'E00011         ', CAST(0x0000A58900D2EFFF AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00028         ', N'AddNew', N'Recommend', N'NWSDB/CC004/A0000003', N' ', N'E00011         ', N'E00011         ', CAST(0x0000A58900D332B1 AS DateTime), N'not-seen')
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00001        ', N'C00001         ', N'Tables', 20, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00002        ', N'C00002         ', N'Laptops', 10, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00003        ', N'C00002         ', N'Desktop Computers', 12, 4)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00004        ', N'C00001         ', N'Chair', 7, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00005        ', N'C00001         ', N'Cupboard', 12, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00006        ', N'C00001         ', N'Couche', 12, 8)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00007        ', N'C00001         ', N'Computer Table', 54, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00008        ', N'C00001         ', N'Sofa', 46, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00009        ', N'C00001         ', N'File Rack', 40, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00010        ', N'C00002         ', N'Projector', 68, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00011        ', N'C00002         ', N'Printer', 10, 10)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00012        ', N'C00002         ', N'Server', 5, 10)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00013        ', N'C00002         ', N'Scanner', 20, 10)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00014        ', N'C00003         ', N'Van', 10, 10)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00015        ', N'C00003         ', N'water bouser', 10, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00016        ', N'C00004         ', N'Cofee Machine', 5, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00017        ', N'C00002         ', N'Fax machine', 10, 10)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00018        ', N'C00004         ', N'Fan', 5, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00019        ', N'C00005         ', N'water plant', 1, 100)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00020        ', N'C00005         ', N'Office', 2, 50)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00021        ', N'C00005         ', N'Land', 1, 100)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00022        ', N'C00006         ', N'PM machine', 4, 25)
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00001         ', N'CC001          ', N'vihanga', N'FC5C0C549DF84268B7DEDD4BD5535B89DF39271D', N'admin')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00002         ', N'CC001          ', N'thisara', N'04CA103AF9F8DCB9E7B9697D2C07E3CC91B30AC7', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00003         ', N'CC001          ', N'sanju', N'47F10DB6BD41F394D2514249987D23DE96A09998', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00004         ', N'CC002          ', N'dineth', N'661DE767AD0182B809C501DD199EA4E210D79E60', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00005         ', N'CC002          ', N'charee', N'4DF7032F53875FA8A59E555BDF5BF2B22A9C0517', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00006         ', N'CC002          ', N'sandra', N'CAD1524360E58851CD0AE1E82B75FF5283474667', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00007         ', N'CC003          ', N'hiranthi', N'623AEB8C53774C62653F34E6528586A5FAAA93C5', N'generateReportUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00008         ', N'CC003          ', N'Sula', N'49FFF476C7868C87CDE693534EAEF379D717D1B0', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00009         ', N'CC005          ', N'hashini', N'400063E0CBE74C62EEB43146E185F374CB28E1FC', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00010         ', N'CC005          ', N'dula', N'529B6F982EBE8252FC0EDB5D33062458826DF4E4', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00011         ', N'CC004          ', N'somba', N'011C0D90B5D4119EF965C6FFF851B3B469540851', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00012         ', N'CC004          ', N'kalinga', N'AE346F3050B9DD94932D5AF4FB4197918BFA375E', N'generateReportUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00013         ', N'CC006          ', N'thuyya', N'2FACA8FB271D35ED1B0882EDC9357976A1F38ABC', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00014         ', N'CC006          ', N'kasun', N'3DAA2C246564BAF6D1909404D0C52CA53C9E7917', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00019         ', N'CC003          ', N'nadeesha', N'AFD19745E0190480F2912765F60AD85D879CE4FE', N'manageAssetUser')
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00001        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'complete', CAST(0x0000A58900B52B5C AS DateTime), N'L00002         ', N'E00004         ', N'E00005         ', N'E00004         ')
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00002        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'pendding', CAST(0x0000A58900BB4400 AS DateTime), N'L00002         ', N'E00005         ', N'E00005         ', NULL)
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[TransferAsset] ADD  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[UpgradeAsset] ADD  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_costCenter_costID] FOREIGN KEY([costID])
REFERENCES [dbo].[CostCenter] ([costID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_costCenter_costID]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Employee] FOREIGN KEY([owner])
REFERENCES [dbo].[Employee] ([empID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Employee]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_SubCategory] FOREIGN KEY([subcategory])
REFERENCES [dbo].[SubCategory] ([scatID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_SubCategory]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_SystemUser_approve] FOREIGN KEY([approve])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_SystemUser_approve]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_SystemUser_recommend] FOREIGN KEY([recommend])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_SystemUser_recommend]
GO
ALTER TABLE [dbo].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_costID_SystemUser_approve] FOREIGN KEY([approvePerson])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[CostCenter] CHECK CONSTRAINT [FK_costID_SystemUser_approve]
GO
ALTER TABLE [dbo].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_costID_SystemUser_recommend] FOREIGN KEY([recommendPerson])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[CostCenter] CHECK CONSTRAINT [FK_costID_SystemUser_recommend]
GO
ALTER TABLE [dbo].[CostLocation]  WITH CHECK ADD  CONSTRAINT [FK_costID_costCenter] FOREIGN KEY([costID])
REFERENCES [dbo].[CostCenter] ([costID])
GO
ALTER TABLE [dbo].[CostLocation] CHECK CONSTRAINT [FK_costID_costCenter]
GO
ALTER TABLE [dbo].[CostLocation]  WITH CHECK ADD  CONSTRAINT [FK_locID_location] FOREIGN KEY([locID])
REFERENCES [dbo].[Location] ([locID])
GO
ALTER TABLE [dbo].[CostLocation] CHECK CONSTRAINT [FK_locID_location]
GO
ALTER TABLE [dbo].[DisposeAsset]  WITH CHECK ADD  CONSTRAINT [FK_DisposeAsset_SystemUser_approve] FOREIGN KEY([approve])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[DisposeAsset] CHECK CONSTRAINT [FK_DisposeAsset_SystemUser_approve]
GO
ALTER TABLE [dbo].[DisposeAsset]  WITH CHECK ADD  CONSTRAINT [FK_DisposeAsset_SystemUser_recommend] FOREIGN KEY([recommend])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[DisposeAsset] CHECK CONSTRAINT [FK_DisposeAsset_SystemUser_recommend]
GO
ALTER TABLE [dbo].[DisposeAsset]  WITH CHECK ADD  CONSTRAINT [has_dispose] FOREIGN KEY([assetID])
REFERENCES [dbo].[Asset] ([assetID])
GO
ALTER TABLE [dbo].[DisposeAsset] CHECK CONSTRAINT [has_dispose]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_costID_Employee] FOREIGN KEY([costID])
REFERENCES [dbo].[CostCenter] ([costID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_costID_Employee]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_SystemUser_receive] FOREIGN KEY([receiveUser])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_SystemUser_receive]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_SystemUser_send] FOREIGN KEY([sendUser])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_SystemUser_send]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([catID])
REFERENCES [dbo].[Category] ([catID])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
ALTER TABLE [dbo].[SystemUser]  WITH CHECK ADD  CONSTRAINT [FK_SystemUser_Employee] FOREIGN KEY([empID])
REFERENCES [dbo].[Employee] ([empID])
GO
ALTER TABLE [dbo].[SystemUser] CHECK CONSTRAINT [FK_SystemUser_Employee]
GO
ALTER TABLE [dbo].[TransferAsset]  WITH CHECK ADD  CONSTRAINT [FK_TransferAsset_Employee] FOREIGN KEY([owner])
REFERENCES [dbo].[Employee] ([empID])
GO
ALTER TABLE [dbo].[TransferAsset] CHECK CONSTRAINT [FK_TransferAsset_Employee]
GO
ALTER TABLE [dbo].[TransferAsset]  WITH CHECK ADD  CONSTRAINT [FK_TransferAsset_SystemUser_approve] FOREIGN KEY([approve])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[TransferAsset] CHECK CONSTRAINT [FK_TransferAsset_SystemUser_approve]
GO
ALTER TABLE [dbo].[TransferAsset]  WITH CHECK ADD  CONSTRAINT [FK_TransferAsset_SystemUser_recommend] FOREIGN KEY([recommend])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[TransferAsset] CHECK CONSTRAINT [FK_TransferAsset_SystemUser_recommend]
GO
ALTER TABLE [dbo].[TransferAsset]  WITH CHECK ADD  CONSTRAINT [has_transfer] FOREIGN KEY([assetID])
REFERENCES [dbo].[Asset] ([assetID])
GO
ALTER TABLE [dbo].[TransferAsset] CHECK CONSTRAINT [has_transfer]
GO
ALTER TABLE [dbo].[TransferAsset]  WITH CHECK ADD  CONSTRAINT [trans_loc] FOREIGN KEY([location])
REFERENCES [dbo].[Location] ([locID])
GO
ALTER TABLE [dbo].[TransferAsset] CHECK CONSTRAINT [trans_loc]
GO
ALTER TABLE [dbo].[UpgradeAsset]  WITH CHECK ADD  CONSTRAINT [FK_UpgradeAsset_SystemUser_approve] FOREIGN KEY([approve])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[UpgradeAsset] CHECK CONSTRAINT [FK_UpgradeAsset_SystemUser_approve]
GO
ALTER TABLE [dbo].[UpgradeAsset]  WITH CHECK ADD  CONSTRAINT [FK_UpgradeAsset_SystemUser_recommend] FOREIGN KEY([recommend])
REFERENCES [dbo].[SystemUser] ([empID])
GO
ALTER TABLE [dbo].[UpgradeAsset] CHECK CONSTRAINT [FK_UpgradeAsset_SystemUser_recommend]
GO
ALTER TABLE [dbo].[UpgradeAsset]  WITH CHECK ADD  CONSTRAINT [has_upgrade] FOREIGN KEY([assetID])
REFERENCES [dbo].[Asset] ([assetID])
GO
ALTER TABLE [dbo].[UpgradeAsset] CHECK CONSTRAINT [has_upgrade]
GO
USE [master]
GO
ALTER DATABASE [FixAMz] SET  READ_WRITE 
GO
