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

    // SINGLE source‑generated property ↓↓↓
    [ObservableProperty]
    private DateTime selectedDate = DateTime.Today;

    public ReservationViewModel() => LoadTables();

    // will be called automatically when SelectedDate changes
    partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue) => LoadTables();

    // ---------------------------------------------------------

    private void LoadTables()
    {
        var start = SelectedDate.Date;      // 00:00 of the day
        var end   = start.AddDays(1);       // 00:00 next day

        var busy = _reservationSrv.GetReservedTableIds(start, end);

        Tables.Clear();
        foreach (var t in _tableSrv.GetAllTables())
            Tables.Add(new TableViewItem
            {
                Table      = t,
                IsReserved = busy.Contains(t.Id)
            });
    }

    // Toolkit will generate ReserveCommand
    [RelayCommand]
    private async Task ReserveAsync(TableViewItem item)
    {
        if (item.IsReserved) return;

        var name = await Application.Current.MainPage!
            .DisplayPromptAsync("Your name", "Enter reservation name:");
        if (string.IsNullOrWhiteSpace(name)) return;

        _reservationSrv.AddReservation(new Reservation
        {
            TableId = item.Table.Id,
            Name    = name,
            Date    = SelectedDate.Date + new TimeSpan(19, 0, 0) // 19:00
        });

        LoadTables();

        await NotificationService.ShowSuccess(
            "Reservation confirmed",
            $"Table {item.Table.Number} reserved for {SelectedDate:d}");
    }
}
