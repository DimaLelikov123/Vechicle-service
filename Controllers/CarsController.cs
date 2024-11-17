using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController(CarService carService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetById(int id)
        {
            var car = await carService.GetCarAsync(id);
            return Ok(car);
        }

        [HttpGet]
        public async Task<ActionResult<CarDto>> GetAll()
        {
            var cars = await carService.GetAllAsync();
            return Ok(cars);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await carService.DeleteCarAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarUpdateDto carUpdateDto)
        {
            var updatedCar = await carService.UpdateCarAsync(id, carUpdateDto);
            return Ok(updatedCar);
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> Create([FromBody] CarCreateDto carCreateDto)
        {
            var car = await carService.AddCarAsync(carCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}