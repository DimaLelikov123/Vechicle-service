using AutoMapper;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;
using Vehicle_service.Repositories;

namespace Vehicle_service.Services
{
    public class CarService(ICarRepository carRepository, IMapper mapper)
    {
        public async Task<CarDto> GetCarAsync(int id)
        {
            var foundCar = await carRepository.GetCarById(id);
            return mapper.Map<CarDto>(foundCar);
        }

        public async Task<List<CarDto>> GetAllAsync()
        {
            var cars = await carRepository.GetAllCars();
            return mapper.Map<List<CarDto>>(cars);
        }

        public async Task DeleteCarAsync(int id)
        {
            await carRepository.GetCarById(id);
            await carRepository.DeleteCar(id);
        }

        public async Task<CarUpdateDto> UpdateCarAsync(int carId, CarUpdateDto updateDto)
        {
            var existingCar = await carRepository.GetCarById(carId);
            mapper.Map(updateDto, existingCar);
            var updatedCar = await carRepository.UpdateCar(existingCar);
            return mapper.Map<CarUpdateDto>(updatedCar);
        }

        public async Task<CarDto> AddCarAsync(CarCreateDto createDto)
        {
            var car = mapper.Map<Car>(createDto);
            var createdCar = await carRepository.CreateCar(car);
            return mapper.Map<CarDto>(createdCar);
        }
    }
}