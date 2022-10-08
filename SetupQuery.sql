USE master

-- Remove existing database
DROP DATABASE TestDb
GO

-- Create database
CREATE DATABASE TestDb
GO

USE TestDb
-- Product table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'Product')
CREATE TABLE Product 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[Name] nvarchar(255) NOT NULL,
	[Description] nvarchar(max),

	CONSTRAINT UK_Product_Name UNIQUE(Name),
	INDEX IX_Product_Name NONCLUSTERED (Name ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON)
)

-- ProductVersion table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'ProductVersion')
CREATE TABLE ProductVersion 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[ProductID] uniqueidentifier FOREIGN KEY REFERENCES dbo.Product(ID) ON DELETE CASCADE NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[Description] nvarchar(max),
	[CreatedDate] smalldatetime NOT NULL DEFAULT getDate(),
	[Width] real NOT NULL,
	[Height] real NOT NULL,
	[Length] real NOT NULL,

	INDEX IX_ProductVersion_Name NONCLUSTERED (Name ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON),
	INDEX IX_ProductVersion_CreatedDate NONCLUSTERED (CreatedDate ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON),
	INDEX IX_ProductVersion_Width NONCLUSTERED (Width ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON),
	INDEX IX_ProductVersion_Height NONCLUSTERED (Height ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON),
	INDEX IX_ProductVersion_Length NONCLUSTERED (Length ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON)
)

-- EventLog table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'EventLog')
CREATE TABLE EventLog 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[EventDate] smalldatetime NOT NULL DEFAULT getDate(),
	[Description] nvarchar(max),
)
CREATE NONCLUSTERED INDEX IX_EventLog_EventDate ON TestDb.dbo.EventLog(EventDate ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

--Triggers for Product table
CREATE TRIGGER TR_Product_Insert ON Product
FOR INSERT AS
BEGIN
	DECLARE @affectedProductNames varchar(1000)
	SET @affectedProductNames = ''
	SELECT @affectedProductNames = @affectedProductNames + Name + ', '
	FROM Inserted

	SET @affectedProductNames = SUBSTRING(@affectedProductNames, 1, LEN(@affectedProductNames) - 1) 

    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('Product - Inserted ', (SELECT Count(*) FROM Inserted), ' row(s) with Names: ', @affectedProductNames)
    );
END
GO

CREATE TRIGGER TR_Product_Update ON Product
FOR UPDATE AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('Product - Updated ', (SELECT Count(*) FROM Inserted), ' row(s)')
    );
END
GO

CREATE TRIGGER TR_Product_Delete ON Product
FOR DELETE AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('Product - Deleted ', (SELECT Count(*) FROM Deleted), ' row(s)')
    );
END
GO 

--Triggers for ProductVersion table
CREATE TRIGGER TR_ProductVersion_Insert ON ProductVersion
FOR INSERT AS
BEGIN
  	DECLARE @affectedProductVersionNames varchar(1000)
	SET @affectedProductVersionNames = ''
	SELECT @affectedProductVersionNames = @affectedProductVersionNames + Name + ', '
	FROM Inserted

	SET @affectedProductVersionNames = SUBSTRING(@affectedProductVersionNames, 1, LEN(@affectedProductVersionNames) - 1) 

    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('ProductVersion - Inserted ', (SELECT Count(*) FROM Inserted), ' row(s) with Names: ', @affectedProductVersionNames)
    );
END
GO

CREATE TRIGGER TR_ProductVersion_Update ON ProductVersion
FOR UPDATE AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('ProductVersion - Updated ', (SELECT Count(*) FROM Inserted), ' row(s)')
    );
END
GO

CREATE TRIGGER TR_ProductVersion_Delete ON ProductVersion
FOR DELETE AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('ProductVersion - Deleted ', (SELECT Count(*) FROM Deleted), ' row(s)')
    );
END
GO

CREATE FUNCTION ProductVersion_Search_Func (
	@productName varchar(100),
	@productVersionName varchar(100),
	@productMinVolume int,
	@productMaxVolume int
)
RETURNS TABLE AS
RETURN
	SELECT pv.ID AS ProductVersionID, p.Name AS ProductName, 
		pv.Name AS ProductVersionName, pv.Width AS Width, 
		pv.Length AS Length, pv.Height AS Height
	FROM ProductVersion pv
	INNER JOIN Product p ON pv.ProductID = p.ID
	WHERE p.Name LIKE '%' + @productName + '%'
		AND pv.Name LIKE '%' + @productVersionName + '%'
		AND @productMinVolume <= (pv.Height * pv.Width * pv.Length)
		AND @productMaxVolume >= (pv.Height * pv.Width * pv.Length)