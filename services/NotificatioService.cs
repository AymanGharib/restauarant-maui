namespace MauiMySQLDemo.Services;

public static class NotificationService
{
    public static Task ShowSuccess(string title, string message) =>
        Application.Current!.MainPage!.DisplayAlert(title, message, "OK");

    public static Task ShowError(string title, string message) =>
        Application.Current!.MainPage!.DisplayAlert(title, message, "Close");

    public static Task<bool> ShowConfirmation(string title, string message) =>
        Application.Current!.MainPage!.DisplayAlert(title, message, "Yes", "Cancel");
}
