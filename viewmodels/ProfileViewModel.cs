using CommunityToolkit.Mvvm.ComponentModel;
using MauiMySQLDemo;

namespace MauiMySQLDemo.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string role = string.Empty;

    public ProfileViewModel()
    {
        var user = AppData.CurrentUser;
        if (user != null)
        {
            Name = user.Name;
            Email = user.Email;
            Role = user.Role;
        }
    }
}
