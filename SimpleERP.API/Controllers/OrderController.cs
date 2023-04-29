using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderServices.GetAllOrdersAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderServices.GetOrderByIdAsync(id);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var order = _mapper.Map<Order>(model);

                await _orderServices.CreateOrderAsync(order);

                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id } , order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
