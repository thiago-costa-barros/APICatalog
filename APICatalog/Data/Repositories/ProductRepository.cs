using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Data.Repositories.DAOs;
using APICatalog.Data.Repositories.Interfaces;

namespace APICatalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;
        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productDAO.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productDAO.GetProductByIdAsync(id);
            return product ?? throw new InvalidOperationException($"Product not found.");
        }

        public async Task<Product> InsertProductAsync(Product product)
        {
            return await _productDAO.InsertProductAsync(product);
        }
        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var updateProduct = await _productDAO.UpdateProductAsync(id, product);
            return updateProduct ?? throw new InvalidOperationException($"Product not found.");
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            return await _productDAO.RemoveProductAsync(id);
        }

    }
}
