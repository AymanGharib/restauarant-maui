// Services/CartService.cs
using CommunityToolkit.Mvvm.ComponentModel;
using MauiMySQLDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauiMySQLDemo.Services;

public partial class CartService : ObservableObject
{
    public static CartService Instance { get; } = new();

    private readonly PlatService _platRepo = new();   // ← new helper

    public List<CartItem> Items { get; } = new();

    public async Task AddItem(Plat plat)
    {
        var delta = 1;                // how many we’re about to add
        var existing = Items.FirstOrDefault(i => i.Plat.Id == plat.Id);

        if (existing is not null)
        {
            existing.Quantity += delta;
        }
        else
        {
            Items.Add(new CartItem { Plat = plat, Quantity = delta });
        }

        OnPropertyChanged(nameof(Items));

        // kick the DB update to the thread‑pool so the UI never blocks
        _ = Task.Run(() => _platRepo.IncrementTimesOrderedAsync(plat.Id, delta));
    }

    public decimal GetTotal() =>
        Items.Sum(i => i.Plat.Price * i.Quantity);

    public void Clear()
    {
        Items.Clear();
        OnPropertyChanged(nameof(Items));
    }
}
