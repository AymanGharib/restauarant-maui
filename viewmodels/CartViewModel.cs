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
private async Task ValidateOrderAsync()
{
  if (_cartService.Items.Count == 0)
    {
        await NotificationService.ShowError("Cart is empty", "Please add items before validating.");
        return;
    }

    // Step 1: Prompt for dummy payment info
    var name = await Application.Current!.MainPage!
        .DisplayPromptAsync("Payment Info", "Enter your full name:");
    if (string.IsNullOrWhiteSpace(name)) return;

    var bankId = await Application.Current!.MainPage!
        .DisplayPromptAsync("Payment Info", "Enter your Bank ID:");
    if (string.IsNullOrWhiteSpace(bankId)) return;

    var code = await Application.Current!.MainPage!
        .DisplayPromptAsync("Payment Info", "Enter your Security Code:");
    if (string.IsNullOrWhiteSpace(code)) return;

    // Step 2: Continue with your original order logic
    var total = _cartService.GetTotal();
    var commandeId = new CommandeService().CreateCommande(total);

    var ligneService = new LigneCommandeService();
    foreach (var ci in _cartService.Items)
        ligneService.AddLigneCommande(commandeId, ci.Plat.Id, ci.Quantity);

    // Step 3: Clear cart, reload UI
    _cartService.Clear();
    LoadCart();
    UpdateTotalText();

    // Step 4: Success notification
 await NotificationService.ShowSuccess("Payment Confirmed", $"Thank you {name}, your order has been placed!");
}

}
