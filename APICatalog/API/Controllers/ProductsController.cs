using APICatalog.API.DTOs;
using APICatalog.API.DTOs.Mapping;
using APICatalog.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDbTransaction _dbTransaction;

        public ProductsController(IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _dbTransaction.ProductRepository.GetAllProductsAsync();

            if (products is null)
            {
                return NotFound("Products not found...");
            }

            var productsDTO = products.MapToProductDTOList();

            return Ok(productsDTO);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _dbTransaction.ProductRepository.GetProductByIdAsync(id);

            var productDTO = product.MapToProductDTO();

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> InsertProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalided Product...");
            }

            var product = productDTO.MapToProduct();

            var newProduct = await _dbTransaction.ProductRepository.InsertProductAsync(product);
            _dbTransaction.Commit();

            if (newProduct == null)
            {
                return BadRequest("Product could not be created.");
            }

            var insertProduct = newProduct.MapToProductDTO();

            return CreatedAtRoute("GetProductById", new { id = insertProduct.ProductId }, insertProduct);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var product = productDTO.MapToProduct();

            var updatedProduct = await _dbTransaction.ProductRepository.UpdateProductAsync(id, product);
            _dbTransaction.Commit();

            var updatedProductDTO = updatedProduct.MapToProductDTO();

            return Ok(updatedProductDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            var removedProduct = await _dbTransaction.ProductRepository.RemoveProductAsync(id);
            _dbTransaction.Commit();

            return NoContent();
        }
    }
}
