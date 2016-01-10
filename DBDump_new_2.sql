USE [master]
GO
/****** Object:  Database [FixAMz]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[Asset]    Script Date: 1/10/2016 2:37:55 PM ******/
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
	[approvedDate] [date] NULL,
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
/****** Object:  Table [dbo].[Category]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[CostCenter]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[CostLocation]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[DisposeAsset]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[Location]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[SubCategory]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[SystemUser]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[TransferAsset]    Script Date: 1/10/2016 2:37:55 PM ******/
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
/****** Object:  Table [dbo].[UpgradeAsset]    Script Date: 1/10/2016 2:37:55 PM ******/
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
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000001', N'CC002          ', N'Damro Office Table', 18000, 6500, 17987.4, N'C00001         ', N'SC00001        ', N'E00006         ', N'1', N'L00004         ', CAST(0xE13A0B00 AS Date), N'E00004         ', N'E00005         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000002', N'CC002          ', N'Arpico Cushion Chair', 7500, 2500, 7498.08, N'C00001         ', N'SC00004        ', N'E00005         ', N'1', N'L00005         ', CAST(0xE13A0B00 AS Date), N'E00004         ', N'E00005         ')
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000003', N'CC002          ', N'Damro Model 21', 25000, 7000, 25000, N'C00001         ', N'SC00005        ', N'E00011         ', N'0', N'L00006         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000004', N'CC002          ', N'Arpico Model 78', 20000, 7500, 20000, N'C00001         ', N'SC00007        ', N'E00019         ', N'0', N'L00008         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000005', N'CC002          ', N'Damro Computer Table Model 2x', 8500, 4200, 8500, N'C00001         ', N'SC00008        ', N'E00020         ', N'0', N'L00009         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000006', N'CC002          ', N'Damro Sofa Model 3XG', 25000, 10000, 25000, N'C00001         ', N'SC00009        ', N'E00005         ', N'0', N'L00007         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000007', N'CC002          ', N'Arpico Rack Model 4F30', 6999, 2400, 6999, N'C00001         ', N'SC00010        ', N'E00004         ', N'0', N'L00004         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000008', N'CC002          ', N'Dell Inspiron N3543', 72500, 49000, 72500, N'C00002         ', N'SC00002        ', N'E00020         ', N'0', N'L00005         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000009', N'CC002          ', N'HP Pro Desk 600 G1', 18900, 14700, 18900, N'C00002         ', N'SC00003        ', N'E00006         ', N'0', N'L00006         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000010', N'CC002          ', N'Epson Pro Cinema 1985', 48000, 29000, 48000, N'C00002         ', N'SC00011        ', N'E00011         ', N'0', N'L00008         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000011', N'CC002          ', N'LX 350 Impact Printer', 23000, 19000, 23000, N'C00002         ', N'SC00012        ', N'E00004         ', N'0', N'L00002         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000012', N'CC002          ', N'QNAP TS 219P II', 128000, 98000, 128000, N'C00002         ', N'SC00013        ', N'E00006         ', N'0', N'L00005         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000013', N'CC002          ', N'Panasonic GX95 ', 7800, 4000, 7800, N'C00002         ', N'SC00014        ', N'E00004         ', N'0', N'L00006         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000014', N'CC002          ', N'ARRIS SURFboard SB6141 DOCSIS 3 Cable Modem', 10000, 8000, 10000, N'C00002         ', N'SC00015        ', N'E00019         ', N'0', N'L00004         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000015', N'CC002          ', N'Logitech  HD Webcam C270  Black', 5300, 2100, 5300, N'C00002         ', N'SC00016        ', N'E00019         ', N'0', N'L00006         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000016', N'CC002          ', N'HP P1102 Wireless Scanner ', 14700, 12500, 14700, N'C00002         ', N'SC00017        ', N'E00011         ', N'0', N'L00008         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000017', N'CC002          ', N'Mediabridge Cat5e Ethernet Patch Cable', 5000, 3500, 5000, N'C00002         ', N'SC00018        ', N'E00005         ', N'0', N'L00008         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000018', N'CC002          ', N'Toyota Dolphin LH 113', 420000, 390000, 420000, N'C00003         ', N'SC00021        ', N'E00019         ', N'0', N'L00007         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000019', N'CC002          ', N'Ashok Leyland', 20000, 18000, 20000, N'C00003         ', N'SC00022        ', N'E00019         ', N'0', N'L00005         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Asset] ([assetID], [costID], [name], [value], [salvageValue], [updatedValue], [category], [subcategory], [owner], [status], [location], [approvedDate], [recommend], [approve]) VALUES (N'NWSDB/CC002/A0000020', N'CC002          ', N'wooden table', 5000, 4000, 5000, N'C00001         ', N'SC00004        ', N'E00006         ', N'0', N'L00002         ', NULL, N'E00004         ', NULL)
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'               ', N'')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00001         ', N'Furniture')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00002         ', N' Computer Accessories')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00003         ', N'Vehicle')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00004         ', N'Electrical equipment')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00005         ', N'Land and Building')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00006         ', N'Machinery')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00007         ', N'Buildings')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00008         ', N'machinery')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC001          ', N'IT Department', N'E00002         ', N'E00003         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC002          ', N'Finance', N'E00004         ', N'E00005         ')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC003          ', N'HR Department', N'E00003         ', N'E00004         ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00001         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00002         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00003         ', N'CC001          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00004         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00005         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00006         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00007         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00008         ', N'CC002          ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00009         ', N'CC002          ')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00001         ', N'CC001          ', N'Vihanga', N'Liyanage', N'0758598063', N'vihangaliyanage007@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00002         ', N'CC001          ', N'Thisara', N'Salgado', N'0712233445', N'mtysalgado@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00003         ', N'CC001          ', N'Nipuna', N'Jayaweera', N'0712233445', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00004         ', N'CC002          ', N'Dineth', N'Madusara', N'0722323235', N'dineth454@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00005         ', N'CC001          ', N'Charee', N'Paranamana', N'0712233453', N'chareepgmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00006         ', N'CC002          ', N'Tom', N'Cruize', N'0717504859', N'tom@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00007         ', N'CC001          ', N'Hiranthi', N'Tennakoon', N'0778546235', N'hiranthithennakoon@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00009         ', N'CC001          ', N'Sulakshi', N'Chandrasiri', N'0712569885', N'sulakshichandrasiri@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00010         ', N'CC001          ', N'Hashini', N'Gunathilake', N'0758569985', N'hashini17s1@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00011         ', N'CC002          ', N'Dulaji', N'Hidellaarachchi', N'0715688556', N'dulajidinupama@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00012         ', N'CC001          ', N'Pasindu', N'Deeyagahage', N'0768859845', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00013         ', N'CC001          ', N'Kalinga', N'Yapa', N'0765562145', N'cparanamana93@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00014         ', N'CC001          ', N'Thusitha', N'Thiyushan', N'0789652354', N'dineth654@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00015         ', N'CC001          ', N'Kasun', N'Mathugama', N'0712566352', N'hiranthitrk@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00016         ', N'CC001          ', N'Darshana', N'Tennakoon', N'0778554412', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00017         ', N'CC001          ', N'Anuradhi', N'Wickramasinghe', N'0765566441', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00018         ', N'CC001          ', N'Nuren', N'Wijewickrama', N'0778844652', N'dineth454@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00019         ', N'CC002          ', N'Bhagya', N'Tharushi', N'0725463254', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00020         ', N'CC002          ', N'Kumari', N'Chandrasiri', N'0785588456', N'princesslogic@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00022         ', N'CC003          ', N'Chulani', N'Weerathunga', N'0752145875', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00023         ', N'CC001          ', N'Nadeesha', N'Gangodawila', N'0412548754', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00024         ', N'CC001          ', N'heshani', N'jayasinghe', N'0715425565', N'lilanticlockwise@gmail.com')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00001         ', N'Thelawala', N'No. 23, Thelawala Road.', N'0718899887')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00002         ', N'Kegalle', N'No. 32, Dewalagama', N'0717504859')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00003         ', N'Head Office', N'No. 54, Rathmalane', N'0717504859')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00004         ', N'NWSDB Raddolugama', N'No. 6/41, Raddolugama housing complex, Raddolugama', N'0335648752')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00005         ', N'NWSDB Kandy', N'kandy road, lewalla', N'0814566235')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00006         ', N'NWSDB Galle', N'dangedara junction, Galle', N'0915644213')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00007         ', N'NWSDB Matara', N'No. 6/81, Hakmana Rd, Matara', N'0412254125')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00008         ', N'NWSDB Nugegoda', N'No. 34/183, Thalapathpitiya Rd, Nugegoda', N'0112569874')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00009         ', N'NWSDB Colombo 07', N'No. 07, Reid Avenue, Colombo 07', N'0112541145')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00010         ', N'NWSDB  Nuwara eliya', N'No.18.4, maskeliya, maskeliya ', N'0254568954')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00011         ', N'NWSDB Kegalle', N'No. 6, Kegalle', N'0458745632')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00001         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000003', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F27CCC AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00002         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000004', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F31039 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00003         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000005', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F4678B AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00004         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000006', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F4CBD9 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00005         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000007', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F52441 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00006         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000008', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F6D5A2 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00007         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000009', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F7FDF1 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00008         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000010', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F8831B AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00009         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000011', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600F986F3 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00010         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000012', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600FAEDC2 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00011         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000013', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600FDA340 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00012         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000014', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600FF0AF7 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00013         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000015', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58600FFE265 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00014         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000016', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A5860100E920 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00016         ', N'AddNew', N'Recommend', N'NWSDB/CC002/A0000018', N' ', N'E00005         ', N'E00004         ', CAST(0x0000A58601037191 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00020         ', N'Transfer', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800A68A56 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00022         ', N'Transfer', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800A7E21B AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00024         ', N'Transfer', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800A8160D AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00026         ', N'Transfer', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800AD710F AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00028         ', N'Update', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800AF2A3E AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00030         ', N'Update', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800AF6681 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00032         ', N'Update', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800AFB656 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00034         ', N'Update', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800AFFBF3 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00036         ', N'Update', N'Approve', N'NWSDB/CC002/A0000001', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800B4EE6B AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00038         ', N'Delete', N'Approve', N'NWSDB/CC002/A0000001', N'', N'E00004         ', N'E00005         ', CAST(0x0000A58800B5C138 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00040         ', N'Delete', N'Approve', N'NWSDB/CC002/A0000001', N'', N'E00004         ', N'E00005         ', CAST(0x0000A58800B6572B AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00042         ', N'AddNew', N'Cancel', N'NWSDB/CC002/A0000020', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800BF0EA1 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00043         ', N'Delete', N'Cancel', N'NWSDB/CC002/A0000001', N'', N'E00004         ', N'E00004         ', CAST(0x0000A58800C1C2F9 AS DateTime), N'seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00044         ', N'AddNew', N'Approve', N'NWSDB/CC002/A0000019', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800C1DAF4 AS DateTime), N'not-seen')
INSERT [dbo].[Notification] ([notID], [type], [action], [assetID], [notContent], [sendUser], [receiveUser], [date], [status]) VALUES (N'N00045         ', N'AddNew', N'Approve', N'NWSDB/CC002/A0000017', N' ', N'E00004         ', N'E00005         ', CAST(0x0000A58800C4E25B AS DateTime), N'seen')
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00001        ', N'C00001         ', N'Tables', 20, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00002        ', N'C00002         ', N'Laptops', 10, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00003        ', N'C00002         ', N'Desktop Computers', 12, 4)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00004        ', N'C00001         ', N'Chair', 7, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00005        ', N'C00001         ', N'Cupboard', 12, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00006        ', N'C00001         ', N'Table', 15, 12)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00007        ', N'C00001         ', N'couche', 12, 8)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00008        ', N'C00001         ', N'Computer table', 54, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00009        ', N'C00001         ', N'Sofa', 46, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00010        ', N'C00001         ', N'File rack', 40, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00011        ', N'C00002         ', N'Projector', 68, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00012        ', N'C00002         ', N'Printer', 36, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00013        ', N'C00002         ', N'Server', 5, 60)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00014        ', N'C00002         ', N'speaker', 30, 12)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00015        ', N'C00002         ', N'Modem', 20, 25)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00016        ', N'C00002         ', N'Web Camera', 48, 12)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00017        ', N'C00002         ', N'Scanner', 32, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00018        ', N'C00002         ', N'Networking cables', 14, 50)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00021        ', N'C00003         ', N'Van', 42, 12)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00022        ', N'C00003         ', N'Bowser', 18, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00023        ', N'C00003         ', N'Car', 18, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00024        ', N'C00003         ', N'Cab', 16, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00025        ', N'C00003         ', N'Truck', 13, 22)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00026        ', N'C00004         ', N'Fax Machine', 59, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00027        ', N'C00004         ', N'Coffee Machine', 36, 15)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00028        ', N'C00004         ', N'Fan', 48, 20)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00029        ', N'C00004         ', N'Air Conditioner', 24, 18)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00030        ', N'C00004         ', N'CCTV Cameras', 23, 12)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00031        ', N'C00005         ', N'Galle Rd Ahangama', 2, 80)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00032        ', N'C00005         ', N'Water Purification Tank', 6, 50)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00033        ', N'C00005         ', N'Water Tank', 7, 50)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00034        ', N'C00006         ', N'Purification monitoring machine', 21, 30)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00035        ', N'C00001         ', N'a', 4, 4)
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00001         ', N'CC001          ', N'vihanga', N'FC5C0C549DF84268B7DEDD4BD5535B89DF39271D', N'admin')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00002         ', N'CC001          ', N'thisara', N'04CA103AF9F8DCB9E7B9697D2C07E3CC91B30AC7', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00003         ', N'CC001          ', N'sanju', N'47F10DB6BD41F394D2514249987D23DE96A09998', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00004         ', N'CC002          ', N'dineth', N'661DE767AD0182B809C501DD199EA4E210D79E60', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00005         ', N'CC002          ', N'charee', N'4DF7032F53875FA8A59E555BDF5BF2B22A9C0517', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00006         ', N'CC002          ', N'tom', N'96835DD8BFA718BD6447CCC87AF89AE1675DAECA', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00009         ', N'CC001          ', N'Sulakshi93', N'40BD001563085FC35165329EA1FF5C5ECBDBBEEF', N'admin')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00010         ', N'CC001          ', N'Hashini93', N'DCFE5404CEA0021C738DC4246B7F099C091A8A09', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00011         ', N'CC002          ', N'Dulaji93', N'A7AD9B664420551FCFAFA15A339FCC42ACE977C1', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00012         ', N'CC001          ', N'Pasindu92', N'DF989E4678A1FC14CCE8F2B9A4CAF793C6226A6F', N'generateReportUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00013         ', N'CC001          ', N'Kalinga92', N'AE346F3050B9DD94932D5AF4FB4197918BFA375E', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00014         ', N'CC001          ', N'Thusitha', N'F4ED510B08C98CD7027E7970E1F1933D8CD5658F', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00015         ', N'CC001          ', N'Kasun', N'3DAA2C246564BAF6D1909404D0C52CA53C9E7917', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00016         ', N'CC001          ', N'Darshana', N'54AA2FEA0656D42F88D357AECE1D165E4D78B50D', N'generateReportUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00017         ', N'CC001          ', N'Anuradhi', N'E067B0486639AFB997C04C24DE3CB95BB74F6B23', N'admin')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00018         ', N'CC001          ', N'Nuren', N'1DA74DF5879C2DA30BE18E99266D308FDF261310', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00020         ', N'CC002          ', N'logic', N'775BB961B81DA1CA49217A48E533C832C337154A', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00022         ', N'CC003          ', N'Chulani92', N'E3085C3852A1AE6EC586432CFB6F6A1321E3499E', N'manageReport')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00023         ', N'CC001          ', N'nadeesha', N'AFD19745E0190480F2912765F60AD85D879CE4FE', N'generateReportUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00024         ', N'CC001          ', N'heshani', N'82F7194A06AC6D1CB3068CAEA2F8F6FB5F265D02', N'manageReport')
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00001        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'pendding', CAST(0x0000A58800A4DAF8 AS DateTime), N'L00007         ', N'E00006         ', N'E00004         ', NULL)
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00002        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'pendding', CAST(0x0000A58800A75A12 AS DateTime), N'L00004         ', N'E00011         ', N'E00004         ', NULL)
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00003        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'pendding', CAST(0x0000A58800A80686 AS DateTime), N'L00004         ', N'E00006         ', N'E00004         ', NULL)
INSERT [dbo].[TransferAsset] ([transID], [assetID], [costID], [type], [status], [date], [location], [owner], [recommend], [approve]) VALUES (N'TA00004        ', N'NWSDB/CC002/A0000001', N'CC002               ', N'0', N'pendding', CAST(0x0000A58800AA0853 AS DateTime), N'L00004         ', N'E00006         ', N'E00004         ', NULL)
INSERT [dbo].[UpgradeAsset] ([upID], [assetID], [value], [updatedValue], [date], [description], [recommend], [approve], [status]) VALUES (N'U00001         ', N'NWSDB/CC002/A0000001', 18000, 100, CAST(0x0000A58800AE57B9 AS DateTime), N'paint the table', N'E00004         ', NULL, N'cancel')
INSERT [dbo].[UpgradeAsset] ([upID], [assetID], [value], [updatedValue], [date], [description], [recommend], [approve], [status]) VALUES (N'U00002         ', N'NWSDB/CC002/A0000001', 18000, 450, CAST(0x0000A58800AFDF82 AS DateTime), N'painting', N'E00004         ', NULL, N'cancel')
INSERT [dbo].[UpgradeAsset] ([upID], [assetID], [value], [updatedValue], [date], [description], [recommend], [approve], [status]) VALUES (N'U00003         ', N'NWSDB/CC002/A0000001', 18000, 100, CAST(0x0000A58800B27A6E AS DateTime), N'4kl', N'E00004         ', NULL, N'cancel')
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
