using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using Vehicle_service.Data;
using Vehicle_service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VehicleContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "Repair Service",
    Description = "Swagger documentation for repair service",
    Contact = new OpenApiContact
    {
      Name = "Dmytro",
      Email = "lelikovdima8@gmail.com"
    }
  });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<VehicleContext>();
  dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
  });
}

app.UseAuthorization();
app.MapControllers();
app.Run();