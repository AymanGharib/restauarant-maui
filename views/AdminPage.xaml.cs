namespace MauiMySQLDemo.Views;

public partial class AdminPage : ContentPage
{
    public AdminPage()
    {
        InitializeComponent();
    }

    private async void OnAddDishClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new AddDishPage());

    private async void OnManageDishesClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new ManageDishesPage());

    private async void OnManageReservationsClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new ManageReservationsPage());
}
