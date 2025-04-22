namespace MauiMySQLDemo.Models;
public class Commande
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }

    // Optional: navigation property
    public List<LigneCommande> Lignes { get; set; } = new();
}