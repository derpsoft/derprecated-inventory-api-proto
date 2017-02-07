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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'IsDeleted'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		DROP CONSTRAINT [DF_Location_IsDeleted]
	;
	ALTER TABLE [dbo].[Location]
		DROP COLUMN [IsDeleted]
	;
END

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'DeleteDate'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		DROP COLUMN [DeleteDate]
	;
END
