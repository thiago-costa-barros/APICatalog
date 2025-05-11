using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Core.Common.Pagination;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Data.Repositories.DAOs
{
    public class CategoryDAO
    {
        private readonly AppDbContext _context;
        public CategoryDAO(AppDbContext context)
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

        public async Task<PagedList<Category>> GetCategoriesPaged(PaginationParams paginationParams)
        {
            var categories = await _context.Categories
                .FromSqlRaw(
                """
                EXEC
                    GetCategoriesPaged 
                    @PageNumber, 
                    @PageSize
                """,
                    new SqlParameter("@PageNumber", paginationParams.PageNumber),
                    new SqlParameter("@PageSize", paginationParams.PageSize))
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await _context.Categories
                .Where(c => c.DeletionDate == null)
                .CountAsync();

            var categoriesPaged = new PagedList<Category>(
                categories,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );

            return categoriesPaged;
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
            return true;
        }
    }
}
