USE master

DROP DATABASE TestDb
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE [name] = N'TestDb')
CREATE DATABASE TestDb
GO


USE TestDb
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'Product')
CREATE TABLE Product 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[Name] nvarchar(255) NOT NULL,
	[Description] nvarchar(max),
	CONSTRAINT UK_Name UNIQUE(Name)
)
CREATE NONCLUSTERED INDEX IX_Product_Name ON TestDb.dbo.Product(Name ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'ProductVersion')
CREATE TABLE ProductVersion 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[ProductID] uniqueidentifier FOREIGN KEY REFERENCES dbo.Product(ID) NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[Description] nvarchar(max),
	[CreatedDate] smalldatetime NOT NULL DEFAULT getDate(),
	[Width] real NOT NULL,
	[Height] real NOT NULL,
	[Length] real NOT NULL,

	CONSTRAINT UK_Name UNIQUE(Name)
)
GO

CREATE NONCLUSTERED INDEX IX_ProductVersion_Name ON TestDb.dbo.Product(Name ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_CreatedDate ON TestDb.dbo.ProductVersion(CreatedDate ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Width ON TestDb.dbo.ProductVersion(Width ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Height ON TestDb.dbo.ProductVersion(Height ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Length ON TestDb.dbo.ProductVersion(Length ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
