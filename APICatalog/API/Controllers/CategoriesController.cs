using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDbTransaction _dbTransaction;

        public CategoriesController(IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _dbTransaction.CategoryRepository.GetAllCategoriesAsync();
            if (categories is null)
            {
                return NotFound("Categories not found...");
            }
            return Ok(categories);
        }

        [HttpGet("products")]
        public async Task<ActionResult<Category>> GetAllCategoriesWithProducts()
        {
            var categories = await _dbTransaction.CategoryRepository.GetAllCategoriesWithProductsAsync();
            if (categories.Count() < 1)
            {
                return NotFound("Category not found...");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _dbTransaction.CategoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found...");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> InsertCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is invalid.");
            }

            var newCategory = await _dbTransaction.CategoryRepository.InsertCategoryAsync(category);
            _dbTransaction.Commit();

            if (newCategory == null)
            {
                return BadRequest("Category could not be created.");
            }
            return CreatedAtRoute("GetCategoryById", new { id = newCategory.CategoryId }, newCategory);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is invalid.");
            }

            var updatedCategory = await _dbTransaction.CategoryRepository.UpdateCategoryAsync(id, category);
            _dbTransaction.Commit();

            return Ok(updatedCategory);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoveCategory(int id)
        {
            var removedCategory = await _dbTransaction.CategoryRepository.RemoveCategoryAsync(id);
            _dbTransaction.Commit();

            return NoContent();
        }
    }
}