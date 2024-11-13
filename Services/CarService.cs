using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;
using Vehicle_service.Repositories;
using Vehicle_service.Repositories.Impl;

namespace Vehicle_service.Services
{
  public class CarService
  {
    private readonly VehicleContext _context;
    
    private readonly ICarRepository _repository;
    
    private readonly IMapper _mapper;

    public CarService(VehicleContext context, ICarRepository carRepository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = carRepository;
      _context = context; 
    }

    public async Task<List<Car>> GetAllAsync()
    {
      return await _context.Cars.ToListAsync();
    }

    public async Task<Car?> GetCarAsync(int id)
    {
      return await _context.Cars.FindAsync(id);
    }
    

    public async Task<CarDto> AddCarAsync(CarCreateDto createDto)
    {
      var car = _mapper.Map<Car>(createDto);
      var createdCar = await _repository.createCar(car);
      return _mapper.Map<CarDto>(createdCar);
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