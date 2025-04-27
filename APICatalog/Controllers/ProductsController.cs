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
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _context.Products
                .Where(p => p.DeletionDate == null)
                .ToList();

            if (products.Count() < 1)
                {
                return NotFound("Product not found...");
                }
            return products;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductId == id && p.DeletionDate == null);
            if (product == null)
            {
                return NotFound("Product not found...");
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> InsertProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalided Product...");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtRoute("GetProductById", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || product.ProductId != id)
            {
                BadRequest("Product data is invalid.");
            }

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

        [HttpDelete("{id:int}")]
        public ActionResult RemoveProduct(int id)
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
    }
}
