using AutoMapper;
using Microsoft.Extensions.Options;
using Vehicle_service.Config;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Models;
using Vehicle_service.Repositories.Impl;

namespace Vehicle_service.Services
{
  public class OrderService(
    IOrderRepository orderRepository,
    IMapper mapper,
    ILogger<OrderService> logger,
    IOptions<OrdersConfig> config
  )
  {
    private OrdersConfig _config = config.Value;

    public async Task<OrderDto> GetOrderAsync(int id)
    {
      var foundOrder = await orderRepository.GetOrderById(id);
      logger.LogInformation($"GetOrderAsync called with id: {id}");
      return mapper.Map<OrderDto>(foundOrder);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
      if (!_config.EnableGetAll)
      {
        logger.LogWarning("GetAll is disabled by configuration.");
        return new List<OrderDto>();
      }

      logger.LogInformation("Fetching all cars (limit: {Limit})", _config.MaxOrdersLimit);

      var orders = await orderRepository.GetAllOrders();
      return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task DeleteOrderAsync(int id)
    {
      var orderToDelete = await orderRepository.GetOrderById(id);
      await orderRepository.DeleteOrder(id);
      logger.LogInformation($"DeleteOrderAsync called with id: {orderToDelete.Id}");
    }

    public async Task<OrderUpdateDto> UpdateOrderAsync(int orderId, OrderUpdateDto updateDto)
    {
      var existingOrder = await orderRepository.GetOrderById(orderId);
      mapper.Map(updateDto, existingOrder);
      var updatedOrder = await orderRepository.UpdateOrder(existingOrder);
      logger.LogInformation($"UpdateOrderAsync called with id: {orderId}");
      return mapper.Map<OrderUpdateDto>(updatedOrder);
    }

    public async Task<OrderDto> AddOrderAsync(OrderCreateDto createDto)
    {
      var order = mapper.Map<Order>(createDto);
      var createdOrder = await orderRepository.CreateOrder(order);
      logger.LogInformation($"AddOrderAsync called with id: {createdOrder.Id}");
      return mapper.Map<OrderDto>(createdOrder);
    }
  }
}