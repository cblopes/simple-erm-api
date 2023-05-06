using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [Route("api/v1/products")]
    public class ProductController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IProductServices _productService;
        public ProductController(IProductServices productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <returns>Coleção de produtos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _productService.GetAllProductsAsync();

                var productViewModel = _mapper.ProjectTo<ProductViewModel>(product.AsQueryable()).ToList();

                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Obter um produto por Id
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Informações de um produto</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                var productViewModel = _mapper.Map<ProductViewModel>(product);

                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
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
        public async Task<IActionResult> Post(CreateProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            try
            {
                var product = _mapper.Map<Product>(model);

                await _productService.CreateProductAsync(product);

                var productViewModel = _mapper.Map<ProductViewModel>(product);

                return CreatedAtAction(nameof(GetById), new { id = productViewModel.Id }, productViewModel);
            }
            catch (ApplicationException ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Atualizar dados de um produto
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <param name="model">Dados do produto</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, AlterProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            try
            {
                var product = _mapper.Map<Product>(model);

                await _productService.UpdateProductAsync(id, product);

                return NoContent();
            }
            catch(Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.RemoveProductAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
            
        }
    }
}
