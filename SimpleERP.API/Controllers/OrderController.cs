using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;

        public OrderController(IMapper mapper, IOrderServices orderServices)
        {
            _mapper = mapper;
            _orderServices = orderServices;
        }

        /// <summary>
        /// Obter todos os pedidos
        /// </summary>
        /// <returns>Coleção de pedidos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderServices.GetAllOrdersAsync();

                var orderViewModel = _mapper.ProjectTo<OrderViewModel>(orders.AsQueryable()).ToList();

                return Ok(orderViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obter um pedidopor por Id
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Dados do pedido</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderServices.GetOrderByIdAsync(id);

                var orderViewModel = _mapper.Map<OrderViewModel>(order);

                return Ok(orderViewModel);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
            
        }

        /// <summary>
        /// Abrir um pedido
        /// </summary>
        /// <param name="input">Identificador do cliente</param>
        /// <returns>Pedido criado</returns>
        /// <reponse code="201">Sucesso</reponse>
        /// <reponse code="400">Má requisição</reponse>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var order = _mapper.Map<Order>(input);

                await _orderServices.CreateOrderAsync(order);

                var orderViewModel = _mapper.Map<OrderViewModel>(order);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderViewModel.Id } , orderViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Fechar/Finalizar um pedido
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Sem retorno</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPatch("{id}/finish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FinishOrder(Guid id)
        {
            try
            {
                await _orderServices.FinishOrderAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Cancelar um pedido
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Sem retorno</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                await _orderServices.CancelOrderAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItem(Guid orderId, CreateOrderItemViewModel input)
        {
            try
            {
                var item = _mapper.Map<OrderItem>(input);
                await _orderServices.AddItemAsync(orderId, item);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderId} );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{orderId}/items/{itemId}")]
        public async Task<IActionResult> AlterItem(Guid orderId, Guid itemId, AlterOrderItemViewModel input)
        {
            try
            {
                var item = _mapper.Map<OrderItem>(input);
                await _orderServices.AlterQuantityItemAsync(orderId, itemId, item);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid orderId, Guid itemId)
        {
            try
            {
                await _orderServices.DeleteItemAsync(orderId, itemId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
