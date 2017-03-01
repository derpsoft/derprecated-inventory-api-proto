-- UP CATEGORY
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		ADD [IsDeleted] BIT NOT NULL
			CONSTRAINT [DF_Category_IsDeleted]
			DEFAULT 0
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		ADD [DeleteDate] DATETIME
	;
END

-- UP VENDOR
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Vendor]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Vendor]
		ADD [IsDeleted] BIT NOT NULL
			CONSTRAINT [DF_Vendor_IsDeleted]
			DEFAULT 0
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Vendor]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Vendor]
		ADD [DeleteDate] DATETIME
	;
END
-- UP WAREHOUSE
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Warehouse]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Warehouse]
		ADD [IsDeleted] BIT NOT NULL
			CONSTRAINT [DF_Warehouse_IsDeleted]
			DEFAULT 0
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Warehouse]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Warehouse]
		ADD [DeleteDate] DATETIME
	;
END

-- DOWN WAREHOUSE
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Warehouse]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Warehouse]
		DROP CONSTRAINT [DF_Warehouse_IsDeleted]
	;
	ALTER TABLE [dbo].[Warehouse]
		DROP COLUMN [IsDeleted]
	;
END

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Warehouse]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Warehouse]
		DROP COLUMN [DeleteDate]
	;
END

-- DOWN VENDOR
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Vendor]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Vendor]
		DROP CONSTRAINT [DF_Vendor_IsDeleted]
	;
	ALTER TABLE [dbo].[Vendor]
		DROP COLUMN [IsDeleted]
	;
END

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Vendor]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Vendor]
		DROP COLUMN [DeleteDate]
	;
END

-- DOWN CATEGORY
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		DROP CONSTRAINT [DF_Category_IsDeleted]
	;
	ALTER TABLE [dbo].[Category]
		DROP COLUMN [IsDeleted]
	;
END

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		DROP COLUMN [DeleteDate]
	;
END
