using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Services;

public class ReservationService
{
    private readonly string _connectionString =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    public void AddReservation(Reservation reservation)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO Reservations (TableId, Name, Date, Time) VALUES (@tid, @name, @date, @time)", conn);
        cmd.Parameters.AddWithValue("@tid", reservation.TableId);
        cmd.Parameters.AddWithValue("@name", reservation.Name);
        cmd.Parameters.AddWithValue("@date", reservation.Date.Date);
        cmd.Parameters.AddWithValue("@time", reservation.Date.TimeOfDay);

        cmd.ExecuteNonQuery();
    }
// in ReservationService
public List<int> GetReservedTableIds(DateTime selectedDate)
{
    var reserved = new List<int>();
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    // define the start of the day and the start of the next day
    var start = selectedDate.Date;
    var end   = start.AddDays(1);

    using var cmd = new MySqlCommand(@"
        SELECT TableId
          FROM Reservations
         WHERE `Date` >= @start
           AND `Date` <  @end
    ", conn);

    cmd.Parameters.Add("@start", MySqlDbType.DateTime).Value = start;
    cmd.Parameters.Add("@end",   MySqlDbType.DateTime).Value = end;

    using var rdr = cmd.ExecuteReader();
    while (rdr.Read())
        reserved.Add(rdr.GetInt32(0));

    return reserved;
}



}
