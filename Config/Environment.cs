namespace Vehicle_service.Config
{
  public static class EnvironmentConfigExtensions
  {
    public static void DefineEnvironment(this WebApplicationBuilder builder)
    {
      builder.Configuration
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    }
  }
}