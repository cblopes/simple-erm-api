using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ErpDbContext _context;
        public ProductController(ErpDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <returns>Coleção de produtos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var product = _context.Products.Where(p => !p.IsDeleted).ToList();

            var productViewModel = _mapper.ProjectTo<ProductViewModel>(product.AsQueryable()).ToList();

            return Ok(productViewModel);
        }

        /// <summary>
        /// Obter um produto por Id
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Informações de um produto</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Cadastrar um produto
        /// </summary>
        /// <param name="model">Dados do produto</param>
        /// <returns>Produto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(CreateProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var product = _mapper.Map<Product>(model);

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        /// <summary>
        /// Atualizar dados de um produto
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <param name="model">Dados do produto</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid id, UpdateProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

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

        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
