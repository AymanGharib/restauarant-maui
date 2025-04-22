using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiMySQLDemo.Models;
using MauiMySQLDemo.Services;
using MauiMySQLDemo.Views;

namespace MauiMySQLDemo.ViewModels;

public class MenuViewModel
{
    public ObservableCollection<Plat> Plats { get; set; } = new();
    public ICommand AddToCartCommand { get; }

    private readonly PlatService _platService = new();
    private readonly CartService _cartService = CartService.Instance;

    public MenuViewModel()
    {
        AddToCartCommand = new Command<Plat>(AddToCart);
        LoadPlats();
    }

    private void LoadPlats()
    {
        var plats = _platService.GetAllPlats();
        foreach (var plat in plats)
            Plats.Add(plat);
    }

    private void AddToCart(Plat plat)
    {
        _cartService.AddItem(plat);

    }
}