namespace MauiMySQLDemo.Models;


public class Reservation
{
    public int Id { get; set; }

    public int TableId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; } // both date and time in one

    // Optional: navigation
    public Table? Table { get; set; }
}
