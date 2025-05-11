USE ApiCatalogDb
GO

CREATE OR ALTER PROCEDURE GetProductsPaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SELECT 
        p.ProductId
		, p.ProductName
		, p.Description
		, p.Price
		, p.Stock
		, p.ImageUrl
		, p.CategoryId
		, p.CreationDate
		, p.UpdateDate
		, p.DeletionDate
		, p.CreationUserId
		, p.UpdateUserId
    FROM Products p
    WHERE p.DeletionDate IS NULL
    ORDER BY p.ProductId
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;