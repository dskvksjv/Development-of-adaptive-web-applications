using Microsoft.AspNetCore.Mvc;
using project7.Model;
using project7.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(new ResponseModel<List<Order>> { Data = orders, StatusCode = 200, Message = "Success" });
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            var newOrder = await _orderService.AddOrderAsync(order);
            return Ok(new ResponseModel<Order> { Data = newOrder, StatusCode = 200, Message = "Order added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, order);
            if (updatedOrder == null)
            {
                return NotFound(new ResponseModel<Order> { StatusCode = 404, Message = "Order not found" });
            }
            return Ok(new ResponseModel<Order> { Data = updatedOrder, StatusCode = 200, Message = "Order updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound(new ResponseModel<object> { StatusCode = 404, Message = "Order not found" });
            }
            return Ok(new ResponseModel<object> { StatusCode = 200, Message = "Order deleted successfully" });
        }
    }
}
