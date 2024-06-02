
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231001211326_Init', N'7.0.4')
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[NameAr] [nvarchar](500) NULL,
	[Code] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[NameAr] [nvarchar](500) NULL,
	[NameEn] [nvarchar](500) NULL,
	[FullName] [nvarchar](500) NULL,
	[Code] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[DeletedDate] [datetime2](7) NULL,
	[Token] [nvarchar](max) NULL,
	[WebToken] [nvarchar](max) NULL,
	[IsVerifyCode] [bit] NULL,
	[IsAddPassword] [bit] NULL,
	[UserType] [int] NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Business]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Business](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](250) NULL,
	[NameEn] [nvarchar](250) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Business] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Countries]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Countries](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](250) NULL,
	[NameEn] [nvarchar](250) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Currencies]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Currencies](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](250) NULL,
	[NameEn] [nvarchar](250) NULL,
	[Symbol] [nvarchar](250) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Customers]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Customers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](500) NULL,
	[NameEn] [nvarchar](500) NULL,
	[CountryId] [bigint] NOT NULL,
	[BusinessId] [bigint] NULL,
	[Code] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[CompanySize] [nvarchar](max) NULL,
	[MultiCompanies] [bit] NULL,
	[MultiBranches] [bit] NULL,
	[NumberOfCompany] [int] NULL,
	[NumberOfBranch] [int] NULL,
	[PassWord] [nvarchar](max) NULL,
	[IsVerifyCode] [bit] NULL,
	[VerifyCode] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[DatabaseName] [nvarchar](max) NULL,
	[ServerName] [nvarchar](max) NULL,
	[SubDomain] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[CustomerSubscription]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[CustomerSubscription](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[ContractStartDate] [datetime2](7) NULL,
	[ContractEndDate] [datetime2](7) NULL,
	[Applications] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CustomerSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[PermissionRoles]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PermissionRoles](
	[RoleId] [nvarchar](450) NOT NULL,
	[PermissionId] [bigint] NOT NULL,
	[IsChecked] [bit] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PermissionRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Permissions]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Permissions](
	[Id] [bigint] NOT NULL,
	[ActionNameAr] [nvarchar](max) NOT NULL,
	[ActionNameEn] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[ScreenId] [bigint] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Screens]    Script Date: 10/10/2023 6:28:18 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Screens](
	[Id] [bigint] NOT NULL,
	[ScreenNameAr] [nvarchar](max) NOT NULL,
	[ScreenNameEn] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Screens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[Subscriptions](
	[Id] [bigint] NOT NULL,
	[NumberOfCompany] [int] NULL,
	[NumberOfBranch] [int] NULL,
	[MultiCompanies] [bit] NULL,
	[MultiBranches] [bit] NULL,
	[ContractStartDate] [datetime2](7) NULL,
	[ContractEndDate] [datetime2](7) NULL,
	[Applications] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](36) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [nvarchar](36) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdateBy] [nvarchar](36) NULL,
	[Notes] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

INSERT [dbo].[AspNetRoles] ([Id], [NameAr], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'SuperAdmin', N'1', CAST(N'2023-03-09T06:52:32.4926457' AS DateTime2), NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, N'SuperAdmin', N'SUPERADMIN', N'0181b74a-5388-46c3-b0bb-da0a5585aa4b')

INSERT [dbo].[AspNetRoles] ([Id], [NameAr], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', N'مصر', N'3', CAST(N'2023-03-09T07:32:21.6123022' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, 0, NULL, NULL, NULL, NULL, NULL, N'egypt', N'EGYPT', N'912ccede-659c-4b3c-b40c-6cffb3061b42')

INSERT [dbo].[AspNetRoles] ([Id], [NameAr], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', N'ccccccccccc', N'3', CAST(N'2023-09-10T23:37:05.4434774' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, 0, NULL, NULL, NULL, NULL, NULL, N'mahmoud abdo', N'MAHMOUD ABDO', NULL)

INSERT [dbo].[AspNetRoles] ([Id], [NameAr], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', N'mahmoud abdo', N'2', CAST(N'2023-03-09T06:55:55.3494927' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, 0, NULL, NULL, NULL, NULL, NULL, N'egypt27', N'EGYPT27', N'f904c3d9-f275-4842-8010-a61ad6ede8d0')

INSERT [dbo].[AspNetRoles] ([Id], [NameAr], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', N'test', N'2', CAST(N'2023-03-09T07:23:56.7342960' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, 0, NULL, NULL, NULL, NULL, NULL, N'test', N'TEST', N'ac86be0b-beca-4d46-8986-245226f68226')

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsActive], [IsDeleted]) VALUES (N'0dc54fde-a90b-43bc-bbcc-8de6905b4c23', N'1', 1, 0)

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsActive], [IsDeleted]) VALUES (N'f52eed0e-758d-45d1-aac2-552419cf8b41', N'1', 1, 0)

