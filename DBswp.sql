USE [master]
GO
/****** Object:  Database [DentalSystemDB]    Script Date: 3/22/2023 5:01:16 PM ******/
CREATE DATABASE [DentalSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DentalSystemDB', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\DentalSystemDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DentalSystemDB_log', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\DentalSystemDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DentalSystemDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DentalSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DentalSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DentalSystemDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DentalSystemDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DentalSystemDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DentalSystemDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DentalSystemDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DentalSystemDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DentalSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DentalSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DentalSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DentalSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DentalSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DentalSystemDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DentalSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DentalSystemDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DentalSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DentalSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DentalSystemDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DentalSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DentalSystemDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DentalSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DentalSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DentalSystemDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DentalSystemDB] SET  MULTI_USER 
GO
ALTER DATABASE [DentalSystemDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DentalSystemDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DentalSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DentalSystemDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DentalSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DentalSystemDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DentalSystemDB] SET QUERY_STORE = OFF
GO
USE [DentalSystemDB]
GO
/****** Object:  Table [dbo].[material_export]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[material_export](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[material_id] [bigint] NOT NULL,
	[amount] [int] NOT NULL,
	[total_price] [int] NOT NULL,
	[patient_record_id] [bigint] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_material_export] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[material_import]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[material_import](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[material_id] [bigint] NOT NULL,
	[date] [datetime] NOT NULL,
	[amount] [int] NOT NULL,
	[supply_name] [varchar](max) NOT NULL,
	[total_price] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_material_import] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materials]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materials](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](max) NOT NULL,
	[unit] [varchar](max) NOT NULL,
	[amount] [int] NOT NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_materials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[patient_record]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patient_record](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[reason] [nvarchar](max) NOT NULL,
	[diagnostic] [nvarchar](max) NOT NULL,
	[causal] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[treatment_name] [nvarchar](max) NOT NULL,
	[marrow_record] [nvarchar](max) NOT NULL,
	[debit] [nvarchar](max) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[treatment_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[patient_id] [bigint] NOT NULL,
	[prescription] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__patient___3213E83F5D17537E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[patient_record_service_map]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patient_record_service_map](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[patient_record_id] [bigint] NOT NULL,
	[service_id] [bigint] NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[patients]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patients](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[birthday] [datetime] NOT NULL,
	[gender] [bit] NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[body_prehistory] [nvarchar](max) NOT NULL,
	[teeth_prehistory] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK__patients__3213E83F28CB1AF0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permission]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permission](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_permission_map]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_permission_map](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id] [bigint] NOT NULL,
	[permission_id] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[schedule]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schedule](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[patient_id] [bigint] NOT NULL,
	[date] [datetime] NOT NULL,
	[status] [int] NOT NULL,
	[booked] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[services]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[services](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[unit] [nvarchar](max) NOT NULL,
	[market_price] [int] NOT NULL,
	[price] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[OwnerId] [bigint] NOT NULL,
 CONSTRAINT [PK__SystemLo__3214EC075467928E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[timekeeping]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[timekeeping](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[time_checkin] [datetime] NOT NULL,
	[time_checkout] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[treatment_service_map]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[treatment_service_map](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[treatment_id] [bigint] NOT NULL,
	[service_id] [bigint] NOT NULL,
	[current_price] [int] NOT NULL,
	[discount] [float] NOT NULL,
	[patient_record_id] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[treatments]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[treatments](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[patient_id] [bigint] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/22/2023 5:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[full_name] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[birthday] [datetime] NOT NULL,
	[phone] [varchar](max) NOT NULL,
	[salary] [int] NOT NULL,
	[role_id] [bigint] NOT NULL,
	[enable] [bit] NOT NULL,
	[email] [varchar](max) NOT NULL,
 CONSTRAINT [PK__users__3213E83FB32E9154] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[materials] ON 

INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (1, N'Chi nha khoa1', N'Soi', 100, 10000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (2, N'Nieng rang', N'Chiec', 10, 1000000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (4, N'Bua nha khoa', N'Chiec', 10, 100000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (5, N'Keo', N'Chiec', 10, 100000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (8, N'Rang gia', N'Chiec', 100, 500000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (11, N'Nieng rang', N'Bo', 10, 5000000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (17, N'Khan sach', N'Chiec', 100, 1000000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (18, N'Thuoc gay me', N'Mui tiem', 100, 10000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (19, N'Thuoc sau rang', N'Chai', 100, 50000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (20, N'Thuoc giam dau', N'Vi', 100, 10000)
INSERT [dbo].[materials] ([id], [name], [unit], [amount], [price]) VALUES (21, N'Dao phau thuat', N'Chiec', 100, 20000)
SET IDENTITY_INSERT [dbo].[materials] OFF
GO
SET IDENTITY_INSERT [dbo].[patients] ON 

INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (6, N'Nguyễn Văn Đức', CAST(N'2010-01-11T00:00:00.000' AS DateTime), 1, N'Ha Long', N'0368074353', N'minh3ehqz@gmail.com', N'bình thường', N'bình thường', 0, 0)
INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (7, N'Vũ Đức Minh', CAST(N'2002-04-04T00:00:00.000' AS DateTime), 1, N'Ha Long', N'0931232131', N'minhewhqz@gmail.com', N'suy dinh dưỡng', N'sâu răng', 0, 0)
INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (8, N'Phạm Tuyết Nga', CAST(N'2023-02-28T00:00:00.000' AS DateTime), 0, N'Hả Phòng', N'0932131241', N'minhvd43469@fpt.edu.vn', N'bình thường', N'bình thường', 0, 0)
INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (9, N'Trần Thế trung', CAST(N'2002-11-12T00:00:00.000' AS DateTime), 1, N'Đà Nẵng', N'0731231232', N'minưwhqz@gmail.com', N'béo phì', N'bình thường', 0, 0)
INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (10, N'Phạm Bảo Ngọc', CAST(N'2023-02-27T00:00:00.000' AS DateTime), 1, N'Hà Nội', N'0932131999', N'manh3ehqz@gmail.com', N'bình thường', N'răng mọc nghiêng', 0, 0)
INSERT [dbo].[patients] ([id], [name], [birthday], [gender], [address], [phone], [email], [body_prehistory], [teeth_prehistory], [status], [is_deleted]) VALUES (11, N'Nguyễn Đăng Tú', CAST(N'2001-09-04T00:00:00.000' AS DateTime), 1, N'Hà Nội', N'0932133781', N'tuehqz@gmail.com', N'bình thường', N'bình thường', 0, 0)
SET IDENTITY_INSERT [dbo].[patients] OFF
GO
SET IDENTITY_INSERT [dbo].[permission] ON 

INSERT [dbo].[permission] ([id], [name]) VALUES (1, N'/Patients')
INSERT [dbo].[permission] ([id], [name]) VALUES (2, N'/Patients/Details')
INSERT [dbo].[permission] ([id], [name]) VALUES (3, N'/Patients/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (4, N'/SystemLogs')
INSERT [dbo].[permission] ([id], [name]) VALUES (5, N'/SystemLogs/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (6, N'/Schedules')
INSERT [dbo].[permission] ([id], [name]) VALUES (7, N'/Schedules/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (8, N'/User')
INSERT [dbo].[permission] ([id], [name]) VALUES (9, N'/User/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (10, N'/User/Details')
INSERT [dbo].[permission] ([id], [name]) VALUES (11, N'/Service')
INSERT [dbo].[permission] ([id], [name]) VALUES (12, N'/Service/index')
INSERT [dbo].[permission] ([id], [name]) VALUES (13, N'/Service/details')
INSERT [dbo].[permission] ([id], [name]) VALUES (14, N'/RestPassword')
INSERT [dbo].[permission] ([id], [name]) VALUES (15, N'/RestPassword/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (16, N'/PatientsRecord')
INSERT [dbo].[permission] ([id], [name]) VALUES (17, N'/PatientsRecord/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (18, N'/Material')
INSERT [dbo].[permission] ([id], [name]) VALUES (19, N'/Material/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (20, N'/Material/Details')
INSERT [dbo].[permission] ([id], [name]) VALUES (21, N'/Material/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (22, N'/Login')
INSERT [dbo].[permission] ([id], [name]) VALUES (23, N'/Login/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (24, N'/ImportMaterial')
INSERT [dbo].[permission] ([id], [name]) VALUES (25, N'/ImportMaterial/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (26, N'/ImportMaterial/Detail')
INSERT [dbo].[permission] ([id], [name]) VALUES (27, N'/Home')
INSERT [dbo].[permission] ([id], [name]) VALUES (28, N'/Home/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (29, N'/ForgotPassword')
INSERT [dbo].[permission] ([id], [name]) VALUES (30, N'/ForgotPassword/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (31, N'/ExportMaterial')
INSERT [dbo].[permission] ([id], [name]) VALUES (32, N'/ExportMaterial/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (33, N'/ExportMaterial/Details')
INSERT [dbo].[permission] ([id], [name]) VALUES (34, N'/ExportMaterial/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (35, N'/ExportMaterial/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (36, N'/Patients/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (37, N'/Patients/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (38, N'/Patients/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (39, N'/User/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (40, N'/User/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (41, N'/Service/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (42, N'/Service/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (43, N'/Service/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (44, N'/PatientsRecord/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (45, N'/PatientsRecord/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (46, N'/PatientsRecord/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (47, N'/Material/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (48, N'/ImportMaterial/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (49, N'/ImportMaterial/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (50, N'/ImportMaterial/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (51, N'/Material/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (52, N'/ExportMaterial/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (53, N'/Home/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (54, N'/Home')
INSERT [dbo].[permission] ([id], [name]) VALUES (55, N'/Treatments/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (56, N'/Treatments/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (57, N'/Treatments/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (58, N'/User/ViewProfile')
INSERT [dbo].[permission] ([id], [name]) VALUES (59, N'/User/EditProfile')
INSERT [dbo].[permission] ([id], [name]) VALUES (60, N'/PatientsRecord/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (61, N'/PatientsRecord/Edit')
INSERT [dbo].[permission] ([id], [name]) VALUES (62, N'/PatientsRecord/Details')
INSERT [dbo].[permission] ([id], [name]) VALUES (63, N'/PatientsRecord/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (64, N'/PatientsRecord/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (65, N'/Treatments/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (66, N'/Schedules/Delete')
INSERT [dbo].[permission] ([id], [name]) VALUES (67, N'/Schedules/Update')
INSERT [dbo].[permission] ([id], [name]) VALUES (68, N'/Schedules/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (69, N'/Patients/Create')
INSERT [dbo].[permission] ([id], [name]) VALUES (70, N'/TimeKeeping/Index')
INSERT [dbo].[permission] ([id], [name]) VALUES (71, N'/TimeKeeping/KeepingManager')
SET IDENTITY_INSERT [dbo].[permission] OFF
GO
SET IDENTITY_INSERT [dbo].[role_permission_map] ON 

INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (1, 5, 2)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (2, 5, 1)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (3, 5, 3)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (4, 1, 1)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (5, 1, 2)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (6, 1, 3)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (7, 1, 4)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (8, 1, 5)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (9, 1, 6)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (10, 1, 7)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (11, 1, 8)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (12, 1, 9)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (13, 1, 10)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (14, 1, 11)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (15, 1, 12)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (16, 1, 13)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (17, 1, 14)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (18, 1, 15)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (19, 1, 16)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (20, 1, 17)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (21, 1, 18)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (22, 1, 19)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (23, 1, 20)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (24, 1, 21)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (25, 1, 22)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (26, 1, 23)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (27, 1, 24)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (28, 1, 25)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (29, 1, 26)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (30, 1, 27)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (31, 1, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (32, 1, 29)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (33, 1, 30)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (34, 1, 31)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (35, 1, 32)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (36, 1, 33)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (37, 1, 34)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (38, 6, 1)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (39, 6, 3)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (40, 4, 1)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (41, 4, 3)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (42, 4, 2)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (43, 6, 2)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (44, 1, 35)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (45, 2, 11)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (46, 2, 12)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (47, 6, 11)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (48, 6, 12)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (49, 2, 13)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (50, 7, 24)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (51, 7, 25)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (52, 7, 26)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (53, 4, 24)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (54, 4, 25)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (55, 4, 26)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (56, 2, 24)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (57, 2, 25)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (58, 2, 26)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (59, 7, 18)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (60, 7, 19)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (61, 7, 20)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (62, 7, 21)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (63, 4, 18)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (64, 4, 19)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (65, 2, 20)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (67, 2, 8)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (68, 2, 9)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (69, 2, 10)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (70, 3, 4)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (71, 3, 5)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (72, 5, 6)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (73, 5, 7)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (74, 6, 7)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (75, 6, 6)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (76, 2, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (77, 3, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (78, 4, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (79, 5, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (80, 6, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (81, 7, 28)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (82, 4, 36)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (83, 6, 36)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (84, 2, 39)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (85, 1, 34)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (86, 1, 38)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (87, 1, 36)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (88, 1, 37)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (89, 1, 39)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (90, 1, 40)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (91, 1, 41)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (92, 1, 42)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (93, 1, 43)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (94, 1, 44)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (95, 1, 45)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (96, 1, 46)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (97, 1, 47)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (98, 1, 48)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (99, 1, 49)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (100, 1, 50)
GO
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (101, 1, 51)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (102, 1, 52)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (103, 1, 53)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (104, 1, 54)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (105, 1, 55)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (106, 1, 56)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (107, 1, 57)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (108, 1, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (110, 1, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (111, 2, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (112, 2, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (113, 3, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (114, 3, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (115, 4, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (116, 4, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (117, 5, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (118, 5, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (119, 6, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (120, 6, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (121, 7, 58)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (122, 7, 59)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (123, 6, 37)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (124, 5, 55)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (125, 5, 56)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (126, 5, 57)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (127, 4, 53)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (128, 4, 54)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (129, 1, 60)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (130, 1, 61)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (131, 1, 62)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (132, 1, 63)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (133, 5, 60)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (134, 5, 61)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (135, 5, 62)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (136, 5, 63)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (137, 1, 64)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (138, 1, 65)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (139, 1, 66)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (140, 1, 67)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (141, 1, 68)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (142, 1, 69)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (143, 1, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (144, 1, 71)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (145, 2, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (146, 2, 71)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (147, 3, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (148, 3, 71)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (149, 4, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (150, 5, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (151, 6, 70)
INSERT [dbo].[role_permission_map] ([id], [role_id], [permission_id]) VALUES (152, 7, 70)
SET IDENTITY_INSERT [dbo].[role_permission_map] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([id], [name]) VALUES (1, N'Giám đốc')
INSERT [dbo].[roles] ([id], [name]) VALUES (2, N'Quản lí')
INSERT [dbo].[roles] ([id], [name]) VALUES (3, N'Quản trị viên hệ thống')
INSERT [dbo].[roles] ([id], [name]) VALUES (4, N'Y tá')
INSERT [dbo].[roles] ([id], [name]) VALUES (5, N'Bác sĩ')
INSERT [dbo].[roles] ([id], [name]) VALUES (6, N'Tiếp tân')
INSERT [dbo].[roles] ([id], [name]) VALUES (7, N'Kế toán')
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[schedule] ON 

INSERT [dbo].[schedule] ([id], [patient_id], [date], [status], [booked]) VALUES (1, 6, CAST(N'2023-03-23T09:36:00.000' AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[services] ON 

INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (1, N'Tẩy trắng răng', N'Cái ', 200000, 300000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (5, N'Trám răng', N'Cái', 100000, 200000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (6, N'Cạo vôi', N'Cái', 500000, 100000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (8, N'Chữa trị sâu răng', N'Cái', 60000, 90000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (9, N'Trồng răng', N'Cái', 300000, 400000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (10, N'Niềng răng', N'Lần', 5000000, 7000000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (11, N'Làm răng sứ', N'Cái', 2000000, 3000000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (12, N'Tư vấn về chăm sóc răng miệng', N'Lần', 200000, 250000)
INSERT [dbo].[services] ([id], [name], [unit], [market_price], [price]) VALUES (13, N'Phục hình răng', N'Cái', 200000, 400000)
SET IDENTITY_INSERT [dbo].[services] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemLog] ON 

INSERT [dbo].[SystemLog] ([Id], [CreatedDate], [Content], [OwnerId]) VALUES (1, CAST(N'2023-03-21T11:36:27.380' AS DateTime), N'người dùng đã đặt lịch hẹn lúc 3/23/2023 9:36:00 AM của bênh nhân Nguyễn Văn Đức số điện thoại là 0368074353', 21)
SET IDENTITY_INSERT [dbo].[SystemLog] OFF
GO
SET IDENTITY_INSERT [dbo].[timekeeping] ON 

INSERT [dbo].[timekeeping] ([id], [user_id], [time_checkin], [time_checkout]) VALUES (1, 21, CAST(N'2023-03-21T11:21:13.377' AS DateTime), CAST(N'1755-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[timekeeping] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (2, N'dieu', N'Nguyễn Thị Phương Diệu', N'dieu123', CAST(N'2023-02-10T00:00:00.000' AS DateTime), N'0986743832', 5000000, 1, 0, N'dieu@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (20, N'huy', N'Nguyễn Trường Huy', N'trghuy12345', CAST(N'2002-02-03T00:00:00.000' AS DateTime), N'0937284728', 4500000, 2, 0, N'huy@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (21, N'minh', N'Vũ Đức Minh', N'minh2345', CAST(N'2003-02-12T00:00:00.000' AS DateTime), N'0976845632', 4000000, 1, 0, N'minh@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (22, N'thang', N'Phạm Xuân Thắng', N'thang2345', CAST(N'2002-02-23T00:00:00.000' AS DateTime), N'0958432467', 400000, 1, 0, N'thang@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (23, N'huy', N'Nguyễn Thành Huy', N'huyth1234', CAST(N'2002-02-10T00:00:00.000' AS DateTime), N'0978594738', 4320000, 1, 0, N'thhuy@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (24, N'hoa', N'Nguyễn Xuân Hòa', N'hia23253', CAST(N'2006-03-12T00:00:00.000' AS DateTime), N'0965743521', 35600000, 1, 0, N'hoa@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (25, N'hung', N'Nguyễn Văn Hùng', N'hung1234', CAST(N'2005-02-28T00:00:00.000' AS DateTime), N'0967483721', 34500000, 1, 0, N'hung@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (26, N'linh', N'Bùi Hoàng Linh', N'linh12345', CAST(N'1995-06-23T00:00:00.000' AS DateTime), N'0957382659', 37000000, 4, 0, N'linh@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (27, N'trinh', N'Mai Kiều Trinh', N'trinh123', CAST(N'1999-02-03T00:00:00.000' AS DateTime), N'0965335567', 5000000, 5, 0, N'trinh@gmail.com')
INSERT [dbo].[users] ([id], [username], [full_name], [password], [birthday], [phone], [salary], [role_id], [enable], [email]) VALUES (28, N'nam', N'Vũ Hoài Nam', N'nam12345', CAST(N'1996-02-04T00:00:00.000' AS DateTime), N'0978493648', 60000000, 7, 0, N'nam@gmail.com')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[material_export]  WITH CHECK ADD  CONSTRAINT [FK_material_export_materials] FOREIGN KEY([material_id])
REFERENCES [dbo].[materials] ([id])
GO
ALTER TABLE [dbo].[material_export] CHECK CONSTRAINT [FK_material_export_materials]
GO
ALTER TABLE [dbo].[material_export]  WITH CHECK ADD  CONSTRAINT [FK_material_export_patient_record] FOREIGN KEY([patient_record_id])
REFERENCES [dbo].[patient_record] ([id])
GO
ALTER TABLE [dbo].[material_export] CHECK CONSTRAINT [FK_material_export_patient_record]
GO
ALTER TABLE [dbo].[material_import]  WITH CHECK ADD  CONSTRAINT [FK_material_import_materials] FOREIGN KEY([material_id])
REFERENCES [dbo].[materials] ([id])
GO
ALTER TABLE [dbo].[material_import] CHECK CONSTRAINT [FK_material_import_materials]
GO
ALTER TABLE [dbo].[patient_record]  WITH CHECK ADD  CONSTRAINT [FK_patient_record_patients] FOREIGN KEY([patient_id])
REFERENCES [dbo].[patients] ([id])
GO
ALTER TABLE [dbo].[patient_record] CHECK CONSTRAINT [FK_patient_record_patients]
GO
ALTER TABLE [dbo].[patient_record]  WITH CHECK ADD  CONSTRAINT [FK_patient_record_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[patient_record] CHECK CONSTRAINT [FK_patient_record_users]
GO
ALTER TABLE [dbo].[patient_record_service_map]  WITH CHECK ADD  CONSTRAINT [FK_patient_record_service_map_patient_record] FOREIGN KEY([patient_record_id])
REFERENCES [dbo].[patient_record] ([id])
GO
ALTER TABLE [dbo].[patient_record_service_map] CHECK CONSTRAINT [FK_patient_record_service_map_patient_record]
GO
ALTER TABLE [dbo].[patient_record_service_map]  WITH CHECK ADD  CONSTRAINT [FK_patient_record_service_map_services] FOREIGN KEY([service_id])
REFERENCES [dbo].[services] ([id])
GO
ALTER TABLE [dbo].[patient_record_service_map] CHECK CONSTRAINT [FK_patient_record_service_map_services]
GO
ALTER TABLE [dbo].[role_permission_map]  WITH CHECK ADD  CONSTRAINT [FK_role_permission_map_permission] FOREIGN KEY([permission_id])
REFERENCES [dbo].[permission] ([id])
GO
ALTER TABLE [dbo].[role_permission_map] CHECK CONSTRAINT [FK_role_permission_map_permission]
GO
ALTER TABLE [dbo].[role_permission_map]  WITH CHECK ADD  CONSTRAINT [FK_role_permission_map_roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[role_permission_map] CHECK CONSTRAINT [FK_role_permission_map_roles]
GO
ALTER TABLE [dbo].[schedule]  WITH CHECK ADD  CONSTRAINT [FK_schedule_patients] FOREIGN KEY([patient_id])
REFERENCES [dbo].[patients] ([id])
GO
ALTER TABLE [dbo].[schedule] CHECK CONSTRAINT [FK_schedule_patients]
GO
ALTER TABLE [dbo].[SystemLog]  WITH CHECK ADD  CONSTRAINT [FK__SystemLog__Owner__6B24EA82] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[SystemLog] CHECK CONSTRAINT [FK__SystemLog__Owner__6B24EA82]
GO
ALTER TABLE [dbo].[timekeeping]  WITH CHECK ADD  CONSTRAINT [FK_timekeeping_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[timekeeping] CHECK CONSTRAINT [FK_timekeeping_users]
GO
ALTER TABLE [dbo].[treatment_service_map]  WITH CHECK ADD  CONSTRAINT [FK_treatment_service_map_patient_record] FOREIGN KEY([patient_record_id])
REFERENCES [dbo].[patient_record] ([id])
GO
ALTER TABLE [dbo].[treatment_service_map] CHECK CONSTRAINT [FK_treatment_service_map_patient_record]
GO
ALTER TABLE [dbo].[treatment_service_map]  WITH CHECK ADD  CONSTRAINT [FK_treatment_service_map_services] FOREIGN KEY([service_id])
REFERENCES [dbo].[services] ([id])
GO
ALTER TABLE [dbo].[treatment_service_map] CHECK CONSTRAINT [FK_treatment_service_map_services]
GO
ALTER TABLE [dbo].[treatment_service_map]  WITH CHECK ADD  CONSTRAINT [FK_treatment_service_map_treatments] FOREIGN KEY([treatment_id])
REFERENCES [dbo].[treatments] ([id])
GO
ALTER TABLE [dbo].[treatment_service_map] CHECK CONSTRAINT [FK_treatment_service_map_treatments]
GO
ALTER TABLE [dbo].[treatments]  WITH CHECK ADD  CONSTRAINT [FK_treatments_patients] FOREIGN KEY([patient_id])
REFERENCES [dbo].[patients] ([id])
GO
ALTER TABLE [dbo].[treatments] CHECK CONSTRAINT [FK_treatments_patients]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_roles]
GO
USE [master]
GO
ALTER DATABASE [DentalSystemDB] SET  READ_WRITE 
GO
