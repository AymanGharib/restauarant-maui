using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMySQLDemo.Models;
using MauiMySQLDemo.Services;

namespace MauiMySQLDemo.ViewModels;

// one partial class – the source‑generator adds the other half
public partial class CartViewModel : ObservableObject
{
    private readonly CartService _cartService = CartService.Instance;

    // Observable collection of real objects (not tuples)
    public ObservableCollection<CartItem> CartItems { get; } = new();

    // Generates: backing field + property + change notification
    [ObservableProperty] 
    private string totalText = string.Empty;

    public CartViewModel()
    {
        LoadCart();
        UpdateTotalText();
    }

    private void LoadCart()
    {
        CartItems.Clear();
        foreach (var item in _cartService.Items)
            CartItems.Add(item);
    }

    private void UpdateTotalText()
        => TotalText = $"Total: {_cartService.GetTotal():F2} MAD";

    // Generates ValidateOrderCommand for you
    [RelayCommand]
    private void ValidateOrder()
    {
        var total = _cartService.GetTotal();
        var commandeId = new CommandeService().CreateCommande(total);

        var ligneService = new LigneCommandeService();
        foreach (var ci in _cartService.Items)
            ligneService.AddLigneCommande(commandeId, ci.Plat.Id, ci.Quantity);

        _cartService.Clear();
        LoadCart();
        UpdateTotalText();

        Application.Current?.MainPage?
            .DisplayAlert("Success", "Order validated!", "OK");
    }
}
