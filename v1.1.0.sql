-- UP
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'CreateDate'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		ADD [CreateDate] 
			DATETIME NOT NULL
			CONSTRAINT [DF_Sale_CreateDate]
				DEFAULT GETUTCDATE()
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'ModifyDate'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		ADD [ModifyDate] 
			DATETIME NOT NULL 
			CONSTRAINT [DF_Sale_ModifyDate]
				DEFAULT GETUTCDATE()
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'RowVersion'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		ADD [RowVersion] 
			TIMESTAMP NOT NULL
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'UserAuthId'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		ADD [UserAuthId] 
			INT NOT NULL
			CONSTRAINT [DF_Sale_UserAuthId] 
				DEFAULT 1
			CONSTRAINT [FK_Sale_UserAuthId]
				FOREIGN KEY (UserAuthId) 
				REFERENCES [dbo].[UserAuth](Id)
	;
	
	ALTER TABLE [dbo].[Sale]
		DROP CONSTRAINT [DF_Sale_UserAuthId]
	;
END

-- UP
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'CategoryId'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		ADD [CategoryId] int null
		CONSTRAINT [FK_Product_CategoryId] REFERENCES [dbo].[Category](Id)
	;
END

-- UP
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'Name'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		ADD [Name] VARCHAR(50);
END

-- UP
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'ParentId'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		ALTER COLUMN ParentId INTEGER null
	;
	
	UPDATE [dbo].[Category] SET ParentId = NULL WHERE ParentId = 0;

	ALTER TABLE [dbo].[Category]
		ADD CONSTRAINT [FK_Category_ParentId] FOREIGN KEY (ParentId) REFERENCES [dbo].[Category](Id)
			ON DELETE NO ACTION
			ON UPDATE NO ACTION
	;
END

-- UP
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		ADD [IsDeleted] BIT NOT NULL
			CONSTRAINT [DF_Location_IsDeleted]
			DEFAULT 0
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		ADD [DeleteDate] DATETIME
	;
END

-- UP
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		ADD [IsDeleted] BIT NOT NULL
			CONSTRAINT [DF_Product_IsDeleted]
			DEFAULT 0
	;
END
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		ADD [DeleteDate] DATETIME
	;
END

-- UP
ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Shelf] VARCHAR(10) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Rack] VARCHAR(10) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Bin] VARCHAR(10) NULL
; 

-- UP
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'InventoryTransactionId'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [InventoryTransactionId]
	;
END

-- UP
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'VendorId'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [VendorId]
	;
END
