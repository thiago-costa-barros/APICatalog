using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.API.DTOs.Mapping
{
    public static class CategoryDTOMappingExtensions
    {
        public static CategoryDTO? MapToCategoryDTO(this Category category)
        {
            if (category is null) return null;

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ImageUrl = category.ImageUrl
            };
        }

        public static Category? MapToCategory(this CategoryDTO categoryDTO)
        {
            if (categoryDTO is null) return null;

            return new Category
            {
                CategoryId = categoryDTO.CategoryId,
                CategoryName = categoryDTO.CategoryName,
                Description = categoryDTO.Description,
                ImageUrl = categoryDTO.ImageUrl
            };
        }

        public static IEnumerable<CategoryDTO> MapToCategoryDTOList(this IEnumerable<Category> categories)
        {
            if(categories is null || !categories.Any()) return new List<CategoryDTO>();

            return categories.Select(category => new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ImageUrl = category.ImageUrl
            }).ToList();
        }
    }
}
