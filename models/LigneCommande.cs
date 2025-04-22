namespace MauiMySQLDemo.Models;
public class LigneCommande
{
    public int Id { get; set; }

    public int CommandeId { get; set; }
    public int PlatId { get; set; }
    public int Quantity { get; set; }

    // Optional: navigation
    public Plat? Plat { get; set; }
}