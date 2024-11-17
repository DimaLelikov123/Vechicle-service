using AutoMapper;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Models;
using Vehicle_service.Repositories.Impl;

namespace Vehicle_service.Services
{
  public class OrderService(IOrderRepository orderRepository, IMapper mapper)
  {
    public async Task<OrderDto> GetOrderAsync(int id)
    {
      var foundOrder = await orderRepository.GetOrderById(id);
      return mapper.Map<OrderDto>(foundOrder);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
      var orders = await orderRepository.GetAllOrders();
      return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task DeleteOrderAsync(int id)
    {
      await orderRepository.GetOrderById(id);
      await orderRepository.DeleteOrder(id);
    }

    public async Task<OrderUpdateDto> UpdateOrderAsync(int orderId, OrderUpdateDto updateDto)
    {
      var existingOrder = await orderRepository.GetOrderById(orderId);
      mapper.Map(updateDto, existingOrder);
      var updatedOrder = await orderRepository.UpdateOrder(existingOrder);
      return mapper.Map<OrderUpdateDto>(updatedOrder);
    }

    public async Task<OrderDto> AddOrderAsync(OrderCreateDto createDto)
    {
      var order = mapper.Map<Order>(createDto);
      var createdOrder = await orderRepository.CreateOrder(order);
      return mapper.Map<OrderDto>(createdOrder);
    }
  }
}