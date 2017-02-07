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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'CreateDate'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP CONSTRAINT [DF_Sale_CreateDate]
	;
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [CreateDate]
	;
END
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'ModifyDate'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP CONSTRAINT [DF_Sale_ModifyDate]
	;
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [ModifyDate]
	;
END
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'RowVersion'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [RowVersion]
	;
END
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Sale]') 
         AND name = 'UserAuthId'
)
BEGIN
	ALTER TABLE [dbo].[Sale]
		DROP CONSTRAINT [FK_Sale_UserAuthId]
	;
	ALTER TABLE [dbo].[Sale]
		DROP COLUMN [UserAuthId]
	;
END
