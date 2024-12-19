using Microsoft.OpenApi.Models;

namespace Vehicle_service.Config
{
  public static class SwaggerConfig
  {
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(c =>
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
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
      });
    }
  }
}