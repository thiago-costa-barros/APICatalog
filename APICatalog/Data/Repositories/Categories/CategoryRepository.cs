using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.APICatalog.Data.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;
        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryDAO.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync()
        {
            return await _categoryDAO.GetAllCategoriesWithProductsAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryDAO.GetCategoryByIdAsync(id);
            return category ?? throw new InvalidOperationException($"Category not found.");
        }

        public async Task<Category> InsertCategoryAsync(Category category)
        {
            return await _categoryDAO.InsertCategoryAsync(category);
        }


        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var updatedCategory = await _categoryDAO.UpdateCategoryAsync(id, category);
            return updatedCategory ?? throw new InvalidOperationException($"Category not found.");
        }
        public async Task<bool> RemoveCategoryAsync(int id)
        {
            return await _categoryDAO.RemoveCategoryAsync(id);
        }
    }
}
