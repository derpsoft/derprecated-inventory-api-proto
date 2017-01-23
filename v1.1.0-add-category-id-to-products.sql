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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') 
         AND name = 'CategoryId'
)
BEGIN
	ALTER TABLE [dbo].[Product]
		DROP CONSTRAINT [FK_Product_CategoryId],
		COLUMN [CategoryId]
	;
END
