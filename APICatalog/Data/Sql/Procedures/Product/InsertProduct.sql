USE [ApiCatalogDb]
GO

CREATE OR ALTER PROCEDURE InsertProduct
	@ProductName NVARCHAR(100),  
	@Description NVARCHAR(512),  
	@ImageUrl NVARCHAR(512),
	@Price DECIMAL(14,2),
	@Stock INT,
	@CategoryId INT
AS
BEGIN

SET NOCOUNT ON;  
  
	DECLARE @paramDate DATETIME = GETDATE();  

	INSERT INTO [dbo].[Products]
	           ([ProductName]
	           ,[Description]
	           ,[ImageUrl]
	           ,[Price]
	           ,[Stock]
	           ,[CategoryId]
	           ,[CreationDate]
	           ,[UpdateDate]
	           ,[CreationUserId]
	           ,[UpdateUserId])
	OUTPUT INSERTED.* 
	VALUES
	           (@ProductName
	           ,@Description
	           ,@ImageUrl
	           ,@Price
	           ,@Stock
	           ,@CategoryId
	           ,@paramDate
	           ,@paramDate
	           ,null
	           ,null)
END;