using AutoMapper; // маппер потрібен для DTO / Profiles ( profiles створенний для зручності, якщо хочеш можеш видалити )
using Microsoft.AspNetCore.Mvc;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;
using Vehicle_service.Services;

namespace Vehicle_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CarsController(CarService carService, IMapper mapper) : ControllerBase
  {
    [HttpGet]
    // task - Represents an asynchronous operation that can return a value.
    // ActionResult - A type that wraps either an TValue instance or an ActionResult. ( TValue === будь яке значення яке прийде нам )
    // List - список обʼєктів, типу JSON-a як в свагеррі в методі getAll
    public async Task<ActionResult<List<CarReadDto>>> GetAll()
    {
      var cars = await carService.GetAllAsync(); // отримуємо машини викликаючи кар-сервіс.getAllAsync
      var carDtos = mapper.Map<List<CarReadDto>>(cars); // мапимо в Dto формат ( This line uses AutoMapper to convert the list of Car entities into a list of CarReadDto objects. This is useful for sending only the necessary data to the client and adhering to data transfer best practices. )
      return Ok(carDtos); // повертаємо форматовану відповідь
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarReadDto>> GetById(int id)
    {
      var car = await carService.GetCarAsync(id);
      if (car == null)
      {
        return NotFound();
      }

      var carDto = mapper.Map<CarReadDto>(car);
      return Ok(carDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var isRemoved = await carService.DeleteCarAsync(id);
      if (!isRemoved)
      {
        return NotFound();
      }

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CarUpdateDto carUpdateDto)
    {
      if (id != carUpdateDto.Id)
      {
        return BadRequest();
      }

      var car = await carService.GetCarAsync(id);
      if (car == null)
      {
        return NotFound();
      }

      mapper.Map(carUpdateDto, car);
      await carService.UpdateCarAsync(car);

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CarReadDto>> Create([FromBody] CarCreateDto carCreateDto)
    {
      var car = mapper.Map<Car>(carCreateDto);
      await carService.AddCarAsync(car);

      var carReadDto = mapper.Map<CarReadDto>(car);
      return CreatedAtAction(nameof(GetById), new { id = car.Id }, carReadDto);
    }
  }
}