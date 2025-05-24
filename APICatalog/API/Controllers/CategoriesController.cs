using APICatalog.API.DTOs;
using APICatalog.API.DTOs.Common;
using APICatalog.API.DTOs.Common.Mapping;
using APICatalog.API.DTOs.Mapping;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Common.Pagination;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Context;
using Microsoft.AspNetCore.Mvc;
using APICatalog.Core.Common;

namespace APICatalog.APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IUserService _userService;

        public CategoriesController(IDbTransaction dbTransaction, IUserService userService)
        {
            _dbTransaction = dbTransaction;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponseDTO<CategoryDTO>>> GetCategoriesPaged([FromQuery] PaginationParams paginationParams)
        {
            var categories = await _dbTransaction.CategoryRepository.GetCategoriesPaged(paginationParams);
            if (categories is null)
            {
                return NotFound("Categories not found...");
            }
            var categoriesDTO = categories.MapToCategoryDTOList();
            var pagedCategoriesDTO = new PagedList<CategoryDTO>(
                categoriesDTO.ToList(),
                categories.TotalCount,
                categories.CurrentPage,
                categories.PageSize
                ).MapToPagedResponseDTO();
            return Ok(pagedCategoriesDTO);
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
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _dbTransaction.CategoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found...");
            }

            var categoryDTO = category.MapToCategoryDTO();

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> InsertCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category data is invalid.");
            }

            var category = categoryDTO.MapToCategory();

            var newCategory = await _dbTransaction.CategoryRepository.InsertCategoryAsync(category);
            _dbTransaction.Commit();

            if (newCategory == null)
            {
                return BadRequest("Category could not be created.");
            }

            var newCategoryDTO = newCategory.MapToCategoryDTO();


            return CreatedAtRoute("GetCategoryById", new { id = newCategoryDTO.CategoryId }, newCategoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category data is invalid.");
            }

            var category = categoryDTO.MapToCategory();

            var updatedCategory = await _dbTransaction.CategoryRepository.UpdateCategoryAsync(id, category);
            _dbTransaction.Commit();

            var updatedCategoryDTO = updatedCategory.MapToCategoryDTO();

            return Ok(updatedCategoryDTO);
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