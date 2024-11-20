using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Cars
{
  public class CarCreateDto
  {
     public string Name { get; set; }

     public string Model { get; set; }
  }
}