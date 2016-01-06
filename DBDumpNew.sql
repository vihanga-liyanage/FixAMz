USE [master]
GO
DROP DATABASE [FixAMz]
/****** Object:  Database [FixAMz]    Script Date: 1/4/2016 12:04:33 PM ******/
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
/****** Object:  Table [dbo].[Asset]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[CostCenter]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[CostLocation]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[DisposeAsset]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[Location]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[SubCategory]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[SystemUser]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[TransferAsset]    Script Date: 1/4/2016 12:04:34 PM ******/
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
/****** Object:  Table [dbo].[UpgradeAsset]    Script Date: 1/4/2016 12:04:34 PM ******/
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
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00001         ', N'CC001        ', N'Vihanga', N'Liyanage', N'0758598063', N'vihangaliyanage007@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00002         ', N'CC001        ', N'Thisara', N'Salgado', N'0712233445', N'mtysalgado@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00003         ', N'CC001        ', N'Nipuna', N'Jayaweera', N'0712233445', N'nipuna.player@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00004         ', N'CC002        ', N'Dineth', N'Madusara', N'0722323235', N'dineth454@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00005         ', N'CC002        ', N'Charee', N'Paranamana', N'0712233445', N'chareep@gmail.com')
INSERT [dbo].[Employee] ([empID], [costID], [firstName], [lastName], [contactNo], [email]) VALUES (N'E00006         ', N'CC002        ', N'Tom', N'Cruize', N'0717504859', N'tom@gmail.com')
GO
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00001         ', N'CC001        ', N'vihanga', N'FC5C0C549DF84268B7DEDD4BD5535B89DF39271D', N'admin')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00002         ', N'CC001        ', N'thisara', N'04CA103AF9F8DCB9E7B9697D2C07E3CC91B30AC7', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00003         ', N'CC001        ', N'sanju', N'47F10DB6BD41F394D2514249987D23DE96A09998', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00004         ', N'CC002        ', N'dineth', N'661DE767AD0182B809C501DD199EA4E210D79E60', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00005         ', N'CC002        ', N'charee', N'4DF7032F53875FA8A59E555BDF5BF2B22A9C0517', N'manageAssetUser')
INSERT [dbo].[SystemUser] ([empID], [costID], [username], [password], [type]) VALUES (N'E00006         ', N'CC002        ', N'tom', N'96835DD8BFA718BD6447CCC87AF89AE1675DAECA', N'manageAssetUser')
GO
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00001', N'Furniture')
INSERT [dbo].[Category] ([catID], [name]) VALUES (N'C00002', N' Computer Accessories')
GO
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00001        ', N'C00001         ', N'Tables', 20, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00002        ', N'C00002         ', N'Laptops', 10, 5)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00003        ', N'C00002         ', N'Desktop Computers', 12, 4)
INSERT [dbo].[SubCategory] ([scatID], [catID], [name], [depreciationRate], [lifetime]) VALUES (N'SC00004        ', N'C00001         ', N'Chair', 7, 5)
GO
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC001', N'IT Department', N'E00002', N'E00003')
INSERT [dbo].[CostCenter] ([costID], [name], [recommendPerson], [approvePerson]) VALUES (N'CC002', N'Finance', N'E00004', N'E00005')
GO
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00001         ', N'Thelawala', N'No. 23, Thelawala Road.', N'0718899887')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00002         ', N'Kegalle', N'No. 32, Dewalagama', N'0717504859')
INSERT [dbo].[Location] ([locID], [name], [address], [contactNo]) VALUES (N'L00003         ', N'Head Office', N'No. 54, Rathmalane', N'0717504859')
GO
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00001         ', N'CC001        ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00002         ', N'CC002        ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00003         ', N'CC001        ')
INSERT [dbo].[CostLocation] ([locID], [costID]) VALUES (N'L00003         ', N'CC002        ')
GO
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