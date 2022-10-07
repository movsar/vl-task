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

	CONSTRAINT UK_Product_Name UNIQUE(Name),
	INDEX IX_Product_Name NONCLUSTERED (Name ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON)
)

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

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [name] = N'EventLog')
CREATE TABLE EventLog 
(
	[ID] uniqueidentifier PRIMARY KEY NOT NULL DEFAULT NewID(),
	[EventDate] smalldatetime NOT NULL DEFAULT getDate(),
	[Description] nvarchar(max),
)
CREATE NONCLUSTERED INDEX IX_EventLog_EventDate ON TestDb.dbo.EventLog(EventDate ASC) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

CREATE TRIGGER TR_Product_Insert ON Product
FOR INSERT AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('Inserted ', (SELECT Count(*) FROM Inserted), ' row(s)')
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
		CONCAT('Updated ', (SELECT Count(*) FROM Inserted), ' row(s)')
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
		CONCAT('Deleted ', (SELECT Count(*) FROM Deleted), ' row(s)')
    );
END
GO 


CREATE TRIGGER TR_ProductVersion_Insert ON ProductVersion
FOR INSERT AS
BEGIN
    INSERT INTO EventLog(
        [EventDate],
        [Description]
    )
    VALUES(
        getDate(),
		CONCAT('Inserted ', (SELECT Count(*) FROM Inserted), ' row(s)')
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
		CONCAT('Updated ', (SELECT Count(*) FROM Inserted), ' row(s)')
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
		CONCAT('Deleted ', (SELECT Count(*) FROM Deleted), ' row(s)')
    );
END