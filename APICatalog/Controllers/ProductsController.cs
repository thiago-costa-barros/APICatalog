using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.DeletionDate == null)
                    .AsNoTracking()
                    .ToListAsync();

                if (!products.Any())
                {
                    return NotFound("Product not found...");
                }
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.DeletionDate == null);
                if (product == null)
                {
                    return NotFound("Product not found...");
                }
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<Product> InsertProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalided Product...");
                }
                _context.Products.Add(product);
                _context.SaveChanges();
                return CreatedAtRoute("GetProductById", new { id = product.ProductId }, product);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is invalid.");
                }

                product.ProductId = id;

                var existingProduct = _context.Products
                    .FirstOrDefault(p => p.ProductId == id && p.DeletionDate == null);

                if (existingProduct == null)
                {
                    return NotFound("Product not found...");
                }

                existingProduct.ProductName = product.ProductName ?? existingProduct.ProductName;
                existingProduct.Description = product.Description ?? existingProduct.Description;
                existingProduct.ImageUrl = product.ImageUrl ?? existingProduct.ImageUrl;
                existingProduct.Price = product.Price != 0 ? product.Price : existingProduct.Price;
                existingProduct.CategoryId = product.CategoryId ?? existingProduct.CategoryId;
                existingProduct.UpdateDate = DateTime.UtcNow;

                _context.Products.Update(existingProduct);
                _context.SaveChanges();

                return Ok(existingProduct);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult RemoveProduct(int id)
        {
            try
            {
                var existingProduct = _context.Products
                    .FirstOrDefault(p => p.ProductId == id && p.DeletionDate == null);

                if (existingProduct == null)
                {
                    return NotFound("Product not found...");
                }

                existingProduct.DeletionDate = DateTime.UtcNow;
                existingProduct.UpdateDate = DateTime.UtcNow;

                _context.Products.Update(existingProduct);
                _context.SaveChanges();

                return Ok($"Product '{existingProduct.ProductName}' was deleted successfully...");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