INSERT [dbo].[AspNetUsers] ([Id], [NameAr], [NameEn], [FullName], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [CreatedOn], [UpdatedOn], [UpdatedBy], [DeletedDate], [Token], [WebToken], [IsVerifyCode], [IsAddPassword], [UserType], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0dc54fde-a90b-43bc-bbcc-8de6905b4c23', N'mahmoud abdo', N'mahmoud abdo', N'eman', N'2', CAST(N'2023-09-10T23:35:58.3179708' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, 1, NULL, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, NULL, 0, 0, 2, N'0dc54fde-a90b-43bc-bbcc-8de6905b4c23', N'0DC54FDE-A90B-43BC-BBCC-8DE6905B4C23', N'mahmoud@gmail.com', N'MAHMOUD@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEO36lwjblL2hOhPLnMsoFbMYpkrgDUAlQCGThf7XTwF7KH0Ev1mDpHis9/+2OB6AUw==', N'SML2W4UI5FITRJPTOXOZXPB6XKJO3XJE', N'873409fd-65bc-4170-8d99-e09d9e1172f2', N'010365298555', 0, 0, NULL, 1, 0)

INSERT [dbo].[AspNetUsers] ([Id], [NameAr], [NameEn], [FullName], [Code], [CreatedAt], [CreatedBy], [IsDeleted], [IsActive], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [CreatedOn], [UpdatedOn], [UpdatedBy], [DeletedDate], [Token], [WebToken], [IsVerifyCode], [IsAddPassword], [UserType], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f52eed0e-758d-45d1-aac2-552419cf8b41', N'SuperAdmin', N'SuperAdmin', N'Admin', N'1', CAST(N'2023-03-09T06:52:32.1417170' AS DateTime2), N'System', 0, 0, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-03-09T06:52:32.1469805' AS DateTime2), NULL, NULL, NULL, NULL, NULL, NULL, 1, 2, N'f52eed0e-758d-45d1-aac2-552419cf8b41', N'F52EED0E-758D-45D1-AAC2-552419CF8B41', N'mahmoudabd231@gmail.com', N'MAHMOUDABD231@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEByFCANNMt1YFn7TCqbmYCSmiH9ZQ/jzieZ20ftNww2jdLrrF2YIo6/m4IYkWACc+w==', N'HIJKKONO2DTOL44S2ZP6ROQQNKSELDBY', N'13ee8c15-f139-45b1-9296-e9aa0c50e8fb', N'01234543333', 1, 0, NULL, 0, 0)

SET IDENTITY_INSERT [dbo].[Business] ON 

