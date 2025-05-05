using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Data.Repositories.Products
{
    public class ProductProcedures
    {
        private readonly AppDbContext _context;

        public ProductProcedures(AppDbContext context)
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

        public async Task<Product> InsertProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
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
