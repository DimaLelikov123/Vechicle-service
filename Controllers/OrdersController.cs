using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMemoryCache _cache;

        public OrdersController(OrderService orderService, IMemoryCache cache)
        {
            _orderService = orderService;
            _cache = cache;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            string cacheKey = $"Order_{id}";
            
            if (!_cache.TryGetValue(cacheKey, out OrderDto order))
            {
                order = await _orderService.GetOrderAsync(id);
                if (order == null)
                    return NotFound(new { Message = $"Order with ID {id} not found." });
                
                _cache.Set(cacheKey, order, TimeSpan.FromMinutes(10));
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            string cacheKey = "AllOrders";
            
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<OrderDto> orders))
            {
                orders = await _orderService.GetAllAsync();
                
                _cache.Set(cacheKey, orders, TimeSpan.FromMinutes(5));
            }

            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            
            string cacheKey = $"Order_{id}";
            _cache.Remove(cacheKey);
            
            _cache.Remove("AllOrders");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] OrderUpdateDto orderUpdateDto,
            [FromServices] IValidator<OrderUpdateDto> validator)
        {
            var validationResult = await validator.ValidateAsync(orderUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderUpdateDto);
            
            string cacheKey = $"Order_{id}";
            _cache.Set(cacheKey, updatedOrder, TimeSpan.FromMinutes(10));
            _cache.Remove("AllOrders");

            return Ok(updatedOrder);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(
            [FromBody] OrderCreateDto orderCreateDto,
            [FromServices] IValidator<OrderCreateDto> validator)
        {
            var validationResult = await validator.ValidateAsync(orderCreateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var order = await _orderService.AddOrderAsync(orderCreateDto);
            
            _cache.Remove("AllOrders");

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
    }
}
