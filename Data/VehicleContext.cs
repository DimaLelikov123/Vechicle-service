using Microsoft.EntityFrameworkCore;
using Vehicle_service.Models;

namespace Vehicle_service.Data
{
    public class VehicleContext(DbContextOptions<VehicleContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}