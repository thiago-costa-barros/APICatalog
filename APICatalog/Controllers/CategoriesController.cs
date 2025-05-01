using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = _context.Categories
                .Where(c => c.DeletionDate == null)
                .ToList();
            if (categories.Count() < 1)
            {
                return NotFound("Category not found...");
            }
            return categories;
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.CategoryId == id && c.DeletionDate == null);
            if (category == null)
            {
                return NotFound("Category not found...");
            }
            return category;
        }

        [HttpPost]
        public ActionResult<Category> InsertCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is invalid.");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> UpdateCategory(int id, [FromBody] Category category)
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

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategory(int id) 
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
    }
}
