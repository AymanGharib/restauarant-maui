using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class TableService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

  public List<Table> GetAllTables()
{
    try
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("SELECT Id, `Number`, Seats FROM `Tables`;", conn);
        using var reader = cmd.ExecuteReader();

        var tables = new List<Table>();
        while (reader.Read())
        {
            tables.Add(new Table
            {
                Id     = reader.GetInt32("Id"),
                Number = reader.GetInt32("Number"),
                Seats  = reader.GetInt32("Seats")
            });
        }
        return tables;
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);               // VS output
        Application.Current?.MainPage?
            .DisplayAlert("DB error", ex.Message, "OK");       // visible popup
        return new();  // empty list
    }
}

}
