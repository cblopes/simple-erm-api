using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Controllers
{
    [Route("api-simple-erp/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _context.Products.Where(p => !p.IsDeleted).ToList();

            return Ok(product);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            _context.Products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Product input)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(input.Description, input.QuantityInStock, input.Price);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _context.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Delete();

            return NoContent();
        }
    }
}
