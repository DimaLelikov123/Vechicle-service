using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Cars
{
  public class CarUpdateDto
  {
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;

    [Required] public string Model { get; set; } = null!;
  }
}