using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Data.Context;
using APICatalog.Data.Repositories.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var products = await _dbTransaction.ProductRepository.GetAllProductsAsync();

            if (products.Count() < 1)
            {
                return NotFound("Products not found...");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _dbTransaction.ProductRepository.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> InsertProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalided Product...");
            }

            var insertProduct = await _dbTransaction.ProductRepository.InsertProductAsync(product);
            _dbTransaction.Commit();

            if (insertProduct == null)
            {
                return BadRequest("Product could not be created.");
            }

            return CreatedAtRoute("GetProductById", new { id = insertProduct.ProductId }, insertProduct);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var updatedProduct = await _dbTransaction.ProductRepository.UpdateProductAsync(id, product);
            _dbTransaction.Commit();

            return Ok(updatedProduct);
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
