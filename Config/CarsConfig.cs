namespace Vehicle_service.Config;

public class CarsConfig
{
  public bool AllowCreate { get; set; } = true;
  public bool EnableGetAll { get; set; } = true;
  public int MaxCarsLimit { get; set; } = 100;
}