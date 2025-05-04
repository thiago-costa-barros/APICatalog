using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories
                .Where(c => c.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();
                if (!categories.Any())
                {
                    return NotFound("Category not found...");
                }
                return Ok(categories);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("products")]
        public async Task<ActionResult<Category>> GetAllCategoriesWithProducts()
        {
            try
            {
                var categories = await _context.Categories
                .Include(c => c.Products.Where(p => p.DeletionDate == null))
                .Where(c => c.DeletionDate == null)
                .AsNoTracking()
                .ToListAsync();
                if (categories.Count() < 1)
                {
                    return NotFound("Category not found...");
                }
                return Ok(categories);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id && c.DeletionDate == null);
                if (category == null)
                {
                    return NotFound("Category not found...");
                }
                return Ok(category);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<Category> InsertCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is invalid.");
                }
                _context.Categories.Add(category);
                _context.SaveChanges();
                return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is invalid.");
                }

                category.CategoryId = id;

                var existingCategory = _context.Categories
                    .FirstOrDefault(c => c.CategoryId == id && c.DeletionDate == null);
                if (existingCategory == null)
                {
                    return NotFound("Category not found...");
                }

                existingCategory.CategoryName = category.CategoryName ?? existingCategory.CategoryName;
                existingCategory.Description = category.Description ?? existingCategory.Description;
                existingCategory.ImageUrl = category.ImageUrl ?? existingCategory.ImageUrl;
                existingCategory.UpdateDate = DateTime.UtcNow;

                _context.Categories.Update(existingCategory);
                _context.SaveChanges();

                return Ok(existingCategory);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                var existingCategory = _context.Categories
                .FirstOrDefault(c => c.CategoryId == id && c.DeletionDate == null);

                if (existingCategory == null)
                {
                    return NotFound("Category not found...");
                }

                existingCategory.DeletionDate = DateTime.UtcNow;
                existingCategory.UpdateDate = DateTime.UtcNow;

                _context.Categories.Update(existingCategory);
                _context.SaveChanges();

                return Ok($"Category {existingCategory.CategoryName} deleted successfully.");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
