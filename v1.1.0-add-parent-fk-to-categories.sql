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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Category]') 
         AND name = 'ParentId'
)
BEGIN
	ALTER TABLE [dbo].[Category]
		DROP CONSTRAINT [FK_Category_ParentId]
	;
END