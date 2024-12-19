using AutoMapper;
using Microsoft.Extensions.Options;
using Vehicle_service.Config;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;
using Vehicle_service.Repositories;

namespace Vehicle_service.Services
{
  public class CarService(
    ICarRepository carRepository,
    IMapper mapper,
    ILogger<CarService> logger,
    IOptions<CarsConfig> config
  )
  {
    private readonly CarsConfig _config = config.Value;

    public async Task<CarDto> GetCarAsync(int id)
    {
      var foundCar = await carRepository.GetCarById(id);
      logger.LogInformation($"Car ID: {id} is found");
      return mapper.Map<CarDto>(foundCar);
    }

    public async Task<List<CarDto>> GetAllAsync()
    {
      if (!_config.EnableGetAll)
      {
        logger.LogWarning("GetAll is disabled by configuration.");
        return new List<CarDto>();
      }

      logger.LogInformation("Fetching all cars (limit: {Limit})", _config.MaxCarsLimit);

      var cars = await carRepository.GetAllCars();
      var limitedCars = cars.Take(_config.MaxCarsLimit).ToList();

      return mapper.Map<List<CarDto>>(limitedCars);
    }

    public async Task DeleteCarAsync(int id)
    {
      var carToDelete = await carRepository.GetCarById(id);
      await carRepository.DeleteCar(id);
      logger.LogInformation($"Car ID: {carToDelete.Id} is deleted");
    }

    public async Task<CarUpdateDto> UpdateCarAsync(int carId, CarUpdateDto updateDto)
    {
      var existingCar = await carRepository.GetCarById(carId);
      mapper.Map(updateDto, existingCar);
      var updatedCar = await carRepository.UpdateCar(existingCar);
      logger.LogInformation($"Car ID: {updatedCar.Id} is updated");
      return mapper.Map<CarUpdateDto>(updatedCar);
    }

    public async Task<CarDto> AddCarAsync(CarCreateDto createDto)
    {
      if (!_config.AllowCreate)
      {
        throw new ApplicationException("AllowCreate is disabled by configuration.");
      }

      var car = mapper.Map<Car>(createDto);
      var createdCar = await carRepository.CreateCar(car);
      logger.LogInformation($"Car ID: {createdCar.Id} is created");
      return mapper.Map<CarDto>(createdCar);
    }
  }
}