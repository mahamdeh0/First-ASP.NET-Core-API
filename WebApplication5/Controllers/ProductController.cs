using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using System.Threading.Tasks;
using WebApplication5.Filter;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogSensitiveActionAttribute]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbcontext _dbContext;

        public ProductController(ApplicationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            product.Id = 0; 
            _dbContext.Set<Product>().Add(product);
            await _dbContext.SaveChangesAsync();

            return Ok(product.Id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            var existingProduct = await _dbContext.Set<Product>().FindAsync(product.Id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {product.Id} not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.sku = product.sku;
            _dbContext.Set<Product>().Update(existingProduct);
            await _dbContext.SaveChangesAsync();

            return Ok("Product updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _dbContext.Set<Product>().FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _dbContext.Set<Product>().Remove(product);
            await _dbContext.SaveChangesAsync();

            return Ok($"Product with ID {id} deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _dbContext.Set<Product>().FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }
    }
}
