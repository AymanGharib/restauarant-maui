using MySql.Data.MySqlClient;
using MauiMySQLDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiMySQLDemo.Services;

public class ReservationService
{
    private readonly string _cs =
        "Server=localhost;Database=restaurant;Uid=root;Pwd=yourpassword;";

    /* ------------------------------------------------------------
     *  ADD reservation + increment counter (single transaction)
     * -----------------------------------------------------------*/
    public async Task AddReservation(Reservation r)
    {
        await using var conn = new MySqlConnection(_cs);
        await conn.OpenAsync();
        await using var tx   = await conn.BeginTransactionAsync();

        try
        {
            /* insert row in Reservations */
            const string ins = @"
                INSERT INTO Reservations (TableId, Name, Date, Time)
                VALUES (@tid, @name, @date, @time)";
            await using (var cmd = new MySqlCommand(ins, conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@tid",  r.TableId);
                cmd.Parameters.AddWithValue("@name", r.Name);
                cmd.Parameters.AddWithValue("@date", r.Date.Date);
                cmd.Parameters.AddWithValue("@time", r.Date.TimeOfDay);
                await cmd.ExecuteNonQueryAsync();
            }

            /* bump TimesReserved */
            const string upd = @"
                UPDATE `Tables`
                SET TimesReserved = COALESCE(TimesReserved,0) + 1
                WHERE Id = @tid";
            await using (var cmd = new MySqlCommand(upd, conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@tid", r.TableId);
                await cmd.ExecuteNonQueryAsync();
            }

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;                          // propagate for UI to handle
        }
    }

    /* unchanged: get IDs already reserved in a given range */
    public List<int> GetReservedTableIds(DateTime start, DateTime end)
    {
        var list = new List<int>();

        using var conn = new MySqlConnection(_cs);
        conn.Open();

        const string sql = @"
            SELECT TableId
            FROM Reservations
            WHERE `Date` >= @start AND `Date` < @end";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.Add("@start", MySqlDbType.DateTime).Value = start;
        cmd.Parameters.Add("@end",   MySqlDbType.DateTime).Value = end;

        using var rdr = cmd.ExecuteReader();
        while (rdr.Read())
            list.Add(rdr.GetInt32(0));

        return list;
    }
}
