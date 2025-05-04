using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.APICatalog.Data.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryProcedures _categoryProcedures;
        public CategoryRepository(CategoryProcedures categoryProcedures)
        {
            _categoryProcedures = categoryProcedures;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryProcedures.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync()
        {
            return await _categoryProcedures.GetAllCategoriesWithProductsAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryProcedures.GetCategoryByIdAsync(id);
            return category ?? throw new InvalidOperationException($"Category not found.");
        }

        public async Task<Category> InsertCategoryAsync(Category category)
        {
            return await _categoryProcedures.InsertCategoryAsync(category);
        }


        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var updatedCategory = await _categoryProcedures.UpdateCategoryAsync(id, category);
            return updatedCategory ?? throw new InvalidOperationException($"Category not found.");
        }
        public async Task<bool> RemoveCategoryAsync(int id)
        {
            return await _categoryProcedures.RemoveCategoryAsync(id);
        }
    }
}
