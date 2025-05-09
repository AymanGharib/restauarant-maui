namespace MauiMySQLDemo.Models;

public class Plat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public int? TimesOrdered { get; set; }  // new field

}
