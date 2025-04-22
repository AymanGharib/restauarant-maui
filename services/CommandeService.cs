using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class CommandeService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public int CreateCommande(decimal total)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO Commandes (Date, Total) VALUES (@date, @total); SELECT LAST_INSERT_ID();", conn);
        cmd.Parameters.AddWithValue("@date", DateTime.Now);
        cmd.Parameters.AddWithValue("@total", total);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}
