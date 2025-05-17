using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Pagination;

namespace APICatalog.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync();
        Task<PagedList<Category>> GetCategoriesPaged(PaginationParams paginationParams);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> InsertCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category category);
        Task<bool> RemoveCategoryAsync(int id);
    }
}
