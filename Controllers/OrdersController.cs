using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController(OrderService orderService) : ControllerBase
  {
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
      var order = await orderService.GetOrderAsync(id);
      return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<OrderDto>> GetAll()
    {
      var orders = await orderService.GetAllAsync();
      return Ok(orders);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
      await orderService.DeleteOrderAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto orderUpdateDto, [FromServices] IValidator<OrderUpdateDto> validator)
    {
      var validationResult = await validator.ValidateAsync(orderUpdateDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(validationResult.ToDictionary());
      }
      
      var updatedOrder = await orderService.UpdateOrderAsync(id, orderUpdateDto);
      return Ok(updatedOrder);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto orderCreateDto, [FromServices] IValidator<OrderCreateDto> validator)
    {
      var validationResult = await validator.ValidateAsync(orderCreateDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(validationResult.ToDictionary());
      }
      
      var order = await orderService.AddOrderAsync(orderCreateDto);
      return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
  }
}