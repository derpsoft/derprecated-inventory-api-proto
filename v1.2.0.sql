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

-- UP UQ Product.Sku
IF NOT EXISTS (
  SELECT * 
  FROM   sys.objects
  WHERE  type_desc LIKE 'UNIQUE_CONSTRAINT'
		 AND object_name(object_id)='UQ_Product_Sku'
)
BEGIN
	TRUNCATE TABLE dbo.ProductCategory;
	TRUNCATE TABLE dbo.ProductImage;
	TRUNCATE TABLE dbo.ProductTag;
	
	DELETE FROM dbo.Product;

	ALTER TABLE [dbo].[Product]
		ALTER COLUMN [Sku] VARCHAR(200) NOT NULL
	;

	ALTER TABLE dbo.Product
		ADD CONSTRAINT [UQ_Product_Sku] UNIQUE(Sku)
	;
END
