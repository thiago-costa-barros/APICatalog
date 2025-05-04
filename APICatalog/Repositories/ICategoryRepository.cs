using APICatalog.Entities.Models;

namespace APICatalog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> InsertCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category category);
        Task<bool> RemoveCategoryAsync(int id);
    }
}
