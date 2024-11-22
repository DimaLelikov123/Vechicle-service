using System.ComponentModel.DataAnnotations;

namespace Vehicle_service.Dto.Cars
{
  public class CarUpdateDto
  {
    [Key] public int Id { get; set; }
    
    public string Name { get; set; }

    public string Model { get; set; }
  }
}