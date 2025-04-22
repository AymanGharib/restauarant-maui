using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiMySQLDemo.Services;

public class PlatService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    /* -------------------------------------------------
     * 1)  LOAD ALL PLATS (now includes TimesOrdered)
     * ------------------------------------------------*/
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
                Id          = reader.GetInt32   ("Id"),
                Name        = reader.GetString  ("Name"),
                Description = reader["Description"]?.ToString(),
                Price       = reader.GetDecimal ("Price"),
                Category    = reader["Category"]?.ToString(),
                ImageUrl    = reader["ImageUrl"]?.ToString(),

                // new ↓↓↓
                TimesOrdered = reader["TimesOrdered"] is DBNull
                               ? 0
                               : reader.GetInt32("TimesOrdered")
            });
        }

        return plats;
    }

    /* -------------------------------------------------
     * 2)  INCREMENT TimesOrdered
     * ------------------------------------------------*/
    public async Task IncrementTimesOrderedAsync(int platId, int delta = 1)
    {
        await using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();

        const string sql = @"
            UPDATE Plats
            SET TimesOrdered = COALESCE(TimesOrdered,0) + @delta
            WHERE Id = @id";

        await using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@delta", delta);
        cmd.Parameters.AddWithValue("@id",    platId);

        await cmd.ExecuteNonQueryAsync();
    }
}
