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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Location]') 
         AND name = 'Name'
)
BEGIN
	ALTER TABLE [dbo].[Location]
		DROP COLUMN [Name];
END
