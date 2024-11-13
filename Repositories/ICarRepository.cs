using Vehicle_service.Models;

namespace Vehicle_service.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> createCar(Car car);
    }
}