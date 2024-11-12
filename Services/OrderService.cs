using Vehicle_service.Models;

namespace Vehicle_service.Services
{
    public class OrderService
    {
        private readonly List<Order> _orders;

        public OrderService()
        {
            _orders = new List<Order>();
        }

        public List<Order> GetAll()
        {
            return _orders;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrder(int id)
        {
            return _orders.Find(p => p.Id == id);
        }

        public bool UpdateOrder(Order order)
        {
            var toUpdate = _orders.Find(p => p.Id == order.Id);
            if (toUpdate != null)
            {
                toUpdate.Price = order.Price;
                toUpdate.CarId = order.CarId;
                return true;
            }
            return false;
        }

        public bool DeleteOrder(int id)
        {
            var toDelete = _orders.Find(p => p.Id == id);
            if (toDelete != null)
            {
                _orders.Remove(toDelete);
                return true;
            }
            return false;
        }
    }
}
