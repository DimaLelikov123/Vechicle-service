using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly IMemoryCache _cache;

        public CarsController(CarService carService, IMemoryCache cache)
        {
            _carService = carService;
            _cache = cache;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetById(int id)
        {
            string cacheKey = $"Car_{id}";
            
            if (!_cache.TryGetValue(cacheKey, out CarDto car))
            {
                car = await _carService.GetCarAsync(id);
                if (car == null)
                    return NotFound(new { Message = $"Car with ID {id} not found." });
                
                _cache.Set(cacheKey, car, TimeSpan.FromMinutes(10));
            }

            return Ok(car);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
        {
            string cacheKey = "AllCars";
            
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<CarDto> cars))
            {
                cars = await _carService.GetAllAsync();
                
                _cache.Set(cacheKey, cars, TimeSpan.FromMinutes(5));
            }

            return Ok(cars);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.DeleteCarAsync(id);
            
            string cacheKey = $"Car_{id}";
            _cache.Remove(cacheKey);
            
            _cache.Remove("AllCars");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] CarUpdateDto carUpdateDto, 
            [FromServices] IValidator<CarUpdateDto> validator)
        {
            var validationResult = await validator.ValidateAsync(carUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var updatedCar = await _carService.UpdateCarAsync(id, carUpdateDto);
            
            string cacheKey = $"Car_{id}";
            _cache.Set(cacheKey, updatedCar, TimeSpan.FromMinutes(10));
            _cache.Remove("AllCars"); 

            return Ok(updatedCar);
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> Create(
            [FromBody] CarCreateDto carCreateDto, 
            [FromServices] IValidator<CarCreateDto> validator)
        {
            var validationResult = await validator.ValidateAsync(carCreateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var car = await _carService.AddCarAsync(carCreateDto);
            
            _cache.Remove("AllCars");

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}