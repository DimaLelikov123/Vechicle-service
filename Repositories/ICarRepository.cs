using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Models;

namespace Vehicle_service.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> CreateCar(Car car);
        
        public Task<Car> UpdateCar(Car car);
        
        public Task DeleteCar(int carId);
        
        public Task<List<Car>> GetAllCars();
        
        public Task<Car> GetCarById(int carId);
    }
}