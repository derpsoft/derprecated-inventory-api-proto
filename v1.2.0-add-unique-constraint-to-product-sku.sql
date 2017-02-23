-- UP
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

-- DOWN
IF EXISTS (
  SELECT * 
  FROM   sys.objects
  WHERE  type_desc LIKE 'UNIQUE_CONSTRAINT'
		 AND object_name(object_id)='UQ_Product_Sku'
)
BEGIN
	ALTER TABLE dbo.Product
		DROP CONSTRAINT [UQ_Product_Sku];
	;

	ALTER TABLE [dbo].[Product]
		ALTER COLUMN [Sku] VARCHAR(8000) NULL
	;
END
