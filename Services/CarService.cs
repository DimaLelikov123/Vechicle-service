using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.Models;

namespace Vehicle_service.Services
{
  public class CarService
  {
    private readonly VehicleContext _context; // 1. створюєш змінну для збереження контексту бази

    public CarService(VehicleContext context)
    {
      _context = context; // передаєш контекст бази в змінну
    }

    public async Task<List<Car>> GetAllAsync()
    {
      return await _context.Cars.ToListAsync(); // використовуєш контекст ( підключення до бд ) для CRUD методів
    }

    public async Task<Car?> GetCarAsync(int id)
    {
      return await _context.Cars.FindAsync(id);
    }

    public async Task AddCarAsync(Car car)
    {
      _context.Cars.Add(car);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateCarAsync(Car car)
    {
      var existingCar = await _context.Cars.FindAsync(car.Id);
      if (existingCar == null)
      {
        return false;
      }

      existingCar.Name = car.Name;
      existingCar.Model = car.Model;

      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
      var car = await _context.Cars.FindAsync(id);
      if (car == null)
      {
        return false;
      }

      _context.Cars.Remove(car);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}