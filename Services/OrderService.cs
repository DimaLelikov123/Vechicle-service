using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.Models;

namespace Vehicle_service.Services
{
  public class OrderService
  {
    private readonly VehicleContext _context;

    public OrderService(VehicleContext context)
    {
      _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
      return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
      return await _context.Orders.FindAsync(id);
    }

    public async Task AddOrderAsync(Order order)
    {
      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
      var existingOrder = await _context.Orders.FindAsync(order.Id);
      if (existingOrder == null)
      {
        return false;
      }

      existingOrder.Price = order.Price;
      existingOrder.CarId = order.CarId;

      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
      var order = await _context.Orders.FindAsync(id);
      if (order == null)
      {
        return false;
      }

      _context.Orders.Remove(order);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}