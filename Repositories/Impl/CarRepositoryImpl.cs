using Vehicle_service.Data;
using Vehicle_service.Models;

namespace Vehicle_service.Repositories.Impl
{
    public class CarRepositoryImpl : ICarRepository
    {
        private readonly VehicleContext _context;
    
        public CarRepositoryImpl(VehicleContext context)
        {
            _context = context;
        }
    
        public async Task<Car> createCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }
    }
}