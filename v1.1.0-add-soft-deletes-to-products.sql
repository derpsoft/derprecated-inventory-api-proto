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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		DROP CONSTRAINT [DF_Product_IsDeleted]
	;
	ALTER TABLE [dbo].[Product]
		DROP COLUMN [IsDeleted]
	;
END

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		DROP COLUMN [DeleteDate]
	;
END
