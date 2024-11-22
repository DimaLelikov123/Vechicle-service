using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.Exceptions;
using Vehicle_service.Models;

namespace Vehicle_service.Repositories.Impl
{
    public class CarRepositoryImpl(VehicleContext context) : ICarRepository
    {
        public async Task<Car> GetCarById(int carId)
        {
            var foundCar = await context.Cars.FindAsync(carId);
            if (foundCar == null)
            {
                throw new CarNotFoundException();
            }

            return foundCar;
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await context.Cars.ToListAsync();
        }

        public async Task DeleteCar(int id)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null)
            {
                throw new CarNotFoundException();
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }

        public async Task<Car> UpdateCar(Car car)
        {
            var foundCar = await context.Cars.FindAsync(car.Id);
            if (foundCar == null)
            {
                throw new CarNotFoundException();
            }

            context.Cars.Update(car);
            await context.SaveChangesAsync();
            return car;
        }


        public async Task<Car> CreateCar(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return car;
        }
    }
}