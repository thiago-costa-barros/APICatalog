﻿using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Pagination;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PagedList<Product>> GetProductsPaged(PaginationParams paginationParams);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> InsertProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> RemoveProductAsync(int id);
    }
}
