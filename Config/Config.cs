using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Vehicle_service.Data;
using Vehicle_service.FluentValidation.CarsFluentValidation;
using Vehicle_service.Repositories;
using Vehicle_service.Repositories.Impl;
using Vehicle_service.Services;

namespace Vehicle_service.Config
{
  public static class Config
  {
    public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
      // Database context
      services.AddDbContext<VehicleContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      // FluentValidation
      services.AddControllers()
        .AddFluentValidation(config =>
        {
          config.RegisterValidatorsFromAssemblyContaining<CarCreateDtoValidator>();
          config.DisableDataAnnotationsValidation = true;
        });

      // Memory cache
      services.AddMemoryCache();

      // AutoMapper
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      // Repositories
      services.AddScoped<ICarRepository, CarRepositoryImpl>();
      services.AddScoped<IOrderRepository, OrderRepositoryImpl>();

      // Services
      services.AddScoped<CarService>();
      services.AddScoped<OrderService>();
      
      // Api config
      services.Configure<CarsConfig>(configuration.GetSection("CarsConfig"));
      
      // Swagger
      services.AddSwaggerConfiguration();
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using var scope = app.ApplicationServices.CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<VehicleContext>();
      dbContext.Database.Migrate();
    }
  }
}