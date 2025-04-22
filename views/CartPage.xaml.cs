using MauiMySQLDemo.Views;

namespace MauiMySQLDemo.Views;
using MauiMySQLDemo.ViewModels;


public partial class CartPage : ContentPage
{
    public CartPage()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }
}