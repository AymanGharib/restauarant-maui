using MauiMySQLDemo.Views;

namespace MauiMySQLDemo.Views;
using MauiMySQLDemo.ViewModels;


public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
        BindingContext = new MenuViewModel(); // no need to pass Navigation anymore
    }

  private async void OnCartClicked(object sender, EventArgs e)
    {
        // Create new instance of CartPage with fresh ViewModel
        await Navigation.PushAsync(new CartPage());
    }


private async void OnTablesClicked(object sender, EventArgs e)
    => await Navigation.PushAsync(new ReservationPage());





}