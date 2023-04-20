using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Route("api-simple-erp/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ErpDbContext _context;
        public ProductController(ErpDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _context.Products.Where(p => !p.IsDeleted).ToList();

            var productViewModel = _mapper.ProjectTo<ProductViewModel>(product.AsQueryable()).ToList();

            return Ok(productViewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return Ok(productViewModel);
        }

        [HttpPost]
        public IActionResult Post(CreateProductModel model)
        {
            var product = _mapper.Map<Product>(model);

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateProductModel model)
        {
            var product = _mapper.Map<Product>(model);

            product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(model.Description, model.QuantityInStock, model.Price);

            _context.Update(product);
            _context.SaveChanges();

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
            _context.SaveChanges();

            return NoContent();
        }
    }
}
