namespace MauiMySQLDemo.Models;


public class Table
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Seats { get; set; }
    public int? TimesReserved { get; set; }  // new field

}
