
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/14/2020 16:24:43
-- Generated from EDMX file: C:\Working\TESTej\ShoppingMail\ShoppingMail\Models\dbShoppingMailModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbShoppingMail];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ItemModel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemModel];
GO
IF OBJECT_ID(N'[dbo].[ProductModel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductModel];
GO
IF OBJECT_ID(N'[dbo].[tAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tAttributes];
GO
IF OBJECT_ID(N'[dbo].[tExchangeRate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tExchangeRate];
GO
IF OBJECT_ID(N'[dbo].[ThumbsUp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ThumbsUp];
GO
IF OBJECT_ID(N'[dbo].[tMember]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tMember];
GO
IF OBJECT_ID(N'[dbo].[tOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tOrder];
GO
IF OBJECT_ID(N'[dbo].[tOrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[tProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tProduct];
GO
IF OBJECT_ID(N'[dbo].[tProductCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tProductCategory];
GO
IF OBJECT_ID(N'[dbo].[tProductProperty]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tProductProperty];
GO
IF OBJECT_ID(N'[dbo].[tProductStock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tProductStock];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tMember'
CREATE TABLE [dbo].[tMember] (
    [fId] int IDENTITY(1,1) NOT NULL,
    [fUserId] nvarchar(50)  NULL,
    [fPwd] nvarchar(50)  NULL,
    [fName] nvarchar(50)  NULL,
    [fEmail] nvarchar(50)  NULL
);
GO

-- Creating table 'tOrder'
CREATE TABLE [dbo].[tOrder] (
    [fId] int IDENTITY(1,1) NOT NULL,
    [fOrderId] int  NULL,
    [fUserId] nvarchar(50)  NULL,
    [fReceiver] nvarchar(50)  NULL,
    [fEmail] nvarchar(50)  NULL,
    [fAddress] nvarchar(50)  NULL,
    [fDate] datetime  NULL
);
GO

-- Creating table 'tOrderDetail'
CREATE TABLE [dbo].[tOrderDetail] (
    [fId] int IDENTITY(1,1) NOT NULL,
    [fOrderId] int  NULL,
    [fUserId] nvarchar(50)  NULL,
    [fPId] int  NULL,
    [fName] nvarchar(50)  NULL,
    [fPrice] int  NULL,
    [fQty] int  NULL,
    [fIsApproved] nvarchar(50)  NULL,
    [fImg] nvarchar(50)  NULL,
    [fColor] nvarchar(50)  NULL,
    [fSize] nvarchar(50)  NULL
);
GO

-- Creating table 'tProductCategory'
CREATE TABLE [dbo].[tProductCategory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fParent_Id] int  NULL,
    [fName] nvarchar(50)  NULL
);
GO

-- Creating table 'tProductProperty'
CREATE TABLE [dbo].[tProductProperty] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PId] int  NULL,
    [PColor] nvarchar(50)  NULL,
    [PSize] nvarchar(50)  NULL,
    [PQty] int  NULL
);
GO

-- Creating table 'ThumbsUp'
CREATE TABLE [dbo].[ThumbsUp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fMId] int  NULL,
    [fPId] int  NULL
);
GO

-- Creating table 'tExchangeRate'
CREATE TABLE [dbo].[tExchangeRate] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Currency] nvarchar(50)  NULL,
    [CashRate] int  NULL
);
GO

-- Creating table 'tAttributes'
CREATE TABLE [dbo].[tAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fAId] int  NOT NULL,
    [fAName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tProductStock'
CREATE TABLE [dbo].[tProductStock] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fPId] int  NOT NULL,
    [fAId_1] int  NULL,
    [fAId_2] int  NULL,
    [fAId_3] int  NULL,
    [fSupplyLimit] int  NOT NULL,
    [fQty] int  NULL,
    [fAId_4] int  NULL,
    [fAId_5] int  NULL
);
GO

-- Creating table 'tProduct'
CREATE TABLE [dbo].[tProduct] (
    [fId] int IDENTITY(1,1) NOT NULL,
    [fPId] int  NULL,
    [fCategory] int  NULL,
    [fName] nvarchar(50)  NULL,
    [fPrice] int  NULL,
    [fImg] nvarchar(50)  NULL,
    [fP_islike] int  NULL,
    [fChangeQTY] int  NULL
);
GO

-- Creating table 'ItemModel'
CREATE TABLE [dbo].[ItemModel] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [ParentCategoryId] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [SupplierName] nvarchar(50)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [Price] int  NOT NULL,
    [Img] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductModel'
CREATE TABLE [dbo].[ProductModel] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [ParentCategoryId] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [SupplierName] nvarchar(50)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [Price] int  NOT NULL,
    [Img] nvarchar(50)  NOT NULL,
    [Stock] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [fId] in table 'tMember'
ALTER TABLE [dbo].[tMember]
ADD CONSTRAINT [PK_tMember]
    PRIMARY KEY CLUSTERED ([fId] ASC);
GO

-- Creating primary key on [fId] in table 'tOrder'
ALTER TABLE [dbo].[tOrder]
ADD CONSTRAINT [PK_tOrder]
    PRIMARY KEY CLUSTERED ([fId] ASC);
GO

-- Creating primary key on [fId] in table 'tOrderDetail'
ALTER TABLE [dbo].[tOrderDetail]
ADD CONSTRAINT [PK_tOrderDetail]
    PRIMARY KEY CLUSTERED ([fId] ASC);
GO

-- Creating primary key on [Id] in table 'tProductCategory'
ALTER TABLE [dbo].[tProductCategory]
ADD CONSTRAINT [PK_tProductCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tProductProperty'
ALTER TABLE [dbo].[tProductProperty]
ADD CONSTRAINT [PK_tProductProperty]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ThumbsUp'
ALTER TABLE [dbo].[ThumbsUp]
ADD CONSTRAINT [PK_ThumbsUp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tExchangeRate'
ALTER TABLE [dbo].[tExchangeRate]
ADD CONSTRAINT [PK_tExchangeRate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tAttributes'
ALTER TABLE [dbo].[tAttributes]
ADD CONSTRAINT [PK_tAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tProductStock'
ALTER TABLE [dbo].[tProductStock]
ADD CONSTRAINT [PK_tProductStock]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [fId] in table 'tProduct'
ALTER TABLE [dbo].[tProduct]
ADD CONSTRAINT [PK_tProduct]
    PRIMARY KEY CLUSTERED ([fId] ASC);
GO

-- Creating primary key on [Id] in table 'ItemModel'
ALTER TABLE [dbo].[ItemModel]
ADD CONSTRAINT [PK_ItemModel]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductModel'
ALTER TABLE [dbo].[ProductModel]
ADD CONSTRAINT [PK_ProductModel]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------