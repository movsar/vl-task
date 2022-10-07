USE master
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE [name] = N'TestDb')
CREATE DATABASE TestDb

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