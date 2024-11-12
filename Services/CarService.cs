using Vehicle_service.Models;

namespace Vehicle_service.Services
{
    public class CarService
    {
        private List<Car> _cars;

        public CarService()
        {
            _cars = new List<Car>();
        }

        public List<Car> GetAll() 
        {
            return _cars;
        }

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

        public Car GetCar(int id)
        {
            return _cars.Find(p => p.Id == id);
        }

        public bool UpdateCar(Car car)
        {
            var toUpdate = _cars.Find(p => p.Id == car.Id);
            if (toUpdate != null)
            {
                toUpdate.Name = car.Name;
                toUpdate.Model = car.Model;
                return true;
            }
            return false;
        }

        public bool DeleteCar(int id)
        {
            var toDelete = _cars.Find(p => p.Id == id);
            if (toDelete != null)
            {
                _cars.Remove(toDelete);
                return true;
            }
            return false;
        }
    }
}
