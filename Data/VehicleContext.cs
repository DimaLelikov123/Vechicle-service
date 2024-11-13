using Microsoft.EntityFrameworkCore;
using Vehicle_service.Models;

namespace Vehicle_service.Data
{
  public class VehicleContext : DbContext // DbContext це тип з самих шарпів
  {
    public VehicleContext(DbContextOptions<VehicleContext> options)
      : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
  }
}