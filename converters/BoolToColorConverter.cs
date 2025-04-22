// Converters/BoolToColorConverter.cs
namespace MauiMySQLDemo.Converters;

using System.Globalization;
using Microsoft.Maui.Controls;

public class BoolToColorConverter : IValueConverter
{
    // ConverterParameter must be "reservedColor,freeColor"
    public object Convert(object value, Type t, object parameter, CultureInfo culture)
    {
        var colors = (parameter as string)?.Split(',') ?? new[] { "#FFCDD2", "#C8E6C9" };
        return (bool)value ? Color.FromArgb(colors[0]) : Color.FromArgb(colors[1]);
    }
    public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
}
