using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MauiMySQLDemo.Services;

public class StatisticsService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public List<(string Name, int Count)> GetTopDishes(int limit = 5)
    {
        var results = new List<(string, int)>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand(@"
            SELECT `Name`, `TimesOrdered`
            FROM `Plats`
            ORDER BY `TimesOrdered` DESC
            LIMIT @limit", conn);
        cmd.Parameters.AddWithValue("@limit", limit);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            results.Add((reader.GetString(0), reader.GetInt32(1)));

        return results;
    }

    public List<(string TableLabel, int Count)> GetTopReservedTables()
    {
        var results = new List<(string, int)>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand(@"
            SELECT `Number`, `TimesReserved` FROM `Tables`;
", conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var label = $"Table {reader.GetInt32(0)}";
            var count = reader.GetInt32(1);
            results.Add((label, count));
        }

        return results;
    }
}
