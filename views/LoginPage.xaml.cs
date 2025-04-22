// ===============================
// LoginPage.xaml.cs
// ===============================
using MauiMySQLDemo.Models;
using MauiMySQLDemo.Services;

namespace MauiMySQLDemo.Views;

public partial class LoginPage : ContentPage
{
    private readonly UserService _userService = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
            return;
        }

        var user = _userService.Authenticate(email, password);
        if (user != null)
        {
            AppData.CurrentUser = user; // Store user session globally
            await Navigation.PushAsync(new MenuPage());
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid email or password.", "Try Again");
        }
    }
}
