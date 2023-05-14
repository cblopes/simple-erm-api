using Microsoft.AspNetCore.Mvc;
using SimpleERP.MVC.Services;

namespace SimpleERP.MVC.Controllers
{
    public class OrderController : MainController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            return View(order);
        }
    }
}
