using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.Exceptions;
using Vehicle_service.Models;

namespace Vehicle_service.Repositories.Impl
{
    public class OrderRepositoryImpl(VehicleContext context) : IOrderRepository
    {
        public async Task<Order> GetOrderById(int orderId)
        {
            var foundOrder = await context.Orders.FindAsync(orderId);
            if (foundOrder == null)
            {
                throw new OrderNotFoundException();
            }

            return foundOrder;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var foundOrder = await context.Orders.FindAsync(order.Id);
            if (foundOrder == null)
            {
                throw new OrderNotFoundException();
            }

            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }


        public async Task<Order> CreateOrder(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return order;
        }
    }
}