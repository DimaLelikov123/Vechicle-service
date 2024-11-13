using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Cars
{
  public class CarCreateDto
  {
    
    [Required] public string Name { get; set; } = null!;

    [Required] public string Model { get; set; } = null!;
  }
}