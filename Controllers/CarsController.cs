using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_service.Models;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isRemoved = await _carService.DeleteCarAsync(id);
            if (!isRemoved)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            var isUpdated = await _carService.UpdateCarAsync(car);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Car car)
        {
            await _carService.AddCarAsync(car);
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}
