USE [ApiCatalogDb]
GO

CREATE OR ALTER PROCEDURE InsertCategory
	@CategoryName NVARCHAR(100),
	@Description NVARCHAR(512),
	@ImageUrl NVARCHAR(512)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @paramDate DATETIME = GETDATE();

	INSERT INTO [Categories] (
		[CategoryName],
		[Description],
		[ImageUrl],
		[CreationDate],
		[UpdateDate],
		[CreationUserId],
		[UpdateUserId]
	)
	OUTPUT INSERTED.*
	VALUES (
		@CategoryName,
		@Description,
		@ImageUrl,
		@paramDate,
		@paramDate,
		NULL,
		NULL
	);
END;