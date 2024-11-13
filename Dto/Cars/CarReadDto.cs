namespace Vehicle_service.Dto.Cars
{
  public class CarReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
  }
}