USE [DemoWebAPI]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07/23/2020 15:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[UnitPrice] [decimal](18, 0) NOT NULL,
	[AvailableQuantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON
INSERT [dbo].[Products] ([ProductId], [Name], [UnitPrice], [AvailableQuantity]) VALUES (1, N'Desktop', CAST(20000 AS Decimal(18, 0)), 0)
INSERT [dbo].[Products] ([ProductId], [Name], [UnitPrice], [AvailableQuantity]) VALUES (2, N'Laptop', CAST(30000 AS Decimal(18, 0)), 10)
INSERT [dbo].[Products] ([ProductId], [Name], [UnitPrice], [AvailableQuantity]) VALUES (3, N'Smart Phone', CAST(20000 AS Decimal(18, 0)), 5)
INSERT [dbo].[Products] ([ProductId], [Name], [UnitPrice], [AvailableQuantity]) VALUES (4, N'iPad', CAST(50000 AS Decimal(18, 0)), 6)
SET IDENTITY_INSERT [dbo].[Products] OFF
/****** Object:  Table [dbo].[Orders]    Script Date: 07/23/2020 15:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[OrderedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Orders] ON
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [TotalPrice], [OrderedDate], [Status]) VALUES (1, 2, CAST(12 AS Decimal(18, 0)), CAST(0x0000AC2000C5C100 AS DateTime), 1)
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [TotalPrice], [OrderedDate], [Status]) VALUES (2, 2, CAST(600 AS Decimal(18, 0)), CAST(0x0000AC0100C5C100 AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
/****** Object:  Table [dbo].[Customers]    Script Date: 07/23/2020 15:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email], [Password], [CreatedDate]) VALUES (1, N'A', N'a@a.com', N'a123', CAST(0x0000ABFF00000000 AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email], [Password], [CreatedDate]) VALUES (2, N'BB', N'b@a.com', N'b123', CAST(0x0000ABFF00000000 AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email], [Password], [CreatedDate]) VALUES (3, N'CC', N'c@a.com', N'c123', CAST(0x0000ABFF00000000 AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email], [Password], [CreatedDate]) VALUES (4, N'DDDDD', N'c@a.com', N'c123', CAST(0x0000AC0100326E42 AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email], [Password], [CreatedDate]) VALUES (7, N'SSSS', N'x@x.com', N'123', CAST(0x0000AC010035E3AF AS DateTime))
SET IDENTITY_INSERT [dbo].[Customers] OFF
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderStatus]    Script Date: 07/23/2020 15:48:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[SP_UpdateOrderStatus]
@orderId as int,
@status as int
As
BEGIN
	Update Orders Set Status=@status Where OrderId=@orderId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerListByHasOrder]    Script Date: 07/23/2020 15:48:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[SP_CustomerListByHasOrder]
@hasOrder as bit
As
BEGIN
	IF @hasOrder=1
		BEGIN
			SELECT distinct a.* from Customers a
			INNER JOIN Orders b on b.CustomerId=a.CustomerId
		END
	ELSE
		BEGIN
			SELECT distinct a.* from Customers a
			left JOIN Orders b on b.CustomerId=a.CustomerId
			Where b.CustomerId IS null
		END
END
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 07/23/2020 15:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (1, 1, 1, 2, CAST(22 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (2, 1, 2, 3, CAST(111 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (3, 1, 3, 3, CAST(33 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (4, 2, 1, 1, CAST(100 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (5, 2, 2, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [Price]) VALUES (6, 2, 3, 1, CAST(300 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
/****** Object:  Default [DF__Customers__Creat__014935CB]    Script Date: 07/23/2020 15:48:12 ******/
ALTER TABLE [dbo].[Customers] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF__Orders__OrderedD__1273C1CD]    Script Date: 07/23/2020 15:48:12 ******/
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderedDate]
GO
/****** Object:  ForeignKey [FK__OrderDeta__Order__173876EA]    Script Date: 07/23/2020 15:48:12 ******/
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
