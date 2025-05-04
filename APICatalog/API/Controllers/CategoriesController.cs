using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICatalog.Data.Repositories.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await _repository.GetAllCategoriesAsync();
                if (categories is null)
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
                var categories = await _repository.GetAllCategoriesWithProductsAsync();
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
                var category = await _repository.GetCategoryByIdAsync(id);
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
        public async Task<ActionResult<Category>> InsertCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is invalid.");
                }

                var newCategory = await _repository.InsertCategoryAsync(category);

                if (newCategory == null)
                {
                    return BadRequest("Category could not be created.");
                }
                return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is invalid.");
                }

                var updateCategory = await _repository.UpdateCategoryAsync(id, category);

                return Ok(updateCategory);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoveCategory(int id)
        {
            try
            {
                var removedCategory = await _repository.RemoveCategoryAsync(id);

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
