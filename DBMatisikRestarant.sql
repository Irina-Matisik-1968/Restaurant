USE [DBMatisikRestarant22]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 06.02.2024 17:33:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 06.02.2024 17:33:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductCategory] [int] NOT NULL,
	[ProductCost] [float] NOT NULL,
	[ProductDescription] [nvarchar](200) NULL,
	[ProductPhotoString] [nvarchar](200) NULL,
	[ProductPhotoByte] [varbinary](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06.02.2024 17:33:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserLogin] [nvarchar](50) NOT NULL,
	[UserPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Первое блюдо')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Второе блюдо')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Гарнир')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Десерт')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (5, N'Напитки')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductCategory], [ProductCost], [ProductDescription], [ProductPhotoString], [ProductPhotoByte]) VALUES (2, N'Борщ', 1, 100, NULL, NULL, NULL)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductCategory], [ProductCost], [ProductDescription], [ProductPhotoString], [ProductPhotoByte]) VALUES (3, N'Суп гороховый', 1, 130, N'С копченостями', NULL, NULL)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductCategory], [ProductCost], [ProductDescription], [ProductPhotoString], [ProductPhotoByte]) VALUES (4, N'Каша гречневая', 2, 30, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserLogin], [UserPassword]) VALUES (1, N'Admin1', N'Password1')
INSERT [dbo].[User] ([UserId], [UserLogin], [UserPassword]) VALUES (2, N'Admin2', N'Password2')
INSERT [dbo].[User] ([UserId], [UserLogin], [UserPassword]) VALUES (3, N'Admin3', N'Password3')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([ProductCategory])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
