using Microsoft.Extensions.Configuration;
using Npgsql;

namespace studentManagementSystem.Database;

public class DatabaseService(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public void TestConnection()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        Console.WriteLine("Connected to PostgreSQL successfully!");
    }
}