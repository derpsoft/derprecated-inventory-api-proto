﻿-- UP
ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Shelf] VARCHAR(10) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Rack] VARCHAR(10) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Bin] VARCHAR(10) NULL
; 
 

-- DOWN
ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Shelf] VARCHAR(MAX) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Rack] VARCHAR(MAX) NULL
;

ALTER TABLE [dbo].[Location] 
	ALTER COLUMN [Bin] VARCHAR(MAX) NULL
; 
