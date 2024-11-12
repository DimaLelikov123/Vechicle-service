using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Models;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
           _carService = carService;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetAll()
        {
            return Ok(_carService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetById(int id)
        {
            var car = _carService.GetCar(id);
            if (car != null) 
            {
                return Ok(car);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isRemoved = _carService.DeleteCar(id);
            if (!isRemoved)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Car car)
        {
            var isUpdated = _carService.UpdateCar(car);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Car car)
        {
            Console.WriteLine(car.Id);
            _carService.AddCar(car);
            return Created();
        }
    }
}
