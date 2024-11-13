using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Models;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
      _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAll()
    {
      var orders = await _orderService.GetAllAsync();
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(int id)
    {
      var order = await _orderService.GetOrderAsync(id);
      if (order != null)
      {
        return Ok(order);
      }

      return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
      await _orderService.AddOrderAsync(order);
      return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
      if (id != order.Id)
      {
        return BadRequest();
      }

      var updated = await _orderService.UpdateOrderAsync(order);
      if (!updated)
      {
        return NotFound();
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var deleted = await _orderService.DeleteOrderAsync(id);
      if (!deleted)
      {
        return NotFound();
      }

      return NoContent();
    }
  }
}