using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMySQLDemo.Models;
using MauiMySQLDemo.Services;
using System.Collections.ObjectModel;

namespace MauiMySQLDemo.ViewModels;

public partial class AdminViewModel : ObservableObject
{
    private readonly AdminService _adminService = new();

    public ObservableCollection<Plat> Plats { get; } = new();

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string description = string.Empty;
    [ObservableProperty] private decimal price = 0;
    [ObservableProperty] private string category = string.Empty;
    [ObservableProperty] private string imageUrl = string.Empty;
    [ObservableProperty] private Plat? selectedPlat;

    public AdminViewModel()
    {
        LoadPlats();
    }

    [RelayCommand]
    private void LoadPlats()
    {
        Plats.Clear();
        foreach (var p in _adminService.GetAllPlats())
            Plats.Add(p);
    }

    [RelayCommand]
    private void AddPlat()
    {
        var plat = new Plat
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Category = Category,
            ImageUrl = ImageUrl
        };

        _adminService.AddPlat(plat);
        LoadPlats();
        ClearForm();
    }

    [RelayCommand]
    private void DeletePlat()
    {
        if (SelectedPlat is null) return;
        _adminService.DeletePlat(SelectedPlat.Id);
        LoadPlats();
        SelectedPlat = null;
    }

    [RelayCommand]
    private void UpdatePlat()
    {
        if (SelectedPlat is null) return;
        SelectedPlat.Name = Name;
        SelectedPlat.Description = Description;
        SelectedPlat.Price = Price;
        SelectedPlat.Category = Category;
        SelectedPlat.ImageUrl = ImageUrl;

        _adminService.UpdatePlat(SelectedPlat);
        LoadPlats();
        SelectedPlat = null;
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        Category = string.Empty;
        ImageUrl = string.Empty;
    }
}
