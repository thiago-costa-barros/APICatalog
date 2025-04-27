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
                .FirstOrDefault(p => p.ProductId == id);
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
    }
}
