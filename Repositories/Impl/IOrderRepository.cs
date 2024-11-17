using Vehicle_service.Models;

namespace Vehicle_service.Repositories.Impl
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Order order);
        
        public Task<Order> UpdateOrder(Order order);
        
        public Task DeleteOrder(int orderId);
        
        public Task<List<Order>> GetAllOrders();
        
        public Task<Order> GetOrderById(int orderId);
    }
}