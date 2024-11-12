using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Models;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isRemoved = _orderService.DeleteOrder(id);
            if (!isRemoved)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Order order)
        {
            var isUpdated = _orderService.UpdateOrder(order);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            Console.WriteLine(order.Id);
            _orderService.AddOrder(order);
            return Created();
        }
    }
}