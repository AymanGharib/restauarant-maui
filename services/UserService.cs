using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class UserService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public User? Authenticate(string email, string password)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("SELECT * FROM Users WHERE Email = @Email", conn);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var storedHash = reader.GetString("PasswordHash");

            // Replace this with a real hash check
            if (storedHash == password)  // TEMP: use hashing in production!
            {
                return new User
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    PasswordHash = storedHash,
                    Role = reader.GetString("Role")
                };
            }
        }

        return null;
    }
}
