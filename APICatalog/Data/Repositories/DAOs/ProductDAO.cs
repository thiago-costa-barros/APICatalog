using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Core.Common.Pagination;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Data.Repositories.DAOs
{
    public class ProductDAO
    {
        private readonly AppDbContext _context;

        public ProductDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Where(p => p.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PagedList<Product>> GetProductsPaged(PaginationParams paginationParams)
        {
            var products = await _context.Products
                .FromSqlRaw("""
                EXEC 
                    GetProductsPaged 
                    @PageNumber, 
                    @PageSize
                """,
                new SqlParameter("@PageNumber", paginationParams.PageNumber),
                new SqlParameter("@PageSize", paginationParams.PageSize))
                .AsNoTracking()
                .ToListAsync();

            var totalCount = await _context.Products
                .Where(p => p.DeletionDate == null)
                .CountAsync();

            var productsPaged = new PagedList<Product>(
                products,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );

            return productsPaged;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.DeletionDate == null);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<Product?> InsertProductAsync(Product product)
        {
            var procedure = await _context.Products
                .FromSqlRaw("""
                EXEC 
                    InsertProduct 
                    @ProductName, 
                    @Description, 
                    @ImageUrl, 
                    @Price, 
                    @Stock,
                    @CategoryId
                """,
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@Description", product.Description ?? (object)DBNull.Value),
                new SqlParameter("@ImageUrl", product.ImageUrl),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Stock", product.Stock),
                new SqlParameter("@CategoryId", product.CategoryId))
                .AsNoTracking()
                .ToListAsync();

            var insertProduct = procedure.FirstOrDefault();

            return insertProduct;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            product.ProductId = id;

            var existingProduct = await GetProductByIdAsync(id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.ProductName = product.ProductName ?? existingProduct.ProductName;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.ImageUrl = product.ImageUrl ?? existingProduct.ImageUrl;
            existingProduct.Price = product.Price != 0 ? product.Price : existingProduct.Price;
            existingProduct.CategoryId = product.CategoryId ?? existingProduct.CategoryId;
            existingProduct.UpdateDate = DateTime.UtcNow;

            _context.Products.Update(existingProduct);
            return existingProduct;
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            product.DeletionDate = DateTime.UtcNow;

            _context.Products.Update(product);
            return true;
        }
    }
}
