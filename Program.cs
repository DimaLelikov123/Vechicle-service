using Vehicle_service.Config;

var builder = WebApplication.CreateBuilder(args);

builder.DefineEnvironment();

builder.Services.RegisterApplicationServices(builder.Configuration);

var app = builder.Build();
app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
  app.UseSwaggerConfiguration();
}

app.UseAuthorization();
app.MapControllers();
app.Run();