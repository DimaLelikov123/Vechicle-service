using AutoMapper;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Models;
using Vehicle_service.Repositories.Impl;

namespace Vehicle_service.Services
{
  public class OrderService(IOrderRepository orderRepository, IMapper mapper, ILoggerFactory loggerFactory)
  {
    public async Task<OrderDto> GetOrderAsync(int id)
    {
      var foundOrder = await orderRepository.GetOrderById(id);
      loggerFactory.CreateLogger<OrderService>().LogInformation($"GetOrderAsync called with id: {id}");
      return mapper.Map<OrderDto>(foundOrder);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
      var orders = await orderRepository.GetAllOrders();
      loggerFactory.CreateLogger<OrderService>().LogInformation($"GetAllAsync called");
      return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task DeleteOrderAsync(int id)
    {
      var orderToDelete = await orderRepository.GetOrderById(id);
      await orderRepository.DeleteOrder(id);
      loggerFactory.CreateLogger<OrderService>().LogInformation($"DeleteOrderAsync called with id: {orderToDelete.Id}");
    }

    public async Task<OrderUpdateDto> UpdateOrderAsync(int orderId, OrderUpdateDto updateDto)
    {
      var existingOrder = await orderRepository.GetOrderById(orderId);
      mapper.Map(updateDto, existingOrder);
      var updatedOrder = await orderRepository.UpdateOrder(existingOrder);
      loggerFactory.CreateLogger<OrderService>().LogInformation($"UpdateOrderAsync called with id: {orderId}");
      return mapper.Map<OrderUpdateDto>(updatedOrder);
    }

    public async Task<OrderDto> AddOrderAsync(OrderCreateDto createDto)
    {
      var order = mapper.Map<Order>(createDto);
      var createdOrder = await orderRepository.CreateOrder(order);
      loggerFactory.CreateLogger<OrderService>().LogInformation($"AddOrderAsync called with id: {createdOrder.Id}");
      return mapper.Map<OrderDto>(createdOrder);
    }
  }
}