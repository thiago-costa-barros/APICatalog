using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.Data.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductProcedures _productProcedures;
        public ProductRepository(ProductProcedures productProcedures)
        {
            _productProcedures = productProcedures;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productProcedures.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productProcedures.GetProductByIdAsync(id);
            return product ?? throw new InvalidOperationException($"Product not found.");
        }

        public async Task<Product> InsertProductAsync(Product product)
        {
            return await _productProcedures.InsertProductAsync(product);
        }
        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var updateProduct = await _productProcedures.UpdateProductAsync(id, product);
            return updateProduct ?? throw new InvalidOperationException($"Product not found.");
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            return await _productProcedures.RemoveProductAsync(id);
        }

    }
}
