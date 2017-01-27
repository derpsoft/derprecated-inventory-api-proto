﻿-- UP
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