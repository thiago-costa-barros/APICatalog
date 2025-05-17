using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Data.Repositories.DAOs;
using System.Net.NetworkInformation;

namespace APICatalog.API.DTOs.Mapping
{
    public static class ProductDTOMappingExtensions
    {
        public static ProductDTO? MapToProductDTO(this Product product)
        {
            if (product is null) return null;
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId ?? 0
            };
        }

        public static Product? MapToProduct(this ProductDTO productDTO)
        {
            if (productDTO is null) return null;
            return new Product
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId
            };
        }

        public static IEnumerable<ProductDTO> MapToProductDTOList(this IEnumerable<Product> products)
        {
            if (products is null || !products.Any()) return new List<ProductDTO>();

            return products.Select(product => new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId ?? 0
            }).ToList();
        }
    }
}
