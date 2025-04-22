using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class PlatService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public List<Plat> GetAllPlats()
    {
        var plats = new List<Plat>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("SELECT * FROM Plats", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            plats.Add(new Plat
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Description = reader["Description"]?.ToString(),
                Price = reader.GetDecimal("Price"),
                Category = reader["Category"]?.ToString(),
                ImageUrl = reader["ImageUrl"]?.ToString()
            });
        }

        return plats;
    }
}
