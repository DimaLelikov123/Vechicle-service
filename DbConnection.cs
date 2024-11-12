using Npgsql;

namespace Vehicle_service;

public class DatabaseConnection
{
  public void ConnectToDatabase()
  {
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    Console.WriteLine("Connected to the database successfully!");
  }
}