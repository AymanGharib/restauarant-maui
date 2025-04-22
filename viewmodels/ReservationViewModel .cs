using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMySQLDemo.Models;
using MauiMySQLDemo.Services;

namespace MauiMySQLDemo.ViewModels;

public partial class ReservationViewModel : ObservableObject
{
    private readonly TableService       _tableSrv       = new();
    private readonly ReservationService _reservationSrv = new();

    public ObservableCollection<TableViewItem> Tables { get; } = new();

    [ObservableProperty] private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Today);

    public ReservationViewModel() => LoadTables();

    partial void OnSelectedDateChanged(DateOnly oldValue, DateOnly newValue) => LoadTables();

// in your ReservationViewModel
private void LoadTables()
{
    Tables.Clear();

    // convert your DateOnly to a DateTime at midnight
    var date = SelectedDate.ToDateTime(TimeOnly.MinValue);

    // get the list of reserved table IDs _for that date only_
    var busy = _reservationSrv.GetReservedTableIds(date);

    foreach (var t in _tableSrv.GetAllTables())
        Tables.Add(new TableViewItem {
            Table      = t,
            IsReserved = busy.Contains(t.Id)
        });
}

    // The Toolkit generates ReserveCommand (IAsyncRelayCommand<TableViewItem>)
[RelayCommand]
private async Task ReserveAsync(TableViewItem item)
{
    if (item.IsReserved)
        return; // already booked

    // 1) prompt user for name
    var name = await Application.Current!
                     .MainPage!
                     .DisplayPromptAsync("Your name", "Enter reservation name:");
    if (string.IsNullOrWhiteSpace(name))
        return;

    // 2) insert into DB
    _reservationSrv.AddReservation(new Reservation
    {
        TableId = item.Table.Id,
        Name    = name,
        // store full DateTime (we only filter by date when reading)
        Date    = SelectedDate.ToDateTime(new TimeOnly(19, 0))
    });

    // 3) reload everything so busy‑flags get recalculated
    LoadTables();

    // 4) confirm to user
    await Application.Current
                   .MainPage
                   .DisplayAlert("Booked",
                                 $"Table {item.Table.Number} reserved for {SelectedDate:d}.",
                                 "OK");
}

}
