namespace Vehicle_service.Config;

public class OrdersConfig
{
  public bool EnableGetAll { get; set; } = true;
  public int MaxOrdersLimit { get; set; } = 100;
}