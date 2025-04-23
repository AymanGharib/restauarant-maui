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


    private async void OnStatisticsClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new StatisticsPage());

        private async void OnProfileClicked(object sender, EventArgs e)
    => await Navigation.PushAsync(new ProfilePage());

protected override void OnAppearing()
{
    base.OnAppearing();

    // Only show admin tab if current user is admin
    if (AppData.CurrentUser?.Role == "admin" && !ToolbarItems.Any(t => t.Text == "Admin"))
    {
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "Admin",
            IconImageSource = "admin.png",
            Order = ToolbarItemOrder.Primary,
            Priority = 4,
            Command = new Command(async () => await Navigation.PushAsync(new AdminPage()))
        });
    }
}



}