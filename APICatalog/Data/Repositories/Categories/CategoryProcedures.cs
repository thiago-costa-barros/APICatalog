using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.APICatalog.Data.Repositories.Categories
{
    public class CategoryProcedures
    {
        private readonly AppDbContext _context;
        public CategoryProcedures(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.Products.Where(p => p.DeletionDate == null))
                .Where(c => c.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id && c.DeletionDate == null);
            if (category == null)
            {
                return null;
            }
            return await category;
        }

        public async Task<Category> InsertCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            category.CategoryId = id;

            var existingCategory = await GetCategoryByIdAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.CategoryName = category.CategoryName ?? existingCategory.CategoryName;
            existingCategory.Description = category.Description ?? existingCategory.Description;
            existingCategory.ImageUrl = category.ImageUrl ?? existingCategory.ImageUrl;
            existingCategory.UpdateDate = DateTime.UtcNow;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> RemoveCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            category.DeletionDate = DateTime.UtcNow;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
