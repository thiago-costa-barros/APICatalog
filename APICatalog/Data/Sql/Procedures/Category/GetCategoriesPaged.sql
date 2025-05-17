USE ApiCatalogDb
GO

CREATE OR ALTER PROCEDURE GetCategoriesPaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
	SELECT 
		p.[CategoryId]
		, p.[CategoryName]
		, p.[Description]
		, p.[ImageUrl]
		, p.[CreationDate]
		, p.[UpdateDate]
		, p.[DeletionDate]
		, p.[CreationUserId]
		, p.[UpdateUserId]
	FROM [Categories] p
	WHERE p.[DeletionDate] is null
	ORDER BY p.[CategoryId]
	OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;