using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class LigneCommandeService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public void AddLigneCommande(int commandeId, int platId, int quantity)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO LigneCommande (CommandeId, PlatId, Quantity) VALUES (@cid, @pid, @qty)", conn);
        cmd.Parameters.AddWithValue("@cid", commandeId);
        cmd.Parameters.AddWithValue("@pid", platId);
        cmd.Parameters.AddWithValue("@qty", quantity);

        cmd.ExecuteNonQuery();
    }
}
