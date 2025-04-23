using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiMySQLDemo.Services;

public class AdminService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    // Get all dishes
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
                ImageUrl = reader["ImageUrl"]?.ToString(),
                TimesOrdered = reader["TimesOrdered"] is DBNull ? 0 : reader.GetInt32("TimesOrdered")
            });
        }

        return plats;
    }

    // Add a new dish
    public void AddPlat(Plat plat)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand(@"
            INSERT INTO Plats (Name, Description, Price, Category, ImageUrl, TimesOrdered)
            VALUES (@name, @desc, @price, @cat, @img, 0)", conn);

        cmd.Parameters.AddWithValue("@name", plat.Name);
        cmd.Parameters.AddWithValue("@desc", plat.Description);
        cmd.Parameters.AddWithValue("@price", plat.Price);
        cmd.Parameters.AddWithValue("@cat", plat.Category);
        cmd.Parameters.AddWithValue("@img", plat.ImageUrl);

        cmd.ExecuteNonQuery();
    }

    // Delete a dish
    public void DeletePlat(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("DELETE FROM Plats WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    // Update a dish
    public void UpdatePlat(Plat plat)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand(@"
            UPDATE Plats
            SET Name = @name, Description = @desc, Price = @price, 
                Category = @cat, ImageUrl = @img
            WHERE Id = @id", conn);

        cmd.Parameters.AddWithValue("@id", plat.Id);
        cmd.Parameters.AddWithValue("@name", plat.Name);
        cmd.Parameters.AddWithValue("@desc", plat.Description);
        cmd.Parameters.AddWithValue("@price", plat.Price);
        cmd.Parameters.AddWithValue("@cat", plat.Category);
        cmd.Parameters.AddWithValue("@img", plat.ImageUrl);

        cmd.ExecuteNonQuery();
    }
} 