INSERT [dbo].[Business] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (1, N'1', N'testAr', N'testEn', CAST(N'2023-03-11T11:57:36.9796181' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Business] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (2, N'2', N'testAr2', N'testEn2', CAST(N'2023-03-11T11:57:53.2677910' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Business] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (3, N'3', N'test3test3', N'test3', CAST(N'2023-09-10T23:46:27.6345609' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)

SET IDENTITY_INSERT [dbo].[Business] OFF

SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (1, N'1', N'testAr1', N'testEn', CAST(N'2023-03-11T11:56:58.0937531' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Countries] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (2, N'2', N'testAr1', N'testEn1', CAST(N'2023-03-11T11:57:15.4238299' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Countries] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (3, N'3', N'xxcxc', N'egypt27', CAST(N'2023-09-10T23:37:36.0734236' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Countries] ([Id], [Code], [NameAr], [NameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (4, N'3', N'مصر', N'test', CAST(N'2023-09-10T23:44:04.2898764' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)

SET IDENTITY_INSERT [dbo].[Countries] OFF

SET IDENTITY_INSERT [dbo].[Currencies] ON 

INSERT [dbo].[Currencies] ([Id], [Code], [NameAr], [NameEn], [Symbol], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (1, N'1', N'testAr1', N'test2En', N'1', CAST(N'2023-03-11T11:58:15.5144106' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Currencies] ([Id], [Code], [NameAr], [NameEn], [Symbol], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (2, N'2', N'testAr1', N'testEn1', N'4', CAST(N'2023-03-11T11:58:35.9489279' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Currencies] ([Id], [Code], [NameAr], [NameEn], [Symbol], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (3, N'3', N'test', N'test', N'$this.listIds', CAST(N'2023-09-10T23:48:55.6433053' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)

SET IDENTITY_INSERT [dbo].[Currencies] OFF

SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [NameAr], [NameEn], [CountryId], [BusinessId], [Code], [PhoneNumber], [Email], [CompanySize], [MultiCompanies], [MultiBranches], [NumberOfCompany], [NumberOfBranch], [PassWord], [IsVerifyCode], [VerifyCode], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [DatabaseName], [ServerName], [SubDomain]) VALUES (10004, N'Test', N'Test', 1, NULL, N'1', N'01239393224', N'mahmoudabd23451@gmail.com', NULL, 0, 0, NULL, NULL, N'12345678', 0, N'0000000426', CAST(N'2023-10-09T22:00:43.3471264' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 1, N'ERP_1', N'.', N'http://localhost:5600')

SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[CustomerSubscription] ON 

INSERT [dbo].[CustomerSubscription] ([Id], [CustomerId], [ContractStartDate], [ContractEndDate], [Applications], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (10002, 10004, CAST(N'2023-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2023-10-19T00:00:00.0000000' AS DateTime2), N'1,5,3,2,4', CAST(N'2023-10-09T22:02:07.8310025' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 0)

SET IDENTITY_INSERT [dbo].[CustomerSubscription] OFF

SET IDENTITY_INSERT [dbo].[PermissionRoles] ON 

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 1, 1, 49, CAST(N'2023-03-09T07:32:21.6123315' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 2, 1, 50, CAST(N'2023-03-09T07:32:21.6123331' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 3, 1, 51, CAST(N'2023-03-09T07:32:21.6123339' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 4, 1, 52, CAST(N'2023-03-09T07:32:21.6123348' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 5, 1, 53, CAST(N'2023-03-09T07:32:21.6123356' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 6, 1, 54, CAST(N'2023-03-09T07:32:21.6123397' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 7, 1, 55, CAST(N'2023-03-09T07:32:21.6123404' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 8, 1, 56, CAST(N'2023-03-09T07:32:21.6123412' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 9, 1, 57, CAST(N'2023-03-09T07:32:21.6123420' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 10, 1, 58, CAST(N'2023-03-09T07:32:21.6123430' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 11, 1, 59, CAST(N'2023-03-09T07:32:21.6123438' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 12, 1, 60, CAST(N'2023-03-09T07:32:21.6123447' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 13, 1, 61, CAST(N'2023-03-09T07:32:21.6123455' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 14, 1, 62, CAST(N'2023-03-09T07:32:21.6123462' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 15, 1, 63, CAST(N'2023-03-09T07:32:21.6123470' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 16, 1, 64, CAST(N'2023-03-09T07:32:21.6123477' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 17, 1, 65, CAST(N'2023-03-09T07:32:21.6123487' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 18, 1, 66, CAST(N'2023-03-09T07:32:21.6123500' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 19, 1, 67, CAST(N'2023-03-09T07:32:21.6123508' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 20, 1, 68, CAST(N'2023-03-09T07:32:21.6123516' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 21, 1, 69, CAST(N'2023-03-09T07:32:21.6123524' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 22, 1, 70, CAST(N'2023-03-09T07:32:21.6123531' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 23, 1, 71, CAST(N'2023-03-09T07:32:21.6123540' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'43bd2e81-09e5-4a3c-9c85-ed13a5100871', 24, 1, 72, CAST(N'2023-03-09T07:32:21.6123557' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 1, 1, 73, CAST(N'2023-09-10T23:37:05.4446194' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 2, 1, 74, CAST(N'2023-09-10T23:37:05.4446244' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 3, 1, 75, CAST(N'2023-09-10T23:37:05.4446253' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 4, 1, 76, CAST(N'2023-09-10T23:37:05.4446261' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 5, 1, 77, CAST(N'2023-09-10T23:37:05.4446268' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 6, 1, 78, CAST(N'2023-09-10T23:37:05.4446280' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 7, 1, 79, CAST(N'2023-09-10T23:37:05.4446287' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 8, 1, 80, CAST(N'2023-09-10T23:37:05.4446328' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 9, 1, 81, CAST(N'2023-09-10T23:37:05.4446335' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 10, 1, 82, CAST(N'2023-09-10T23:37:05.4446343' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 11, 1, 83, CAST(N'2023-09-10T23:37:05.4446439' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 12, 1, 84, CAST(N'2023-09-10T23:37:05.4446446' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 13, 1, 85, CAST(N'2023-09-10T23:37:05.4446455' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 14, 1, 86, CAST(N'2023-09-10T23:37:05.4446461' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 15, 1, 87, CAST(N'2023-09-10T23:37:05.4446468' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 16, 1, 88, CAST(N'2023-09-10T23:37:05.4446474' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 17, 1, 89, CAST(N'2023-09-10T23:37:05.4446482' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 18, 1, 90, CAST(N'2023-09-10T23:37:05.4446521' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 19, 1, 91, CAST(N'2023-09-10T23:37:05.4446529' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 20, 1, 92, CAST(N'2023-09-10T23:37:05.4446535' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 21, 1, 93, CAST(N'2023-09-10T23:37:05.4446542' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 22, 1, 94, CAST(N'2023-09-10T23:37:05.4446548' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 23, 1, 95, CAST(N'2023-09-10T23:37:05.4446554' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'b5c606ff-4189-45df-a9e9-ec484edee7be', 24, 1, 96, CAST(N'2023-09-10T23:37:05.4446561' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, NULL, NULL, NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 1, 0, 1, CAST(N'2023-03-09T06:55:59.7178515' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8203920' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 2, 0, 2, CAST(N'2023-03-09T06:56:03.7320803' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8207645' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 3, 0, 3, CAST(N'2023-03-09T06:56:06.3408013' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8210737' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 4, 0, 4, CAST(N'2023-03-09T06:56:08.7783093' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8213923' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 5, 0, 5, CAST(N'2023-03-09T06:56:08.7783139' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8216891' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 6, 0, 6, CAST(N'2023-03-09T06:56:08.7783185' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8219773' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 7, 0, 7, CAST(N'2023-03-09T06:56:08.7783204' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8222671' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 8, 0, 8, CAST(N'2023-03-09T06:56:08.7783219' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8225594' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 9, 0, 9, CAST(N'2023-03-09T06:56:08.7783238' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8277969' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 10, 0, 10, CAST(N'2023-03-09T06:56:08.7783259' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8281780' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 11, 0, 11, CAST(N'2023-03-09T06:56:08.7783278' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8284396' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 12, 0, 12, CAST(N'2023-03-09T06:56:08.7783322' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8287306' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 13, 0, 13, CAST(N'2023-03-09T06:56:08.7783363' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8289967' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 14, 0, 14, CAST(N'2023-03-09T06:56:08.7783376' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8292738' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 15, 0, 15, CAST(N'2023-03-09T06:56:08.7783385' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8295582' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 16, 0, 16, CAST(N'2023-03-09T06:56:08.7783395' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8298463' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 17, 0, 17, CAST(N'2023-03-09T06:56:08.7783404' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8301480' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 18, 0, 18, CAST(N'2023-03-09T06:56:08.7783418' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8303945' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 19, 0, 19, CAST(N'2023-03-09T06:56:08.7783427' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8306965' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 20, 0, 20, CAST(N'2023-03-09T06:56:08.7783436' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8309836' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 21, 0, 21, CAST(N'2023-03-09T06:56:08.7783446' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8312563' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 22, 0, 22, CAST(N'2023-03-09T06:56:08.7783474' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8316037' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 23, 0, 23, CAST(N'2023-03-09T06:56:08.7783484' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8319222' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'de8a783a-2f76-494e-84a8-37df767fe65d', 24, 0, 24, CAST(N'2023-03-09T06:56:08.7783494' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 1, NULL, NULL, CAST(N'2023-03-09T07:22:38.8321865' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 1, 1, 25, CAST(N'2023-03-09T07:23:56.7343233' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6660196' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 2, 1, 26, CAST(N'2023-03-09T07:23:56.7343291' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6778654' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 3, 1, 27, CAST(N'2023-03-09T07:23:56.7343307' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6789056' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 4, 1, 28, CAST(N'2023-03-09T07:23:56.7343322' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6793036' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 5, 0, 29, CAST(N'2023-03-09T07:23:56.7343337' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6796431' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 6, 0, 30, CAST(N'2023-03-09T07:23:56.7343356' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6799231' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 7, 1, 31, CAST(N'2023-03-09T07:23:56.7343372' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6802589' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 8, 1, 32, CAST(N'2023-03-09T07:23:56.7343387' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6805636' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 9, 1, 33, CAST(N'2023-03-09T07:23:56.7343403' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6808597' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 10, 1, 34, CAST(N'2023-03-09T07:23:56.7343422' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6811608' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 11, 1, 35, CAST(N'2023-03-09T07:23:56.7343437' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6815546' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 12, 1, 36, CAST(N'2023-03-09T07:23:56.7343451' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6818771' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 13, 1, 37, CAST(N'2023-03-09T07:23:56.7343469' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6821663' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 14, 1, 38, CAST(N'2023-03-09T07:23:56.7343483' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6824666' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 15, 1, 39, CAST(N'2023-03-09T07:23:56.7343495' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6827617' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 16, 1, 40, CAST(N'2023-03-09T07:23:56.7343509' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6830459' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 17, 1, 41, CAST(N'2023-03-09T07:23:56.7343523' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6833459' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 18, 1, 42, CAST(N'2023-03-09T07:23:56.7343540' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6836219' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 19, 1, 43, CAST(N'2023-03-09T07:23:56.7343554' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6839683' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 20, 1, 44, CAST(N'2023-03-09T07:23:56.7343568' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6842608' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 21, 1, 45, CAST(N'2023-03-09T07:23:56.7343587' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6845470' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 22, 1, 46, CAST(N'2023-03-09T07:23:56.7343601' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6848187' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 23, 1, 47, CAST(N'2023-03-09T07:23:56.7343617' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6850925' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

INSERT [dbo].[PermissionRoles] ([RoleId], [PermissionId], [IsChecked], [Id], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (N'e5dee1ce-cbb7-44f6-9648-c48049ccc178', 24, 1, 48, CAST(N'2023-03-09T07:23:56.7343631' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', 0, NULL, NULL, CAST(N'2023-09-10T23:37:18.6853760' AS DateTime2), N'f52eed0e-758d-45d1-aac2-552419cf8b41', NULL, 0)

SET IDENTITY_INSERT [dbo].[PermissionRoles] OFF

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (1, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9042516' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 1)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (2, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9056884' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 1)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (3, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9056949' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 1)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (4, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9056974' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 1)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (5, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9056996' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 2)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (6, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9057021' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 2)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (7, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9057088' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 2)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (8, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9057116' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 2)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (9, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9057139' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 3)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (10, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9057163' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 3)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (11, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9062916' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 3)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (12, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9062956' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 3)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (13, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9062989' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 4)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (14, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9063027' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 4)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (15, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9063049' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 4)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (16, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9063071' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 4)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (17, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9063092' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 5)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (18, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9063120' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 5)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (19, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9063143' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 5)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (20, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9063168' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 5)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (21, N'اضافة', N'Add', CAST(N'2023-03-09T08:52:32.9063189' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 6)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (22, N'تعديل', N'Edit', CAST(N'2023-03-09T08:52:32.9063211' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 6)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (23, N'حذف', N'Delete', CAST(N'2023-03-09T08:52:32.9063233' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 6)

INSERT [dbo].[Permissions] ([Id], [ActionNameAr], [ActionNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive], [ScreenId]) VALUES (24, N'عرض', N'Show', CAST(N'2023-03-09T08:52:32.9063254' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1, 6)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (1, N'الدول', N'Country', CAST(N'2023-03-09T08:52:32.8041366' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (2, N'الانشطة', N'Business', CAST(N'2023-03-09T08:52:32.8404769' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (3, N'المستخدمين', N'User', CAST(N'2023-03-09T08:52:32.8405034' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (4, N'العملاء', N'Customer', CAST(N'2023-03-09T08:52:32.8405064' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (5, N'العملات', N'Currency', CAST(N'2023-03-09T08:52:32.8405084' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

INSERT [dbo].[Screens] ([Id], [ScreenNameAr], [ScreenNameEn], [CreatedAt], [CreatedBy], [IsDeleted], [DeletedAt], [DeletedBy], [UpdatedAt], [UpdateBy], [Notes], [IsActive]) VALUES (6, N'الأدوار', N'Role', CAST(N'2023-03-09T08:52:32.8405110' AS DateTime2), NULL, 0, NULL, NULL, NULL, NULL, NULL, 1)

ALTER TABLE [dbo].[AspNetUserRoles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]

ALTER TABLE [dbo].[AspNetUserRoles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]

ALTER TABLE [dbo].[Permissions] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [ScreenId]

ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Business_BusinessId] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[Business] ([Id])

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Business_BusinessId]

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Countries_CountryId]

ALTER TABLE [dbo].[CustomerSubscription]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSubscription_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[CustomerSubscription] CHECK CONSTRAINT [FK_CustomerSubscription_Customers_CustomerId]

ALTER TABLE [dbo].[PermissionRoles]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[PermissionRoles] CHECK CONSTRAINT [FK_PermissionRoles_AspNetRoles_RoleId]

ALTER TABLE [dbo].[PermissionRoles]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoles_Permissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[PermissionRoles] CHECK CONSTRAINT [FK_PermissionRoles_Permissions_PermissionId]

ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Screens_ScreenId]

