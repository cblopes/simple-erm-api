using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [Route("api/v1/orders")]
    public class OrderController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IClientServices _clientServices;
        private readonly IOrderServices _orderServices;
        private readonly IProductServices _productServices;

        public OrderController(IMapper mapper, IOrderServices orderServices, IClientServices clientServices, IProductServices productServices)
        {
            _mapper = mapper;
            _clientServices = clientServices;
            _orderServices = orderServices;
            _productServices = productServices;
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

                var ordersViewModel = _mapper.ProjectTo<AllOrdersViewModel>(orders.AsQueryable()).ToList();

                return Ok(ordersViewModel);
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Obter um pedido por por Id
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
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
            
        }

        /// <summary>
        /// Abrir um pedido
        /// </summary>
        /// <param name="document">CPF/CNPJ do cliente</param>
        /// <returns>Pedido criado</returns>
        /// <reponse code="201">Sucesso</reponse>
        /// <reponse code="400">Má requisição</reponse>
        [HttpPost("{document}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(string document)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var client = await _clientServices.GetClientByDocumentAsync(document);

                Guid clientId = client.Id;

                var order = new Order { ClientId = clientId };

                await _orderServices.CreateOrderAsync(order);

                var orderViewModel = _mapper.Map<OrderViewModel>(order);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderViewModel.Id } , orderViewModel);
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Fechar/Finalizar um pedido
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Sem retorno</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPatch("{id}/finish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinishOrder(Guid id)
        {
            try
            {
                await _orderServices.FinishOrderAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Cancelar um pedido
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Sem retorno</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                await _orderServices.CancelOrderAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Adicionar item ao pedido
        /// </summary>
        /// <param name="orderId">Identificador do pedido</param>
        /// <param name="input">Dados do item de pedido</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPost("{orderId}/items")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItem(Guid orderId, CreateOrderItemModel input)
        {
            try
            {
                var product = await _productServices.GetProductByCodeAsync(input.Code);

                OrderItem orderItem = new OrderItem 
                { 
                    ProductId = product.Id, 
                    Quantity = input.Quantity 
                };

                var item = _mapper.Map<OrderItem>(orderItem);
                await _orderServices.AddItemAsync(orderId, item);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Alterar a quantidade de um item do pedido
        /// </summary>
        /// <param name="orderId">Identificador do pedido</param>
        /// <param name="itemId">Identificador do item</param>
        /// <param name="input">Quantidade a ser alterada</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPatch("{orderId}/items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterItem(Guid orderId, Guid itemId, AlterOrderItemModel input)
        {
            try
            {
                var item = _mapper.Map<OrderItem>(input);
                await _orderServices.AlterQuantityItemAsync(orderId, itemId, item);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Remover um item do pedido
        /// </summary>
        /// <param name="orderId">Identificador do pedido</param>
        /// <param name="itemId">Identificador do item</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpDelete("{orderId}/items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteItem(Guid orderId, Guid itemId)
        {
            try
            {
                await _orderServices.DeleteItemAsync(orderId, itemId);

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
