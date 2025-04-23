using MauiMySQLDemo.Models;

namespace MauiMySQLDemo.Views;

public partial class PlatDetailModal : ContentPage
{
    public PlatDetailModal(Plat plat)
    {
        InitializeComponent();

        NameLabel.Text        = plat.Name;
        CategoryLabel.Text    = $"Category: {plat.Category}";
        PriceLabel.Text       = $"Price: {plat.Price} MAD";
        DescriptionLabel.Text = plat.Description;
        PlatImage.Source      = plat.ImageUrl;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
