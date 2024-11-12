using Microsoft.OpenApi.Models;
using Vehicle_service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CarService>();

builder.Services.AddSingleton<OrderService>();

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Repair service",
        Description = "Swagger documentation for repair service",
        Contact = new OpenApiContact
        {
            Name = "Dmytro",
            Email = "lelikovdima8@gmail.com"
        }
    });
});

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;  // Set Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
