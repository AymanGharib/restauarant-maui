using MauiMySQLDemo.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices; // Add this for CallerMemberName
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



namespace MauiMySQLDemo.Services;
public class CartService : ObservableObject
{
    public static CartService Instance { get; } = new();

    public List<CartItem> Items { get; } = new();

    public void AddItem(Plat plat)
    {
        var existing = Items.FirstOrDefault(i => i.Plat.Id == plat.Id);
        if (existing is not null)
            existing.Quantity++;
        else
            Items.Add(new CartItem { Plat = plat, Quantity = 1 });

        OnPropertyChanged(nameof(Items));   // notifies view‑models
    }

    public decimal GetTotal() => Items.Sum(i => i.Plat.Price * i.Quantity);

    public void Clear()
    {
        Items.Clear();
        OnPropertyChanged(nameof(Items));
    }
}
